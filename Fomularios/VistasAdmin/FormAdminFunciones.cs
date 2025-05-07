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
        private int usuarioId;


        public FormAdminFunciones(int usuarioId)
        {
            InitializeComponent();
            panelConfirmacion.Visible = false;
            panelIngresoMonto.Visible = false;
            panelResultadoCorte.Visible = false;
            decimal montoContado = 0;
            idUsuario = usuarioId;  // Guardamos el ID del usuario en la variable
            this.idUsuario = usuarioId;

            // Tamaño fijo
            CentrarPanel(Panerl_corteX); // Reemplaza 'miPanel' con el nombre real de tu panel

        }
        private void CentrarPanel(Panel panel)
        {
            int x = (this.ClientSize.Width - panel.Width) / 2;
            int y = (this.ClientSize.Height - panel.Height) / 2;
            panel.Location = new Point(x, y);
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
            // Convertir el monto ingresado en el TextBox a decimal
            decimal montoContado;
            if (!decimal.TryParse(txtMontoContado.Text, out montoContado))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            // Obtener el ID de la caja actual (esta caja debe haber sido inicializada previamente)
            int idCajaActual = conexion.ObtenerUltimoIdCajaInicializada(this.idUsuario); // Este método ya lo tienes
            if (idCajaActual == -1)
            {
                MessageBox.Show("No se encontró una caja inicial activa.");
                return;
            }

            // ✅ NUEVA VALIDACIÓN: verificar si ya se hizo un corte para esta caja
            if (conexion.CorteYaRealizado(idCajaActual))
            {
                MessageBox.Show("Ya se ha realizado un corte para esta caja. Debe iniciar una nueva caja antes de realizar otro corte.");
                return;
            }

            // Guardar el corte de caja
            conexion.GuardarCorteCaja(montoContado, this.idUsuario, idCajaActual);

            // Obtener los valores necesarios para mostrar en el corte
            decimal cajaInicial = conexion.ObtenerCajaInicial(idCajaActual);
            decimal totalGenerado = conexion.ObtenerTotalGenerado(idCajaActual);
            decimal totalGastos = conexion.ObtenerGastos(idCajaActual);

            // Calcular la cantidad contada
            decimal cantidadContada = totalGenerado + cajaInicial;

            // Calcular el total esperado
            decimal totalEsperado = cajaInicial + totalGenerado - totalGastos;

            // Calcular la diferencia
            decimal diferencia = montoContado - totalEsperado;

            // Mostrar el resultado en los labels
            labelResultado.Text =
                                  $"Dinero Ingresado: {montoContado:C}\n" +
                                  $"Caja Inicial: {cajaInicial:C}\n" +
                                  $"Cantidad Contada : {cantidadContada:C}\n" +
                                  $"Total Generado : {totalGenerado:C}\n" +
                                  $"Gastos: {totalGastos:C}\n" +
                                  $"Total Esperado : {totalEsperado:C}\n" +
                                  $"Diferencia: {diferencia:C}";

            // Cambiar la visibilidad de los paneles
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
            // Validar que el usuario haya hecho corte de su última caja
            bool corteHecho = conexion.ExisteCorteParaUltimaCaja(this.idUsuario);

            if (!corteHecho)
            {
                MessageBox.Show("Debe realizar el corte de caja antes de iniciar una nueva caja.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            panelCaja.Visible = true; // Mostrar panel solo si se permite iniciar caja
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

            int idUsuario = this.idUsuario;
            int idCaja = this.idCaja;

            // Obtener el total de ventas con tarjeta y la cantidad de ventas
            var (totalTarjetas, cantidadVentas) = conn.ObtenerTotalTarjetas(idCaja);
            string nombreCajero = conn.ObtenerNombreUsuario(idUsuario);

            // Crear el mensaje con los resultados
            StringBuilder resultado = new StringBuilder();
            resultado.AppendLine($"Cajero: {nombreCajero}");
            resultado.AppendLine($"Ventas con tarjeta: {cantidadVentas} venta(s)");
            resultado.AppendLine($"Total en tarjetas: {totalTarjetas:C}");

            // Mostrar el resultado en el label
            Label_resultadoX.Text = resultado.ToString();

            // Mostrar el panel de resultados
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
