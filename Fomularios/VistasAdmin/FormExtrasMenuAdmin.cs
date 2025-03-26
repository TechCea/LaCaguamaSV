using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
            CargarExtras(); // Llamamos la función al iniciar el formulario
            dgvExtras.DataSource = conexion.ObtenerExtras();
            dgvExtras.SelectionChanged += dgvExtras_SelectionChanged;
        }

        private void CargarExtras()
        {
            dgvExtras.DataSource = conexion.ObtenerExtras();
        }

        private void btnEliminarE_Click(object sender, EventArgs e)
        {
            if (dgvExtras.SelectedRows.Count > 0) // Verifica si hay una fila seleccionada
            {
                int idExtra = Convert.ToInt32(dgvExtras.SelectedRows[0].Cells["ID"].Value); // Obtiene el ID del extra seleccionado

                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este extra?",
                                                      "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes) // Si el usuario confirma
                {
                    Conexion conexion = new Conexion();
                    if (conexion.EliminarExtra(idExtra))
                    {
                        MessageBox.Show("Extra eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarExtras(); // Recarga los datos del DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el extra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un extra para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCrearExtra_Click(object sender, EventArgs e)
        {
            string nombreExtra = txtNombreE.Text.Trim();
            string precioTexto = txtPrecioUE.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nombreExtra) || string.IsNullOrEmpty(precioTexto))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que el precio sea un número válido
            if (!decimal.TryParse(precioTexto, out decimal precioExtra) || precioExtra <= 0)
            {
                MessageBox.Show("Ingrese un precio válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insertar en la base de datos
            Conexion conexion = new Conexion();
            if (conexion.AgregarExtra(nombreExtra, precioExtra))
            {
                MessageBox.Show("Extra agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombreE.Clear();
                txtPrecioUE.Clear();
                CargarExtras(); // Recargar DataGridView
            }
            else
            {
                MessageBox.Show("No se pudo agregar el extra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvExtras_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvExtras.SelectedRows.Count > 0)
            {
                // Obtener el nombre del extra desde la fila seleccionada
                string nombreExtra = dgvExtras.SelectedRows[0].Cells["nombre"].Value.ToString();

                // Mostrar el nombre en el label
                lblSeleccionExtra.Text = $"Extra seleccionado: {nombreExtra}";
            }
        }

        private void btnActualizarB_Click(object sender, EventArgs e)
        {
            if (dgvExtras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un extra de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            Conexion conexion = new Conexion();
            if (conexion.ActualizarExtra(idExtra, nuevoNombre, nuevoPrecio))
            {
                MessageBox.Show("Extra actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombreE.Clear();
                txtPrecioUE.Clear();
                CargarExtras();
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
    }
}

