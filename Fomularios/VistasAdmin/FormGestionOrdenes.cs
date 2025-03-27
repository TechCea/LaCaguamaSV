using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormGestionOrdenes: Form
    {
        public FormGestionOrdenes(int idOrden, string nombreCliente, decimal total, decimal descuento, string fechaOrden, string numeroMesa, string tipoPago, string nombreUsuario, string estadoOrden)
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
            dataGridViewMenu.DataSource = OrdenesD.ObtenerBebidas();
            tipoItemActual = "bebida";
        }

        private void CargarPlatos()
        {
            dataGridViewMenu.DataSource = OrdenesD.ObtenerPlatos();
            tipoItemActual = "plato";
        }

        private void CargarExtras()
        {
            dataGridViewMenu.DataSource = OrdenesD.ObtenerExtras();
            tipoItemActual = "extra";
        }


        private void CargarDatosOrden(int idOrden, string nombreCliente, decimal total, decimal descuento, string fechaOrden, string numeroMesa, string tipoPago, string nombreUsuario, string estadoOrden)
        {
            lblIdOrden.Text = idOrden.ToString();
            lblNombreCliente.Text = nombreCliente;
            lblTotal.Text = total.ToString("C");
            lblDescuento.Text = descuento.ToString("C");
            lblFechaOrden.Text = fechaOrden;
            lblNumeroMesa.Text = numeroMesa;
            lblTipoPago.Text = tipoPago;
            lblNombreUsuario.Text = nombreUsuario;
            lblEstadoOrden.Text = estadoOrden;
        }

        private void FormGestionOrdenes_Load(object sender, EventArgs e)
        {

        }

        private void lblNombreUsuario_Click(object sender, EventArgs e)
        {

        }

        private void lblNumeroMesa_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewMenu.Rows[e.RowIndex];

                int idItem = Convert.ToInt32(row.Cells["ID"].Value);
                string nombre = row.Cells["nombre"].Value.ToString();
                decimal precio = Convert.ToDecimal(row.Cells["precioUnitario"].Value);

                // Intentar agregar a la base de datos
                bool agregado = AgregarPedido(idItem, 1); // Se asume cantidad = 1, puedes cambiarlo

                if (agregado)
                {
                    pedidos.Add((idItem, nombre, precio));
                    totalOrden += precio;
                    MostrarPedidosEnPanel();
                    ActualizarTotal();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el pedido a la base de datos.");
                }
            }
        }

        private bool AgregarPedido(int idItem, int cantidad)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string query = "INSERT INTO pedidos (id_orden, id_estadoP, id_plato, id_bebida, id_extra, Cantidad) " +
                                   "VALUES (@idOrden, 1, @idPlato, @idBebida, @idExtra, @Cantidad)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
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
            flowLayoutPanelPedidos.Controls.Clear();

            // Agrupar pedidos iguales
            var pedidosAgrupados = pedidos
                .GroupBy(p => p.idPedido)
                .Select(g => new {
                    IdPedido = g.Key,
                    Nombre = g.First().nombre,
                    Precio = g.First().precio,
                    Cantidad = g.Count()
                });

            foreach (var grupo in pedidosAgrupados)
            {
                // Crear un Panel contenedor para cada pedido
                Panel panelPedido = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.DarkGreen,
                    Margin = new Padding(5),
                    Padding = new Padding(5),
                    AutoSize = true,
                    Tag = grupo.IdPedido
                };

                // Label con la información del pedido
                Label lblPedido = new Label
                {
                    Text = $"{grupo.Nombre} - {grupo.Precio:C} x{grupo.Cantidad}",
                    AutoSize = true,
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand,
                    Dock = DockStyle.Fill
                };

                // Botón para eliminar
                Button btnEliminar = new Button
                {
                    Text = "X",
                    ForeColor = Color.Red,
                    BackColor = Color.Red,
                    FlatStyle = FlatStyle.Flat,
                    Width = 15,
                    Height = 10,
                    Dock = DockStyle.Right,
                    Cursor = Cursors.Hand,
                    Tag = grupo.IdPedido
                };

                // Configurar el evento Click para el label y el botón
                lblPedido.Click += (sender, e) => MostrarConfirmacionEliminacion(grupo.IdPedido, grupo.Nombre, grupo.Cantidad);
                btnEliminar.Click += (sender, e) => MostrarConfirmacionEliminacion(grupo.IdPedido, grupo.Nombre, grupo.Cantidad);

                // Agregar controles al panel
                panelPedido.Controls.Add(lblPedido);
                panelPedido.Controls.Add(btnEliminar);

                // Agregar el panel al flowLayoutPanel
                flowLayoutPanelPedidos.Controls.Add(panelPedido);
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
                    // Calcular el total REAL consultando todos los pedidos
                    string queryTotal = @"
                SELECT 
                    IFNULL(SUM(
                        CASE 
                            WHEN p.id_plato IS NOT NULL THEN pl.precioUnitario * p.Cantidad
                            WHEN p.id_bebida IS NOT NULL THEN b.precioUnitario * p.Cantidad
                            WHEN p.id_extra IS NOT NULL THEN e.precioUnitario * p.Cantidad
                            ELSE 0
                        END
                    ), 0) AS Total
                FROM pedidos p
                LEFT JOIN platos pl ON p.id_plato = pl.id_plato
                LEFT JOIN bebidas b ON p.id_bebida = b.id_bebida
                LEFT JOIN extras e ON p.id_extra = e.id_extra
                WHERE p.id_orden = @idOrden";

                    decimal nuevoTotal = 0;

                    using (MySqlCommand cmdTotal = new MySqlCommand(queryTotal, conexion))
                    {
                        cmdTotal.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
                        object result = cmdTotal.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            nuevoTotal = Convert.ToDecimal(result);
                        }
                    }

                    // Actualizar el total en la orden
                    string queryUpdate = "UPDATE ordenes SET total = @total WHERE id_orden = @idOrden";
                    using (MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, conexion))
                    {
                        cmdUpdate.Parameters.AddWithValue("@total", nuevoTotal);
                        cmdUpdate.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // Actualizar la UI
                    totalOrden = nuevoTotal;
                    lblTotal.Text = totalOrden.ToString("C");

                    // Recargar los pedidos desde la BD para mantener consistencia
                    CargarPedidosDesdeBD();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el total: " + ex.Message);
                }
                // Notificar al FormAdmin para que actualice la lista de órdenes
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
            COALESCE(pl.id_plato, b.id_bebida, e.id_extra) AS id_item,
            COALESCE(pl.nombrePlato, i.nombreBebida, e.nombre) AS nombre,
            COALESCE(pl.precioUnitario, b.precioUnitario, e.precioUnitario) AS precio,
            p.Cantidad
        FROM pedidos p
        LEFT JOIN platos pl ON p.id_plato = pl.id_plato
        LEFT JOIN bebidas b ON p.id_bebida = b.id_bebida
        LEFT JOIN inventario i ON b.id_inventario = i.id_inventario
        LEFT JOIN extras e ON p.id_extra = e.id_extra
        WHERE p.id_orden = @idOrden";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", Convert.ToInt32(lblIdOrden.Text));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPedido = reader.GetInt32("id_pedido");
                            string nombre = reader.GetString("nombre");
                            decimal precio = reader.GetDecimal("precio");
                            int cantidad = reader.GetInt32("Cantidad");

                            // Agregamos cada item con su ID de pedido
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

        private void flowLayoutPanelPedidos_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}
