using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Configuracion
{
    class Conexion
    {
        private MySqlConnection conectar = null;
        //hola
        private static string usuario = "root";
        private static string contrasenia = "root";
        private static string bd = "lacaguamabd";
        private static string ip = "localhost";
        private static string puerto = "3306";

        string cadenaConexion = $"Server={ip};Port={puerto};Database={bd};User Id={usuario};Password={contrasenia};";



        public MySqlConnection EstablecerConexion()
        {
            try
            {
                conectar = new MySqlConnection(cadenaConexion);
                conectar.Open();
                MessageBox.Show("Conexión establecida");
                return conectar;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al establecer la conexión");
            }
            return conectar;
        }


        public void CerrarConexion()
        {
            try
            {
                if (conectar != null && conectar.State == System.Data.ConnectionState.Open)
                    conectar.Close();
                MessageBox.Show("Conexión cerrada");

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cerrar la conexión" + e.ToString());
            }
        }


        public Tuple<int, int> ObtenerDatosUsuario(string usuario, string contrasena)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT id_usuario, id_rol FROM usuarios WHERE usuario = @usuario AND contrasenya = @contrasena";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idUsuario = reader.GetInt32(0);
                                int idRol = reader.GetInt32(1);
                                return Tuple.Create(idUsuario, idRol);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar usuario: " + ex.Message);
            }
            return null;
        }
        // Obtener todos los usuarios
        public DataTable ObtenerUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT u.id_usuario AS 'ID', u.usuario AS 'Usuario', u.nombre AS 'Nombre', " +
                                   "u.correo AS 'Correo', u.telefono_contacto AS 'Teléfono', " +
                                   "u.contrasenya AS 'Contraseña', r.nombre_rol AS 'Rol' " +
                                   "FROM usuarios u JOIN roles r ON u.id_rol = r.id_rol";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener usuarios: " + ex.Message);
            }
            return dt;
        }

        public bool AgregarUsuario(string nombre, string correo, string usuario, string contrasena, string telefono, int idRol)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "INSERT INTO usuarios (nombre, correo, usuario, contrasenya, telefono_contacto, id_rol) " +
                                   "VALUES (@nombre, @correo, @usuario, @contrasena, @telefono, @idRol)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@idRol", idRol);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar usuario: " + ex.Message);
                return false;
            }
        }
        public DataTable ObtenerRoles()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT id_rol, nombre_rol FROM roles";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener roles: " + ex.Message);
            }
            return dt;
        }

        public bool EliminarUsuario(int idUsuario)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "DELETE FROM usuarios WHERE id_usuario = @idUsuario";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar usuario: " + ex.Message);
                return false;
            }
        }


        public bool EditarUsuario(int idUsuario, string usuario, string nombre, string correo, string contrasena, string telefono, int idRol)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "UPDATE usuarios SET usuario = @usuario, nombre = @nombre, correo = @correo, " +
                                   "contrasenya = @contrasena, telefono_contacto = @telefono, id_rol = @idRol " +
                                   "WHERE id_usuario = @idUsuario";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@idRol", idRol);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar usuario: " + ex.Message);
                return false;
            }
        }

        //Funcion para obtener las bebidas
        public DataTable ObtenerBebidas()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT b.id_bebida AS 'ID Bebida', " +
                                   "i.nombreBebida AS 'Nombre Bebida', " +
                                   "c.tipo AS 'Categoría', " +
                                   "b.precioUnitario AS 'Precio Unitario' " +
                                   "FROM bebidas b " +
                                   "JOIN inventario i ON b.id_inventario = i.id_inventario " +
                                   "JOIN categorias c ON b.id_categoria = c.id_categoria";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener bebidas: " + ex.Message);
            }
            return dt;
        }


        //Filtro para las categorias de bebidas
        public DataTable ObtenerBebidasPorCategoria(string categoria)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT i.nombreBebida AS 'Nombre Bebida', " +
                                   "b.id_bebida AS 'ID Bebida', b.precioUnitario AS 'Precio Unitario', " +
                                   "c.tipo AS 'Categoría' " +
                                   "FROM bebidas b " +
                                   "JOIN inventario i ON b.id_inventario = i.id_inventario " +
                                   "JOIN categorias c ON b.id_categoria = c.id_categoria " +
                                   "WHERE c.tipo = @categoria";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener bebidas por categoría: " + ex.Message);
            }
            return dt;
        }

        public DataTable ObtenerCategorias()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT tipo FROM categorias";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener categorías: " + ex.Message);
            }
            return dt;
        }


        //Funcion para obtener las Comidas
        public DataTable ObtenerComidas()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT p.id_plato AS 'ID Plato', " +
                                   "p.nombrePlato AS 'Nombre Plato', " +
                                   "p.precioUnitario AS 'Precio Unitario', " +
                                   "p.descripcion AS 'Descripción', " +
                                   "cp.tipo AS 'Categoría' " +
                                   "FROM platos p " +
                                   "JOIN categoria_platos cp ON p.id_categoriaP = cp.id_categoriaP";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener comidas: " + ex.Message);
            }
            return dt;
        }

        //Función para obtener comidas filtradas por categoría
        public DataTable ObtenerComidasPorCategoria(string categoria)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT p.id_plato AS 'ID Plato', " +
                                   "p.nombrePlato AS 'Nombre Plato', " +
                                   "p.precioUnitario AS 'Precio Unitario', " +
                                   "p.descripcion AS 'Descripción', " +
                                   "cp.tipo AS 'Categoría' " +
                                   "FROM platos p " +
                                   "JOIN categoria_platos cp ON p.id_categoriaP = cp.id_categoriaP " +
                                   "WHERE cp.tipo = @categoria";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener comidas por categoría: " + ex.Message);
            }
            return dt;
        }

        //Función para obtener las categorías de comidas
        public DataTable ObtenerCategoriasComida()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT tipo FROM categoria_platos";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener categorías de comidas: " + ex.Message);
            }
            return dt;
        }

        public bool EliminarBebida(int idBebida)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "DELETE FROM bebidas WHERE id_bebida = @idBebida";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idBebida", idBebida);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la bebida: " + ex.Message);
                return false;
            }
        }

        //Actualizar nombre de bebidas (en inventario), precio y categoria en la tabla bebidas
        public bool ActualizarBebida(int idBebida, string nuevoNombre, string nuevaCategoria, decimal nuevoPrecio)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Obtener el ID de la categoría seleccionada
                    string queryCategoria = "SELECT id_categoria FROM categorias WHERE tipo = @tipo";
                    int idCategoria;

                    using (MySqlCommand cmdCategoria = new MySqlCommand(queryCategoria, conexion))
                    {
                        cmdCategoria.Parameters.AddWithValue("@tipo", nuevaCategoria);
                        object resultado = cmdCategoria.ExecuteScalar();
                        if (resultado == null)
                        {
                            MessageBox.Show("Categoría no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        idCategoria = Convert.ToInt32(resultado);
                    }

                    // Actualizar el nombre en la tabla Inventario
                    string queryInventario = "UPDATE inventario SET nombreBebida = @nuevoNombre WHERE id_inventario = (SELECT id_inventario FROM bebidas WHERE id_bebida = @idBebida)";

                    using (MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conexion))
                    {
                        cmdInventario.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                        cmdInventario.Parameters.AddWithValue("@idBebida", idBebida);
                        cmdInventario.ExecuteNonQuery();
                    }

                    // Actualizar la categoría y el precio en la tabla Bebidas
                    string queryBebida = "UPDATE bebidas SET id_categoria = @idCategoria, precioUnitario = @nuevoPrecio WHERE id_bebida = @idBebida";

                    using (MySqlCommand cmdBebida = new MySqlCommand(queryBebida, conexion))
                    {
                        cmdBebida.Parameters.AddWithValue("@idCategoria", idCategoria);
                        cmdBebida.Parameters.AddWithValue("@nuevoPrecio", nuevoPrecio);
                        cmdBebida.Parameters.AddWithValue("@idBebida", idBebida);
                        int filasAfectadas = cmdBebida.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la bebida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable ObtenerExtras()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT id_extra AS 'ID', nombre AS 'Nombre', precioUnitario AS 'Precio Unitario' FROM extras";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los extras: " + ex.Message);
            }
            return dt;
        }

        public bool EliminarExtra(int idExtra)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "DELETE FROM extras WHERE id_extra = @idExtra";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idExtra", idExtra);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el extra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool AgregarExtra(string nombre, decimal precio)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "INSERT INTO extras (nombre, precioUnitario) VALUES (@nombre, @precio)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@precio", precio);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el extra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ActualizarExtra(int idExtra, string nuevoNombre, decimal nuevoPrecio)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "UPDATE extras SET nombre = @nombre, precioUnitario = @precio WHERE id_extra = @idExtra";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@precio", nuevoPrecio);
                        cmd.Parameters.AddWithValue("@idExtra", idExtra);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el extra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


    }
}