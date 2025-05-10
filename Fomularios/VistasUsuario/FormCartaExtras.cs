using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormCartaExtras : Form
    {
        private Conexion conexion = new Conexion();

        public FormCartaExtras()
        {
            InitializeComponent();
            CargarExtras();
            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CargarExtras()
        {
            dgvCartaE.DataSource = conexion.ObtenerExtras();
            dgvCartaE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario
        }

        private void dgvCartaE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes agregar funcionalidad aquí si quieres editar o eliminar
        }
    }
}


