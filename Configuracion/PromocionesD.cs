using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Configuracion
{
    class PromocionesD
    {
        public static DataTable ObtenerTodasPromociones()
        {
            DataTable dt = new DataTable();
            using (var conexion = new Conexion().EstablecerConexion())
            {
                string query = @"SELECT id_promocion, nombre, precio_especial, 
                           fecha_inicio, fecha_fin, activa 
                           FROM promociones";
                new MySqlDataAdapter(query, conexion).Fill(dt);
            }
            return dt;
        }

        public static DataTable ObtenerPromocionPorId(int id)
        {
            DataTable dt = new DataTable();
            using (var conexion = new Conexion().EstablecerConexion())
            {
                string query = @"SELECT * FROM promociones WHERE id_promocion = @id";
                var cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);
                new MySqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public static DataTable ObtenerItemsPromocion(int idPromocion)
        {
            DataTable dt = new DataTable();
            using (var conexion = new Conexion().EstablecerConexion())
            {
                string query = @"
            SELECT 
                pi.tipo_item, 
                pi.id_item, 
                pi.cantidad,
                CASE 
                    WHEN pi.tipo_item = 'PLATO' THEN p.nombrePlato
                    WHEN pi.tipo_item = 'BEBIDA' THEN i.nombreProducto
                    WHEN pi.tipo_item = 'EXTRA' THEN i.nombreProducto
                END AS nombre
            FROM promocion_items pi
            LEFT JOIN platos p ON pi.tipo_item = 'PLATO' AND pi.id_item = p.id_plato
            LEFT JOIN bebidas b ON pi.tipo_item = 'BEBIDA' AND pi.id_item = b.id_bebida
            LEFT JOIN extras e ON pi.tipo_item = 'EXTRA' AND pi.id_item = e.id_extra
            LEFT JOIN inventario i ON (b.id_inventario = i.id_inventario OR e.id_inventario = i.id_inventario)
            WHERE pi.id_promocion = @id";

                var cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", idPromocion);
                new MySqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public static int CrearPromocion(string nombre, string descripcion, decimal precio,
                                        DateTime inicio, DateTime? fin, bool activa)
        {
            using (var conexion = new Conexion().EstablecerConexion())
            {
                string query = @"INSERT INTO promociones 
                           (nombre, descripcion, precio_especial, fecha_inicio, fecha_fin, activa)
                           VALUES (@nombre, @desc, @precio, @inicio, @fin, @activa);
                           SELECT LAST_INSERT_ID();";

                var cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@desc", descripcion);
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fin", fin ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@activa", activa);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static bool AgregarItemAPromocion(int idPromocion, string tipo, int idItem, int cantidad)
        {
            using (var conexion = new Conexion().EstablecerConexion())
            {
                string query = @"INSERT INTO promocion_items 
                           (id_promocion, tipo_item, id_item, cantidad)
                           VALUES (@idPromo, @tipo, @idItem, @cant)";

                var cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idPromo", idPromocion);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@idItem", idItem);
                cmd.Parameters.AddWithValue("@cant", cantidad);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool GuardarPromocionCompleta(int? idPromocion, string nombre, string descripcion, decimal precio,
                                          DateTime inicio, DateTime? fin, bool activa, DataTable items)
        {
            using (var conexion = new Conexion().EstablecerConexion())
            {
                using (var transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        int idPromo;

                        if (idPromocion.HasValue)
                        {
                            // Actualizar promoción existente
                            idPromo = idPromocion.Value;
                            string updateQuery = @"UPDATE promociones SET 
                                        nombre = @nombre,
                                        descripcion = @desc,
                                        precio_especial = @precio,
                                        fecha_inicio = @inicio,
                                        fecha_fin = @fin,
                                        activa = @activa
                                        WHERE id_promocion = @id";

                            var updateCmd = new MySqlCommand(updateQuery, conexion, transaction);
                            updateCmd.Parameters.AddWithValue("@nombre", nombre);
                            updateCmd.Parameters.AddWithValue("@desc", descripcion);
                            updateCmd.Parameters.AddWithValue("@precio", precio);
                            updateCmd.Parameters.AddWithValue("@inicio", inicio);
                            updateCmd.Parameters.AddWithValue("@fin", fin ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@activa", activa);
                            updateCmd.Parameters.AddWithValue("@id", idPromo);
                            updateCmd.ExecuteNonQuery();

                            // Eliminar items antiguos
                            string deleteQuery = "DELETE FROM promocion_items WHERE id_promocion = @id";
                            var deleteCmd = new MySqlCommand(deleteQuery, conexion, transaction);
                            deleteCmd.Parameters.AddWithValue("@id", idPromo);
                            deleteCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            // Crear nueva promoción
                            string insertQuery = @"INSERT INTO promociones 
                                        (nombre, descripcion, precio_especial, fecha_inicio, fecha_fin, activa)
                                        VALUES (@nombre, @desc, @precio, @inicio, @fin, @activa);
                                        SELECT LAST_INSERT_ID();";

                            var insertCmd = new MySqlCommand(insertQuery, conexion, transaction);
                            insertCmd.Parameters.AddWithValue("@nombre", nombre);
                            insertCmd.Parameters.AddWithValue("@desc", descripcion);
                            insertCmd.Parameters.AddWithValue("@precio", precio);
                            insertCmd.Parameters.AddWithValue("@inicio", inicio);
                            insertCmd.Parameters.AddWithValue("@fin", fin ?? (object)DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@activa", activa);

                            idPromo = Convert.ToInt32(insertCmd.ExecuteScalar());
                        }

                        // Insertar nuevos items
                        foreach (DataRow row in items.Rows)
                        {
                            string itemQuery = @"INSERT INTO promocion_items 
                              (id_promocion, tipo_item, id_item, cantidad)
                              VALUES (@idPromo, @tipo, @idItem, @cant)";

                            var itemCmd = new MySqlCommand(itemQuery, conexion, transaction);
                            itemCmd.Parameters.AddWithValue("@idPromo", idPromo);
                            itemCmd.Parameters.AddWithValue("@tipo", row["Tipo"].ToString());
                            itemCmd.Parameters.AddWithValue("@idItem", Convert.ToInt32(row["ID"]));
                            itemCmd.Parameters.AddWithValue("@cant", Convert.ToInt32(row["Cantidad"]));
                            itemCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al guardar la promoción: " + ex.Message);
                    }
                }
            }
        }
    }
}