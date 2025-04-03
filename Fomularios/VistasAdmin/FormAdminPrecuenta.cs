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

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormAdminPrecuenta : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private int idOrden; // Esta declaración debe estar DENTRO de la clase
        public FormAdminPrecuenta(int idOrden)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Agregar evento para mover el formulario
            this.MouseDown += new MouseEventHandler(FormAdminPrecuenta_MouseDown);

            // Aplicar esquinas redondeadas a controles específicos
            RoundedControl.ApplyRoundedCorners(dgvDetalle, 15);   // Redondear un Panel

         
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
                            lblNumeroFactura.Text = $"Precuenta #: {reader["id_orden"]}";
                            lblFecha.Text = $"Fecha: {reader["fecha_orden"]:dd/MM/yyyy}";
                            lblHora.Text = $"Hora: {reader["fecha_orden"]:HH:mm}";
                            lblCliente.Text = $"Cliente: {reader["nombreCliente"]}";
                            lblMesa.Text = $"Mesa: {reader["nombreMesa"]}";
                            lblAtendidoPor.Text = $"Atendido por: {reader["nombreUsuario"]}";
                            lblTipoPago.Text = $"Método de pago: {reader["tipoPago"]}";
                        }
                    }
                }
            }
        }

        private void CargarDetallePedidos()
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
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

                decimal descuento = 0;
                decimal total = subtotal - descuento;

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

        }

        public class RoundedControl
        {
            public static void ApplyRoundedCorners(Control control, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }
        private void FormAdminPrecuenta_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}