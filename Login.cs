﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using LaCaguamaSV.Fomularios.VistasAdmin;
using LaCaguamaSV.Fomularios.VistasUsuario;
using MySql.Data.MySqlClient;

namespace LaCaguamaSV
{
    public partial class Login: Form
    {
        Configuracion.Conexion conexion = new Configuracion.Conexion();
        public Login()
        {
            InitializeComponent();

            Configuracion.Conexion conexion = new Configuracion.Conexion();
            conexion.EstablecerConexion();

            txtContrasena.PasswordChar = '*';
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;

            // Obtiene los datos del usuario
            var usuarioData = conexion.ObtenerDatosUsuario(usuario, contrasena);

            if (usuarioData != null)
            {
                // Guarda los datos en la sesión
                SesionUsuario.IdUsuario = usuarioData.Item1;
                SesionUsuario.NombreUsuario = usuario;
                SesionUsuario.Rol = usuarioData.Item2;

                MessageBox.Show("Inicio de sesión exitoso");

                // Redirige según el rol
                if (SesionUsuario.Rol == 1) // Administrador
                {
                    FormAdmin formAdmin = new FormAdmin();
                    this.Hide();
                    formAdmin.ShowDialog();
                }
                else if (SesionUsuario.Rol == 2) // Usuario normal
                {
                    FormUsuario formUsuario = new FormUsuario(); // Crea este formulario
                    this.Hide();
                    formUsuario.ShowDialog();
                }

                // Cierra el login después de abrir otro formulario
                this.Close();
            }
            else
            {

                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Limpiar los campos de usuario y contraseña
                txtUsuario.Clear();
                txtContrasena.Clear();
            }
        }

    }
}
