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
        private int idUsuario;  // Variable que guarda el ID del usuario



        public FormAdminFunciones(int usuarioId)
        {
            InitializeComponent();
            panelConfirmacion.Visible = false;
            panelIngresoMonto.Visible = false;
            panelResultadoCorte.Visible = false;
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

        private  void btnConfirmarCorte_Click(object sender, EventArgs e)
        {
           
        }
        

        private void btnCancelarCorte_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = false; // Ocultar la ventana de confirmación
        }

        private void btnConfirmarMonto_Click(object sender, EventArgs e)
        {
            
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

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirRecibo_Click_Click(object sender, EventArgs e)
        {
          
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
       
       




    }
}
