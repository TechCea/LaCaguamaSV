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

        public FormComidasMenuAdmin()
        {
            InitializeComponent();
            CargarCategoriasComida();
            CargarComidas();
            cbCategoriaC.SelectedIndexChanged += cbCategoriaC_SelectedIndexChanged; // OJO: Usar cbCategoriaC (minúscula)
        }

        private void CargarComidas()
        {
            dgvComidas.DataSource = conexion.ObtenerComidas();  // Asegúrate de tener este método en tu clase Conexion
            dgvComidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarCategoriasComida()
        {
            DataTable dtCategorias = conexion.ObtenerCategoriasComida();  // Asegúrate de tener este método en Conexion
            cbCategoriaC.Items.Add("Todas"); // Opción para mostrar todas las comidas

            foreach (DataRow row in dtCategorias.Rows)
            {
                cbCategoriaC.Items.Add(row["tipo"].ToString());
            }

            cbCategoriaC.SelectedIndex = 0; // Selecciona "Todas" por defecto
        }

        private void cbCategoriaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategoriaC.SelectedItem != null)
            {
                string categoriaSeleccionada = cbCategoriaC.SelectedItem.ToString();

                if (categoriaSeleccionada == "Todas")
                {
                    CargarComidas();
                }
                else
                {
                    dgvComidas.DataSource = conexion.ObtenerComidasPorCategoria(categoriaSeleccionada); // Implementa este método en tu clase Conexion
                    dgvComidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }

        private void dgvComidas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Puedes agregar funcionalidad aquí si quieres editar o eliminar
        }

        private void btnRegresarMenu_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra este formulario
        }
    }
}
