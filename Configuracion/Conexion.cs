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
        private static string bd = "LaCaguamaBD";
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
                    string query = "SELECT \r\n                    u.usuario AS 'Usuario', \r\n                    u.nombre AS 'Nombre',\r\n                    u.correo AS 'Correo',\r\n                    u.telefono_contacto AS 'Teléfono',\r\n        u.contrasenya AS 'Contraseña',\r\n            r.nombre_rol AS 'Rol'\r\n                FROM usuarios u\r\n                JOIN roles r ON u.id_rol = r.id_rol";
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


    }
}