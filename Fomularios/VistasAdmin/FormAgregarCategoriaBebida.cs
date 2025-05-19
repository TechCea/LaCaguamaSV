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
    public partial class FormAgregarCategoriaBebida : Form
    {
        public FormAgregarCategoriaBebida()
        {
            InitializeComponent();
            CargarCategoriasBebidas();
            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvCategoriasB.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategoriasB.ReadOnly = true;
            dgvCategoriasB.MultiSelect = false;
            dgvCategoriasB.AllowUserToAddRows = false;
            dgvCategoriasB.AllowUserToDeleteRows = false;
            dgvCategoriasB.AllowUserToResizeRows = false;

            dgvCategoriasB.CellClick += dgvCategoriasB_CellClick;
        }

        private void CargarCategoriasBebidas()
        {
            Conexion conn = new Conexion();
            dgvCategoriasB.DataSource = conn.ObtenerCategoriasBebidas();

            // Opcional: ajustar estilos
            dgvCategoriasB.Columns["ID"].Width = 60;
            dgvCategoriasB.Columns["Categoría"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnCrearCat_Click(object sender, EventArgs e)
        {
            string nuevaCategoria = txtNuevaCategoria.Text.Trim();

            if (string.IsNullOrEmpty(nuevaCategoria))
            {
                MessageBox.Show("Por favor, ingresa un nombre para la nueva categoría.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Conexion conn = new Conexion();
            bool exito = conn.AgregarCategoriaBebida(nuevaCategoria);

            if (exito)
            {
                MessageBox.Show("Categoría agregada exitosamente 🎉", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNuevaCategoria.Clear();
                CargarCategoriasBebidas(); // actualiza el dgv
            }
            else
            {
                MessageBox.Show("No se pudo agregar la categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int idCategoriaSeleccionada = -1;

        private void dgvCategoriasB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvCategoriasB.Rows[e.RowIndex];
                idCategoriaSeleccionada = Convert.ToInt32(fila.Cells["ID"].Value); // Asegurate que esta columna esté definida así
                txtNuevaCategoria.Text = fila.Cells["Categoría"].Value.ToString(); // O ajustá al nombre de columna real
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == -1)
            {
                MessageBox.Show("Selecciona una categoría para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nuevoNombre = txtNuevaCategoria.Text.Trim();

            if (string.IsNullOrEmpty(nuevoNombre))
            {
                MessageBox.Show("El nombre de la categoría no puede estar vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Conexion conn = new Conexion();
            bool resultado = conn.ActualizarCategoriaBebida(idCategoriaSeleccionada, nuevoNombre);

            if (resultado)
            {
                MessageBox.Show("Categoría actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNuevaCategoria.Clear();
                idCategoriaSeleccionada = -1;
                CargarCategoriasBebidas(); // Asegúrate de tener esta función implementada
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
