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
using static LaCaguamaSV.Login;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class CrearOrden: Form
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

        public CrearOrden()
        {
            InitializeComponent();
            CargarTipoPago();
            CargarMesas();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Agregar evento para mover el formulario
            this.MouseDown += new MouseEventHandler(CrearOrden_MouseDown);

         


        }

        private void CargarTipoPago()
        {
            DataTable dtPagos = OrdenesD.ObtenerTiposPago();
            cmbTipoPago.DataSource = dtPagos;
            cmbTipoPago.DisplayMember = "nombrePago"; // Mostrar el nombre del método de pago
            cmbTipoPago.ValueMember = "id_pago"; // Guardar el ID del método de pago

            // Seleccionar por defecto "Efectivo"
            foreach (DataRow row in dtPagos.Rows)
            {
                if (row["nombrePago"].ToString().ToLower() == "efectivo")
                {
                    cmbTipoPago.SelectedValue = row["id_pago"];
                    break;
                }
            }
        }

        private void CargarMesas()
        {
            DataTable dtMesas = OrdenesD.ObtenerMesas(); // Obtener mesas desde la BD
            cmbMesas.DataSource = dtMesas;
            cmbMesas.DisplayMember = "nombreMesa"; // Mostrar el nombre de la mesa
            cmbMesas.ValueMember = "id_mesa"; // Usar el ID de la mesa como valor

            if (dtMesas.Rows.Count > 0)
            {
                cmbMesas.SelectedIndex = 0; // Seleccionar la primera mesa por defecto
            }
        }


        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMesas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre del cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Obtener datos del formulario
                string nombreCliente = txtNombreCliente.Text;
                int idMesa = Convert.ToInt32(cmbMesas.SelectedValue);
                int tipoPago = Convert.ToInt32(cmbTipoPago.SelectedValue);

                // Crear la orden vacía (total y descuento en 0, estado "Abierta" por defecto)
                int idOrden = OrdenesD.CrearOrdenVacia(nombreCliente, idMesa, tipoPago);

                if (idOrden > 0)
                {
                    MessageBox.Show($"Orden #{idOrden} creada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Aquí podrías abrir el formulario para agregar pedidos a esta orden
                    // Por ejemplo:
                    // var formAgregarPedidos = new AgregarPedidosForm(idOrden);
                    // formAgregarPedidos.ShowDialog();

                    // Cerrar este formulario
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al crear la orden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {

        }

        private void btnMaximizar_Click_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnMinimizar_Click_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
        private void CrearOrden_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
