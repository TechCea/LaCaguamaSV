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
    public partial class FormAdminPagos : Form
    {
        private int idOrden;
        private int idUsuarioCreador; // Cambiamos el nombre para claridad
        private decimal totalOrden;
        private int idMesa;

        public FormAdminPagos(int idOrden, string nombreCliente, decimal total, int idMesa, int idUsuarioCreador)
        {
            InitializeComponent();
            this.idOrden = idOrden;
            this.idUsuarioCreador = idUsuarioCreador; // Usamos el usuario creador
            this.totalOrden = total;
            this.idMesa = idMesa;

            // Configurar controles
            lblNumeroOrden.Text = $"Orden #: {idOrden}";
            lblCliente.Text = $"Cliente: {nombreCliente}";
            lblTotalP.Text = total.ToString("C");

            CargarMetodosPago();
            CargarDetalleOrden();
            ActualizarCamposPago();
        }

        private void CargarMetodosPago()
        {
            try
            {
                DataTable metodosPago = OrdenesD.ObtenerTiposPago();
                comboMetodoPago.DataSource = metodosPago;
                comboMetodoPago.DisplayMember = "nombrePago";
                comboMetodoPago.ValueMember = "id_pago";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar métodos de pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDetalleOrden()
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
                    SELECT 
                        COALESCE(
                            pl.nombrePlato,
                            (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = p.id_bebida),
                            (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = p.id_extra)
                        ) AS Producto,
                        p.Cantidad,
                        COALESCE(
                            pl.precioUnitario,
                            (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                            (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra)
                        ) AS PrecioUnitario,
                        (p.Cantidad * COALESCE(
                            pl.precioUnitario,
                            (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                            (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra)
                        )) AS Subtotal
                    FROM pedidos p
                    LEFT JOIN platos pl ON p.id_plato = pl.id_plato
                    WHERE p.id_orden = @idOrden";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);

                        dataGridViewDetalle.DataSource = dt;
                        dataGridViewDetalle.Columns["Producto"].HeaderText = "Producto";
                        dataGridViewDetalle.Columns["Cantidad"].HeaderText = "Cantidad";
                        dataGridViewDetalle.Columns["PrecioUnitario"].HeaderText = "Precio Unitario";
                        dataGridViewDetalle.Columns["Subtotal"].HeaderText = "Subtotal";

                        // Formato de columnas
                        dataGridViewDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C";
                        dataGridViewDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "C";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalle de orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarCamposPago()
        {
            try
            {
                if (comboMetodoPago.SelectedValue != null && comboMetodoPago.SelectedValue is int)
                {
                    int metodoSeleccionado = (int)comboMetodoPago.SelectedValue;
                    panelEfectivo.Visible = (metodoSeleccionado == 1); // 1 = Efectivo

                    if (panelEfectivo.Visible)
                    {
                        txtRecibido.Focus();
                        txtRecibido.Text = totalOrden.ToString("0.00"); // Sugerir el monto exacto
                    }
                }
                else
                {
                    panelEfectivo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                panelEfectivo.Visible = false;
                Console.WriteLine($"Error al actualizar campos de pago: {ex.Message}");
            }
        }

        private bool ProcesarPagoEnBD(int metodoPago, decimal recibido, decimal cambio)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // 1. Registrar el pago (usamos idUsuarioCreador en lugar de idUsuario)
                        string queryPago = @"
                INSERT INTO pagos 
                    (id_orden, monto, recibido, cambio, id_usuario, id_tipo_pago) 
                VALUES 
                    (@idOrden, @total, @recibido, @cambio, @idUsuarioCreador, @tipoPago)";

                        using (MySqlCommand cmd = new MySqlCommand(queryPago, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.Parameters.AddWithValue("@total", totalOrden);
                            cmd.Parameters.AddWithValue("@recibido", metodoPago == 1 ? recibido : totalOrden);
                            cmd.Parameters.AddWithValue("@cambio", cambio);
                            cmd.Parameters.AddWithValue("@idUsuarioCreador", idUsuarioCreador); // Usamos el creador
                            cmd.Parameters.AddWithValue("@tipoPago", metodoPago);

                            cmd.ExecuteNonQuery();
                        }

                        // 2. Actualizar tipo de pago en la orden
                        string queryUpdateOrden = @"
                UPDATE ordenes 
                SET tipo_pago = @tipoPago 
                WHERE id_orden = @idOrden";

                        using (MySqlCommand cmd = new MySqlCommand(queryUpdateOrden, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@tipoPago", metodoPago);
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }

                        // 3. Actualizar estado de los pedidos a "Entregado" (id_estadoP = 2)
                        string queryPedidos = "UPDATE pedidos SET id_estadoP = 2 WHERE id_orden = @idOrden";
                        using (MySqlCommand cmd = new MySqlCommand(queryPedidos, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }

                        // 4. Actualizar estado de la orden a "Cerrada"
                        string queryOrden = "UPDATE ordenes SET id_estadoO = 2 WHERE id_orden = @idOrden";
                        using (MySqlCommand cmd = new MySqlCommand(queryOrden, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }

                        // 5. Liberar la mesa
                        string queryMesa = "UPDATE mesas SET id_estadoM = 1 WHERE id_mesa = @idMesa";
                        using (MySqlCommand cmd = new MySqlCommand(queryMesa, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idMesa", idMesa);
                            cmd.ExecuteNonQuery();
                        }

                        // 6. Actualizar inventario (¡ESTA ES LA PARTE QUE FALTABA!)
                        ActualizarInventario(conexion, transaction);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error en la transacción: {ex.Message}", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }
        private void ActualizarInventario(MySqlConnection conexion, MySqlTransaction transaction)
        {
            try
            {
                // Restar inventario para platos (ingredientes según recetas)
                string queryPlatos = @"
                UPDATE inventario i
                JOIN recetas r ON i.id_inventario = r.id_inventario
                JOIN pedidos p ON r.id_plato = p.id_plato
                SET i.cantidad = i.cantidad - (r.cantidad_necesaria * p.Cantidad)
                WHERE p.id_orden = @idOrden AND p.id_plato IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryPlatos, conexion, transaction))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    cmd.ExecuteNonQuery();
                }

                // Restar inventario para bebidas (1 unidad por bebida)
                string queryBebidas = @"
                UPDATE inventario i
                JOIN bebidas b ON i.id_inventario = b.id_inventario
                JOIN pedidos p ON b.id_bebida = p.id_bebida
                SET i.cantidad = i.cantidad - p.Cantidad
                WHERE p.id_orden = @idOrden AND p.id_bebida IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryBebidas, conexion, transaction))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    cmd.ExecuteNonQuery();
                }

                // Restar inventario para extras (1 unidad por extra)
                string queryExtras = @"
                UPDATE inventario i
                JOIN extras e ON i.id_inventario = e.id_inventario
                JOIN pedidos p ON e.id_extra = p.id_extra
                SET i.cantidad = i.cantidad - p.Cantidad
                WHERE p.id_orden = @idOrden AND p.id_extra IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryExtras, conexion, transaction))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar inventario: {ex.Message}");
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnProcesarPago_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Confirmar pago de la orden?", "Confirmar Pago",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int metodoPago = (int)comboMetodoPago.SelectedValue;
                    decimal recibido = 0;
                    decimal cambio = 0;

                    if (metodoPago == 1) // Efectivo
                    {
                        if (!decimal.TryParse(txtRecibido.Text, out recibido))
                        {
                            MessageBox.Show("Ingrese un monto válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        cambio = recibido - totalOrden;
                        if (cambio < 0)
                        {
                            MessageBox.Show("El monto recibido no cubre el total", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Procesar el pago en la base de datos
                    bool exito = ProcesarPagoEnBD(metodoPago, recibido, cambio);

                    if (exito)
                    {
                        MessageBox.Show("Pago procesado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cerrar el formulario y actualizar la orden principal
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al procesar el pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImprimirComprobante_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Generar comprobante simple (puedes mejorarlo con Reporting Services)
                StringBuilder comprobante = new StringBuilder();
                comprobante.AppendLine("COMPROBANTE DE PAGO");
                comprobante.AppendLine("---------------------");
                comprobante.AppendLine($"Orden: #{idOrden}");
                comprobante.AppendLine($"Cliente: {lblCliente.Text.Replace("Cliente: ", "")}");
                comprobante.AppendLine($"Fecha: {DateTime.Now.ToString("g")}");
                comprobante.AppendLine();
                comprobante.AppendLine("Detalle:");

                foreach (DataGridViewRow row in dataGridViewDetalle.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        comprobante.AppendLine($"{row.Cells["Producto"].Value} x{row.Cells["Cantidad"].Value} - {row.Cells["Subtotal"].Value}");
                    }
                }

                comprobante.AppendLine();
                comprobante.AppendLine($"Total: {lblTotalP.Text}");
                comprobante.AppendLine($"Método de pago: {comboMetodoPago.Text}");

                if ((int)comboMetodoPago.SelectedValue == 1) // Efectivo
                {
                    comprobante.AppendLine($"Recibido: {txtRecibido.Text}");
                    comprobante.AppendLine($"Cambio: {lblCambio.Text}");
                }

                comprobante.AppendLine();
                comprobante.AppendLine("¡Gracias por su visita!");

                // Mostrar comprobante (en producción podrías imprimirlo)
                MessageBox.Show(comprobante.ToString(), "Comprobante de Pago",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar comprobante: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRecibido_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtRecibido.Text, out decimal recibido))
            {
                decimal cambio = recibido - totalOrden;
                if (cambio < 0) cambio = 0;

                lblCambio.Text = cambio.ToString("C");

                // Habilitar botón de pago solo si el monto es suficiente
                btnProcesarPago.Enabled = (recibido >= totalOrden);
            }
            else
            {
                lblCambio.Text = "$0.00";
                btnProcesarPago.Enabled = false;
            }
        }

        private void txtRecibido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal y tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void comboMetodoPago_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ActualizarCamposPago();
        }

        private void FormAdminPagos_Load(object sender, EventArgs e)
        {

        }

        private void lblTotalP_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblCliente_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}