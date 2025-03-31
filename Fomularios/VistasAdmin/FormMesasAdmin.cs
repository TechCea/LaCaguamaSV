using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormMesasAdmin : Form
    {
        private Conexion conexion = new Conexion();
        private int mesaSeleccionada = -1; 

        public FormMesasAdmin()
        {
            InitializeComponent();
            CargarMesasDesdeBD();
        }

        private void CargarMesasDesdeBD()
        {
            using (MySqlConnection conn = conexion.EstablecerConexion())
            {
                if (conn == null)
                {
                    MessageBox.Show("No se pudo conectar a la base de datos.");
                    return;
                }

                try
                {
                    string query = @"
                            SELECT m.id_mesa, m.nombreMesa, e.nombreEstadoM 
                            FROM mesas m
                            JOIN estado_mesa e ON m.id_estadoM = e.id_estadoM
                            ORDER BY m.id_mesa ASC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    int index = 1;

                    while (reader.Read())
                    {
                        int idMesa = reader.GetInt32("id_mesa"); // ID de la mesa
                        string nombreMesa = reader.GetString("nombreMesa");
                        string estadoMesa = reader.GetString("nombreEstadoM");

                        Panel panelMesa = Controls.Find($"panel{index}", true).FirstOrDefault() as Panel;
                        Label lblMesa = Controls.Find($"lblmesa{index}", true).FirstOrDefault() as Label;
                        PictureBox imgMesa = Controls.Find($"imgmesa{index}", true).FirstOrDefault() as PictureBox; 

                        if (panelMesa != null && lblMesa != null && imgMesa != null)
                        {
                            lblMesa.Text = nombreMesa;
                            panelMesa.Tag = idMesa; 
                            imgMesa.Tag = idMesa; 

                            switch (estadoMesa.ToLower())
                            {
                                case "disponible":
                                    panelMesa.BackColor = Color.Green;
                                    break;
                                case "ocupado":
                                    panelMesa.BackColor = Color.Red;
                                    break;
                                case "reservado":
                                    panelMesa.BackColor = Color.Gray;
                                    break;
                                default:
                                    panelMesa.BackColor = Color.White;
                                    break;
                            }

                            panelMesa.Click -= SeleccionarMesa;
                            imgMesa.Click -= SeleccionarMesa;

                            panelMesa.Click += SeleccionarMesa;
                            imgMesa.Click += SeleccionarMesa;
                        }

                        index++;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar mesas: " + ex.Message);
                }
            }
        }

        private void SeleccionarMesa(object sender, EventArgs e)
        {
            // Primero, eliminamos el borde de todos los paneles (panel1, panel2, ..., panel23)
            for (int i = 1; i <= 23; i++)
            {
                Panel panelMesa = Controls.Find($"panel{i}", true).FirstOrDefault() as Panel;
                if (panelMesa != null)
                {
                    // Restaurar el borde de todos los paneles
                    panelMesa.BorderStyle = BorderStyle.None;
                }
            }

            // Asignar la nueva mesa seleccionada, ya sea desde un Panel o desde una Imagen
            if (sender is Panel panel)
            {
                mesaSeleccionada = (int)panel.Tag;  // Actualiza la mesa seleccionada con el Tag de la mesa
            }
            else if (sender is PictureBox img)
            {
                mesaSeleccionada = (int)img.Tag;   // Si se selecciona desde la imagen
            }

            // Actualizar el texto del label para reflejar la mesa seleccionada
            Label lblMesa = Controls.Find($"lblmesa{mesaSeleccionada}", true).FirstOrDefault() as Label;
            if (lblMesa != null)
            {
                lblMesaSeleccionada.Text = $"Mesa seleccionada: {lblMesa.Text}";
            }

            // Resaltar la mesa seleccionada con un borde 3D
            Panel panelSeleccionado = Controls.Find($"panel{mesaSeleccionada}", true).FirstOrDefault() as Panel;
            if (panelSeleccionado != null)
            {
                // Añadir el borde a la mesa seleccionada
                panelSeleccionado.BorderStyle = BorderStyle.Fixed3D;
            }
        }


        private void CambiarEstadoMesa(int nuevoEstado)
        {
            if (mesaSeleccionada == -1)
            {
                MessageBox.Show("Seleccione una mesa primero.");
                return;
            }

            using (MySqlConnection conn = conexion.EstablecerConexion())
            {
                if (conn == null)
                {
                    MessageBox.Show("No se pudo conectar a la base de datos.");
                    return;
                }

                try
                {
                    string query = "UPDATE mesas SET id_estadoM = @nuevoEstado WHERE id_mesa = @idMesa";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@idMesa", mesaSeleccionada);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Estado de la mesa actualizado.");
                        CargarMesasDesdeBD(); // Recargar mesas para actualizar colores
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar la mesa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar mesa: " + ex.Message);
                }
            }
        }


        private void btnDisponible_Click_1(object sender, EventArgs e)
        {
            CambiarEstadoMesa(1); // ID 1 = Disponible
        }

        private void btnReserva_Click(object sender, EventArgs e)
        {
            CambiarEstadoMesa(3); // ID 3 = Reservada
        }

        private void btnOcupada_Click(object sender, EventArgs e)
        {
            CambiarEstadoMesa(2); // ID 2 = Ocupada
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual de mesas
            this.Close();

            // Crear una nueva instancia de FormAdmin y mostrarla
            FormAdmin formAdmin = new FormAdmin();
            formAdmin.Show();
        }
    }
}