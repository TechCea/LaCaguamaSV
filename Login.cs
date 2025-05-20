using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using LaCaguamaSV.Fomularios.VistasAdmin;
using LaCaguamaSV.Fomularios.VistasUsuario;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LaCaguamaSV
{
    public partial class Login: Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;


        Configuracion.Conexion conexion = new Configuracion.Conexion();
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));


            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;


            // Aplicar esquinas redondeadas a controles específicos
            RoundedControl.ApplyRoundedCorners(panel1, 15);   // Redondear un Panel
          



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

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnMaximizar_Click_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnMinimizar_Click_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public class RoundedControl
        {
            public static void ApplyRoundedCorners(Control control, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }
    }
}
