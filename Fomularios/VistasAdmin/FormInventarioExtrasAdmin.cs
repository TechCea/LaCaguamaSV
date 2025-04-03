using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormInventarioExtrasAdmin : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private Conexion conexion = new Conexion();
        private int idExtraSeleccionado = -1;
        private int idInventarioSeleccionado = -1;

        public FormInventarioExtrasAdmin()
        {
            InitializeComponent();

            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Agregar evento para mover el formulario
            this.MouseDown += FormInventarioExtrasAdmin_MouseDown;

            // Aplicar esquinas redondeadas a controles
            RoundedControl.ApplyRoundedCorners(dgvInventarioE, 15);
            RoundedControl.ApplyRoundedCorners(gbDatosExtras, 15);

            // Cargar datos al iniciar
            CargarDatosIniciales();
            dgvInventarioE.SelectionChanged += dgvInventarioE_SelectionChanged;
        }

        public class RoundedControl
        {
            public static void ApplyRoundedCorners(Control control, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }

        private void FormInventarioExtrasAdmin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void CargarDatosIniciales()
        {
            try
            {
                // Cargar datos principales
                DataTable extras = conexion.ObtenerExtrasCompletos();
                dgvInventarioE.DataSource = extras;

                // Cargar proveedores
                DataTable proveedores = conexion.ObtenerProveedores();

                if (proveedores == null || proveedores.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron proveedores registrados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Configuración del ComboBox
                cbxProveedor.BeginUpdate();
                cbxProveedor.DataSource = null;
                cbxProveedor.Items.Clear();

                // Verificar nombres de columnas
                string columnasDisponibles = "Columnas disponibles:\n";
                foreach (DataColumn col in proveedores.Columns)
                {
                    columnasDisponibles += $"- {col.ColumnName}\n";
                }

                // Usar nombres de columnas exactos (ajusta según tu base de datos)
                string displayMember = proveedores.Columns.Contains("Nombre") ? "Nombre" : "nombreProv";
                string valueMember = proveedores.Columns.Contains("ID") ? "ID" : "id_proveedor";

                cbxProveedor.DataSource = proveedores;
                cbxProveedor.DisplayMember = displayMember;
                cbxProveedor.ValueMember = valueMember;
                cbxProveedor.EndUpdate();

                // Configurar DataGridView
                dgvInventarioE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvInventarioE.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvInventarioE.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvInventarioE_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventarioE.SelectedRows.Count > 0 && !dgvInventarioE.SelectedRows[0].IsNewRow)
            {
                try
                {
                    DataRowView row = dgvInventarioE.SelectedRows[0].DataBoundItem as DataRowView;
                    if (row != null)
                    {
                        // Cargar datos en los TextBox
                        txtNombre.Text = row["Nombre"]?.ToString() ?? "";
                        txtCantida.Text = row["Cantidad"]?.ToString() ?? "0.00";
                        txtPrecio.Text = row["Precio"]?.ToString() ?? "0.00";

                        // Cargar proveedor en ComboBox
                        if (row["ID_Proveedor"] != null && row["ID_Proveedor"] != DBNull.Value)
                        {
                            cbxProveedor.SelectedValue = Convert.ToInt32(row["ID_Proveedor"]);
                        }
                        else
                        {
                            cbxProveedor.SelectedIndex = -1;
                        }

                        // Guardar IDs para operaciones
                        idExtraSeleccionado = row["ID"] != DBNull.Value ? Convert.ToInt32(row["ID"]) : -1;
                        idInventarioSeleccionado = row["ID_Inventario"] != DBNull.Value ? Convert.ToInt32(row["ID_Inventario"]) : -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar datos de la fila seleccionada:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese un nombre válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtCantida.Text, out decimal cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida (ejemplo: 5.00)", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
                {
                    MessageBox.Show("Ingrese un precio válido (ejemplo: 12.50)", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbxProveedor.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un proveedor", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProveedor = Convert.ToInt32(cbxProveedor.SelectedValue);

                // Agregar nuevo extra
                if (conexion.AgregarExtraConInventario(txtNombre.Text, precio, cantidad, idProveedor))
                {
                    MessageBox.Show("Extra agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDatosIniciales();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idExtraSeleccionado <= 0 || idInventarioSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un registro para actualizar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese un nombre válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtCantida.Text, out decimal cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida (ejemplo: 5.00)", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
                {
                    MessageBox.Show("Ingrese un precio válido (ejemplo: 12.50)", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbxProveedor.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un proveedor", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProveedor = Convert.ToInt32(cbxProveedor.SelectedValue);

                // Actualizar extra
                if (conexion.ActualizarExtraConInventario(
                    idExtraSeleccionado,
                    idInventarioSeleccionado,
                    txtNombre.Text,
                    precio,
                    cantidad,
                    idProveedor))
                {
                    MessageBox.Show("Extra actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosIniciales();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idExtraSeleccionado <= 0 || idInventarioSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un registro para eliminar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este extra?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (conexion.EliminarExtraConInventario(idExtraSeleccionado, idInventarioSeleccionado))
                    {
                        MessageBox.Show("Extra eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarDatosIniciales();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtCantida.Text = "0.00";
            txtPrecio.Text = "0.00";
            cbxProveedor.SelectedIndex = -1;
            idExtraSeleccionado = -1;
            idInventarioSeleccionado = -1;
            dgvInventarioE.ClearSelection();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormInventarioAdmin forminventario = new FormInventarioAdmin();
            forminventario.ShowDialog();
        }

        // Eventos vacíos para el diseñador
        private void dgvInventarioE_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtCantida_TextChanged(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}