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
using LaCaguamaSV.Fomularios.VistasAdmin;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormUsuario: Form
    {
        public FormUsuario()
        {
            InitializeComponent();
            CargarOrdenes();

            // Si el usuario no es normal (rol 2), cierra el formulario
            if (SesionUsuario.Rol != 2)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de usuario.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
        }

        private void CargarOrdenes()
        {
            // Limpiar el DataSource para forzar la actualización
            dataGridViewOrdenesUsuario.DataSource = null;
            dataGridViewOrdenesUsuario.DataSource = OrdenesService.ListarOrdenes();

            // Configuración del DataGridView
            dataGridViewOrdenesUsuario.ReadOnly = true;
            dataGridViewOrdenesUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrdenesUsuario.MultiSelect = false;


            // Formatear columnas numéricas
            if (dataGridViewOrdenesUsuario.Columns["total"] != null)
            {
                dataGridViewOrdenesUsuario.Columns["total"].DefaultCellStyle.Format = "C";
            }
            if (dataGridViewOrdenesUsuario.Columns["descuento"] != null)
            {
                dataGridViewOrdenesUsuario.Columns["descuento"].DefaultCellStyle.Format = "C";
            }

            // Autoajustar columnas
            dataGridViewOrdenesUsuario.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        public void RefrescarOrdenes()
        {
            CargarOrdenes();
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {
            // Asegurar que el DataGridView no sea editable y seleccione filas completas
            dataGridViewOrdenesUsuario.ReadOnly = true;
            dataGridViewOrdenesUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrdenesUsuario.MultiSelect = false;
        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            using (CrearOrden formOrden = new CrearOrden())
            {
                formOrden.ShowDialog(); // Mostrar como modal
                CargarOrdenes(); // Refrescar lista de órdenes después de cerrar
            }
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            FormMesasUsuario formmesasU = new FormMesasUsuario();
            formmesasU.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Cierra la sesión
            SesionUsuario.CerrarSesion();

            // Vuelve al formulario de login
            Login loginForm = new Login();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void dataGridViewOrdenesUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurar que no se haga clic en el encabezado
            {
                // Seleccionar fila completa
                dataGridViewOrdenesUsuario.Rows[e.RowIndex].Selected = true;

                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewOrdenesUsuario.Rows[e.RowIndex];

                // Obtener datos de la orden
                int idOrden = Convert.ToInt32(row.Cells["id_orden"].Value);
                string nombreCliente = row.Cells["nombreCliente"].Value.ToString();
                decimal total = Convert.ToDecimal(row.Cells["total"].Value);
                decimal descuento = Convert.ToDecimal(row.Cells["descuento"].Value);
                string fechaOrden = row.Cells["fecha_orden"].Value.ToString();
                string numeroMesa = row.Cells["numero_mesa"].Value.ToString();
                string tipoPago = row.Cells["tipo_pago"].Value.ToString();
                string nombreUsuario = row.Cells["nombre_usuario"].Value.ToString();
                string estadoOrden = row.Cells["estado_orden"].Value.ToString();

                // Abrir formulario de gestión de órdenes con los datos seleccionados
                FormGestionOrdenes formOrden = new FormGestionOrdenes(idOrden, nombreCliente, total,
                                                                      descuento, fechaOrden, numeroMesa,
                                                                      tipoPago, nombreUsuario, estadoOrden);
                formOrden.ShowDialog();

                CargarOrdenes();
            }
        }

    }
}
