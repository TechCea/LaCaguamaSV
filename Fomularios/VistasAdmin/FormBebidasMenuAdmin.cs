using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormBebidasMenuAdmin : Form
    {
        Conexion conexion = new Conexion();

        public FormBebidasMenuAdmin()
        {
            InitializeComponent();
            CargarCategorias();
            CargarBebidas();
            CargarCategoriasBebidas(); // Cargar categorías en cbCategoriaB
            cbCategoria.SelectedIndexChanged += CbCategoria_SelectedIndexChanged;
            dgvBebidas.SelectionChanged += dgvBebidas_SelectionChanged;
        }

        private void CargarBebidas()
        {
            dgvBebidas.DataSource = conexion.ObtenerBebidas();
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
                string nuevaCategoria = cbCategoriaB.SelectedItem.ToString();
                decimal nuevoPrecio;

                if (!decimal.TryParse(txtPrecioU.Text, out nuevoPrecio))
                {
                    MessageBox.Show("Ingrese un precio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (conexion.ActualizarBebida(idBebida, nuevoNombre, nuevaCategoria, nuevoPrecio))
                {
                    MessageBox.Show("Bebida actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarBebidas(); // Recargar la lista después de actualizar
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
                cbCategoriaB.SelectedItem = row.Cells["Categoría"].Value.ToString();
                txtPrecioU.Text = row.Cells["Precio Unitario"].Value.ToString();
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

    }
}
