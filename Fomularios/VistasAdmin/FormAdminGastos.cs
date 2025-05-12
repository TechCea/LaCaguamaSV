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
            try
            {
                idCaja = conexion.ObtenerCajaActiva(idUsuario);  // Obtenemos la caja más reciente activa
                InitializeComponent();
                dgvGastos.MultiSelect = false;
                // Tamaño fijo
                this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

                // Posición fija (centrada en la pantalla)
                this.StartPosition = FormStartPosition.CenterScreen;

                CargarGastosDelDia();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la caja activa: " + ex.Message);
            }

        }


        // Método para cargar el resumen del día
        private void CargarResumenDelDia()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el resumen del día: " + ex.Message);
            }
        }




        // Evento Load para cargar los datos al abrir el formulario
        private void FormAdminGastos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGastosDelDia();
                CargarResumenDelDia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
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
                decimal fondoInicial = conexion.ObtenerFondoInicial(idUsuario);
                decimal efectivoRecolectado = conexion.ObtenerEfectivoRecolectado(idCaja);
                decimal totalGastos = conexion.ObtenerTotalGastosDelDia(idCaja);
                decimal utilidad = efectivoRecolectado - totalGastos;

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



        private void LimpiarCampos()
        {
            try
            {
                // Asegúrate de obtener el idCaja de forma segura
                int idCaja = conexion.ObtenerCajaActiva(idUsuario); // O el método que uses para obtener el idCaja

                // Verificamos que idCaja sea válido antes de hacer cualquier operación
                if (idCaja > 0)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar los campos: " + ex.Message);
            }
        }

        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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

        // Evento para eliminar un gasto seleccionado
        private void btnEliminarGasto_Click(object sender, EventArgs e)
        {

            try
            {
                if (dgvGastos.SelectedRows.Count > 0)
                {
                    int idGasto = Convert.ToInt32(dgvGastos.SelectedRows[0].Cells["id_gasto"].Value);
                    conexion.EliminarGasto(idGasto);
                    CargarResumenDelDia(); // Actualiza los resúmenes después de la eliminación
                }
                else
                {
                    MessageBox.Show("Selecciona un gasto para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el gasto: " + ex.Message);
            }
        }

        private void btnMostrarGastos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGastosDelDia();
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los gastos: " + ex.Message);
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


        private void btnFiltrar_Click(object sender, EventArgs e)
        {

        }
    }
}
