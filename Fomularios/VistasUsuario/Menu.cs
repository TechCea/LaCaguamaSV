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
            using (FormCartaBebidas formBebidas = new FormCartaBebidas())
            {
                this.Hide(); // Oculta el menú
                formBebidas.ShowDialog(); // Muestra el formulario de bebidas de forma modal
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de bebidas se cierra
            }
        }

        // Redirige al formulario de extras
        private void btnEMU_Click(object sender, EventArgs e)
        {
            using (FormCartaExtras formExtras = new FormCartaExtras())
            {
                this.Hide(); // Oculta el menú
                formExtras.ShowDialog(); // Muestra el formulario de extras de forma modal
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de extras se cierra
            }
        }

        // Redirige al formulario de platos
        private void btnCMU_Click(object sender, EventArgs e)
        {
            using (FormCartaPlatos formPlatos = new FormCartaPlatos())
            {
                this.Hide(); // Oculta el menú
                formPlatos.ShowDialog(); // Muestra el formulario de platos de forma modal
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de platos se cierra
            }
        }

        // Cierra la aplicación
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Cierra la aplicación completamente
        }
    }
}
