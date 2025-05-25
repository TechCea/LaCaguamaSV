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
using LaCaguamaSV.Fomularios.VistasAdmin;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormUsuario: Form
    {
        private bool mostrarSoloHoy = true;
        public FormUsuario()
        {
            InitializeComponent();

            //Ojoooo, esto hace que pueda seleccionar toda la fila de datos, independientemente de donde le de click
            dataGridViewOrdenesUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrdenesUsuario.MultiSelect = false;
            dataGridViewOrdenesUsuario.ReadOnly = true;
            dataGridViewOrdenesUsuario.AllowUserToAddRows = false;
            dataGridViewOrdenesUsuario.AllowUserToDeleteRows = false;
            dataGridViewOrdenesUsuario.AllowUserToResizeRows = false;

           

            // Asignar manejadores de eventos
            dataGridViewOrdenesUsuario.CellClick += dataGridViewOrdenesUsuario_CellClick;
            dataGridViewOrdenesUsuario.CellFormatting += dataGridViewOrdenesUsuario_CellFormatting;


            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar


            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

           

            // Si el usuario no es normal (rol 2), cierra el formulario
            if (SesionUsuario.Rol != 2)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de usuario.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            CargarOrdenes(true); // Cargar solo órdenes del día por defecto
        }

        private void CargarOrdenes(bool soloHoy = true)
        {
            try
            {
                // Limpiar el DataSource primero
                dataGridViewOrdenesUsuario.DataSource = null;

                // Obtener los datos
                DataTable dt = OrdenesD.ObtenerOrdenes(soloHoy);

                // Asignar el DataSource
                dataGridViewOrdenesUsuario.DataSource = dt;

                // Configurar columnas después de asignar el DataSource
                if (dataGridViewOrdenesUsuario.Columns.Contains("total"))
                {
                    dataGridViewOrdenesUsuario.Columns["total"].DefaultCellStyle.Format = "C";
                    dataGridViewOrdenesUsuario.Columns["total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dataGridViewOrdenesUsuario.Columns.Contains("descuento"))
                {
                    dataGridViewOrdenesUsuario.Columns["descuento"].DefaultCellStyle.Format = "C";
                    dataGridViewOrdenesUsuario.Columns["descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Ajustar tamaño de columnas
                dataGridViewOrdenesUsuario.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // Forzar una actualización visual
                dataGridViewOrdenesUsuario.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
    
            if (dgv.Columns["total"] != null)
            {
                dgv.Columns["total"].DefaultCellStyle.Format = "C";
            }
            if (dgv.Columns["descuento"] != null)
            {
                dgv.Columns["descuento"].DefaultCellStyle.Format = "C";
            }
    
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public void RefrescarOrdenes()
        {
            CargarOrdenes();
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {
            // Configuración inicial del DataGridView
            dataGridViewOrdenesUsuario.ReadOnly = true;
            dataGridViewOrdenesUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrdenesUsuario.MultiSelect = false;
        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            using (CrearOrden formOrden = new CrearOrden())
            {
                formOrden.ShowDialog(); // Mostrar como modal
                CargarOrdenes(); // Refrescar lista de órdenes después de cerrar
            }
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            FormMesasAdmin formMesas = new FormMesasAdmin();
            formMesas.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Cierra la sesión
            SesionUsuario.CerrarSesion();

            // Vuelve al formulario de login
            Login loginForm = new Login();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void dataGridViewOrdenesUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurar que no se haga clic en el encabezado
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewOrdenesUsuario.Rows[e.RowIndex];

                // Obtener el estado de la orden
                string estadoOrden = row.Cells["estado_orden"].Value?.ToString() ?? "";
                int idOrden = Convert.ToInt32(row.Cells["id_orden"].Value);

                if (estadoOrden == "Cerrada")
                {
                    // Mostrar factura/comprobante para órdenes cerradas
                    MostrarFacturaOrden(idOrden);
                }
                else
                {
                    // Abrir gestión de orden para órdenes abiertas
                    AbrirGestionOrden(row);
                }
            }

        }

        private void MostrarFacturaOrden(int idOrden)
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

                    // Calcular el porcentaje de descuento basado en el monto y subtotal
                    decimal porcentajeCalculado = 0;
                    if (totalCalculado > 0 && descuento > 0)
                    {
                        porcentajeCalculado = (descuento / totalCalculado) * 100;
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
                    factura.AppendLine($"SUBTOTAL: {totalCalculado.ToString("C").PadLeft(20)}");

                    if (descuento > 0)
                    {
                        if (descuentoEsPorcentaje)
                        {
                            // Formato mejorado para descuentos porcentuales
                            factura.AppendLine($"DESCUENTO (Porcentaje):");
                            factura.AppendLine($"   {porcentajeCalculado.ToString("0.00")}%   -{descuento.ToString("C").PadLeft(8)}");
                        }
                        else
                        {
                            // Formato para montos fijos
                            factura.AppendLine($"DESCUENTO (Monto fijo): -{descuento.ToString("C").PadLeft(15)}");
                        }
                    }

                    factura.AppendLine($"TOTAL: {total.ToString("C").PadLeft(25)}");
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

        private string ObtenerItemsPromocion(int idPromocion, int cantidadPromo)
        {
            StringBuilder items = new StringBuilder();
            items.AppendLine("   ┌───────────────────────────────");
            items.AppendLine("   │ Items incluidos en la promoción:");

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

                            items.AppendLine($"   ├─ {nombreItem} ({tipoItem}) x{cantidadTotal}");
                        }
                    }
                }
            }

            items.AppendLine("   └───────────────────────────────");
            return items.ToString();
        }

        private void AbrirGestionOrden(DataGridViewRow row)
        {
            // Obtener datos de la orden
            int idOrden = Convert.ToInt32(row.Cells["id_orden"].Value);
            string nombreCliente = row.Cells["nombreCliente"].Value.ToString();
            decimal total = Convert.ToDecimal(row.Cells["total"].Value);
            decimal descuento = Convert.ToDecimal(row.Cells["descuento"].Value);
            string fechaOrden = row.Cells["fecha_orden"].Value.ToString();
            string numeroMesa = row.Cells["numero_mesa"].Value.ToString();
            string tipoPago = row.Cells["tipo_pago"].Value.ToString();
            string nombreUsuario = row.Cells["nombre_usuario"].Value.ToString();
            string estadoOrden = row.Cells["estado_orden"].Value.ToString();

            // Abrir formulario de gestión de órdenes
            FormGestionOrdenes formOrden = new FormGestionOrdenes(idOrden, nombreCliente, total,
                                                                  descuento, fechaOrden, numeroMesa,
                                                                  tipoPago, nombreUsuario, estadoOrden);
            formOrden.ShowDialog();

            CargarOrdenes(); // Refrescar lista después de cerrar
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            Menu formMenu = new Menu();
            formMenu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int usuarioId = SesionUsuario.IdUsuario;  // Suponiendo que ya tienes el ID del usuario
            FormAdminFunciones formFunciones = new FormAdminFunciones(usuarioId);
            formFunciones.ShowDialog();
        }

        private void dataGridViewOrdenesUsuario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewOrdenesUsuario.Columns[e.ColumnIndex].Name == "estado_orden")
            {
                var estado = e.Value?.ToString();
                if (!string.IsNullOrEmpty(estado))
                {
                    DataGridViewRow row = dataGridViewOrdenesUsuario.Rows[e.RowIndex];

                    if (estado == "Cerrada")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200); // rojo claro
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (estado == "Abierta")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 200); // verde claro
                        row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Cierra la sesión
            SesionUsuario.CerrarSesion();

            // Vuelve al formulario de login
            Login loginForm = new Login();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void btnFiltrarOrdenes_Click(object sender, EventArgs e)
        {
            mostrarSoloHoy = !mostrarSoloHoy;
            CargarOrdenes(mostrarSoloHoy);
            btnFiltrarOrdenes.Text = mostrarSoloHoy ? "Ver todas" : "Ver turno actual (10am-6am)";
        }

        private void dataGridViewOrdenesUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
