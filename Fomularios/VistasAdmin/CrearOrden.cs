using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using static LaCaguamaSV.Login;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class CrearOrden: Form
    {
        public CrearOrden()
        {
            InitializeComponent();
            CargarTipoPago();
            CargarMesas();

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            this.Size = new Size(1000, 500); // Establece un tamaño fijo

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;





        }

        private void CargarTipoPago()
        {
            DataTable dtPagos = OrdenesD.ObtenerTiposPago();
            cmbTipoPago.DataSource = dtPagos;
            cmbTipoPago.DisplayMember = "nombrePago"; // Mostrar el nombre del método de pago
            cmbTipoPago.ValueMember = "id_pago"; // Guardar el ID del método de pago

            // Seleccionar por defecto "Efectivo"
            foreach (DataRow row in dtPagos.Rows)
            {
                if (row["nombrePago"].ToString().ToLower() == "efectivo")
                {
                    cmbTipoPago.SelectedValue = row["id_pago"];
                    break;
                }
            }
        }

        private void CargarMesas()
        {
            DataTable dtMesas = OrdenesD.ObtenerMesas(); // Obtener mesas desde la BD
            cmbMesas.DataSource = dtMesas;
            cmbMesas.DisplayMember = "nombreMesa"; // Mostrar el nombre de la mesa
            cmbMesas.ValueMember = "id_mesa"; // Usar el ID de la mesa como valor

            if (dtMesas.Rows.Count > 0)
            {
                cmbMesas.SelectedIndex = 0; // Seleccionar la primera mesa por defecto
            }
        }


        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMesas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre del cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Obtener datos del formulario
                string nombreCliente = txtNombreCliente.Text;
                int idMesa = Convert.ToInt32(cmbMesas.SelectedValue);
                int tipoPago = Convert.ToInt32(cmbTipoPago.SelectedValue);

                // Verificar si la mesa está disponible
                if (!MesaDisponible(idMesa))
                {
                    MessageBox.Show("La mesa seleccionada no está disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear la orden vacía
                int idOrden = OrdenesD.CrearOrdenVacia(nombreCliente, idMesa, tipoPago);

                if (idOrden > 0)
                {
                    MessageBox.Show($"Orden #{idOrden} creada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cerrar este formulario
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al crear la orden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool MesaDisponible(int idMesa)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = "SELECT id_estadoM FROM mesas WHERE id_mesa = @idMesa";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idMesa", idMesa);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int estado = Convert.ToInt32(result);
                            return estado == 1; // 1 = Disponible
                        }
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {

        }

        private void btnMaximizar_Click_Click(object sender, EventArgs e)
        {
        }

        private void btnMinimizar_Click_Click(object sender, EventArgs e)
        {
           
        }
       
        private void CrearOrden_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void CrearOrden_Load(object sender, EventArgs e)
        {

        }
    }
}
