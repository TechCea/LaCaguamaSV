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
                    string query = @"SELECT o.id_orden, o.nombreCliente, o.total, o.descuento, o.fecha_pedido, 
                                    m.nombreMesa AS numero_mesa, 
                                    tp.nombrePago AS tipo_pago, 
                                    u.nombre AS nombre_usuario
                             FROM ordenes o
                             INNER JOIN mesas m ON o.id_mesa = m.id_mesa
                             INNER JOIN tipoPago tp ON o.tipo_pago = tp.id_pago
                             INNER JOIN usuarios u ON o.id_usuario = u.id_usuario";

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

        public static void AgregarOrden(string nombreCliente, decimal total, decimal descuento, int idMesa, int tipoPago, int idUsuario)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string query = "INSERT INTO ordenes (nombreCliente, total, descuento, id_mesa, tipo_pago, id_usuario) VALUES (@nombre, @total, @descuento, @mesa, @pago, @usuario)";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombre", nombreCliente);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@descuento", descuento);
                    cmd.Parameters.AddWithValue("@mesa", idMesa);
                    cmd.Parameters.AddWithValue("@pago", tipoPago);
                    cmd.Parameters.AddWithValue("@usuario", idUsuario);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar orden: " + ex.Message);
                }
            }
        }

        public static void ActualizarOrden(int idOrden, decimal total, decimal descuento, int idMesa, int tipoPago)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string query = "UPDATE ordenes SET total = @total, descuento = @descuento, id_mesa = @mesa, tipo_pago = @pago WHERE id_orden = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@descuento", descuento);
                    cmd.Parameters.AddWithValue("@mesa", idMesa);
                    cmd.Parameters.AddWithValue("@pago", tipoPago);
                    cmd.Parameters.AddWithValue("@id", idOrden);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar orden: " + ex.Message);
                }
            }
        }

        public static void EliminarOrden(int idOrden)
        {
            using (MySqlConnection conexion = new Conexion().EstablecerConexion())
            {
                try
                {
                    string query = "DELETE FROM ordenes WHERE id_orden = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@id", idOrden);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar orden: " + ex.Message);
                }
            }
        }
    }
}
