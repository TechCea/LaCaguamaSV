using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        // Redirige al formulario de bebidas
        private void btnBMU_Click(object sender, EventArgs e)
        {
            FormCartaBebidas formBebidas = new FormCartaBebidas();
            formBebidas.Show(); // Muestra el formulario de bebidas
            this.Hide(); // Oculta el formulario actual (Menu)
        }

        // Redirige al formulario de extras
        private void btnEMU_Click(object sender, EventArgs e)
        {
            FormCartaExtras formExtras = new FormCartaExtras();
            formExtras.Show(); // Muestra el formulario de extras
            this.Hide(); // Oculta el formulario actual (Menu)
        }

        // Redirige al formulario de platos
        private void btnCMU_Click(object sender, EventArgs e)
        {
            FormCartaPlatos formPlatos = new FormCartaPlatos();
            formPlatos.Show(); // Muestra el formulario de platos
            this.Hide(); // Oculta el formulario actual (Menu)
        }

        // Cierra la aplicación
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Cierra la aplicación completamente
        }
    }
}

