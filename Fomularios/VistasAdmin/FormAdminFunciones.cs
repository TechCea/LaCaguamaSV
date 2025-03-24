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
        private string connectionString = "server=root; database=lacaguamabd; uid=root; pwd=slenderman;";

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

            // Centrar el panel en el formulario
            panelIngresoMonto.Location = new Point(
                (this.ClientSize.Width - panelIngresoMonto.Width) / 2,
                (this.ClientSize.Height - panelIngresoMonto.Height) / 2
            );
        }

        private void btnCancelarCorte_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = false; // Ocultar la ventana de confirmación
        }

        private void btnConfirmarMonto_Click(object sender, EventArgs e)
        {
            decimal montoContado;
            if (decimal.TryParse(txtMontoContado.Text, out montoContado))
            {
                MessageBox.Show("Corte de caja confirmado.\nMonto contado: $" + montoContado, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelIngresoMonto.Visible = false; // Ocultar la ventana de ingreso después de confirmar
            }
            else
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            decimal dineroContado;

            // Validar si el monto ingresado es un número válido
            if (!decimal.TryParse(txtMontoContado.Text, out dineroContado))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            // Obtener ingresos y gastos
            decimal totalEfectivo = ObtenerTotalEfectivo();
            decimal totalGastos = ObtenerTotalGastos();
            decimal totalEsperado = totalEfectivo - totalGastos;

            // Calcular la diferencia
            decimal diferencia = dineroContado - totalEsperado;

            // Mostrar resultados
            MessageBox.Show($"Total en efectivo: {totalEfectivo:C2}\n" +
                            $"Total de gastos: {totalGastos:C2}\n" +
                            $"Total esperado en caja: {totalEsperado:C2}\n" +
                            $"Diferencia: {diferencia:C2}");

            // Guardar en la base de datos
            RegistrarCorteCaja(dineroContado);



        }
        private decimal ObtenerTotalEfectivo()
        {
            decimal total = 0;
            string query = "SELECT SUM(total) FROM tipopago WHERE id_pago = 1 )";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            total = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener ingresos en efectivo: " + ex.Message);
                    }
                }
            }
            return total;
        }

        private decimal ObtenerTotalGastos()
        {
            decimal total = 0;
            string query = "SELECT SUM(cantidad) FROM gastos WHERE id_gasto IN (SELECT id_caja FROM caja WHERE DATE(fecha) = CURDATE())";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            total = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener los gastos: " + ex.Message);
                    }
                }
            }
            return total;
        }

        private void RegistrarCorteCaja(decimal dineroContado)
        {
            string query = "INSERT INTO caja (cantidad, fecha) VALUES (@cantidad, NOW(), ";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cantidad", dineroContado);


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Corte de caja registrado exitosamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar el corte de caja: " + ex.Message);
                    }
                }
            }
        }
 



        private void btnCancelarMonto_Click(object sender, EventArgs e)
        {
            panelIngresoMonto.Visible = false; // Ocultar la ventana de ingreso
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin();
            formAdmin.ShowDialog();
        }
    }
}
