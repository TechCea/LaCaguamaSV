using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormBebidasMenuAdmin : Form
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



        Conexion conexion = new Conexion();

        public FormBebidasMenuAdmin()
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
            this.MouseDown += new MouseEventHandler(FormBebidasMenuAdmin_MouseDown);

            // Aplicar esquinas redondeadas a controles específicos
            RoundedControl.ApplyRoundedCorners(dgvBebidas, 15);   // Redondear un Panel
            RoundedControl.ApplyRoundedCorners(gbDatosBebida, 15);   // Redondear un Panel



            CargarCategorias();
            CargarBebidas();
            CargarCategoriasBebidas(); // Cargar categorías en cbCategoriaB
            cbCategoria.SelectedIndexChanged += CbCategoria_SelectedIndexChanged;
            dgvBebidas.SelectionChanged += dgvBebidas_SelectionChanged;
            dgvBebidas.CellClick += dgvBebidas_CellClick;
        }

        private void CargarBebidas()
        {
            dgvBebidas.DataSource = conexion.ObtenerBebidas();
        }        
        
        private void limpiar()
        {
            // Limpiar los TextBox después de la actualización
            txtNombreB.Clear();
            txtPrecioU.Clear();
            cbCategoriaB.SelectedIndex = -1; // Desseleccionar categoría
        }


        private void CargarCategorias()
        {
            DataTable dtCategorias = conexion.ObtenerCategorias();
            cbCategoria.Items.Add("Todas"); // Opción para mostrar todas las bebidas

            foreach (DataRow row in dtCategorias.Rows)
            {
                cbCategoria.Items.Add(row["tipo"].ToString());
            }

            cbCategoria.SelectedIndex = 0; // Selecciona "Todas" por defecto
        }

        private void CargarCategoriasBebidas()
        {
            DataTable dtCategorias = conexion.ObtenerCategorias();

            cbCategoriaB.Items.Clear(); // Limpia el ComboBox antes de cargar datos

            foreach (DataRow row in dtCategorias.Rows)
            {
                cbCategoriaB.Items.Add(row["tipo"].ToString()); // Agrega cada categoría al ComboBox
            }

            if (cbCategoriaB.Items.Count > 0)
            {
                cbCategoriaB.SelectedIndex = 0; // Selecciona la primera opción por defecto
            }
        }

        private void CbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoriaSeleccionada = cbCategoria.SelectedItem.ToString();

            if (categoriaSeleccionada == "Todas")
            {
                CargarBebidas();
            }
            else
            {
                dgvBebidas.DataSource = conexion.ObtenerBebidasPorCategoria(categoriaSeleccionada);
            }
        }

        private void dgvBebidas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbCategoria_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnRegresarMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMenuAdmin FormMenuAdmin = new FormMenuAdmin();
            FormMenuAdmin.Show();
        }

        private void btnActualizarB_Click(object sender, EventArgs e)
        {
            if (dgvBebidas.SelectedRows.Count > 0)
            {
                int idBebida = Convert.ToInt32(dgvBebidas.SelectedRows[0].Cells["ID Bebida"].Value);
                string nuevoNombre = txtNombreB.Text;
                string nuevaCategoria = cbCategoriaB.SelectedItem?.ToString();
                decimal nuevoPrecio;

                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    MessageBox.Show("El nombre de la bebida no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(nuevaCategoria))
                {
                    MessageBox.Show("Seleccione una categoría válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtPrecioU.Text, out nuevoPrecio) || nuevoPrecio <= 0)
                {
                    MessageBox.Show("Ingrese un precio válido mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (conexion.ActualizarBebida(idBebida, nuevoNombre, nuevaCategoria, nuevoPrecio))
                {
                    MessageBox.Show("Bebida actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarBebidas(); // Recargar la lista después de actualizar
                    limpiar();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la bebida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una bebida para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminarB_Click(object sender, EventArgs e)
        {
            if (dgvBebidas.SelectedRows.Count > 0)
            {
                // Obtener el ID de la bebida seleccionada
                int idBebida = Convert.ToInt32(dgvBebidas.SelectedRows[0].Cells["ID Bebida"].Value);

                // Confirmación antes de eliminar
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta bebida?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    if (conexion.EliminarBebida(idBebida))
                    {
                        MessageBox.Show("Bebida eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarBebidas(); // Recargar la tabla después de eliminar
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la bebida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una bebida para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void cbCategoriaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvBebidas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurar que se selecciona una fila válida
            {
                DataGridViewRow row = dgvBebidas.Rows[e.RowIndex];

                txtNombreB.Text = row.Cells["Nombre Bebida"].Value.ToString();
                txtPrecioU.Text = row.Cells["Precio Unitario"].Value.ToString();

                // Buscar la categoría en el ComboBox y seleccionarla
                string categoria = row.Cells["Categoría"].Value.ToString();
                int index = cbCategoriaB.FindStringExact(categoria);
                if (index >= 0)
                {
                    cbCategoriaB.SelectedIndex = index;
                }
                else
                {
                    cbCategoriaB.SelectedIndex = -1; // Si no la encuentra, lo deja sin selección
                }
            }
        }

        private void dgvBebidas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBebidas.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvBebidas.SelectedRows[0];

                // Obtener el nombre de la bebida
                string nombreBebida = filaSeleccionada.Cells["Nombre Bebida"].Value.ToString();

                // Mostrar en el Label
                lblSeleccionBebida.Text = $"Has seleccionado la bebida: {nombreBebida}";
            }
        }

        private void FormBebidasMenuAdmin_Load(object sender, EventArgs e)
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
        private void FormBebidasMenuAdmin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
