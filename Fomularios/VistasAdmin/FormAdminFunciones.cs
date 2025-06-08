using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Diagnostics;


namespace LaCaguamaSV.Fomularios.VistasAdmin
{


    public partial class FormAdminFunciones : Form
    {
        Conexion conexion = new Conexion();
        private int idUsuario;  // Variable que guarda el ID del usuario
        private int idCaja;
        public string nombreUsuarioActual;
        private int usuarioId;
        // Constantes ESC/POS
        private const string ESC = "\x1B";
        private const string GS = "\x1D";
        private const string LF = "\x0A";
        private decimal montoContadoActual = 0;


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

            Panerl_corteX.BringToFront();
            panelCaja.BringToFront();
            panelResultadoCorte.BringToFront();
            panelConfirmacion.BringToFront();
            Panel_vistaX.BringToFront();
            panelIngresoMonto.BringToFront();
            panel_cortegeneral1.BringToFront();
            panel_corte_general.BringToFront();


            if (conexion.CorteGeneralYaRealizado())
            {
                btn_okgeneral.Enabled = false;
                label_general.Text = "⚠️ El corte general ya fue realizado este turno.";
            }


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

            Conexion conn = new Conexion();

            if (conn.CorteTarjetasYaRealizado())
            {
                Btn_confirmartarjeta.Enabled = false;
            }
            if (conexion.CorteGeneralYaRealizado())
            {
                btn_okgeneral.Enabled = false;
            }

        }

        private void Corte_Caja_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = true; // Mostrar la ventana de confirmación
            int idCajaActual = conexion.ObtenerIdCajaActualCorte(); // sin parámetro

            if (idCajaActual == -1)
            {
                MessageBox.Show("No se encontró una caja inicial activa.");
                panelConfirmacion.Visible = false;
                return;
            }

            // Verificar que el usuario actual sea el que abrió la caja
            int usuarioDeLaCaja = conexion.ObtenerUsuarioDeCaja(idCajaActual);
            if (usuarioDeLaCaja != this.idUsuario)
            {
                string nombreUsuarioCaja = conexion.ObtenerNombreUsuario(usuarioDeLaCaja);
                MessageBox.Show($"Solo el usuario que inició la caja puede hacer el corte.\nCajero responsable: {nombreUsuarioCaja}");
                panelConfirmacion.Visible = false;
                return;
            }

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

            if (!decimal.TryParse(txtMontoContado.Text, out decimal cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingresa una cantidad válida y mayor a cero.");
                return;
            }


            // Convertir el monto ingresado en el TextBox a decimal
            if (!decimal.TryParse(txtMontoContado.Text, out decimal montoContado))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            // Guardar el monto en la variable de clase
            montoContadoActual = montoContado;

            // Obtener el ID de la caja actual (esta caja debe haber sido inicializada previamente)
            int idCajaActual = conexion.ObtenerIdCajaActualCorte(); // sin parámetro

            if (idCajaActual == -1)
            {
                MessageBox.Show("No se encontró una caja inicial activa.");
                return;
            }

            // Verificar que el usuario actual sea el que abrió la caja
            int usuarioDeLaCaja = conexion.ObtenerUsuarioDeCaja(idCajaActual);
            if (usuarioDeLaCaja != this.idUsuario)
            {
                string nombreUsuarioCaja = conexion.ObtenerNombreUsuario(usuarioDeLaCaja);
                MessageBox.Show($"Solo el usuario que inició la caja puede hacer el corte.\nCajero responsable: {nombreUsuarioCaja}");
                return;
            }


            decimal montoCorte;
            if (!decimal.TryParse(txtMontoContado.Text, out montoCorte))
            {
                MessageBox.Show("Ingresa un número válido.");
                return;
            }

            if (montoCorte < 0)
            {
                MessageBox.Show("El monto del corte de caja no puede ser negativo.");
                return;
            }



            // Guardar el corte de caja (usando montoContado que es la variable local)
            conexion.GuardarCorteCaja(montoContado, this.idUsuario, idCajaActual);

            // Obtener los valores necesarios para mostrar en el corte
            decimal cajaInicial = conexion.ObtenerCajaInicial(idCajaActual);
            decimal totalGenerado = conexion.ObtenerTotalGenerado(idCajaActual);
            decimal totalGastos = conexion.ObtenerGastos(idCajaActual);
            string nombreCajero = conexion.ObtenerNombreUsuario(this.idUsuario);
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // Calcular la cantidad contada
            decimal cantidadContada = totalGenerado + cajaInicial;

            // Calcular el total esperado
            decimal totalEsperado = cajaInicial + totalGenerado - totalGastos;

            // Calcular la diferencia
            decimal diferencia = montoContado - totalEsperado;

            // Mostrar el resultado en los labels (usando montoContadoActual)
            labelResultado.Text = $"Fecha: {fechaActual}\n\n" +
                                  $"Cajero: {nombreCajero}\n\n" +

                                  $"Caja Inicial: {cajaInicial:C}\n" +
                                  $"Dinero Ingresado: {montoContadoActual:C}\n" +
                                  
                                  $"Dinero Contado : {cantidadContada:C}\n" +
                                  $"Efectivo Generado : {totalGenerado:C}\n" +
                                  $"Gastos del turno: {totalGastos:C}\n\n" +

                                  $"Total Esperado: {totalEsperado:C}\n" +
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

            // Habilitar botón de imprimir
            btnImprimirRecibo_Click.Enabled = true;

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
            Conexion conn = new Conexion();

            if (conn.CorteTarjetasYaRealizado())
            {
                MessageBox.Show("El corte de tarjetas ya fue realizado en este turno.");
                Panerl_corteX.Visible = false;
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel_cortegeneral1.Visible = true;
            panel_corte_general.Visible = false;

            if (conexion.CorteGeneralYaRealizado())
            {
                MessageBox.Show("El corte general ya fue realizado en este turno.");
                panel_cortegeneral1.Visible = false;

                return;
            }
        }


        private void btnImprimirRecibo_Click_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que hay un monto contado válido
                if (montoContadoActual <= 0)
                {
                    MessageBox.Show("No hay un monto contado válido para imprimir.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el ID de la caja actual
                int idCajaActual = conexion.ObtenerUltimoIdCajaInicializada(this.idUsuario);
                if (idCajaActual == -1)
                {
                    MessageBox.Show("No se encontró una caja activa.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Generar el contenido del comprobante de corte
                string contenidoComprobante = GenerarContenidoCorteComprobante(idCajaActual, montoContadoActual);

                // Método de impresión por USB
                ImprimirPorUSB(contenidoComprobante);

                MessageBox.Show("Comprobante de corte enviado a la impresora", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir corte: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarContenidoCorteComprobante(int idCaja, decimal montoContado)
        {
            StringBuilder sb = new StringBuilder();

            // Obtener los datos necesarios para el corte
            decimal cajaInicial = conexion.ObtenerCajaInicial(idCaja);
            decimal totalGenerado = conexion.ObtenerTotalGenerado(idCaja);
            decimal totalGastos = conexion.ObtenerGastos(idCaja);
            string nombreCajero = conexion.ObtenerNombreUsuario(this.idUsuario);

            // Calcular valores derivados
            decimal cantidadContada = totalGenerado + cajaInicial;
            decimal totalEsperado = cajaInicial + totalGenerado - totalGastos;
            decimal diferencia = montoContado - totalEsperado;

            // 1. Inicialización y encabezado
            sb.Append(ESC + "@"); // Reset printer
            sb.Append(ESC + "!" + "\x38"); // Fuente tamaño doble
            sb.Append(CenterText("LA CAGUAMA RESTAURANTE"));
            sb.Append(LF);
            sb.Append(CenterText("CORTE DE CAJA"));
            sb.Append(LF);
            sb.Append(CenterText("══════════════════════"));
            sb.Append(LF + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente normal

            // 2. Información básica del corte
            sb.Append($"FECHA: {DateTime.Now.ToString("g")}{LF}");
            sb.Append($"CAJERO: {nombreCajero}{LF}");
            sb.Append($"CAJA: #{idCaja}{LF}");
            sb.Append("──────────────────────────" + LF);

            // 3. Detalles del corte
            sb.Append(ESC + "!" + "\x08"); // Fuente enfatizada
            sb.Append("        DETALLE DEL CORTE" + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente
            sb.Append("──────────────────────────" + LF);

            sb.Append($"Dinero Ingresado: {montoContado.ToString("C")}{LF}");
            sb.Append($"Caja Inicial: {cajaInicial.ToString("C")}{LF}");
            sb.Append($"Cantidad Contada: {cantidadContada.ToString("C")}{LF}");
            sb.Append($"Efectivo Generado: {totalGenerado.ToString("C")}{LF}");
            sb.Append($"Gastos: {totalGastos.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF);
            sb.Append($"Total Esperado: {totalEsperado.ToString("C")}{LF}");
            sb.Append($"Diferencia: {diferencia.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF);

            // 4. Pie de página y cortes
            sb.Append(LF + LF);
            sb.Append(CenterText("¡Corte realizado con éxito!"));
            sb.Append(LF + LF);
            sb.Append(CenterText("Firma: ___________________"));
            sb.Append(LF + LF + LF + LF); // Espacios adicionales antes del corte
            sb.Append(GS + "V" + "\x41" + "\x00"); // Corte completo

            return sb.ToString();
        }

        // Métodos auxiliares (los mismos que en el otro formulario)
        private string CenterText(string text)
        {
            int maxWidth = 32; // Ajustar según tu impresora
            if (text.Length >= maxWidth) return text;

            int spaces = (maxWidth - text.Length) / 2;
            return new string(' ', spaces) + text;
        }

        private void ImprimirPorUSB(string contenido)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = ObtenerNombreImpresoraTermica();

            pd.PrintPage += (sender, e) =>
            {
                Font font = new Font("Courier New", 9);
                e.Graphics.DrawString(contenido, font, Brushes.Black,
                    new RectangleF(0, 0, pd.DefaultPageSettings.PrintableArea.Width,
                                  pd.DefaultPageSettings.PrintableArea.Height));
            };

            pd.Print();
        }

        private string ObtenerNombreImpresoraTermica()
        {
            // Busca la impresora por nombre
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.Contains("PT-210") || printer.Contains("GOOJPRT") || printer.Contains("POS"))
                {
                    return printer;
                }
            }

            // Si no la encuentra, usa la predeterminada
            return new PrinterSettings().PrinterName;
        }

        public class Impresion
        {
            private string textoImprimir;

            public void ImprimirTicket(string nombreCliente, decimal total, decimal descuento, decimal totalFinal)
            {

            }

            private void PrintPage(object sender, PrintPageEventArgs e)
            {
                Font fuente = new Font("Courier New", 10); // Letra monoespaciada
                float y = 10;
                float saltoLinea = fuente.GetHeight(e.Graphics) + 2;

                using (StringReader sr = new StringReader(textoImprimir))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        e.Graphics.DrawString(linea, fuente, Brushes.Black, 10, y);
                        y += saltoLinea;
                    }
                }
            }
        }





        private void btnCerrarPanel_Click_Click(object sender, EventArgs e)
        {
            panelResultadoCorte.Visible = false; // Ocultar el panel de resultados
        }


        private void btnCajaInicial_Click(object sender, EventArgs e)
        {
            // Validar que NO haya una caja activa hoy (sin corte)
            bool corteHecho = conexion.ExisteCorteParaUltimaCaja();

            if (!corteHecho)
            {
                MessageBox.Show("Ya hay una caja activa. Debe realizar el corte antes de iniciar una nueva.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            panelCaja.Visible = true;
        }

        private void btnConfirmarCaja_Click(object sender, EventArgs e)
        {
            decimal monto;




            if (!decimal.TryParse(txtMontoCaja.Text, out decimal cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingresa una cantidad válida y mayor a cero.");
                return;
            }

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

            if (conn.CorteTarjetasYaRealizado())
            {
                MessageBox.Show("El corte de tarjetas ya fue realizado en este turno.");
                
                return;
            }

            int idCajaActual = conexion.ObtenerUltimaCajaRegistrada();
            ActualizarLabelCaja();
            if (idCajaActual == -1)
            {
                MessageBox.Show("No hay caja activa.");
                return;
            }

            int idCaja = conn.ObtenerUltimoIdCaja();

            var (totalTarjetas, cantidadVentas) = conn.ObtenerCorteTarjetasPorHorario();
            string nombreCajero = conn.ObtenerNombreUsuario(this.idUsuario);

            StringBuilder resultado = new StringBuilder();
            resultado.AppendLine($"Cajero: {nombreCajero}");
            resultado.AppendLine($"Ventas con tarjeta: {cantidadVentas} venta(s)");
            resultado.AppendLine($"Total en tarjetas: {totalTarjetas:C}");
            
            Label_resultadoX.Text = resultado.ToString();

            conn.GuardarCorteTarjetas(totalTarjetas, this.idUsuario, idCaja);

            Panel_vistaX.Visible = true;
            Panerl_corteX.Visible = false;

            Btn_confirmartarjeta.Enabled = false; // lo deshabilitas una vez guardado
        }

        private void btnImprimir_corteX_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion conn = new Conexion();

                // Obtener el ID de la caja actual
                int idCaja = conn.ObtenerUltimoIdCaja();

                // Obtener los datos del corte de tarjetas
                var (totalTarjetas, cantidadVentas) = conn.ObtenerCorteTarjetasPorHorario();
                string nombreCajero = conn.ObtenerNombreUsuario(this.idUsuario);
                string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                // Generar el contenido del comprobante
                string contenidoComprobante = GenerarContenidoCorteTarjetas(
                    idCaja,
                    totalTarjetas,
                    cantidadVentas,
                    nombreCajero,
                    fechaActual
                );

                // Método de impresión por USB
                ImprimirPorUSB(contenidoComprobante);

                MessageBox.Show("Comprobante de corte de tarjetas enviado a la impresora", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir corte de tarjetas: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarContenidoCorteTarjetas(int idCaja, decimal totalTarjetas,
                                                   int cantidadVentas, string nombreCajero,
                                                   string fecha)
        {
            StringBuilder sb = new StringBuilder();

            // 1. Inicialización y encabezado
            sb.Append(ESC + "@"); // Reset printer
            sb.Append(ESC + "!" + "\x38"); // Fuente tamaño doble
            sb.Append(CenterText("LA CAGUAMA RESTAURANTE"));
            sb.Append(LF);
            sb.Append(CenterText("CORTE DE TARJETAS"));
            sb.Append(LF);
            sb.Append(CenterText("══════════════════════"));
            sb.Append(LF + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente normal

            // 2. Información básica del corte
            sb.Append($"FECHA: {fecha}{LF}");
            sb.Append($"CAJERO: {nombreCajero}{LF}");
            sb.Append($"CAJA: #{idCaja}{LF}");
            sb.Append("──────────────────────────" + LF);

            // 3. Detalles del corte
            sb.Append(ESC + "!" + "\x08"); // Fuente enfatizada
            sb.Append("        DETALLE DEL CORTE" + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente
            sb.Append("──────────────────────────" + LF);

            sb.Append($"Ventas con tarjeta: {cantidadVentas} venta(s){LF}");
            sb.Append($"Total en tarjetas: {totalTarjetas.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF + LF);

            // 4. Pie de página y cortes
            sb.Append(CenterText("¡Corte realizado con éxito!"));
            sb.Append(LF + LF);
            sb.Append(CenterText("Firma: ___________________"));
            sb.Append(LF + LF + LF + LF); // Espacios adicionales antes del corte
            sb.Append(GS + "V" + "\x41" + "\x00"); // Corte completo

            return sb.ToString();
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


            if (conexion.CorteGeneralYaRealizado())
            {
                MessageBox.Show("El corte general ya fue realizado en este turno.");
                
                return;
            }


            int idCajaActual = conexion.ObtenerUltimaCajaRegistrada();
            ActualizarLabelCaja();
            if (idCajaActual == -1)
            {
                MessageBox.Show("No hay caja activa.");
                return;
            }

            // Obtener datos por horario (10am - 6am)
            var (totalEfectivo, totalTarjeta, descuento, gastos) = conexion.ObtenerDatosCorteGeneral();

            // Calcular total final
            decimal totalFinal = totalEfectivo + totalTarjeta - gastos;

            // Calcular Venta efectivo final
            decimal totalFinalEfectivo = totalEfectivo  - gastos;
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
                 $"Fecha: {fechaActual}\n\n" +

                $"Ventas en efectivo del dia: {totalEfectivo:C}\n" +
                $"Ventas en tarjetas del dia: {totalTarjeta:C}\n" +
                $"Descuentos Realizado en el Dia: {descuento:C}\n" +
                $"Gastos del dia : {gastos:C}\n\n" +


                $"Efectivo Final Generado: {totalFinalEfectivo:C}\n" +
                $"Total de Ventas Generadas: {totalFinal:C}\n";

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
            int idCaja = conexion.ObtenerUltimaCaja();

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

        private void btnRespaldarBD_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo SQL (*.sql)|*.sql";
            sfd.FileName = $"respaldo_lacaguama_{DateTime.Now:yyyyMMdd_HHmmss}.sql";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string rutaRespaldo = sfd.FileName;
                    string usuario = "root";
                    string contraseña = "Caguama2025";
                    string baseDeDatos = "LaCaguamaBD";
                    string servidor = "localhost";
                    int puerto = 3306; // Especificamos el puerto 3307 aquí

                    string mysqldumpPath = @"C:\Program Files\MySQL\MySQL Server 9.3\bin\mysqldump.exe";

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = mysqldumpPath,
                        Arguments = $"--host={servidor} --port={puerto} -u {usuario} -p{contraseña} {baseDeDatos} --routines --triggers --single-transaction",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardError = true
                    };

                    using (Process process = new Process())
                    {
                        process.StartInfo = psi;
                        process.Start();

                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        process.WaitForExit();

                        if (!string.IsNullOrEmpty(error) && !error.Contains("[Warning] Using a password"))
                        {
                            throw new Exception($"Error en mysqldump: {error}");
                        }

                        File.WriteAllText(rutaRespaldo, output);
                        MessageBox.Show($"Respaldo creado exitosamente en:\n{rutaRespaldo}", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al crear respaldo:\n{ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImportarBD_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar si el usuario es administrador (rol = 1)
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.",
                              "Acceso Restringido",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            FormHistorialCortes formhistorialcortes = new FormHistorialCortes();
            formhistorialcortes.ShowDialog();
        }

        private void btnReimprimirCorte_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el último corte realizado por este usuario
                int idCajaActual = conexion.ObtenerUltimoIdCajaInicializada(this.idUsuario);
                if (idCajaActual == -1)
                {
                    MessageBox.Show("No se encontró una caja activa.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el último corte de caja para esta caja
                int idUltimoCorte = conexion.ObtenerUltimoCortePorCaja(idCajaActual);
                if (idUltimoCorte == -1)
                {
                    MessageBox.Show("No se encontró un corte realizado para esta caja.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener los datos del último corte
                var datosCorte = conexion.ObtenerDatosUltimoCorte(idUltimoCorte);
                if (datosCorte == null)
                {
                    MessageBox.Show("No se pudieron obtener los datos del último corte.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Generar el contenido del comprobante con los datos obtenidos
                string contenidoComprobante = GenerarContenidoCorteComprobante(
                    idCajaActual,
                    datosCorte.MontoContado,
                    datosCorte.CajaInicial,
                    datosCorte.TotalGenerado,
                    datosCorte.TotalGastos,
                    datosCorte.NombreCajero
                );

                // Método de impresión por USB
                ImprimirPorUSB(contenidoComprobante);

                MessageBox.Show("Comprobante de corte reimpreso correctamente", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reimprimir el corte: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarContenidoCorteComprobante(int idCaja, decimal montoContado, decimal cajaInicial,
                                              decimal totalGenerado, decimal totalGastos, string nombreCajero)
        {
            StringBuilder sb = new StringBuilder();

            // Calcular valores derivados
            decimal cantidadContada = totalGenerado + cajaInicial;
            decimal totalEsperado = cajaInicial + totalGenerado - totalGastos;
            decimal diferencia = montoContado - totalEsperado;

            // 1. Inicialización y encabezado
            sb.Append(ESC + "@"); // Reset printer
            sb.Append(ESC + "!" + "\x38"); // Fuente tamaño doble
            sb.Append(CenterText("LA CAGUAMA RESTAURANTE"));
            sb.Append(LF);
            sb.Append(CenterText("REIMPRESIÓN DE CORTE"));
            sb.Append(LF);
            sb.Append(CenterText("══════════════════════"));
            sb.Append(LF + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente normal

            // 2. Información básica del corte
            sb.Append($"FECHA: {DateTime.Now.ToString("g")}{LF}");
            sb.Append($"CAJERO: {nombreCajero}{LF}");
            sb.Append($"CAJA: #{idCaja}{LF}");
            sb.Append("──────────────────────────" + LF);
            // 3. Detalles del corte
            sb.Append(ESC + "!" + "\x08"); // Fuente enfatizada
            sb.Append("        DETALLE DEL CORTE" + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente
            sb.Append("──────────────────────────" + LF);

            sb.Append($"Dinero Ingresado: {montoContado.ToString("C")}{LF}");
            sb.Append($"Caja Inicial: {cajaInicial.ToString("C")}{LF}");
            sb.Append($"Cantidad Contada: {cantidadContada.ToString("C")}{LF}");
            sb.Append($"Efectivo Generado: {totalGenerado.ToString("C")}{LF}");
            sb.Append($"Gastos: {totalGastos.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF);
            sb.Append($"Total Esperado: {totalEsperado.ToString("C")}{LF}");
            sb.Append($"Diferencia: {diferencia.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF);

            // 4. Pie de página y cortes
            sb.Append(LF + LF);
            sb.Append(CenterText("¡Corte realizado con éxito!"));
            sb.Append(LF + LF);
            sb.Append(CenterText("Firma: ___________________"));
            sb.Append(LF + LF + LF + LF); // Espacios adicionales antes del corte
            sb.Append(GS + "V" + "\x41" + "\x00"); // Corte completo

            return sb.ToString();
        }

        private void btn_cortegeneral_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID de la caja actual
                int idCajaActual = conexion.ObtenerUltimoIdCajaInicializada(this.idUsuario);
                if (idCajaActual == -1)
                {
                    MessageBox.Show("No hay caja activa.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener los datos del corte general
                var (totalEfectivo, totalTarjeta, descuento, gastos) = conexion.ObtenerDatosCorteGeneral();
                string nombreCajero = conexion.ObtenerNombreUsuario(this.idUsuario);
                string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                // Calcular totales
                decimal totalFinal = totalEfectivo + totalTarjeta - gastos;
                decimal totalFinalEfectivo = totalEfectivo - gastos;

                // Generar el contenido del comprobante
                string contenidoComprobante = GenerarContenidoCorteGeneral(
                    idCajaActual,
                    totalEfectivo,
                    totalTarjeta,
                    descuento,
                    gastos,
                    totalFinalEfectivo,
                    totalFinal,
                    nombreCajero,
                    fechaActual
                );

                // Método de impresión por USB
                ImprimirPorUSB(contenidoComprobante);

                MessageBox.Show("Comprobante de corte general enviado a la impresora", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir corte general: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarContenidoCorteGeneral(int idCaja, decimal totalEfectivo, decimal totalTarjeta,
                                                  decimal descuento, decimal gastos, decimal totalFinalEfectivo,
                                                  decimal totalFinal, string nombreCajero, string fecha)
        {
            StringBuilder sb = new StringBuilder();

            // 1. Inicialización y encabezado
            sb.Append(ESC + "@"); // Reset printer
            sb.Append(ESC + "!" + "\x38"); // Fuente tamaño doble
            sb.Append(CenterText("LA CAGUAMA RESTAURANTE"));
            sb.Append(LF);
            sb.Append(CenterText("CORTE GENERAL DEL DÍA"));
            sb.Append(LF);
            sb.Append(CenterText("══════════════════════"));
            sb.Append(LF + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente normal

            // 2. Información básica del corte
            sb.Append($"FECHA: {fecha}{LF}");
            sb.Append($"CAJERO: {nombreCajero}{LF}");
            sb.Append($"CAJA: #{idCaja}{LF}");
            sb.Append("──────────────────────────" + LF);

            // 3. Detalles del corte
            sb.Append(ESC + "!" + "\x08"); // Fuente enfatizada
            sb.Append("        RESUMEN DEL DÍA" + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente
            sb.Append("──────────────────────────" + LF);

            sb.Append($"Ventas en efectivo: {totalEfectivo.ToString("C")}{LF}");
            sb.Append($"Ventas en tarjetas: {totalTarjeta.ToString("C")}{LF}");
            sb.Append($"Descuentos aplicados: {descuento.ToString("C")}{LF}");
            sb.Append($"Gastos del día: {gastos.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF);
            sb.Append($"Total efectivo final: {totalFinalEfectivo.ToString("C")}{LF}");
            sb.Append($"Total general: {totalFinal.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF + LF);

            // 4. Pie de página y cortes
            sb.Append(CenterText("¡Corte general realizado!"));
            sb.Append(LF + LF);
            sb.Append(CenterText("Firma: ___________________"));
            sb.Append(LF + LF + LF + LF); // Espacios adicionales antes del corte
            sb.Append(GS + "V" + "\x41" + "\x00"); // Corte completo

            return sb.ToString();
        }

        private void btnReimprimir_corte_general_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el último corte general realizado
                var ultimoCorte = conexion.ObtenerUltimoCorteGeneral();

                if (ultimoCorte == null)
                {
                    MessageBox.Show("No se encontró un corte general para reimprimir.", "Información",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Calcular totales
                decimal totalFinal = ultimoCorte.CantEfectivo + ultimoCorte.CantTarjeta - ultimoCorte.TotalGastosGeneral;
                decimal totalFinalEfectivo = ultimoCorte.CantEfectivo - ultimoCorte.TotalGastosGeneral;

                // Generar el contenido del comprobante
                string contenidoComprobante = GenerarContenidoReimpresionCorteGeneral(
                    ultimoCorte.IdCaja,
                    ultimoCorte.CantEfectivo,
                    ultimoCorte.CantTarjeta,
                    ultimoCorte.Descuento,
                    ultimoCorte.TotalGastosGeneral,
                    totalFinalEfectivo,
                    totalFinal,
                    ultimoCorte.NombreCajero,
                    ultimoCorte.Fecha.ToString("dd/MM/yyyy HH:mm")
                );

                // Método de impresión por USB
                ImprimirPorUSB(contenidoComprobante);

                MessageBox.Show("Comprobante de corte general reimpreso correctamente", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reimprimir corte general: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarContenidoReimpresionCorteGeneral(int idCaja, decimal totalEfectivo, decimal totalTarjeta,
                                                             decimal descuento, decimal gastos, decimal totalFinalEfectivo,
                                                             decimal totalFinal, string nombreCajero, string fecha)
        {
            StringBuilder sb = new StringBuilder();

            // 1. Inicialización y encabezado
            sb.Append(ESC + "@"); // Reset printer
            sb.Append(ESC + "!" + "\x38"); // Fuente tamaño doble
            sb.Append(CenterText("LA CAGUAMA RESTAURANTE"));
            sb.Append(LF);
            sb.Append(CenterText("REIMPRESIÓN DE CORTE GENERAL"));
            sb.Append(LF);
            sb.Append(CenterText("══════════════════════"));
            sb.Append(LF + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente normal

            // 2. Información básica del corte
            sb.Append($"FECHA ORIGINAL: {fecha}{LF}");
            sb.Append($"CAJERO: {nombreCajero}{LF}");
            sb.Append($"CAJA: #{idCaja}{LF}");
            sb.Append($"FECHA REIMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}{LF}");
            sb.Append("──────────────────────────" + LF);

            // 3. Detalles del corte
            sb.Append(ESC + "!" + "\x08"); // Fuente enfatizada
            sb.Append("        RESUMEN DEL DÍA" + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente
            sb.Append("──────────────────────────" + LF);

            sb.Append($"Ventas en efectivo: {totalEfectivo.ToString("C")}{LF}");
            sb.Append($"Ventas en tarjetas: {totalTarjeta.ToString("C")}{LF}");
            sb.Append($"Descuentos aplicados: {descuento.ToString("C")}{LF}");
            sb.Append($"Gastos del día: {gastos.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF);
            sb.Append($"Total efectivo final: {totalFinalEfectivo.ToString("C")}{LF}");
            sb.Append($"Total general: {totalFinal.ToString("C")}{LF}");
            sb.Append("──────────────────────────" + LF + LF);

            // 4. Pie de página y cortes
            sb.Append(CenterText("¡Reimpresión de corte general!"));
            sb.Append(LF + LF);
            sb.Append(CenterText("Firma: ___________________"));
            sb.Append(LF + LF + LF + LF); // Espacios adicionales antes del corte
            sb.Append(GS + "V" + "\x41" + "\x00"); // Corte completo

            return sb.ToString();
        }

        private void lbl_caja_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Vendasday_Click(object sender, EventArgs e)
        {
            try
            {
                Form reporteForm = new Form();
                reporteForm.Text = "Reporte de Ventas (10am - 6am) - Órdenes Pagadas";
                reporteForm.Size = new Size(800, 600);
                reporteForm.StartPosition = FormStartPosition.CenterScreen;

                DataGridView dgvReporte = new DataGridView();
                dgvReporte.Dock = DockStyle.Fill;
                dgvReporte.ReadOnly = true;
                dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvReporte.Columns.Add("Tipo", "Tipo");
                dgvReporte.Columns.Add("Nombre", "Nombre");
                dgvReporte.Columns.Add("Cantidad", "Cantidad Vendida");
                dgvReporte.Columns.Add("Total", "Total Generado");

                DateTime fechaActual = DateTime.Now;
                DateTime inicio = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 10, 0, 0);
                DateTime fin = inicio.AddHours(20);

                if (fechaActual.TimeOfDay < new TimeSpan(6, 0, 0))
                {
                    inicio = inicio.AddDays(-1);
                    fin = fin.AddDays(-1);
                }

                using (MySqlConnection conn = conexion.EstablecerConexion())
                {
                    // 1. Platos vendidos directamente o en promociones
                    string queryPlatos = @"
            SELECT 
                'Plato' AS Tipo,
                p.nombrePlato AS Nombre,
                SUM(pd.Cantidad) AS Cantidad,
                SUM(p.precioUnitario * pd.Cantidad) AS Total
            FROM pedidos pd
            JOIN platos p ON pd.id_plato = p.id_plato
            JOIN ordenes o ON pd.id_orden = o.id_orden
            WHERE o.fecha_orden >= @inicio 
              AND o.fecha_orden < @fin
              AND o.id_estadoO = 2
              AND pd.id_promocion IS NULL
            GROUP BY p.nombrePlato

            UNION ALL

            -- Platos vendidos como parte de promociones
            SELECT 
                'Plato (Promo)' AS Tipo,
                p.nombrePlato AS Nombre,
                SUM(pi.cantidad * pd.Cantidad) AS Cantidad,
                SUM(p.precioUnitario * pi.cantidad * pd.Cantidad) AS Total
            FROM pedidos pd
            JOIN promociones pr ON pd.id_promocion = pr.id_promocion
            JOIN promocion_items pi ON pr.id_promocion = pi.id_promocion
            JOIN platos p ON pi.id_item = p.id_plato AND pi.tipo_item = 'PLATO'
            JOIN ordenes o ON pd.id_orden = o.id_orden
            WHERE o.fecha_orden >= @inicio 
              AND o.fecha_orden < @fin
              AND o.id_estadoO = 2
            GROUP BY p.nombrePlato";

                    // 2. Bebidas vendidas directamente o en promociones
                    string queryBebidas = @"
            SELECT 
                'Bebida' AS Tipo,
                i.nombreProducto AS Nombre,
                SUM(pd.Cantidad) AS Cantidad,
                SUM(b.precioUnitario * pd.Cantidad) AS Total
            FROM pedidos pd
            JOIN bebidas b ON pd.id_bebida = b.id_bebida
            JOIN inventario i ON b.id_inventario = i.id_inventario
            JOIN ordenes o ON pd.id_orden = o.id_orden
            WHERE o.fecha_orden >= @inicio 
              AND o.fecha_orden < @fin
              AND o.id_estadoO = 2
              AND pd.id_promocion IS NULL
            GROUP BY i.nombreProducto

            UNION ALL

            -- Bebidas vendidas como parte de promociones
            SELECT 
                'Bebida (Promo)' AS Tipo,
                i.nombreProducto AS Nombre,
                SUM(pi.cantidad * pd.Cantidad) AS Cantidad,
                SUM(b.precioUnitario * pi.cantidad * pd.Cantidad) AS Total
            FROM pedidos pd
            JOIN promociones pr ON pd.id_promocion = pr.id_promocion
            JOIN promocion_items pi ON pr.id_promocion = pi.id_promocion
            JOIN bebidas b ON pi.id_item = b.id_bebida AND pi.tipo_item = 'BEBIDA'
            JOIN inventario i ON b.id_inventario = i.id_inventario
            JOIN ordenes o ON pd.id_orden = o.id_orden
            WHERE o.fecha_orden >= @inicio 
              AND o.fecha_orden < @fin
              AND o.id_estadoO = 2
            GROUP BY i.nombreProducto";

                    // 3. Extras vendidos directamente o en promociones
                    string queryExtras = @"
            SELECT 
                'Extra' AS Tipo,
                i.nombreProducto AS Nombre,
                SUM(pd.Cantidad) AS Cantidad,
                SUM(e.precioUnitario * pd.Cantidad) AS Total
            FROM pedidos pd
            JOIN extras e ON pd.id_extra = e.id_extra
            JOIN inventario i ON e.id_inventario = i.id_inventario
            JOIN ordenes o ON pd.id_orden = o.id_orden
            WHERE o.fecha_orden >= @inicio 
              AND o.fecha_orden < @fin
              AND o.id_estadoO = 2
              AND pd.id_promocion IS NULL
            GROUP BY i.nombreProducto

            UNION ALL

            -- Extras vendidos como parte de promociones
            SELECT 
                'Extra (Promo)' AS Tipo,
                i.nombreProducto AS Nombre,
                SUM(pi.cantidad * pd.Cantidad) AS Cantidad,
                SUM(e.precioUnitario * pi.cantidad * pd.Cantidad) AS Total
            FROM pedidos pd
            JOIN promociones pr ON pd.id_promocion = pr.id_promocion
            JOIN promocion_items pi ON pr.id_promocion = pi.id_promocion
            JOIN extras e ON pi.id_item = e.id_extra AND pi.tipo_item = 'EXTRA'
            JOIN inventario i ON e.id_inventario = i.id_inventario
            JOIN ordenes o ON pd.id_orden = o.id_orden
            WHERE o.fecha_orden >= @inicio 
              AND o.fecha_orden < @fin
              AND o.id_estadoO = 2
            GROUP BY i.nombreProducto";

                    // Ejecutar las consultas y llenar el DataGridView
                    EjecutarConsultaYAgregarFilas(queryPlatos, conn, dgvReporte, inicio, fin);
                    EjecutarConsultaYAgregarFilas(queryBebidas, conn, dgvReporte, inicio, fin);
                    EjecutarConsultaYAgregarFilas(queryExtras, conn, dgvReporte, inicio, fin);
                }

                // Agregar totales
                decimal totalGeneral = 0;
                foreach (DataGridViewRow row in dgvReporte.Rows)
                {
                    if (row.Cells["Total"].Value != null)
                    {
                        decimal valor = decimal.Parse(row.Cells["Total"].Value.ToString().Replace("$", ""));
                        totalGeneral += valor;
                    }
                }

                dgvReporte.Rows.Add("", "TOTAL GENERAL", "", $"${totalGeneral:N2}");

                Label lblTitulo = new Label();
                lblTitulo.Text = $"Ventas desde {inicio.ToString("g")} hasta {fin.ToString("g")} (Solo órdenes pagadas)";
                lblTitulo.Dock = DockStyle.Top;
                lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
                lblTitulo.Font = new Font(lblTitulo.Font, FontStyle.Bold);

                reporteForm.Controls.Add(dgvReporte);
                reporteForm.Controls.Add(lblTitulo);
                reporteForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EjecutarConsultaYAgregarFilas(string query, MySqlConnection conn, DataGridView dgv, DateTime inicio, DateTime fin)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fin", fin);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgv.Rows.Add(
                            reader["Tipo"],
                            reader["Nombre"],
                            reader["Cantidad"],
                            $"${Convert.ToDecimal(reader["Total"]):N2}"
                        );
                    }
                }
            }
        }

        private void btnImportarBD_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Archivo SQL (*.sql)|*.sql";
            dialog.Title = "Seleccionar archivo de respaldo";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string rutaArchivo = dialog.FileName;
                    string usuario = "root";
                    string contraseña = "Caguama2025";
                    string baseDeDatos = "LaCaguamaBD";
                    string servidor = "localhost";
                    int puerto = 3306; // Especificamos el puerto 3307 aquí
                    string mysqlPath = @"C:\Program Files\MySQL\MySQL Server 9.3\bin\mysql.exe";

                    string sqlContent = File.ReadAllText(rutaArchivo);

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = mysqlPath,
                        Arguments = $"--host={servidor} --port={puerto} -u {usuario} -p{contraseña} {baseDeDatos}",
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardError = true
                    };

                    using (Process process = new Process())
                    {
                        process.StartInfo = psi;
                        process.Start();

                        using (StreamWriter sw = process.StandardInput)
                        {
                            if (sw.BaseStream.CanWrite)
                            {
                                sw.WriteLine("SET FOREIGN_KEY_CHECKS=0;");
                                sw.Write(sqlContent);
                                sw.WriteLine("SET FOREIGN_KEY_CHECKS=1;");
                            }
                        }

                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit();

                        if (!string.IsNullOrEmpty(error) && !error.Contains("[Warning] Using a password"))
                        {
                            throw new Exception($"Error en mysql: {error}");
                        }

                        MessageBox.Show("Base de datos importada correctamente.", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al importar la base de datos:\n{ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

