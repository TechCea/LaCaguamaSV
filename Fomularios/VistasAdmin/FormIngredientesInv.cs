using LaCaguamaSV.Configuracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormIngredientesInv : Form
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
        private int idIngredienteSeleccionado = -1;

        public FormIngredientesInv()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Agregar evento para mover el formulario
            this.MouseDown += new MouseEventHandler(FormIngredientesInv_MouseDown);

            // Aplicar esquinas redondeadas a controles específicos
            RoundedControl.ApplyRoundedCorners(dgvIngredientes, 15);   // Redondear un Panel
            RoundedControl.ApplyRoundedCorners(gbDatosIngrediente, 15);   // Redondear un Panel



            CargarIngredientes();
            CargarProveedores();
            CargarProveedoresCombo();
            cbFiltrarProv.SelectedIndexChanged += cbFiltrarProv_SelectedIndexChanged;
            dgvIngredientes.SelectionChanged += dgvIngredientes_SelectionChanged;
            btnEliminar.Enabled = false;
            btnActualizarB.Enabled = false;

        }

        private void CargarIngredientes()
        {
            DataTable dt = conexion.ObtenerIngredientes();
            dgvIngredientes.DataSource = conexion.ObtenerIngredientes();
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
        private void FormIngredientesInv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void CargarProveedores()
        {
            DataTable dtProveedores = conexion.ObtenerProveedoresIngredientes();

            // Limpiar y agregar "Todos" como opción predeterminada
            cbFiltrarProv.Items.Clear();
            cbFiltrarProv.Items.Add("Todos");

            // Llenar el ComboBox con los nombres de los proveedores
            foreach (DataRow row in dtProveedores.Rows)
            {
                cbFiltrarProv.Items.Add(row["nombreProv"].ToString());
            }

            // Seleccionar por defecto "Todos"
            cbFiltrarProv.SelectedIndex = 0;
        }

        private void CargarProveedoresCombo()
        {
            DataTable dtProveedores = conexion.ObtenerProveedoresIngredientes();

            // Limpiar ComboBox antes de llenarlo
            cbProveedores.Items.Clear();

            // Llenar el ComboBox con los nombres de los proveedores
            foreach (DataRow row in dtProveedores.Rows)
            {
                cbProveedores.Items.Add(row["nombreProv"].ToString());
            }

            // Opcional: seleccionar el primer proveedor por defecto si hay datos
            if (cbProveedores.Items.Count > 0)
            {
                cbProveedores.SelectedIndex = 0;
            }
        }

        private void cbFiltrarProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string proveedorSeleccionado = cbFiltrarProv.SelectedItem.ToString();

            if (proveedorSeleccionado == "Todos")
            {
                dgvIngredientes.DataSource = conexion.ObtenerIngredientes(); // Mostrar todos
            }
            else
            {
                dgvIngredientes.DataSource = conexion.FiltrarIngredientesPorProveedor(proveedorSeleccionado); // Filtrar por proveedor
            }
        }

        private void dgvIngredientes_SelectionChanged(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el DataGridView
            if (dgvIngredientes.SelectedRows.Count > 0)
            {
                // Obtener los valores del ingrediente seleccionado
                idIngredienteSeleccionado = Convert.ToInt32(dgvIngredientes.SelectedRows[0].Cells["ID"].Value);
                string nombreIngrediente = dgvIngredientes.SelectedRows[0].Cells["Nombre"].Value.ToString();
                string cantidad = dgvIngredientes.SelectedRows[0].Cells["Cantidad"].Value.ToString();
                string proveedor = dgvIngredientes.SelectedRows[0].Cells["Proveedor"].Value.ToString();

                // Mostrar los valores en los controles
                lblSeleccion.Text = $"Se ha seleccionado el ingrediente: {nombreIngrediente}";
                txtNombreC.Text = nombreIngrediente;
                txtCantidad.Text = cantidad;
                cbProveedores.SelectedItem = proveedor;

                // Habilitar botones de eliminar y actualizar
                btnEliminar.Enabled = true;
                btnActualizarB.Enabled = true;
            }
            else
            {
                // Si no hay selección, restablecer valores
                lblSeleccion.Text = "Selecciona un ingrediente.";
                txtNombreC.Clear();
                txtCantidad.Clear();
                cbProveedores.SelectedIndex = -1; // Deseleccionar cualquier opción

                // Deshabilitar botones de eliminar y actualizar
                btnEliminar.Enabled = false;
                btnActualizarB.Enabled = false;
            }
        }
        

        private int ObtenerIdProveedor(string nombreProveedor)
        {
            DataTable dt = conexion.ObtenerProveedoresIngredientes(); 
            foreach (DataRow row in dt.Rows)
            {
                if (row["nombreProv"].ToString() == nombreProveedor)
                {
                    return Convert.ToInt32(row["id_proveedor"]);
                }
            }
            return -1; // Si no se encuentra, retorna un valor que indique error
        }

        private void btnAgregarIng_Click(object sender, EventArgs e)
        {
            // Obtener datos de los controles
            string nombreProducto = txtNombreC.Text;
            decimal cantidad;
            bool esCantidadValida = decimal.TryParse(txtCantidad.Text, out cantidad);
            string proveedorSeleccionado = cbProveedores.SelectedItem.ToString();

            // Verificar que la cantidad sea válida
            if (!esCantidadValida)
            {
                MessageBox.Show("Por favor ingresa una cantidad válida.");
                return;
            }

            // Obtener el ID del proveedor
            int idProveedor = ObtenerIdProveedor(proveedorSeleccionado);

            if (idProveedor == -1)
            {
                MessageBox.Show("Proveedor no encontrado.");
                return;
            }

            // Llamar a la función de agregar ingrediente
            bool resultado = conexion.AgregarIngrediente(nombreProducto, cantidad, idProveedor);

            if (resultado)
            {
                MessageBox.Show("Ingrediente agregado correctamente.");
                // Recargar la lista de ingredientes
                dgvIngredientes.DataSource = conexion.ObtenerIngredientes();
            }
            else
            {
                MessageBox.Show("Error al agregar el ingrediente.");
            }

        }

        private void btnActualizarB_Click(object sender, EventArgs e)
        {
            if (idIngredienteSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un ingrediente antes de actualizar.");
                return;
            }

            string nuevoNombre = txtNombreC.Text;
            decimal nuevaCantidad;

            if (!decimal.TryParse(txtCantidad.Text, out nuevaCantidad))
            {
                MessageBox.Show("Por favor ingresa una cantidad válida.");
                return;
            }

            string proveedorSeleccionado = cbProveedores.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(proveedorSeleccionado))
            {
                MessageBox.Show("Selecciona un proveedor.");
                return;
            }

            // Obtener el ID del proveedor
            int nuevoIdProveedor = ObtenerIdProveedor(proveedorSeleccionado);
            if (nuevoIdProveedor == -1)
            {
                MessageBox.Show("Proveedor no encontrado.");
                return;
            }

            // Llamar a la función para actualizar el ingrediente
            bool resultado = conexion.EditarIngrediente(idIngredienteSeleccionado, nuevoNombre, nuevaCantidad, nuevoIdProveedor);

            if (resultado)
            {
                MessageBox.Show("Ingrediente actualizado correctamente.");
                dgvIngredientes.DataSource = conexion.ObtenerIngredientes(); // Recargar la tabla
            }
            else
            {
                MessageBox.Show("Error al actualizar el ingrediente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvIngredientes.SelectedRows.Count > 0)
            {
                // Obtener el ID del ingrediente seleccionado
                int idIngrediente = Convert.ToInt32(dgvIngredientes.SelectedRows[0].Cells["ID"].Value);

                // Confirmación antes de eliminar
                DialogResult confirmacion = MessageBox.Show("¿Estás seguro de que deseas eliminar este ingrediente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    // Llamar a la función para eliminar el ingrediente
                    bool resultado = conexion.EliminarIngrediente(idIngrediente);

                    if (resultado)
                    {
                        MessageBox.Show("Ingrediente eliminado correctamente.");
                        // Recargar los ingredientes en el DataGridView después de eliminar
                        CargarIngredientes();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el ingrediente.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un ingrediente para eliminar.");
            }
        }

        private void btncrearPlato_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPlatosMenu formplatos = new FormPlatosMenu();
            formplatos.ShowDialog();
        }

        private void FormIngredientesInv_Load(object sender, EventArgs e)
        {

        }
    }
}
