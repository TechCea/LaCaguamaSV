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
                this.Hide();
                this.Close();// Oculta el menú
                formProveedor.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }
        }

        private void btnBebidas_Click(object sender, EventArgs e)
        {


            using (FormInventarioBebidasAdmin formInventarioBebidas = new FormInventarioBebidasAdmin())
            {
                this.Hide();
                this.Close();// Oculta el menú
                formInventarioBebidas.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }
        }

        private void btnComidas_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            FormIngredientesInv formIngredientes = new FormIngredientesInv();
            formIngredientes.ShowDialog();
        }

        private void btnExtras_Click(object sender, EventArgs e)
        {
            using (FormInventarioExtrasAdmin FormInventarioExtras = new FormInventarioExtrasAdmin())
            {
                this.Hide();
                this.Close();
                FormInventarioExtras.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }

        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormInventarioAdmin_Load(object sender, EventArgs e)
        {

        }
        private void FormInventarioAdmin_MouseDown(object sender, MouseEventArgs e)
        {
           
        }
       
    }
}
