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


namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormGestionOrdenes : Form
    {
     

        private int idMesaActual; // Almacenar el ID de la mesa actual
        public FormGestionOrdenes(int idOrden, string nombreCliente, decimal total, decimal descuento, string fechaOrden, string numeroMesa, string tipoPago, string nombreUsuario, string estadoOrden)
        {
            InitializeComponent();

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar
            
            this.Size = new Size(1000, 500); // Establece un tamaño fijo

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            CargarDatosOrden(idOrden, nombreCliente, total, descuento, fechaOrden, numeroMesa, tipoPago, nombreUsuario, estadoOrden);
            CargarPedidosDesdeBD(); // Cargar pedidos existentes al iniciar
            ActualizarTotal(); // Sincronizar el total
            dataGridViewMenu.MultiSelect = false;
        }

        private List<(int idPedido, string nombre, decimal precio, string tipo, int cantidad)> pedidos = new List<(int, string, decimal, string, int)>();
        private decimal totalOrden = 0;


        private string tipoItemActual;


        private void CargarBebidas()
        {
            dataGridViewMenu.DataSource = OrdenesD.ObtenerBebidas();
            dataGridViewMenu.Tag = "BEBIDA"; // Usar mayúsculas para consistencia
            tipoItemActual = "BEBIDA";
        }

        private void CargarPlatos()
        {
            dataGridViewMenu.DataSource = OrdenesD.ObtenerPlatos();
            dataGridViewMenu.Tag = "PLATO";
            tipoItemActual = "PLATO";
        }

        private void CargarExtras()
        {
            dataGridViewMenu.DataSource = OrdenesD.ObtenerExtras();
            dataGridViewMenu.Tag = "EXTRA";
            tipoItemActual = "EXTRA";
        }


        private void CargarDatosOrden(int idOrden, string nombreCliente, decimal total, decimal descuento,
                    string fechaOrden, string numeroMesa, string tipoPago,
                    string nombreUsuario, string estadoOrden)
        {
            lblIdOrden.Text = idOrden.ToString();
            lblNombreCliente.Text = nombreCliente;
            lblTotal.Text = total.ToString("C");
            lblDescuento.Text = descuento.ToString("C");
            lblFechaOrden.Text = fechaOrden;

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
            comboBoxMesas.BeginUpdate(); // Evitar parpadeo
            comboBoxMesas.DataSource = mesasDisponibles;
            comboBoxMesas.DisplayMember = "nombreMesa";
            comboBoxMesas.ValueMember = "id_mesa";

            // Asegurar que el valor sea del tipo correcto
            try
            {
                comboBoxMesas.SelectedValue = idMesaActual;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar mesa: {ex.Message}");
                // Seleccionar el primer elemento como fallback
                if (comboBoxMesas.Items.Count > 0)
                    comboBoxMesas.SelectedIndex = 0;
            }
            finally
            {
                comboBoxMesas.EndUpdate();
            }

            lblTipoPago.Text = tipoPago;
            lblNombreUsuario.Text = nombreUsuario;
            lblEstadoOrden.Text = estadoOrden;
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

        private void FormGestionOrdenes_Load(object sender, EventArgs e)
        {

        }

        private void lblNombreUsuario_Click(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCargarBebidas_Click(object sender, EventArgs e)
        {
            CargarBebidas();
        }

        private void btnCargarPlatos_Click(object sender, EventArgs e)
        {
            CargarPlatos();
        }

        private void btnCargarExtras_Click(object sender, EventArgs e)
        {
            CargarExtras();
        }

        private void dataGridViewMenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewMenu.Rows[e.RowIndex];
            string tipoActual = dataGridViewMenu.Tag?.ToString() ?? tipoItemActual ?? "";

            if (!new[] { "PLATO", "BEBIDA", "EXTRA", "PROMOCION" }.Contains(tipoActual))
            {
                MessageBox.Show("Tipo de ítem no reconocido");
                return;
            }

            int idItem = Convert.ToInt32(row.Cells["ID"].Value);
            string nombre = row.Cells["nombre"].Value.ToString();
            decimal precio = Convert.ToDecimal(row.Cells["precioUnitario"].Value);

            // Mostrar formulario de cantidad (el mismo que antes)
            var cantidadForm = new Form()
            {
                Text = $"Agregar {nombre}",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(350, 200),
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };

            // Panel principal
            var panel = new Panel() { Dock = DockStyle.Fill, Padding = new Padding(20) };

            // Etiqueta
            var lbl = new Label()
            {
                Text = $"Cantidad de {nombre}:",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            // Selector de cantidad
            var numCantidad = new NumericUpDown()
            {
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Dock = DockStyle.Top,
                Font = new Font("Arial", 12),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White
            };

            // Panel de botones
            var panelBotones = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(5)
            };

            // Botón Aceptar
            var btnAceptar = new Button()
            {
                Text = "Agregar",
                DialogResult = DialogResult.OK,
                Width = 100,
                Dock = DockStyle.Right,
                BackColor = Color.FromArgb(70, 130, 180),
                FlatStyle = FlatStyle.Flat
            };

            // Botón Cancelar
            var btnCancelar = new Button()
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Width = 100,
                Dock = DockStyle.Right,
                BackColor = Color.FromArgb(220, 80, 80),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 0, 10, 0)
            };

            // Agregar controles
            panelBotones.Controls.AddRange(new[] { btnAceptar, btnCancelar });
            panel.Controls.AddRange(new Control[] { lbl, numCantidad, panelBotones });
            cantidadForm.Controls.Add(panel);

            // Configurar botones
            cantidadForm.AcceptButton = btnAceptar;
            cantidadForm.CancelButton = btnCancelar;

            if (cantidadForm.ShowDialog(this) == DialogResult.OK)
            {
                int cantidad = (int)numCantidad.Value;
                bool resultado = false;

                if (tipoActual == "PROMOCION")
                {
                    string mensajeInventario;
                    resultado = OrdenesD.AgregarPromocionAOrden(Convert.ToInt32(lblIdOrden.Text), idItem, cantidad, out mensajeInventario);

                    if (!string.IsNullOrEmpty(mensajeInventario))
                    {
                        DialogResult result = MessageBox.Show(
                            $"Advertencia de inventario para la promoción:\n\n{mensajeInventario}\n\n" +
                            "¿Desea continuar con el pedido de todas formas?",
                            "Inventario Bajo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            // Forzar la inserción ignorando las advertencias
                            resultado = OrdenesD.AgregarPromocionAOrdenForzado(Convert.ToInt32(lblIdOrden.Text), idItem, cantidad);
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                }
                else
                {
                    resultado = AgregarPedido(idItem, cantidad, tipoActual);
                }

                if (resultado)
                {
                    CargarPedidosDesdeBD();
                    ActualizarTotal();
                }
            }
        }


        private bool AgregarPedido(int idItem, int cantidad, string tipoItem)
        {
            // Validar parámetros
            if (idItem <= 0 || cantidad <= 0 || string.IsNullOrEmpty(tipoItem))
            {
                MessageBox.Show("Datos inválidos para agregar el pedido", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Obtener nombre y precio según el tipo de ítem
            string nombreItem = ObtenerNombreItem(idItem, tipoItem);
            decimal precioItem = ObtenerPrecioItem(idItem, tipoItem);

            if (string.IsNullOrEmpty(nombreItem) || precioItem <= 0)
            {
                MessageBox.Show("No se pudo obtener la información del ítem", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Verificar inventario (opcional)
            string mensajeInventario = VerificarInventarioItem(idItem, tipoItem);
            if (!string.IsNullOrEmpty(mensajeInventario))
            {
                DialogResult result = MessageBox.Show(
                    $"Advertencia de inventario para {nombreItem}:\n\n{mensajeInventario}\n\n" +
                    "¿Desea continuar con el pedido de todas formas?",
                    "Inventario Bajo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }

            // Insertar en la base de datos
            bool resultadoDB = false;
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    // Primero verificar si ya existe un pedido igual en esta orden
                    string queryVerificar = @"SELECT id_pedido, Cantidad 
                            FROM pedidos 
                            WHERE id_orden = @idOrden AND 
                                  ((id_plato = @idPlato AND @tipoItem = 'PLATO') OR
                                   (id_bebida = @idBebida AND @tipoItem = 'BEBIDA') OR
                                   (id_extra = @idExtra AND @tipoItem = 'EXTRA') OR
                                   (id_promocion = @idPromocion AND @tipoItem = 'PROMOCION'))";

                    int idPedidoExistente = -1;
                    int cantidadExistente = 0;

                    using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conexion))
                    {
                        cmdVerificar.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));

                        // Configurar parámetros según el tipo de ítem
                        switch (tipoItem.ToUpper())
                        {
                            case "PLATO":
                                cmdVerificar.Parameters.AddWithValue("@idPlato", idItem);
                                cmdVerificar.Parameters.AddWithValue("@idBebida", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idExtra", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idPromocion", DBNull.Value);
                                break;
                            case "BEBIDA":
                                cmdVerificar.Parameters.AddWithValue("@idPlato", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idBebida", idItem);
                                cmdVerificar.Parameters.AddWithValue("@idExtra", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idPromocion", DBNull.Value);
                                break;
                            case "EXTRA":
                                cmdVerificar.Parameters.AddWithValue("@idPlato", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idBebida", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idExtra", idItem);
                                cmdVerificar.Parameters.AddWithValue("@idPromocion", DBNull.Value);
                                break;
                            case "PROMOCION":
                                cmdVerificar.Parameters.AddWithValue("@idPlato", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idBebida", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idExtra", DBNull.Value);
                                cmdVerificar.Parameters.AddWithValue("@idPromocion", idItem);
                                break;
                        }
                        cmdVerificar.Parameters.AddWithValue("@tipoItem", tipoItem.ToUpper());

                        using (MySqlDataReader reader = cmdVerificar.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idPedidoExistente = reader.GetInt32("id_pedido");
                                cantidadExistente = reader.GetInt32("Cantidad");
                            }
                        }
                    }

                    // Si ya existe, actualizar la cantidad
                    if (idPedidoExistente > 0)
                    {
                        string queryActualizar = @"UPDATE pedidos 
                                  SET Cantidad = Cantidad + @cantidad 
                                  WHERE id_pedido = @idPedido";

                        using (MySqlCommand cmdActualizar = new MySqlCommand(queryActualizar, conexion))
                        {
                            cmdActualizar.Parameters.AddWithValue("@idPedido", idPedidoExistente);
                            cmdActualizar.Parameters.AddWithValue("@cantidad", cantidad);
                            resultadoDB = cmdActualizar.ExecuteNonQuery() > 0;
                        }
                    }
                    else
                    {
                        // Si no existe, insertar nuevo pedido
                        string queryInsertar = @"INSERT INTO pedidos 
                           (id_orden, id_estadoP, id_plato, id_bebida, id_extra, id_promocion, Cantidad) 
                           VALUES (@idOrden, 1, @idPlato, @idBebida, @idExtra, @idPromocion, @cantidad)";

                        using (MySqlCommand cmdInsertar = new MySqlCommand(queryInsertar, conexion))
                        {
                            cmdInsertar.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
                            cmdInsertar.Parameters.AddWithValue("@cantidad", cantidad);

                            // Configurar parámetros según el tipo de ítem
                            switch (tipoItem.ToUpper())
                            {
                                case "PLATO":
                                    cmdInsertar.Parameters.AddWithValue("@idPlato", idItem);
                                    cmdInsertar.Parameters.AddWithValue("@idBebida", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idExtra", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idPromocion", DBNull.Value);
                                    break;
                                case "BEBIDA":
                                    cmdInsertar.Parameters.AddWithValue("@idPlato", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idBebida", idItem);
                                    cmdInsertar.Parameters.AddWithValue("@idExtra", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idPromocion", DBNull.Value);
                                    break;
                                case "EXTRA":
                                    cmdInsertar.Parameters.AddWithValue("@idPlato", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idBebida", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idExtra", idItem);
                                    cmdInsertar.Parameters.AddWithValue("@idPromocion", DBNull.Value);
                                    break;
                                case "PROMOCION":
                                    cmdInsertar.Parameters.AddWithValue("@idPlato", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idBebida", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idExtra", DBNull.Value);
                                    cmdInsertar.Parameters.AddWithValue("@idPromocion", idItem);
                                    break;
                            }

                            resultadoDB = cmdInsertar.ExecuteNonQuery() > 0;
                        }
                    }

                    // Si se insertó correctamente en la BD, actualizar la interfaz
                    if (resultadoDB)
                    {
                        CargarPedidosDesdeBD();
                        ActualizarTotal();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar pedido a la base de datos: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return resultadoDB;
        }


        // Métodos auxiliares necesarios
        private string ObtenerNombreItem(int idItem, string tipoItem)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = "";
                switch (tipoItem.ToUpper())
                {
                    case "PLATO":
                        query = "SELECT nombrePlato FROM platos WHERE id_plato = @idItem";
                        break;
                    case "BEBIDA":
                        query = @"SELECT i.nombreProducto FROM bebidas b 
                         JOIN inventario i ON b.id_inventario = i.id_inventario
                         WHERE b.id_bebida = @idItem";
                        break;
                    case "EXTRA":
                        query = @"SELECT i.nombreProducto FROM extras e 
                         JOIN inventario i ON e.id_inventario = i.id_inventario
                         WHERE e.id_extra = @idItem";
                        break;
                    case "PROMOCION":
                        query = "SELECT nombre FROM promociones WHERE id_promocion = @idItem";
                        break;
                    default:
                        return "Ítem desconocido";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idItem", idItem);
                    return cmd.ExecuteScalar()?.ToString() ?? "Ítem desconocido";
                }
            }
        }

        private decimal ObtenerPrecioItem(int idItem, string tipoItem)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = "";
                switch (tipoItem.ToUpper())
                {
                    case "PLATO":
                        query = "SELECT precioUnitario FROM platos WHERE id_plato = @idItem";
                        break;
                    case "BEBIDA":
                        query = "SELECT precioUnitario FROM bebidas WHERE id_bebida = @idItem";
                        break;
                    case "EXTRA":
                        query = "SELECT precioUnitario FROM extras WHERE id_extra = @idItem";
                        break;
                    case "PROMOCION":
                        query = "SELECT precio_especial FROM promociones WHERE id_promocion = @idItem";
                        break;
                    default:
                        return 0;
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idItem", idItem);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        private string VerificarInventarioItem(int idItem, string tipoItem)
        {
            switch (tipoItem.ToUpper())
            {
                case "PLATO":
                    return OrdenesD.VerificarInventarioPlato(idItem);
                case "BEBIDA":
                    return OrdenesD.VerificarInventarioBebida(idItem);
                case "EXTRA":
                    return OrdenesD.VerificarInventarioExtra(idItem);
                case "PROMOCION":
                    return OrdenesD.VerificarInventarioPromocion(idItem);
                default:
                    return "Tipo de ítem no válido";
            }
        }

        private void MostrarPedidosEnPanel()
        {
            flowLayoutPanelPedidos.Controls.Clear();

            // Agrupar por id_pedido para evitar duplicados
            var pedidosAgrupados = pedidos
                .GroupBy(p => p.idPedido)
                .Select(g => g.First()); // Tomar el primer elemento de cada grupo

            foreach (var pedido in pedidosAgrupados)
            {
                Panel panelPedido = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = GetColorPorTipo(pedido.tipo),
                    Margin = new Padding(5),
                    Padding = new Padding(5),
                    Width = flowLayoutPanelPedidos.Width - 25,
                    Height = 40,
                    Tag = pedido.idPedido
                };

                Label lblPedido = new Label
                {
                    Text = $"{pedido.nombre} - {pedido.precio:C} x{pedido.cantidad}",
                    ForeColor = Color.White,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };

                Button btnEliminar = new Button
                {
                    Text = "X",
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(220, 80, 80),
                    FlatStyle = FlatStyle.Flat,
                    Width = 30,
                    Dock = DockStyle.Right,
                    Tag = pedido.idPedido
                };

                // Eventos
                lblPedido.Click += (s, e) => MostrarConfirmacionEliminacion(pedido.idPedido, pedido.nombre, pedido.cantidad);
                btnEliminar.Click += (s, e) => MostrarConfirmacionEliminacion(pedido.idPedido, pedido.nombre, pedido.cantidad);
                panelPedido.Click += (s, e) => MostrarConfirmacionEliminacion(pedido.idPedido, pedido.nombre, pedido.cantidad);

                panelPedido.Controls.Add(lblPedido);
                panelPedido.Controls.Add(btnEliminar);
                flowLayoutPanelPedidos.Controls.Add(panelPedido);
            }
        }

        private Color GetColorPorTipo(string tipo)
        {
            // Asignar colores según el tipo de ítem
            switch (tipo?.ToUpper())
            {
                case "PLATO":
                    return Color.FromArgb(70, 130, 180);  // Azul
                case "BEBIDA":
                    return Color.FromArgb(50, 205, 50);   // Verde
                case "EXTRA":
                    return Color.FromArgb(255, 165, 0);    // Naranja
                case "PROMOCION":
                    return Color.FromArgb(147, 112, 219); // Morado
                default:
                    return Color.DarkGray;                // Gris para tipos desconocidos
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
                        // Primero obtener información del pedido para actualizar inventario si es necesario
                        var infoPedido = ObtenerInfoPedido(idPedido, conexion, transaction);

                        // Eliminar el pedido
                        string queryDelete = "DELETE FROM pedidos WHERE id_pedido = @idPedido";
                        using (MySqlCommand cmdDelete = new MySqlCommand(queryDelete, conexion, transaction))
                        {
                            cmdDelete.Parameters.AddWithValue("@idPedido", idPedido);
                            int affectedRows = cmdDelete.ExecuteNonQuery();

                            if (affectedRows == 0)
                            {
                                transaction.Rollback();
                                MessageBox.Show("No se encontró el pedido a eliminar");
                                return;
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error al eliminar el pedido: " + ex.Message);
                    }
                }
            }
        }

        private (int? idPlato, int? idBebida, int? idExtra, int? idPromocion, int cantidad) ObtenerInfoPedido(int idPedido, MySqlConnection conexion, MySqlTransaction transaction)
        {
            string query = @"SELECT id_plato, id_bebida, id_extra, id_promocion, Cantidad 
                    FROM pedidos 
                    WHERE id_pedido = @idPedido";

            using (MySqlCommand cmd = new MySqlCommand(query, conexion, transaction))
            {
                cmd.Parameters.AddWithValue("@idPedido", idPedido);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int? idPlato = reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0);
                        int? idBebida = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1);
                        int? idExtra = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                        int? idPromocion = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                        int cantidad = reader.GetInt32(4);

                        return (idPlato, idBebida, idExtra, idPromocion, cantidad);
                    }
                }
            }

            return (null, null, null, null, 0);
        }


        private void ActualizarTotal()
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            IFNULL(SUM(
                CASE 
                    WHEN p.id_plato IS NOT NULL THEN (SELECT pl.precioUnitario FROM platos pl WHERE pl.id_plato = p.id_plato) * p.Cantidad
                    WHEN p.id_bebida IS NOT NULL THEN (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida) * p.Cantidad
                    WHEN p.id_extra IS NOT NULL THEN (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra) * p.Cantidad
                    WHEN p.id_promocion IS NOT NULL THEN (SELECT pr.precio_especial FROM promociones pr WHERE pr.id_promocion = p.id_promocion) * p.Cantidad
                    ELSE 0
                END
            ), 0) AS Total
        FROM pedidos p
        WHERE p.id_orden = @idOrden";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
                    object result = cmd.ExecuteScalar();

                    decimal total = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    lblTotal.Text = total.ToString("C");

                    // Actualizar el total en la base de datos
                    string updateQuery = "UPDATE ordenes SET total = @total WHERE id_orden = @idOrden";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conexion))
                    {
                        updateCmd.Parameters.AddWithValue("@total", total);
                        updateCmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
                        updateCmd.ExecuteNonQuery();
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
                                        (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = p.id_extra),
                                        (SELECT pr.nombre FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                                    ) AS nombre,
                                    COALESCE(
                                        pl.precioUnitario,
                                        (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                                        (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra),
                                        (SELECT pr.precio_especial FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                                    ) AS precio,
                                    p.Cantidad,
                                    CASE
                                        WHEN p.id_plato IS NOT NULL THEN 'PLATO'
                                        WHEN p.id_bebida IS NOT NULL THEN 'BEBIDA'
                                        WHEN p.id_extra IS NOT NULL THEN 'EXTRA'
                                        WHEN p.id_promocion IS NOT NULL THEN 'PROMOCION'
                                        ELSE 'DESCONOCIDO'
                                    END AS tipo
                                FROM pedidos p
                                LEFT JOIN platos pl ON p.id_plato = pl.id_plato
                                WHERE p.id_orden = @idOrden";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipo = reader["tipo"].ToString();
                            int idPedido = Convert.ToInt32(reader["id_pedido"]);
                            string nombre = reader["nombre"].ToString();
                            decimal precio = Convert.ToDecimal(reader["precio"]);
                            int cantidad = Convert.ToInt32(reader["Cantidad"]);

                            // Agregar a la lista con la cantidad correcta
                            pedidos.Add((idPedido, nombre, precio, tipo, cantidad));
                        }
                    }
                }
            }

            MostrarPedidosEnPanel();
        }

        private void flowLayoutPanelPedidos_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void comboBoxMesas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evitar ejecución durante inicialización
            if (comboBoxMesas.SelectedValue == null ||
                !(comboBoxMesas.SelectedValue is int) ||
                comboBoxMesas.SelectedIndex == -1)
            {
                return;
            }

            int nuevaMesaId;
            try
            {
                // Conversión segura
                nuevaMesaId = (int)comboBoxMesas.SelectedValue;
            }
            catch (InvalidCastException)
            {
                // Manejar caso donde el valor no es int
                MessageBox.Show("Error: El ID de mesa no es válido");
                comboBoxMesas.SelectedValue = idMesaActual;
                return;
            }

            // Si es la misma mesa, no hacer nada
            if (nuevaMesaId == idMesaActual)
                return;

            // Confirmar cambio
            var confirmacion = MessageBox.Show($"¿Cambiar a {comboBoxMesas.Text}?",
                                             "Confirmar cambio de mesa",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
            {
                comboBoxMesas.SelectedValue = idMesaActual;
                return;
            }

            try
            {
                bool exito = await OrdenesD.CambiarMesaOrdenAsync(
                    Convert.ToInt32(lblIdOrden.Text),
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
                    comboBoxMesas.SelectedValue = idMesaActual;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar mesa: {ex.Message}");
                comboBoxMesas.SelectedValue = idMesaActual;
            }
        }

        private void btnPrecuenta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblIdOrden.Text) || !int.TryParse(lblIdOrden.Text, out int idOrden))
            {
                MessageBox.Show("No hay una orden seleccionada válida");
                return;
            }

            FormAdminPrecuenta precuentaForm = new FormAdminPrecuenta(idOrden);
            precuentaForm.ShowDialog();
        }

        public class RoundedControl
        {
            public static void ApplyRoundedCorners(Control control, int radius)
            {
             
            }
        }
        private void FormGestionOrdenes_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificaciones más robustas
                if (string.IsNullOrEmpty(lblIdOrden.Text) || !int.TryParse(lblIdOrden.Text, out int ordenId))
                {
                    MessageBox.Show("La orden no es válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (pedidos.Count == 0)
                {
                    MessageBox.Show("La orden no tiene productos agregados", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar inventario con manejo de errores
                try
                {
                    if (!VerificarInventarioSuficiente())
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al verificar inventario: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idUsuarioCreador = ObtenerUsuarioCreadorOrden(Convert.ToInt32(lblIdOrden.Text));

                // Crear formulario de pago con el usuario creador
                using (var formPago = new FormAdminPagos(
                    ordenId,
                    lblNombreCliente.Text,
                    totalOrden,
                    idMesaActual,
                    idUsuarioCreador)) // Pasamos el usuario creador en lugar de SesionUsuario.IdUsuario
                {
                    var result = formPago.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para obtener el usuario creador de la orden
        private int ObtenerUsuarioCreadorOrden(int idOrden)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = "SELECT id_usuario FROM ordenes WHERE id_orden = @idOrden";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        private bool VerificarInventarioSuficiente()
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    // Desactivar ONLY_FULL_GROUP_BY para esta sesión
                    using (MySqlCommand setModeCmd = new MySqlCommand("SET SESSION sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY',''))", conexion))
                    {
                        setModeCmd.ExecuteNonQuery();
                    }

                    string query = @"
            SELECT 
                i.nombreProducto,
                i.cantidad AS stock_actual,
                CASE
                    WHEN p.id_plato IS NOT NULL THEN SUM(r.cantidad_necesaria * p.Cantidad)
                    WHEN p.id_bebida IS NOT NULL THEN SUM(p.Cantidad)
                    WHEN p.id_extra IS NOT NULL THEN SUM(p.Cantidad)
                END AS cantidad_necesaria
            FROM pedidos p
            LEFT JOIN platos pl ON p.id_plato = pl.id_plato
            LEFT JOIN recetas r ON pl.id_plato = r.id_plato
            LEFT JOIN bebidas b ON p.id_bebida = b.id_bebida
            LEFT JOIN extras e ON p.id_extra = e.id_extra
            LEFT JOIN inventario i ON 
                (r.id_inventario = i.id_inventario OR 
                 b.id_inventario = i.id_inventario OR 
                 e.id_inventario = i.id_inventario)
            WHERE p.id_orden = @idOrden
            GROUP BY i.nombreProducto, i.cantidad
            HAVING i.cantidad < cantidad_necesaria";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            StringBuilder mensaje = new StringBuilder();
                            mensaje.AppendLine("No hay suficiente inventario para:");
                            foreach (DataRow row in dt.Rows)
                            {
                                mensaje.AppendLine($"- {row["nombreProducto"]} (Stock: {row["stock_actual"]}, Necesario: {row["cantidad_necesaria"]})");
                            }
                            MessageBox.Show(mensaje.ToString(), "Inventario Insuficiente",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar inventario: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnPromociones_Click(object sender, EventArgs e)
        {
            CargarPromociones();
        }

        private void CargarPromociones()
        {
            try
            {
                dataGridViewMenu.DataSource = OrdenesD.ObtenerPromocionesActivas();
                dataGridViewMenu.Tag = "PROMOCION"; // Establecer el tipo actual
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar promociones: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}