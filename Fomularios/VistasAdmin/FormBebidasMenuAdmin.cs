using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
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
    public partial class FormBebidasMenuAdmin : Form
    {
        Conexion conexion = new Conexion();

        public FormBebidasMenuAdmin()
        {
            InitializeComponent();
            CargarCategorias();
            CargarBebidas();
            cbCategoria.SelectedIndexChanged += CbCategoria_SelectedIndexChanged; // Evento para filtrar
        }

        private void CargarBebidas()
        {
            dgvBebidas.DataSource = conexion.ObtenerBebidas();
        }

        private void CargarCategorias()
        {
            DataTable dtCategorias = conexion.ObtenerCategorias();
            cbCategoria.Items.Add("Todas"); // Opción para mostrar todas las bebidas

            foreach (DataRow row in dtCategorias.Rows)
            {
                cbCategoria.Items.Add(row["tipo"].ToString());
            }

            cbCategoria.SelectedIndex = 0; // Selecciona "Todas" por defecto
        }

        private void CbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoriaSeleccionada = cbCategoria.SelectedItem.ToString();

            if (categoriaSeleccionada == "Todas")
            {
                CargarBebidas();
            }
            else
            {
                dgvBebidas.DataSource = conexion.ObtenerBebidasPorCategoria(categoriaSeleccionada);
            }
        }
    }
}
