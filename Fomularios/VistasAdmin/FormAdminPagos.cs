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
                comboMetodoPago.ValueMember = "id_pago";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar métodos de pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (comboMetodoPago.SelectedValue != null && comboMetodoPago.SelectedValue is int)
                {
                    int metodoSeleccionado = (int)comboMetodoPago.SelectedValue;
                    panelEfectivo.Visible = (metodoSeleccionado == 1); // 1 = Efectivo

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

        private bool ProcesarPagoEnBD(int metodoPago, decimal recibido, decimal cambio)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // Calcular el total con descuento si aplica
                        decimal totalConDescuento = totalOrden;
                        if (descuentoAplicado)
                        {
                            bool esPorcentaje = tipoDescuento.Contains("%");
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

                        // 1. Registrar el pago (actualizado para incluir tipo de descuento)
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
                            cmd.Parameters.AddWithValue("@recibido", metodoPago == 1 ? recibido : totalConDescuento);
                            cmd.Parameters.AddWithValue("@cambio", cambio);
                            cmd.Parameters.AddWithValue("@idUsuarioCreador", idUsuarioCreador);
                            cmd.Parameters.AddWithValue("@tipoPago", metodoPago);
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
                    descuento = @descuento
                WHERE id_orden = @idOrden";

                        using (MySqlCommand cmd = new MySqlCommand(queryUpdateOrden, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@tipoPago", metodoPago);
                            cmd.Parameters.AddWithValue("@total", totalConDescuento);
                            cmd.Parameters.AddWithValue("@descuento", descuentoAplicado ? montoDescuento : 0);
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }


                        // 3. Actualizar estado de los pedidos a "Entregado" (id_estadoP = 2)
                        string queryPedidos = "UPDATE pedidos SET id_estadoP = 2 WHERE id_orden = @idOrden";
                        using (MySqlCommand cmd = new MySqlCommand(queryPedidos, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }

                        // 4. Actualizar estado de la orden a "Cerrada"
                        string queryOrden = "UPDATE ordenes SET id_estadoO = 2 WHERE id_orden = @idOrden";
                        using (MySqlCommand cmd = new MySqlCommand(queryOrden, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }

                        // 5. Liberar la mesa
                        string queryMesa = "UPDATE mesas SET id_estadoM = 1 WHERE id_mesa = @idMesa";
                        using (MySqlCommand cmd = new MySqlCommand(queryMesa, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idMesa", idMesa);
                            cmd.ExecuteNonQuery();
                        }

                        // 6. Actualizar inventario (¡ESTA ES LA PARTE QUE FALTABA!)
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
                    int metodoPago = (int)comboMetodoPago.SelectedValue;
                    decimal recibido = 0;
                    decimal cambio = 0;
                    decimal totalAPagar = totalOrden;

                    // Calcular total con descuento si aplica (no depende del checkbox)
                    if (descuentoAplicado)
                    {
                        if (tipoDescuento == "Porcentaje (%)")
                        {
                            totalAPagar = totalOrden * (1 - (montoDescuento / 100));
                        }
                        else
                        {
                            totalAPagar = totalOrden - montoDescuento;
                            if (totalAPagar < 0) totalAPagar = 0;
                        }
                    }

                    if (metodoPago == 1) // Efectivo
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

                    // Procesar el pago en la base de datos
                    bool exito = ProcesarPagoEnBD(metodoPago, recibido, cambio);

                    if (exito)
                    {
                        MessageBox.Show("Pago procesado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al procesar el pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Calcular total con descuento si aplica
                decimal totalAPagar = totalOrden;
                if (descuentoAplicado)
                {
                    if (tipoDescuento == "Porcentaje (%)")
                    {
                        totalAPagar = totalOrden * (1 - (montoDescuento / 100));
                    }
                    else
                    {
                        totalAPagar = totalOrden - montoDescuento;
                        if (totalAPagar < 0) totalAPagar = 0;
                    }
                }

                // Obtener detalles de promociones para esta orden
                Dictionary<int, List<string>> promocionesConItems = ObtenerDetallesPromociones();

                // Generar comprobante mejorado
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

                // Recorrer todos los items del pedido
                foreach (DataGridViewRow row in dataGridViewDetalle.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string producto = row.Cells["Producto"].Value.ToString();
                        int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                        decimal subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value);

                        // Verificar si es una promoción
                        bool esPromocion = promocionesConItems.ContainsKey(row.Index);

                        comprobante.AppendLine($"{producto} x{cantidad} - {subtotal.ToString("C")}");

                        // Si es promoción, mostrar sus items
                        if (esPromocion)
                        {
                            comprobante.AppendLine("   ┌───────────────────────────────");
                            comprobante.AppendLine("   │ Items incluidos en la promoción:");

                            foreach (string item in promocionesConItems[row.Index])
                            {
                                comprobante.AppendLine($"   ├─ {item}");
                            }

                            comprobante.AppendLine("   └───────────────────────────────");
                        }
                    }
                }
                comprobante.AppendLine("──────────────────────────────────");
                comprobante.AppendLine($"SUBTOTAL: {totalOrden.ToString("C")}");

                // Mostrar descuento si aplica
                if (descuentoAplicado)
                {
                    string tipoDesc = tipoDescuento == "Porcentaje (%)" ? $"{montoDescuento}%" : "";
                    comprobante.AppendLine($"DESCUENTO ({tipoDesc}): -{montoDescuento.ToString("C")}");
                }

                comprobante.AppendLine("──────────────────────────────────");
                comprobante.AppendLine($"TOTAL: {lblTotalP.Text}");
                comprobante.AppendLine("──────────────────────────────────");
                comprobante.AppendLine($"Método de pago: {comboMetodoPago.Text}");

                if ((int)comboMetodoPago.SelectedValue == 1) // Efectivo
                {
                    comprobante.AppendLine($"Recibido: {txtRecibido.Text}");
                    comprobante.AppendLine($"Cambio: {lblCambio.Text}");
                }

                comprobante.AppendLine("══════════════════════════════════");
                comprobante.AppendLine("   ¡Gracias por su preferencia!   ");
                comprobante.AppendLine("══════════════════════════════════");

                // Mostrar en un formulario con scroll para mejor visualización
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
                        Font = new Font("Consolas", 10) // Usar fuente monoespaciada para alineación
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
            if (decimal.TryParse(txtRecibido.Text, out decimal recibido))
            {
                decimal totalAPagar = CalcularTotalConDescuento();
                decimal cambio = recibido - totalAPagar;
                if (cambio < 0) cambio = 0;

                lblCambio.Text = cambio.ToString("C");

                // Habilitar botón de pago solo si el monto es suficiente
                btnProcesarPago.Enabled = (recibido >= totalAPagar);
            }
            else
            {
                lblCambio.Text = "$0.00";
                btnProcesarPago.Enabled = false;
            }
        }

        private void txtRecibido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal y tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void comboMetodoPago_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ActualizarCamposPago();
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
                decimal nuevoTotal = totalOrden;

                if (descuentoAplicado)
                {
                    bool esPorcentaje = tipoDescuento.Contains("%");
                    if (esPorcentaje)
                    {
                        // Validar que el porcentaje esté entre 0 y 100
                        if (montoDescuento > 100)
                        {
                            MessageBox.Show("El porcentaje de descuento no puede ser mayor a 100%", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Aplicar descuento porcentual
                        decimal porcentajeDescuento = montoDescuento / 100;
                        decimal descuento = totalOrden * porcentajeDescuento;
                        nuevoTotal = totalOrden - descuento;
                    }
                    else
                    {
                        // Aplicar descuento fijo
                        nuevoTotal = totalOrden - montoDescuento;

                        // No permitir total negativo
                        if (nuevoTotal < 0)
                        {
                            MessageBox.Show("El descuento no puede ser mayor que el total", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            nuevoTotal = 0;
                        }
                    }
                }

                // Actualizar el total mostrado
                lblTotalP.Text = nuevoTotal.ToString("C");

                // No actualizar automáticamente txtRecibido aquí
                // El usuario lo llenará manualmente o se actualizará cuando cambie el método de pago
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
                bool esPorcentaje = tipoDescuento.Contains("%");
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