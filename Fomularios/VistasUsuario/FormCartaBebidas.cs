using System;
using System.Data;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormCartaBebidas : Form
    {
        private Conexion conexion;

        public FormCartaBebidas()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private void FormCartaBebidas_Load(object sender, EventArgs e)
        {
            // Cargar las bebidas sin filtro
            dgvCartaB.DataSource = conexion.ObtenerBebidas();

            // Cargar categorías de bebidas en el ComboBox
            cbxFiltrarCB.DataSource = conexion.ObtenerCategorias();
            cbxFiltrarCB.DisplayMember = "tipo"; // Nombre de la columna para mostrar
            cbxFiltrarCB.ValueMember = "tipo"; // Valor a seleccionar
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar el formulario
        }

        private void dgvCartaB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes agregar la lógica para cuando se haga click en una celda, si es necesario.
        }

        private void cbxFiltrarCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoriaSeleccionada = cbxFiltrarCB.SelectedValue.ToString();
            dgvCartaB.DataSource = conexion.ObtenerBebidasPorCategoria(categoriaSeleccionada);
        }
    }
}
