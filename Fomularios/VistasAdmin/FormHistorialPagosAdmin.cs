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
            CargarHistorialPagos();
            ConfigurarDataGridView();
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
                        p.recibido AS 'Recibido',
                        p.cambio AS 'Cambio',
                        tp.nombrePago AS 'Método Pago',
                        u.nombre AS 'Cajero',
                        DATE_FORMAT(p.fecha_pago, '%Y-%m-%d %H:%i') AS 'Fecha/Hora'
                    FROM pagos p
                    JOIN ordenes o ON p.id_orden = o.id_orden
                    JOIN tipoPago tp ON p.id_tipo_pago = tp.id_pago
                    JOIN usuarios u ON p.id_usuario = u.id_usuario
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
            if (dataGridViewHistorial.Columns["Total"] != null)
            {
                dataGridViewHistorial.Columns["Total"].DefaultCellStyle.Format = "C";
            }
            if (dataGridViewHistorial.Columns["Recibido"] != null)
            {
                dataGridViewHistorial.Columns["Recibido"].DefaultCellStyle.Format = "C";
            }
            if (dataGridViewHistorial.Columns["Cambio"] != null)
            {
                dataGridViewHistorial.Columns["Cambio"].DefaultCellStyle.Format = "C";
            }
        }


        private void MostrarFacturaCompleta(int idOrden)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    // Obtener información básica de la orden
                    string queryOrden = @"
                    SELECT 
                        o.id_orden, o.nombreCliente, o.total, o.descuento, 
                        DATE_FORMAT(o.fecha_orden, '%Y-%m-%d %H:%i') AS fecha_orden,
                        m.nombreMesa AS mesa,
                        tp.nombrePago AS metodo_pago,
                        u.nombre AS mesero
                    FROM ordenes o
                    JOIN mesas m ON o.id_mesa = m.id_mesa
                    JOIN tipoPago tp ON o.tipo_pago = tp.id_pago
                    JOIN usuarios u ON o.id_usuario = u.id_usuario
                    WHERE o.id_orden = @idOrden";

                    string cliente = "";
                    decimal total = 0;
                    decimal descuento = 0;
                    string fechaOrden = "";
                    string mesa = "";
                    string metodoPago = "";
                    string mesero = "";

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
                            }
                        }
                    }

                    // Obtener detalles de los pedidos
                    string queryPedidos = @"
                    SELECT 
                        COALESCE(
                            pl.nombrePlato,
                            (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = p.id_bebida),
                            (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = p.id_extra)
                        ) AS producto,
                        p.Cantidad,
                        COALESCE(
                            pl.precioUnitario,
                            (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                            (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra)
                        ) AS precio_unitario,
                        (p.Cantidad * COALESCE(
                            pl.precioUnitario,
                            (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                            (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra)
                        )) AS subtotal
                    FROM pedidos p
                    LEFT JOIN platos pl ON p.id_plato = pl.id_plato
                    WHERE p.id_orden = @idOrden";

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

                                detallePedidos.AppendLine($"{producto} x{cantidad} - {precioUnitario.ToString("C")} = {subtotal.ToString("C")}");
                                totalCalculado += subtotal;
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

                    // Construir el comprobante/factura
                    StringBuilder factura = new StringBuilder();
                    factura.AppendLine("══════════════════════════════════");
                    factura.AppendLine("        LA CAGUAMA RESTAURANTE    ");
                    factura.AppendLine("══════════════════════════════════");
                    factura.AppendLine($"Orden: #{idOrden}");
                    factura.AppendLine($"Fecha orden: {fechaOrden}");
                    if (!string.IsNullOrEmpty(fechaPago))
                        factura.AppendLine($"Fecha pago: {fechaPago}");
                    factura.AppendLine($"Mesa: {mesa}");
                    factura.AppendLine($"Cliente: {cliente}");
                    factura.AppendLine($"Mesero: {mesero}");
                    if (!string.IsNullOrEmpty(cajero))
                        factura.AppendLine($"Cajero: {cajero}");
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine("              DETALLE             ");
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine(detallePedidos.ToString());
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine($"Subtotal: {totalCalculado.ToString("C")}");
                    if (descuento > 0)
                        factura.AppendLine($"Descuento: -{descuento.ToString("C")}");
                    factura.AppendLine($"TOTAL: {total.ToString("C")}");
                    factura.AppendLine("──────────────────────────────────");
                    factura.AppendLine($"Método de pago: {metodoPago}");

                    if (metodoPago == "Efectivo")
                    {
                        factura.AppendLine($"Recibido: {recibido.ToString("C")}");
                        factura.AppendLine($"Cambio: {cambio.ToString("C")}");
                    }

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
                            Font = new Font("Consolas", 10) // Usar fuente monoespaciada para alineación
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
    
