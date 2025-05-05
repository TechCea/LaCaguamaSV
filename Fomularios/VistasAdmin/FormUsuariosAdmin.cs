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
    public partial class FormUsuariosAdmin: Form
    {
        Conexion conexion = new Conexion();
        public FormUsuariosAdmin()
        {
            InitializeComponent();

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarUsuarios();

            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink; // Evita el parpadeo
            errorProvider1.ContainerControl = this;
            dgvUsuarios.MultiSelect = false;
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = conexion.ObtenerUsuarios();
            // Oculta la columna del ID para que no se vea
            dgvUsuarios.Columns["ID"].Visible = false;

        }


        private void CargarRoles()
        {
            DataTable dtRoles = conexion.ObtenerRoles(); // Necesitas crear este método en `CConexio`
            cmbRol.DataSource = dtRoles;
            cmbRol.DisplayMember = "nombre_rol";
            cmbRol.ValueMember = "id_rol";
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            // Validar que solo contenga números
            if (!string.IsNullOrEmpty(txtTelefono.Text) && System.Text.RegularExpressions.Regex.IsMatch(txtTelefono.Text, "[^0-9]"))
            {
                int posicion = txtTelefono.SelectionStart - 1;
                txtTelefono.Text = txtTelefono.Text.Remove(posicion, 1);
                txtTelefono.SelectionStart = posicion;

                MostrarError(txtTelefono, "⚠ Solo se permiten números");
                return;
            }

            // Limpiar error si todo está bien
            errorProvider1.SetError(txtTelefono, "");
        }

        private void txtNombreU_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtNombreU.Text, "[^a-zA-ZáéíóúÁÉÍÓÚñÑ ]"))
            {
                errorProvider1.SetError(txtNombreU, "Solo letras y espacios permitidos");
                txtNombreU.Text = txtNombreU.Text.Remove(txtNombreU.Text.Length - 1);
                txtNombreU.SelectionStart = txtNombreU.Text.Length;
            }
            else
            {
                errorProvider1.SetError(txtNombreU, "");
            }
        }

        private void txtNombreU_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreU.Text))
            {
                MostrarError(txtNombreU, "⚠ El campo Nombre Completo es obligatorio");
            }
            else if (txtNombreU.Text.Length < 3)
            {
                MostrarError(txtNombreU, "⚠ El nombre debe tener al menos 3 caracteres");
            }
            else
            {
                errorProvider1.SetError(txtNombreU, "");
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MostrarError(txtUsuario, "⚠ Debes ingresar un nombre de usuario");
            }
            else if (txtUsuario.Text.Length < 4)
            {
                MostrarError(txtUsuario, "⚠ El usuario debe tener al menos 4 caracteres");
            }
            else
            {
                errorProvider1.SetError(txtUsuario, "");
            }
        }

        private void MostrarError(Control control, string mensaje)
        {
            errorProvider1.SetError(control, mensaje);

            // Opcional: mostrar tooltip adicional
            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipTitle = "Error de validación";
            toolTip.Show(mensaje, control, 0, control.Height, 2000); // Muestra por 2 segundos
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MostrarError(txtCorreo, "⚠ El correo electrónico es obligatorio");
            }
            else if (!EsCorreoValido(txtCorreo.Text))
            {
                MostrarError(txtCorreo, "⚠ Formato de correo inválido\nEjemplo: usuario@dominio.com");
            }
            else
            {
                errorProvider1.SetError(txtCorreo, "");
            }
        }

        private bool EsCorreoValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void txtContraseñaU_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseñaU.Text))
            {
                MostrarError(txtContraseñaU, "⚠ Debes ingresar una contraseña");
            }
            else if (txtContraseñaU.Text.Length < 6)
            {
                MostrarError(txtContraseñaU, "⚠ La contraseña debe tener al menos 6 caracteres");
            }
            else
            {
                errorProvider1.SetError(txtContraseñaU, "");
            }
        }

        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            // Validar campo obligatorio (si lo deseas)
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MostrarError(txtTelefono, "⚠ El teléfono es obligatorio");
                return;
            }

            // Validar longitud mínima
            if (txtTelefono.Text.Length < 8)
            {
                MostrarError(txtTelefono, "⚠ El teléfono debe tener al menos 8 dígitos");
                return;
            }

            // Validar longitud máxima (si lo deseas)
            if (txtTelefono.Text.Length > 12)
            {
                MostrarError(txtTelefono, "⚠ El teléfono no puede exceder 12 dígitos");
                return;
            }

            // Si todo está correcto, limpiar errores
            errorProvider1.SetError(txtTelefono, "");
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            txtTelefono.BackColor = SystemColors.Window;
        }
        private void txtContraseñaU_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnAgregarU_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombreU.Text))
            {
                errorProvider1.SetError(txtNombreU, "El nombre es obligatorio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                errorProvider1.SetError(txtCorreo, "El correo es obligatorio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                errorProvider1.SetError(txtUsuario, "El usuario es obligatorio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContraseñaU.Text))
            {
                errorProvider1.SetError(txtContraseñaU, "La contraseña es obligatoria");
                return;
            }

            if (cmbRol.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbRol, "Seleccione un rol");
                return;
            }

            // Validar formato de correo
            if (!EsCorreoValido(txtCorreo.Text))
            {
                errorProvider1.SetError(txtCorreo, "Formato de correo inválido");
                return;
            }

            string nombre = txtNombreU.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContraseñaU.Text;
            string telefono = txtTelefono.Text.Trim();
            int idRol = Convert.ToInt32(cmbRol.SelectedValue);

            if (conexion.AgregarUsuario(nombre, correo, usuario, contrasena, telefono, idRol))
            {
                MessageBox.Show("Usuario agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al agregar usuario");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdUsuario.Text))
            {
                MessageBox.Show("Seleccione un usuario para eliminar.");
                return;
            }

            int idUsuario = Convert.ToInt32(txtIdUsuario.Text);

            DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?",
                                                        "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                if (conexion.EliminarUsuario(idUsuario))
                {
                    MessageBox.Show("Usuario eliminado correctamente.");
                    CargarUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar usuario.");
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdUsuario.Text))
            {
                MessageBox.Show("Seleccione un usuario para editar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            errorProvider1.Clear();

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtNombreU.Text))
            {
                errorProvider1.SetError(txtNombreU, "El nombre es obligatorio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                errorProvider1.SetError(txtCorreo, "El correo es obligatorio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                errorProvider1.SetError(txtUsuario, "El usuario es obligatorio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContraseñaU.Text))
            {
                errorProvider1.SetError(txtContraseñaU, "La contraseña es obligatoria");
                return;
            }

            if (!EsCorreoValido(txtCorreo.Text))
            {
                errorProvider1.SetError(txtCorreo, "Formato de correo inválido");
                return;
            }

            int idUsuario = Convert.ToInt32(txtIdUsuario.Text);
            string nombre = txtNombreU.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContraseñaU.Text;
            string telefono = txtTelefono.Text.Trim();
            int idRol = Convert.ToInt32(cmbRol.SelectedValue);

            if (conexion.EditarUsuario(idUsuario, usuario, nombre, correo, contrasena, telefono, idRol))
            {
                MessageBox.Show("Usuario actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar usuario.");
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarUsuarios();

            if (e.RowIndex >= 0) // Verifica que se hizo clic en una fila válida
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                txtIdUsuario.Text = fila.Cells["ID"].Value.ToString(); // Guardar el ID del usuario
                txtUsuario.Text = fila.Cells["Usuario"].Value.ToString();
                txtNombreU.Text = fila.Cells["Nombre"].Value.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                txtTelefono.Text = fila.Cells["Teléfono"].Value.ToString();
                txtContraseñaU.Text = fila.Cells["Contraseña"].Value.ToString();
                cmbRol.Text = fila.Cells["Rol"].Value.ToString();
            }
        }
        private void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtNombreU.Clear();
            txtCorreo.Clear();
            txtContraseñaU.Clear();
            txtTelefono.Clear();
            cmbRol.SelectedIndex = -1; // Deseleccionar el rol
        }

        private void FormUsuariosAdmin_Load(object sender, EventArgs e)
        {
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
            CargarRoles();
        }

        private void txtIdUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
