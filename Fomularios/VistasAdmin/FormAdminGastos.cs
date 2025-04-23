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
                txtEfectivoRecolectado.Text = conexion.ObtenerEfectivoRecolectado().ToString("C");

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
            dgvGastos.DataSource = conexion.ObtenerGastosDelDia();
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

        }


        // Evento para agregar un nuevo gasto
        private void btnAgregarGasto_Click(object sender, EventArgs e)
        {
            // 1. Obtener datos desde los TextBox
            decimal cantidad = Convert.ToDecimal(txtCantidad.Text);
            string descripcion = txtDescripcion.Text;

            // 2. Obtener la última caja inicializada
            int idCaja = conexion.ObtenerUltimaCaja();

            // 3. Validar existencia de caja
            if (idCaja == -1)
            {
                MessageBox.Show("⚠️ No hay una caja inicializada. Por favor, regístrala primero.");
                return;
            }

            // 4. Insertar gasto
            conexion.InsertarGasto(cantidad, descripcion, idCaja);

            // 5. Recargar gastos del día en el DataGridView
            CargarGastosDelDia();

            // 6. Limpiar campos si quieres
            txtCantidad.Clear();
            txtDescripcion.Clear();
            txtCantidad.Focus();


        }

        private void btnEditarGasto_Click(object sender, EventArgs e)
        {

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


    }
}
