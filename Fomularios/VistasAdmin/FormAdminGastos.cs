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
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormAdminGastos: Form

    {
        Conexion conexion = new Conexion();
        private int idUsuario = 1;  // Este valor debe ser tomado del usuario logueado
        private int idCaja;
        private int idGastoSeleccionado = -1;



        public FormAdminGastos()
        {
           
            idCaja = conexion.ObtenerCajaActiva(idUsuario);  // Obtenemos la caja más reciente activa
            InitializeComponent();

        }


        // Método para cargar el resumen del día
        private void CargarResumenDelDia()
        {
            if (idCaja > 0)
            {
                // Mostrar fondo inicial
                txtFondoInicial.Text = conexion.ObtenerFondoInicial(idUsuario).ToString("C");

                // Mostrar efectivo recolectado
                txtEfectivoRecolectado.Text = conexion.ObtenerEfectivoRecolectado(idCaja).ToString("C");

                // Mostrar total de gastos
                txtTotalGastos.Text = conexion.ObtenerTotalGastosDelDia(idCaja).ToString("C");

                // Mostrar los gastos en el DataGridView
                dgvGastos.DataSource = conexion.ObtenerGastosDelDia(idCaja);
            }
            else
            {
                MessageBox.Show("No hay una caja activa para este usuario.");
            }
        }

      

        // Evento Load para cargar los datos al abrir el formulario
        private void FormAdminGastos_Load(object sender, EventArgs e)
        {
            CargarGastosDelDia();
            CargarResumenDelDia();
        
        }

        private void CargarGastosDelDia()
        {
            DataTable dt = conexion.ObtenerGastosDelDia();
            dgvGastos.DataSource = dt;

            // Formato
            dgvGastos.Columns["cantidad"].DefaultCellStyle.Format = "C2";
            dgvGastos.Columns["descripcion"].HeaderText = "Descripción";
            dgvGastos.Columns["fecha"].DefaultCellStyle.Format = "g";

        }
        private void MostrarTotales()
        {
            decimal fondoInicial = conexion.ObtenerFondoInicial(idUsuario);
            decimal efectivoRecolectado = conexion.ObtenerEfectivoRecolectado(idCaja);
            decimal totalGastos = conexion.ObtenerTotalGastosDelDia();
            decimal utilidad = efectivoRecolectado - totalGastos;

            txtFondoInicial.Text = fondoInicial.ToString("C2");
            txtEfectivoRecolectado.Text = efectivoRecolectado.ToString("C2");
            txtTotalGastos.Text = totalGastos.ToString("C2");
            txtUtilidad.Text = utilidad.ToString("C2");
        }
        private void LimpiarCampos()
        {
            try
            {
                // Limpiar los campos de texto
                txtCantidad.Clear();
                txtDescripcion.Clear();
                idGastoSeleccionado = -1; // Restablecer el ID del gasto seleccionado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar campos: " + ex.Message);
            }
        }
        private void ActualizarResumen()
        {
            if (idCaja > 0)
            {
                decimal fondoInicial = conexion.ObtenerFondoInicial(idUsuario);
                decimal efectivoRecolectado = conexion.ObtenerEfectivoRecolectado(idCaja);  // Pasa idCaja
                decimal totalGastos = conexion.ObtenerTotalGastosDelDia(idCaja);
                decimal utilidad = efectivoRecolectado - totalGastos;

                txtFondoInicial.Text = fondoInicial.ToString("C2");
                txtEfectivoRecolectado.Text = efectivoRecolectado.ToString("C2");
                txtTotalGastos.Text = totalGastos.ToString("C2");
                txtUtilidad.Text = utilidad.ToString("C2");
            }
            else
            {
                MessageBox.Show("No hay una caja activa para mostrar los totales.");
            }
        }



























        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFondoInicial_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEfectivoRecolectado_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotalGastos_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvGastos.Rows[e.RowIndex];

                idGastoSeleccionado = Convert.ToInt32(fila.Cells["id_gasto"].Value);
                txtCantidad.Text = fila.Cells["cantidad"].Value.ToString();
                txtDescripcion.Text = fila.Cells["descripcion"].Value.ToString();
            }

        }


        // Evento para agregar un nuevo gasto
        private void btnAgregarGasto_Click(object sender, EventArgs e)
        {
            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtCantidad.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            // Validar que la cantidad sea numérica y positiva
            if (!decimal.TryParse(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingresa una cantidad válida y mayor a 0.");
                return;
            }

            string descripcion = txtDescripcion.Text.Trim();
            int idCaja = conexion.ObtenerUltimaCaja();

            if (idCaja == -1)
            {
                MessageBox.Show("⚠️ No hay una caja inicializada.");
                return;
            }

            conexion.InsertarGasto(cantidad, descripcion, idCaja);
            CargarGastosDelDia();
            MostrarTotales();

            txtCantidad.Clear();
            txtDescripcion.Clear();
            txtCantidad.Focus();

        }

        private void btnEditarGasto_Click(object sender, EventArgs e)
        {
            if (idGastoSeleccionado != -1)
            {
                decimal cantidad;
                if (!decimal.TryParse(txtCantidad.Text, out cantidad))
                {
                    MessageBox.Show("Cantidad no válida.");
                    return;
                }

                string descripcion = txtDescripcion.Text;

                bool actualizado = conexion.ActualizarGasto(idGastoSeleccionado, cantidad, descripcion);
                if (actualizado)
                {
                    MessageBox.Show("Gasto actualizado correctamente.");
                    CargarGastosDelDia(); // refrescar el DataGridView
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el gasto.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un gasto para editar.");
            }

        }

        // Evento para eliminar un gasto seleccionado
        private void btnEliminarGasto_Click(object sender, EventArgs e)
        {

            if (dgvGastos.SelectedRows.Count > 0)
            {
                int idGasto = Convert.ToInt32(dgvGastos.SelectedRows[0].Cells["id_gasto"].Value);
                conexion.EliminarGasto(idGasto);
                CargarResumenDelDia();
            }
        }


        private void btnRegresar_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnMostrarGastos_Click(object sender, EventArgs e)
        {
            CargarGastosDelDia();      // Actualiza el DataGridView
            ActualizarResumen();       // Actualiza los totales en los TextBox
        }
    }
}
