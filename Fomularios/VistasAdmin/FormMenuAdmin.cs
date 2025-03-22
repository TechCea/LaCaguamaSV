using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormMenuAdmin : Form
    {
        public FormMenuAdmin()
        {
            InitializeComponent();
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
    }
}
