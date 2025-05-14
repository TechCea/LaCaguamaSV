using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormComidasMenuAdmin : Form
    {

        Conexion conexion = new Conexion();
        int idPlatoSeleccionado = -1;

        public FormComidasMenuAdmin()
        {
            InitializeComponent();

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;


            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvComidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvComidas.MultiSelect = false;
            dgvComidas.ReadOnly = true;
            dgvComidas.AllowUserToAddRows = false;
            dgvComidas.AllowUserToDeleteRows = false;
            dgvComidas.AllowUserToResizeRows = false;

            CargarCategoriasComida();
            CargarComidas();
            cbCategoriaC.SelectedIndexChanged += cbCategoriaC_SelectedIndexChanged;
            dgvComidas.CellClick += dgvComidas_CellClick;
            dgvComidas.MultiSelect = false;
        }

        private void CargarComidas()
        {
            dgvComidas.DataSource = conexion.ObtenerComidas();
            dgvComidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void CargarCategoriasComida()
        {
            DataTable dtCategorias = conexion.ObtenerCategoriasComida();
            cbCategoriaC.Items.Add("Todas");
            cbCategoriaB.Items.Clear();

            foreach (DataRow row in dtCategorias.Rows)
            {
                cbCategoriaC.Items.Add(row["tipo"].ToString());
                cbCategoriaB.Items.Add(row["tipo"].ToString());
            }

            cbCategoriaC.SelectedIndex = 0;
            if (cbCategoriaB.Items.Count > 0) cbCategoriaB.SelectedIndex = 0;
        }

        private void cbCategoriaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoriaSeleccionada = cbCategoriaC.SelectedItem.ToString();
            dgvComidas.DataSource = categoriaSeleccionada == "Todas"
                ? conexion.ObtenerComidas()
                : conexion.ObtenerComidasPorCategoria(categoriaSeleccionada);
            dgvComidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvComidas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvComidas.Rows[e.RowIndex];
                idPlatoSeleccionado = Convert.ToInt32(fila.Cells["ID Plato"].Value);
                txtNombreC.Text = fila.Cells["Nombre Plato"].Value.ToString();
                txtDescripcionC.Text = fila.Cells["Descripción"].Value.ToString();
                txtPrecioU.Text = fila.Cells["Precio Unitario"].Value.ToString();
                cbCategoriaB.SelectedItem = fila.Cells["Categoría"].Value.ToString();
            }
        }


        private void btnActualizarC_Click(object sender, EventArgs e)
        {
            if (idPlatoSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un plato para actualizar");
                return;
            }

            string nombre = txtNombreC.Text;
            string descripcion = txtDescripcionC.Text;
            string categoria = cbCategoriaB.SelectedItem?.ToString();
            decimal precio;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del plato no puede estar vacío.");
                return;
            }

            if (string.IsNullOrWhiteSpace(categoria))
            {
                MessageBox.Show("Selecciona una categoría.");
                return;
            }

            if (!decimal.TryParse(txtPrecioU.Text, out precio) || precio <= 0)
            {
                MessageBox.Show("Precio inválido o menor que 0.");
                return;
            }

            // Llamada a la función ActualizarPlato con los parámetros correctos
            if (conexion.ActualizarPlato(idPlatoSeleccionado, nombre, descripcion, precio, categoria))
            {
                MessageBox.Show("Plato actualizado correctamente.");
                CargarComidas();  // Recargar la lista de platos
                LimpiarCampos();  // Limpiar los campos de texto
            }
            else
            {
                MessageBox.Show("Error al actualizar el plato.");
            }
        }

        private void LimpiarCampos()
        {
            idPlatoSeleccionado = -1;
            txtNombreC.Clear();
            txtDescripcionC.Clear();
            txtPrecioU.Clear();
            if (cbCategoriaB.Items.Count > 0) cbCategoriaB.SelectedIndex = 0;
        }

        private void btnRegresarMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMenuAdmin FormMenuAdmin = new FormMenuAdmin();
            FormMenuAdmin.Show();
        }

        public class RoundedControl
        {
            public static void ApplyRoundedCorners(Control control, int radius)
            {
          
            }
        }
    }
}
