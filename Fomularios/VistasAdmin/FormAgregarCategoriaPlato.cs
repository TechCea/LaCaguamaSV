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
    public partial class FormAgregarCategoriaPlato : Form
    {
        public FormAgregarCategoriaPlato()
        {
            InitializeComponent();
            this.Load += FormAgregarCategoriaPlato_Load;
            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvCategoriasCom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategoriasCom.ReadOnly = true;
            dgvCategoriasCom.MultiSelect = false;
            dgvCategoriasCom.AllowUserToAddRows = false;
            dgvCategoriasCom.AllowUserToDeleteRows = false;
            dgvCategoriasCom.AllowUserToResizeRows = false;

            dgvCategoriasCom.CellClick += dgvCategoriasCom_CellClick;
            btnActualizar.Click += btnActualizar_Click;

        }

        private void CargarCategorias()
        {
            Conexion conexion = new Conexion(); // Asegurate de tener una instancia válida
            dgvCategoriasCom.DataSource = conexion.ObtenerCategoriasPlatos();

            // Opcional: renombrar columnas o configurar apariencia
            dgvCategoriasCom.Columns["id_categoriaP"].HeaderText = "ID";
            dgvCategoriasCom.Columns["tipo"].HeaderText = "Categoría";
        }

        private void FormAgregarCategoriaPlato_Load(object sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void btnCrearCat_Click(object sender, EventArgs e)
        {
            string nuevaCategoria = txtNuevaCategoria.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevaCategoria))
            {
                MessageBox.Show("Por favor, ingresa un nombre para la categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Conexion conexion = new Conexion();
            bool exito = conexion.AgregarCategoriaPlato(nuevaCategoria);

            if (exito)
            {
                MessageBox.Show("Categoría agregada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNuevaCategoria.Clear(); // limpia el textbox
                CargarCategorias(); // actualiza el DataGridView
            }
            else
            {
                MessageBox.Show("No se pudo agregar la categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int idCategoriaSeleccionada = -1;

        private void dgvCategoriasCom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvCategoriasCom.Rows[e.RowIndex];
                idCategoriaSeleccionada = Convert.ToInt32(fila.Cells["id_categoriaP"].Value);
                txtNuevaCategoria.Text = fila.Cells["tipo"].Value.ToString();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == -1)
            {
                MessageBox.Show("Selecciona una categoría para actualizar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nuevoNombre = txtNuevaCategoria.Text.Trim();
            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                MessageBox.Show("Ingresa un nuevo nombre para la categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Conexion conexion = new Conexion();
            bool exito = conexion.ActualizarCategoriaPlato(idCategoriaSeleccionada, nuevoNombre);

            if (exito)
            {
                MessageBox.Show("Categoría actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNuevaCategoria.Clear();
                idCategoriaSeleccionada = -1;
                CargarCategorias(); // actualiza el DataGridView
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }


}
