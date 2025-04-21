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

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormAdminIngredientesInv : Form
    {
       

        public FormAdminIngredientesInv()
        {
            InitializeComponent();

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void FormAdminIngredientesInv_Load(object sender, EventArgs e)
        {

            
        }

        private void dgvIngredientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gbDatosIngrediente_Enter(object sender, EventArgs e)
        {

        }

       
        private void FormAdminIngredientesInv_MouseDown(object sender, MouseEventArgs e)
        {
          
        }
    }
}
