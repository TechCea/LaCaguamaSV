using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using System.IO.Ports;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;


namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormGestionOrdenes : Form
    {
     

        private int idMesaActual; // Almacenar el ID de la mesa actual
        // Constantes ESC/POS
        private const string ESC = "\x1B";
        private const string GS = "\x1D";
        private const string LF = "\x0A";
        private bool comandaImpresa = false; // Variable de control para evitar impresión duplicada
        private Dictionary<int, string> notasPedidos = new Dictionary<int, string>();
        public FormGestionOrdenes(int idOrden, string nombreCliente, decimal total, decimal descuento, string fechaOrden, string numeroMesa, string tipoPago, string nombreUsuario, string estadoOrden)
        {
            InitializeComponent();



            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar
            
            this.Size = new Size(1000, 500); // Establece un tamaño fijo

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;
            lblTipoPago.Text = tipoPago; // Directamente usar el string recibido

            CargarDatosOrden(idOrden, nombreCliente, total, descuento, fechaOrden, numeroMesa, tipoPago, nombreUsuario, estadoOrden);
            CargarPedidosDesdeBD(); // Cargar pedidos existentes al iniciar
            ActualizarTotal(); // Sincronizar el total
            dataGridViewMenu.MultiSelect = false;

            dataGridViewMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMenu.MultiSelect = false;
            dataGridViewMenu.ReadOnly = true;
            dataGridViewMenu.AllowUserToAddRows = false;
            dataGridViewMenu.AllowUserToDeleteRows = false;
            dataGridViewMenu.AllowUserToResizeRows = false;

            dataGridViewMenu.CellDoubleClick += dataGridViewMenu_CellDoubleClick;
            btnImprimirComanda.Click += btnImprimirComanda_Click;

            lblTotal.Font = new Font(lblTotal.Font.FontFamily, 16);
            label5.Font = new Font(label5.Font.FontFamily, 14);
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

            // Mostrar formulario de cantidad con notas solo para platos
            var cantidadForm = new Form()
            {
                Text = $"Agregar {nombre}",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(350, tipoActual == "PLATO" ? 275 : 250),
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };

            var panel = new Panel() { Dock = DockStyle.Fill, Padding = new Padding(20) };

            // Etiqueta y selector de cantidad
            var lbl = new Label()
            {
                Text = $"Cantidad de {nombre}:",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

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

            // Campo de notas solo para platos
            TextBox txtNotas = null;
            if (tipoActual == "PLATO")
            {
                var lblNotas = new Label()
                {
                    Text = "Notas especiales:",
                    Dock = DockStyle.Top,
                    Margin = new Padding(0, 10, 0, 0),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                txtNotas = new TextBox()
                {
                    Dock = DockStyle.Top,
                    Multiline = true,
                    Height = 60,
                    MaxLength = 100
                };

                panel.Controls.Add(lblNotas);
                panel.Controls.Add(txtNotas);
            }

            // Panel de botones
            var panelBotones = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(5)
            };

            var btnAceptar = new Button()
            {
                Text = "Agregar",
                DialogResult = DialogResult.OK,
                Width = 100,
                Dock = DockStyle.Right,
                BackColor = Color.FromArgb(70, 130, 180),
                FlatStyle = FlatStyle.Flat
            };

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

            panelBotones.Controls.AddRange(new[] { btnAceptar, btnCancelar });
            panel.Controls.AddRange(new Control[] { lbl, numCantidad, panelBotones });
            if (tipoActual == "PLATO") panel.Controls.Add(txtNotas);
            cantidadForm.Controls.Add(panel);

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
                    resultado = AgregarPedido(idItem, cantidad, tipoActual, txtNotas?.Text);
                }

                if (resultado)
                {
                    CargarPedidosDesdeBD();
                    ActualizarTotal();
                }
            }
        }

        // Método auxiliar para obtener el último ID insertado
        private int ObtenerUltimoIdPedido(MySqlConnection conexion)
        {
            string query = "SELECT LAST_INSERT_ID()";
            using (MySqlCommand cmd = new MySqlCommand(query, conexion))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private bool AgregarPedido(int idItem, int cantidad, string tipoItem, string nota = null)
        {
            if (idItem <= 0 || cantidad <= 0 || string.IsNullOrEmpty(tipoItem))
            {
                MessageBox.Show("Datos inválidos para agregar el pedido", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        bool crearNuevoPedido = true;

                        // Solo verificar pedidos existentes si es un plato SIN nota
                        if (tipoItem.ToUpper() == "PLATO" && string.IsNullOrWhiteSpace(nota))
                        {
                            // Primero obtenemos todos los pedidos sin nota en una lista
                            List<(int idPedido, int cantidad)> pedidosSinNota = new List<(int, int)>();

                            string queryVerificar = @"SELECT p.id_pedido, p.Cantidad 
                        FROM pedidos p
                        LEFT JOIN notas_pedidos np ON p.id_pedido = np.id_pedido
                        WHERE p.id_orden = @idOrden AND 
                              p.id_plato = @idPlato AND
                              (np.nota IS NULL OR np.nota = '')";

                            using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conexion, transaction))
                            {
                                cmdVerificar.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
                                cmdVerificar.Parameters.AddWithValue("@idPlato", idItem);

                                using (var reader = cmdVerificar.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        pedidosSinNota.Add((
                                            reader.GetInt32("id_pedido"),
                                            reader.GetInt32("Cantidad")
                                        ));
                                    }
                                } // El reader se cierra automáticamente aquí
                            }

                            // Si encontramos pedidos sin nota, actualizamos el primero
                            if (pedidosSinNota.Count > 0)
                            {
                                var primerPedido = pedidosSinNota[0];

                                string queryActualizar = @"UPDATE pedidos 
                                SET Cantidad = @nuevaCantidad 
                                WHERE id_pedido = @idPedido";

                                using (MySqlCommand cmdActualizar = new MySqlCommand(queryActualizar, conexion, transaction))
                                {
                                    cmdActualizar.Parameters.AddWithValue("@idPedido", primerPedido.idPedido);
                                    cmdActualizar.Parameters.AddWithValue("@nuevaCantidad", primerPedido.cantidad + cantidad);
                                    cmdActualizar.ExecuteNonQuery();
                                }

                                crearNuevoPedido = false;
                            }
                        }

                        if (crearNuevoPedido)
                        {
                            string queryInsertar = @"INSERT INTO pedidos 
                           (id_orden, id_estadoP, id_plato, id_bebida, id_extra, id_promocion, Cantidad) 
                           VALUES (@idOrden, 1, @idPlato, @idBebida, @idExtra, @idPromocion, @cantidad);
                           SELECT LAST_INSERT_ID();";

                            using (MySqlCommand cmdInsertar = new MySqlCommand(queryInsertar, conexion, transaction))
                            {
                                cmdInsertar.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
                                cmdInsertar.Parameters.AddWithValue("@cantidad", cantidad);

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
                                }

                                int idPedidoInsertado = Convert.ToInt32(cmdInsertar.ExecuteScalar());

                                if (!string.IsNullOrWhiteSpace(nota))
                                {
                                    OrdenesD.GuardarNotaPedido(idPedidoInsertado, nota, transaction);
                                }
                            }
                        }

                        transaction.Commit();
                        CargarPedidosDesdeBD();
                        ActualizarTotal();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error al agregar pedido: {ex.Message}");
                        return false;
                    }
                }
            }
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
            flowLayoutPanelPedidos.SuspendLayout();

            try
            {
                var pedidosAgrupados = pedidos
                    .GroupBy(p => new {
                        Tipo = p.tipo,
                        Nombre = p.nombre,
                        Nota = notasPedidos.ContainsKey(p.idPedido) ? notasPedidos[p.idPedido] : null
                    })
                    .Select(g => new {
                        IdPedido = g.First().idPedido,
                        Nombre = g.Key.Nombre,
                        Precio = g.First().precio,
                        Cantidad = g.Sum(x => x.cantidad),
                        Tipo = g.Key.Tipo,
                        Nota = g.Key.Nota
                    })
                    .OrderBy(p => p.Tipo);

                foreach (var pedido in pedidosAgrupados)
                {
                    // Obtener color base según el tipo
                    Color colorBase = GetColorPorTipo(pedido.Tipo);

                    // Aclarar el color si tiene nota
                    Color colorFinal = !string.IsNullOrEmpty(pedido.Nota)
                        ? Color.FromArgb(
                            Math.Min(255, colorBase.R + 40),
                            Math.Min(255, colorBase.G + 40),
                            Math.Min(255, colorBase.B + 40))
                        : colorBase;

                    Panel panelPedido = new Panel
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = colorFinal,
                        Margin = new Padding(5),
                        Padding = new Padding(5),
                        Width = flowLayoutPanelPedidos.ClientSize.Width - 25,
                        Height = pedido.Nota != null ? 90 : 50,
                        Tag = pedido.IdPedido,
                        Cursor = Cursors.Hand
                    };

                    // Resto del código permanece igual...
                    Label lblPedido = new Label
                    {
                        Text = $"{pedido.Nombre}\n{pedido.Precio:C} x{pedido.Cantidad}",
                        ForeColor = !string.IsNullOrEmpty(pedido.Nota) ? Color.Black : Color.White,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Arial", 9, FontStyle.Bold),
                        Padding = new Padding(5),
                        AutoEllipsis = true
                    };

                    if (pedido.Nota != null)
                    {
                        lblPedido.Text += $"\nNota: {pedido.Nota}";
                    }

                    // Resto de la implementación...
                    Button btnEliminar = new Button
                    {
                        Text = "X",
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(220, 80, 80),
                        FlatStyle = FlatStyle.Flat,
                        Width = 30,
                        Height = 30,
                        Dock = DockStyle.Right,
                        Tag = pedido.IdPedido,
                        Margin = new Padding(3),
                        Cursor = Cursors.Hand
                    };

                    Button btnEditarNota = new Button
                    {
                        Text = "✏️",
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(70, 130, 180),
                        FlatStyle = FlatStyle.Flat,
                        Width = 30,
                        Height = 30,
                        Dock = DockStyle.Right,
                        Tag = pedido.IdPedido,
                        Margin = new Padding(3),
                        Cursor = Cursors.Hand,
                        Visible = pedido.Tipo == "PLATO"
                    };

                    // Eventos y agregado de controles...
                    lblPedido.Click += (s, e) => MostrarOpcionesPedido(pedido.IdPedido, pedido.Nombre, pedido.Cantidad);
                    btnEliminar.Click += (s, e) => MostrarOpcionesPedido(pedido.IdPedido, pedido.Nombre, pedido.Cantidad);
                    btnEditarNota.Click += (s, e) => EditarNotaPedido(pedido.IdPedido);
                    panelPedido.Click += (s, e) => MostrarOpcionesPedido(pedido.IdPedido, pedido.Nombre, pedido.Cantidad);

                    panelPedido.Controls.Add(lblPedido);
                    if (pedido.Tipo == "PLATO") panelPedido.Controls.Add(btnEditarNota);
                    panelPedido.Controls.Add(btnEliminar);

                    flowLayoutPanelPedidos.Controls.Add(panelPedido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar pedidos: {ex.Message}");
            }
            finally
            {
                flowLayoutPanelPedidos.ResumeLayout();
            }
        }

        private void MostrarOpcionesPedido(int idPedido, string nombre, int cantidad)
        {
            var menu = new ContextMenuStrip();

            var itemEliminar = new ToolStripMenuItem("Eliminar");
            itemEliminar.Click += (s, e) => {
                if (MessageBox.Show($"¿Eliminar {nombre} (x{cantidad})?", "Confirmar",
                                  MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    EliminarPedidoDeBD(idPedido);
                }
            };
            menu.Items.Add(itemEliminar);

            // Solo mostrar opción de editar nota si es un plato con nota
            if (notasPedidos.ContainsKey(idPedido))
            {
                var itemEditarNota = new ToolStripMenuItem("Editar Nota");
                itemEditarNota.Click += (s, e) => EditarNotaPedido(idPedido);
                menu.Items.Add(itemEditarNota);
            }

            menu.Show(Cursor.Position);
        }

        private void EditarNotaPedido(int idPedido)
        {
            string notaActual = OrdenesD.ObtenerNotaPedido(idPedido);

            var formNota = new Form()
            {
                Text = "Editar Nota",
                Size = new Size(350, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            var txtNota = new TextBox()
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                Text = notaActual,
                ScrollBars = ScrollBars.Vertical
            };

            var panelBotones = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                Padding = new Padding(5)
            };

            var btnGuardar = new Button()
            {
                Text = "Guardar",
                DialogResult = DialogResult.OK,
                Width = 100,
                Dock = DockStyle.Right
            };

            var btnEliminarNota = new Button()
            {
                Text = "Eliminar Nota",
                DialogResult = DialogResult.OK,
                Width = 100,
                Dock = DockStyle.Right,
                Margin = new Padding(0, 0, 10, 0),
                Visible = !string.IsNullOrEmpty(notaActual)
            };

            var btnCancelar = new Button()
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Width = 100,
                Dock = DockStyle.Left
            };

            btnGuardar.Click += (s, e) =>
            {
                if (OrdenesD.GuardarNotaPedido(idPedido, txtNota.Text))
                {
                    CargarPedidosDesdeBD();
                    formNota.Close();
                }
            };

            btnEliminarNota.Click += (s, e) =>
            {
                if (MessageBox.Show("¿Eliminar esta nota?", "Confirmar",
                                  MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (OrdenesD.GuardarNotaPedido(idPedido, null)) // Pasar null para eliminar
                    {
                        CargarPedidosDesdeBD();
                        formNota.Close();
                    }
                }
            };

            panelBotones.Controls.AddRange(new[] { btnCancelar, btnEliminarNota, btnGuardar });
            formNota.Controls.Add(txtNota);
            formNota.Controls.Add(panelBotones);

            formNota.AcceptButton = btnGuardar;
            formNota.CancelButton = btnCancelar;

            formNota.ShowDialog(this);
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

        private void EliminarPedidoDeBD(int idPedido)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        var infoPedido = ObtenerInfoPedido(idPedido, conexion, transaction);

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

                        // Eliminar también la nota asociada si existe
                        if (notasPedidos.ContainsKey(idPedido))
                        {
                            notasPedidos.Remove(idPedido);
                        }

                        transaction.Commit();
                        CargarPedidosDesdeBD();
                        ActualizarTotal();
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
            notasPedidos.Clear();

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                // Primero cargamos todos los pedidos
                string queryPedidos = @"SELECT 
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
                        p.Cantidad,
                        CASE
                            WHEN p.id_plato IS NOT NULL THEN 'PLATO'
                            WHEN p.id_bebida IS NOT NULL THEN 'BEBIDA'
                            WHEN p.id_extra IS NOT NULL THEN 'EXTRA'
                            ELSE 'DESCONOCIDO'
                        END AS tipo
                    FROM pedidos p
                    LEFT JOIN platos pl ON p.id_plato = pl.id_plato
                    WHERE p.id_orden = @idOrden";

                using (MySqlCommand cmdPedidos = new MySqlCommand(queryPedidos, conexion))
                {
                    cmdPedidos.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));

                    using (MySqlDataReader reader = cmdPedidos.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipo = reader["tipo"].ToString();
                            int idPedido = Convert.ToInt32(reader["id_pedido"]);
                            string nombre = reader["nombre"].ToString();
                            decimal precio = Convert.ToDecimal(reader["precio"]);
                            int cantidad = Convert.ToInt32(reader["Cantidad"]);

                            pedidos.Add((idPedido, nombre, precio, tipo, cantidad));
                        }
                    }
                }

                // Luego cargamos todas las notas en una consulta separada
                string queryNotas = @"SELECT id_pedido, nota FROM notas_pedidos 
                      WHERE id_pedido IN (
                          SELECT id_pedido FROM pedidos WHERE id_orden = @idOrden
                      )";

                using (MySqlCommand cmdNotas = new MySqlCommand(queryNotas, conexion))
                {
                    cmdNotas.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));

                    using (MySqlDataReader reader = cmdNotas.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPedido = reader.GetInt32("id_pedido");
                            string nota = reader.IsDBNull(1) ? string.Empty : reader.GetString("nota");
                            notasPedidos[idPedido] = nota;
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirComanda_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si ya se imprimió para evitar duplicados
                if (comandaImpresa)
                {
                    comandaImpresa = false; // Resetear para futuras impresiones
                    return;
                }

                // 1. Generar el contenido de la comanda con formato mejorado
                string contenidoComanda = GenerarContenidoComanda();

                // 2. Método de impresión por USB con manejo de errores
                ImprimirPorUSB(contenidoComanda);

                // Marcar como impresa antes de mostrar el mensaje
                comandaImpresa = true;

                // Mostrar mensaje con botón OK solamente (sin botones Sí/No)
                MessageBox.Show("Comanda enviada a la impresora", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                comandaImpresa = false; // Resetear en caso de error
                MessageBox.Show($"Error al imprimir comanda: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarContenidoComanda()
        {
            StringBuilder sb = new StringBuilder();

            // 1. Inicialización y encabezado con formato mejorado
            sb.Append(ESC + "@"); // Reset printer
            sb.Append(ESC + "!" + "\x18"); // Fuente tamaño mediano (no tan grande como el comprobante)
            sb.Append(CenterText("LA CAGUAMA RESTAURANTE"));
            sb.Append(LF);
            sb.Append(CenterText("══════════════════════"));
            sb.Append(LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente normal

            // 2. Información básica de la comanda
            sb.Append($"ORDEN: #{lblIdOrden.Text}{LF}");
            sb.Append($"MESA: {comboBoxMesas.Text}{LF}");
            sb.Append($"FECHA: {DateTime.Now.ToString("g")}{LF}");
            sb.Append($"CLIENTE: {lblNombreCliente.Text}{LF}");
            sb.Append("──────────────────────────" + LF);

            // 3. Detalle de productos (solo platos y extras)
            sb.Append(ESC + "!" + "\x08"); // Fuente enfatizada
            sb.Append("          COMANDA" + LF);
            sb.Append(ESC + "!" + "\x00"); // Restaurar fuente
            sb.Append("──────────────────────────" + LF);

            // Obtener detalles de los pedidos agrupados por tipo
            var platos = ObtenerItemsParaComanda("PLATO");
            var extras = ObtenerItemsParaComanda("EXTRA");

            // Sección de PLATOS
            if (platos.Any())
            {
                sb.Append(ESC + "!" + "\x08"); // Fuente enfatizada
                sb.Append("PLATOS:" + LF);
                sb.Append(ESC + "!" + "\x00"); // Restaurar fuente

                foreach (var plato in platos)
                {
                    sb.Append($"{plato.Cantidad}x {plato.Nombre.Replace("PLATO", "").Trim()}{LF}");
                    if (!string.IsNullOrWhiteSpace(plato.Notas))
                    {
                        sb.Append($"  Nota: {plato.Notas}{LF}");
                    }
                }
                sb.Append(LF);
            }

            // Sección de EXTRAS
            if (extras.Any())
            {
                sb.Append(ESC + "!" + "\x08"); // Fuente enfatizada
                sb.Append("EXTRAS:" + LF);
                sb.Append(ESC + "!" + "\x00"); // Restaurar fuente

                foreach (var extra in extras)
                {
                    sb.Append($"{extra.Cantidad}x {extra.Nombre.Replace("EXTRA", "").Trim()}{LF}");
                }
                sb.Append(LF);
            }

            // 4. Totales resumidos
            sb.Append("──────────────────────────" + LF);
            sb.Append($"TOTAL PLATOS: {platos.Sum(p => p.Cantidad)}{LF}");
            sb.Append($"TOTAL EXTRAS: {extras.Sum(e => e.Cantidad)}{LF}");
            sb.Append("──────────────────────────" + LF);

            // 5. Pie de página y cortes
            sb.Append(LF);
            sb.Append(CenterText("¡LISTO PARA PREPARAR!"));
            sb.Append(LF + LF);
            sb.Append(LF + LF + LF); // Espacios adicionales antes del corte
            sb.Append(GS + "V" + "\x41" + "\x00"); // Corte completo

            return sb.ToString();
        }

        private void ImprimirPorUSB(string contenido)
        {
            // Opción 1: Usando PrintDocument (recomendado para Windows)
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = ObtenerNombreImpresoraTermica();

            pd.PrintPage += (sender, e) =>
            {
                // Usar fuente monoespaciada para mejor alineación
                Font font = new Font("Courier New", 9);
                e.Graphics.DrawString(contenido, font, Brushes.Black,
                    new RectangleF(0, 0, pd.DefaultPageSettings.PrintableArea.Width,
                                  pd.DefaultPageSettings.PrintableArea.Height));
            };

            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                // Intentar con impresora predeterminada si falla
                try
                {
                    pd.PrinterSettings.PrinterName = new PrinterSettings().PrinterName;
                    pd.Print();
                }
                catch
                {
                    throw new Exception("No se pudo imprimir. Verifique la conexión con la impresora.");
                }
            }
        }

        private string CenterText(string text)
        {
            int maxWidth = 32; // Ajustar según tu impresora
            if (text.Length >= maxWidth) return text;

            int spaces = (maxWidth - text.Length) / 2;
            return new string(' ', spaces) + text;
        }

        private string ObtenerNombreImpresoraTermica()
        {
            // Busca la impresora por nombre
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.Contains("PT-210") || printer.Contains("GOOJPRT") || printer.Contains("POS") ||
                    printer.Contains("Receipt") || printer.Contains("Termica"))
                {
                    return printer;
                }
            }

            // Si no la encuentra, usa la predeterminada
            return new PrinterSettings().PrinterName;
        }

        private List<(string Nombre, int Cantidad, string Notas)> ObtenerItemsParaComanda(string tipoItem)
        {
            var items = new List<(string, int, string)>();

            string query = @"
SELECT 
    p.id_pedido,
    CASE
        WHEN p.id_plato IS NOT NULL THEN CONCAT('PLATO ', pl.nombrePlato)
        WHEN p.id_extra IS NOT NULL THEN CONCAT('EXTRA ', i.nombreProducto)
    END AS Nombre,
    p.Cantidad
FROM pedidos p
LEFT JOIN platos pl ON p.id_plato = pl.id_plato
LEFT JOIN extras e ON p.id_extra = e.id_extra
LEFT JOIN inventario i ON e.id_inventario = i.id_inventario
WHERE p.id_orden = @idOrden AND ";

            query += tipoItem == "PLATO" ? "p.id_plato IS NOT NULL" : "p.id_extra IS NOT NULL";

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["Nombre"].ToString();
                            int cantidad = Convert.ToInt32(reader["Cantidad"]);
                            int idPedido = Convert.ToInt32(reader["id_pedido"]);
                            string notas = notasPedidos.ContainsKey(idPedido) ? notasPedidos[idPedido] : "";

                            items.Add((nombre, cantidad, notas));
                        }
                    }
                }
            }

            return items;
        }

    }
}

