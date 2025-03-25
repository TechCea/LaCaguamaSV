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
    public partial class FormAdmin: Form
    {

        public FormAdmin()
        {
            InitializeComponent();
            CargarOrdenes();

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void CargarOrdenes()
        {
            dataGridViewOrdenesAdmin.DataSource = OrdenesService.ListarOrdenes();

            dataGridViewOrdenesAdmin.Columns["id_orden"].Visible = false;
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            FormUsuariosAdmin formUsuarios = new FormUsuariosAdmin();
            formUsuarios.ShowDialog();
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

        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenuAdmin formMenu = new FormMenuAdmin();
            formMenu.ShowDialog();
        }

        private void Ordenes_Click(object sender, EventArgs e)
        {
            
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            // Asegurar que se seleccione toda la fila al hacer clic
            dataGridViewOrdenesAdmin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrdenesAdmin.MultiSelect = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAdminFunciones formFunciones = new FormAdminFunciones();
            formFunciones.ShowDialog();
        }

        private void dataGridViewOrdenesAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que no sea un encabezado
            {
                // Seleccionar toda la fila al hacer clic en cualquier celda
                dataGridViewOrdenesAdmin.Rows[e.RowIndex].Selected = true;

                DataGridViewRow row = dataGridViewOrdenesAdmin.Rows[e.RowIndex];

                int idOrden = Convert.ToInt32(row.Cells["id_orden"].Value);
                string nombreCliente = row.Cells["nombreCliente"].Value.ToString();
                decimal total = Convert.ToDecimal(row.Cells["total"].Value);
                decimal descuento = Convert.ToDecimal(row.Cells["descuento"].Value);
                string fechaOrden = row.Cells["fecha_orden"].Value.ToString();
                string numeroMesa = row.Cells["numero_mesa"].Value.ToString();
                string tipoPago = row.Cells["tipo_pago"].Value.ToString();
                string nombreUsuario = row.Cells["nombre_usuario"].Value.ToString();
                string estadoOrden = row.Cells["estado_orden"].Value.ToString();

                FormGestionOrdenes formOrden = new FormGestionOrdenes(idOrden, nombreCliente, total, descuento, fechaOrden, numeroMesa, tipoPago, nombreUsuario, estadoOrden);
                formOrden.ShowDialog();
            }
        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            using (CrearOrden formOrden = new CrearOrden())
            {
                formOrden.ShowDialog(); // Mostrar como modal
                CargarOrdenes(); // Refrescar lista de órdenes después de cerrar
            }

        }
    }
}
