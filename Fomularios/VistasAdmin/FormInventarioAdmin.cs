using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using LaCaguamaSV.Fomularios.VistasUsuario;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormInventarioAdmin : Form
    {

        public FormInventarioAdmin()
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (FormGestionarProvAdmin formProveedor = new FormGestionarProvAdmin())
            {
                formProveedor.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }
        }

        private void btnBebidas_Click(object sender, EventArgs e)
        {
            using (FormInventarioBebidasAdmin formInventarioBebidas = new FormInventarioBebidasAdmin())
            {
                formInventarioBebidas.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }
        }

        private void btnComidas_Click(object sender, EventArgs e)
        {
            FormIngredientesInv formIngredientes = new FormIngredientesInv();
            formIngredientes.ShowDialog();
            this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
        }

        private void btnExtras_Click(object sender, EventArgs e)
        {
            using (FormInventarioExtrasAdmin FormInventarioExtras = new FormInventarioExtrasAdmin())
            {
                FormInventarioExtras.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }

        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAgregarUnidades formUnidades = new FormAgregarUnidades();
            formUnidades.ShowDialog();
            this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
        }
    }
}
