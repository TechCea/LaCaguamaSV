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
        private static string contrasenia = "slenderman";
        private static string bd = "lacaguamabd";
        private static string ip = "localhost";
        private static string puerto = "3306"; // o 3307 si eres javier 

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

        public bool ExisteCampo(string campo, string valor, int? idUsuario = null)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = $"SELECT COUNT(*) FROM usuarios WHERE {campo} = @valor";

                    if (idUsuario.HasValue)
                    {
                        query += " AND id_usuario != @idUsuario";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@valor", valor);
                        if (idUsuario.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@idUsuario", idUsuario.Value);
                        }

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar {campo}: " + ex.Message);
                return false;
            }
        }

        public bool ValidarCredencialesUnicas(string usuario, string nombre, string contrasena)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario OR nombre = @nombre OR contrasenya = @contrasena";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar credenciales únicas: " + ex.Message);
                return false;
            }
        }

        public bool AgregarUsuario(string nombre, string correo, string usuario, string contrasena, string telefono, int idRol)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Verificar si ya existe el usuario, nombre o contraseña
                    if (!ValidarCredencialesUnicas(usuario, nombre, contrasena))
                    {
                        MessageBox.Show("El nombre de usuario, nombre completo o contraseña ya están en uso");
                        return false;
                    }

                    // Verificar si el correo ya existe
                    if (ExisteCampo("correo", correo))
                    {
                        MessageBox.Show("Este correo electrónico ya está registrado");
                        return false;
                    }

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

                    // Verificar si el nuevo usuario ya existe en otro registro
                    if (ExisteCampo("usuario", usuario, idUsuario))
                    {
                        MessageBox.Show("Este nombre de usuario ya está en uso por otro usuario");
                        return false;
                    }

                    // Verificar si el nuevo nombre ya existe en otro registro
                    if (ExisteCampo("nombre", nombre, idUsuario))
                    {
                        MessageBox.Show("Este nombre completo ya está en uso por otro usuario");
                        return false;
                    }

                    // Verificar si la nueva contraseña ya existe en otro registro
                    if (ExisteCampo("contrasenya", contrasena, idUsuario))
                    {
                        MessageBox.Show("Esta contraseña ya está en uso por otro usuario");
                        return false;
                    }

                    // Verificar si el nuevo correo ya existe en otro registro
                    if (ExisteCampo("correo", correo, idUsuario))
                    {
                        MessageBox.Show("Este correo electrónico ya está registrado por otro usuario");
                        return false;
                    }

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
                                   "i.nombreProducto AS 'Nombre Bebida', " + // Se ajusta al nuevo nombre del campo
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
                    string query = "SELECT i.nombreProducto AS 'Nombre Bebida', " + // Se ajusta al nuevo nombre del campo
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
                    using (MySqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            // Obtener el ID de la categoría
                            string queryCategoria = "SELECT id_categoria FROM categorias WHERE tipo = @tipo";
                            int idCategoria;

                            using (MySqlCommand cmdCategoria = new MySqlCommand(queryCategoria, conexion, transaccion))
                            {
                                cmdCategoria.Parameters.AddWithValue("@tipo", nuevaCategoria);
                                object resultado = cmdCategoria.ExecuteScalar();

                                if (resultado == null)
                                {
                                    MessageBox.Show($"Categoría '{nuevaCategoria}' no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaccion.Rollback();
                                    return false;
                                }
                                idCategoria = Convert.ToInt32(resultado);
                            }

                            // Obtener el ID del inventario asociado
                            string queryObtenerInventario = "SELECT id_inventario FROM bebidas WHERE id_bebida = @idBebida";
                            int idInventario;

                            using (MySqlCommand cmdInventario = new MySqlCommand(queryObtenerInventario, conexion, transaccion))
                            {
                                cmdInventario.Parameters.AddWithValue("@idBebida", idBebida);
                                object resultado = cmdInventario.ExecuteScalar();

                                if (resultado == null)
                                {
                                    MessageBox.Show($"No se encontró inventario para la bebida con ID {idBebida}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaccion.Rollback();
                                    return false;
                                }
                                idInventario = Convert.ToInt32(resultado);
                            }

                            // Actualizar el nombre del producto en inventario
                            string queryActualizarInventario = "UPDATE inventario SET nombreProducto = @nuevoNombre WHERE id_inventario = @idInventario";

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

                                if (filasAfectadas == 0)
                                {
                                    MessageBox.Show("No se realizaron cambios en la bebida. Verifique si los valores ingresados son diferentes a los actuales.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    transaccion.Rollback();
                                    return false;
                                }

                                transaccion.Commit(); // Confirmar la transacción
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            MessageBox.Show("Error al actualizar la bebida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public DataTable ObtenerCategorias()
        {
            string query = "SELECT id_categoria, tipo FROM categorias"; // Consulta corregida

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    // Diagnóstico (puedes eliminar esto después)
                    Console.WriteLine($"Columnas obtenidas: {string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                    Console.WriteLine($"Registros obtenidos: {dt.Rows.Count}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener categorías: {ex.Message}");
                }
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
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
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
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
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

                    // Actualizamos los datos del plato en una sola consulta
                    string query = "UPDATE platos SET nombrePlato = @nombre, descripcion = @descripcion, " +
                                   "precioUnitario = @precio, id_categoriaP = " +
                                   "(SELECT id_categoriaP FROM categoria_platos WHERE tipo = @categoria) " +
                                   "WHERE id_plato = @idPlato";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        cmd.Parameters.AddWithValue("@idPlato", idPlato);

                        return cmd.ExecuteNonQuery() > 0; // Devuelve true si al menos una fila fue afectada
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el plato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // Eliminamos el plato de la tabla platos
                    string queryPlato = "DELETE FROM platos WHERE id_plato = @idPlato";
                    using (MySqlCommand cmdPlato = new MySqlCommand(queryPlato, conexion))
                    {
                        cmdPlato.Parameters.AddWithValue("@idPlato", idPlato);
                        int rowsAffected = cmdPlato.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true; // Plato eliminado correctamente
                        }
                        else
                        {
                            MessageBox.Show("El plato no existe o ya fue eliminado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el plato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string query = "SELECT e.id_extra AS 'ID', i.nombreProducto AS 'Nombre', e.precioUnitario AS 'Precio Unitario' " +
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
                        string queryInventario = "UPDATE inventario SET nombreProducto = @nuevoNombre WHERE id_inventario = @idInventario";
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


        public DataTable ObtenerProveedores()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT id_proveedor AS 'ID', nombreProv AS 'Nombre', telefono_contacto AS 'Contacto', direccion AS 'Direccion' FROM proveedores";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener proveedores: " + ex.Message);
            }
            return dt;
        }

        public bool AgregarProveedor(string nombre, string contacto, string direccion)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    // Usamos nombreProv y telefono_contacto
                    string query = "INSERT INTO proveedores (nombreProv, telefono_contacto, direccion) VALUES (@nombre, @contacto, @direccion)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@contacto", contacto);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar proveedor: " + ex.Message);
                return false;
            }
        }

        public bool ActualizarProveedor(int id, string nombre, string contacto, string direccion)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    // Actualizamos usando nombreProv y telefono_contacto
                    string query = "UPDATE proveedores SET nombreProv = @nombre, telefono_contacto = @contacto, direccion = @direccion WHERE id_proveedor = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@contacto", contacto);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar proveedor: " + ex.Message);
                return false;
            }
        }

        public bool EliminarProveedor(int id)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "DELETE FROM proveedores WHERE id_proveedor = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar proveedor: " + ex.Message);
                return false;
            }
        }


        // Función para obtener el inventario de bebidas uniendo las tablas inventario y bebidas
        public DataTable ObtenerInventarioBebidas()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT i.id_inventario AS 'ID', " +
                                   "i.nombreProducto AS 'Nombre', " +
                                   "i.cantidad AS 'Cantidad', " +
                                   "b.precioUnitario AS 'Precio', " +
                                   "i.id_proveedor AS 'ID_Proveedor', " +
                                   "b.id_categoria AS 'ID_Categoria' " +
                                   "FROM inventario i " +
                                   "JOIN bebidas b ON i.id_inventario = b.id_inventario";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener inventario de bebidas: " + ex.Message);
            }
            return dt;
        }

        // Función para agregar una bebida al inventario
        public bool AgregarInventarioBebida(string nombre, decimal cantidad, decimal precio, int idProveedor, int idCategoria)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    // Inserción en inventario
                    string queryInventario = "INSERT INTO inventario (nombreProducto, cantidad, id_proveedor) " +
                                             "VALUES (@nombre, @cantidad, @idProveedor); SELECT LAST_INSERT_ID();";
                    int idInventario;
                    using (MySqlCommand cmd = new MySqlCommand(queryInventario, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        idInventario = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    // Inserción en bebidas
                    string queryBebidas = "INSERT INTO bebidas (precioUnitario, id_inventario, id_categoria) " +
                                          "VALUES (@precio, @idInventario, @idCategoria)";
                    using (MySqlCommand cmd = new MySqlCommand(queryBebidas, conexion))
                    {
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar bebida al inventario: " + ex.Message);
                return false;
            }
        }

        // Función para actualizar el inventario de una bebida
        public bool ActualizarInventarioBebida(int idInventario, string nombre, decimal cantidad, decimal precio, int idProveedor, int idCategoria)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    // Actualización en inventario
                    string queryInventario = "UPDATE inventario SET nombreProducto = @nombre, cantidad = @cantidad, id_proveedor = @idProveedor " +
                                             "WHERE id_inventario = @idInventario";
                    using (MySqlCommand cmd = new MySqlCommand(queryInventario, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);
                        cmd.ExecuteNonQuery();
                    }
                    // Actualización en bebidas
                    string queryBebidas = "UPDATE bebidas SET precioUnitario = @precio, id_categoria = @idCategoria " +
                                          "WHERE id_inventario = @idInventario";
                    using (MySqlCommand cmd = new MySqlCommand(queryBebidas, conexion))
                    {
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar inventario de bebida: " + ex.Message);
                return false;
            }
        }

        // Función para eliminar el inventario de una bebida
        public bool EliminarInventarioBebida(int idInventario)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    // Primero, eliminamos de la tabla bebidas
                    string queryBebidas = "DELETE FROM bebidas WHERE id_inventario = @idInventario";
                    using (MySqlCommand cmd = new MySqlCommand(queryBebidas, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);
                        cmd.ExecuteNonQuery();
                    }
                    // Luego, eliminamos de la tabla inventario
                    string queryInventario = "DELETE FROM inventario WHERE id_inventario = @idInventario";
                    using (MySqlCommand cmd = new MySqlCommand(queryInventario, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar inventario de bebida: " + ex.Message);
                return false;
            }
        }

        //CODIGO DE TODO LO DE INVENTARIO:
        // Funciones para los ingredientes de inventario
        public DataTable ObtenerIngredientes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = @"
                            SELECT i.id_inventario AS 'ID', 
                                   i.nombreProducto AS 'Nombre', 
                                   i.cantidad AS 'Cantidad', 
                                   p.nombreProv AS 'Proveedor'
                            FROM inventario i
                            JOIN proveedores p ON i.id_proveedor = p.id_proveedor
                            WHERE NOT EXISTS (SELECT 1 FROM bebidas b WHERE b.id_inventario = i.id_inventario)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener ingredientes: " + ex.Message);
            }
            return dt;
        }

        //Filtrar ingrediente spor el  proveedor
        public DataTable FiltrarIngredientesPorProveedor(string nombreProveedor)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = @"
                    SELECT i.id_inventario AS 'ID', 
                           i.nombreProducto AS 'Nombre', 
                           i.cantidad AS 'Cantidad', 
                           p.nombreProv AS 'Proveedor'
                    FROM inventario i
                    JOIN proveedores p ON i.id_proveedor = p.id_proveedor
                    WHERE NOT EXISTS (SELECT 1 FROM bebidas b WHERE b.id_inventario = i.id_inventario)
                      AND (p.nombreProv = @nombreProveedor OR @nombreProveedor = 'Todos')";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombreProveedor", nombreProveedor);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar ingredientes: " + ex.Message);
            }
            return dt;
        }

        //Obtener el nombre de los proveedores
        public DataTable ObtenerProveedoresIngredientes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT id_proveedor, nombreProv FROM proveedores";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener proveedores: " + ex.Message);
            }
            return dt;
        }

        //Agregar ingrediente nuevo
        public bool AgregarIngrediente(string nombreProducto, decimal cantidad, int idProveedor)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "INSERT INTO inventario (nombreProducto, cantidad, id_proveedor) " +
                                   "VALUES (@nombreProducto, @cantidad, @idProveedor)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombreProducto", nombreProducto);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0; // Retorna true si se insertó correctamente
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar ingrediente: " + ex.Message);
                return false;
            }
        }

        //Eliminar ingrediente
        public bool EliminarIngrediente(int idInventario)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "DELETE FROM inventario WHERE id_inventario = @idInventario";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0; // Retorna true si se eliminó correctamente
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar ingrediente: " + ex.Message);
                return false;
            }
        }

        //Agregar un nuevo ingrediente
        public bool EditarIngrediente(int idInventario, string nuevoNombre, decimal nuevaCantidad, int nuevoIdProveedor)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "UPDATE inventario SET nombreProducto = @nuevoNombre, cantidad = @nuevaCantidad, id_proveedor = @nuevoIdProveedor WHERE id_inventario = @idInventario";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@nuevaCantidad", nuevaCantidad);
                        cmd.Parameters.AddWithValue("@nuevoIdProveedor", nuevoIdProveedor);
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0; // Retorna true si se actualizó correctamente
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar ingrediente: " + ex.Message);
                return false;
            }
        }

        //Agregar plato:
        public bool AgregarPlato(string nombrePlato, decimal precioUnitario, string descripcion, int idCategoria)
        {
            try
            {
                using (var conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "INSERT INTO platos (nombrePlato, precioUnitario, descripcion, id_categoriaP) VALUES (@nombre, @precio, @descripcion, @idCategoria)";

                    using (var comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombrePlato);
                        comando.Parameters.AddWithValue("@precio", precioUnitario);
                        comando.Parameters.AddWithValue("@descripcion", descripcion);
                        comando.Parameters.AddWithValue("@idCategoria", idCategoria);

                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el plato: " + ex.Message);
                return false;
            }
        }


        public DataTable EjecutarConsulta(string query)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en consulta: {ex.Message}\nConsulta: {query}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public int EjecutarNonQuery(string query)
        {
            int resultado = 0;
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    resultado = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en ejecución: {ex.Message}\nConsulta: {query}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return resultado;
        }



        


        public DataTable ObtenerExtrasCompletos()
        {
            try
            {
                string query = @"SELECT 
                        e.id_extra AS ID, 
                        i.nombreProducto AS Nombre, 
                        e.precioUnitario AS Precio, 
                        i.cantidad AS Cantidad, 
                        p.nombreProv AS Proveedor,
                        i.id_proveedor AS ID_Proveedor, 
                        e.id_inventario AS ID_Inventario 
                        FROM extras e 
                        INNER JOIN inventario i ON e.id_inventario = i.id_inventario 
                        INNER JOIN proveedores p ON i.id_proveedor = p.id_proveedor";

                DataTable result = EjecutarConsulta(query);

                // Verificación básica de columnas
                string[] requiredColumns = { "ID", "Nombre", "Precio", "Cantidad", "Proveedor", "ID_Proveedor", "ID_Inventario" };
                foreach (var col in requiredColumns)
                {
                    if (!result.Columns.Contains(col))
                        throw new Exception($"Falta la columna requerida: {col}");
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener extras: {ex.Message}");
                return new DataTable(); // Retorna tabla vacía para evitar null
            }
        }





        public bool AgregarExtraConInventario(string nombre, decimal precio, int cantidad, int idProveedor)
        {
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Insertar en inventario
                    string queryInventario = @"
                INSERT INTO inventario 
                (nombreProducto, cantidad, id_proveedor) 
                VALUES (@nombre, @cantidad, @idProveedor);
                SELECT LAST_INSERT_ID();";

                    MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conn, transaction);
                    cmdInventario.Parameters.AddWithValue("@nombre", nombre);
                    cmdInventario.Parameters.AddWithValue("@cantidad", cantidad);
                    cmdInventario.Parameters.AddWithValue("@idProveedor", idProveedor);

                    int idInventario = Convert.ToInt32(cmdInventario.ExecuteScalar());

                    // 2. Insertar en extras
                    string queryExtra = @"
                INSERT INTO extras 
                (precioUnitario, id_inventario) 
                VALUES (@precio, @idInventario)";

                    MySqlCommand cmdExtra = new MySqlCommand(queryExtra, conn, transaction);
                    cmdExtra.Parameters.AddWithValue("@precio", precio);
                    cmdExtra.Parameters.AddWithValue("@idInventario", idInventario);
                    cmdExtra.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool ActualizarExtraConInventario(int idExtra, int idInventario, string nombre, decimal precio, int cantidad, int idProveedor)
        {
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Actualizar inventario
                    string queryInventario = @"
                UPDATE inventario SET 
                    nombreProducto = @nombre,
                    cantidad = @cantidad,
                    id_proveedor = @idProveedor
                WHERE id_inventario = @idInventario";

                    MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conn, transaction);
                    cmdInventario.Parameters.AddWithValue("@nombre", nombre);
                    cmdInventario.Parameters.AddWithValue("@cantidad", cantidad);
                    cmdInventario.Parameters.AddWithValue("@idProveedor", idProveedor);
                    cmdInventario.Parameters.AddWithValue("@idInventario", idInventario);
                    cmdInventario.ExecuteNonQuery();

                    // 2. Actualizar extras
                    string queryExtra = @"
                UPDATE extras SET 
                    precioUnitario = @precio
                WHERE id_extra = @idExtra";

                    MySqlCommand cmdExtra = new MySqlCommand(queryExtra, conn, transaction);
                    cmdExtra.Parameters.AddWithValue("@precio", precio);
                    cmdExtra.Parameters.AddWithValue("@idExtra", idExtra);
                    cmdExtra.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool EliminarExtraConInventario(int idExtra, int idInventario)
        {
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Eliminar de extras
                    string queryExtra = "DELETE FROM extras WHERE id_extra = @idExtra";
                    MySqlCommand cmdExtra = new MySqlCommand(queryExtra, conn, transaction);
                    cmdExtra.Parameters.AddWithValue("@idExtra", idExtra);
                    cmdExtra.ExecuteNonQuery();

                    // 2. Eliminar de inventario
                    string queryInventario = "DELETE FROM inventario WHERE id_inventario = @idInventario";
                    MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conn, transaction);
                    cmdInventario.Parameters.AddWithValue("@idInventario", idInventario);
                    cmdInventario.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        

    }
}



