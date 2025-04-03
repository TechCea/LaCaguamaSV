using LaCaguamaSV.Configuracion;
using System.Data;
using System.Windows.Forms;
using System;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormPlatosMenu : Form
    {
        Conexion conexion = new Conexion();

        public FormPlatosMenu()
        {
            InitializeComponent();
            CargarCategoriasComida();
            CargarComidas();
            cbCategoriaC.SelectedIndexChanged += cbCategoriaC_SelectedIndexChanged;
            dgvComidas.CellClick += dgvComidas_CellClick;

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

        private void LimpiarCampos()
        {
            txtNombreC.Clear();
            txtDescripcionC.Clear();
            txtPrecioU.Clear();
            if (cbCategoriaB.Items.Count > 0) cbCategoriaB.SelectedIndex = 0;
        }

        private void dgvComidas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0) // Evitar clic en encabezado
            {
                // Seleccionar la fila
                dgvComidas.Rows[e.RowIndex].Selected = true;

                // Obtener la fila seleccionada
                DataGridViewRow row = dgvComidas.Rows[e.RowIndex];

                // Extraer ID y nombre del plato
                int idPlato = Convert.ToInt32(row.Cells["ID Plato"].Value);
                string nombrePlato = row.Cells["Nombre Plato"].Value.ToString();

                // Abrir el formulario de receta con los datos seleccionados
                FormRecetaPlato formReceta = new FormRecetaPlato(idPlato, nombrePlato);
                formReceta.ShowDialog();
            }

        }


        private void btnActualizarPlato_Click(object sender, EventArgs e)
        {
            this.Close();
            FormComidasMenuAdmin FormMenuAdmin = new FormComidasMenuAdmin();
            FormMenuAdmin.Show();

        }

        private void btnCrearPlato_Click(object sender, EventArgs e)
        {
            string nombrePlato = txtNombreC.Text.Trim();
            string descripcion = txtDescripcionC.Text.Trim();
            decimal precioUnitario;

            if (!decimal.TryParse(txtPrecioU.Text, out precioUnitario) || precioUnitario <= 0)
            {
                MessageBox.Show("Ingrese un precio válido mayor a 0.");
                return;
            }

            if (cbCategoriaB.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            int idCategoria = cbCategoriaB.SelectedIndex + 1; // Asegúrate de que el índice coincide con la base de datos

            bool exito = conexion.AgregarPlato(nombrePlato, precioUnitario, descripcion, idCategoria);

            if (exito)
            {
                MessageBox.Show("Plato agregado exitosamente.");
                LimpiarCampos();
                CargarComidas(); // Para actualizar la tabla
            }
            else
            {
                MessageBox.Show("No se pudo agregar el plato.");
            }
        }

        private void btnEliminarC_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormComidasMenuAdmin formplatos = new FormComidasMenuAdmin();
            formplatos.ShowDialog();
        }

        private void FormPlatosMenu_Load(object sender, EventArgs e)
        {

        }

        private void cbCategoriaB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbCategoriaC_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnRegresarInv_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormInventarioAdmin forminventario = new FormInventarioAdmin();
            forminventario.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}