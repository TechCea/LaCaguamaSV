﻿using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormGestionarProvAdmin : Form
    {
   

        private Conexion conexion = new Conexion();

        public FormGestionarProvAdmin()
        {
            InitializeComponent();
            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvProveedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedor.ReadOnly = true;
            dgvProveedor.MultiSelect = false;
            dgvProveedor.AllowUserToAddRows = false;
            dgvProveedor.AllowUserToDeleteRows = false;
            dgvProveedor.AllowUserToResizeRows = false;


            CargarProveedores();
            dgvProveedor.CellClick += dgvProveedor_CellClick;
            dgvProveedor.MultiSelect = false;
        }

        // Carga los proveedores en el DataGridView
        private void CargarProveedores()
        {
            dgvProveedor.DataSource = conexion.ObtenerProveedores();
        }

        // Limpia los campos de texto
        private void LimpiarCampos()
        {
            txtNombreP.Clear();
            txtContacto.Clear();
            txtDirecion.Clear();
        }

        private void dgvProveedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProveedor.Rows[e.RowIndex];
                txtNombreP.Text = row.Cells["Nombre"].Value.ToString();
                txtContacto.Text = row.Cells["Contacto"].Value.ToString();
                txtDirecion.Text = row.Cells["Direccion"].Value.ToString();
            }
        }

        // Agrega un nuevo proveedor
        private void btnAgregarP_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreP.Text.Trim();
            string contacto = txtContacto.Text.Trim();
            string direccion = txtDirecion.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contacto) || string.IsNullOrEmpty(direccion))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (conexion.AgregarProveedor(nombre, contacto, direccion))
            {
                MessageBox.Show("Proveedor agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProveedores();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al agregar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Actualiza el proveedor seleccionado
        private void btnActualizarP_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un proveedor de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProveedor = Convert.ToInt32(dgvProveedor.SelectedRows[0].Cells["ID"].Value);
            string nombre = txtNombreP.Text.Trim();
            string contacto = txtContacto.Text.Trim();
            string direccion = txtDirecion.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contacto) || string.IsNullOrEmpty(direccion))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (conexion.ActualizarProveedor(idProveedor, nombre, contacto, direccion))
            {
                MessageBox.Show("Proveedor actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProveedores();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Elimina el proveedor seleccionado
        private void btnEliminarE_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un proveedor para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProveedor = Convert.ToInt32(dgvProveedor.SelectedRows[0].Cells["ID"].Value);
            DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar este proveedor?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (conexion.EliminarProveedor(idProveedor))
                {
                    MessageBox.Show("Proveedor eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProveedores();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Regresa al menú
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Los métodos de eventos de cambio de texto se dejan vacíos si no se necesita funcionalidad adicional.
        private void txtNombreP_TextChanged(object sender, EventArgs e) { }
        private void txtContacto_TextChanged(object sender, EventArgs e) { }
        private void txtDirecion_TextChanged(object sender, EventArgs e) { }
        private void dgvProveedor_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
