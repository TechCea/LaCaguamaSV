using LaCaguamaSV.Configuracion;
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
    public partial class FormRecetaPlato : Form
    {
        Conexion conexion = new Conexion();

        public FormRecetaPlato(int idPlato, string nombrePlato)
        {
            InitializeComponent();

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            // Si el usuario no es administrador, cierra el formulario
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asignar los valores a los labels
            lblIDPLATO.Text = idPlato.ToString();
            lblPlato.Text = nombrePlato;
            MostrarIngredientesEnPanel(idPlato);
            MostrarInventarioEnGrid();
            dataGridViewReceta.CellClick += dataGridViewReceta_CellClick;
            dataGridViewReceta.MultiSelect = false;
        }

        private void MostrarIngredientesEnPanel(int idPlato)
        {
            flowLayoutPanelReceta.Controls.Clear(); // Limpiar el panel antes de agregar nuevos ingredientes

            DataTable dtIngredientes = conexion.ObtenerIngredientesPorPlato(idPlato);

            foreach (DataRow row in dtIngredientes.Rows)
            {
                string nombreIngrediente = row["nombreProducto"].ToString();
                decimal cantidad = Convert.ToDecimal(row["cantidad_necesaria"]);

                // Crear el panel para cada ingrediente
                Panel panelIngrediente = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGray,
                    Margin = new Padding(5),
                    Padding = new Padding(5),
                    Width = flowLayoutPanelReceta.Width - 25,
                    Height = 40
                };

                // Crear la etiqueta para mostrar el nombre y cantidad
                Label lblIngrediente = new Label
                {
                    Text = $"{nombreIngrediente} - {cantidad} unidades",
                    ForeColor = Color.Black,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Cursor = Cursors.Hand // Cambiar el cursor para indicar que se puede hacer clic
                };

                // Evento al hacer clic en la etiqueta (para editar la cantidad)
                lblIngrediente.Click += (sender, e) => {
                    // Crear el cuadro de diálogo para ingresar la nueva cantidad
                    EditarCantidadIngrediente(idPlato, nombreIngrediente, cantidad);
                };

                // Crear el botón para eliminar el ingrediente
                Button btnEliminar = new Button
                {
                    Text = "X",
                    ForeColor = Color.White,
                    BackColor = Color.Red,
                    FlatStyle = FlatStyle.Flat,
                    Width = 30,
                    Dock = DockStyle.Right,
                    Cursor = Cursors.Hand,
                    Tag = nombreIngrediente // Guardamos el nombre del ingrediente en el botón
                };

                // Evento para eliminar el ingrediente al hacer clic en el botón
                btnEliminar.Click += (sender, e) => {
                    Button btn = (Button)sender;
                    string ingredienteAEliminar = btn.Tag.ToString();

                    DialogResult result = MessageBox.Show($"¿Seguro que quieres eliminar {ingredienteAEliminar}?",
                                                          "Confirmar eliminación",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        if (conexion.EliminarIngredienteDeReceta(idPlato, ingredienteAEliminar))
                        {
                            MessageBox.Show("Ingrediente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MostrarIngredientesEnPanel(idPlato); // Recargar la lista
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el ingrediente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };

                // Agregar la etiqueta y el botón al panel
                panelIngrediente.Controls.Add(lblIngrediente);
                panelIngrediente.Controls.Add(btnEliminar);
                flowLayoutPanelReceta.Controls.Add(panelIngrediente);
            }

        }

        private void EditarCantidadIngrediente(int idPlato, string nombreIngrediente, decimal cantidadActual)
        {
            using (var inputDialog = new Form())
            {
                inputDialog.Text = $"Editar cantidad de {nombreIngrediente}";
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.MaximizeBox = false;
                inputDialog.MinimizeBox = false;
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.Size = new Size(300, 150);

                Label lblCantidad = new Label()
                {
                    Text = "Ingrese la nueva cantidad:",
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(10)
                };

                NumericUpDown numericUpDown = new NumericUpDown()
                {
                    Minimum = 1,
                    Maximum = 1000,
                    Value = cantidadActual,  // Mostrar la cantidad actual
                    Dock = DockStyle.Top,
                    TextAlign = HorizontalAlignment.Center,
                    Font = new Font("Arial", 12, FontStyle.Bold)
                };

                Button okButton = new Button()
                {
                    Text = "Aceptar",
                    DialogResult = DialogResult.OK,
                    Dock = DockStyle.Bottom,
                    BackColor = Color.LightGreen,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };

                inputDialog.Controls.Add(numericUpDown);
                inputDialog.Controls.Add(lblCantidad);
                inputDialog.Controls.Add(okButton);
                inputDialog.AcceptButton = okButton;

                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    decimal nuevaCantidad = numericUpDown.Value;
                    if (conexion.EditarCantidadIngrediente(idPlato, nombreIngrediente, nuevaCantidad))
                    {
                        MessageBox.Show("Cantidad actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostrarIngredientesEnPanel(idPlato); // Recargar los ingredientes después de actualizar
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar la cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridViewReceta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener fila seleccionada
                DataGridViewRow filaSeleccionada = dataGridViewReceta.Rows[e.RowIndex];

                int idInventario = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                string nombreIngrediente = filaSeleccionada.Cells["Nombre Producto"].Value.ToString();

                // Crear formulario de entrada de cantidad
                using (var inputDialog = new Form())
                {
                    inputDialog.Text = $"Seleccionar cantidad para {nombreIngrediente}";
                    inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                    inputDialog.MaximizeBox = false;
                    inputDialog.MinimizeBox = false;
                    inputDialog.StartPosition = FormStartPosition.CenterParent;
                    inputDialog.Size = new Size(300, 150);

                    Label lblCantidad = new Label()
                    {
                        Text = "Ingrese la cantidad:",
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Padding = new Padding(10)
                    };

                    NumericUpDown numericUpDown = new NumericUpDown()
                    {
                        Minimum = 1,
                        Maximum = 100,
                        Value = 1,
                        Dock = DockStyle.Top,
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font("Arial", 12, FontStyle.Bold)
                    };

                    Button okButton = new Button()
                    {
                        Text = "Aceptar",
                        DialogResult = DialogResult.OK,
                        Dock = DockStyle.Bottom,
                        BackColor = Color.LightGreen,
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };

                    inputDialog.Controls.Add(numericUpDown);
                    inputDialog.Controls.Add(lblCantidad);
                    inputDialog.Controls.Add(okButton);
                    inputDialog.AcceptButton = okButton;

                    if (inputDialog.ShowDialog() == DialogResult.OK)
                    {
                        decimal cantidad = numericUpDown.Value;
                        int idPlato = Convert.ToInt32(lblIDPLATO.Text);

                        // Insertar en la base de datos
                        if (conexion.AgregarIngredienteAReceta(idPlato, idInventario, cantidad))
                        {
                            MessageBox.Show("Ingrediente agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MostrarIngredientesEnPanel(idPlato); // Refrescar el panel
                        }
                        else
                        {
                            MessageBox.Show("Error al agregar el ingrediente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void MostrarInventarioEnGrid()
        {
            dataGridViewReceta.DataSource = conexion.ObtenerInventario();
            dataGridViewReceta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnCerrar_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
