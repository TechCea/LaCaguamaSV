using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaCaguamaSV.Configuracion;
using MySql.Data.MySqlClient;


namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    public partial class FormGestionPromociones : Form
    {
        private Font itemFont = new Font("Arial", 10, FontStyle.Regular);
        private Font emptyListFont = new Font("Arial", 10, FontStyle.Italic);
        private int? promocionSeleccionadaId = null;
        private DataTable itemsAgregados = new DataTable();

        public FormGestionPromociones()
        {
            InitializeComponent();
            ConfigurarTablaItems();
            CargarPromociones();
            CargarBotonesCategorias();
            ConfigurarPanelItems();
            ConfigurarValidaciones();

            dataGridViewMenu.CellDoubleClick += DataGridViewMenu_CellDoubleClick;

            txtPrecio.Enter += txtPrecio_Enter;
            txtPrecio.Leave += txtPrecio_Leave;
            txtPrecio.KeyPress += txtPrecio_KeyPress;
            txtPrecio.TextChanged += txtPrecio_TextChanged;
        }
        private void ConfigurarTablaItems()
        {
            itemsAgregados.Columns.Add("Tipo", typeof(string));
            itemsAgregados.Columns.Add("ID", typeof(int));
            itemsAgregados.Columns.Add("Nombre", typeof(string));
            itemsAgregados.Columns.Add("Cantidad", typeof(int));
        }

        private void ConfigurarPanelItems()
        {
            panelItems.AutoScroll = true;
            panelItems.BorderStyle = BorderStyle.FixedSingle;
            panelItems.Padding = new Padding(5);
        }

        private void ConfigurarValidaciones()
        {
            // Validación para el nombre
            txtNombre.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    errorProvider.SetError(txtNombre, "El nombre es requerido");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(txtNombre, "");
                }
            };

            // Validación para el precio (ahora en TextBox)
            txtPrecio.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    errorProvider.SetError(txtPrecio, "El precio es requerido");
                    e.Cancel = true;
                }
                else if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
                {
                    errorProvider.SetError(txtPrecio, "El precio debe ser un número mayor a 0");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(txtPrecio, "");
                }
            };

            // Validación para la fecha de inicio
            dateInicio.Validating += (s, e) =>
            {
                if (dateInicio.Value < DateTime.Today)
                {
                    errorProvider.SetError(dateInicio, "La fecha no puede ser anterior a hoy");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(dateInicio, "");
                }
            };

            // Validación para la fecha fin (si no está marcado sin fin)
            checkSinFin.CheckedChanged += (s, e) =>
            {
                dateFin.Enabled = !checkSinFin.Checked;
                if (checkSinFin.Checked)
                {
                    errorProvider.SetError(dateFin, "");
                }
            };

            dateFin.Validating += (s, e) =>
            {
                if (!checkSinFin.Checked && dateFin.Value < dateInicio.Value)
                {
                    errorProvider.SetError(dateFin, "La fecha fin no puede ser anterior a la fecha inicio");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(dateFin, "");
                }
            };
        }


        private void CargarBotonesCategorias()
        {
            btnPlatos.Tag = "PLATO";
            btnBebidas.Tag = "BEBIDA";
            btnExtras.Tag = "EXTRA";
        }

        private void CargarPromociones()
        {
            dataGridViewPromociones.AutoGenerateColumns = false;
            dataGridViewPromociones.Columns.Clear();

            // Configurar columnas con estilos mejorados
            dataGridViewPromociones.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "id_promocion",
                HeaderText = "ID",
                Name = "id_promocion",
                Width = 50,
                Visible = false
            });

            dataGridViewPromociones.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "nombre",
                HeaderText = "Nombre",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Arial", 9, FontStyle.Bold) }
            });

            dataGridViewPromociones.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "precio_especial",
                HeaderText = "Precio",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Font = new Font("Arial", 9) }
            });

            dataGridViewPromociones.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "fecha_inicio",
                HeaderText = "Inicio",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d", Font = new Font("Arial", 9) }
            });

            dataGridViewPromociones.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "fecha_fin",
                HeaderText = "Fin",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d", Font = new Font("Arial", 9) }
            });

            dataGridViewPromociones.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "activa",
                HeaderText = "Activa",
                Width = 60
            });

            // Estilo para el encabezado
            dataGridViewPromociones.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridViewPromociones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewPromociones.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dataGridViewPromociones.EnableHeadersVisualStyles = false;

            dataGridViewPromociones.DataSource = PromocionesD.ObtenerTodasPromociones();
        }

        private void CargarItems(string tipo)
        {
            try
            {
                // Configurar DataGridView
                dataGridViewMenu.AutoGenerateColumns = false;
                dataGridViewMenu.Columns.Clear();
                dataGridViewMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewMenu.MultiSelect = false;

                // Configurar columnas
                dataGridViewMenu.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "ID",
                    HeaderText = "ID",
                    Name = "ID",
                    Width = 50,
                    Visible = false
                });

                dataGridViewMenu.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "nombre",
                    HeaderText = "Nombre",
                    Name = "nombre",
                    Width = 200,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Font = new Font("Arial", 9, FontStyle.Regular),
                        ForeColor = Color.Black
                    }
                });

                dataGridViewMenu.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "precioUnitario",
                    HeaderText = "Precio",
                    Name = "precioUnitario",
                    Width = 80,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "C2",
                        Font = new Font("Arial", 9, FontStyle.Regular),
                        ForeColor = Color.DarkGreen,
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                // Estilos para el grid
                dataGridViewMenu.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGreen;
                dataGridViewMenu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridViewMenu.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                dataGridViewMenu.EnableHeadersVisualStyles = false;
                dataGridViewMenu.RowHeadersVisible = false;
                dataGridViewMenu.AllowUserToAddRows = false;
                dataGridViewMenu.AllowUserToDeleteRows = false;
                dataGridViewMenu.ReadOnly = true;
                dataGridViewMenu.BackgroundColor = SystemColors.Window;

                // Asignar tipo y evento DoubleClick
                dataGridViewMenu.Tag = tipo;
                dataGridViewMenu.CellDoubleClick -= DataGridViewMenu_CellDoubleClick; // Remover si ya existe
                dataGridViewMenu.CellDoubleClick += DataGridViewMenu_CellDoubleClick;

                // Obtener datos según el tipo
                DataTable datos = null;
                switch (tipo)
                {
                    case "PLATO":
                        datos = OrdenesD.ObtenerPlatos();
                        break;
                    case "BEBIDA":
                        datos = OrdenesD.ObtenerBebidas();
                        break;
                    case "EXTRA":
                        datos = OrdenesD.ObtenerExtras();
                        break;
                }

                // Verificar si hay datos
                if (datos == null || datos.Rows.Count == 0)
                {
                    MessageBox.Show($"No se encontraron items de tipo {tipo}", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Validar estructura de datos
                if (!datos.Columns.Contains("ID") || !datos.Columns.Contains("nombre") ||
                    !datos.Columns.Contains("precioUnitario"))
                {
                    MessageBox.Show("La estructura de datos no contiene las columnas esperadas", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Asignar datos y configurar selección
                dataGridViewMenu.DataSource = datos;
                dataGridViewMenu.ClearSelection();

                // Agregar tooltip para indicar el doble clic
                toolTip1.SetToolTip(dataGridViewMenu, "Haga doble clic en un ítem para agregarlo a la promoción");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar items: {ex.Message}\n\nDetalles:\n{ex.StackTrace}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "0.00"; // Valor por defecto
            dateInicio.Value = DateTime.Today;
            dateFin.Value = DateTime.Today.AddDays(7);
            checkSinFin.Checked = false;
            checkActiva.Checked = true;
            itemsAgregados.Rows.Clear();
            promocionSeleccionadaId = null;
            ActualizarPanelItems();
            errorProvider.Clear();
        }

        private void ActualizarPanelItems()
        {
            try
            {
                // Verificar que panelItems existe
                if (panelItems == null)
                {
                    MessageBox.Show("Error: El panel 'panelItems' no está inicializado");
                    return;
                }

                // Limpiar controles existentes
                panelItems.Controls.Clear();

                // Verificar que itemsAgregados está inicializado
                if (itemsAgregados == null)
                {
                    itemsAgregados = new DataTable();
                    ConfigurarTablaItems();
                }

                // Mostrar mensaje si no hay items
                if (itemsAgregados.Rows.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = "No hay items agregados",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Gray,
                        Font = emptyListFont // Usamos la fuente declarada
                    };
                    panelItems.Controls.Add(lblEmpty);
                    return;
                }

                // Asegurar que las columnas existen
                if (!itemsAgregados.Columns.Contains("Tipo") ||
                    !itemsAgregados.Columns.Contains("Nombre") ||
                    !itemsAgregados.Columns.Contains("Cantidad"))
                {
                    MessageBox.Show("Error: La estructura de datos no tiene las columnas esperadas");
                    return;
                }

                int yPos = 5;
                foreach (DataRow row in itemsAgregados.Rows)
                {
                    // Verificar valores nulos
                    string tipo = row["Tipo"]?.ToString() ?? "DESCONOCIDO";
                    string nombre = row["Nombre"]?.ToString() ?? "Sin nombre";
                    int cantidad = 1;

                    try
                    {
                        cantidad = Convert.ToInt32(row["Cantidad"]);
                    }
                    catch
                    {
                        cantidad = 1; // Valor por defecto si hay error
                    }

                    // Crear panel para el item
                    Panel itemPanel = new Panel
                    {
                        BackColor = tipo == "PLATO" ? Color.LightBlue :
                                   tipo == "BEBIDA" ? Color.LightGreen :
                                   Color.LightGoldenrodYellow,
                        BorderStyle = BorderStyle.FixedSingle,
                        Width = panelItems.ClientSize.Width - 25,
                        Height = 40,
                        Location = new Point(5, yPos),
                        Tag = row,
                        Margin = new Padding(0, 0, 0, 5) // Margen inferior
                    };

                    // Etiqueta con nombre y cantidad
                    Label lblItem = new Label
                    {
                        Text = $"{nombre} (x{cantidad})",
                        Font = itemFont, // Usamos la fuente declarada
                        ForeColor = Color.Black,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Padding = new Padding(5, 0, 0, 0),
                        Cursor = Cursors.Hand
                    };

                    // Botón para eliminar
                    Button btnRemove = new Button
                    {
                        Text = "X",
                        Font = new Font("Arial", 9, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.Red,
                        FlatStyle = FlatStyle.Flat,
                        Width = 30,
                        Height = 30,
                        Dock = DockStyle.Right,
                        Margin = new Padding(0, 5, 5, 5),
                        Tag = row,
                        Cursor = Cursors.Hand
                    };
                    btnRemove.Click += (s, e) => {
                        if (MessageBox.Show("¿Eliminar este item?", "Confirmar",
                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            itemsAgregados.Rows.Remove(row);
                            ActualizarPanelItems();
                        }
                    };

                    // Agregar controles al panel
                    itemPanel.Controls.Add(lblItem);
                    itemPanel.Controls.Add(btnRemove);

                    // Agregar panel al contenedor principal
                    panelItems.Controls.Add(itemPanel);

                    // Incrementar posición vertical
                    yPos += itemPanel.Height + 5;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar panel: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosPromocion(int idPromocion)
        {
            try
            {
                DataTable promocion = PromocionesD.ObtenerPromocionPorId(idPromocion);
                if (promocion.Rows.Count > 0)
                {
                    DataRow row = promocion.Rows[0];
                    txtNombre.Text = row["nombre"].ToString();
                    txtDescripcion.Text = row["descripcion"]?.ToString() ?? "";
                    txtPrecio.Text = Convert.ToDecimal(row["precio_especial"]).ToString("0.00");
                    dateInicio.Value = Convert.ToDateTime(row["fecha_inicio"]);

                    if (row["fecha_fin"] != DBNull.Value)
                    {
                        dateFin.Value = Convert.ToDateTime(row["fecha_fin"]);
                        checkSinFin.Checked = false;
                    }
                    else
                    {
                        checkSinFin.Checked = true;
                        dateFin.Value = DateTime.Today.AddDays(7);
                    }

                    checkActiva.Checked = Convert.ToBoolean(row["activa"]);
                }

                // Cargar items
                itemsAgregados.Rows.Clear();
                DataTable items = PromocionesD.ObtenerItemsPromocion(idPromocion);
                foreach (DataRow item in items.Rows)
                {
                    itemsAgregados.Rows.Add(
                        item["tipo_item"].ToString(),
                        Convert.ToInt32(item["id_item"]),
                        item["nombre"].ToString(),
                        Convert.ToInt32(item["cantidad"]));
                }
                ActualizarPanelItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar promoción: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormGestionPromociones_Load(object sender, EventArgs e)
        {

        }

        private void btnPlatos_Click_1(object sender, EventArgs e)
        {
            CargarItems("PLATO");
        }

        private void btnBebidas_Click_1(object sender, EventArgs e)
        {
            CargarItems("BEBIDA");
        }

        private void btnExtras_Click(object sender, EventArgs e)
        {
            CargarItems("EXTRA");
        }

        
        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is DataRow row)
            {
                itemsAgregados.Rows.Remove(row);
                ActualizarPanelItems();
            }
        }

        private void dataGridViewPromociones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPromociones.Rows[e.RowIndex];
                promocionSeleccionadaId = Convert.ToInt32(row.Cells["id_promocion"].Value);
                CargarDatosPromocion(promocionSeleccionadaId.Value);
            }
        }

        private void dataGridViewItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridViewMenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Solo procesar si es un clic válido en una celda de datos (no encabezados)
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                DataGridViewRow row = dataGridViewMenu.Rows[e.RowIndex];
                string tipo = dataGridViewMenu.Tag?.ToString();

                if (string.IsNullOrEmpty(tipo)) return;

                // Verificar que las celdas necesarias tienen valores
                if (row.Cells["ID"].Value == null || row.Cells["nombre"].Value == null)
                {
                    MessageBox.Show("No se pudo obtener la información del item seleccionado.");
                    return;
                }

                int idItem = Convert.ToInt32(row.Cells["ID"].Value);
                string nombre = row.Cells["nombre"].Value.ToString();

                MostrarDialogoCantidadMejorado(tipo, idItem, nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar item: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método mejorado para mostrar el diálogo de cantidad
        private void MostrarDialogoCantidadMejorado(string tipo, int idItem, string nombre)
        {
            var existingItem = itemsAgregados.AsEnumerable()
                .FirstOrDefault(r => r.Field<int>("ID") == idItem && r.Field<string>("Tipo") == tipo);

            using (var dialog = new Form())
            {
                dialog.Text = $"Agregar a promoción: {nombre}";
                dialog.Size = new Size(300, 150);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;

                var lblCantidad = new Label
                {
                    Text = "Cantidad:",
                    Dock = DockStyle.Top,
                    Padding = new Padding(0, 10, 0, 0)
                };

                var numCantidad = new NumericUpDown
                {
                    Minimum = 1,
                    Maximum = 100,
                    Value = 1, // Siempre empezar con 1
                    Dock = DockStyle.Top,
                    DecimalPlaces = 0
                };

                var panelBotones = new Panel { Dock = DockStyle.Bottom, Height = 40 };
                var btnAceptar = new Button { Text = "Aceptar", DialogResult = DialogResult.OK, Width = 80 };
                var btnCancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Width = 80, Left = 90 };

                panelBotones.Controls.AddRange(new[] { btnAceptar, btnCancelar });
                dialog.Controls.AddRange(new Control[] { lblCantidad, numCantidad, panelBotones });

                dialog.AcceptButton = btnAceptar;
                dialog.CancelButton = btnCancelar;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (existingItem != null)
                    {
                        // Sumar automáticamente a la cantidad existente
                        existingItem["Cantidad"] = Convert.ToInt32(existingItem["Cantidad"]) + (int)numCantidad.Value;
                    }
                    else
                    {
                        itemsAgregados.Rows.Add(tipo, idItem, nombre, (int)numCantidad.Value);
                    }
                    ActualizarPanelItems();
                }
            }
        }



        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void numPrecio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateInicio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkSinFin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkActiva_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardarPromocion_Click_1(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Por favor corrija los errores en el formulario", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (itemsAgregados.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un item a la promoción", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool resultado = PromocionesD.GuardarPromocionCompleta(
                    promocionSeleccionadaId,
                    txtNombre.Text,
                    txtDescripcion.Text,
                    precio,
                    dateInicio.Value,
                    checkSinFin.Checked ? null : (DateTime?)dateFin.Value,
                    checkActiva.Checked,
                    itemsAgregados);

                if (resultado)
                {
                    MessageBox.Show("Promoción guardada exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarPromociones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrecio_Enter(object sender, EventArgs e)
        {
            // Cuando entra al TextBox, limpia el valor si es 0.00
            if (txtPrecio.Text == "0.00")
            {
                txtPrecio.Text = "";
            }
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            // Cuando sale del TextBox, formatea el valor
            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                txtPrecio.Text = "0.00";
            }
            else if (decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                // Asegura que el precio no sea negativo
                if (precio < 0) precio = 0;
                txtPrecio.Text = precio.ToString("0.00");
            }
            else
            {
                txtPrecio.Text = "0.00";
            }
        }
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números, punto decimal y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Solo permite un punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            // Validación en tiempo real
            if (!string.IsNullOrEmpty(txtPrecio.Text))
            {
                // Si el usuario escribe algo inválido, lo corrige
                if (!decimal.TryParse(txtPrecio.Text, out _) && txtPrecio.Text != ".")
                {
                    // Mantiene solo los caracteres válidos
                    string newText = new string(txtPrecio.Text.Where(c => char.IsDigit(c) || c == '.').ToArray());

                    // Elimina puntos decimales adicionales
                    if (newText.Count(c => c == '.') > 1)
                    {
                        int firstDot = newText.IndexOf('.');
                        newText = newText.Substring(0, firstDot + 1) +
                                 newText.Substring(firstDot + 1).Replace(".", "");
                    }

                    txtPrecio.Text = newText;
                    txtPrecio.SelectionStart = txtPrecio.Text.Length;
                }
            }
        }

        
    }
}