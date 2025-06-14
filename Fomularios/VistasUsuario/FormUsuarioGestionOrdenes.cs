﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using LaCaguamaSV.Fomularios.VistasAdmin;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    public partial class FormUsuarioGestionOrdenes: Form
    {
        private int idMesaActual; // Almacenar el ID de la mesa actual
        public FormUsuarioGestionOrdenes(int idOrden, string nombreCliente, decimal total, decimal descuento, string fechaOrden, string numeroMesa, string tipoPago, string nombreUsuario, string estadoOrden)
        {
            InitializeComponent();
            CargarDatosOrden(idOrden, nombreCliente, total, descuento, fechaOrden, numeroMesa, tipoPago, nombreUsuario, estadoOrden);
            CargarPedidosDesdeBD(); // Cargar pedidos existentes al iniciar
            ActualizarTotal(); // Sincronizar el total
        }
        private List<(int idPedido, string nombre, decimal precio)> pedidos = new List<(int, string, decimal)>();
        private decimal totalOrden = 0;


        private string tipoItemActual;


        private void CargarBebidas()
        {
            dataGridViewMenuU.DataSource = OrdenesD.ObtenerBebidas();
            tipoItemActual = "bebida";
        }

        private void CargarPlatos()
        {
            dataGridViewMenuU.DataSource = OrdenesD.ObtenerPlatos();
            tipoItemActual = "plato";
        }

        private void CargarExtras()
        {
            dataGridViewMenuU.DataSource = OrdenesD.ObtenerExtras();
            tipoItemActual = "extra";
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCargarBebidasU_Click_1(object sender, EventArgs e)
        {
            CargarBebidas();
        }

        private void btnCargarPlatosU_Click_1(object sender, EventArgs e)
        {
            CargarPlatos();
        }

        private void btnCargarExtrasU_Click_1(object sender, EventArgs e)
        {
            CargarExtras();
        }

        private void btnPrecuentaU_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewMenuU_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewMenuU.Rows[e.RowIndex];

                int idItem = Convert.ToInt32(row.Cells["ID"].Value);
                string nombre = row.Cells["nombre"].Value.ToString();
                decimal precio = Convert.ToDecimal(row.Cells["precioUnitario"].Value);

                using (var inputDialog = new Form())
                {
                    inputDialog.Text = $"Seleccionar cantidad para {nombre}";
                    inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                    inputDialog.MaximizeBox = false;
                    inputDialog.MinimizeBox = false;
                    inputDialog.StartPosition = FormStartPosition.CenterParent;
                    inputDialog.Size = new Size(300, 150);

                    Label lblCantidad = new Label()
                    {
                        Text = "Ingrese la cantidad:",
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Padding = new Padding(10)
                    };

                    NumericUpDown numericUpDown = new NumericUpDown()
                    {
                        Minimum = 1,
                        Maximum = 100,
                        Value = 1,
                        Dock = DockStyle.Top,
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font("Arial", 12, FontStyle.Bold)
                    };

                    Button okButton = new Button()
                    {
                        Text = "Aceptar",
                        DialogResult = DialogResult.OK,
                        Dock = DockStyle.Bottom,
                        BackColor = Color.LightGreen,
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };

                    inputDialog.Controls.Add(numericUpDown);
                    inputDialog.Controls.Add(lblCantidad);
                    inputDialog.Controls.Add(okButton);
                    inputDialog.AcceptButton = okButton;

                    if (inputDialog.ShowDialog() == DialogResult.OK)
                    {
                        int cantidad = (int)numericUpDown.Value;
                        bool agregado = AgregarPedido(idItem, cantidad);

                        if (agregado)
                        {
                            for (int i = 0; i < cantidad; i++)
                            {
                                pedidos.Add((-1, nombre, precio));
                            }
                            MostrarPedidosEnPanel();
                            ActualizarTotal();
                        }
                    }
                }
            }
        }



        private bool AgregarPedido(int idItem, int cantidad)
        {
            // Verificar inventario antes de agregar
            string mensajeInventario = "";

            // Versión compatible con C# 7.3
            if (tipoItemActual == "plato")
            {
                mensajeInventario = OrdenesD.VerificarInventarioPlato(idItem);
            }
            else if (tipoItemActual == "bebida")
            {
                mensajeInventario = OrdenesD.VerificarInventarioBebida(idItem);
            }
            else if (tipoItemActual == "extra")
            {
                mensajeInventario = OrdenesD.VerificarInventarioExtra(idItem);
            }

            // Mostrar advertencia si hay inventario bajo
            if (!string.IsNullOrEmpty(mensajeInventario))
            {
                string tipoItem;
                if (tipoItemActual == "plato")
                {
                    tipoItem = "plato";
                }
                else if (tipoItemActual == "bebida")
                {
                    tipoItem = "bebida";
                }
                else if (tipoItemActual == "extra")
                {
                    tipoItem = "extra";
                }
                else
                {
                    tipoItem = "ítem";
                }

                DialogResult result = MessageBox.Show(
                    $"⚠ Advertencia: Algunos ingredientes o productos tienen un inventario bajo:\n\n" +
                    $"{mensajeInventario}\n\n" +
                    "Para reponer stock, por favor contacte a un supervisor o administrador.\n\n" +
                    "¿Desea continuar con el pedido de todas formas?",
                    "Inventario Bajo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }

            // Resto del método permanece igual...
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string query = "INSERT INTO pedidos (id_orden, id_estadoP, id_plato, id_bebida, id_extra, Cantidad) " +
                                   "VALUES (@idOrden, 1, @idPlato, @idBebida, @idExtra, @Cantidad)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrdenU.Text));
                        cmd.Parameters.AddWithValue("@Cantidad", cantidad);

                        cmd.Parameters.AddWithValue("@idPlato", tipoItemActual == "plato" ? idItem : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@idBebida", tipoItemActual == "bebida" ? idItem : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@idExtra", tipoItemActual == "extra" ? idItem : (object)DBNull.Value);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            ActualizarTotal(); // Actualizar el total desde la BD
                            return true;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar pedido: " + ex.Message);
                    return false;
                }
            }
        }


        private void MostrarPedidosEnPanel()
        {
            flowLayoutPanelPedidosU.Controls.Clear();

            var pedidosAgrupados = pedidos
                .GroupBy(p => (p.idPedido, p.nombre, p.precio))
                .Select(g => new {
                    IdPedido = g.Key.idPedido,
                    Nombre = g.Key.nombre,
                    Precio = g.Key.precio,
                    Cantidad = g.Count()
                })
                .OrderBy(x => x.Nombre);

            foreach (var grupo in pedidosAgrupados)
            {
                Panel panelPedido = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.DarkGreen,
                    Margin = new Padding(5),
                    Padding = new Padding(5),
                    Width = flowLayoutPanelPedidosU.Width - 25,
                    Height = 40
                };

                Label lblPedido = new Label
                {
                    Text = $"{grupo.Nombre} - {grupo.Precio:C} x{grupo.Cantidad}",
                    ForeColor = Color.White,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Cursor = Cursors.Hand
                };

                Button btnEliminar = new Button
                {
                    Text = "X",
                    ForeColor = Color.White,
                    BackColor = Color.Red,
                    FlatStyle = FlatStyle.Flat,
                    Width = 30,
                    Dock = DockStyle.Right,
                    Cursor = Cursors.Hand,
                    Tag = grupo.IdPedido
                };

                lblPedido.Click += (sender, e) => MostrarConfirmacionEliminacion(grupo.IdPedido, grupo.Nombre, grupo.Cantidad);
                btnEliminar.Click += (sender, e) => MostrarConfirmacionEliminacion(grupo.IdPedido, grupo.Nombre, grupo.Cantidad);

                panelPedido.Controls.Add(lblPedido);
                panelPedido.Controls.Add(btnEliminar);
                flowLayoutPanelPedidosU.Controls.Add(panelPedido);
            }
        }


        private void MostrarConfirmacionEliminacion(int idPedido, string nombre, int cantidad)
        {
            if (MessageBox.Show($"¿Eliminar {nombre} (x{cantidad})?", "Confirmar Eliminación",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EliminarPedidoDeBD(idPedido);
                CargarPedidosDesdeBD();
                ActualizarTotal();
            }
        }

        private void EliminarPedidoDeBD(int idPedido)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        string queryDelete = "DELETE FROM pedidos WHERE id_pedido = @idPedido";
                        using (MySqlCommand cmdDelete = new MySqlCommand(queryDelete, conexion, transaction))
                        {
                            cmdDelete.Parameters.AddWithValue("@idPedido", idPedido);
                            cmdDelete.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error al eliminar el pedido: " + ex.Message);
                        throw;
                    }
                }
            }
        }
        private void ActualizarTotal()
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string queryTotal = @"
                    SELECT 
                        IFNULL(SUM(
                            CASE 
                                WHEN p.id_plato IS NOT NULL THEN (SELECT pl.precioUnitario FROM platos pl WHERE pl.id_plato = p.id_plato) * p.Cantidad
                                WHEN p.id_bebida IS NOT NULL THEN (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida) * p.Cantidad
                                WHEN p.id_extra IS NOT NULL THEN (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra) * p.Cantidad
                                ELSE 0
                            END
                        ), 0) AS Total
                    FROM pedidos p
                    WHERE p.id_orden = @idOrden";

                    decimal nuevoTotal = 0;

                    using (MySqlCommand cmdTotal = new MySqlCommand(queryTotal, conexion))
                    {
                        cmdTotal.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrdenU.Text));
                        object result = cmdTotal.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            nuevoTotal = Convert.ToDecimal(result);
                        }
                    }

                    string queryUpdate = "UPDATE ordenes SET total = @total WHERE id_orden = @idOrden";
                    using (MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, conexion))
                    {
                        cmdUpdate.Parameters.AddWithValue("@total", nuevoTotal);
                        cmdUpdate.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrdenU.Text));
                        cmdUpdate.ExecuteNonQuery();
                    }

                    totalOrden = nuevoTotal;
                    lblTotalU.Text = totalOrden.ToString("C");
                    CargarPedidosDesdeBD();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el total: " + ex.Message);
                }

                // Notificar al FormAdmin para actualizar
                foreach (Form form in Application.OpenForms)
                {
                    if (form is FormAdmin)
                    {
                        ((FormAdmin)form).RefrescarOrdenes();
                        break;
                    }
                }
            }
        }

        private void CargarPedidosDesdeBD()
        {
            pedidos.Clear();

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
            SELECT 
                p.id_pedido,
                COALESCE(
                    pl.nombrePlato,
                    (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = p.id_bebida),
                    (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = p.id_extra)
                ) AS nombre,
                COALESCE(
                    pl.precioUnitario,
                    (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                    (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra)
                ) AS precio,
                p.Cantidad
            FROM pedidos p
            LEFT JOIN platos pl ON p.id_plato = pl.id_plato
            WHERE p.id_orden = @idOrden";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrdenU.Text));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPedido = reader.GetInt32("id_pedido");
                            string nombre = reader.GetString("nombre");
                            decimal precio = reader.GetDecimal("precio");
                            int cantidad = reader.GetInt32("Cantidad");

                            for (int i = 0; i < cantidad; i++)
                            {
                                pedidos.Add((idPedido, nombre, precio));
                            }
                        }
                    }
                }
            }

            MostrarPedidosEnPanel();
        }



        private async void comboBoxMesasU_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evitar ejecución durante inicialización
            if (comboBoxMesasU.SelectedValue == null ||
                !(comboBoxMesasU.SelectedValue is int) ||
                comboBoxMesasU.SelectedIndex == -1)
            {
                return;
            }

            int nuevaMesaId;
            try
            {
                // Conversión segura
                nuevaMesaId = (int)comboBoxMesasU.SelectedValue;
            }
            catch (InvalidCastException)
            {
                // Manejar caso donde el valor no es int
                MessageBox.Show("Error: El ID de mesa no es válido");
                comboBoxMesasU.SelectedValue = idMesaActual;
                return;
            }

            // Si es la misma mesa, no hacer nada
            if (nuevaMesaId == idMesaActual)
                return;

            // Confirmar cambio
            var confirmacion = MessageBox.Show($"¿Cambiar a {comboBoxMesasU.Text}?",
                                             "Confirmar cambio de mesa",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
            {
                comboBoxMesasU.SelectedValue = idMesaActual;
                return;
            }

            try
            {
                bool exito = await OrdenesD.CambiarMesaOrdenAsync(
                    Convert.ToInt32(lblIdOrdenU.Text),
                    idMesaActual,
                    nuevaMesaId);

                if (exito)
                {
                    idMesaActual = nuevaMesaId;
                    MessageBox.Show("Mesa cambiada exitosamente");

                    // Actualizar la lista de órdenes en el formulario principal
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form is FormAdmin adminForm)
                        {
                            adminForm.RefrescarOrdenes();
                            break;
                        }
                    }
                }
                else
                {
                    comboBoxMesasU.SelectedValue = idMesaActual;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar mesa: {ex.Message}");
                comboBoxMesasU.SelectedValue = idMesaActual;
            }
        }


        private void CargarDatosOrden(int idOrden, string nombreCliente, decimal total, decimal descuento,
                    string fechaOrden, string numeroMesa, string tipoPago,
                    string nombreUsuario, string estadoOrden)
        {
            lblIdOrdenU.Text = idOrden.ToString();
            lblNombreClienteU.Text = nombreCliente;
            lblTotalU.Text = total.ToString("C");
            lblDescuentoU.Text = descuento.ToString("C");
            lblFechaOrdenU.Text = fechaOrden;

            // Obtener ID de mesa actual con manejo robusto
            idMesaActual = ObtenerIdMesaActual(numeroMesa);

            if (idMesaActual <= 0)
            {
                MessageBox.Show("Error: No se pudo obtener la mesa actual");
                return;
            }

            // Cargar mesas disponibles
            var mesasDisponibles = OrdenesD.ObtenerMesasDisponibles(idMesaActual);

            // Verificar que se obtuvieron resultados
            if (mesasDisponibles.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron mesas disponibles");
                return;
            }

            // Configurar ComboBox
            comboBoxMesasU.BeginUpdate(); // Evitar parpadeo
            comboBoxMesasU.DataSource = mesasDisponibles;
            comboBoxMesasU.DisplayMember = "nombreMesa";
            comboBoxMesasU.ValueMember = "id_mesa";

            // Asegurar que el valor sea del tipo correcto
            try
            {
                comboBoxMesasU.SelectedValue = idMesaActual;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar mesa: {ex.Message}");
                // Seleccionar el primer elemento como fallback
                if (comboBoxMesasU.Items.Count > 0)
                    comboBoxMesasU.SelectedIndex = 0;
            }
            finally
            {
                comboBoxMesasU.EndUpdate();
            }

            lblTipoPagoU.Text = tipoPago;
            lblNombreUsuarioU.Text = nombreUsuario;
            lblEstadoOrdenU.Text = estadoOrden;
        }

        private int ObtenerIdMesaActual(string nombreMesa)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = "SELECT id_mesa FROM mesas WHERE nombreMesa = @nombreMesa";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombreMesa", nombreMesa);
                        object result = cmd.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                        {
                            MessageBox.Show($"No se encontró la mesa: {nombreMesa}");
                            return -1;
                        }

                        // Convertir explícitamente a int
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al obtener ID de mesa: {ex.Message}");
                return -1;
            }
        }

        public static DataTable ObtenerMesasDisponibles(int idMesaActual)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"SELECT id_mesa, nombreMesa 
                          FROM mesas 
                          WHERE id_estadoM = 1 OR id_mesa = @idMesaActual
                          ORDER BY nombreMesa";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        // Asegurar el tipo de parámetro
                        cmd.Parameters.Add("@idMesaActual", MySqlDbType.Int32).Value = idMesaActual;

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);

                            // Verificar que la mesa actual está incluida
                            bool contieneMesaActual = false;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToInt32(row["id_mesa"]) == idMesaActual)
                                {
                                    contieneMesaActual = true;
                                    break;
                                }
                            }

                            if (!contieneMesaActual && idMesaActual > 0)
                            {
                                // Agregar la mesa actual si no está en los resultados
                                DataRow newRow = dt.NewRow();
                                newRow["id_mesa"] = idMesaActual;
                                newRow["nombreMesa"] = "[Mesa Actual]";
                                dt.Rows.InsertAt(newRow, 0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar mesas: {ex.Message}");
            }

            return dt;
        }

        private void flowLayoutPanelPedidosU_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
