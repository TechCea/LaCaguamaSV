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

        public FormInventarioBebidasAdmin()
        {
            InitializeComponent();

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Agregar evento para mover el formulario
            this.MouseDown += new MouseEventHandler(FormInventarioBebidasAdmin_MouseDown);

            // Aplicar esquinas redondeadas a controles específicos
            RoundedControl.ApplyRoundedCorners(dgvInventarioB, 15);   // Redondear un Panel
            RoundedControl.ApplyRoundedCorners(gbDatosExtras, 15);   // Redondear un Panel






            // Cargar datos al iniciar
            CargarInventarioBebidas();
            CargarProveedores();
            CargarCategorias();
            dgvInventarioB.SelectionChanged += dgvInventarioB_SelectionChanged;
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
        private void FormInventarioBebidasAdmin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }






        // Carga el inventario de bebidas en el DataGridView
        private void CargarInventarioBebidas()
        {
            dgvInventarioB.DataSource = conexion.ObtenerInventarioBebidas();
            dgvInventarioB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                // 1. Obtener datos
                DataTable categorias = conexion.ObtenerCategorias();

                // 2. Verificación exhaustiva
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

                // 3. Diagnóstico detallado (puedes eliminar esto después)
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

                // 4. Configuración del ComboBox (versión reforzada)
                cbxCategoria.BeginUpdate(); // Para evitar parpadeos
                cbxCategoria.DataSource = null; // Limpiar primero

                // Crear una nueva vista para asegurar los nombres de columnas
                DataView vistaCategorias = new DataView(categorias);
                cbxCategoria.DataSource = vistaCategorias;
                cbxCategoria.DisplayMember = "tipo";
                cbxCategoria.ValueMember = "id_categoria";

                cbxCategoria.EndUpdate();

                // 5. Verificación final
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
                    // Asignar los ComboBox (Proveedor y Categoría) usando los valores:\n
                    cbxProveedor.SelectedValue = row["ID_Proveedor"]; // Asegúrate de que la columna se llame así en la consulta\n
                    cbxCategoria.SelectedValue = row["ID_Categoria"];   // Asegúrate de que la consulta lo retorne con ese alias\n
                }
            }
        }

        // Limpia los campos de entrada
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCantida.Clear();
            txtPrecio.Clear();
            // Opcional: restablecer la selección de los ComboBox si lo deseas
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

            if (conexion.AgregarInventarioBebida(nombre, cantidad, precio, idProveedor, idCategoria))
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

            if (conexion.ActualizarInventarioBebida(idInventario, nombre, cantidad, precio, idProveedor, idCategoria))
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

        // Eliminar el registro seleccionado
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvInventarioB.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro de inventario para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idInventario = Convert.ToInt32(dgvInventarioB.SelectedRows[0].Cells["ID"].Value);
            DialogResult dr = MessageBox.Show("¿Está seguro de eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                if (conexion.EliminarInventarioBebida(idInventario))
                {
                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarInventarioBebidas();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        }
    }
}
