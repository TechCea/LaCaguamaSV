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
        private static string usuario = "root";
        private static string contrasenia = "root";
        private static string bd = "lacaguamabd";
        private static string ip = "localhost";
        private static string puerto = "3307"; // o 3307 si eres javier 

        string cadenaConexion = $"Server={ip};Port={puerto};Database={bd};User Id={usuario};Password={contrasenia};";



        public MySqlConnection EstablecerConexion()
        {
            try
            {
                conectar = new MySqlConnection(cadenaConexion);
                conectar.Open();
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
                                   "i.nombreAlimento AS 'Nombre Bebida', " + // Se ajusta al nuevo nombre del campo
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
                    string query = "SELECT i.nombreAlimento AS 'Nombre Bebida', " + // Se ajusta al nuevo nombre del campo
                                   "b.id_bebida AS 'ID Bebida', " +
                                   "b.precioUnitario AS 'Precio Unitario', " +
                                   "c.tipo AS 'Categoría' " +
                                   "FROM bebidas b " +
                                   "JOIN inventario i ON b.id_inventario = i.id_inventario " +
                                   "JOIN categorias c ON b.id_categoria = c.id_categoria " +
                                   "WHERE c.tipo = @categoria";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener bebidas por categoría: " + ex.Message);
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
                    using (MySqlTransaction transaccion = conexion.BeginTransaction()) // Se usa transacción para evitar inconsistencias
                    {
                        try
                        {
                            // Obtener el ID del inventario asociado a la bebida antes de eliminarla
                            string obtenerInventarioQuery = "SELECT id_inventario FROM bebidas WHERE id_bebida = @idBebida";
                            int idInventario = -1;

                            using (MySqlCommand cmdObtener = new MySqlCommand(obtenerInventarioQuery, conexion, transaccion))
                            {
                                cmdObtener.Parameters.AddWithValue("@idBebida", idBebida);
                                var result = cmdObtener.ExecuteScalar();
                                if (result != null)
                                {
                                    idInventario = Convert.ToInt32(result);
                                }
                            }

                            // Primero, eliminar la bebida de la tabla bebidas
                            string eliminarBebidaQuery = "DELETE FROM bebidas WHERE id_bebida = @idBebida";
                            using (MySqlCommand cmdEliminarBebida = new MySqlCommand(eliminarBebidaQuery, conexion, transaccion))
                            {
                                cmdEliminarBebida.Parameters.AddWithValue("@idBebida", idBebida);
                                cmdEliminarBebida.ExecuteNonQuery();
                            }

                            // Luego, eliminar el registro del inventario si existe
                            if (idInventario != -1)
                            {
                                string eliminarInventarioQuery = "DELETE FROM inventario WHERE id_inventario = @idInventario";
                                using (MySqlCommand cmdEliminarInventario = new MySqlCommand(eliminarInventarioQuery, conexion, transaccion))
                                {
                                    cmdEliminarInventario.Parameters.AddWithValue("@idInventario", idInventario);
                                    cmdEliminarInventario.ExecuteNonQuery();
                                }
                            }

                            transaccion.Commit(); // Confirmar la transacción
                            return true;
                        }
                        catch (Exception)
                        {
                            transaccion.Rollback(); // Deshacer cambios si hay un error
                            throw;
                        }
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
                    using (MySqlTransaction transaccion = conexion.BeginTransaction()) // Transacción para asegurar consistencia
                    {
                        try
                        {
                            // Obtener el ID de la categoría seleccionada
                            string queryCategoria = "SELECT id_categoria FROM categorias WHERE tipo = @tipo";
                            int idCategoria;

                            using (MySqlCommand cmdCategoria = new MySqlCommand(queryCategoria, conexion, transaccion))
                            {
                                cmdCategoria.Parameters.AddWithValue("@tipo", nuevaCategoria);
                                object resultado = cmdCategoria.ExecuteScalar();
                                if (resultado == null)
                                {
                                    MessageBox.Show("Categoría no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaccion.Rollback();
                                    return false;
                                }
                                idCategoria = Convert.ToInt32(resultado);
                            }

                            // Obtener el ID del inventario asociado a la bebida
                            string queryObtenerInventario = "SELECT id_inventario FROM bebidas WHERE id_bebida = @idBebida";
                            int idInventario;

                            using (MySqlCommand cmdInventario = new MySqlCommand(queryObtenerInventario, conexion, transaccion))
                            {
                                cmdInventario.Parameters.AddWithValue("@idBebida", idBebida);
                                object resultado = cmdInventario.ExecuteScalar();
                                if (resultado == null)
                                {
                                    MessageBox.Show("Inventario no encontrado para la bebida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaccion.Rollback();
                                    return false;
                                }
                                idInventario = Convert.ToInt32(resultado);
                            }

                            // Actualizar el nombre en la tabla Inventario (nombreAlimento en la nueva base de datos)
                            string queryActualizarInventario = "UPDATE inventario SET nombreAlimento = @nuevoNombre WHERE id_inventario = @idInventario";

                            using (MySqlCommand cmdActualizarInventario = new MySqlCommand(queryActualizarInventario, conexion, transaccion))
                            {
                                cmdActualizarInventario.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                                cmdActualizarInventario.Parameters.AddWithValue("@idInventario", idInventario);
                                cmdActualizarInventario.ExecuteNonQuery();
                            }

                            // Actualizar la categoría y el precio en la tabla Bebidas
                            string queryActualizarBebida = "UPDATE bebidas SET id_categoria = @idCategoria, precioUnitario = @nuevoPrecio WHERE id_bebida = @idBebida";

                            using (MySqlCommand cmdActualizarBebida = new MySqlCommand(queryActualizarBebida, conexion, transaccion))
                            {
                                cmdActualizarBebida.Parameters.AddWithValue("@idCategoria", idCategoria);
                                cmdActualizarBebida.Parameters.AddWithValue("@nuevoPrecio", nuevoPrecio);
                                cmdActualizarBebida.Parameters.AddWithValue("@idBebida", idBebida);
                                int filasAfectadas = cmdActualizarBebida.ExecuteNonQuery();

                                if (filasAfectadas > 0)
                                {
                                    transaccion.Commit(); // Confirmar la transacción
                                    return true;
                                }
                                else
                                {
                                    transaccion.Rollback();
                                    return false;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            transaccion.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la bebida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
                                   "i.nombreAlimento AS 'Nombre Plato', " +
                                   "p.precioUnitario AS 'Precio Unitario', " +
                                   "p.descripcion AS 'Descripción', " +
                                   "cp.tipo AS 'Categoría' " +
                                   "FROM platos p " +
                                   "JOIN inventario i ON p.id_inventario = i.id_inventario " +
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
                MessageBox.Show("Error al obtener comidas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                   "i.nombreAlimento AS 'Nombre Plato', " +
                                   "p.precioUnitario AS 'Precio Unitario', " +
                                   "p.descripcion AS 'Descripción', " +
                                   "cp.tipo AS 'Categoría' " +
                                   "FROM platos p " +
                                   "JOIN inventario i ON p.id_inventario = i.id_inventario " +
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
                MessageBox.Show("Error al obtener comidas por categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        //Función para Actualizar comidas
        public bool ActualizarPlato(int idPlato, string nombre, string descripcion, decimal precio, string categoria)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Primero actualizamos el nombre en la tabla inventario usando el id_inventario de la tabla platos
                    string queryInventario = "UPDATE inventario SET nombreAlimento = @nombre WHERE id_inventario = (SELECT id_inventario FROM platos WHERE id_plato = @idPlato)";
                    using (MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conexion))
                    {
                        cmdInventario.Parameters.AddWithValue("@nombre", nombre);
                        cmdInventario.Parameters.AddWithValue("@idPlato", idPlato);
                        cmdInventario.ExecuteNonQuery(); // Ejecutamos la actualización del nombre
                    }

                    // Ahora actualizamos los demás campos en la tabla platos (descripcion, precio, categoria)
                    string queryPlato = "UPDATE platos SET descripcion = @descripcion, precioUnitario = @precio, " +
                                        "id_categoriaP = (SELECT id_categoriaP FROM categoria_platos WHERE tipo = @categoria) " +
                                        "WHERE id_plato = @idPlato";

                    using (MySqlCommand cmdPlato = new MySqlCommand(queryPlato, conexion))
                    {
                        cmdPlato.Parameters.AddWithValue("@descripcion", descripcion);
                        cmdPlato.Parameters.AddWithValue("@precio", precio);
                        cmdPlato.Parameters.AddWithValue("@categoria", categoria);
                        cmdPlato.Parameters.AddWithValue("@idPlato", idPlato);
                        return cmdPlato.ExecuteNonQuery() > 0; // Ejecutamos la actualización del plato
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el plato: " + ex.Message);
                return false;
            }
        }

        //Función para eliminar comidas
        public bool EliminarPlato(int idPlato)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Primero, obtenemos el id_inventario del plato que se quiere eliminar
                    string queryInventario = "SELECT id_inventario FROM platos WHERE id_plato = @idPlato";
                    int idInventario = 0;

                    using (MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conexion))
                    {
                        cmdInventario.Parameters.AddWithValue("@idPlato", idPlato);
                        object result = cmdInventario.ExecuteScalar();
                        if (result != null)
                        {
                            idInventario = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("Plato no encontrado.");
                            return false;
                        }
                    }

                    // Ahora eliminamos el plato de la tabla platos
                    string queryPlato = "DELETE FROM platos WHERE id_plato = @idPlato";
                    using (MySqlCommand cmdPlato = new MySqlCommand(queryPlato, conexion))
                    {
                        cmdPlato.Parameters.AddWithValue("@idPlato", idPlato);
                        int rowsAffected = cmdPlato.ExecuteNonQuery();

                        // Si se eliminó correctamente el plato, revisamos si debemos eliminar el inventario
                        if (rowsAffected > 0)
                        {
                            // Comprobamos si hay más platos usando el mismo id_inventario
                            string queryInventarioUso = "SELECT COUNT(*) FROM platos WHERE id_inventario = @idInventario";
                            using (MySqlCommand cmdInventarioUso = new MySqlCommand(queryInventarioUso, conexion))
                            {
                                cmdInventarioUso.Parameters.AddWithValue("@idInventario", idInventario);
                                int count = Convert.ToInt32(cmdInventarioUso.ExecuteScalar());

                                // Si no hay más platos que usen ese id_inventario, eliminamos el inventario
                                if (count == 0)
                                {
                                    string queryEliminarInventario = "DELETE FROM inventario WHERE id_inventario = @idInventario";
                                    using (MySqlCommand cmdEliminarInventario = new MySqlCommand(queryEliminarInventario, conexion))
                                    {
                                        cmdEliminarInventario.Parameters.AddWithValue("@idInventario", idInventario);
                                        cmdEliminarInventario.ExecuteNonQuery();
                                    }
                                }
                            }
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el plato.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el plato: " + ex.Message);
                return false;
            }
        }

        //Función para obtener extras
        public DataTable ObtenerExtras()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    // La consulta ahora toma el nombre de extra desde la tabla 'inventario' y el precio desde 'extras'
                    string query = "SELECT e.id_extra AS 'ID', i.nombreAlimento AS 'Nombre', e.precioUnitario AS 'Precio Unitario' " +
                                   "FROM extras e " +
                                   "JOIN inventario i ON e.id_inventario = i.id_inventario";

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

        //Función para elimiminar extra
        public bool EliminarExtra(int idExtra)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Primero, obtenemos el id_inventario relacionado con el extra
                    string obtenerInventarioQuery = "SELECT id_inventario FROM extras WHERE id_extra = @idExtra";
                    int idInventario = 0;

                    using (MySqlCommand cmd = new MySqlCommand(obtenerInventarioQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idExtra", idExtra);
                        var result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            idInventario = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el extra con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    // Eliminamos el extra de la tabla extras
                    string eliminarExtraQuery = "DELETE FROM extras WHERE id_extra = @idExtra";

                    using (MySqlCommand cmd = new MySqlCommand(eliminarExtraQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idExtra", idExtra);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            // Verificamos si hay más registros en extras que utilicen el mismo id_inventario
                            string verificarExtrasQuery = "SELECT COUNT(*) FROM extras WHERE id_inventario = @idInventario";
                            using (MySqlCommand cmdVerificar = new MySqlCommand(verificarExtrasQuery, conexion))
                            {
                                cmdVerificar.Parameters.AddWithValue("@idInventario", idInventario);
                                int count = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                                // Si no hay más registros en extras con este id_inventario, podemos eliminar el inventario
                                if (count == 0)
                                {
                                    string eliminarInventarioQuery = "DELETE FROM inventario WHERE id_inventario = @idInventario";
                                    using (MySqlCommand cmdInventario = new MySqlCommand(eliminarInventarioQuery, conexion))
                                    {
                                        cmdInventario.Parameters.AddWithValue("@idInventario", idInventario);
                                        cmdInventario.ExecuteNonQuery();
                                    }
                                }
                            }
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el extra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //Actualizar datos del extra
        public bool ActualizarExtra(int idExtra, string nuevoNombre, decimal nuevoPrecio)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Obtenemos el id_inventario del extra seleccionado
                    string obtenerInventarioQuery = "SELECT id_inventario FROM extras WHERE id_extra = @idExtra";
                    int idInventario = 0;

                    using (MySqlCommand cmd = new MySqlCommand(obtenerInventarioQuery, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idExtra", idExtra);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            idInventario = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el extra con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    // Actualizamos la tabla extras
                    string query = "UPDATE extras SET precioUnitario = @precio WHERE id_extra = @idExtra";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@precio", nuevoPrecio);
                        cmd.Parameters.AddWithValue("@idExtra", idExtra);
                        cmd.ExecuteNonQuery();
                    }

                    // Si el nombre cambió, actualizamos también el inventario
                    if (!string.IsNullOrEmpty(nuevoNombre))
                    {
                        string queryInventario = "UPDATE inventario SET nombreAlimento = @nuevoNombre WHERE id_inventario = @idInventario";
                        using (MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conexion))
                        {
                            cmdInventario.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                            cmdInventario.Parameters.AddWithValue("@idInventario", idInventario);
                            cmdInventario.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el extra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        // caja incial
        public decimal ObtenerCajaInicial(DateTime fecha)
        {
            decimal cajaInicial = 0;
            string query = @"SELECT cantidad FROM caja WHERE DATE(fecha) = @fecha LIMIT 1";

            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd"));
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            cajaInicial = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener la caja inicial: " + ex.Message);
                    }
                }
            }
            return cajaInicial;
        }

        // Corte de caja


        public decimal ObtenerTotalEfectivo(DateTime fecha)
        {
            decimal total = 0;
            string query = @"
                            SELECT COALESCE(SUM(o.total), 0) 
                            FROM ordenes o
                            INNER JOIN tipopago t ON o.id_pago = t.id_pago
                            WHERE t.nombrePago = 'Efectivo' AND DATE(o.fecha) = @fecha;
                                ";

            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha.Date);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            total = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener ingresos en efectivo: " + ex.Message);
                    }
                }
            }
            return total;
        }
        public decimal ObtenerTotalGeneradoEfectivo(DateTime fecha)
        {
            decimal total = 0;
            string query = @"
                            SELECT COALESCE(SUM(o.total), 0)
                            FROM ordenes o
                            INNER JOIN tipopago t ON o.id_pago = t.id_pago
                            WHERE t.nombrePago = 'Efectivo' AND DATE(o.fecha) = @fecha;
                        ";

            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd"));

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            total = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener total generado: " + ex.Message);
                    }
                }
            }
            return total;
        }
        public decimal ObtenerTotalGenerado(DateTime fecha)
        {
            decimal totalGenerado = 0;

            string query = @"
                        SELECT 
                            IFNULL(SUM(o.total - IFNULL(o.descuento, 0)), 0) AS totalGenerado
                        FROM 
                            ordenes o
                        WHERE 
                            o.tipo_pago = 1  -- Solo efectivo
                            AND DATE(o.fecha_orden) = @Fecha  -- Solo las órdenes de la fecha seleccionada
                            AND o.id_estadoO = 2;  -- Solo órdenes cerradas";

            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                // Convertimos la fecha a string antes de pasarla como parámetro
                cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("yyyy-MM-dd"));

                try
                {
                    conn.Open(); // Asegurarse de que la conexión esté abierta antes de ejecutar la consulta
                    object result = cmd.ExecuteScalar(); // Ejecutamos la consulta

                    if (result != null && result != DBNull.Value) // Verificamos si el resultado no es nulo o DBNull
                    {
                        totalGenerado = Convert.ToDecimal(result); // Convertimos el resultado a decimal
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener el total generado: {ex.Message}");
                }
            }

            return totalGenerado;

        }

        public decimal ObtenerTotalGastos(DateTime fecha)
        {
            decimal total = 0;
            string query = @"
                        SELECT COALESCE(SUM(cantidad), 0)
                        FROM gastos
                        WHERE DATE(fecha) = @fecha;
                    ";

            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha.Date);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            total = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener los gastos: " + ex.Message);
                    }
                }
            }
            return total;
        }

        // 🔹 Nueva función para actualizar el estado de la mesa
        public bool ActualizarEstadoMesa(int idMesa, int nuevoEstado)
        {
            bool actualizado = false;

            using (MySqlConnection conn = EstablecerConexion())
            {
                if (conn == null)
                {
                    MessageBox.Show("No se pudo conectar a la base de datos.");
                    return false;
                }

                try
                {
                    string query = "UPDATE mesas SET id_estadoM = @nuevoEstado WHERE id_mesa = @idMesa";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@idMesa", idMesa);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        actualizado = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar mesa: " + ex.Message);
                }
            }
            return actualizado;
        }
        // Iniciar Caja
        public bool RegistrarCajaInicial(decimal cantidad, int idUsuario)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "INSERT INTO caja (monto_inicial, id_usuario, estado_caja, fecha) VALUES (@cantidad, @idUsuario, 1, @fecha)";
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@cantidad", cantidad);
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@fecha", DateTime.Now);

                    try
                    {
                        conexion.Open();
                        return comando.ExecuteNonQuery() > 0; // Si se insertó correctamente, devuelve true
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public bool CajaInicialYaEstablecida()
        {
            
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "SELECT COUNT(*) FROM caja WHERE DATE(fecha) = CURDATE()";
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    try
                    {
                        conexion.Open();
                        int count = Convert.ToInt32(comando.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}



