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


        private Conexion conexion = new Conexion();
        private int idIngredienteSeleccionado = -1;
        

        public FormIngredientesInv()
        {
            InitializeComponent();

            dgvIngredientes.MultiSelect = false;

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar


            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvIngredientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIngredientes.ReadOnly = true;
            dgvIngredientes.MultiSelect = false;
            dgvIngredientes.AllowUserToAddRows = false;
            dgvIngredientes.AllowUserToDeleteRows = false;
            dgvIngredientes.AllowUserToResizeRows = false;

            CargarIngredientes();
            CargarProveedores();
            CargarProveedoresCombo();
            CargarUnidadesCombo();
            cbFiltrarProv.SelectedIndexChanged += cbFiltrarProv_SelectedIndexChanged;
            dgvIngredientes.CellClick += dgvIngredientes_CellClick;
            btnActualizarB.Enabled = false;
            CargarDisponibilidadCombo();
            dgvIngredientes.CellFormatting += dgvIngredientes_CellFormatting;

        }

        private void CargarIngredientes()
        {
            DataTable dt = conexion.ObtenerIngredientes();
            dgvIngredientes.DataSource = conexion.ObtenerIngredientes();
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
                dgvIngredientes.DataSource = conexion.ObtenerIngredientes(); 
            }
            else
            {
                dgvIngredientes.DataSource = conexion.FiltrarIngredientesPorProveedor(proveedorSeleccionado); 
            }
        }

        private void dgvIngredientes_CellClick(object sender, EventArgs e)
        {
            if (dgvIngredientes.SelectedRows.Count > 0)
            {
                idIngredienteSeleccionado = Convert.ToInt32(dgvIngredientes.SelectedRows[0].Cells["ID"].Value);
                string nombreIngrediente = dgvIngredientes.SelectedRows[0].Cells["Nombre"].Value.ToString();
                string cantidad = dgvIngredientes.SelectedRows[0].Cells["Cantidad"].Value.ToString();
                string proveedor = dgvIngredientes.SelectedRows[0].Cells["Proveedor"].Value.ToString();
                string disponibilidad = dgvIngredientes.SelectedRows[0].Cells["Disponibilidad"].Value.ToString();
                string unidad = dgvIngredientes.SelectedRows[0].Cells["Unidad"].Value.ToString(); // 👈 Nuevo

                // Asignar valores a los controles
                txtNombreC.Text = nombreIngrediente;
                txtCantidad.Text = null;
                cbProveedores.SelectedItem = proveedor;
                cbc_disponibilidad.SelectedIndex = cbc_disponibilidad.FindStringExact(disponibilidad);
                cbb_unidad.SelectedIndex = cbb_unidad.FindStringExact(unidad); // 👈 Nuevo

                lblSeleccion.Text = $"Se ha seleccionado el ingrediente: {nombreIngrediente}";
                label1.Text = "Cantidad a sumar: ";

                // Control de botones
                btnActualizarB.Enabled = true;
                btnAgregarIng.Enabled = false;
            }
            else
            {
                lblSeleccion.Text = "Selecciona un ingrediente.";
                txtNombreC.Clear();
                txtCantidad.Clear();
                cbProveedores.SelectedIndex = -1;
                cbb_unidad.SelectedIndex = -1;
                cbc_disponibilidad.SelectedIndex = -1;
                label1.Text = "Cantidad a agregar: ";

                btnActualizarB.Enabled = false;
                btnAgregarIng.Enabled = true;
            }
        }

        private void dgvIngredientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Solo procesamos si la fila es válida (no encabezado, etc.)
            if (e.RowIndex >= 0)
            {
                // Buscamos la columna de "Disponibilidad" por nombre (ajusta si tiene otro nombre)
                var disponibilidadCell = dgvIngredientes.Rows[e.RowIndex].Cells["Disponibilidad"];

                if (disponibilidadCell.Value != null)
                {
                    string disponibilidad = disponibilidadCell.Value.ToString().ToLower();

                    Color colorTexto = Color.Black; // Por defecto

                    if (disponibilidad == "disponible")
                    {
                        colorTexto = Color.Green;
                    }
                    else if (disponibilidad == "no disponible")
                    {
                        colorTexto = Color.Red;
                    }

                    // Aplicamos el color al texto de cada celda de la fila
                    e.CellStyle.ForeColor = colorTexto;
                }
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
            if (string.IsNullOrWhiteSpace(txtNombreC.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del producto.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Por favor, ingrese la cantidad.");
                return;
            }

            if (cbProveedores.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un proveedor.");
                return;
            }

            if (cbc_disponibilidad.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecciona una disponibilidad.");
                return;
            }

            if (cbb_unidad.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecciona una unidad.");
                return;
            }

            string nombreProducto = txtNombreC.Text;
            decimal cantidad;
            if (!decimal.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida y mayor a cero.");
                return;
            }

            string proveedorSeleccionado = cbProveedores.SelectedItem.ToString();
            int idProveedor = ObtenerIdProveedor(proveedorSeleccionado);

            if (idProveedor == -1)
            {
                MessageBox.Show("Proveedor no encontrado.");
                return;
            }

            int idUnidad = Convert.ToInt32(cbb_unidad.SelectedValue);
            int idDisponibilidad = Convert.ToInt32(cbc_disponibilidad.SelectedValue);

            bool resultado = conexion.AgregarIngrediente(nombreProducto, cantidad, idProveedor, idUnidad, idDisponibilidad);

            if (resultado)
            {
                MessageBox.Show("Ingrediente agregado correctamente.");
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

            int nuevoIdProveedor = ObtenerIdProveedor(proveedorSeleccionado);
            if (nuevoIdProveedor == -1)
            {
                MessageBox.Show("Proveedor no encontrado.");
                return;
            }

            if (cbc_disponibilidad.SelectedValue == null)
            {
                MessageBox.Show("Selecciona una disponibilidad.");
                return;
            }

            int nuevoIdDisponibilidad = Convert.ToInt32(cbc_disponibilidad.SelectedValue);

            if (cbb_unidad.SelectedValue == null)
            {
                MessageBox.Show("Selecciona una unidad.");
                return;
            }

            int nuevoIdUnidad = Convert.ToInt32(cbb_unidad.SelectedValue); // 👈 nuevo dato

            // Llama la función con el nuevo parámetro
            bool resultado = conexion.EditarIngrediente(
                idIngredienteSeleccionado,
                nuevoNombre,
                nuevaCantidad,
                nuevoIdProveedor,
                nuevoIdDisponibilidad,
                nuevoIdUnidad 
            );

            if (resultado)
            {
                MessageBox.Show("Ingrediente actualizado correctamente.");
                dgvIngredientes.DataSource = conexion.ObtenerIngredientes();
            }
            else
            {
                MessageBox.Show("Error al actualizar el ingrediente.");
            }
        }

        private void CargarDisponibilidadCombo()
        {
            DataTable dtDisponibilidad = conexion.ObtenerDisponibilidad();

            // Limpiar cualquier ítem previo
            cbc_disponibilidad.DataSource = null;
            cbc_disponibilidad.Items.Clear();

            // Configurar DataSource
            cbc_disponibilidad.DataSource = dtDisponibilidad;
            cbc_disponibilidad.DisplayMember = "nombreDis"; // Lo que el usuario verá
            cbc_disponibilidad.ValueMember = "id_disponibilidad"; // El valor real asociado
        }

        private void btncrearPlato_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPlatosMenu formplatos = new FormPlatosMenu();
            formplatos.ShowDialog();
        }

        private void CargarUnidadesCombo()
        {
            DataTable dtUnidades = conexion.ObtenerUnidades();
            cbb_unidad.DataSource = dtUnidades;
            cbb_unidad.DisplayMember = "nombreUnidad"; // Lo que el usuario verá
            cbb_unidad.ValueMember = "id_unidad"; // El valor real usado internamente
        }

        private void btnRegresarMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_limpiarcampos_Click(object sender, EventArgs e)
        {
            // Limpia los campos
            txtNombreC.Clear();
            txtCantidad.Clear();
            cbProveedores.SelectedIndex = -1;
            cbc_disponibilidad.SelectedIndex = -1;
            lblSeleccion.Text = "Campos limpios. Listo para agregar un nuevo ingrediente.";

            // Rehabilita el botón de agregar
            btnAgregarIng.Enabled = true;

            //Deshabilitar actualizar
            btnActualizarB.Enabled = false;

            // Reseteamos la variable por si acaso
            idIngredienteSeleccionado = -1;
        }
    }
}
