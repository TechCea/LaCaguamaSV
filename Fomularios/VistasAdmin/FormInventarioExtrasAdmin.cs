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
     
        private Conexion conexion = new Conexion();
        private int idExtraSeleccionado = -1;
        private int idInventarioSeleccionado = -1;

        public FormInventarioExtrasAdmin()
        {
            InitializeComponent();

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            } 

            // Cargar datos al iniciar
            CargarDatosIniciales();
            CargarUnidadesCombo();
            dgvInventarioE.SelectionChanged += dgvInventarioE_SelectionChanged;
            dgvInventarioE.MultiSelect = false;
            btnLimpiar.Click += btnLimpiar_Click;
            dgvInventarioE.DataBindingComplete += dgvInventarioExtras_DataBindingComplete;
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

                // Configuración del ComboBox Proveedor
                cbxProveedor.BeginUpdate();
                cbxProveedor.DataSource = null;
                cbxProveedor.Items.Clear();

                string displayMember = proveedores.Columns.Contains("Nombre") ? "Nombre" : "nombreProv";
                string valueMember = proveedores.Columns.Contains("ID") ? "ID" : "id_proveedor";

                cbxProveedor.DataSource = proveedores;
                cbxProveedor.DisplayMember = displayMember;
                cbxProveedor.ValueMember = valueMember;
                cbxProveedor.EndUpdate();

                CargarDisponibilidad();

                // Configurar DataGridView
                dgvInventarioE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvInventarioE.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvInventarioE.MultiSelect = false;
                AplicarColoresDisponibilidad();
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
                    btnAgregar.Enabled = false;
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

                        if (row["ID_Proveedor"] != null && row["ID_Proveedor"] != DBNull.Value)
                        {
                            cbxProveedor.SelectedValue = Convert.ToInt32(row["ID_Proveedor"]);
                        }
                        else
                        {
                            cbxProveedor.SelectedIndex = -1;
                        }

                        if (row["ID_Disponibilidad"] != null && row["ID_Disponibilidad"] != DBNull.Value)
                        {
                            cbxDisponibilidad.SelectedValue = Convert.ToInt32(row["ID_Disponibilidad"]);
                        }
                        else
                        {
                            cbxDisponibilidad.SelectedIndex = -1;
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
            btnAgregar.Enabled = false;
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

                if (cbb_unidad.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una unidad válida", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbxProveedor.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un proveedor", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProveedor = Convert.ToInt32(cbxProveedor.SelectedValue);

                int idUnidad = Convert.ToInt32(cbb_unidad.SelectedValue);

                if (conexion.AgregarExtraConInventario(txtNombre.Text, precio, cantidad, idProveedor, Convert.ToInt32(cbxDisponibilidad.SelectedValue), idUnidad))
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

                if (cbxDisponibilidad.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una disponibilidad", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbb_unidad.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una unidad", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProveedor = Convert.ToInt32(cbxProveedor.SelectedValue);
                int idDisponibilidad = Convert.ToInt32(cbxDisponibilidad.SelectedValue);
                int idUnidad = Convert.ToInt32(cbb_unidad.SelectedValue);

                if (conexion.ActualizarExtraConInventario(
                    idExtraSeleccionado,
                    idInventarioSeleccionado,
                    txtNombre.Text,
                    precio,
                    cantidad,
                    idProveedor,
                    idDisponibilidad,
                    idUnidad)) // <<--- Este es el nuevo parámetro
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


        private void CargarDisponibilidad()
        {
            try
            {
                DataTable disponibilidad = conexion.ObtenerDisponibilidad(); // Este método ya debería existir en tu clase Conexion

                if (disponibilidad == null)
                {
                    MessageBox.Show("La consulta de disponibilidad devolvió null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (disponibilidad.Rows.Count == 0)
                {
                    MessageBox.Show("No hay registros de disponibilidad.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cbxDisponibilidad.BeginUpdate();
                cbxDisponibilidad.DataSource = null;

                // Configurar DataSource
                DataView vistaDisponibilidad = new DataView(disponibilidad);
                cbxDisponibilidad.DataSource = vistaDisponibilidad;
                cbxDisponibilidad.DisplayMember = "nombreDis"; // Ajusta si tu columna se llama diferente
                cbxDisponibilidad.ValueMember = "id_disponibilidad";

                cbxDisponibilidad.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar disponibilidad: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void AplicarColoresDisponibilidad()
        {
            foreach (DataGridViewRow row in dgvInventarioE.Rows) // Asegúrate de usar el nombre correcto de tu DataGridView
            {
                var celdaDisponibilidad = row.Cells["Disponibilidad"].Value?.ToString();

                if (celdaDisponibilidad != null)
                {
                    if (celdaDisponibilidad.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void dgvInventarioExtras_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AplicarColoresDisponibilidad();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
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
            btnAgregar.Enabled = true;
        }


        private void CargarUnidadesCombo()
        {
            DataTable dtUnidades = conexion.ObtenerUnidades();
            cbb_unidad.DataSource = dtUnidades;
            cbb_unidad.DisplayMember = "nombreUnidad"; // Lo que el usuario verá
            cbb_unidad.ValueMember = "id_unidad"; // El valor real usado internamente
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Eventos vacíos para el diseñador
        private void dgvInventarioE_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtCantida_TextChanged(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e) { }

    }
}