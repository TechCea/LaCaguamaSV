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
using Org.BouncyCastle.Asn1.Crmf;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormAdminPagos : Form
    {
        private int idOrden;
        private int idUsuarioCreador; // Cambiamos el nombre para claridad
        private decimal totalOrden;
        private int idMesa;
        private bool descuentoAplicado = false;
        private decimal montoDescuento = 0;
        private string tipoDescuento = "";
        private int idTipoDescuentoSeleccionado = 0;
        private DataTable tiposDescuentoDisponibles;

        public FormAdminPagos(int idOrden, string nombreCliente, decimal total, int idMesa, int idUsuarioCreador)
        {
            InitializeComponent();
            this.idOrden = idOrden;
            this.idUsuarioCreador = idUsuarioCreador; // Usamos el usuario creador
            this.totalOrden = total;
            this.idMesa = idMesa;

            // Configurar controles
            lblNumeroOrden.Text = $"Orden #: {idOrden}";
            lblCliente.Text = $"Cliente: {nombreCliente}";
            lblTotalP.Text = total.ToString("C");

            dataGridViewDetalle.RowHeadersVisible = false;

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;


            // Configurar controles
            lblNumeroOrden.Text = $"Orden #: {idOrden}";
            lblCliente.Text = $"Cliente: {nombreCliente}";
            lblTotalP.Text = total.ToString("C");

            CargarMetodosPago();
            CargarTiposDescuento();
            CargarDetalleOrden();
            ActualizarCamposPago();
        }

        private void CargarMetodosPago()
        {
            try
            {
                DataTable metodosPago = OrdenesD.ObtenerTiposPago();
                comboMetodoPago.DataSource = metodosPago;
                comboMetodoPago.DisplayMember = "nombrePago";
                comboMetodoPago.ValueMember = "nombrePago"; // Usar el nombre directamente

                // Seleccionar el método de pago actual de la orden
                string metodoPagoActual = ObtenerMetodoPagoActual(idOrden);
                if (!string.IsNullOrEmpty(metodoPagoActual))
                {
                    comboMetodoPago.SelectedValue = metodoPagoActual;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar métodos de pago: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ObtenerMetodoPagoActual(int idOrden)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"SELECT tp.nombrePago 
                            FROM ordenes o 
                            JOIN tipoPago tp ON o.tipo_pago = tp.id_pago 
                            WHERE o.id_orden = @idOrden";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);
                        return cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch
            {
                return null;
            }
        }


        private void CargarDetalleOrden()
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
            SELECT 
                COALESCE(
                    pl.nombrePlato,
                    (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = p.id_bebida),
                    (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = p.id_extra),
                    (SELECT pr.nombre FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                ) AS Producto,
                p.Cantidad,
                COALESCE(
                    pl.precioUnitario,
                    (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                    (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra),
                    (SELECT pr.precio_especial FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                ) AS PrecioUnitario,
                (p.Cantidad * COALESCE(
                    pl.precioUnitario,
                    (SELECT b.precioUnitario FROM bebidas b WHERE b.id_bebida = p.id_bebida),
                    (SELECT e.precioUnitario FROM extras e WHERE e.id_extra = p.id_extra),
                    (SELECT pr.precio_especial FROM promociones pr WHERE pr.id_promocion = p.id_promocion)
                )) AS Subtotal
            FROM pedidos p
            LEFT JOIN platos pl ON p.id_plato = pl.id_plato
            LEFT JOIN promociones pr ON p.id_promocion = pr.id_promocion
            WHERE p.id_orden = @idOrden";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);

                        dataGridViewDetalle.DataSource = dt;

                        // Recalcular el total basado en los datos actuales
                        decimal nuevoTotal = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            nuevoTotal += Convert.ToDecimal(row["Subtotal"]);
                        }

                        totalOrden = nuevoTotal;
                        lblTotalP.Text = totalOrden.ToString("C");

                        // Configurar columnas
                        dataGridViewDetalle.Columns["Producto"].HeaderText = "Producto";
                        dataGridViewDetalle.Columns["Cantidad"].HeaderText = "Cantidad";
                        dataGridViewDetalle.Columns["PrecioUnitario"].HeaderText = "Precio Unitario";
                        dataGridViewDetalle.Columns["Subtotal"].HeaderText = "Subtotal";

                        // Formato de columnas
                        dataGridViewDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C";
                        dataGridViewDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "C";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalle de orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Modifica el método ActualizarCamposPago para manejar mejor el txtRecibido
        private void ActualizarCamposPago()
        {
            try
            {
                if (comboMetodoPago.SelectedValue != null)
                {
                    string metodoSeleccionado = comboMetodoPago.SelectedValue.ToString();
                    panelEfectivo.Visible = (metodoSeleccionado == "Efectivo"); // Comparar con el string directamente

                    if (panelEfectivo.Visible)
                    {
                        // Solo sugerir monto si el campo está vacío
                        if (string.IsNullOrEmpty(txtRecibido.Text))
                        {
                            decimal totalActual = CalcularTotalConDescuento();
                            txtRecibido.Text = totalActual.ToString("0.00");
                        }
                        txtRecibido.Focus();
                    }
                    else
                    {
                        // Limpiar el campo si no es pago en efectivo
                        txtRecibido.Text = "";
                    }
                }
                else
                {
                    panelEfectivo.Visible = false;
                    txtRecibido.Text = "";
                }

                // Actualizar el cambio
                txtRecibido_TextChanged(txtRecibido, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                panelEfectivo.Visible = false;
                Console.WriteLine($"Error al actualizar campos de pago: {ex.Message}");
            }
        }

        private bool ProcesarPagoEnBD(string nombrePago, decimal recibido, decimal cambio)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // Obtener ID del método de pago
                        int idMetodoPago = ObtenerIdMetodoPago(nombrePago, conexion, transaction);
                        if (idMetodoPago == -1)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Método de pago no válido", "Error",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        // Calcular total con descuento
                        decimal totalConDescuento = CalcularTotalConDescuento();
                        bool esEfectivo = (nombrePago == "Efectivo");

                        // 1. Registrar el pago
                        string queryPago = @"
                    INSERT INTO pagos 
                        (id_orden, monto, recibido, cambio, id_usuario, id_tipo_pago, 
                         descuento, id_tipo_descuento) 
                    VALUES 
                        (@idOrden, @total, @recibido, @cambio, @idUsuarioCreador, @tipoPago, 
                         @descuento, @idTipoDescuento)";

                        using (MySqlCommand cmd = new MySqlCommand(queryPago, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.Parameters.AddWithValue("@total", totalConDescuento);
                            cmd.Parameters.AddWithValue("@recibido", esEfectivo ? recibido : totalConDescuento);
                            cmd.Parameters.AddWithValue("@cambio", cambio);
                            cmd.Parameters.AddWithValue("@idUsuarioCreador", idUsuarioCreador);
                            cmd.Parameters.AddWithValue("@tipoPago", idMetodoPago);
                            cmd.Parameters.AddWithValue("@descuento", descuentoAplicado ? montoDescuento : 0);
                            cmd.Parameters.AddWithValue("@idTipoDescuento", descuentoAplicado ? idTipoDescuentoSeleccionado : (object)DBNull.Value);

                            cmd.ExecuteNonQuery();
                        }

                        // 2. Actualizar la orden con el descuento
                        string queryUpdateOrden = @"
                    UPDATE ordenes 
                    SET 
                        tipo_pago = @tipoPago,
                        total = @total,
                        descuento = @descuento,
                        id_estadoO = 2 -- Estado 'Cerrada'
                    WHERE id_orden = @idOrden";

                        using (MySqlCommand cmd = new MySqlCommand(queryUpdateOrden, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@tipoPago", idMetodoPago);
                            cmd.Parameters.AddWithValue("@total", totalConDescuento);
                            cmd.Parameters.AddWithValue("@descuento", descuentoAplicado ? montoDescuento : 0);
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }

                        // 3. Actualizar estado de los pedidos a "Entregado"
                        string queryPedidos = "UPDATE pedidos SET id_estadoP = 2 WHERE id_orden = @idOrden";
                        using (MySqlCommand cmd = new MySqlCommand(queryPedidos, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }

                        // 4. Verificar si es la última orden abierta para esta mesa
                        string queryOrdenesAbiertas = @"
                    SELECT COUNT(*) 
                    FROM ordenes 
                    WHERE id_mesa = @idMesa 
                    AND id_estadoO = 1 -- Estado 'Abierta'
                    AND id_orden != @idOrden";

                        int ordenesAbiertas = 0;
                        using (MySqlCommand cmd = new MySqlCommand(queryOrdenesAbiertas, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idMesa", idMesa);
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            ordenesAbiertas = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 5. Liberar la mesa solo si no hay más órdenes abiertas
                        if (ordenesAbiertas == 0)
                        {
                            string queryMesa = "UPDATE mesas SET id_estadoM = 1 WHERE id_mesa = @idMesa";
                            using (MySqlCommand cmd = new MySqlCommand(queryMesa, conexion, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idMesa", idMesa);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 6. Actualizar inventario
                        ActualizarInventario(conexion, transaction);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error en la transacción: {ex.Message}", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        private int ObtenerIdMetodoPago(string nombrePago, MySqlConnection conexion, MySqlTransaction transaction)
        {
            string query = "SELECT id_pago FROM tipoPago WHERE nombrePago = @nombrePago";
            using (MySqlCommand cmd = new MySqlCommand(query, conexion, transaction))
            {
                cmd.Parameters.AddWithValue("@nombrePago", nombrePago);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        private void ActualizarInventario(MySqlConnection conexion, MySqlTransaction transaction)
        {
            try
            {
                // 1. Restar inventario para platos individuales (ingredientes según recetas)
                string queryPlatos = @"
        UPDATE inventario i
        JOIN recetas r ON i.id_inventario = r.id_inventario
        JOIN pedidos p ON r.id_plato = p.id_plato
        SET i.cantidad = i.cantidad - (r.cantidad_necesaria * p.Cantidad)
        WHERE p.id_orden = @idOrden AND p.id_plato IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryPlatos, conexion, transaction))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    cmd.ExecuteNonQuery();
                }

                // 2. Restar inventario para bebidas individuales
                string queryBebidas = @"
        UPDATE inventario i
        JOIN bebidas b ON i.id_inventario = b.id_inventario
        JOIN pedidos p ON b.id_bebida = p.id_bebida
        SET i.cantidad = i.cantidad - p.Cantidad
        WHERE p.id_orden = @idOrden AND p.id_bebida IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryBebidas, conexion, transaction))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    cmd.ExecuteNonQuery();
                }

                // 3. Restar inventario para extras individuales
                string queryExtras = @"
        UPDATE inventario i
        JOIN extras e ON i.id_inventario = e.id_inventario
        JOIN pedidos p ON e.id_extra = p.id_extra
        SET i.cantidad = i.cantidad - p.Cantidad
        WHERE p.id_orden = @idOrden AND p.id_extra IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryExtras, conexion, transaction))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    cmd.ExecuteNonQuery();
                }

                // 4. Restar inventario para promociones (todos sus componentes)
                string queryPromociones = @"
        SELECT 
            pi.id_promocion,
            pi.tipo_item,
            pi.id_item,
            pi.cantidad AS cantidad_en_promo,
            p.Cantidad AS cantidad_pedida
        FROM promocion_items pi
        JOIN pedidos p ON pi.id_promocion = p.id_promocion
        WHERE p.id_orden = @idOrden AND p.id_promocion IS NOT NULL";

                DataTable dtPromociones = new DataTable();
                using (MySqlCommand cmd = new MySqlCommand(queryPromociones, conexion, transaction))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dtPromociones);
                }

                foreach (DataRow row in dtPromociones.Rows)
                {
                    string tipoItem = row["tipo_item"].ToString();
                    int idItem = Convert.ToInt32(row["id_item"]);
                    int cantidadEnPromo = Convert.ToInt32(row["cantidad_en_promo"]);
                    int cantidadPedida = Convert.ToInt32(row["cantidad_pedida"]);
                    int cantidadTotal = cantidadEnPromo * cantidadPedida;

                    switch (tipoItem)
                    {
                        case "PLATO":
                            // Para platos en promoción, restar sus ingredientes
                            string queryPlatoPromo = @"
                    UPDATE inventario i
                    JOIN recetas r ON i.id_inventario = r.id_inventario
                    SET i.cantidad = i.cantidad - (r.cantidad_necesaria * @cantidadTotal)
                    WHERE r.id_plato = @idItem";

                            using (MySqlCommand cmd = new MySqlCommand(queryPlatoPromo, conexion, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idItem", idItem);
                                cmd.Parameters.AddWithValue("@cantidadTotal", cantidadTotal);
                                cmd.ExecuteNonQuery();
                            }
                            break;

                        case "BEBIDA":
                            // Para bebidas en promoción, restar directamente del inventario
                            string queryBebidaPromo = @"
                    UPDATE inventario i
                    JOIN bebidas b ON i.id_inventario = b.id_inventario
                    SET i.cantidad = i.cantidad - @cantidadTotal
                    WHERE b.id_bebida = @idItem";

                            using (MySqlCommand cmd = new MySqlCommand(queryBebidaPromo, conexion, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idItem", idItem);
                                cmd.Parameters.AddWithValue("@cantidadTotal", cantidadTotal);
                                cmd.ExecuteNonQuery();
                            }
                            break;

                        case "EXTRA":
                            // Para extras en promoción, restar directamente del inventario
                            string queryExtraPromo = @"
                    UPDATE inventario i
                    JOIN extras e ON i.id_inventario = e.id_inventario
                    SET i.cantidad = i.cantidad - @cantidadTotal
                    WHERE e.id_extra = @idItem";

                            using (MySqlCommand cmd = new MySqlCommand(queryExtraPromo, conexion, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idItem", idItem);
                                cmd.Parameters.AddWithValue("@cantidadTotal", cantidadTotal);
                                cmd.ExecuteNonQuery();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar inventario: {ex.Message}");
            }
        }

        private bool VerificarStockDisponible()
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                // Verificar stock para items individuales
                string queryVerificar = @"
        SELECT i.nombreProducto, i.cantidad AS stock
        FROM pedidos p
        LEFT JOIN platos pl ON p.id_plato = pl.id_plato
        LEFT JOIN recetas r ON pl.id_plato = r.id_plato
        LEFT JOIN inventario i ON r.id_inventario = i.id_inventario
        WHERE p.id_orden = @idOrden AND i.cantidad < (r.cantidad_necesaria * p.Cantidad)
        
        UNION
        
        SELECT i.nombreProducto, i.cantidad AS stock
        FROM pedidos p
        LEFT JOIN bebidas b ON p.id_bebida = b.id_bebida
        LEFT JOIN inventario i ON b.id_inventario = i.id_inventario
        WHERE p.id_orden = @idOrden AND i.cantidad < p.Cantidad
        
        UNION
        
        SELECT i.nombreProducto, i.cantidad AS stock
        FROM pedidos p
        LEFT JOIN extras e ON p.id_extra = e.id_extra
        LEFT JOIN inventario i ON e.id_inventario = i.id_inventario
        WHERE p.id_orden = @idOrden AND i.cantidad < p.Cantidad
        
        UNION
        
        -- Verificación para promociones
        SELECT i.nombreProducto, i.cantidad AS stock
        FROM pedidos p
        JOIN promocion_items pi ON p.id_promocion = pi.id_promocion
        LEFT JOIN platos pl ON pi.tipo_item = 'PLATO' AND pi.id_item = pl.id_plato
        LEFT JOIN recetas r ON pl.id_plato = r.id_plato
        LEFT JOIN inventario i ON r.id_inventario = i.id_inventario
        WHERE p.id_orden = @idOrden AND i.cantidad < (r.cantidad_necesaria * pi.cantidad * p.Cantidad)
        
        UNION
        
        SELECT i.nombreProducto, i.cantidad AS stock
        FROM pedidos p
        JOIN promocion_items pi ON p.id_promocion = pi.id_promocion
        LEFT JOIN bebidas b ON pi.tipo_item = 'BEBIDA' AND pi.id_item = b.id_bebida
        LEFT JOIN inventario i ON b.id_inventario = i.id_inventario
        WHERE p.id_orden = @idOrden AND i.cantidad < (pi.cantidad * p.Cantidad)
        
        UNION
        
        SELECT i.nombreProducto, i.cantidad AS stock
        FROM pedidos p
        JOIN promocion_items pi ON p.id_promocion = pi.id_promocion
        LEFT JOIN extras e ON pi.tipo_item = 'EXTRA' AND pi.id_item = e.id_extra
        LEFT JOIN inventario i ON e.id_inventario = i.id_inventario
        WHERE p.id_orden = @idOrden AND i.cantidad < (pi.cantidad * p.Cantidad)";

                DataTable dt = new DataTable();
                using (MySqlCommand cmd = new MySqlCommand(queryVerificar, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("¡Stock insuficiente para los siguientes productos:");
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine($"- {row["nombreProducto"]} (Stock: {row["stock"]})");
                    }
                    MessageBox.Show(sb.ToString(), "Error de stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnProcesarPago_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Confirmar pago de la orden?", "Confirmar Pago",
       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!VerificarStockDisponible())
                {
                    return;
                }
                try
                {
                    string metodoPago = comboMetodoPago.SelectedValue.ToString();
                    decimal recibido = 0;
                    decimal cambio = 0;
                    decimal totalAPagar = CalcularTotalConDescuento();

                    if (metodoPago == "Efectivo") // Comparar con el string
                    {
                        if (!decimal.TryParse(txtRecibido.Text, out recibido))
                        {
                            MessageBox.Show("Ingrese un monto válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        cambio = recibido - totalAPagar;
                        if (cambio < 0)
                        {
                            MessageBox.Show("El monto recibido no cubre el total", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else // Tarjeta u otro método
                    {
                        recibido = totalAPagar;
                        cambio = 0;

                        if (MessageBox.Show($"¿Confirmar pago con {metodoPago} por {totalAPagar.ToString("C")}?",
                            "Confirmar Pago",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            return;
                        }
                    }

                    bool exito = ProcesarPagoEnBD(metodoPago, recibido, cambio);

                    if (exito)
                    {
                        MessageBox.Show("Pago procesado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImprimirComprobante_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Calcular valores
                decimal subtotal = totalOrden;
                decimal descuentoMonto = 0;
                decimal totalAPagar = subtotal;

                if (descuentoAplicado)
                {
                    bool esPorcentaje = tipoDescuento.Contains("%") ||
                                      (tiposDescuentoDisponibles != null &&
                                       cmbTipoDescuento.SelectedIndex >= 0 &&
                                       Convert.ToBoolean(tiposDescuentoDisponibles.Rows[cmbTipoDescuento.SelectedIndex]["es_porcentaje"]));

                    if (esPorcentaje)
                    {
                        descuentoMonto = subtotal * (montoDescuento / 100);
                    }
                    else
                    {
                        descuentoMonto = montoDescuento;
                    }
                    totalAPagar = subtotal - descuentoMonto;
                    if (totalAPagar < 0) totalAPagar = 0;
                }

                // Obtener detalles de promociones
                Dictionary<int, List<string>> promocionesConItems = ObtenerDetallesPromociones();

                // Generar comprobante
                StringBuilder comprobante = new StringBuilder();
                comprobante.AppendLine("══════════════════════════════════");
                comprobante.AppendLine("        LA CAGUAMA RESTAURANTE    ");
                comprobante.AppendLine("══════════════════════════════════");
                comprobante.AppendLine($"Orden: #{idOrden}");
                comprobante.AppendLine($"Cliente: {lblCliente.Text.Replace("Cliente: ", "")}");
                comprobante.AppendLine($"Fecha: {DateTime.Now.ToString("g")}");
                comprobante.AppendLine("──────────────────────────────────");
                comprobante.AppendLine("              DETALLE             ");
                comprobante.AppendLine("──────────────────────────────────");

                // Detalle de productos
                for (int i = 0; i < dataGridViewDetalle.Rows.Count; i++)
                {
                    if (!dataGridViewDetalle.Rows[i].IsNewRow)
                    {
                        string producto = dataGridViewDetalle.Rows[i].Cells["Producto"].Value?.ToString() ?? "Desconocido";
                        int cantidad = Convert.ToInt32(dataGridViewDetalle.Rows[i].Cells["Cantidad"].Value);
                        decimal subtotalItem = Convert.ToDecimal(dataGridViewDetalle.Rows[i].Cells["Subtotal"].Value);

                        comprobante.AppendLine($"{producto} x{cantidad} - {subtotalItem.ToString("C")}");

                        // Mostrar items de promoción si existe
                        if (promocionesConItems.ContainsKey(i))
                        {
                            comprobante.AppendLine("   ┌───────────────────────────────");
                            comprobante.AppendLine("   │ Items incluidos en la promoción:");
                            foreach (var item in promocionesConItems[i])
                            {
                                comprobante.AppendLine($"   ├─ {item}");
                            }
                            comprobante.AppendLine("   └───────────────────────────────");
                        }
                    }
                }

                comprobante.AppendLine("──────────────────────────────────");
                comprobante.AppendLine($"SUBTOTAL: {subtotal.ToString("C")}");

                // Mostrar descuento si aplica
                if (descuentoAplicado)
                {
                    string tipoDesc = tipoDescuento.Contains("%") ? $"{montoDescuento}%" : "Fijo";
                    comprobante.AppendLine($"DESCUENTO ({tipoDesc}): -{descuentoMonto.ToString("C")}");
                }

                comprobante.AppendLine("──────────────────────────────────");
                comprobante.AppendLine($"TOTAL: {totalAPagar.ToString("C")}");
                comprobante.AppendLine("──────────────────────────────────");
                comprobante.AppendLine($"Método de pago: {comboMetodoPago.Text}");

                if (panelEfectivo.Visible) // Verificar si es pago en efectivo
                {
                    comprobante.AppendLine($"Recibido: {txtRecibido.Text}");
                    comprobante.AppendLine($"Cambio: {lblCambio.Text}");
                }

                comprobante.AppendLine("══════════════════════════════════");
                comprobante.AppendLine("   ¡Gracias por su preferencia!   ");
                comprobante.AppendLine("══════════════════════════════════");

                // Mostrar el comprobante en un formulario
                using (var formFactura = new Form()
                {
                    Width = 500,
                    Height = 600,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = $"Comprobante Orden #{idOrden}",
                    StartPosition = FormStartPosition.CenterParent,
                    MaximizeBox = false,
                    MinimizeBox = false
                })
                {
                    var textBox = new TextBox()
                    {
                        Multiline = true,
                        Dock = DockStyle.Fill,
                        ReadOnly = true,
                        ScrollBars = ScrollBars.Vertical,
                        Text = comprobante.ToString(),
                        Font = new Font("Consolas", 10)
                    };

                    formFactura.Controls.Add(textBox);
                    formFactura.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar comprobante: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<int, List<string>> ObtenerDetallesPromociones()
        {
            var promocionesConItems = new Dictionary<int, List<string>>();

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                // Obtener promociones en esta orden
                string query = @"
SELECT 
    p.id_promocion,
    pr.nombre AS nombre_promocion,
    p.Cantidad AS cantidad_pedida,
    pi.tipo_item,
    CASE
        WHEN pi.tipo_item = 'PLATO' THEN (SELECT pl.nombrePlato FROM platos pl WHERE pl.id_plato = pi.id_item)
        WHEN pi.tipo_item = 'BEBIDA' THEN (SELECT i.nombreProducto FROM bebidas b JOIN inventario i ON b.id_inventario = i.id_inventario WHERE b.id_bebida = pi.id_item)
        WHEN pi.tipo_item = 'EXTRA' THEN (SELECT i.nombreProducto FROM extras e JOIN inventario i ON e.id_inventario = i.id_inventario WHERE e.id_extra = pi.id_item)
    END AS nombre_item,
    pi.cantidad AS cantidad_en_promo
FROM pedidos p
JOIN promociones pr ON p.id_promocion = pr.id_promocion
JOIN promocion_items pi ON pr.id_promocion = pi.id_promocion
WHERE p.id_orden = @idOrden AND p.id_promocion IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreItem = reader["nombre_item"].ToString();
                            string tipoItem = reader["tipo_item"].ToString();
                            int cantidadEnPromo = Convert.ToInt32(reader["cantidad_en_promo"]);
                            int cantidadPedida = Convert.ToInt32(reader["cantidad_pedida"]);
                            int cantidadTotal = cantidadEnPromo * cantidadPedida;

                            string itemDesc = $"{nombreItem} ({tipoItem}) x{cantidadTotal}";

                            // Buscar el índice de la fila en el DataGridView que corresponde a esta promoción
                            int rowIndex = -1;
                            for (int i = 0; i < dataGridViewDetalle.Rows.Count; i++)
                            {
                                if (!dataGridViewDetalle.Rows[i].IsNewRow &&
                                    dataGridViewDetalle.Rows[i].Cells["Producto"].Value.ToString() == reader["nombre_promocion"].ToString())
                                {
                                    rowIndex = i;
                                    break;
                                }
                            }

                            if (rowIndex >= 0)
                            {
                                if (!promocionesConItems.ContainsKey(rowIndex))
                                {
                                    promocionesConItems[rowIndex] = new List<string>();
                                }
                                promocionesConItems[rowIndex].Add(itemDesc);
                            }
                        }
                    }
                }
            }

            return promocionesConItems;
        }

        private void txtRecibido_TextChanged(object sender, EventArgs e)
        {
            // Solo validar monto si es pago en efectivo (manteniendo tu lógica original)
            if (panelEfectivo.Visible)
            {
                // Validar el formato del texto mientras se escribe
                if (txtRecibido.Text.Length > 0)
                {
                    // Eliminar múltiples puntos decimales
                    if (txtRecibido.Text.Count(c => c == '.') > 1)
                    {
                        int lastIndex = txtRecibido.Text.LastIndexOf('.');
                        txtRecibido.Text = txtRecibido.Text.Remove(lastIndex, 1);
                        txtRecibido.SelectionStart = txtRecibido.Text.Length;
                        return;
                    }

                    // Si comienza con punto, agregar 0 antes
                    if (txtRecibido.Text.StartsWith("."))
                    {
                        txtRecibido.Text = "0" + txtRecibido.Text;
                        txtRecibido.SelectionStart = txtRecibido.Text.Length;
                        return;
                    }

                    // Eliminar cualquier carácter no numérico excepto punto
                    string newText = new string(txtRecibido.Text.Where(c => char.IsDigit(c) || c == '.').ToArray());
                    if (txtRecibido.Text != newText)
                    {
                        txtRecibido.Text = newText;
                        txtRecibido.SelectionStart = txtRecibido.Text.Length;
                        return;
                    }
                }

                if (decimal.TryParse(txtRecibido.Text, out decimal recibido))
                {
                    decimal totalAPagar = CalcularTotalConDescuento();
                    decimal cambio = recibido - totalAPagar;

                    // Mostrar cambio (no permitir valores negativos)
                    lblCambio.Text = cambio >= 0 ? cambio.ToString("C") : "$0.00";

                    // Habilitar botón solo si el monto es suficiente
                    btnProcesarPago.Enabled = (recibido >= totalAPagar);
                }
                else
                {
                    lblCambio.Text = "$0.00";
                    btnProcesarPago.Enabled = false;
                }
            }
        }

        private void txtRecibido_Leave(object sender, EventArgs e)
        {
            // Formatear al salir del campo
            if (panelEfectivo.Visible)
            {
                if (decimal.TryParse(txtRecibido.Text, out decimal valor))
                {
                    txtRecibido.Text = valor.ToString("0.00");
                }
                else if (string.IsNullOrEmpty(txtRecibido.Text))
                {
                    txtRecibido.Text = "0.00";
                }
                else
                {
                    // Si el texto no es un número válido, resetear a 0.00
                    txtRecibido.Text = "0.00";
                }

                // Forzar actualización del cambio
                txtRecibido_TextChanged(sender, e);
            }
        }

        private void txtRecibido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir:
            // - Dígitos (0-9)
            // - Punto decimal (.)
            // - Tecla de retroceso (Backspace)
            // - Tecla de suprimir (Delete)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
                return;
            }

            // Si el punto es el primer carácter, agregar un 0 antes
            if (e.KeyChar == '.' && (sender as TextBox).Text.Length == 0)
            {
                (sender as TextBox).Text = "0";
                (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
            }
        }

        private void comboMetodoPago_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (comboMetodoPago.SelectedValue != null)
                {
                    string metodoSeleccionado = comboMetodoPago.SelectedValue.ToString();
                    panelEfectivo.Visible = (metodoSeleccionado == "Efectivo");
                    btnProcesarPago.Enabled = true;
                    ActualizarCamposPago();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar método de pago: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormAdminPagos_Load(object sender, EventArgs e)
        {

        }

        private void lblTotalP_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblCliente_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void chkAplicarDescuento_CheckedChanged(object sender, EventArgs e)
        {

            cmbTipoDescuento.Visible = chkAplicarDescuento.Checked;
            txtMontoDescuento.Visible = chkAplicarDescuento.Checked;
            btnAplicarDescuento.Visible = chkAplicarDescuento.Checked;
            label5.Visible = chkAplicarDescuento.Checked; // Etiqueta "Tipo Descuento"
            label6.Visible = chkAplicarDescuento.Checked; // Etiqueta "Monto"

            if (!chkAplicarDescuento.Checked)
            {
                // Si se desmarca, quitamos el descuento pero mantenemos los valores por si se vuelve a activar
                descuentoAplicado = false;
                ActualizarTotalConDescuento();
            }
            else
            {
                // Si se marca, seleccionamos el primer tipo de descuento por defecto
                if (cmbTipoDescuento.Items.Count > 0)
                {
                    cmbTipoDescuento.SelectedIndex = 0;
                }
                txtMontoDescuento.Focus();
            }
        }

        private void txtMontoDescuento_TextChanged(object sender, EventArgs e)
        {
            // Validar el texto después de que cambia
            if (sender is TextBox textBox)
            {
                // Eliminar caracteres no válidos manteniendo la lógica original
                string newText = string.Empty;
                bool hasDecimal = false;

                foreach (char c in textBox.Text)
                {
                    if (char.IsDigit(c))
                    {
                        newText += c;
                    }
                    else if (c == '.' && !hasDecimal)
                    {
                        newText += c;
                        hasDecimal = true;
                    }
                }

                if (textBox.Text != newText)
                {
                    textBox.Text = newText;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }

        }

        private void txtMontoDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal y tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
                return;
            }

            // Validación adicional para porcentajes
            if (cmbTipoDescuento.SelectedValue != null &&
                tiposDescuentoDisponibles.Rows[cmbTipoDescuento.SelectedIndex]["es_porcentaje"].ToString() == "True")
            {
                // Si es porcentaje, validar que no sea mayor a 100
                string currentText = (sender as TextBox).Text + e.KeyChar;
                if (decimal.TryParse(currentText, out decimal valor) && valor > 100)
                {
                    e.Handled = true;
                    MessageBox.Show("El descuento porcentual no puede ser mayor a 100%", "Advertencia",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnAplicarDescuento_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si ya se aplicó un descuento
                if (descuentoAplicado)
                {
                    MessageBox.Show("Ya se ha aplicado un descuento a esta orden", "Advertencia",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbTipoDescuento.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un tipo de descuento", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtMontoDescuento.Text, out decimal monto) || monto <= 0)
                {
                    MessageBox.Show("Ingrese un monto válido mayor a cero", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el tipo de descuento seleccionado
                DataRowView selectedRow = (DataRowView)cmbTipoDescuento.SelectedItem;
                idTipoDescuentoSeleccionado = Convert.ToInt32(selectedRow["id_tipo_descuento"]);
                tipoDescuento = selectedRow["nombre"].ToString();
                bool esPorcentaje = Convert.ToBoolean(selectedRow["es_porcentaje"]);

                // Validaciones específicas
                if (esPorcentaje && monto > 100)
                {
                    MessageBox.Show("El descuento porcentual no puede ser mayor al 100%", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aplicar el descuento
                descuentoAplicado = true;
                montoDescuento = monto;

                // Deshabilitar controles de descuento después de aplicar
                chkAplicarDescuento.Enabled = false;
                cmbTipoDescuento.Enabled = false;
                txtMontoDescuento.Enabled = false;
                btnAplicarDescuento.Enabled = false;

                // Actualizar la interfaz
                ActualizarTotalConDescuento();

                // Actualizar txtRecibido solo si es pago en efectivo y está visible
                if (panelEfectivo.Visible)
                {
                    decimal totalActual = CalcularTotalConDescuento();
                    txtRecibido.Text = totalActual.ToString("0.00");
                }

                MessageBox.Show("Descuento aplicado correctamente", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar descuento: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ActualizarTotalConDescuento()
        {
            try
            {
                decimal subtotal = totalOrden;
                decimal descuentoMonto = 0;
                decimal totalConDescuento = subtotal;

                if (descuentoAplicado)
                {
                    bool esPorcentaje = tipoDescuento.Contains("%") ||
                                      (tiposDescuentoDisponibles != null &&
                                       cmbTipoDescuento.SelectedIndex >= 0 &&
                                       Convert.ToBoolean(tiposDescuentoDisponibles.Rows[cmbTipoDescuento.SelectedIndex]["es_porcentaje"]));

                    if (esPorcentaje)
                    {
                        descuentoMonto = subtotal * (montoDescuento / 100);
                    }
                    else
                    {
                        descuentoMonto = montoDescuento;
                    }

                    totalConDescuento = subtotal - descuentoMonto;
                    if (totalConDescuento < 0) totalConDescuento = 0;

                    // Mostrar el descuento aplicado
                    lblDescuentoAplicado.Text = $"-{descuentoMonto.ToString("C")}";
                    if (esPorcentaje)
                    {
                        lblDescuentoAplicado.Text += $" ({montoDescuento}%)";
                    }
                    lblDescuentoAplicado.Visible = true;
                }
                else
                {
                    lblDescuentoAplicado.Visible = false;
                }

                // Actualizar los labels
                lblSubtotal.Text = subtotal.ToString("C");
                lblTotalP.Text = totalConDescuento.ToString("C");

                // Actualizar txtRecibido si es pago en efectivo
                if (panelEfectivo.Visible)
                {
                    txtRecibido.Text = totalConDescuento.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular el descuento: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarTiposDescuento()
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = "SELECT id_tipo_descuento, nombre, es_porcentaje FROM tipo_descuento";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    tiposDescuentoDisponibles = new DataTable();
                    da.Fill(tiposDescuentoDisponibles);

                    cmbTipoDescuento.DataSource = tiposDescuentoDisponibles;
                    cmbTipoDescuento.DisplayMember = "nombre";
                    cmbTipoDescuento.ValueMember = "id_tipo_descuento";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de descuento: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nuevo método para calcular el total con descuento
        private decimal CalcularTotalConDescuento()
        {
            decimal totalConDescuento = totalOrden;

            if (descuentoAplicado)
            {
                bool esPorcentaje = tipoDescuento.Contains("%") ||
                                  (tiposDescuentoDisponibles != null &&
                                   cmbTipoDescuento.SelectedIndex >= 0 &&
                                   Convert.ToBoolean(tiposDescuentoDisponibles.Rows[cmbTipoDescuento.SelectedIndex]["es_porcentaje"]));

                if (esPorcentaje)
                {
                    totalConDescuento = totalOrden * (1 - (montoDescuento / 100));
                }
                else
                {
                    totalConDescuento = totalOrden - montoDescuento;
                    if (totalConDescuento < 0) totalConDescuento = 0;
                }
            }

            return totalConDescuento;
        }
    }
}