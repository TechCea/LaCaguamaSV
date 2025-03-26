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
            panelResultadoCorte.Visible = false; // Ocultamos el panel de resultado
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
                DateTime fechaActual = DateTime.Now;
                decimal cajaInicial = conexion.ObtenerCajaInicial(fechaActual);
                decimal totalEfectivo = ObtenerTotalEfectivo(fechaActual);
                decimal totalGastos = ObtenerTotalGastos(fechaActual);
                decimal totalGenerado = conexion.ObtenerTotalGenerado(fechaActual);
                decimal totalEsperado = cajaInicial + totalGenerado - totalGastos;

                // Aquí actualizamos el texto del label
                string mensaje = $"Corte de caja confirmado.\n" +
                                 $"Monto contado: ${montoContado}\n" +
                                 $"Caja inicial: ${cajaInicial}\n" +
                                 $"Total generado: ${totalGenerado}\n" +
                                 $"Gastos: ${totalGastos}\n" +
                                 $"Total esperado: ${totalEsperado}\n";

                labelResultado.Text = mensaje; // Actualizamos el texto del Label
                panelResultadoCorte.Visible = true; // Mostrar el panel de resultados

                // Ocultamos el panel de ingreso de monto
                panelIngresoMonto.Visible = false; // Ocultamos panelIngresoMonto
            }
            else
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal ObtenerTotalEfectivo(DateTime fecha)
        {
            return conexion.ObtenerTotalEfectivo(fecha);
        }

        private decimal ObtenerTotalGastos(DateTime fecha)
        {
            return conexion.ObtenerTotalGastos(fecha);
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirRecibo_Click_Click(object sender, EventArgs e)
        {
            // Lógica de impresión del recibo (ejemplo)
            MessageBox.Show("Imprimiendo el recibo...", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCerrarPanel_Click_Click(object sender, EventArgs e)
        {
            panelResultadoCorte.Visible = false; // Ocultar el panel de resultados
        }

        private void panelResultadoCorte_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelResultado_Click(object sender, EventArgs e)
        {
           
        }
    }
}
