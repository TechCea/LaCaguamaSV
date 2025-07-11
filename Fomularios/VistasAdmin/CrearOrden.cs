﻿using System;
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
using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;
using static LaCaguamaSV.Login;

namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class CrearOrden: Form
    {
        public int OrdenCreadaId { get; private set; } = -1;
        public CrearOrden()
        {
            InitializeComponent();
            CargarTipoPago();
            CargarMesas();


            // Configuración de estilo para el ComboBox
            cmbMesas.FlatStyle = FlatStyle.Flat;
            cmbMesas.BackColor = SystemColors.Window;
            cmbMesas.ForeColor = SystemColors.WindowText;

            // Configurar el botón desplegable
            cmbMesas.DropDownStyle = ComboBoxStyle.DropDownList;

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            this.Size = new Size(1000, 500); // Establece un tamaño fijo

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void CargarTipoPago()
        {
            DataTable dtPagos = OrdenesD.ObtenerTiposPago();
            cmbTipoPago.DataSource = dtPagos;
            cmbTipoPago.DisplayMember = "nombrePago"; // Mostrar el nombre del método de pago
            cmbTipoPago.ValueMember = "id_pago"; // Guardar el ID del método de pago

            // Seleccionar por defecto "Efectivo"
            foreach (DataRow row in dtPagos.Rows)
            {
                if (row["nombrePago"].ToString().ToLower() == "efectivo")
                {
                    cmbTipoPago.SelectedValue = row["id_pago"];
                    break;
                }
            }
        }

        private void CargarMesas()
        {
            // Configurar estilos del ComboBox antes de cargar los datos
            cmbMesas.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cmbMesas.DropDownHeight = 200; // Altura del menú desplegable
            cmbMesas.Height = 35; // Altura del control
            cmbMesas.DropDownWidth = cmbMesas.Width; // Ancho del menú desplegable

            DataTable dtMesas = new DataTable();
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
                SELECT m.id_mesa, m.nombreMesa, m.id_estadoM,
                    CASE 
                        WHEN m.nombreMesa = 'Para Llevar' THEN 1
                        WHEN m.id_estadoM = 1 THEN 1
                        ELSE 0
                    END AS disponible
                FROM mesas m
                ORDER BY m.nombreMesa";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dtMesas);
                        }
                    }
                }

                cmbMesas.DataSource = dtMesas;
                cmbMesas.DisplayMember = "nombreMesa";
                cmbMesas.ValueMember = "id_mesa";

                // Personalizar el dibujado del ComboBox para mejor visualización
                cmbMesas.DrawMode = DrawMode.OwnerDrawVariable;
                cmbMesas.DrawItem += CmbMesas_DrawItem;
                cmbMesas.MeasureItem += CmbMesas_MeasureItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar mesas: {ex.Message}");
            }
        }

        private void CmbMesas_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            // Obtener el texto del ítem
            string text = cmbMesas.GetItemText(cmbMesas.Items[e.Index]);

            // Configurar el color del texto según disponibilidad
            Brush brush;
            DataRowView row = (DataRowView)cmbMesas.Items[e.Index];
            bool disponible = Convert.ToBoolean(row["disponible"]);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                brush = Brushes.White;
            }
            else
            {
                brush = disponible ? Brushes.Black : Brushes.Gray;
            }

            // Dibujar el texto con el estilo y tamaño adecuados
            e.Graphics.DrawString(text,
                                 new Font("Segoe UI", 12F, FontStyle.Regular),
                                 brush,
                                 e.Bounds);

            e.DrawFocusRectangle();
        }

        private void CmbMesas_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // Establecer la altura de cada ítem en el menú desplegable
            e.ItemHeight = 30;
        }

        

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            // Validar longitud máxima (50 caracteres)
            if (txtNombreCliente.Text.Length > 50)
            {
                txtNombreCliente.Text = txtNombreCliente.Text.Substring(0, 50);
                txtNombreCliente.SelectionStart = 50;
                return;
            }

            // Validar caracteres permitidos (letras, espacios, apóstrofes y guiones)
            string textoValidado = new string(txtNombreCliente.Text
                .Where(c => char.IsLetter(c) || c == ' ' || c == '\'' || c == '-').ToArray());

            if (txtNombreCliente.Text != textoValidado)
            {
                int posicion = txtNombreCliente.SelectionStart;
                txtNombreCliente.Text = textoValidado;
                txtNombreCliente.SelectionStart = posicion - 1;
            }

            // Validar que no comience con espacio
            if (txtNombreCliente.Text.StartsWith(" "))
            {
                txtNombreCliente.Text = txtNombreCliente.Text.TrimStart();
                txtNombreCliente.SelectionStart = 0;
            }

            // Validar múltiples espacios consecutivos
            if (txtNombreCliente.Text.Contains("  "))
            {
                int posicion = txtNombreCliente.SelectionStart;
                txtNombreCliente.Text = System.Text.RegularExpressions.Regex.Replace(txtNombreCliente.Text, @"\s+", " ");
                txtNombreCliente.SelectionStart = posicion - 1;
            }

            // Actualizar color de borde según validación
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                txtNombreCliente.BorderStyle = BorderStyle.FixedSingle;
                txtNombreCliente.BackColor = SystemColors.Window;
            }
            else
            {
                txtNombreCliente.BorderStyle = BorderStyle.Fixed3D;
                txtNombreCliente.BackColor = Color.LightYellow;
            }
        }

        private void txtNombreCliente_Enter(object sender, EventArgs e)
        {
            // Resaltar cuando el campo recibe foco
            txtNombreCliente.BackColor = Color.LightYellow;
            txtNombreCliente.BorderStyle = BorderStyle.Fixed3D;
        }

        private void txtNombreCliente_Leave(object sender, EventArgs e)
        {
            // Normalizar cuando pierde el foco
            txtNombreCliente.BackColor = SystemColors.Window;
            txtNombreCliente.BorderStyle = BorderStyle.FixedSingle;

            // Capitalizar nombre (opcional)
            if (!string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                txtNombreCliente.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreCliente.Text.ToLower());
            }
        }

        private void cmbMesas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resaltar visualmente cuando se selecciona una mesa ocupada
            if (cmbMesas.SelectedValue != null)
            {
                DataRowView selectedRow = (DataRowView)cmbMesas.SelectedItem;
                bool disponible = Convert.ToBoolean(selectedRow["disponible"]);

                if (!disponible && selectedRow["nombreMesa"].ToString() != "Para Llevar")
                {
                    cmbMesas.BackColor = Color.LightYellow;
                }
                else
                {
                    cmbMesas.BackColor = SystemColors.Window;
                }
            }
        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre del cliente", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string nombreCliente = txtNombreCliente.Text;
                int idMesa = Convert.ToInt32(cmbMesas.SelectedValue);
                int tipoPago = Convert.ToInt32(cmbTipoPago.SelectedValue);

                // Verificar disponibilidad (con confirmación para mesas ocupadas)
                if (!MesaDisponible(idMesa))
                {
                    return;
                }

                // Crear la orden vacía
                int idOrden = OrdenesD.CrearOrdenVacia(nombreCliente, idMesa, tipoPago);

                if (idOrden > 0)
                {
                    OrdenCreadaId = idOrden;
                    this.DialogResult = DialogResult.OK; // Cambiar a OK para indicar éxito
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Error al crear la orden", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la orden: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool MesaDisponible(int idMesa)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = "SELECT id_estadoM, nombreMesa FROM mesas WHERE id_mesa = @idMesa";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idMesa", idMesa);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nombreMesa = reader.GetString("nombreMesa");
                                int estado = reader.GetInt32("id_estadoM");

                                // "Para Llevar" siempre está disponible
                                if (nombreMesa.Equals("PARA LLEVAR", StringComparison.OrdinalIgnoreCase))
                                    return true;

                                // Si la mesa está ocupada, preguntar al usuario
                                if (estado != 1) // 1 = Disponible
                                    return MostrarConfirmacionMesaOcupada(nombreMesa);

                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        private bool MostrarConfirmacionMesaOcupada(string nombreMesa)
        {
            // No mostrar confirmación si es "Para Llevar"
            if (nombreMesa == "PARA LLEVAR" || nombreMesa == "Para Llevar")
                return true;

            DialogResult result = MessageBox.Show(
                $"La mesa {nombreMesa} está ocupada. ¿Desea agregar otra orden a esta mesa?",
                "Mesa Ocupada",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
