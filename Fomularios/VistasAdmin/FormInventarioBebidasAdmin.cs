using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormInventarioBebidasAdmin : Form
    {

        private Conexion conexion = new Conexion();

        public FormInventarioBebidasAdmin()
        {
            InitializeComponent();

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarProveedores();
            CargarCategorias();
            CargarDisponibilidad();
            dgvInventarioB.SelectionChanged += dgvInventarioB_SelectionChanged;
            dgvInventarioB.MultiSelect = false;
            this.Load += FormInventarioBebidasAdmin_Load;
            dgvInventarioB.DataBindingComplete += dgvInventarioB_DataBindingComplete;
        }
  
        private void FormInventarioBebidasAdmin_MouseDown(object sender, MouseEventArgs e)
        {
           
        }


        // Carga el inventario de bebidas en el DataGridView
        private void CargarInventarioBebidas()
        {
            dgvInventarioB.DataSource = conexion.ObtenerInventarioBebidas();
            dgvInventarioB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AplicarColoresDisponibilidad();
        }

        // Carga la lista de proveedores en el ComboBox (cbxProveedor)
        private void CargarProveedores()
        {
            DataTable dtProveedores = conexion.ObtenerProveedores();
            if (dtProveedores != null && dtProveedores.Rows.Count > 0)
            {
                cbxProveedor.DataSource = dtProveedores;
                cbxProveedor.DisplayMember = "Nombre"; // Asegúrate de que en tu consulta se use este alias
                cbxProveedor.ValueMember = "ID";
            }
        }

        // Carga la lista de categorías en el ComboBox (cbxCategoria)
        private void CargarCategorias()
        {
            try
            {
                DataTable categorias = conexion.ObtenerCategorias();

                if (categorias == null)
                {
                    MessageBox.Show("La consulta devolvió null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (categorias.Rows.Count == 0)
                {
                    MessageBox.Show("No hay categorías registradas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string infoColumnas = "Columnas en el DataTable:\n";
                foreach (DataColumn col in categorias.Columns)
                {
                    infoColumnas += $"- {col.ColumnName} (Tipo: {col.DataType.Name})\n";
                }

                string infoDatos = "Primeros 3 registros:\n";
                for (int i = 0; i < Math.Min(3, categorias.Rows.Count); i++)
                {
                    infoDatos += $"Fila {i}: ID: {categorias.Rows[i]["id_categoria"]}, Tipo: {categorias.Rows[i]["tipo"]}\n";
                }
                Console.WriteLine(infoColumnas + infoDatos);

                cbxCategoria.BeginUpdate(); // Para evitar parpadeos
                cbxCategoria.DataSource = null; // Limpiar primero

                // Crear una nueva vista para asegurar los nombres de columnas
                DataView vistaCategorias = new DataView(categorias);
                cbxCategoria.DataSource = vistaCategorias;
                cbxCategoria.DisplayMember = "tipo";
                cbxCategoria.ValueMember = "id_categoria";

                cbxCategoria.EndUpdate();

                if (cbxCategoria.Items.Count == 0)
                {
                    MessageBox.Show("El ComboBox se configuró pero no contiene items", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        // Cuando se selecciona una fila en el DataGridView, se muestran los datos en los controles
        private void dgvInventarioB_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventarioB.SelectedRows.Count > 0)
            {
                DataRowView row = dgvInventarioB.SelectedRows[0].DataBoundItem as DataRowView;
                if (row != null)
                {
                    txtNombre.Text = row["Nombre"].ToString();
                    txtCantida.Text = row["Cantidad"].ToString();
                    txtPrecio.Text = row["Precio"].ToString();
                    cbxProveedor.SelectedValue = row["ID_Proveedor"]; 
                    cbxCategoria.SelectedValue = row["ID_Categoria"];   
                    cbc_disponibilidad.SelectedValue = row["ID_Disponibilidad"];

                    btnAgregar.Enabled = false;
                }
            }
        }

        // Limpia los campos de entrada
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCantida.Clear();
            txtPrecio.Clear();
        }

        // Agregar un nuevo registro de inventario de bebida
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (!decimal.TryParse(txtCantida.Text.Trim(), out decimal cantidad))
            {
                MessageBox.Show("Ingrese una cantidad válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("Ingrese un precio válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idProveedor = Convert.ToInt32(cbxProveedor.SelectedValue);
            int idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
            int idDisponibilidad = Convert.ToInt32(cbc_disponibilidad.SelectedValue); 

            if (conexion.AgregarInventarioBebida(nombre, cantidad, precio, idProveedor, idCategoria, idDisponibilidad))
            {
                MessageBox.Show("Bebida agregada correctamente al inventario.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarInventarioBebidas();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al agregar la bebida al inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Actualizar el registro seleccionado
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvInventarioB.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro de inventario para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idInventario = Convert.ToInt32(dgvInventarioB.SelectedRows[0].Cells["ID"].Value);
            string nombre = txtNombre.Text.Trim();
            if (!decimal.TryParse(txtCantida.Text.Trim(), out decimal cantidad))
            {
                MessageBox.Show("Ingrese una cantidad válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("Ingrese un precio válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idProveedor = Convert.ToInt32(cbxProveedor.SelectedValue);
            int idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
            int idDisponibilidad = Convert.ToInt32(cbc_disponibilidad.SelectedValue); 

            if (conexion.ActualizarInventarioBebida(idInventario, nombre, cantidad, precio, idProveedor, idCategoria, idDisponibilidad))
            {
                MessageBox.Show("Inventario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarInventarioBebidas();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar el inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDisponibilidad()
        {
            try
            {
                DataTable disponibilidad = conexion.ObtenerDisponibilidad(); 

                if (disponibilidad == null)
                {
                    MessageBox.Show("La consulta devolvió null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (disponibilidad.Rows.Count == 0)
                {
                    MessageBox.Show("No hay registros de disponibilidad.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cbc_disponibilidad.BeginUpdate();
                cbc_disponibilidad.DataSource = null;

                DataView vistaDisponibilidad = new DataView(disponibilidad);
                cbc_disponibilidad.DataSource = vistaDisponibilidad;
                cbc_disponibilidad.DisplayMember = "nombreDis";
                cbc_disponibilidad.ValueMember = "id_disponibilidad";

                cbc_disponibilidad.EndUpdate();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al cargar disponibilidad: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void AplicarColoresDisponibilidad()
        {
            foreach (DataGridViewRow row in dgvInventarioB.Rows)
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // Regresar al menú
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormInventarioAdmin forminventario = new FormInventarioAdmin();
            forminventario.ShowDialog();
        }


        // Los demás eventos de cambio de texto se dejan vacíos si no se requiere funcionalidad adicional
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtCantida_TextChanged(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dgvInventarioB_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void FormInventarioBebidasAdmin_Load(object sender, EventArgs e)
        {
            CargarInventarioBebidas();

        }

        private void dgvInventarioB_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AplicarColoresDisponibilidad();
            btnAgregar.Enabled = true;
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {

            txtNombre.Clear();
            txtCantida.Clear();
            txtPrecio.Clear();

            cbxProveedor.SelectedIndex = -1;
            cbxCategoria.SelectedIndex = -1;
            cbc_disponibilidad.SelectedIndex = -1;

            btnAgregar.Enabled = true;

            dgvInventarioB.ClearSelection();
        }
    }
}
