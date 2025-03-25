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

    }
}
