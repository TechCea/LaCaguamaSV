using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using System;
using System.Data;
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



        private void txtDescripcionC_TextChanged(object sender, EventArgs e)
        {
            // Evento generado por el diseñador, puedes dejarlo vacío
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Evento generado por el diseñador, puedes dejarlo vacío
        }

        private void txtNombreC_TextChanged(object sender, EventArgs e)
        {
            // Puedes dejarlo vacío o agregar lógica si quieres
        }

        private void txtPrecioU_TextChanged(object sender, EventArgs e)
        {
            // Puedes dejarlo vacío
        }


        private void cbCategoriaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Puedes dejarlo vacío si no usarás nada por ahora
        }

        private void dgvComidas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes dejarlo vacío o meterle lógica si luego la necesitas
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

        private void btnEliminarC_Click(object sender, EventArgs e)
        {
            if (idPlatoSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un plato para eliminar");
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Estás seguro de eliminar este plato?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion == DialogResult.Yes)
            {
                if (conexion.EliminarPlato(idPlatoSeleccionado))
                {
                    MessageBox.Show("Plato eliminado");
                    CargarComidas();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
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
    }
}
