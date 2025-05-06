using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormHistorialPagosAdmin: Form
    {
        public FormHistorialPagosAdmin()
        {
            InitializeComponent();

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarHistorialPagos();
            ConfigurarDataGridView();
            dataGridViewHistorial.MultiSelect = false;
        }

        private void CargarHistorialPagos()
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
            SELECT 
                p.id_pago AS 'ID Pago',
                p.id_orden AS 'Número Orden',
                o.nombreCliente AS 'Cliente',
                p.monto AS 'Total',
                CASE
                    WHEN td.es_porcentaje = 1 THEN (p.monto / (1 - (p.descuento/100))) - p.monto
                    ELSE p.descuento
                END AS 'Descuento',
                p.recibido AS 'Recibido',
                p.cambio AS 'Cambio',
                tp.nombrePago AS 'Método Pago',
                u.nombre AS 'Cajero',
                DATE_FORMAT(p.fecha_pago, '%Y-%m-%d %H:%i') AS 'Fecha/Hora',
                CASE
                    WHEN td.es_porcentaje = 1 THEN CONCAT(p.descuento, '%')
                    ELSE CONCAT('$', FORMAT(p.descuento, 2))
                END AS 'Tipo Descuento'
            FROM pagos p
            JOIN ordenes o ON p.id_orden = o.id_orden
            JOIN tipoPago tp ON p.id_tipo_pago = tp.id_pago
            JOIN usuarios u ON p.id_usuario = u.id_usuario
            LEFT JOIN tipo_descuento td ON p.id_tipo_descuento = td.id_tipo_descuento
            ORDER BY p.fecha_pago DESC";

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conexion);
                    da.Fill(dt);

                    dataGridViewHistorial.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial de pagos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ConfigurarDataGridView()
        {
            dataGridViewHistorial.ReadOnly = true;
            dataGridViewHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHistorial.MultiSelect = false;
            dataGridViewHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Formato de columnas monetarias
            string[] columnasMonetarias = { "Total", "Descuento", "Recibido", "Cambio" };
            foreach (string columna in columnasMonetarias)
            {
                if (dataGridViewHistorial.Columns[columna] != null)
                {
                    dataGridViewHistorial.Columns[columna].DefaultCellStyle.Format = "C";
                }
            }

            // Ocultar columna de tipo de descuento (es informativa)
            if (dataGridViewHistorial.Columns["Tipo Descuento"] != null)
            {
                dataGridViewHistorial.Columns["Tipo Descuento"].Visible = false;
            }
        }


        private void MostrarFacturaCompleta(int idOrden)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    // Obtener información básica de la orden incluyendo tipo de descuento
                    string queryOrden = @"
            SELECT 
                o.id_orden, o.nombreCliente, o.total, o.descuento, 
                DATE_FORMAT(o.fecha_orden, '%Y-%m-%d %H:%i') AS fecha_orden,
                m.nombreMesa AS mesa,
                tp.nombrePago AS metodo_pago,
                u.nombre AS mesero,
                td.nombre AS tipo_descuento_nombre,
                td.es_porcentaje AS descuento_es_porcentaje
            FROM ordenes o
            JOIN mesas m ON o.id_mesa = m.id_mesa
            JOIN tipoPago tp ON o.tipo_pago = tp.id_pago
            JOIN usuarios u ON o.id_usuario = u.id_usuario
            LEFT JOIN pagos p ON o.id_orden = p.id_orden
            LEFT JOIN tipo_descuento td ON p.id_tipo_descuento = td.id_tipo_descuento
            WHERE o.id_orden = @idOrden";

                    string cliente = "";
                    decimal total = 0;
                    decimal descuento = 0;
                    string fechaOrden = "";
                    string mesa = "";
                    string metodoPago = "";
                    string mesero = "";
                    string tipoDescuentoNombre = "Ninguno";
                    bool descuentoEsPorcentaje = false;

                    using (MySqlCommand cmd = new MySqlCommand(queryOrden, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente = reader["nombreCliente"].ToString();
                                total = Convert.ToDecimal(reader["total"]);
                                descuento = Convert.ToDecimal(reader["descuento"]);
                                fechaOrden = reader["fecha_orden"].ToString();
                                mesa = reader["mesa"].ToString();
                                metodoPago = reader["metodo_pago"].ToString();
                                mesero = reader["mesero"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("tipo_descuento_nombre")))
                                {
                                    tipoDescuentoNombre = reader["tipo_descuento_nombre"].ToString();
                                    descuentoEsPorcentaje = Convert.ToBoolean(reader["descuento_es_porcentaje"]);
                                }
                            }
                        }
                    }


                    string queryPedidos = @"
                                    SELECT 
                                        COALESCE(
                                            pl.nombrePlato,
                                            (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = p.id_bebida),
                                            (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = p.id_extra),
                                            (SELECT pr.nombre FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                                        ) AS producto,
                                        p.Cantidad,
                                        COALESCE(
                                            pl.precioUnitario,
                                            (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                                            (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra),
                                            (SELECT pr.precio_especial FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                                        ) AS precio_unitario,
                                        (p.Cantidad * COALESCE(
                                            pl.precioUnitario,
                                            (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                                            (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra),
                                            (SELECT pr.precio_especial FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                                        )) AS subtotal,
                                        CASE
                                            WHEN p.id_promocion IS NOT NULL THEN 1
                                            ELSE 0
                                        END AS es_promocion,
                                        p.id_promocion
                                    FROM pedidos p
                                    LEFT JOIN platos pl ON p.id_plato = pl.id_plato
                                    LEFT JOIN promociones pr ON p.id_promocion = pr.id_promocion
                                    WHERE p.id_orden = @idOrden
                                    ORDER BY es_promocion DESC, producto ASC";

                    StringBuilder detallePedidos = new StringBuilder();
                    decimal totalCalculado = 0;

                    using (MySqlCommand cmd = new MySqlCommand(queryPedidos, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string producto = reader["producto"].ToString();
                                int cantidad = Convert.ToInt32(reader["Cantidad"]);
                                decimal precioUnitario = Convert.ToDecimal(reader["precio_unitario"]);
                                decimal subtotal = Convert.ToDecimal(reader["subtotal"]);
                                bool esPromocion = Convert.ToInt32(reader["es_promocion"]) == 1;
                                int? idPromocion = reader["id_promocion"] as int?;

                                detallePedidos.AppendLine($"{producto} x{cantidad} - {precioUnitario.ToString("C")} = {subtotal.ToString("C")}");
                                totalCalculado += subtotal;

                                // Si es promoción, agregar sus items
                                if (esPromocion && idPromocion.HasValue)
                                {
                                    detallePedidos.AppendLine(ObtenerItemsPromocion(idPromocion.Value, cantidad));
                                }
                            }
                        }
                    }

                    // Obtener información del pago (si existe)
                    string queryPago = @"
                    SELECT 
                        p.recibido, p.cambio, 
                        DATE_FORMAT(p.fecha_pago, '%Y-%m-%d %H:%i') AS fecha_pago,
                        u.nombre AS cajero
                    FROM pagos p
                    JOIN usuarios u ON p.id_usuario = u.id_usuario
                    WHERE p.id_orden = @idOrden";

                    decimal recibido = 0;
                    decimal cambio = 0;
                    string fechaPago = "";
                    string cajero = "";

                    using (MySqlCommand cmd = new MySqlCommand(queryPago, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                recibido = Convert.ToDecimal(reader["recibido"]);
                                cambio = Convert.ToDecimal(reader["cambio"]);
                                fechaPago = reader["fecha_pago"].ToString();
                                cajero = reader["cajero"].ToString();
                            }
                        }
                    }

                    // Calcular el monto real del descuento
                    decimal montoDescuento = descuento;
                    if (descuentoEsPorcentaje)
                    {
                        montoDescuento = total / (1 - (descuento / 100)) - total;
                    }

                    // Construir el comprobante/factura
                    StringBuilder factura = new StringBuilder();
                    factura.AppendLine("══════════════════════════════════");
                    factura.AppendLine("        LA CAGUAMA RESTAURANTE    ");
                    factura.AppendLine("══════════════════════════════════");
                    factura.AppendLine($"Orden: #{idOrden}");
                    factura.AppendLine($"Fecha orden: {fechaOrden}");
                    factura.AppendLine($"Mesa: {mesa}");
                    factura.AppendLine($"Cliente: {cliente}");
                    factura.AppendLine($"Mesero: {mesero}");
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine("              DETALLE             ");
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine(detallePedidos.ToString());
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine($"Subtotal: {totalCalculado.ToString("C")}");

                    if (descuento > 0)
                    {
                        string descuentoTexto = descuentoEsPorcentaje ?
                            $"{descuento}% ( -{montoDescuento.ToString("C")} )" :
                            $"-{descuento.ToString("C")}";

                        factura.AppendLine($"Descuento ({tipoDescuentoNombre}): {descuentoTexto}");
                    }

                    factura.AppendLine($"TOTAL: {total.ToString("C")}");
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine($"Método de pago: {metodoPago}");

                    // Resto del código para mostrar el pago...
                    // (Mantén el mismo código para mostrar recibido y cambio)

                    factura.AppendLine("══════════════════════════════════");
                    factura.AppendLine("   ¡Gracias por su preferencia!   ");
                    factura.AppendLine("══════════════════════════════════");

                    // Mostrar en un MessageBox con scroll
                    using (var scrollableMessageBox = new Form()
                    {
                        Width = 500,
                        Height = 600,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = $"Factura Orden #{idOrden}",
                        StartPosition = FormStartPosition.CenterParent,
                        MaximizeBox = false,
                        MinimizeBox = false
                    })
                    {
                        var textBox = new TextBox()
                        {
                            Multiline = true,
                            Dock = DockStyle.Fill,
                            ReadOnly = true,
                            ScrollBars = ScrollBars.Vertical,
                            Text = factura.ToString(),
                            Font = new Font("Consolas", 10)
                        };

                        scrollableMessageBox.Controls.Add(textBox);
                        scrollableMessageBox.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar factura: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private Dictionary<string, int> ObtenerCantidadesItemsPromocion(int idPromocion, int cantidadPromo)
        {
            var cantidades = new Dictionary<string, int>();

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            CASE
                WHEN pi.tipo_item = 'PLATO' THEN (SELECT nombrePlato FROM platos WHERE id_plato = pi.id_item)
                WHEN pi.tipo_item = 'BEBIDA' THEN (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = pi.id_item)
                WHEN pi.tipo_item = 'EXTRA' THEN (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = pi.id_item)
            END AS nombre_item,
            pi.cantidad AS cantidad_en_promo
        FROM promocion_items pi
        WHERE pi.id_promocion = @idPromocion";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPromocion", idPromocion);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreItem = reader["nombre_item"].ToString();
                            int cantidadEnPromo = Convert.ToInt32(reader["cantidad_en_promo"]);
                            int cantidadTotal = cantidadEnPromo * cantidadPromo;

                            if (cantidades.ContainsKey(nombreItem))
                            {
                                cantidades[nombreItem] += cantidadTotal;
                            }
                            else
                            {
                                cantidades[nombreItem] = cantidadTotal;
                            }
                        }
                    }
                }
            }

            return cantidades;
        }

        private string ObtenerResumenPromociones(int idOrden)
        {
            StringBuilder resumen = new StringBuilder();

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            pr.nombre,
            p.Cantidad,
            pr.precio_especial
        FROM pedidos p
        JOIN promociones pr ON p.id_promocion = pr.id_promocion
        WHERE p.id_orden = @idOrden";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            resumen.AppendLine("PROMOCIONES INCLUIDAS:");
                            resumen.AppendLine("----------------------");

                            while (reader.Read())
                            {
                                string nombre = reader["nombre"].ToString();
                                int cantidad = Convert.ToInt32(reader["Cantidad"]);
                                decimal precio = Convert.ToDecimal(reader["precio_especial"]);

                                resumen.AppendLine($"{nombre} x{cantidad} - {precio.ToString("C")}");
                            }
                            resumen.AppendLine();
                        }
                    }
                }
            }

            return resumen.ToString();
        }

        private string ObtenerItemsPromocion(int idPromocion, int cantidadPromo, bool formatoFactura = false)
        {
            StringBuilder items = new StringBuilder();

            if (formatoFactura)
            {
                items.AppendLine("   ┌───────────────────────────────");
                items.AppendLine("   │ Items incluidos en la promoción:");
            }

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            pi.tipo_item,
            CASE
                WHEN pi.tipo_item = 'PLATO' THEN (SELECT nombrePlato FROM platos WHERE id_plato = pi.id_item)
                WHEN pi.tipo_item = 'BEBIDA' THEN (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = pi.id_item)
                WHEN pi.tipo_item = 'EXTRA' THEN (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = pi.id_item)
            END AS nombre_item,
            pi.cantidad AS cantidad_en_promo
        FROM promocion_items pi
        WHERE pi.id_promocion = @idPromocion";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPromocion", idPromocion);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipoItem = reader["tipo_item"].ToString();
                            string nombreItem = reader["nombre_item"].ToString();
                            int cantidadEnPromo = Convert.ToInt32(reader["cantidad_en_promo"]);
                            int cantidadTotal = cantidadEnPromo * cantidadPromo;

                            if (formatoFactura)
                            {
                                items.AppendLine($"   ├─ {nombreItem} ({tipoItem}) x{cantidadTotal}");
                            }
                            else
                            {
                                items.AppendLine($"   - {nombreItem} ({tipoItem}) x{cantidadTotal}");
                            }
                        }
                    }
                }
            }

            if (formatoFactura)
            {
                items.AppendLine("   └───────────────────────────────");
            }

            return items.ToString();
        }

        private void FormHistorialPagosAdmin_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idOrden = Convert.ToInt32(dataGridViewHistorial.Rows[e.RowIndex].Cells["Número Orden"].Value);
                MostrarFacturaCompleta(idOrden);
            }
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    
