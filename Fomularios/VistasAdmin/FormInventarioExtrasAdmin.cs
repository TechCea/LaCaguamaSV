using System;
using System.Data;
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

            // Solo usando los elementos originales
            CargarDatosIniciales();
            ConfigurarDataGridView();
        }

        private void CargarDatosIniciales()
        {
            try
            {
                // Cargar datos principales
                DataTable extras = conexion.ObtenerExtrasCompletos();
                dgvInventarioE.DataSource = extras;

                // Cargar proveedores con los nombres de columnas correctos
                DataTable proveedores = conexion.ObtenerProveedores();

                if (proveedores == null || proveedores.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron proveedores registrados");
                    return;
                }

                // Configuración segura del ComboBox
                cbxProveedor.BeginUpdate();
                cbxProveedor.DataSource = null;
                cbxProveedor.Items.Clear();

                // Usar los nombres EXACTOS de las columnas como están en el DataTable
                cbxProveedor.DataSource = proveedores;
                cbxProveedor.DisplayMember = "Nombre";  // "Nombre" con mayúscula como en tu consulta
                cbxProveedor.ValueMember = "ID";        // "ID" con mayúscula como en tu consulta
                cbxProveedor.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvInventarioE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventarioE.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dgvInventarioE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que se seleccionó una fila válida (no el encabezado)
            {
                try
                {
                    // Obtener la fila seleccionada
                    DataGridViewRow fila = dgvInventarioE.Rows[e.RowIndex];

                    // Cargar datos en los TextBox
                    txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                    txtCantida.Text = fila.Cells["Cantidad"].Value?.ToString() ?? "";
                    txtPrecio.Text = fila.Cells["Precio"].Value?.ToString() ?? "";

                    // Cargar el proveedor en el ComboBox
                    if (fila.Cells["ID_Proveedor"].Value != null &&
                        fila.Cells["ID_Proveedor"].Value != DBNull.Value)
                    {
                        int idProveedor = Convert.ToInt32(fila.Cells["ID_Proveedor"].Value);
                        cbxProveedor.SelectedValue = idProveedor;
                    }
                    else
                    {
                        cbxProveedor.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los datos: {ex.Message}");
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conexion.AgregarExtraConInventario(
                    txtNombre.Text,
                    decimal.Parse(txtPrecio.Text),
                    int.Parse(txtCantida.Text),
                    (int)cbxProveedor.SelectedValue))
                {
                    MessageBox.Show("Extra agregado correctamente");
                    CargarDatosIniciales();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvInventarioE.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow fila = dgvInventarioE.SelectedRows[0];
                    int idExtra = Convert.ToInt32(fila.Cells["ID"].Value);
                    int idInventario = Convert.ToInt32(fila.Cells["ID_Inventario"].Value);

                    if (conexion.ActualizarExtraConInventario(
                        idExtra,
                        idInventario,
                        txtNombre.Text,
                        decimal.Parse(txtPrecio.Text),
                        int.Parse(txtCantida.Text),
                        (int)cbxProveedor.SelectedValue))
                    {
                        MessageBox.Show("Extra actualizado correctamente");
                        CargarDatosIniciales();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar: " + ex.Message);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvInventarioE.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("¿Eliminar este extra?", "Confirmar",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow fila = dgvInventarioE.SelectedRows[0];
                        int idExtra = Convert.ToInt32(fila.Cells["ID"].Value);
                        int idInventario = Convert.ToInt32(fila.Cells["ID_Inventario"].Value);

                        if (conexion.EliminarExtraConInventario(idExtra, idInventario))
                        {
                            MessageBox.Show("Extra eliminado correctamente");
                            CargarDatosIniciales();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message);
                    }
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Mantenemos estos métodos vacíos para compatibilidad con el diseñador
        private void dgvInventarioE_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtCantida_TextChanged(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}