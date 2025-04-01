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
    public partial class FormAdminPrecuenta: Form
    {
        private int idOrden; // Esta declaración debe estar DENTRO de la clase
        public FormAdminPrecuenta(int idOrden)
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idOrden = idOrden;
            CargarDatosOrden();
            CargarDetallePedidos();
        }

        private void CargarDatosOrden()
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            o.id_orden, o.nombreCliente, o.total, o.descuento, o.fecha_orden,
            m.nombreMesa, u.nombre AS nombreUsuario,
            tp.nombrePago AS tipoPago
        FROM ordenes o
        JOIN mesas m ON o.id_mesa = m.id_mesa
        JOIN usuarios u ON o.id_usuario = u.id_usuario
        JOIN tipoPago tp ON o.tipo_pago = tp.id_pago
        WHERE o.id_orden = @idOrden";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal total = Convert.ToDecimal(reader["total"]);
                            decimal descuento = Convert.ToDecimal(reader["descuento"]);
                            decimal subtotal = total + descuento; // Calculamos el subtotal

                            lblNumeroFactura.Text = $"Precuenta #: {reader["id_orden"]}";
                            lblFecha.Text = $"Fecha: {Convert.ToDateTime(reader["fecha_orden"]):dd/MM/yyyy}";
                            lblHora.Text = $"Hora: {Convert.ToDateTime(reader["fecha_orden"]):HH:mm}";
                            lblCliente.Text = $"Cliente: {reader["nombreCliente"]}";
                            lblMesa.Text = $"Mesa: {reader["nombreMesa"]}";
                            lblAtendidoPor.Text = $"Atendido por: {reader["nombreUsuario"]}";
                            lblTipoPago.Text = $"Método de pago: {reader["tipoPago"]}";

                            // Mostrar los totales
                            lblSubtotal.Text = $"Subtotal: {subtotal:C}";
                            lblDescuento.Text = $"Descuento: {descuento:C}";
                            lblTotal.Text = $"Total: {total:C}";
                        }
                    }
                }
            }
        }

        private void CargarDetallePedidos()
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                // Obtener el descuento actual de la orden
                decimal descuento = 0;
                string queryDescuento = "SELECT descuento FROM ordenes WHERE id_orden = @idOrden";
                using (MySqlCommand cmdDescuento = new MySqlCommand(queryDescuento, conexion))
                {
                    cmdDescuento.Parameters.AddWithValue("@idOrden", idOrden);
                    object result = cmdDescuento.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        descuento = Convert.ToDecimal(result);
                    }
                }

                // Obtener los items de la orden (código existente)
                string query = @"
        SELECT 
            COALESCE(
                pl.nombrePlato,
                (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = p.id_bebida),
                (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = p.id_extra)
            ) AS nombre,
            p.Cantidad,
            COALESCE(
                pl.precioUnitario,
                (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra)
            ) AS precioUnitario,
            (p.Cantidad * COALESCE(
                pl.precioUnitario,
                (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra)
            )) AS subtotal
        FROM pedidos p
        LEFT JOIN platos pl ON p.id_plato = pl.id_plato
        WHERE p.id_orden = @idOrden";

                DataTable dt = new DataTable();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                dgvDetalle.DataSource = dt;

                // Calcular totales
                decimal subtotal = 0;
                foreach (DataRow row in dt.Rows)
                {
                    subtotal += Convert.ToDecimal(row["subtotal"]);
                }

                decimal total = subtotal - descuento;

                // Actualizar los labels
                lblSubtotal.Text = $"Subtotal: {subtotal:C}";
                lblDescuento.Text = $"Descuento: {descuento:C}";
                lblTotal.Text = $"Total: {total:C}";
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormAdminPrecuenta_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarDatosOrden();
            CargarDetallePedidos();
        }
        private void ConfigurarDataGridView()
        {
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvDetalle.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetalle.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Formato de columnas numéricas
            if (dgvDetalle.Columns["precioUnitario"] != null)
                dgvDetalle.Columns["precioUnitario"].DefaultCellStyle.Format = "C2";

            if (dgvDetalle.Columns["subtotal"] != null)
                dgvDetalle.Columns["subtotal"].DefaultCellStyle.Format = "C2";
        }

        private void btnDescuento_Click(object sender, EventArgs e)
        {
            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Aplicar Descuento";
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.MaximizeBox = false;
                inputDialog.MinimizeBox = false;
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.Size = new Size(350, 220); // Tamaño aumentado

                // Panel para organizar mejor el contenido
                Panel panelContenido = new Panel()
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10)
                };

                Label lblPorcentaje = new Label()
                {
                    Text = "Porcentaje de descuento (%):",
                    Dock = DockStyle.Top,
                    Margin = new Padding(0, 0, 0, 5)
                };

                NumericUpDown numPorcentaje = new NumericUpDown()
                {
                    Minimum = 0,
                    Maximum = 100,
                    Value = 0,
                    Dock = DockStyle.Top,
                    Font = new Font("Arial", 12),
                    Margin = new Padding(0, 0, 0, 15)
                };

                Label lblMontoFijo = new Label()
                {
                    Text = "O monto fijo de descuento ($):",
                    Dock = DockStyle.Top,
                    Margin = new Padding(0, 0, 0, 5)
                };

                TextBox txtMontoFijo = new TextBox()
                {
                    Dock = DockStyle.Top,
                    Font = new Font("Arial", 12),
                    TextAlign = HorizontalAlignment.Right,
                    Margin = new Padding(0, 0, 0, 15)
                };

                Button btnAplicar = new Button()
                {
                    Text = "Aplicar Descuento",
                    Dock = DockStyle.Bottom,
                    Height = 40,
                    BackColor = Color.LightGreen,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };

                // Eventos para manejar la entrada
                numPorcentaje.ValueChanged += (s, ev) => {
                    if (numPorcentaje.Value > 0)
                    {
                        txtMontoFijo.Text = "";
                    }
                };

                txtMontoFijo.TextChanged += (s, ev) => {
                    if (!string.IsNullOrEmpty(txtMontoFijo.Text))
                    {
                        numPorcentaje.Value = 0;
                    }
                };

                btnAplicar.Click += (s, ev) => {
                    decimal descuento = 0;
                    decimal totalActual = decimal.Parse(lblTotal.Text.Replace("Total: $", "").Replace(",", ""));

                    if (!string.IsNullOrEmpty(txtMontoFijo.Text) && decimal.TryParse(txtMontoFijo.Text, out decimal montoFijo))
                    {
                        // Validar que el monto no sea mayor al total
                        if (montoFijo > totalActual)
                        {
                            MessageBox.Show("El descuento no puede ser mayor al total actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        descuento = montoFijo;
                    }
                    else if (numPorcentaje.Value > 0)
                    {
                        descuento = totalActual * (numPorcentaje.Value / 100);
                    }

                    // Validar que el descuento no sea negativo
                    if (descuento < 0)
                    {
                        MessageBox.Show("El descuento no puede ser negativo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Actualizar la orden con el nuevo total (total - descuento)
                    decimal nuevoTotal = totalActual - descuento;
                    ActualizarTotalYDescuento(nuevoTotal, descuento);
                    inputDialog.DialogResult = DialogResult.OK;
                };

                // Agregar controles al panel
                panelContenido.Controls.Add(btnAplicar);
                panelContenido.Controls.Add(txtMontoFijo);
                panelContenido.Controls.Add(lblMontoFijo);
                panelContenido.Controls.Add(numPorcentaje);
                panelContenido.Controls.Add(lblPorcentaje);

                // Agregar panel al formulario
                inputDialog.Controls.Add(panelContenido);

                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    // Refrescar los datos
                    CargarDatosOrden();
                }
            }
        }

        private void ActualizarTotalYDescuento(decimal nuevoTotal, decimal descuento)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = "UPDATE ordenes SET total = @total, descuento = @descuento WHERE id_orden = @idOrden";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@total", nuevoTotal);
                    cmd.Parameters.AddWithValue("@descuento", descuento);
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    cmd.ExecuteNonQuery();
                }
            }

            // Actualizar la interfaz
            lblTotal.Text = $"Total: {nuevoTotal:C}";
            lblDescuento.Text = $"Descuento: {descuento:C}";
        }

    }
}
