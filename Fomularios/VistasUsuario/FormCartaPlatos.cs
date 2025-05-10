using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormCartaPlatos : Form
    {
        private Conexion conexion = new Conexion();

        public FormCartaPlatos()
        {
            InitializeComponent();
            CargarCategoriasComida();
            CargarComidas();
            cbxFiltrarCP.SelectedIndexChanged += cbxFiltrarCP_SelectedIndexChanged;
            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CargarComidas()
        {
            dgvCartaP.DataSource = conexion.ObtenerComidas();
            dgvCartaP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarCategoriasComida()
        {
            DataTable dtCategorias = conexion.ObtenerCategoriasComida();
            cbxFiltrarCP.Items.Add("Todas"); // Opción para mostrar todas las comidas

            foreach (DataRow row in dtCategorias.Rows)
            {
                cbxFiltrarCP.Items.Add(row["tipo"].ToString());
            }

            cbxFiltrarCP.SelectedIndex = 0; // Selecciona "Todas" por defecto
        }

        private void cbxFiltrarCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFiltrarCP.SelectedItem != null)
            {
                string categoriaSeleccionada = cbxFiltrarCP.SelectedItem.ToString();

                if (categoriaSeleccionada == "Todas")
                {
                    CargarComidas();
                }
                else
                {
                    dgvCartaP.DataSource = conexion.ObtenerComidasPorCategoria(categoriaSeleccionada);
                    dgvCartaP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario
        }

        private void dgvCartaP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes agregar funcionalidad aquí si quieres editar o eliminar
        }
    }
}
