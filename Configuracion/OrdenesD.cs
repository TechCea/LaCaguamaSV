using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Configuracion
{
    class OrdenesD
    {
        public static DataTable ObtenerOrdenes()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string query = @"SELECT 
                                o.id_orden, 
                                o.nombreCliente, 
                                o.total, 
                                o.descuento, 
                                o.fecha_orden, 
                                m.nombreMesa AS numero_mesa, 
                                tp.nombrePago AS tipo_pago, 
                                u.nombre AS nombre_usuario,
                                eo.nombreEstadoO AS estado_orden
                            FROM ordenes o
                            INNER JOIN mesas m ON o.id_mesa = m.id_mesa
                            INNER JOIN tipoPago tp ON o.tipo_pago = tp.id_pago
                            INNER JOIN usuarios u ON o.id_usuario = u.id_usuario
                            INNER JOIN estado_orden eo ON o.id_estadoO = eo.id_estadoO";

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener órdenes: " + ex.Message);
                }
            }
            return dt;
        }

        public static DataTable ObtenerTiposPago()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string query = "SELECT id_pago, nombrePago FROM tipoPago";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener tipos de pago: " + ex.Message);
                }
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
            int idUsuario = SesionUsuario.IdUsuario; // Obtenemos el ID del usuario de la sesión

            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    // Iniciar transacción
                    using (MySqlTransaction transaction = conexion.BeginTransaction())
                    {
                        try
                        {
                            // 1. Crear la orden vacía (estadoO = 1 "Abierta" por defecto)
                            string queryOrden = @"INSERT INTO ordenes 
                                    (nombreCliente, total, descuento, id_mesa, tipo_pago, id_usuario, id_estadoO) 
                                    VALUES (@nombre, 0, 0, @idMesa, @tipoPago, @idUsuario, 1);
                                    SELECT LAST_INSERT_ID();";

                            using (MySqlCommand cmdOrden = new MySqlCommand(queryOrden, conexion, transaction))
                            {
                                cmdOrden.Parameters.AddWithValue("@nombre", nombreCliente);
                                cmdOrden.Parameters.AddWithValue("@idMesa", idMesa);
                                cmdOrden.Parameters.AddWithValue("@tipoPago", tipoPago);
                                cmdOrden.Parameters.AddWithValue("@idUsuario", idUsuario);

                                idOrden = Convert.ToInt32(cmdOrden.ExecuteScalar());
                            }

                            // 2. Actualizar estado de la mesa a "Ocupado" (id_estadoM = 2)
                            string queryMesa = "UPDATE mesas SET id_estadoM = 2 WHERE id_mesa = @idMesa";
                            using (MySqlCommand cmdMesa = new MySqlCommand(queryMesa, conexion, transaction))
                            {
                                cmdMesa.Parameters.AddWithValue("@idMesa", idMesa);
                                cmdMesa.ExecuteNonQuery();
                            }

                            // Commit de la transacción
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Rollback en caso de error
                            transaction.Rollback();
                            Console.WriteLine("Error al crear la orden vacía: " + ex.Message);
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error de conexión: " + ex.Message);
                    return -1;
                }
            }
            return idOrden;
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
        AND i.cantidad < 10"; // Umbral bajo de stock

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
        AND i.cantidad < 10"; // Umbral bajo de stock

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

        public static string VerificarInventarioPromocion(int idPromocion)
        {
            StringBuilder mensaje = new StringBuilder();
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                // Consulta mejorada que verifica todos los componentes
                string query = @"
        SELECT 
            i.id_inventario,
            i.nombreProducto,
            i.cantidad AS stock_actual,
            SUM(
                CASE 
                    WHEN pi.tipo_item = 'PLATO' THEN 
                        (SELECT SUM(r.cantidad_necesaria) 
                         FROM recetas r 
                         WHERE r.id_plato = pi.id_item)
                    WHEN pi.tipo_item IN ('BEBIDA', 'EXTRA') THEN 1
                    ELSE 0
                END
            ) AS cantidad_necesaria
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
        GROUP BY i.id_inventario, i.nombreProducto, i.cantidad
        HAVING i.cantidad < cantidad_necesaria OR i.cantidad < 10"; // Umbral de stock bajo

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idPromocion", idPromocion);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string producto = reader.GetString("nombreProducto");
                            decimal stock = reader.GetDecimal("stock_actual");
                            decimal necesaria = reader.GetDecimal("cantidad_necesaria");

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

        public static bool AgregarPromocionAOrden(int idOrden, int idPromocion, int cantidad)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                string query = "INSERT INTO pedidos (id_orden, id_estadoP, id_promocion, Cantidad) " +
                              "VALUES (@idOrden, 1, @idPromocion, @cantidad)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idOrden", idOrden);
                    cmd.Parameters.AddWithValue("@idPromocion", idPromocion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

       

    }
}