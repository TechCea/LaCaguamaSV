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
    public partial class FormCrearOrden: Form
    {
        public FormCrearOrden()
        {
            InitializeComponent();
            CargarTipoPago();
            CargarMesas();
        }

        private void CargarTipoPago()
        {
            DataTable dtPagos = OrdenesD.ObtenerTiposPago();
            cmbTipoPago.DataSource = dtPagos;
            cmbTipoPago.DisplayMember = "nombrePago"; // Mostrar el nombre del método de pago
            cmbTipoPago.ValueMember = "id_pago"; // Guardar el ID del método de pago

            // Seleccionar por defecto "Efectivo"
            foreach (DataRow row in dtPagos.Rows)
            {
                if (row["nombrePago"].ToString().ToLower() == "efectivo")
                {
                    cmbTipoPago.SelectedValue = row["id_pago"];
                    break;
                }
            }
        }

        private void CargarMesas()
        {
            DataTable dtMesas = OrdenesD.ObtenerMesas(); // Obtener mesas desde la BD
            cmbMesas.DataSource = dtMesas;
            cmbMesas.DisplayMember = "nombreMesa"; // Mostrar el nombre de la mesa
            cmbMesas.ValueMember = "id_mesa"; // Usar el ID de la mesa como valor

            if (dtMesas.Rows.Count > 0)
            {
                cmbMesas.SelectedIndex = 0; // Seleccionar la primera mesa por defecto
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            // Obtener valores del formulario
            string nombreCliente = txtNombreCliente.Text.Trim();
            int idMesa = Convert.ToInt32(cmbMesas.SelectedValue);
            int tipoPago = Convert.ToInt32(cmbTipoPago.SelectedValue);
            int idUsuario = SesionUsuario.IdUsuario; // Obtener ID del usuario actual

            // Validación básica
            if (string.IsNullOrEmpty(nombreCliente))
            {
                MessageBox.Show("El nombre del cliente es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idMesa <= 0)
            {
                MessageBox.Show("Seleccione una mesa válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear la orden con tipo de pago por defecto
            bool ordenCreada = OrdenesD.AgregarOrden(nombreCliente, idMesa, tipoPago, idUsuario);

            if (ordenCreada)
            {
                MessageBox.Show("Orden creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Cerrar el formulario
            }
            else
            {
                MessageBox.Show("Hubo un problema al crear la orden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void cmbMesas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
