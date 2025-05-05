using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormAdminFunciones : Form
    {
        Conexion conexion = new Conexion();
        private int idUsuario;  // Variable que guarda el ID del usuario
        private int idCaja;
        public string nombreUsuarioActual;


        public FormAdminFunciones(int usuarioId)
        {
            InitializeComponent();
            panelConfirmacion.Visible = false;
            panelIngresoMonto.Visible = false;
            panelResultadoCorte.Visible = false;
            decimal montoContado = 0;
            idUsuario = usuarioId;  // Guardamos el ID del usuario en la variable


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FormAdminFunciones_Load(object sender, EventArgs e)
        {
            panelCaja.Visible = false;


        }





        private void Corte_Caja_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = true; // Mostrar la ventana de confirmación

        }

        private void btnConfirmarCorte_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = false;
            panelIngresoMonto.Visible = true;
        }


        private void btnCancelarCorte_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = false; // Ocultar la ventana de confirmación
        }

        private void btnConfirmarMonto_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMontoContado.Text, out decimal montoContado))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            Conexion conn = new Conexion();

            // Obtener valores necesarios para el corte
            decimal cajaInicial = conn.ObtenerCajaInicialDelDia(); // Método que te pasé
            decimal totalGenerado = conn.ObtenerVentasEfectivoDelDia();
            decimal gastos = conn.ObtenerGastosDelDiaCorteCaja(); // Renombrado para evitar conflicto
            decimal totalEsperado = cajaInicial + totalGenerado - gastos;
            decimal totalcajagenerada = cajaInicial + totalGenerado;

            // Guardar el corte
            conn.InsertarCorteCaja(montoContado, idUsuario); // Ya tienes el idUsuario en este form

            // Mostrar resultados
            StringBuilder resultado = new StringBuilder();
            resultado.AppendLine($"Monto contado: ${montoContado:F2}");
            resultado.AppendLine($"Caja inicial: ${cajaInicial:F2}");
            resultado.AppendLine($"Total generado (ventas): ${totalGenerado:F2}");
            resultado.AppendLine($"Gastos: ${gastos:F2}");
            resultado.AppendLine($"Venta del turno: ${totalcajagenerada:F2}");
            resultado.AppendLine($"Total esperado: ${totalEsperado:F2}");
            resultado.AppendLine($"Diferencia: ${(montoContado - totalEsperado):F2}");

            labelResultado.Text = resultado.ToString();

            // Mostrar panel de resultado
            panelIngresoMonto.Visible = false;
            panelResultadoCorte.Visible = true;
        }



        private void btnCancelarMonto_Click(object sender, EventArgs e)
        {
            panelResultadoCorte.Visible = false; // Ocultar el panel de resultados
        }




        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMontoContado_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Panerl_corteX.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirRecibo_Click_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Recibo enviado a impresora (simulado).");
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCajaInicial_Click(object sender, EventArgs e)
        {
            panelCaja.Visible = true;
        }

        private void btnConfirmarCaja_Click(object sender, EventArgs e)
        {
            decimal monto;

            // Intentamos convertir el monto ingresado
            if (decimal.TryParse(txtMontoCaja.Text, out monto))
            {
                Conexion conexion = new Conexion();

                // Insertamos la caja inicial con el monto y el ID del usuario
                conexion.InsertarCajaInicial(monto, idUsuario);

                MessageBox.Show("Caja inicial registrada correctamente.");
                panelCaja.Visible = false;  // Ocultamos el panel de caja
            }
            else
            {
                MessageBox.Show("Monto inválido.");
            }
        }

        private void btnCancelarCaja_Click(object sender, EventArgs e)
        {
            panelCaja.Visible = false;
            txtMontoCaja.Text = string.Empty;
        }

        private void txtMontoCaja_TextChanged(object sender, EventArgs e)
        {

        }



        private void panelCaja_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            FormAdminGastos formGastos = new FormAdminGastos();
            formGastos.ShowDialog();
        }

        private void Panerl_corteX_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void btn_cancelartarjeta_Click(object sender, EventArgs e)
        {
            Panerl_corteX.Visible = false;
        }

        private void Btn_confirmartarjeta_Click(object sender, EventArgs e)
        {
            Conexion conn = new Conexion();

            // Usa tus variables existentes
            int idUsuario = this.idUsuario;
            int idCaja = this.idCaja;

            decimal totalTarjetas = conn.ObtenerTotalTarjetas(idCaja);
            string nombreCajero = conn.ObtenerNombreUsuario(idUsuario);

            StringBuilder resultado = new StringBuilder();
            resultado.AppendLine($"Cajero: {nombreCajero}");
            resultado.AppendLine($"Total en tarjetas: ${totalTarjetas:F2}");

            Label_resultadoX.Text = resultado.ToString();

            // Mostrar el panel con los resultados
            Panel_vistaX.Visible = true;
            Panerl_corteX.Visible = false;
        }

        private void btnImprimir_corteX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Simulando impresión del corte de tarjeta...", "Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_cerrarXZ_Click(object sender, EventArgs e)
        {

        }

        private void panel_corte_general_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel_vistaX_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btncancelar_x_Click(object sender, EventArgs e)
        {
            Panel_vistaX.Visible = false;
        }




     
    }
}
