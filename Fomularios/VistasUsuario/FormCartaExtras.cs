using System;
using System.Data;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormCartaExtras : Form
    {
        private Conexion conexion;

        public FormCartaExtras()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private void FormCartaExtras_Load(object sender, EventArgs e)
        {
            // Cargar los extras
            dgvCartaE.DataSource = conexion.ObtenerExtras();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar el formulario
        }

        private void dgvCartaE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes agregar la lógica para cuando se haga click en una celda, si es necesario.
        }
    }
}

