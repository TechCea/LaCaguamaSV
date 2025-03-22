using System;
using System.Data;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormCartaPlatos : Form
    {
        private Conexion conexion;

        public FormCartaPlatos()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private void FormCartaPlatos_Load(object sender, EventArgs e)
        {
            // Cargar los platos sin filtro
            dgvCartaP.DataSource = conexion.ObtenerComidas();

            // Cargar categorías en el ComboBox
            cbxFiltrarCP.DataSource = conexion.ObtenerCategoriasComida();
            cbxFiltrarCP.DisplayMember = "tipo"; // Nombre de la columna para mostrar
            cbxFiltrarCP.ValueMember = "tipo"; // Valor a seleccionar
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar el formulario
        }

        private void dgvCartaP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes agregar la lógica para cuando se haga click en una celda, si es necesario.
        }

        private void cbxFiltrarCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoriaSeleccionada = cbxFiltrarCP.SelectedValue.ToString();
            dgvCartaP.DataSource = conexion.ObtenerComidasPorCategoria(categoriaSeleccionada);
        }
    }
}
