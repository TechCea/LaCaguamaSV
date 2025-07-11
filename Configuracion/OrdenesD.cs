﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace LaCaguamaSV.Configuracion
{
    class OrdenesD
    {
        public static DataTable ObtenerOrdenes()
        {
            // Versión original sin filtro por fecha
            return ObtenerOrdenes(false);
        }

        public static DataTable ObtenerOrdenes(bool soloHoy = true)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query;

                if (soloHoy)
                {
                    // Nuevo rango de horas: 10am a 6am del día siguiente
                    query = @"
            SELECT 
                o.id_orden,
                o.nombreCliente,
                o.total,
                o.descuento,
                DATE_FORMAT(o.fecha_orden, '%Y-%m-%d %H:%i') AS fecha_orden,
                m.nombreMesa AS numero_mesa,
                tp.nombrePago AS tipo_pago,
                u.nombre AS nombre_usuario,
                eo.nombreEstadoO AS estado_orden
            FROM ordenes o
            JOIN mesas m ON o.id_mesa = m.id_mesa
            JOIN tipoPago tp ON o.tipo_pago = tp.id_pago
            JOIN usuarios u ON o.id_usuario = u.id_usuario
            JOIN estado_orden eo ON o.id_estadoO = eo.id_estadoO
            WHERE o.fecha_orden BETWEEN 
                CASE 
                    WHEN TIME(NOW()) < '06:00:00' THEN 
                        CONCAT(DATE_SUB(CURDATE(), INTERVAL 1 DAY), ' 10:00:00')
                    ELSE 
                        CONCAT(CURDATE(), ' 10:00:00')
                END
                AND
                CASE 
                    WHEN TIME(NOW()) < '06:00:00' THEN 
                        CONCAT(CURDATE(), ' 06:00:00')
                    ELSE 
                        CONCAT(DATE_ADD(CURDATE(), INTERVAL 1 DAY), ' 06:00:00')
                END
            ORDER BY o.fecha_orden DESC";
                }
                else
                {
                    query = @"
            SELECT 
                o.id_orden,
                o.nombreCliente,
                o.total,
                o.descuento,
                DATE_FORMAT(o.fecha_orden, '%Y-%m-%d %H:%i') AS fecha_orden,
                m.nombreMesa AS numero_mesa,
                tp.nombrePago AS tipo_pago,
                u.nombre AS nombre_usuario,
                eo.nombreEstadoO AS estado_orden
            FROM ordenes o
            JOIN mesas m ON o.id_mesa = m.id_mesa
            JOIN tipoPago tp ON o.tipo_pago = tp.id_pago
            JOIN usuarios u ON o.id_usuario = u.id_usuario
            JOIN estado_orden eo ON o.id_estadoO = eo.id_estadoO
            ORDER BY o.fecha_orden DESC";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static DataTable ObtenerTiposPago()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = "SELECT id_pago, nombrePago FROM tipoPago";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener tipos de pago: {ex.Message}");
            }
            return dt;
        }

        public static DataTable ObtenerMesas()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string query = "SELECT id_mesa, nombreMesa FROM mesas WHERE id_estadoM = 1"; // Solo mesas disponibles
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener mesas: " + ex.Message);
                }
            }
            return dt;
        }

        public static int CrearOrdenVacia(string nombreCliente, int idMesa, int tipoPago)
        {
            int idOrden = -1;
            int idUsuario = SesionUsuario.IdUsuario;
            int idCaja = ObtenerIdCajaActiva();

            if (idCaja <= 0)
            {
                MessageBox.Show("No hay una caja activa para registrar la orden", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
                INSERT INTO ordenes (
                    nombreCliente, 
                    total, 
                    descuento, 
                    fecha_orden, 
                    id_mesa, 
                    tipo_pago, 
                    id_usuario, 
                    id_estadoO,
                    id_caja
                ) 
                VALUES (
                    @nombreCliente, 
                    0, 
                    0, 
                    NOW(), 
                    @idMesa, 
                    @tipoPago, 
                    @idUsuario, 
                    1, -- Estado 'Abierta'
                    @idCaja
                );
                SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombreCliente", nombreCliente);
                        cmd.Parameters.AddWithValue("@idMesa", idMesa);
                        cmd.Parameters.AddWithValue("@tipoPago", tipoPago);
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@idCaja", idCaja);

                        idOrden = Convert.ToInt32(cmd.ExecuteScalar());

                        // Solo actualizar estado de la mesa si NO es "Para Llevar" y es la primera orden
                        if (idOrden > 0 && !EsMesaParaLlevar(idMesa))
                        {
                            // Verificar si es la primera orden para esta mesa
                            string queryCount = "SELECT COUNT(*) FROM ordenes WHERE id_mesa = @idMesa AND id_estadoO = 1";
                            using (MySqlCommand cmdCount = new MySqlCommand(queryCount, conexion))
                            {
                                cmdCount.Parameters.AddWithValue("@idMesa", idMesa);
                                int ordenesAbiertas = Convert.ToInt32(cmdCount.ExecuteScalar());

                                // Si es la primera orden, marcar la mesa como ocupada
                                if (ordenesAbiertas == 1)
                                {
                                    string updateMesa = "UPDATE mesas SET id_estadoM = 2 WHERE id_mesa = @idMesa";
                                    using (MySqlCommand cmdMesa = new MySqlCommand(updateMesa, conexion))
                                    {
                                        cmdMesa.Parameters.AddWithValue("@idMesa", idMesa);
                                        cmdMesa.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la orden: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return idOrden;
        }

        private static bool EsMesaParaLlevar(int idMesa)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = "SELECT nombreMesa FROM mesas WHERE id_mesa = @idMesa";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idMesa", idMesa);
                        string nombreMesa = cmd.ExecuteScalar()?.ToString();
                        return nombreMesa == "Para Llevar";
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        private static int ObtenerIdCajaActiva()
        {
            int idCaja = -1;
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    // Modificamos la consulta para obtener la caja más reciente que esté inicializada
                    string query = @"SELECT id_caja 
                      FROM caja 
                      WHERE id_estado_caja = 2 
                      ORDER BY fecha DESC 
                      LIMIT 1"; // Estado 2 = Inicializada, ordenamos por fecha descendente

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            idCaja = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener caja activa: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return idCaja;
        }

        public static DataTable ObtenerBebidas()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
            SELECT 
                b.id_bebida AS ID, 
                i.nombreProducto AS nombre, 
                b.precioUnitario 
            FROM bebidas b 
            JOIN inventario i ON b.id_inventario = i.id_inventario";

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public static DataTable ObtenerPlatos()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
            SELECT 
                p.id_plato AS ID, 
                p.nombrePlato AS nombre, 
                p.precioUnitario
            FROM platos p";

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }



        public static DataTable ObtenerExtras()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
            SELECT 
                e.id_extra AS ID, 
                i.nombreProducto AS nombre, 
                e.precioUnitario 
            FROM extras e 
            JOIN inventario i ON e.id_inventario = i.id_inventario";

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public static DataTable ObtenerDetallesOrdenPorId(int idOrden)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
                SELECT 
                    o.id_orden,
                    o.nombreCliente,
                    o.total,
                    o.descuento,
                    DATE_FORMAT(o.fecha_orden, '%Y-%m-%d %H:%i') AS fecha_orden,
                    m.nombreMesa AS numero_mesa,
                    tp.nombrePago AS tipo_pago,
                    u.nombre AS nombre_usuario,
                    eo.nombreEstadoO AS estado_orden
                FROM ordenes o
                JOIN mesas m ON o.id_mesa = m.id_mesa
                JOIN tipoPago tp ON o.tipo_pago = tp.id_pago
                JOIN usuarios u ON o.id_usuario = u.id_usuario
                JOIN estado_orden eo ON o.id_estadoO = eo.id_estadoO
                WHERE o.id_orden = @idOrden";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener detalles de orden: {ex.Message}");
            }
            return dt;
        }

        public static DataTable ObtenerMesasDisponibles(int idMesaActual)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"SELECT id_mesa, nombreMesa 
                      FROM mesas 
                      WHERE id_estadoM = 1 OR id_mesa = @idMesaActual
                      ORDER BY nombreMesa";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idMesaActual", idMesaActual);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static async Task<bool> CambiarMesaOrdenAsync(int idOrden, int mesaActualId, int nuevaMesaId)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {

                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // 1. Actualizar orden con nueva mesa
                        string updateOrden = @"UPDATE ordenes 
                                    SET id_mesa = @nuevaMesaId 
                                    WHERE id_orden = @idOrden";

                        using (MySqlCommand cmd = new MySqlCommand(updateOrden, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@nuevaMesaId", nuevaMesaId);
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        // 2. Liberar mesa actual (estado = 1 - Disponible)
                        string liberarMesa = @"UPDATE mesas 
                                      SET id_estadoM = 1 
                                      WHERE id_mesa = @mesaActualId";

                        using (MySqlCommand cmd = new MySqlCommand(liberarMesa, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@mesaActualId", mesaActualId);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        // 3. Ocupar nueva mesa (estado = 2 - Ocupado)
                        string ocuparMesa = @"UPDATE mesas 
                                     SET id_estadoM = 2 
                                     WHERE id_mesa = @nuevaMesaId";

                        using (MySqlCommand cmd = new MySqlCommand(ocuparMesa, conexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@nuevaMesaId", nuevaMesaId);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        Console.WriteLine("Error al cambiar mesa: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        // Método para obtener ID de mesa por nombre
        public static int ObtenerIdMesaPorNombre(string nombreMesa)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = "SELECT id_mesa FROM mesas WHERE nombreMesa = @nombreMesa";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombreMesa", nombreMesa);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public static string VerificarInventarioPlato(int idPlato)
        {
            StringBuilder mensaje = new StringBuilder();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            i.nombreProducto, 
            i.cantidad AS stock_actual, 
            r.cantidad_necesaria,
            (i.cantidad - r.cantidad_necesaria) AS diferencia
        FROM recetas r
        JOIN inventario i ON r.id_inventario = i.id_inventario
        WHERE r.id_plato = @idPlato
        AND i.cantidad < r.cantidad_necesaria";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPlato", idPlato);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ingrediente = reader.GetString("nombreProducto");
                            decimal stock = reader.GetDecimal("stock_actual");
                            decimal necesaria = reader.GetDecimal("cantidad_necesaria");
                            decimal diferencia = reader.GetDecimal("diferencia");

                            mensaje.AppendLine($"- {ingrediente} (Stock: {stock}, Necesario: {necesaria}, Faltante: {Math.Abs(diferencia)})");
                        }
                    }
                }
            }
            return mensaje.ToString();
        }

        public static string VerificarInventarioBebida(int idBebida)
        {
            StringBuilder mensaje = new StringBuilder();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            i.nombreProducto, 
            i.cantidad AS stock_actual
        FROM bebidas b
        JOIN inventario i ON b.id_inventario = i.id_inventario
        WHERE b.id_bebida = @idBebida
        AND i.cantidad < 8"; // Umbral bajo de stock

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idBebida", idBebida);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string bebida = reader.GetString("nombreProducto");
                            decimal stock = reader.GetDecimal("stock_actual");
                            mensaje.AppendLine($"- {bebida} (Stock bajo: {stock})");
                        }
                    }
                }
            }
            return mensaje.ToString();
        }

        public static string VerificarInventarioExtra(int idExtra)
        {
            StringBuilder mensaje = new StringBuilder();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            i.nombreProducto, 
            i.cantidad AS stock_actual
        FROM extras e
        JOIN inventario i ON e.id_inventario = i.id_inventario
        WHERE e.id_extra = @idExtra
        AND i.cantidad < 8"; // Umbral bajo de stock

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idExtra", idExtra);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string extra = reader.GetString("nombreProducto");
                            decimal stock = reader.GetDecimal("stock_actual");
                            mensaje.AppendLine($"- {extra} (Stock bajo: {stock})");
                        }
                    }
                }
            }
            return mensaje.ToString();
        }

        public static string VerificarInventarioPromocion(int idPromocion, int cantidad = 1)
        {
            StringBuilder mensaje = new StringBuilder();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
        SELECT 
            i.nombreProducto,
            i.cantidad AS stock_actual,
            CASE
                WHEN pi.tipo_item = 'PLATO' THEN 
                    (SELECT SUM(r.cantidad_necesaria) 
                     FROM recetas r 
                     WHERE r.id_plato = pi.id_item)
                ELSE 1
            END AS cantidad_necesaria_por_item,
            pi.cantidad AS cantidad_en_promocion,
            (i.cantidad - (CASE
                WHEN pi.tipo_item = 'PLATO' THEN 
                    (SELECT SUM(r.cantidad_necesaria) 
                     FROM recetas r 
                     WHERE r.id_plato = pi.id_item)
                ELSE 1
            END * pi.cantidad * @cantidad)) AS diferencia
        FROM promocion_items pi
        LEFT JOIN platos p ON pi.tipo_item = 'PLATO' AND pi.id_item = p.id_plato
        LEFT JOIN bebidas b ON pi.tipo_item = 'BEBIDA' AND pi.id_item = b.id_bebida
        LEFT JOIN extras e ON pi.tipo_item = 'EXTRA' AND pi.id_item = e.id_extra
        LEFT JOIN inventario i ON 
            (b.id_inventario = i.id_inventario OR 
             e.id_inventario = i.id_inventario OR
             (p.id_plato IS NOT NULL AND 
              EXISTS (SELECT 1 FROM recetas r WHERE r.id_plato = p.id_plato AND r.id_inventario = i.id_inventario)))
        WHERE pi.id_promocion = @idPromocion
        GROUP BY i.id_inventario
        HAVING diferencia < 0 OR i.cantidad < 2"; // Umbral de stock bajo

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPromocion", idPromocion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string producto = reader["nombreProducto"].ToString();
                            decimal stock = reader.GetDecimal("stock_actual");
                            decimal necesaria = reader.GetDecimal("cantidad_necesaria_por_item") *
                                              reader.GetInt32("cantidad_en_promocion") *
                                              cantidad;

                            mensaje.AppendLine($"- {producto} (Stock: {stock}, Necesario: {necesaria})");
                        }
                    }
                }
            }
            return mensaje.ToString();
        }

        public static DataTable ObtenerPromocionesActivas()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
            SELECT 
                p.id_promocion AS ID,
                p.nombre AS nombre,
                p.precio_especial AS precioUnitario,
                p.descripcion AS descripcion,
                'PROMOCION' AS Tipo
            FROM promociones p
            WHERE p.activa = TRUE 
            AND (p.fecha_fin IS NULL OR p.fecha_fin >= CURDATE())
            AND p.fecha_inicio <= CURDATE()";

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public static bool AgregarPromocionAOrden(int idOrden, int idPromocion, int cantidad, string nota, out string mensajeInventario)
        {
            mensajeInventario = string.Empty;

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // Verificar si ya existe esta promoción en la orden con la MISMA nota
                        string queryVerificar = @"SELECT p.id_pedido, p.Cantidad 
                    FROM pedidos p
                    LEFT JOIN notas_pedidos np ON p.id_pedido = np.id_pedido
                    WHERE p.id_orden = @idOrden 
                    AND p.id_promocion = @idPromocion
                    AND (np.nota = @nota OR (np.nota IS NULL AND @nota IS NULL))";

                        int idPedidoExistente = -1;
                        int cantidadExistente = 0;

                        using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conexion, transaction))
                        {
                            cmdVerificar.Parameters.AddWithValue("@idOrden", idOrden);
                            cmdVerificar.Parameters.AddWithValue("@idPromocion", idPromocion);
                            cmdVerificar.Parameters.AddWithValue("@nota", string.IsNullOrEmpty(nota) ? DBNull.Value : (object)nota);

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

                            using (MySqlCommand cmdActualizar = new MySqlCommand(queryActualizar, conexion, transaction))
                            {
                                cmdActualizar.Parameters.AddWithValue("@idPedido", idPedidoExistente);
                                cmdActualizar.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdActualizar.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Si no existe, insertar nueva promoción
                            string queryInsertar = @"INSERT INTO pedidos 
                        (id_orden, id_estadoP, id_promocion, Cantidad) 
                        VALUES (@idOrden, 1, @idPromocion, @cantidad);
                        SELECT LAST_INSERT_ID();";

                            int idPedidoInsertado;
                            using (MySqlCommand cmdInsertar = new MySqlCommand(queryInsertar, conexion, transaction))
                            {
                                cmdInsertar.Parameters.AddWithValue("@idOrden", idOrden);
                                cmdInsertar.Parameters.AddWithValue("@idPromocion", idPromocion);
                                cmdInsertar.Parameters.AddWithValue("@cantidad", cantidad);
                                idPedidoInsertado = Convert.ToInt32(cmdInsertar.ExecuteScalar());
                            }

                            // Guardar la nota si existe
                            if (!string.IsNullOrEmpty(nota))
                            {
                                GuardarNotaPedido(idPedidoInsertado, nota, transaction);
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        mensajeInventario = $"Error al agregar promoción: {ex.Message}";
                        return false;
                    }
                }
            }
        }


        public static bool GuardarNotaPedido(int idPedido, string nota, MySqlTransaction transaction = null)
        {
            bool usarTransaccionExterna = transaction != null;
            MySqlConnection conexion = null;
            MySqlTransaction transaccionLocal = null;

            try
            {
                if (!usarTransaccionExterna)
                {
                    conexion = new Conexion().EstablecerConexion();
                    transaccionLocal = conexion.BeginTransaction();
                }

                // Primero eliminar cualquier nota existente
                string queryEliminar = "DELETE FROM notas_pedidos WHERE id_pedido = @idPedido";
                using (MySqlCommand cmdEliminar = new MySqlCommand(queryEliminar,
                       usarTransaccionExterna ? transaction.Connection : conexion,
                       usarTransaccionExterna ? transaction : transaccionLocal))
                {
                    cmdEliminar.Parameters.AddWithValue("@idPedido", idPedido);
                    cmdEliminar.ExecuteNonQuery();
                }

                // Si hay una nota para insertar, la insertamos
                if (!string.IsNullOrWhiteSpace(nota))
                {
                    string queryInsertar = "INSERT INTO notas_pedidos (id_pedido, nota) VALUES (@idPedido, @nota)";
                    using (MySqlCommand cmdInsertar = new MySqlCommand(queryInsertar,
                           usarTransaccionExterna ? transaction.Connection : conexion,
                           usarTransaccionExterna ? transaction : transaccionLocal))
                    {
                        cmdInsertar.Parameters.AddWithValue("@idPedido", idPedido);
                        cmdInsertar.Parameters.AddWithValue("@nota", nota);
                        cmdInsertar.ExecuteNonQuery();
                    }
                }

                if (!usarTransaccionExterna)
                {
                    transaccionLocal.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!usarTransaccionExterna && transaccionLocal != null)
                {
                    transaccionLocal.Rollback();
                }
                MessageBox.Show($"Error al guardar nota: {ex.Message}");
                return false;
            }
            finally
            {
                if (!usarTransaccionExterna && conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static string ObtenerNotaPedido(int idPedido)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = "SELECT nota FROM notas_pedidos WHERE id_pedido = @idPedido";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPedido", idPedido);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? string.Empty;
                }
            }
        }


        public static bool AgregarPromocionAOrdenForzado(int idOrden, int idPromocion, int cantidad, string nota)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                using (MySqlTransaction transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // Similar a AgregarPromocionAOrden pero sin verificar inventario
                        string queryVerificar = @"SELECT p.id_pedido, p.Cantidad 
                    FROM pedidos p
                    LEFT JOIN notas_pedidos np ON p.id_pedido = np.id_pedido
                    WHERE p.id_orden = @idOrden 
                    AND p.id_promocion = @idPromocion
                    AND (np.nota IS NULL OR np.nota = @nota OR @nota IS NULL)";

                        int idPedidoExistente = -1;
                        int cantidadExistente = 0;

                        using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conexion, transaction))
                        {
                            cmdVerificar.Parameters.AddWithValue("@idOrden", idOrden);
                            cmdVerificar.Parameters.AddWithValue("@idPromocion", idPromocion);
                            cmdVerificar.Parameters.AddWithValue("@nota", string.IsNullOrEmpty(nota) ? DBNull.Value : (object)nota);

                            using (MySqlDataReader reader = cmdVerificar.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    idPedidoExistente = reader.GetInt32("id_pedido");
                                    cantidadExistente = reader.GetInt32("Cantidad");
                                }
                            }
                        }

                        if (idPedidoExistente > 0)
                        {
                            string queryActualizar = @"UPDATE pedidos 
                        SET Cantidad = Cantidad + @cantidad 
                        WHERE id_pedido = @idPedido";

                            using (MySqlCommand cmdActualizar = new MySqlCommand(queryActualizar, conexion, transaction))
                            {
                                cmdActualizar.Parameters.AddWithValue("@idPedido", idPedidoExistente);
                                cmdActualizar.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdActualizar.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string queryInsertar = @"INSERT INTO pedidos 
                        (id_orden, id_estadoP, id_promocion, Cantidad) 
                        VALUES (@idOrden, 1, @idPromocion, @cantidad);
                        SELECT LAST_INSERT_ID();";

                            int idPedidoInsertado;
                            using (MySqlCommand cmdInsertar = new MySqlCommand(queryInsertar, conexion, transaction))
                            {
                                cmdInsertar.Parameters.AddWithValue("@idOrden", idOrden);
                                cmdInsertar.Parameters.AddWithValue("@idPromocion", idPromocion);
                                cmdInsertar.Parameters.AddWithValue("@cantidad", cantidad);
                                idPedidoInsertado = Convert.ToInt32(cmdInsertar.ExecuteScalar());
                            }

                            if (!string.IsNullOrEmpty(nota))
                            {
                                GuardarNotaPedido(idPedidoInsertado, nota, transaction);
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error al agregar promoción: {ex.Message}");
                        return false;
                    }
                }
            }
        }
    }
}