using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormExtrasMenuAdmin : Form
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


        private Conexion conexion = new Conexion(); // Instancia de la clase de conexión

        public FormExtrasMenuAdmin()
        {
            InitializeComponent();

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Agregar evento para mover el formulario
            this.MouseDown += new MouseEventHandler(FormExtrasMenuAdmin_MouseDown);

            // Aplicar esquinas redondeadas a controles específicos
            RoundedControl.ApplyRoundedCorners(dgvExtras, 15);   // Redondear un Panel
            RoundedControl.ApplyRoundedCorners(gbDatosExtras, 15);   // Redondear un Panel


            CargarExtras(); // Llamamos la función al iniciar el formulario
            dgvExtras.DataSource = conexion.ObtenerExtras();
            dgvExtras.SelectionChanged += dgvExtras_SelectionChanged;
        }

        private void CargarExtras()
        {
            dgvExtras.DataSource = conexion.ObtenerExtras();
        }        
        
        private void limpiar()
        {
            txtNombreE.Clear();
            txtPrecioUE.Clear();
        }

        private void btnEliminarE_Click(object sender, EventArgs e)
        {
            if (dgvExtras.SelectedRows.Count > 0) // Verifica si hay una fila seleccionada
            {
                int idExtra = Convert.ToInt32(dgvExtras.SelectedRows[0].Cells["ID"].Value); // Obtiene el ID del extra seleccionado

                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este extra?",
                                                      "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes) // Si el usuario confirma
                {
                    Conexion conexion = new Conexion();
                    if (conexion.EliminarExtra(idExtra))
                    {
                        MessageBox.Show("Extra eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarExtras(); // Recarga los datos del DataGridView
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el extra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un extra para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dgvExtras_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvExtras.SelectedRows.Count > 0)
            {
                // Obtener el ID y el nombre del extra desde la fila seleccionada
                int idExtra = Convert.ToInt32(dgvExtras.SelectedRows[0].Cells["ID"].Value);
                string nombreExtra = dgvExtras.SelectedRows[0].Cells["Nombre"].Value.ToString();
                decimal precioExtra = Convert.ToDecimal(dgvExtras.SelectedRows[0].Cells["Precio Unitario"].Value);

                // Asignar el nombre y el precio a los TextBox
                txtNombreE.Text = nombreExtra;
                txtPrecioUE.Text = precioExtra.ToString("0.00");

            }
        }

        private void btnActualizarB_Click(object sender, EventArgs e)
        {
            if (dgvExtras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un extra de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el idExtra de la fila seleccionada en el DataGridView
            int idExtra = Convert.ToInt32(dgvExtras.SelectedRows[0].Cells["ID"].Value);
            string nuevoNombre = txtNombreE.Text.Trim();
            string precioTexto = txtPrecioUE.Text.Trim();

            if (string.IsNullOrEmpty(nuevoNombre) || string.IsNullOrEmpty(precioTexto))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(precioTexto, out decimal nuevoPrecio) || nuevoPrecio <= 0)
            {
                MessageBox.Show("Ingrese un precio válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Actualizamos el extra con los nuevos datos
            Conexion conexion = new Conexion();
            if (conexion.ActualizarExtra(idExtra, nuevoNombre, nuevoPrecio))
            {
                MessageBox.Show("Extra actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarExtras(); // Recarga los datos del DataGridView
                limpiar();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el extra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresarMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMenuAdmin FormMenuAdmin = new FormMenuAdmin();
            FormMenuAdmin.Show();
        }

        private void FormExtrasMenuAdmin_Load(object sender, EventArgs e)
        {

        }
        private void FormExtrasMenuAdmin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
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
    }
}

