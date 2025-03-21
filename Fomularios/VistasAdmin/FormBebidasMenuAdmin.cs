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
            cbCategoria.SelectedIndexChanged += CbCategoria_SelectedIndexChanged; // Evento para filtrar
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

        }

        private void btnActualizarB_Click(object sender, EventArgs e)
        {
            //if (dgvBebidas.SelectedRows.Count > 0)
            //{
            //    int idBebida = Convert.ToInt32(dgvBebidas.SelectedRows[0].Cells["ID Bebida"].Value);
            //    string nuevoNombre = txtNombreB.Text;
            //    string nuevaCategoria = cbCategoriaB.SelectedItem.ToString();
            //    decimal nuevoPrecio;

            //    if (!decimal.TryParse(txtPrecioU.Text, out nuevoPrecio))
            //    {
            //        MessageBox.Show("Ingrese un precio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    if (conexion.EditarBebida(idBebida, nuevoNombre, nuevaCategoria, nuevoPrecio))
            //    {
            //        MessageBox.Show("Bebida actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        CargarBebidas(); // Recargar datos en la tabla
            //    }
            //    else
            //    {
            //        MessageBox.Show("No se pudo actualizar la bebida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Seleccione una bebida para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void btnEliminarB_Click(object sender, EventArgs e)
        {
            //if (dgvBebidas.SelectedRows.Count > 0)
            //{
            //    // Obtener el ID de la bebida seleccionada
            //    int idBebida = Convert.ToInt32(dgvBebidas.SelectedRows[0].Cells["ID Bebida"].Value);

            //    // Confirmación antes de eliminar
            //    DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta bebida?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //    if (resultado == DialogResult.Yes)
            //    {
            //        if (conexion.EliminarBebida(idBebida))
            //        {
            //            MessageBox.Show("Bebida eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            CargarBebidas(); // Recargar la tabla después de eliminar
            //        }
            //        else
            //        {
            //            MessageBox.Show("No se pudo eliminar la bebida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Seleccione una bebida para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void cbCategoriaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvBebidas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0) // Asegurar que se hace clic en una fila válida
            //{
            //    CargarDatosBebidaSeleccionada(e.RowIndex);
            //}
        }

        private void CargarDatosBebidaSeleccionada(int rowIndex)
        {
            //// Obtener los valores de la fila seleccionada
            //txtNombreB.Text = dgvBebidas.Rows[rowIndex].Cells["Nombre Bebida"].Value.ToString();
            //txtPrecioU.Text = dgvBebidas.Rows[rowIndex].Cells["Precio Unitario"].Value.ToString();

            //// Obtener la categoría de la bebida
            //string categoria = dgvBebidas.Rows[rowIndex].Cells["Categoría"].Value.ToString();

            //// Seleccionar la categoría en el ComboBox
            //cbCategoriaB.SelectedItem = categoria;
        }

    }
}
