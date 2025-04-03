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

        public FormAdminFunciones(int usuarioId)
        {
            InitializeComponent();
            panelConfirmacion.Visible = false; // Ocultar la ventana de confirmación al inicio
            this.Controls.Add(panelIngresoMonto);
            panelIngresoMonto.Visible = false; // Lo ocultamos al inicio
            panelResultadoCorte.Visible = false; // Ocultamos el panel de resultado

            VerificarEstadoCaja();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FormAdminFunciones_Load(object sender, EventArgs e)
        {
            VerificarEstadoCaja();
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

                // Guardar en la tabla corte_de_caja
                int idEstadoCorte = 1; // Asegúrate de que este ID existe en la tabla estado_corte
                if (conexion.RegistrarCorteDeCaja(montoContado, SesionUsuario.IdUsuario, idEstadoCorte))
                {
                    MessageBox.Show("Corte de caja registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Deshabilitar botón de corte y habilitar el de caja inicial
                    btnCajaInicial.Enabled = true;
                    Corte_Caja.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Error al registrar el corte de caja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Mostrar el resumen del corte
                string mensaje = $"Corte de caja confirmado.\n" +
                                 $"Monto contado: ${montoContado}\n" +
                                 $"Caja inicial: ${cajaInicial}\n" +
                                 $"Total generado: ${totalGenerado}\n" +
                                 $"Gastos: ${totalGastos}\n" +
                                 $"Total esperado: ${totalEsperado}\n";

                labelResultado.Text = mensaje;
                panelResultadoCorte.Visible = true;
                panelIngresoMonto.Visible = false;
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
                                               // Limpiar el TextBox
            txtMontoContado.Clear();
        }
        private decimal ObtenerTotalGeneradoEfectivo()
        {
            return conexion.ObtenerTotalGeneradoEfectivo(DateTime.Now);
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCajaInicial_Click(object sender, EventArgs e)
        {
            panelCaja.Visible = true;
        }

        private void btnConfirmarCaja_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMontoCaja.Text, out decimal cajaInicial) && cajaInicial > 0)
            {
                if (!conexion.CajaInicialYaEstablecida())
                {
                    // Verificar que el ID del usuario sea válido
                    if (SesionUsuario.IdUsuario <= 0)
                    {
                        MessageBox.Show("Error: ID de usuario no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (conexion.RegistrarCajaInicial(cajaInicial, SesionUsuario.IdUsuario))
                    {
                        MessageBox.Show("Caja inicial establecida correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCajaInicial.Enabled = false;
                        Corte_Caja.Enabled = true;
                        panelCaja.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Error al establecer la caja inicial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("La caja inicial ya fue establecida hoy.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ingrese un monto válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelarCaja_Click(object sender, EventArgs e)
        {
            panelCaja.Visible = false;
        }

        private void txtMontoCaja_TextChanged(object sender, EventArgs e)
        {

        }
        private void VerificarEstadoCaja()
        {
            if (conexion.CajaInicialYaEstablecida())
            {
                btnCajaInicial.Enabled = false;
                Corte_Caja.Enabled = true;
            }
            else
            {
                btnCajaInicial.Enabled = true;
                Corte_Caja.Enabled = false;
            }

            // Si ya se hizo un corte hoy, deshabilitar el botón de corte de caja
            if (conexion.CorteDeCajaRealizadoHoy())
            {
                btnCajaInicial.Enabled = true;  // Se permite iniciar nueva caja
                Corte_Caja.Enabled = false;     // No se puede hacer otro corte en el mismo día
            }
        }

        private void panelCaja_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
