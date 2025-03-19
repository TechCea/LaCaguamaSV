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
    public partial class FormGestionOrdenes: Form
    {
        public FormGestionOrdenes()
        {
            InitializeComponent();
            CargarOrdenes();
        }

        private void CargarOrdenes()
        {
            dataGridViewOrdenes.DataSource = OrdenesService.ListarOrdenes();
        }

        private void FormGestionOrdenes_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewOrdenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
