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
    public partial class FormGestionOrdenes: Form
    {
        public FormGestionOrdenes(int idOrden, string nombreCliente, decimal total, decimal descuento, string fechaOrden, string numeroMesa, string tipoPago, string nombreUsuario, string estadoOrden)
        {
            InitializeComponent();
            CargarDatosOrden(idOrden, nombreCliente, total, descuento, fechaOrden, numeroMesa, tipoPago, nombreUsuario, estadoOrden);
        }

        private void CargarDatosOrden(int idOrden, string nombreCliente, decimal total, decimal descuento, string fechaOrden, string numeroMesa, string tipoPago, string nombreUsuario, string estadoOrden)
        {
            lblIdOrden.Text = idOrden.ToString();
            lblNombreCliente.Text = nombreCliente;
            lblTotal.Text = total.ToString("C");
            lblDescuento.Text = descuento.ToString("C");
            lblFechaOrden.Text = fechaOrden;
            lblNumeroMesa.Text = numeroMesa;
            lblTipoPago.Text = tipoPago;
            lblNombreUsuario.Text = nombreUsuario;
            lblEstadoOrden.Text = estadoOrden;
        }

        private void FormGestionOrdenes_Load(object sender, EventArgs e)
        {

        }

    }
}
