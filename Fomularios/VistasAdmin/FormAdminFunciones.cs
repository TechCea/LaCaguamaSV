using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormAdminFunciones: Form
    {
        Conexion conexion = new Conexion();

        private decimal dineroContado;

        public FormAdminFunciones()
        {
            InitializeComponent();
            panelConfirmacion.Visible = false; // Ocultar la ventana de confirmación al inicio
            this.Controls.Add(panelIngresoMonto);
            panelIngresoMonto.Visible = false; // Lo ocultamos al inicio
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FormAdminFunciones_Load(object sender, EventArgs e)
        {

        }

        private void Corte_Caja_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = true; // Mostrar la ventana de confirmación
          
        }

        private  void btnConfirmarCorte_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = false;
            panelIngresoMonto.Visible = true;
            panelIngresoMonto.BringToFront();
            panelIngresoMonto.Location = new Point((this.ClientSize.Width - panelIngresoMonto.Width) / 2, (this.ClientSize.Height - panelIngresoMonto.Height) / 2);
        }

        private void btnCancelarCorte_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = false; // Ocultar la ventana de confirmación
        }

        private void btnConfirmarMonto_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMontoContado.Text, out decimal montoContado))
            {
                decimal cajaInicial = ObtenerCajaInicial(); // Ahora obtiene el valor dinámico de la BD
                decimal totalEfectivo = ObtenerTotalEfectivo(); // Obtiene ingresos solo en efectivo
                decimal totalGastos = ObtenerTotalGastos(); // Obtiene los gastos
                decimal totalEsperado = cajaInicial + totalEfectivo - totalGastos;
                decimal diferencia = montoContado - totalEsperado;

                string resultado = diferencia > 0 ? $"Sobrante: ${diferencia}" :
                                   diferencia < 0 ? $"Faltante: ${Math.Abs(diferencia)}" :
                                   "Sin diferencia.";

                // Mostrar mensaje de confirmación
                MessageBox.Show($"Corte de caja confirmado.\n" +
                                $"Monto contado: ${montoContado}\n" +
                                $"Caja inicial: ${cajaInicial}\n" +
                                $"Total generado: ${totalEfectivo}\n" +
                                $"Gastos: ${totalGastos}\n" +
                                $"Total esperado: ${totalEsperado}\n" +
                                $"{resultado}",
                                "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                panelIngresoMonto.Visible = false;
            }
            else
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private decimal ObtenerTotalEfectivo()
        {
            return conexion.ObtenerTotalEfectivo(DateTime.Now);
        }

        private decimal ObtenerTotalGastos()
        {
            return conexion.ObtenerTotalGastos(DateTime.Now);
        }
        private decimal ObtenerCajaInicial()
        {
            return conexion.ObtenerCajaInicial(DateTime.Now);
        }

        private void btnCancelarMonto_Click(object sender, EventArgs e)
        {
            panelIngresoMonto.Visible = false; // Ocultar la ventana de ingreso
        }
        private decimal ObtenerTotalGeneradoEfectivo()
        {
            return conexion.ObtenerTotalGeneradoEfectivo(DateTime.Now);
        }



        private void button9_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin();
            formAdmin.ShowDialog();
        }

        private void txtMontoContado_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
