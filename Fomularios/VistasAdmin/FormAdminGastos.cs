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
    public partial class FormAdminGastos : Form

    {
        Conexion conexion = new Conexion();
        private int idUsuario = 1;  // Este valor debe ser tomado del usuario logueado
       
        private int idCaja;
        private int idGastoSeleccionado = -1;


        public FormAdminGastos()
        {

            InitializeComponent();


            try
            {
                idCaja = conexion.ObtenerUltimaCaja();
                // Obtenemos la caja más reciente activa


                dgvGastos.MultiSelect = false;
                // Tamaño fijo
                this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

                // Posición fija (centrada en la pantalla)
                this.StartPosition = FormStartPosition.CenterScreen;

                //Ojoooo, esto hace que pueda seleccionar toda la fila de datos, independientemente de donde le de click
                dgvGastos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvGastos.MultiSelect = false;
                dgvGastos.ReadOnly = true;
                dgvGastos.AllowUserToAddRows = false;
                dgvGastos.AllowUserToDeleteRows = false;
                dgvGastos.AllowUserToResizeRows = false;

                // 💡 Asocia el evento para permitir clic en cualquier parte de la fila
                dgvGastos.CellClick += dgvGastos_CellClick;

                CargarGastosDelDia();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la caja activa: " + ex.Message);
            }

        }


        public static class SesionUsuario
        {
            public static int IdUsuario { get; set; }
            public static string NombreUsuario { get; set; }
            public static int Rol { get; set; }


            public static bool EsAdmin()
            {
                return Rol == 1;
            }
        }


        // Método para cargar el resumen del día
        private void CargarResumenDelDia()
        {

        }




        // Evento Load para cargar los datos al abrir el formulario
        private void FormAdminGastos_Load_1(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();


            cmbTipoFiltroGasto.Items.AddRange(new string[] {
        "Todos", "Hoy", "Esta semana", "Este mes", "Fecha específica", "Rango de fechas"
    });
            cmbTipoFiltroGasto.SelectedIndex = 0;



            // Obtener la caja más reciente sin filtrar por usuario
            int idCaja = conexion.ObtenerUltimaCaja(); // Asegúrate de tener este método en tu clase Conexion

            if (idCaja != -1)
            {
                decimal fondoInicial = conexion.ObtenerFondoInicial(idCaja);
                decimal efectivoRecolectado = conexion.ObtenerEfectivoRecolectado(idCaja);
                decimal totalGastos = conexion.ObtenerTotalGastos(idCaja);

                txtFondoInicial.Text = fondoInicial.ToString("C2");
                txtEfectivoRecolectado.Text = efectivoRecolectado.ToString("C2");
                txtTotalGastos.Text = totalGastos.ToString("C2");
                CargarGastos();
                MostrarTotales();
            }
        }

        private void CargarGastosDelDia()
        {
            try
            {
                // Llamamos a la función pasando el idCaja
                dgvGastos.DataSource = conexion.ObtenerGastosDelDia(idCaja);

                // Formato
                dgvGastos.Columns["cantidad"].DefaultCellStyle.Format = "C2";
                dgvGastos.Columns["descripcion"].HeaderText = "Descripción";
                dgvGastos.Columns["fecha"].DefaultCellStyle.Format = "g";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los gastos del día: " + ex.Message);
            }
        }
        private void MostrarTotales()
        {
            try
            {
                int idCaja = conexion.ObtenerUltimaCaja(); // ya no por idUsuario

                if (idCaja == -1)
                {
                    MessageBox.Show("No se encontró una caja reciente.");
                    return;
                }

                decimal fondoInicial = conexion.ObtenerFondoInicial(idCaja);

                decimal efectivoRecolectado = conexion.ObtenerEfectivoRecolectado(idCaja);
                decimal totalGastos = conexion.ObtenerTotalGastosDelDia(idCaja);
                decimal utilidad = fondoInicial + efectivoRecolectado - totalGastos;

                txtFondoInicial.Text = fondoInicial.ToString("C2");
                txtEfectivoRecolectado.Text = efectivoRecolectado.ToString("C2");
                txtTotalGastos.Text = totalGastos.ToString("C2");
                txtUtilidad.Text = utilidad.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los totales: " + ex.Message);
            }
        }

        private void ActualizarResumen()
        {
            try
            {
                decimal totalGastos = conexion.ObtenerTotalGastosDelDia(idCaja);
                txtTotalGastos.Text = totalGastos.ToString("C2");

                // Otros TextBox como fondoInicial, efectivo, totalFinal los puedes ocultar o ignorar por ahora
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar resumen: " + ex.Message);
            }
        }



        private void LimpiarCampos()
        {

            try
            {
                // Asegúrate de obtener el idCaja de forma segura
                int idCaja = conexion.ObtenerUltimaCaja(); // O el método que uses para obtener el idCaja

                // Verificamos que idCaja sea válido antes de hacer cualquier operación
                if (idCaja != -1)
                {
                    // Si idCaja es válido, obtenemos los datos
                    decimal totalGastos = conexion.ObtenerTotalGastos(idCaja);
                    txtTotalGastos.Text = totalGastos.ToString("C2");

                    decimal efectivoRecolectado = conexion.ObtenerEfectivoRecolectadoPorCaja(idCaja);
                    txtEfectivoRecolectado.Text = efectivoRecolectado.ToString("C2");
                }
                else
                {
                    // Si no hay caja activa, muestra un mensaje o establece valores por defecto
                    MessageBox.Show("No hay caja activa.");
                    txtTotalGastos.Text = "0.00";
                    txtEfectivoRecolectado.Text = "0.00";
                }
                MostrarTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar los campos: " + ex.Message);
            }
        }

        private void CargarGastos()
        {
            int idCaja = conexion.ObtenerUltimaCaja(); // Asegúrate de tener este método ya hecho

            if (idCaja != -1)
            {
                DataTable gastos = conexion.ObtenerGastosPorCaja(idCaja);
                dgvGastos.DataSource = gastos;
            }
        }


        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                MessageBox.Show("Rol actual: " + SesionUsuario.Rol); // <-- aquí
                if (SesionUsuario.Rol !=1)
                {
                    MessageBox.Show("Solo los administradores pueden editar gastos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvGastos.Rows[e.RowIndex];

                    idGastoSeleccionado = Convert.ToInt32(fila.Cells["id_gasto"].Value);
                    txtCantidad.Text = fila.Cells["cantidad"].Value.ToString();
                    txtDescripcion.Text = fila.Cells["descripcion"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar un gasto: " + ex.Message);
            }
        }


        // Evento para agregar un nuevo gasto
        private void btnAgregarGasto_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtCantidad.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor completa todos los campos.");
                return;
            }


            try
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
                // 👉 Validar que el gasto no supere la utilidad
                decimal fondoInicial = conexion.ObtenerFondoInicial(idCaja);
                decimal efectivoRecolectado = conexion.ObtenerEfectivoRecolectado(idCaja);
                decimal totalGastos = conexion.ObtenerTotalGastosDelDia(idCaja);

                decimal utilidad = fondoInicial + efectivoRecolectado - totalGastos;

                if (cantidad >= utilidad)
                {
                    MessageBox.Show("⚠️ El gasto excede el Efectivo disponible. No se puede registrar.");
                    return;
                }

                // Insertar gasto si todo está bien
                conexion.InsertarGasto(cantidad, descripcion, idCaja);
                CargarGastosDelDia();
                MostrarTotales();

                txtCantidad.Clear();
                txtDescripcion.Clear();
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el gasto: " + ex.Message);
            }

        }

        private void btnEditarGasto_Click(object sender, EventArgs e)
        {
           

            try
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
                        MostrarTotales();
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el gasto: " + ex.Message);
            }

        }


        private void FiltrarGastos()
        {
            string tipoFiltro = cmbTipoFiltroGasto.SelectedItem?.ToString();
            DateTime fechaInicio = dtpFechaInicioGasto.Value.Date;
            DateTime fechaFin = dtpFechaFinGasto.Value.Date;

            switch (tipoFiltro)
            {
                case "Hoy":
                    fechaInicio = DateTime.Today;
                    fechaFin = DateTime.Today;
                    break;

                case "Esta semana":
                    int delta = (int)DateTime.Today.DayOfWeek;
                    fechaInicio = DateTime.Today.AddDays(-delta);
                    fechaFin = DateTime.Today;
                    break;

                case "Este mes":
                    fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    fechaFin = DateTime.Today;
                    break;

                case "Fecha específica":
                    fechaFin = fechaInicio;
                    break;

                case "Rango de fechas":
                    // fechaInicio y fechaFin ya vienen del DateTimePicker
                    break;

                case "Todos":
                default:
                    // valores por defecto
                    fechaInicio = DateTime.MinValue;
                    fechaFin = DateTime.MaxValue;
                    break;
            }

            DataTable gastosFiltrados = conexion.ObtenerGastosPorFiltro(tipoFiltro, fechaInicio, fechaFin);
            dgvGastos.DataSource = gastosFiltrados;
        }


        // Evento para eliminar un gasto seleccionado
        private void btnEliminarGasto_Click(object sender, EventArgs e)
        {


        }

        private void btnMostrarGastos_Click(object sender, EventArgs e)
        {

        }




        private void btnFiltrar_Click(object sender, EventArgs e)
        {

            FiltrarGastos();
        }

        private void cmbTipoFiltroGasto_SelectedIndexChanged(object sender, EventArgs e)
        {

            string filtro = cmbTipoFiltroGasto.SelectedItem.ToString();

            dtpFechaInicioGasto.Enabled = filtro == "Fecha específica" || filtro == "Rango de fechas";
            dtpFechaFinGasto.Enabled = filtro == "Rango de fechas";


            FiltrarGastos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarGastosDeCajaReciente();
        }

        private void MostrarGastosDeCajaReciente()
        {
            int idCaja = conexion.ObtenerUltimaCajaRegistrada(); // Ya no depende del idUsuario

            if (idCaja != -1)
            {
                DataTable tablaGastos = conexion.ObtenerGastosPorIdCaja(idCaja);
                dgvGastos.DataSource = tablaGastos;
            }
            else
            {
                MessageBox.Show("No hay una caja reciente registrada.");
                dgvGastos.DataSource = null;
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUtilidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
