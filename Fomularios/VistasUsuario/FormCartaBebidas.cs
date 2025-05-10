using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormCartaBebidas : Form
    {
        private Conexion conexion = new Conexion();

        public FormCartaBebidas()
        {
            InitializeComponent();
            CargarCategoriasBebidas();
            CargarBebidas();
            cbxFiltrarCB.SelectedIndexChanged += cbxFiltrarCB_SelectedIndexChanged;
            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CargarBebidas()
        {
            dgvCartaB.DataSource = conexion.ObtenerBebidasDisponibles();
            dgvCartaB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarCategoriasBebidas()
        {
            DataTable dtCategorias = conexion.ObtenerCategorias();
            cbxFiltrarCB.Items.Add("Todas"); // Opción para mostrar todas las bebidas

            foreach (DataRow row in dtCategorias.Rows)
            {
                cbxFiltrarCB.Items.Add(row["tipo"].ToString());
            }

            cbxFiltrarCB.SelectedIndex = 0; // Selecciona "Todas" por defecto
        }

        private void cbxFiltrarCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFiltrarCB.SelectedItem != null)
            {
                string categoriaSeleccionada = cbxFiltrarCB.SelectedItem.ToString();

                if (categoriaSeleccionada == "Todas")
                {
                    CargarBebidas();
                }
                else
                {
                    dgvCartaB.DataSource = conexion.ObtenerBebidasPorCategoria(categoriaSeleccionada);
                    dgvCartaB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCartaB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes agregar funcionalidad aquí si quieres editar o eliminar
        }
    }
}
