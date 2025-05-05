using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public partial class FormExtrasMenuAdmin : Form
    {
        


        private Conexion conexion = new Conexion(); // Instancia de la clase de conexión

        public FormExtrasMenuAdmin()
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
            


            CargarExtras(); // Llamamos la función al iniciar el formulario
            dgvExtras.DataSource = conexion.ObtenerExtras();
            dgvExtras.SelectionChanged += dgvExtras_SelectionChanged;
            dgvExtras.MultiSelect = false;
        }

        private void CargarExtras()
        {
            dgvExtras.DataSource = conexion.ObtenerExtras();
        }        
        
        private void limpiar()
        {
            txtNombreE.Clear();
            txtPrecioUE.Clear();
        }

        private void dgvExtras_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvExtras.SelectedRows.Count > 0)
            {
                // Obtener el ID y el nombre del extra desde la fila seleccionada
                int idExtra = Convert.ToInt32(dgvExtras.SelectedRows[0].Cells["ID"].Value);
                string nombreExtra = dgvExtras.SelectedRows[0].Cells["Nombre"].Value.ToString();
                decimal precioExtra = Convert.ToDecimal(dgvExtras.SelectedRows[0].Cells["Precio Unitario"].Value);

                // Asignar el nombre y el precio a los TextBox
                txtNombreE.Text = nombreExtra;
                txtPrecioUE.Text = precioExtra.ToString("0.00");

            }
        }

        private void btnActualizarB_Click(object sender, EventArgs e)
        {
            if (dgvExtras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un extra de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el idExtra de la fila seleccionada en el DataGridView
            int idExtra = Convert.ToInt32(dgvExtras.SelectedRows[0].Cells["ID"].Value);
            string nuevoNombre = txtNombreE.Text.Trim();
            string precioTexto = txtPrecioUE.Text.Trim();

            if (string.IsNullOrEmpty(nuevoNombre) || string.IsNullOrEmpty(precioTexto))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(precioTexto, out decimal nuevoPrecio) || nuevoPrecio <= 0)
            {
                MessageBox.Show("Ingrese un precio válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Actualizamos el extra con los nuevos datos
            Conexion conexion = new Conexion();
            if (conexion.ActualizarExtra(idExtra, nuevoNombre, nuevoPrecio))
            {
                MessageBox.Show("Extra actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarExtras(); // Recarga los datos del DataGridView
                limpiar();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el extra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresarMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMenuAdmin FormMenuAdmin = new FormMenuAdmin();
            FormMenuAdmin.Show();
        }

        private void FormExtrasMenuAdmin_Load(object sender, EventArgs e)
        {

        }
        private void FormExtrasMenuAdmin_MouseDown(object sender, MouseEventArgs e)
        {

        }
        public class RoundedControl
        {
            public static void ApplyRoundedCorners(Control control, int radius)
            {
              
            }
        }
    }
}

