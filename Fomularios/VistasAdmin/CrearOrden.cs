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
    public partial class CrearOrden: Form
    {
        public CrearOrden()
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


        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMesas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
