using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormMenuAdmin : Form
    {
        public FormMenuAdmin()
        {
            InitializeComponent();

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnBebidas_Click(object sender, EventArgs e)
        {
            FormBebidasMenuAdmin formBebida = new FormBebidasMenuAdmin();
            formBebida.ShowDialog();
        }

        private void btnComidas_Click(object sender, EventArgs e)
        {
            FormComidasMenuAdmin formComida = new FormComidasMenuAdmin();
            formComida.ShowDialog();
        }

        private void btnExtras_Click(object sender, EventArgs e)
        {
            FormExtrasMenuAdmin formExtras = new FormExtrasMenuAdmin();
            formExtras.ShowDialog();
        }

        private void FormMenuAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
