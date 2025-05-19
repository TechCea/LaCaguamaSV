using LaCaguamaSV.Configuracion;
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
    public partial class FormAgregarUnidades : Form
    {
        private Conexion conexion = new Conexion();
        public FormAgregarUnidades()
        {
            InitializeComponent();
            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvUnidades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnidades.ReadOnly = true;
            dgvUnidades.MultiSelect = false;
            dgvUnidades.AllowUserToAddRows = false;
            dgvUnidades.AllowUserToDeleteRows = false;
            dgvUnidades.AllowUserToResizeRows = false;

            dgvUnidades.CellClick += dgvUnidades_CellClick;

            CargarUnidades();
        }

        private void CargarUnidades()
        {
            DataTable unidades = conexion.ObtenerUnidades();
            dgvUnidades.DataSource = unidades;

            if (dgvUnidades.Columns.Count > 0)
            {
                dgvUnidades.Columns["id_unidad"].HeaderText = "ID";
                dgvUnidades.Columns["nombreUnidad"].HeaderText = "Unidad";
            }
        }

        private void btnCrearU_Click(object sender, EventArgs e)
        {
            string nuevaUnidad = txtNuevaU.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevaUnidad))
            {
                MessageBox.Show("Por favor, escribe el nombre de la unidad.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool creada = conexion.CrearUnidad(nuevaUnidad);

            if (creada)
            {
                MessageBox.Show("Unidad creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNuevaU.Clear();
                CargarUnidades(); // Recarga el DataGridView con los nuevos datos
            }
            else
            {
                MessageBox.Show("No se pudo crear la unidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? idUnidadSeleccionada = null;

        private void dgvUnidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUnidades.Rows[e.RowIndex];
                idUnidadSeleccionada = Convert.ToInt32(fila.Cells["id_unidad"].Value);
                txtNuevaU.Text = fila.Cells["nombreUnidad"].Value.ToString();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idUnidadSeleccionada == null)
            {
                MessageBox.Show("Selecciona una unidad primero.", "Nada seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nuevoNombre = txtNuevaU.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                MessageBox.Show("Por favor, escribe el nuevo nombre de la unidad.", "Campo vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool actualizado = conexion.ActualizarUnidad(idUnidadSeleccionada.Value, nuevoNombre);

            if (actualizado)
            {
                MessageBox.Show("Unidad actualizada correctamente.", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNuevaU.Clear();
                idUnidadSeleccionada = null;
                CargarUnidades();
            }
            else
            {
                // Ya se muestra un mensaje en caso de error desde el método
            }
        }
    }
}
