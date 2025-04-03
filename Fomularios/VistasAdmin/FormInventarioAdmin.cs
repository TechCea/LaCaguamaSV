using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Fomularios.VistasUsuario;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormInventarioAdmin : Form
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

        public FormInventarioAdmin()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Agregar evento para mover el formulario
            this.MouseDown += new MouseEventHandler(FormInventarioAdmin_MouseDown);

            // Aplicar esquinas redondeadas a controles específicos
            RoundedControl.ApplyRoundedCorners(groupBox1, 15);   // Redondear un Panel
            RoundedControl.ApplyRoundedCorners(groupBox2, 15);   // Redondear un Panel

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FormGestionarProvAdmin formProveedor = new FormGestionarProvAdmin())
            {
                this.Hide();
                this.Close();// Oculta el menú
                formProveedor.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }
        }

        private void btnBebidas_Click(object sender, EventArgs e)
        {


            using (FormInventarioBebidasAdmin formInventarioBebidas = new FormInventarioBebidasAdmin())
            {
                this.Hide();
                this.Close();// Oculta el menú
                formInventarioBebidas.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }
        }

        private void btnComidas_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            FormIngredientesInv formIngredientes = new FormIngredientesInv();
            formIngredientes.ShowDialog();
        }

        private void btnExtras_Click(object sender, EventArgs e)
        {
            using (FormInventarioExtrasAdmin FormInventarioExtras = new FormInventarioExtrasAdmin())
            {
                this.Hide();
                this.Close();
                FormInventarioExtras.ShowDialog(); // Muestra el formulario 
                this.Show(); // Vuelve a mostrar el menú cuando el formulario de proveedore se cierra
            }

        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormInventarioAdmin_Load(object sender, EventArgs e)
        {

        }
        private void FormInventarioAdmin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
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
