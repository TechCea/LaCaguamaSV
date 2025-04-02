using LaCaguamaSV.Configuracion;
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
    public partial class FormPlatosMenu : Form
    {
        Conexion conexion = new Conexion();

        public FormPlatosMenu()
        {
            InitializeComponent();
            CargarCategoriasComida();
            CargarComidas();
            cbCategoriaC.SelectedIndexChanged += cbCategoriaC_SelectedIndexChanged;

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

        }


        private void btnActualizarPlato_Click(object sender, EventArgs e)
        {
            this.Close();
            FormComidasMenuAdmin FormMenuAdmin = new FormComidasMenuAdmin();
            FormMenuAdmin.Show();

        }


        private void btnAgregarIng_Click(object sender, EventArgs e)
        {

        }
    }
}
