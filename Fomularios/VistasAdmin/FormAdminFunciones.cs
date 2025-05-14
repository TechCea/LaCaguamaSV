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
            if (!decimal.TryParse(txtMontoContado.Text, out decimal montoContado))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            // Guardar el monto en la variable de clase
            montoContadoActual = montoContado;

            // Obtener el ID de la caja actual (esta caja debe haber sido inicializada previamente)
            int idCajaActual = conexion.ObtenerUltimoIdCajaInicializada(this.idUsuario);
            if (idCajaActual == -1)
            {
                MessageBox.Show("No se encontró una caja inicial activa.");
                return;
            }

            btnCajaInicial.Enabled = true;
            btnCajaInicial.BackColor = ColorTranslator.FromHtml("#e74719");

            if (conexion.CorteYaRealizado(idCajaActual))
            {
                MessageBox.Show("Ya se ha realizado un corte para esta caja. Debe iniciar una nueva caja antes de realizar otro corte.");
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

                                  $"Dinero Ingresado: {montoContadoActual:C}\n" +
                                  $"Caja Inicial: {cajaInicial:C}\n" +
                                  $"Cantidad Contada : {cantidadContada:C}\n" +
                                  $"Efectivo Generado : {totalGenerado:C}\n" +
                                  $"Gastos: {totalGastos:C}\n\n" +

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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel_cortegeneral1.Visible = true;
            panel_corte_general.Visible = false;
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


            if (conexion.CorteGeneralYaRealizado())
            {
                MessageBox.Show("El corte general ya fue realizado en este turno.");
                
                return;
            }
       

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


                $"Total Efectivo Final: {totalFinalEfectivo:C}\n" +
                $"Total de Venta Generada: {totalFinal:C}\n";

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

        private void btnRespaldarBD_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivo SQL (*.sql)|*.sql";
            sfd.FileName = "respaldo_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".sql";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string rutaRespaldo = sfd.FileName;
                string usuario = "root";
                string contraseña = "slenderman"; // Reemplaza por tu contraseña real
                string baseDeDatos = "lacaguamabd"; // Reemplaza por el nombre real de tu base de datos
                string mysqldumpPath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin";

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = mysqldumpPath;
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                psi.Arguments = $"-u {usuario} -p{contraseña} {baseDeDatos}";

                try
                {
                    using (Process process = Process.Start(psi))
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            File.WriteAllText(rutaRespaldo, result);
                        }

                        process.WaitForExit();
                        MessageBox.Show("Respaldo creado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al respaldar la base de datos:\n" + ex.Message);
                }
            }
        }

        private void btnImportarBD_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Archivo SQL (*.sql)|*.sql";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = dialog.FileName;
                    Conexion conexion = new Conexion();
                    conexion.ImportarBaseDeDatos(rutaArchivo);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
    }
}

