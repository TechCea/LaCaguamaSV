using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormAdmin: Form
    {

        private bool mostrarSoloHoy = true;
        public FormAdmin()
        {
            InitializeComponent();
            CargarOrdenes(true); // Cargar solo órdenes del día por defecto
            dataGridViewOrdenesAdmin.CellFormatting += dataGridViewOrdenesAdmin_CellFormatting;

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Size = new Size(1040, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            //Ojoooo, esto hace que pueda seleccionar toda la fila de datos, independientemente de donde le de click
            dataGridViewOrdenesAdmin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrdenesAdmin.MultiSelect = false;
            dataGridViewOrdenesAdmin.ReadOnly = true;
            dataGridViewOrdenesAdmin.AllowUserToAddRows = false;
            dataGridViewOrdenesAdmin.AllowUserToDeleteRows = false;
            dataGridViewOrdenesAdmin.AllowUserToResizeRows = false;

            // 💡 Asocia el evento para permitir clic en cualquier parte de la fila
            dataGridViewOrdenesAdmin.CellClick += dataGridViewOrdenesAdmin_CellClick;

            // 💥 Seguridad al final (por si entra un ninja sin permisos)
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close(); 
            }
        }


        private void CargarOrdenes(bool soloHoy = true)
        {
            try
            {
                dataGridViewOrdenesAdmin.DataSource = null;
                DataTable dt = OrdenesD.ObtenerOrdenes(soloHoy);
                dataGridViewOrdenesAdmin.DataSource = dt;

                // Configuración común del DataGridView
                ConfigurarDataGridView(dataGridViewOrdenesAdmin);
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

        }

        public void RefrescarOrdenes()
        {
            CargarOrdenes();
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            FormUsuariosAdmin formUsuarios = new FormUsuariosAdmin();
            formUsuarios.ShowDialog();
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

        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenuAdmin formMenu = new FormMenuAdmin();
            formMenu.ShowDialog();
        }

        private void Ordenes_Click(object sender, EventArgs e)
        {
            
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            // Asegurar que el DataGridView no sea editable y seleccione filas completas
            dataGridViewOrdenesAdmin.ReadOnly = true;
            dataGridViewOrdenesAdmin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrdenesAdmin.MultiSelect = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int usuarioId = SesionUsuario.IdUsuario;  // Suponiendo que ya tienes el ID del usuario
            FormAdminFunciones formFunciones = new FormAdminFunciones(usuarioId);
            formFunciones.ShowDialog();
        }

        private void dataGridViewOrdenesAdmin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurar que no se haga clic en el encabezado
            {
                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewOrdenesAdmin.Rows[e.RowIndex];

                // Obtener el estado de la orden
                string estadoOrden = row.Cells["estado_orden"].Value.ToString();
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
                    o.nombreCliente, 
                    o.total, 
                    o.descuento,
                    DATE_FORMAT(o.fecha_orden, '%Y-%m-%d %H:%i') AS fecha_orden,
                    m.nombreMesa AS mesa,
                    tp.nombrePago AS metodo_pago,
                    u.nombre AS usuario_creador,
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
                    string usuarioCreador = "";
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
                                usuarioCreador = reader["usuario_creador"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("tipo_descuento_nombre")))
                                {
                                    tipoDescuentoNombre = reader["tipo_descuento_nombre"].ToString();
                                    descuentoEsPorcentaje = Convert.ToBoolean(reader["descuento_es_porcentaje"]);
                                }
                            }
                        }
                    }

                    // Obtener detalles de la orden (código existente)
                    StringBuilder detalle = new StringBuilder();
                    decimal totalCalculado = 0;

                    string queryDetalle = @"
                SELECT 
                    COALESCE(
                        pl.nombrePlato,
                        (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = p.id_bebida),
                        (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = p.id_extra),
                        (SELECT pr.nombre FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                    ) AS Producto,
                    p.Cantidad,
                    COALESCE(
                        pl.precioUnitario,
                        (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                        (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra),
                        (SELECT pr.precio_especial FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                    ) AS PrecioUnitario,
                    (p.Cantidad * COALESCE(
                        pl.precioUnitario,
                        (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                        (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra),
                        (SELECT pr.precio_especial FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                    )) AS Subtotal,
                    CASE
                        WHEN p.id_promocion IS NOT NULL THEN 1
                        ELSE 0
                    END AS EsPromocion,
                    p.id_promocion
                FROM pedidos p
                LEFT JOIN platos pl ON p.id_plato = pl.id_plato
                LEFT JOIN promociones pr ON p.id_promocion = pr.id_promocion
                WHERE p.id_orden = @idOrden
                ORDER BY EsPromocion DESC, Producto ASC";

                    using (MySqlCommand cmd = new MySqlCommand(queryDetalle, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string producto = reader["Producto"].ToString();
                                int cantidad = Convert.ToInt32(reader["Cantidad"]);
                                decimal precioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]);
                                decimal subtotal = Convert.ToDecimal(reader["Subtotal"]);
                                bool esPromocion = Convert.ToInt32(reader["EsPromocion"]) == 1;
                                int? idPromocion = reader["id_promocion"] as int?;

                                detalle.AppendLine($"{producto} x{cantidad} - {precioUnitario.ToString("C")} = {subtotal.ToString("C")}");
                                totalCalculado += subtotal;

                                if (esPromocion && idPromocion.HasValue)
                                {
                                    detalle.AppendLine(ObtenerItemsPromocion(idPromocion.Value, cantidad));
                                }
                            }
                        }
                    }

                    // Obtener información de pago en efectivo si existe
                    decimal recibido = 0;
                    decimal cambio = 0;

                    if (metodoPago == "Efectivo")
                    {
                        string queryPago = "SELECT recibido, cambio FROM pagos WHERE id_orden = @idOrden";

                        using (MySqlCommand cmd = new MySqlCommand(queryPago, conexion))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    recibido = reader.GetDecimal("recibido");
                                    cambio = reader.GetDecimal("cambio");
                                }
                            }
                        }
                    }

                    // Calcular el monto real del descuento (si fue porcentaje)
                    decimal montoDescuento = descuento;
                    if (descuentoEsPorcentaje)
                    {
                        montoDescuento = totalCalculado * (descuento / 100);
                    }

                    // Generar comprobante con mejor formato
                    StringBuilder factura = new StringBuilder();
                    factura.AppendLine("╔══════════════════════════════════╗");
                    factura.AppendLine("║      LA CAGUAMA RESTAURANTE      ║");
                    factura.AppendLine("╚══════════════════════════════════╝");
                    factura.AppendLine($"ORDEN: #{idOrden}".PadRight(37));
                    factura.AppendLine($"FECHA: {fechaOrden}".PadRight(37));
                    factura.AppendLine($"MESA: {mesa}".PadRight(37));
                    factura.AppendLine($"CLIENTE: {cliente}".PadRight(37));
                    factura.AppendLine($"ATENDIDO POR: {usuarioCreador}".PadRight(37));
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine("            DETALLE DE ORDEN       ");
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine(detalle.ToString());
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine($"SUBTOTAL: {totalCalculado.ToString("C").PadLeft(20)}");

                    if (descuento > 0)
                    {
                        string descuentoTexto = descuentoEsPorcentaje ?
                            $"{descuento}% ( -{montoDescuento.ToString("C")} )" :
                            $"-{descuento.ToString("C")}";

                        factura.AppendLine($"DESCUENTO ({tipoDescuentoNombre}): {descuentoTexto.PadLeft(10)}");
                    }

                    factura.AppendLine($"TOTAL: {total.ToString("C").PadLeft(25)}");
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine($"MÉTODO DE PAGO: {metodoPago}");

                    if (metodoPago == "Efectivo")
                    {
                        factura.AppendLine($"RECIBIDO: {recibido.ToString("C").PadLeft(22)}");
                        factura.AppendLine($"CAMBIO: {cambio.ToString("C").PadLeft(24)}");
                    }

                    factura.AppendLine("══════════════════════════════════");
                    factura.AppendLine("   ¡GRACIAS POR SU PREFERENCIA!   ");
                    factura.AppendLine("══════════════════════════════════");

                    // Mostrar en un formulario con scroll para mejor visualización
                    using (var formFactura = new Form()
                    {
                        Width = 500,
                        Height = 600,
                        Text = $"Factura Orden #{idOrden}",
                        StartPosition = FormStartPosition.CenterParent,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        MaximizeBox = false
                    })
                    {
                        var textBox = new TextBox()
                        {
                            Multiline = true,
                            Dock = DockStyle.Fill,
                            ReadOnly = true,
                            ScrollBars = ScrollBars.Vertical,
                            Text = factura.ToString(),
                            Font = new Font("Consolas", 10),
                            BackColor = Color.White
                        };

                        formFactura.Controls.Add(textBox);
                        formFactura.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar factura: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            using (CrearOrden formOrden = new CrearOrden())
            {
                formOrden.ShowDialog(); // Mostrar como modal
                CargarOrdenes(); // Refrescar lista de órdenes después de cerrar
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormMesasAdmin formmesas = new FormMesasAdmin();
            formmesas.ShowDialog();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            FormInventarioAdmin formminven = new FormInventarioAdmin();
            formminven.ShowDialog();
        }

        private void BtnHistorialPagos_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear y mostrar el formulario de historial de pagos
                FormHistorialPagosAdmin historialForm = new FormHistorialPagosAdmin();
                historialForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el historial de pagos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPromociones_Click_1(object sender, EventArgs e)
        {
            FormGestionPromociones formpromociones = new FormGestionPromociones();
            formpromociones.ShowDialog();
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

        private void dataGridViewOrdenesAdmin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewOrdenesAdmin.Columns[e.ColumnIndex].Name == "estado_orden")
            {
                var estado = e.Value?.ToString();
                if (!string.IsNullOrEmpty(estado))
                {
                    DataGridViewRow row = dataGridViewOrdenesAdmin.Rows[e.RowIndex];

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

        private void btnFiltrarOrdenes_Click(object sender, EventArgs e)
        {
            mostrarSoloHoy = !mostrarSoloHoy;
            CargarOrdenes(mostrarSoloHoy);
            btnFiltrarOrdenes.Text = mostrarSoloHoy ? "Ver todas" : "Ver solo hoy";
        }
    }
}
