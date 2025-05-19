using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV.Configuracion
{
    public class DatosCorte
    {
        public decimal MontoContado { get; set; }
        public decimal CajaInicial { get; set; }
        public decimal TotalGenerado { get; set; }
        public decimal TotalGastos { get; set; }
        public string NombreCajero { get; set; }
    }

    public class DatosCorteGeneral
    {
        public int IdCaja { get; set; }
        public decimal CantEfectivo { get; set; }
        public decimal CantTarjeta { get; set; }
        public decimal Descuento { get; set; }
        public decimal TotalGastosGeneral { get; set; }
        public string NombreCajero { get; set; }
        public DateTime Fecha { get; set; }
    }

    class Conexion
    {
        private MySqlConnection conectar = null;
        private static string usuario = "root";
        private static string contrasenia = "root";
        private static string bd = "lacaguamabd";
        private static string ip = "localhost";
        private static string puerto = "3307"; // 3306 o 3307 si eres javier 

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
        public DataTable ObtenerBebidasDisponibles()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT b.id_bebida AS 'ID Bebida', " +
                                   "i.nombreProducto AS 'Nombre Bebida', " +
                                   "c.tipo AS 'Categoría', " +
                                   "b.precioUnitario AS 'Precio Unitario' " +
                                   "FROM bebidas b " +
                                   "JOIN inventario i ON b.id_inventario = i.id_inventario " +
                                   "JOIN categorias c ON b.id_categoria = c.id_categoria " +
                                   "WHERE i.id_disponibilidad = 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener bebidas disponibles: " + ex.Message);
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
                    string query = @"
                SELECT 
                    i.nombreProducto AS 'Nombre Bebida', 
                    b.id_bebida AS 'ID Bebida', 
                    b.precioUnitario AS 'Precio Unitario', 
                    c.tipo AS 'Categoría',
                    d.nombreDis AS 'Disponibilidad'
                FROM bebidas b
                JOIN inventario i ON b.id_inventario = i.id_inventario
                JOIN categorias c ON b.id_categoria = c.id_categoria
                JOIN disponibilidad d ON i.id_disponibilidad = d.id_disponibilidad
                WHERE c.tipo = @categoria";

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
                MessageBox.Show("Error al obtener bebidas por categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
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


        //Función para obtener extras
        public DataTable ObtenerExtras()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT e.id_extra AS 'ID', i.nombreProducto AS 'Nombre', e.precioUnitario AS 'Precio Unitario' " +
                                   "FROM extras e " +
                                   "JOIN inventario i ON e.id_inventario = i.id_inventario " +
                                   "JOIN disponibilidad d ON i.id_disponibilidad = d.id_disponibilidad " +
                                   "WHERE d.nombreDis = 'Disponible'";

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
                                   "b.id_categoria AS 'ID_Categoria', " +
                                   "d.id_disponibilidad AS 'ID_Disponibilidad', " +
                                   "d.nombreDis AS 'Disponibilidad' " +
                                   "FROM inventario i " +
                                   "JOIN bebidas b ON i.id_inventario = b.id_inventario " +
                                   "JOIN disponibilidad d ON i.id_disponibilidad = d.id_disponibilidad";

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
        public bool AgregarInventarioBebida(string nombre, decimal cantidad, decimal precio, int idProveedor, int idCategoria, int idDisponibilidad)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    // Inserción en inventario con unidad y disponibilidad
                    string queryInventario = "INSERT INTO inventario (nombreProducto, cantidad, id_proveedor, id_unidad, id_disponibilidad) " +
                                             "VALUES (@nombre, @cantidad, @idProveedor, @idUnidad, @idDisponibilidad); SELECT LAST_INSERT_ID();";
                    int idInventario;
                    using (MySqlCommand cmd = new MySqlCommand(queryInventario, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        cmd.Parameters.AddWithValue("@idUnidad", 5); // Siempre 5 porque es "unidad"
                        cmd.Parameters.AddWithValue("@idDisponibilidad", idDisponibilidad);
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
        public bool ActualizarInventarioBebida(int idInventario, string nombre, decimal cantidad, decimal precio, int idProveedor, int idCategoria, int idDisponibilidad)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    string queryInventario = @"
                UPDATE inventario 
                SET nombreProducto = @nombre, 
                    cantidad = cantidad + @cantidad, 
                    id_proveedor = @idProveedor, 
                    id_disponibilidad = @idDisponibilidad
                WHERE id_inventario = @idInventario";

                    using (MySqlCommand cmd = new MySqlCommand(queryInventario, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        cmd.Parameters.AddWithValue("@idDisponibilidad", idDisponibilidad);
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);
                        cmd.ExecuteNonQuery();
                    }

                    string queryBebidas = @"
                UPDATE bebidas 
                SET precioUnitario = @precio, 
                    id_categoria = @idCategoria 
                WHERE id_inventario = @idInventario";

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
                       u.nombreUnidad AS 'Unidad',
                       p.nombreProv AS 'Proveedor',
                       d.nombreDis AS 'Disponibilidad'
                FROM inventario i
                JOIN unidad u ON i.id_unidad = u.id_unidad
                JOIN proveedores p ON i.id_proveedor = p.id_proveedor
                JOIN disponibilidad d ON i.id_disponibilidad = d.id_disponibilidad
                WHERE NOT EXISTS (
                    SELECT 1 FROM bebidas b WHERE b.id_inventario = i.id_inventario
                )";

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
                SELECT 
                    i.id_inventario AS 'ID', 
                    i.nombreProducto AS 'Nombre', 
                    i.cantidad AS 'Cantidad', 
                    p.nombreProv AS 'Proveedor',
                    d.nombreDis AS 'Disponibilidad'
                FROM inventario i
                JOIN proveedores p ON i.id_proveedor = p.id_proveedor
                JOIN disponibilidad d ON i.id_disponibilidad = d.id_disponibilidad
                WHERE NOT EXISTS (
                    SELECT 1 FROM bebidas b WHERE b.id_inventario = i.id_inventario
                )
                AND (@nombreProveedor = 'Todos' OR p.nombreProv = @nombreProveedor);";

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
                MessageBox.Show("Error al filtrar ingredientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public bool AgregarIngrediente(string nombreProducto, decimal cantidad, int idProveedor, int idUnidad, int idDisponibilidad)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "INSERT INTO inventario (nombreProducto, cantidad, id_proveedor, id_unidad, id_disponibilidad) " +
                                   "VALUES (@nombreProducto, @cantidad, @idProveedor, @idUnidad, @idDisponibilidad)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombreProducto", nombreProducto);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        cmd.Parameters.AddWithValue("@idUnidad", idUnidad);
                        cmd.Parameters.AddWithValue("@idDisponibilidad", idDisponibilidad);

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


        //Agregar un nuevo ingrediente
        // Editar un ingrediente existente
        public bool EditarIngrediente(int idInventario, string nuevoNombre, decimal cantidadASumar, int nuevoIdProveedor, int nuevoIdDisponibilidad, int nuevoIdUnidad)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = @"
                UPDATE inventario 
                SET nombreProducto = @nuevoNombre, 
                    cantidad = cantidad + @cantidadASumar, 
                    id_proveedor = @nuevoIdProveedor, 
                    id_disponibilidad = @nuevoIdDisponibilidad,
                    id_unidad = @nuevoIdUnidad
                WHERE id_inventario = @idInventario";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@cantidadASumar", cantidadASumar);
                        cmd.Parameters.AddWithValue("@nuevoIdProveedor", nuevoIdProveedor);
                        cmd.Parameters.AddWithValue("@nuevoIdDisponibilidad", nuevoIdDisponibilidad);
                        cmd.Parameters.AddWithValue("@nuevoIdUnidad", nuevoIdUnidad);
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
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

        //CODIGO DE EXTRAS

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
                                u.nombreUnidad AS 'Unidad',  -- <<--- Aquí corregido
                                e.id_inventario AS ID_Inventario,
                                i.id_disponibilidad AS ID_Disponibilidad,
                                d.nombreDis AS Disponibilidad
                            FROM extras e 
                            INNER JOIN inventario i ON e.id_inventario = i.id_inventario 
                            INNER JOIN unidad u ON i.id_unidad = u.id_unidad  -- <<--- Mantén esto
                            INNER JOIN proveedores p ON i.id_proveedor = p.id_proveedor
                            INNER JOIN disponibilidad d ON i.id_disponibilidad = d.id_disponibilidad";


                DataTable result = EjecutarConsulta(query);

                // Verificación básica de columnas
                string[] requiredColumns = { "ID", "Nombre", "Precio", "Cantidad", "Proveedor", "ID_Proveedor", "ID_Inventario", "Disponibilidad" };
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



        public bool AgregarExtraConInventario(string nombre, decimal precio, decimal cantidad, int idProveedor, int idDisponibilidad, int idUnidad)
        {
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Insertar en inventario (incluye unidad)
                    string queryInventario = @"
                INSERT INTO inventario 
                (nombreProducto, cantidad, id_proveedor, id_disponibilidad, id_unidad) 
                VALUES (@nombre, @cantidad, @idProveedor, @idDisponibilidad, @idUnidad);
                SELECT LAST_INSERT_ID();";

                    MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conn, transaction);

                    cmdInventario.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre;
                    cmdInventario.Parameters.Add("@cantidad", MySqlDbType.Decimal).Value = cantidad;
                    cmdInventario.Parameters.Add("@idProveedor", MySqlDbType.Int32).Value = idProveedor;
                    cmdInventario.Parameters.Add("@idDisponibilidad", MySqlDbType.Int32).Value = idDisponibilidad;
                    cmdInventario.Parameters.Add("@idUnidad", MySqlDbType.Int32).Value = idUnidad;

                    int idInventario = Convert.ToInt32(cmdInventario.ExecuteScalar());

                    // 2. Insertar en extras
                    string queryExtra = @"
                INSERT INTO extras 
                (precioUnitario, id_inventario) 
                VALUES (@precio, @idInventario)";

                    MySqlCommand cmdExtra = new MySqlCommand(queryExtra, conn, transaction);
                    cmdExtra.Parameters.Add("@precio", MySqlDbType.Decimal).Value = precio;
                    cmdExtra.Parameters.Add("@idInventario", MySqlDbType.Int32).Value = idInventario;
                    cmdExtra.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error al agregar extra: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }




        public bool ActualizarExtraConInventario(int idExtra, int idInventario, string nombre,
                              decimal precio, decimal cantidad, int idProveedor, int idDisponibilidad, int idUnidad)
        {
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Actualizar inventario (SUMANDO cantidad)
                    string queryInventario = @"
                UPDATE inventario SET 
                    nombreProducto = @nombre,
                    cantidad = cantidad + @cantidad,
                    id_proveedor = @idProveedor,
                    id_disponibilidad = @idDisponibilidad,
                    id_unidad = @idUnidad
                WHERE id_inventario = @idInventario";

                    MySqlCommand cmdInventario = new MySqlCommand(queryInventario, conn, transaction);
                    cmdInventario.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre;
                    cmdInventario.Parameters.Add("@cantidad", MySqlDbType.Decimal).Value = cantidad;
                    cmdInventario.Parameters.Add("@idProveedor", MySqlDbType.Int32).Value = idProveedor;
                    cmdInventario.Parameters.Add("@idDisponibilidad", MySqlDbType.Int32).Value = idDisponibilidad;
                    cmdInventario.Parameters.Add("@idUnidad", MySqlDbType.Int32).Value = idUnidad;
                    cmdInventario.Parameters.Add("@idInventario", MySqlDbType.Int32).Value = idInventario;
                    cmdInventario.ExecuteNonQuery();

                    // 2. Actualizar tabla extras
                    string queryExtra = @"
                UPDATE extras SET 
                    precioUnitario = @precio
                WHERE id_extra = @idExtra";

                    MySqlCommand cmdExtra = new MySqlCommand(queryExtra, conn, transaction);
                    cmdExtra.Parameters.Add("@precio", MySqlDbType.Decimal).Value = precio;
                    cmdExtra.Parameters.Add("@idExtra", MySqlDbType.Int32).Value = idExtra;
                    cmdExtra.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error al actualizar: {ex.Message}");
                    return false;
                }
            }
        }

        //CODIGO DE RECETAS POR CADA PLATO
        //Ingredientes de cada plato
        public DataTable ObtenerIngredientesPorPlato(int idPlato)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT i.nombreProducto, r.cantidad_necesaria " +
                                   "FROM recetas r " +
                                   "JOIN inventario i ON r.id_inventario = i.id_inventario " +
                                   "WHERE r.id_plato = @idPlato";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPlato", idPlato);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener ingredientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        //Eliminar ingrediente de la receta
        public bool EliminarIngredienteDeReceta(int idPlato, string nombreIngrediente)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Obtener el ID del ingrediente desde la tabla inventario
                    string queryIdInventario = "SELECT id_inventario FROM inventario WHERE nombreProducto = @nombreIngrediente";
                    int idInventario;

                    using (MySqlCommand cmd = new MySqlCommand(queryIdInventario, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombreIngrediente", nombreIngrediente);
                        object result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("El ingrediente no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        idInventario = Convert.ToInt32(result);
                    }

                    // Eliminar el ingrediente de la receta
                    string queryEliminar = "DELETE FROM recetas WHERE id_plato = @idPlato AND id_inventario = @idInventario";

                    using (MySqlCommand cmd = new MySqlCommand(queryEliminar, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPlato", idPlato);
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar ingrediente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //Obtener todos los elementos del inventario:
        public DataTable ObtenerInventario()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT i.id_inventario AS 'ID', " +
                                   "i.nombreProducto AS 'Nombre Producto', " +
                                   "CONCAT(i.cantidad, ' ', u.nombreUnidad) AS 'Cantidad Disponible' " +
                                   "FROM inventario i " +
                                   "INNER JOIN unidad u ON i.id_unidad = u.id_unidad";

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
                MessageBox.Show("Error al obtener inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }


        //Agregar ingrediente a la receta 
        public bool AgregarIngredienteAReceta(int idPlato, int idInventario, decimal cantidad)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "INSERT INTO recetas (id_plato, id_inventario, cantidad_necesaria) " +
                                   "VALUES (@idPlato, @idInventario, @cantidad)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idPlato", idPlato);
                        cmd.Parameters.AddWithValue("@idInventario", idInventario);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar ingrediente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //Editar cantidad de ingredientes ya agregados
        public bool EditarCantidadIngrediente(int idPlato, string nombreIngrediente, decimal nuevaCantidad)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "UPDATE recetas " +
                                   "SET cantidad_necesaria = @nuevaCantidad " +
                                   "WHERE id_plato = @idPlato AND id_inventario = (SELECT id_inventario FROM inventario WHERE nombreProducto = @nombreIngrediente)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nuevaCantidad", nuevaCantidad);
                        cmd.Parameters.AddWithValue("@idPlato", idPlato);
                        cmd.Parameters.AddWithValue("@nombreIngrediente", nombreIngrediente);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la cantidad: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // -------------------- Gastos --------------------



        // Método para obtener el fondo inicial de la caja
        public decimal ObtenerFondoInicial(int idCaja)
        {

            decimal cantidad = 0;
            string query = "SELECT cantidad FROM caja WHERE id_caja = @idCaja";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        cantidad = Convert.ToDecimal(result);
                    }
                }
            }

            return cantidad;
        }

        // Método para obtener el total de gastos del día (para un id_caja específico)
        public decimal ObtenerTotalGastosDelDia(int idCaja)
        {
            decimal total = 0;
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "SELECT SUM(cantidad) FROM gastos WHERE DATE(fecha) = CURDATE() AND id_caja = @idCaja";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    conexion.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        total = Convert.ToDecimal(result);
                }
            }
            return total;
        }

        // Método para obtener los gastos del día (para un id_caja específico)
        public DataTable ObtenerGastosDelDia(int idCaja)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT id_gasto, cantidad, descripcion, fecha FROM gastos WHERE DATE(fecha) = CURDATE() AND id_caja = @idCaja";

                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idCaja", idCaja);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los gastos del día: " + ex.Message);
            }
        }



        //Obtener monto caja
        public decimal ObtenerMontoCaja(int idCaja)
        {
            decimal monto = 0;
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "SELECT cantidad FROM caja WHERE id_caja = @idCaja";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    conexion.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null) monto = Convert.ToDecimal(result);
                }
            }
            return monto;
        }


        // Método para insertar un gasto
        public void InsertarGasto(decimal cantidad, string descripcion, int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "INSERT INTO gastos (cantidad, descripcion, fecha, id_caja) VALUES (@cantidad, @descripcion, NOW(), @idCaja)";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener el efectivo recolectado por caja
        public decimal ObtenerEfectivoRecolectadoPorCaja(int idCaja)
        {
            DateTime? fechaCaja = ObtenerFechaCaja(idCaja);  // Obtener la fecha de la caja
            if (fechaCaja == null) return 0;

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                string query = @"
        SELECT IFNULL(SUM(total - descuento), 0) 
        FROM ordenes 
        WHERE tipo_pago = 1 
        AND id_estadoO = 2 
        AND DATE(fecha_orden) = @fecha";

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@fecha", ((DateTime)fechaCaja).Date);

                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        // Método para obtener la fecha de la caja
        public DateTime? ObtenerFechaCaja(int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT fecha FROM caja WHERE id_caja = @idCaja LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idCaja", idCaja);
                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? (DateTime?)Convert.ToDateTime(result) : null;
            }
        }

        // Método para obtener el efectivo recolectado (general)
        public decimal ObtenerEfectivoRecolectado(int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                // Consulta para obtener el total generado (ventas en efectivo)
                string query = @"SELECT SUM(o.total) 
                         FROM ordenes o 
                         INNER JOIN estado_orden eo ON o.id_estadoO = eo.id_estadoO 
                         WHERE o.id_caja = @idCaja AND o.tipo_pago = 1 AND eo.nombreEstadoO = 'cerrada'";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();

                    // Verificamos si el resultado no es nulo y es un valor válido
                    if (result != DBNull.Value && result != null)
                    {
                        return Convert.ToDecimal(result);
                    }
                    else
                    {
                        return 0m; // Si no se encuentra el valor o es nulo, devolvemos 0
                    }
                }
            }
        }

        // Método para obtener la última caja activa (para corte de caja)
        public int ObtenerUltimaCaja()
        {
            int idCaja = -1;

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = @"
        SELECT id_caja 
        FROM caja 
        WHERE id_estado_caja = 2 
        ORDER BY fecha DESC 
        LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    conexion.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idCaja = Convert.ToInt32(result);
                    }
                }
            }

            return idCaja;
        }

        public bool ActualizarGasto(int idGasto, decimal cantidad, string descripcion)
        {
            try
            {
                string query = "UPDATE gastos SET cantidad = @cantidad, descripcion = @descripcion WHERE id_gasto = @idGasto";

                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@idGasto", idGasto);
                        conexion.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el gasto: " + ex.Message);
            }
        }

        public decimal ObtenerTotalGastos(int idCaja, DateTime? fecha = null)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT IFNULL(SUM(cantidad), 0) FROM gastos WHERE id_caja = @idCaja";

                if (fecha.HasValue)
                    query += " AND DATE(fecha) = @fecha";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    if (fecha.HasValue)
                        cmd.Parameters.AddWithValue("@fecha", fecha.Value.ToString("yyyy-MM-dd"));

                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        public DataTable ObtenerGastosPorCaja(int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "SELECT id_gasto, cantidad, descripcion, fecha FROM gastos WHERE id_caja = @idCaja ORDER BY fecha DESC";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    conexion.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        adapter.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public DataTable ObtenerGastosPorIdCaja(int idCaja)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "SELECT id_gasto, cantidad, descripcion, fecha FROM gastos WHERE id_caja = @idCaja ORDER BY fecha DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }

            return tabla;
        }


        public DataTable ObtenerGastosFiltrados(int idCaja, string tipoFiltro, DateTime fechaInicio, DateTime fechaFin)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "SELECT id_gasto, cantidad, descripcion, fecha FROM gastos WHERE id_caja = @idCaja";

                if (tipoFiltro == "Hoy")
                {
                    query += " AND DATE(fecha) = CURDATE()";
                }
                else if (tipoFiltro == "Esta semana")
                {
                    query += " AND YEARWEEK(fecha, 1) = YEARWEEK(CURDATE(), 1)";
                }
                else if (tipoFiltro == "Este mes")
                {
                    query += " AND MONTH(fecha) = MONTH(CURDATE()) AND YEAR(fecha) = YEAR(CURDATE())";
                }
                else if (tipoFiltro == "Fecha específica")
                {
                    query += " AND DATE(fecha) = @fechaInicio";
                }
                else if (tipoFiltro == "Rango de fechas")
                {
                    query += " AND DATE(fecha) BETWEEN @fechaInicio AND @fechaFin";
                }

                query += " ORDER BY fecha DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    if (tipoFiltro == "Fecha específica" || tipoFiltro == "Rango de fechas")
                    {
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio.Date);
                    }
                    if (tipoFiltro == "Rango de fechas")
                    {
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin.Date);
                    }

                    conexion.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        adapter.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }


        public DataTable ObtenerGastosPorFiltro(string tipoFiltro, DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "SELECT id_gasto, cantidad, descripcion, fecha FROM gastos WHERE 1=1";

                if (tipoFiltro != "Todos")
                {
                    query += " AND fecha BETWEEN @fechaInicio AND @fechaFin";
                }

                query += " ORDER BY fecha DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    if (tipoFiltro != "Todos")
                    {
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio.Date);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin.Date.AddDays(1).AddSeconds(-1));
                    }

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        public DataTable ObtenerGastosPorFecha(int idCaja, DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = @"SELECT id_gasto, cantidad, descripcion, fecha
                         FROM gastos
                         WHERE id_caja = @idCaja 
                         AND DATE(fecha) BETWEEN @fechaInicio AND @fechaFin
                         ORDER BY fecha DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin.ToString("yyyy-MM-dd"));

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }

            return tabla;
        }
        public int ObtenerUltimaCajaRegistrada()
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                string query = "SELECT id_caja FROM caja ORDER BY fecha DESC LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    conexion.Open();
                    object resultado = cmd.ExecuteScalar();
                    return resultado != null ? Convert.ToInt32(resultado) : -1;
                }
            }
        }



        // -------------------- CAJA Inicial --------------------


        public void InsertarCajaInicial(decimal cantidad, int idUsuario)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "INSERT INTO caja (cantidad, fecha, id_usuario, id_estado_caja) " +
                                   "VALUES (@cantidad, NOW(), @idUsuario, 2)";  // Estado '2' = 'Inicializada'

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar la caja inicial: " + ex.Message);
            }
        }

        public bool RegistrarInicioCaja(decimal cantidad, int idUsuario)
        {
            bool resultado = false;

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string consulta = @"INSERT INTO caja (cantidad, fecha, id_usuario, id_estado_caja)
                                    VALUES (@cantidad, NOW(), @id_usuario, 2);"; // 2 = Iniciada

                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        resultado = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar la caja: " + ex.Message);
                }
            }

            return resultado;
        }

        public bool HayCajaAbiertaHoy(int idUsuario)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = @"
            SELECT COUNT(*) 
            FROM caja c
            WHERE DATE(c.fecha) = CURDATE()
              AND NOT EXISTS (
                  SELECT 1 FROM corte_de_caja cc WHERE cc.id_caja = c.id_caja
              )";

                int resultado = Convert.ToInt32(new MySqlCommand(consulta, conexion).ExecuteScalar());
                return resultado > 0;
            }
        }

        //Codigo modificacion de disponibilidad
        public DataTable ObtenerDisponibilidad()
        {
            string query = "SELECT id_disponibilidad, nombreDis FROM disponibilidad";

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    // Diagnóstico opcional
                    Console.WriteLine($"Columnas obtenidas: {string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                    Console.WriteLine($"Registros obtenidos: {dt.Rows.Count}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener disponibilidad: {ex.Message}");
                }
            }
            return dt;
        }


        public bool ExisteCorteParaUltimaCaja(int idUsuario)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                // Obtener el último id_caja del usuario
                string consultaCaja = @"
            SELECT id_caja 
            FROM caja 
            WHERE id_usuario = @idUsuario 
            ORDER BY fecha DESC 
            LIMIT 1";

                int idCaja = -1;
                using (MySqlCommand cmd = new MySqlCommand(consultaCaja, conexion))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        idCaja = Convert.ToInt32(result);
                }

                if (idCaja == -1)
                    return true; // Si no hay cajas aún, permitimos iniciar una nueva

                // Verificar si existe un corte de caja con ese id_caja
                string consultaCorte = "SELECT COUNT(*) FROM corte_de_caja WHERE id_caja = @idCaja";

                using (MySqlCommand cmd = new MySqlCommand(consultaCorte, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
        }


        // -------------------- CORTE --------------------


        // Obtener el id_caja más reciente del usuario con estado inicializada
        public int ObtenerIdCajaActualCorte()
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = @"
            SELECT id_caja 
            FROM caja 
            WHERE id_estado_caja = 2 
            ORDER BY fecha DESC 
            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }


        // OBTENER CAJA INICIAL DEL DÍA
        public int ObtenerUltimoIdCajaInicializada(int idUsuario)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = @"
            SELECT id_caja 
            FROM caja 
            WHERE id_usuario = @idUsuario AND id_estado_caja = 2
            ORDER BY fecha DESC 
            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        // Obtener total generado (ventas en efectivo con estado cerrada y misma caja)
        public decimal ObtenerTotalGenerado(int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                // Consulta para obtener el total generado (ventas en efectivo)
                string query = @"SELECT SUM(o.total) 
                         FROM ordenes o 
                         INNER JOIN estado_orden eo ON o.id_estadoO = eo.id_estadoO 
                         WHERE o.id_caja = @idCaja AND o.tipo_pago = 1 AND eo.nombreEstadoO = 'cerrada'";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();

                    // Verificamos si el resultado no es nulo y es un valor válido
                    if (result != DBNull.Value && result != null)
                    {
                        return Convert.ToDecimal(result);
                    }
                    else
                    {
                        return 0m; // Si no se encuentra el valor o es nulo, devolvemos 0
                    }
                }
            }
        }
        // Obtener total de gastos de esa caja
        public decimal ObtenerGastos(int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                // Consulta para obtener el total de los gastos de la caja
                string query = "SELECT SUM(cantidad) FROM gastos WHERE id_caja = @idCaja";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();

                    // Verificamos si el resultado no es nulo y es un valor válido
                    if (result != DBNull.Value && result != null)
                    {
                        return Convert.ToDecimal(result);
                    }
                    else
                    {
                        return 0m; // Si no se encuentra el valor o es nulo, devolvemos 0
                    }
                }
            }
        }

        // Guardar el corte de caja
        public void GuardarCorteCaja(decimal cantidad, int idUsuario, int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                // Definimos el query para insertar el corte de caja
                string query = @"INSERT INTO corte_de_caja (cantidad, fecha, id_estado_corte, id_usuario, id_caja) 
                         VALUES (@cantidad, @fecha, @id_estado_corte, @id_usuario, @id_caja)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    // Agregar los parámetros para evitar inyecciones SQL
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id_estado_corte", 2); // Suponiendo que 2 es el estado de "corte realizado"
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                    cmd.Parameters.AddWithValue("@id_caja", idCaja);

                    // Ejecutar la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public decimal ObtenerCajaInicial(int idCaja)
        {

            decimal cantidad = 0;
            string query = "SELECT cantidad FROM caja WHERE id_caja = @idCaja";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        cantidad = Convert.ToDecimal(result);
                    }
                }
            }

            return cantidad;
        }


        public bool CorteYaRealizado(int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = "SELECT COUNT(*) FROM corte_de_caja WHERE id_caja = @idCaja";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0;
                }
            }
        }


        public void ActualizarEstadoCaja(int idCaja, int nuevoEstado)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "UPDATE caja SET id_estado_caja = @estado WHERE id_caja = @idCaja";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarEstadoCorte(int idCorte, int nuevoEstado)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "UPDATE corte_de_caja SET id_estado_corte = @estado WHERE id_corte = @idCorte";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@idCorte", idCorte);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int ObtenerUltimoCortePorCaja(int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT id_corte FROM corte_de_caja WHERE id_caja = @idCaja ORDER BY fecha DESC LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public bool ExisteCorteParaUltimaCaja()
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                // Obtener la última caja del día (independientemente del usuario)
                string consultaCaja = @"
        SELECT id_caja 
        FROM caja 
        WHERE DATE(fecha) = CURDATE() 
        ORDER BY fecha DESC 
        LIMIT 1";

                int idCaja = -1;
                using (MySqlCommand cmd = new MySqlCommand(consultaCaja, conexion))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        idCaja = Convert.ToInt32(result);
                }

                if (idCaja == -1)
                    return true; // Si no hay cajas hoy, permitimos iniciar una nueva

                // Verificar si esa caja ya fue cerrada con un corte
                string consultaCorte = "SELECT COUNT(*) FROM corte_de_caja WHERE id_caja = @idCaja";

                using (MySqlCommand cmd = new MySqlCommand(consultaCorte, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
        }

        public int ObtenerUsuarioDeCaja(int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT id_usuario FROM caja WHERE id_caja = @idCaja";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public bool UsuarioTieneCajaActiva(int idUsuario)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = @"SELECT COUNT(*) 
                         FROM caja 
                         WHERE id_usuario = @idUsuario 
                         AND id_estado_caja = 2"; // Estado 2 = Inicializada

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
        }



        // -------------------- CORTE Tarjetas --------------------

        public void GuardarCorteTarjetas(decimal cantidad, int idUsuario, int idCaja)
        {
            string query = @"
        INSERT INTO corte_tarjetas (cantidad, fecha, id_usuario, id_caja)
        VALUES (@cantidad, NOW(), @idUsuario, @idCaja);
    ";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);       // Total de dinero en tarjetas
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GuardarCorteTarjetasSinCaja(decimal cantidad, int idUsuario)
        {
            string query = @"
        INSERT INTO corte_tarjetas (cantidad, fecha, id_usuario, id_caja)
        VALUES (@cantidad, NOW(), @idUsuario, NULL);
    ";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public (decimal total, int cantidad) ObtenerCorteTarjetasPorHorario()
        {
            decimal total = 0;
            int cantidad = 0;

            // Obtener el rango de fechas desde hoy a las 10:00 hasta mañana a las 3:00 AM
            DateTime fechaInicio = DateTime.Today.AddHours(10);       // Hoy 10:00 AM
            DateTime fechaFin = DateTime.Today.AddDays(1).AddHours(6); // Mañana 3:00 AM

            string query = @"
        SELECT 
            SUM(total - descuento) AS total, 
            COUNT(*) AS cantidad 
        FROM ordenes 
        WHERE tipo_pago = 2 
        AND id_estadoO = 2 
        AND fecha_orden BETWEEN @inicio AND @fin;
    ";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@inicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fin", fechaFin);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) total = reader.GetDecimal(0);
                            if (!reader.IsDBNull(1)) cantidad = reader.GetInt32(1);
                        }
                    }
                }
            }

            return (total, cantidad);
        }


        public int ObtenerUltimoIdCaja()
        {
            int idCaja = 0;

            string query = "SELECT id_caja FROM caja ORDER BY fecha DESC LIMIT 1"; // sin filtro por estado

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    var resultado = cmd.ExecuteScalar();
                    if (resultado != null)
                    {
                        idCaja = Convert.ToInt32(resultado);
                    }
                }
            }

            return idCaja;
        }


        public string ObtenerNombreUsuario(int idUsuario)
        {
            string nombre = "";

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    string query = "SELECT nombre FROM usuarios WHERE id_usuario = @idUsuario";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    var resultado = cmd.ExecuteScalar();
                    if (resultado != null)
                        nombre = resultado.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener nombre de usuario: " + ex.Message);
            }

            return nombre;
        }

        public bool CorteTarjetasYaRealizado()
        {
            DateTime ahora = DateTime.Now;
            DateTime inicio, fin;

            if (ahora.Hour >= 10) // De 10:00 AM a 11:59 PM
            {
                inicio = DateTime.Today.AddHours(10); // hoy a las 10:00 AM
                fin = DateTime.Today.AddDays(1).AddHours(6); // mañana a las 3:00 AM
            }
            else // De 12:00 AM a 2:59 AM (después de medianoche pero sigue siendo el turno anterior)
            {
                inicio = DateTime.Today.AddDays(-1).AddHours(10); // ayer a las 10:00 AM
                fin = DateTime.Today.AddHours(6); // hoy a las 3:00 AM
            }

            string query = @"
        SELECT COUNT(*) 
        FROM corte_tarjetas 
        WHERE fecha BETWEEN @inicio AND @fin;
    ";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@inicio", inicio);
                    cmd.Parameters.AddWithValue("@fin", fin);

                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0; // Si ya hay un corte en ese rango
                }
            }
        }


        // -------------------- CORTE General--------------------

        public void GuardarCorteGeneral(decimal cajaInicial, decimal totalFinalDia, decimal efectivo, decimal tarjeta, decimal descuento, decimal gastos, int idUsuario, int idCaja)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = @"INSERT INTO corte_general 
        (caja_inicial_dia, total_final_dia, cant_efectivo, cant_tarjeta, descuento, total_gastos_general, fecha, id_gasto, id_usuario, id_caja)
        VALUES (@cajaInicial, @totalFinal, @efectivo, @tarjeta, @descuento, @gastos, NOW(), NULL, @idUsuario, @idCaja)";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@cajaInicial", cajaInicial);
                    cmd.Parameters.AddWithValue("@totalFinal", totalFinalDia);
                    cmd.Parameters.AddWithValue("@efectivo", efectivo);
                    cmd.Parameters.AddWithValue("@tarjeta", tarjeta);
                    cmd.Parameters.AddWithValue("@descuento", descuento);
                    cmd.Parameters.AddWithValue("@gastos", gastos);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public (decimal totalEfectivo, decimal totalTarjeta, decimal totalDescuentos, decimal totalGastos) ObtenerDatosCorteGeneral()
        {
            decimal efectivo = 0, tarjeta = 0, descuentos = 0, gastos = 0;

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                DateTime ahora = DateTime.Now;
                DateTime fechaInicio = ahora.Hour >= 6 ? new DateTime(ahora.Year, ahora.Month, ahora.Day, 10, 0, 0) : new DateTime(ahora.AddDays(-1).Year, ahora.AddDays(-1).Month, ahora.AddDays(-1).Day, 10, 0, 0);
                DateTime fechaFin = fechaInicio.AddHours(20); // 10 AM + 20 horas = 6 AM del día siguiente

                string consulta = @"
            SELECT 
                IFNULL(SUM(CASE WHEN tipo_pago = 1 THEN total ELSE 0 END), 0) AS total_efectivo,
                IFNULL(SUM(CASE WHEN tipo_pago = 2 THEN total ELSE 0 END), 0) AS total_tarjeta,
                IFNULL(SUM(descuento), 0) AS total_descuento
            FROM ordenes
            WHERE id_estadoO = 2 AND fecha_orden BETWEEN @inicio AND @fin;

            SELECT IFNULL(SUM(cantidad), 0)
            FROM gastos
            WHERE fecha BETWEEN @inicio AND @fin;";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@inicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fin", fechaFin);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            efectivo = reader.GetDecimal(0);
                            tarjeta = reader.GetDecimal(1);
                            descuentos = reader.GetDecimal(2);
                        }

                        if (reader.NextResult() && reader.Read())
                        {
                            gastos = reader.GetDecimal(0);
                        }
                    }
                }
            }

            return (efectivo, tarjeta, descuentos, gastos);
        }

        public bool CorteGeneralYaRealizado()
        {
            DateTime ahora = DateTime.Now;
            DateTime inicio, fin;

            if (ahora.Hour >= 10) // De 10:00 AM a 11:59 PM
            {
                inicio = DateTime.Today.AddHours(10); // hoy a las 10:00 AM
                fin = DateTime.Today.AddDays(1).AddHours(6); // mañana a las 6:00 AM
            }
            else // De 12:00 AM a 2:59 AM (después de medianoche pero sigue siendo el turno anterior)
            {
                inicio = DateTime.Today.AddDays(-1).AddHours(10); // ayer a las 10:00 AM
                fin = DateTime.Today.AddHours(6); // hoy a las 6:00 AM
            }
            string query = @"
        SELECT COUNT(*) 
        FROM corte_general 
        WHERE fecha BETWEEN @inicio AND @fin;
    ";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@inicio", inicio);
                    cmd.Parameters.AddWithValue("@fin", fin);

                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0; // Si ya hay un corte en ese rango
                }
            }
        }

        //UNIDADES
        public DataTable ObtenerUnidades()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_unidad, nombreUnidad FROM unidad";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener unidades: " + ex.Message);
                }
            }

            return dt;
        }

        public bool CrearUnidad(string nombreUnidad)
        {
            string queryVerificar = "SELECT COUNT(*) FROM unidad WHERE nombreUnidad = @nombreUnidad";
            string queryInsertar = "INSERT INTO unidad (nombreUnidad) VALUES (@nombreUnidad)";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    // Verificar si ya existe
                    using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conexion))
                    {
                        cmdVerificar.Parameters.AddWithValue("@nombreUnidad", nombreUnidad);
                        long existe = (long)cmdVerificar.ExecuteScalar();

                        if (existe > 0)
                        {
                            MessageBox.Show("La unidad ya existe.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Insertar nueva unidad
                    using (MySqlCommand cmdInsertar = new MySqlCommand(queryInsertar, conexion))
                    {
                        cmdInsertar.Parameters.AddWithValue("@nombreUnidad", nombreUnidad);
                        int filasAfectadas = cmdInsertar.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al crear la unidad: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool ActualizarUnidad(int idUnidad, string nuevoNombre)
        {
            string queryVerificar = "SELECT COUNT(*) FROM unidad WHERE nombreUnidad = @nombreUnidad AND id_unidad != @id";
            string queryActualizar = "UPDATE unidad SET nombreUnidad = @nombreUnidad WHERE id_unidad = @id";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    // Verifica si ya existe otra unidad con el mismo nombre
                    using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conexion))
                    {
                        cmdVerificar.Parameters.AddWithValue("@nombreUnidad", nuevoNombre);
                        cmdVerificar.Parameters.AddWithValue("@id", idUnidad);
                        long existe = (long)cmdVerificar.ExecuteScalar();

                        if (existe > 0)
                        {
                            MessageBox.Show("Ya existe otra unidad con ese nombre.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Actualiza el nombre
                    using (MySqlCommand cmdActualizar = new MySqlCommand(queryActualizar, conexion))
                    {
                        cmdActualizar.Parameters.AddWithValue("@nombreUnidad", nuevoNombre);
                        cmdActualizar.Parameters.AddWithValue("@id", idUnidad);
                        int filas = cmdActualizar.ExecuteNonQuery();
                        return filas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la unidad: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


        //MAS FUNCIONES DE CAJA
        public int ObtenerUltimaCajaInicializada()
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = @"
            SELECT id_caja 
            FROM caja 
            WHERE id_estado_caja = 2 
            ORDER BY fecha DESC 
            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    object resultado = cmd.ExecuteScalar();
                    return resultado != null ? Convert.ToInt32(resultado) : -1;
                }
            }
        }






        // -------------------- Importar--------------------
        public void ImportarBaseDeDatos(string rutaArchivo)
        {
            string usuario = "root";
            string contraseña = "Caguama2025";
            string baseDeDatos = "lacaguamabd";
            string rutaMysql = @"C:\Program Files\MySQL\MySQL Server 9.3\bin";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = rutaMysql,
                Arguments = $"-u {usuario} -p{contraseña} {baseDeDatos} < \"{rutaArchivo}\"",
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proceso = Process.Start(psi))
            {
                proceso.WaitForExit();
            }

            MessageBox.Show("Base de datos importada correctamente.");
        }
        public DatosCorte ObtenerDatosUltimoCorte(int idCorte)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = @"
            SELECT 
                cc.cantidad AS MontoContado,
                c.cantidad AS CajaInicial,
                (SELECT SUM(o.total) FROM ordenes o WHERE o.id_caja = c.id_caja AND o.tipo_pago = 1 AND o.id_estadoO = 2) AS TotalGenerado,
                (SELECT SUM(g.cantidad) FROM gastos g WHERE g.id_caja = c.id_caja) AS TotalGastos,
                u.nombre AS NombreCajero
            FROM corte_de_caja cc
            JOIN caja c ON cc.id_caja = c.id_caja
            JOIN usuarios u ON cc.id_usuario = u.id_usuario
            WHERE cc.id_corte = @idCorte";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idCorte", idCorte);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DatosCorte
                            {
                                MontoContado = reader.GetDecimal("MontoContado"),
                                CajaInicial = reader.GetDecimal("CajaInicial"),
                                TotalGenerado = reader.IsDBNull(reader.GetOrdinal("TotalGenerado")) ? 0 : reader.GetDecimal("TotalGenerado"),
                                TotalGastos = reader.IsDBNull(reader.GetOrdinal("TotalGastos")) ? 0 : reader.GetDecimal("TotalGastos"),
                                NombreCajero = reader.GetString("NombreCajero")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public DatosCorteGeneral ObtenerUltimoCorteGeneral()
        {
            DatosCorteGeneral corte = null;

            string query = @"
                            SELECT 
                                cg.id_caja,
                                cg.cant_efectivo,
                                cg.cant_tarjeta,
                                cg.descuento,
                                cg.total_gastos_general,
                                u.nombre AS nombre_cajero,
                                cg.fecha
                            FROM corte_general cg
                            JOIN usuarios u ON cg.id_usuario = u.id_usuario
                            ORDER BY cg.fecha DESC
                            LIMIT 1;
                        ";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            corte = new DatosCorteGeneral
                            {
                                IdCaja = reader.GetInt32("id_caja"),
                                CantEfectivo = reader.GetDecimal("cant_efectivo"),
                                CantTarjeta = reader.GetDecimal("cant_tarjeta"),
                                Descuento = reader.GetDecimal("descuento"),
                                TotalGastosGeneral = reader.GetDecimal("total_gastos_general"),
                                NombreCajero = reader.GetString("nombre_cajero"),
                                Fecha = reader.GetDateTime("fecha")
                            };
                        }
                    }
                }
            }

            return corte;
        }

        //CODIGO DE AGREGAR CATEGORIA PARA PLATOS
        public DataTable ObtenerCategoriasPlatos()
        {
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string query = "SELECT id_categoriaP, tipo FROM categoria_platos";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
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
                MessageBox.Show("Error al obtener categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public bool AgregarCategoriaPlato(string tipo)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
                {
                    conn.Open();

                    // Validar si ya existe la categoría (ignorando mayúsculas)
                    string queryVerificar = "SELECT COUNT(*) FROM categoria_platos WHERE LOWER(tipo) = LOWER(@tipo)";
                    using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conn))
                    {
                        cmdVerificar.Parameters.AddWithValue("@tipo", tipo);
                        int existe = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                        if (existe > 0)
                        {
                            MessageBox.Show("Ya existe una categoría de plato con ese nombre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Si no existe, insertar
                    string queryInsertar = "INSERT INTO categoria_platos (tipo) VALUES (@tipo)";
                    using (MySqlCommand cmdInsertar = new MySqlCommand(queryInsertar, conn))
                    {
                        cmdInsertar.Parameters.AddWithValue("@tipo", tipo);
                        int filasAfectadas = cmdInsertar.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ActualizarCategoriaPlato(int idCategoria, string nuevoTipo)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string query = "UPDATE categoria_platos SET tipo = @tipo WHERE id_categoriaP = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tipo", nuevoTipo);
                        cmd.Parameters.AddWithValue("@id", idCategoria);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // CODIGO DE AGREGAR CATEGORÍAS
        public DataTable ObtenerCategoriasBebidas()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "SELECT id_categoria AS 'ID', tipo AS 'Categoría' FROM categorias";

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
                MessageBox.Show("Error al obtener categorías de bebidas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public bool AgregarCategoriaBebida(string nombreCategoria)
        {
            bool exito = false;

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Validar si ya existe (ignorando mayúsculas)
                    string queryVerificar = "SELECT COUNT(*) FROM categorias WHERE LOWER(tipo) = LOWER(@tipo)";
                    using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conexion))
                    {
                        cmdVerificar.Parameters.AddWithValue("@tipo", nombreCategoria);
                        int count = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Ya existe una categoría con ese nombre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Si no existe, insertar
                    string queryInsertar = "INSERT INTO categorias (tipo) VALUES (@tipo)";
                    using (MySqlCommand cmdInsertar = new MySqlCommand(queryInsertar, conexion))
                    {
                        cmdInsertar.Parameters.AddWithValue("@tipo", nombreCategoria);
                        exito = cmdInsertar.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar categoría: " + ex.Message);
            }

            return exito;
        }


        public bool ActualizarCategoriaBebida(int idCategoria, string nuevoNombre)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string query = "UPDATE categorias SET tipo = @nuevoNombre WHERE id_categoria = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@id", idCategoria);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


    }

}




