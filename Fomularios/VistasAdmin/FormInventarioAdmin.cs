using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Fomularios.VistasUsuario;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormInventarioAdmin : Form
    {
        public FormInventarioAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FormGestionarProvAdmin formProveedor = new FormGestionarProvAdmin())
            {
                this.Hide(); // Oculta el menú
                formProveedor.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }
        }

        private void btnBebidas_Click(object sender, EventArgs e)
        {


            using (FormInventarioBebidasAdmin formInventarioBebidas = new FormInventarioBebidasAdmin())
            {
                this.Hide(); // Oculta el menú
                formInventarioBebidas.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }
        }

        private void btnComidas_Click(object sender, EventArgs e)
        {
            FormIngredientesInv formIngredientes = new FormIngredientesInv();
            formIngredientes.ShowDialog();
        }
    }
}
