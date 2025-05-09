using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
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
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tamaño fijo
            CentrarPanel(Panerl_corteX); // Reemplaza 'miPanel' con el nombre real de tu panel
            CentrarPanel(panelCaja);
            CentrarPanel(panelConfirmacion);
            CentrarPanel(panelResultadoCorte);  
            CentrarPanel(Panel_vistaX);  
            CentrarPanel(panelIngresoMonto);  
            CentrarPanel(panel_cortegeneral1);  
            CentrarPanel(panel_corte_general);  


        }
        private void CentrarPanel(Panel panel)
        {
            int x = (this.ClientSize.Width - panel.Width) / 2;
            int y = (this.ClientSize.Height - panel.Height) / 2;
            panel.Location = new Point(x, y);
        }

        private void FormAdminFunciones_Load(object sender, EventArgs e)
        {
         
            panelCaja.Visible = false;
            ActualizarLabelCaja(); // Llama aquí para que se actualice al cargar

        }

        private void Corte_Caja_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = true; // Mostrar la ventana de confirmación

        }

        private void btnConfirmarCorte_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = false;
            panelIngresoMonto.Visible = true;
            ActualizarLabelCaja();
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
            btnCajaInicial.Enabled = true;
            btnCajaInicial.BackColor = Color.LightGreen;

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
                                  $"Efectivo Generado : {totalGenerado:C}\n" +
                                  $"Gastos: {totalGastos:C}\n" +
                                  $"Total Esperado : {totalEsperado:C}\n" +
                                  $"Diferencia: {diferencia:C}";




            // Cambiar estados en la base de datos
            conexion.ActualizarEstadoCaja(idCajaActual, 1); // caja a NO inicializada
            int idUltimoCorte = conexion.ObtenerUltimoCortePorCaja(idCajaActual);
            conexion.ActualizarEstadoCorte(idUltimoCorte, 1); // corte a NO inicializado



            // Cambiar la visibilidad de los paneles
            txtMontoContado.Text = ""; // limpiar textbox después de guardar corte
            panelIngresoMonto.Visible = false;
            panelResultadoCorte.Visible = true;


            // Apagar botón de corte
            Corte_Caja.Enabled = false;
            Corte_Caja.BackColor = Color.Gray;

            // Encender botón de inicio de caja
            btnCajaInicial.Enabled = true;
            btnCajaInicial.BackColor = ColorTranslator.FromHtml("#e74719");

            ActualizarLabelCaja();

        }

        private void btnCancelarMonto_Click(object sender, EventArgs e)
        {
            panelIngresoMonto.Visible = false; // Ocultar el panel de resultados
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Panerl_corteX.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel_cortegeneral1.Visible = true;
            panel_corte_general.Visible = false;
        }

        private void btnImprimirRecibo_Click_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Recibo enviado a impresora (simulado).");
        }

        private void btnCerrarPanel_Click_Click(object sender, EventArgs e)
        {
            panelResultadoCorte.Visible = false; // Ocultar el panel de resultados
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
                txtMontoCaja.Text = ""; // o txtCajaInicial.Clear();
                panelCaja.Visible = false;  // Ocultamos el panel de caja

                // Apagar botón de inicio
                btnCajaInicial.Enabled = false;
                btnCajaInicial.BackColor = Color.Gray;

                // Encender botón de corte
                Corte_Caja.Enabled = true;
                Corte_Caja.BackColor = ColorTranslator.FromHtml("#e74719");
                ActualizarLabelCaja();
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

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            FormAdminGastos formGastos = new FormAdminGastos();
            formGastos.ShowDialog();
        }

        private void btn_cancelartarjeta_Click(object sender, EventArgs e)
        {
            Panerl_corteX.Visible = false;
        }

        private void Btn_confirmartarjeta_Click(object sender, EventArgs e)
        {
            Conexion conn = new Conexion();

            // Obtener el id de la última caja cerrada
            int idCaja = conn.ObtenerUltimoIdCaja();

            if (idCaja <= 0)
            {
                MessageBox.Show("No se ha encontrado una caja válida.");
                return;
            }

            // Obtener el total de ventas con tarjeta entre las 10 AM y las 3 AM
            var (totalTarjetas, cantidadVentas) = conn.ObtenerCorteTarjetasPorHorario();


            // Obtener el nombre del usuario que hizo el corte
            string nombreCajero = conn.ObtenerNombreUsuario(this.idUsuario);

            // Crear el mensaje con los resultados
            StringBuilder resultado = new StringBuilder();
            resultado.AppendLine($"Cajero: {nombreCajero}");
            resultado.AppendLine($"Ventas con tarjeta: {cantidadVentas} venta(s)");
            resultado.AppendLine($"Total en tarjetas: {totalTarjetas:C}");

            // Mostrar el resultado en el label
            Label_resultadoX.Text = resultado.ToString();

            // Guardar el corte de tarjetas en la base de datos
            conexion.GuardarCorteTarjetas( 
                totalTarjetas, 
                this.idUsuario, 
                idCaja);

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
            panel_corte_general.Visible = false;
        }

        private void btncancelar_x_Click(object sender, EventArgs e)
        {
            Panel_vistaX.Visible = false;
        }

        private void btn_okgeneral_Click(object sender, EventArgs e)
        {
            int idCajaActual = conexion.ObtenerUltimoIdCajaInicializada(this.idUsuario);
            ActualizarLabelCaja();
            if (idCajaActual == -1)
            {
                MessageBox.Show("No hay caja activa.");
                return;
            }

            // Obtener datos por horario (10am - 3am)
            var (totalEfectivo, totalTarjeta, descuento, gastos) = conexion.ObtenerDatosCorteGeneral();

            // Calcular total final
            decimal totalFinal = totalEfectivo + totalTarjeta - gastos;

            // Guardar en la base de datos
            conexion.GuardarCorteGeneral(
                0, // cajaInicial se ignora, puedes pasar 0 o null si actualizas el método
                totalFinal,
                totalEfectivo,
                totalTarjeta,
                descuento,
                gastos,
                this.idUsuario,
                idCajaActual
            );

            // Mostrar datos en label
            string nombreCajero = conexion.ObtenerNombreUsuario(this.idUsuario);
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            label_general.Text =
                "CORTE GENERAL\n\n" +

                 $"Cajero: {nombreCajero}\n"+
                 $"Fecha: {fechaActual}\n" +

                $"Ventas en efectivo: {totalEfectivo:C}\n" +
                $"Ventas en tarjetas: {totalTarjeta:C}\n" +
                $"Descuentos: {descuento:C}\n" +
                $"Gastos: {gastos:C}\n\n" +
                $"Total generado: {totalFinal:C}\n";

            // Cambiar visibilidad de paneles
            panel_cortegeneral1.Visible = false;
            panel_corte_general.Visible = true;
        }

        private void btn_cancelargeneral_Click(object sender, EventArgs e)
        {
            panel_cortegeneral1.Visible = false;
        }

        private void ActualizarLabelCaja()
        {
            int idCaja = conexion.ObtenerCajaActiva(this.idUsuario);

            if (idCaja > 0)
            {
                decimal monto = conexion.ObtenerMontoCaja(idCaja);
                lbl_caja.Text = $"Caja activa ID: {idCaja}, Monto: {monto:C}";
            }
            else
            {
                lbl_caja.Text = "No hay caja activa. Inicializar caja.";
            }
        }



    }
}
