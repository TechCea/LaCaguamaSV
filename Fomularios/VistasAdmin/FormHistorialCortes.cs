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
    public partial class FormHistorialCortes : Form
    {
        private string vistaActual = "General"; // Puede ser "General", "Tarjetas" o "Caja"

        public FormHistorialCortes()
        {
            InitializeComponent();

            // Tamaño fijo
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Evita redimensionar

            // Posición fija (centrada en la pantalla)
            this.StartPosition = FormStartPosition.CenterScreen;

            // Verificar permisos de administrador
            if (SesionUsuario.Rol != 1)
            {
                MessageBox.Show("Acceso denegado. No tienes permisos de administrador.", "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // Configuración inicial
            cmbTipoFiltroCortes.Items.AddRange(new[] { "Todos", "Hoy", "Esta semana", "Este mes", "Fecha específica", "Rango de fechas" });
            cmbTipoFiltroCortes.SelectedIndex = 0;
            dtpFechaInicioCortes.Value = DateTime.Today;
            dtpFechaFinCortes.Value = DateTime.Today;
            dtpFechaInicioCortes.Visible = false;
            dtpFechaFinCortes.Visible = false;
            lblACortes.Visible = false;

            vistaActual = "General";
            btnCorteGeneral.BackColor = Color.LightGray;
            CargarVistaGeneral();

            ConfigurarDataGridViewCortes();
            dataGridViewCortes.MultiSelect = false;

            // Asociar eventos correctamente
            cmbTipoFiltroCortes.SelectedIndexChanged += CmbTipoFiltroCortes_SelectedIndexChanged;
            btnFiltrarCortes.Click += BtnFiltrarCortes_Click;
            btnResetearCortes.Click += BtnResetearCortes_Click;
            btnCorteGeneral.Click += btnCorteGeneral_Click;
            btnCorteTarjetas.Click += btnCorteTarjetas_Click;
            btnCorteCaja.Click += btnCorteCaja_Click;
            dataGridViewCortes.CellContentClick += DataGridViewCortes_CellContentClick;
        }

        private void CmbTipoFiltroCortes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTipoFiltroCortes.SelectedItem?.ToString())
            {
                case "Fecha específica":
                    dtpFechaInicioCortes.Visible = true;
                    dtpFechaFinCortes.Visible = false;
                    lblACortes.Visible = false;
                    break;

                case "Rango de fechas":
                    dtpFechaInicioCortes.Visible = true;
                    dtpFechaFinCortes.Visible = true;
                    lblACortes.Visible = true;
                    break;

                default:
                    dtpFechaInicioCortes.Visible = false;
                    dtpFechaFinCortes.Visible = false;
                    lblACortes.Visible = false;
                    break;
            }
        }

        private void CargarVistaActual(string tipoFiltro = null, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            switch (vistaActual)
            {
                case "General":
                    CargarVistaGeneral(tipoFiltro, fechaInicio, fechaFin);
                    break;
                case "Tarjetas":
                    CargarVistaTarjetas(tipoFiltro, fechaInicio, fechaFin);
                    break;
                case "Caja":
                    CargarVistaCaja(tipoFiltro, fechaInicio, fechaFin);
                    break;
            }
        }

        private string AplicarFiltros(string query, string tipoFiltro, DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("La consulta no puede estar vacía", nameof(query));
            }
            // Determinar el nombre de la tabla según la vista actual
            string nombreTabla;
            switch (vistaActual)
            {
                case "General":
                    nombreTabla = "cg";
                    break;
                case "Tarjetas":
                    nombreTabla = "ct";
                    break;
                case "Caja":
                    nombreTabla = "cc";
                    break;
                default:
                    nombreTabla = "cg"; // Por defecto
                    break;
            }

            // Construir la parte WHERE de la consulta
            string whereClause;
            switch (tipoFiltro)
            {
                case "Hoy":
                    whereClause = $" WHERE DATE({nombreTabla}.fecha) = CURDATE()";
                    break;
                case "Esta semana":
                    whereClause = $" WHERE YEARWEEK({nombreTabla}.fecha, 1) = YEARWEEK(CURDATE(), 1)";
                    break;
                case "Este mes":
                    whereClause = $" WHERE MONTH({nombreTabla}.fecha) = MONTH(CURDATE()) AND YEAR({nombreTabla}.fecha) = YEAR(CURDATE())";
                    break;
                case "Fecha específica":
                    whereClause = fechaInicio.HasValue ? $" WHERE DATE({nombreTabla}.fecha) = '{fechaInicio.Value:yyyy-MM-dd}'" : "";
                    break;
                case "Rango de fechas":
                    whereClause = (fechaInicio.HasValue && fechaFin.HasValue) ?
                                 $" WHERE DATE({nombreTabla}.fecha) BETWEEN '{fechaInicio.Value:yyyy-MM-dd}' AND '{fechaFin.Value:yyyy-MM-dd}'" : "";
                    break;
                default: // Para "Todos" no agregamos filtro
                    whereClause = "";
                    break;
            }

            // Manejar casos donde ya existe un WHERE en la consulta (para subconsultas)
            if (query.Contains(" WHERE ") && !string.IsNullOrEmpty(whereClause))
            {
                // Reemplazar el WHERE por AND si ya existe una condición
                whereClause = whereClause.Replace(" WHERE ", " AND ");
            }

            // Retornar la consulta final con los filtros aplicados
            return query + whereClause;
        }

        // Métodos de carga modificados para asegurar consistencia en los nombres de campos
        private void CargarVistaGeneral(string tipoFiltro = null, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
            SELECT 
                cg.id_corte_general AS 'ID Corte',
                DATE_FORMAT(cg.fecha, '%Y-%m-%d %H:%i') AS 'Fecha/Hora',
                u.nombre AS 'Responsable',
                cg.cant_efectivo AS 'Ventas Efectivo',
                cg.cant_tarjeta AS 'Ventas Tarjeta',
                cg.descuento AS 'Descuentos',
                cg.total_gastos_general AS 'Total Gastos',
                cg.total_final_dia AS 'Total Final'
            FROM corte_general cg
            JOIN usuarios u ON cg.id_usuario = u.id_usuario
            JOIN caja c ON cg.id_caja = c.id_caja";

                    query = AplicarFiltros(query, tipoFiltro, fechaInicio, fechaFin);
                    query += " ORDER BY cg.fecha DESC";

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conexion);
                    da.Fill(dt);

                    dataGridViewCortes.DataSource = dt;
                    lblTotalRegistrosCortes.Text = $"Total registros: {dt.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial de cortes generales: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVistaTarjetas(string tipoFiltro = null, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
            SELECT 
                ct.id_corte_tarjetas AS 'ID Corte',
                DATE_FORMAT(ct.fecha, '%Y-%m-%d %H:%i') AS 'Fecha/Hora',
                u.nombre AS 'Responsable',
                ct.cantidad AS 'Monto Tarjetas',
                (SELECT IFNULL(SUM(p.descuento), 0) 
                 FROM pagos p 
                 WHERE p.id_tipo_pago = 2 
                 AND DATE(p.fecha_pago) = DATE(ct.fecha)) AS 'Descuentos Tarjetas',
                (ct.cantidad - (SELECT IFNULL(SUM(p.descuento), 0)
                              FROM pagos p 
                              WHERE p.id_tipo_pago = 2 
                              AND DATE(p.fecha_pago) = DATE(ct.fecha))) AS 'Neto Tarjetas'
            FROM corte_tarjetas ct
            JOIN usuarios u ON ct.id_usuario = u.id_usuario
            JOIN caja c ON ct.id_caja = c.id_caja";

                    query = AplicarFiltros(query, tipoFiltro, fechaInicio, fechaFin);
                    query += " ORDER BY ct.fecha DESC";

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conexion);
                    da.Fill(dt);

                    dataGridViewCortes.DataSource = dt;
                    lblTotalRegistrosCortes.Text = $"Total registros: {dt.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial de cortes de tarjetas: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVistaCaja(string tipoFiltro = null, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    string query = @"
            SELECT 
                cc.id_corte AS 'ID Corte',
                DATE_FORMAT(cc.fecha, '%Y-%m-%d %H:%i') AS 'Fecha/Hora',
                u.nombre AS 'Responsable',
                c.cantidad AS 'Caja Inicial',
                cc.cantidad AS 'Caja Final',
                (cc.cantidad - c.cantidad) AS 'Diferencia',
                (SELECT IFNULL(SUM(g.cantidad), 0) 
                 FROM gastos g 
                 WHERE g.id_caja = cc.id_caja) AS 'Total Gastos'
            FROM corte_de_caja cc
            JOIN usuarios u ON cc.id_usuario = u.id_usuario
            JOIN caja c ON cc.id_caja = c.id_caja";

                    query = AplicarFiltros(query, tipoFiltro, fechaInicio, fechaFin);
                    query += " ORDER BY cc.fecha DESC";

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conexion);
                    da.Fill(dt);

                    dataGridViewCortes.DataSource = dt;
                    lblTotalRegistrosCortes.Text = $"Total registros: {dt.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial de cortes de caja: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridViewCortes()
        {
            dataGridViewCortes.ReadOnly = true;
            dataGridViewCortes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCortes.MultiSelect = false;
            dataGridViewCortes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Formato de columnas monetarias
            string[] columnasMonetarias = {
                "Ventas Efectivo", "Ventas Tarjeta", "Descuentos", "Total Gastos", "Total Final", // General
                "Monto Tarjetas", "Descuentos Tarjetas", "Neto Tarjetas", // Tarjetas
                "Caja Inicial", "Caja Final", "Diferencia", "Total Gastos" // Caja
            };

            foreach (DataGridViewColumn columna in dataGridViewCortes.Columns)
            {
                if (columnasMonetarias.Contains(columna.HeaderText))
                {
                    columna.DefaultCellStyle.Format = "C";
                    columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        private void MostrarDetalleCorte(int idCorte)
        {
            try
            {
                using (MySqlConnection conexion = new Conexion().EstablecerConexion())
                {
                    StringBuilder detalle = new StringBuilder();
                    detalle.AppendLine("══════════════════════════════════");
                    detalle.AppendLine("        LA CAGUAMA RESTAURANTE    ");
                    detalle.AppendLine("══════════════════════════════════");

                    if (vistaActual == "General")
                    {
                        // Detalle para corte general
                        string query = @"
                        SELECT 
                            cg.id_corte_general,
                            DATE_FORMAT(cg.fecha, '%Y-%m-%d %H:%i') AS fecha,
                            u.nombre AS responsable,
                            cg.cant_efectivo,
                            cg.cant_tarjeta,
                            cg.descuento,
                            cg.total_gastos_general,
                            cg.total_final_dia
                        FROM corte_general cg
                        JOIN usuarios u ON cg.id_usuario = u.id_usuario
                        WHERE cg.id_corte_general = @idCorte";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@idCorte", idCorte);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    detalle.AppendLine($"Corte General: #{reader["id_corte_general"]}");
                                    detalle.AppendLine($"Fecha: {reader["fecha"]}");
                                    detalle.AppendLine($"Responsable: {reader["responsable"]}");
                                    detalle.AppendLine("──────────────────────────────────");
                                    detalle.AppendLine($"Ventas en efectivo: {Convert.ToDecimal(reader["cant_efectivo"]).ToString("C")}");
                                    detalle.AppendLine($"Ventas con tarjeta: {Convert.ToDecimal(reader["cant_tarjeta"]).ToString("C")}");
                                    detalle.AppendLine($"Descuentos aplicados: {Convert.ToDecimal(reader["descuento"]).ToString("C")}");
                                    detalle.AppendLine($"Total gastos: {Convert.ToDecimal(reader["total_gastos_general"]).ToString("C")}");
                                    detalle.AppendLine($"Total final: {Convert.ToDecimal(reader["total_final_dia"]).ToString("C")}");
                                }
                            }
                        }
                    }
                    else if (vistaActual == "Tarjetas")
                    {
                        // Detalle para corte de tarjetas
                        string query = @"
                        SELECT 
                            ct.id_corte_tarjetas,
                            DATE_FORMAT(ct.fecha, '%Y-%m-%d %H:%i') AS fecha,
                            u.nombre AS responsable,
                            ct.cantidad AS monto_tarjetas,
                            (SELECT SUM(p.descuento) 
                             FROM pagos p 
                             WHERE p.id_tipo_pago = 2 
                             AND DATE(p.fecha_pago) = DATE(ct.fecha)) AS descuentos
                        FROM corte_tarjetas ct
                        JOIN usuarios u ON ct.id_usuario = u.id_usuario
                        WHERE ct.id_corte_tarjetas = @idCorte";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@idCorte", idCorte);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    decimal monto = Convert.ToDecimal(reader["monto_tarjetas"]);
                                    decimal descuentos = reader["descuentos"] != DBNull.Value ? Convert.ToDecimal(reader["descuentos"]) : 0;

                                    detalle.AppendLine($"Corte de Tarjetas: #{reader["id_corte_tarjetas"]}");
                                    detalle.AppendLine($"Fecha: {reader["fecha"]}");
                                    detalle.AppendLine($"Responsable: {reader["responsable"]}");
                                    detalle.AppendLine("──────────────────────────────────");
                                    detalle.AppendLine($"Monto total tarjetas: {monto.ToString("C")}");
                                    detalle.AppendLine($"Descuentos aplicados: {descuentos.ToString("C")}");
                                    detalle.AppendLine($"Neto recibido: {(monto - descuentos).ToString("C")}");
                                }
                            }
                        }
                    }
                    else if (vistaActual == "Caja")
                    {
                        // Detalle para corte de caja
                        string query = @"
                        SELECT 
                            cc.id_corte,
                            DATE_FORMAT(cc.fecha, '%Y-%m-%d %H:%i') AS fecha,
                            u.nombre AS responsable,
                            c.cantidad AS caja_inicial,
                            cc.cantidad AS caja_final,
                            (SELECT SUM(g.cantidad) 
                             FROM gastos g 
                             WHERE g.id_caja = cc.id_caja) AS total_gastos,
                            ec.nombreEstadoCorte AS estado
                        FROM corte_de_caja cc
                        JOIN usuarios u ON cc.id_usuario = u.id_usuario
                        JOIN caja c ON cc.id_caja = c.id_caja
                        JOIN estado_corte ec ON cc.id_estado_corte = ec.id_estado_corte
                        WHERE cc.id_corte = @idCorte";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@idCorte", idCorte);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    decimal inicial = Convert.ToDecimal(reader["caja_inicial"]);
                                    decimal final = Convert.ToDecimal(reader["caja_final"]);
                                    decimal gastos = reader["total_gastos"] != DBNull.Value ? Convert.ToDecimal(reader["total_gastos"]) : 0;

                                    detalle.AppendLine($"Corte de Caja: #{reader["id_corte"]}");
                                    detalle.AppendLine($"Fecha: {reader["fecha"]}");
                                    detalle.AppendLine($"Responsable: {reader["responsable"]}");
                                    detalle.AppendLine($"Estado: {reader["estado"]}");
                                    detalle.AppendLine("──────────────────────────────────");
                                    detalle.AppendLine($"Caja inicial: {inicial.ToString("C")}");
                                    detalle.AppendLine($"Caja final: {final.ToString("C")}");
                                    detalle.AppendLine($"Total gastos: {gastos.ToString("C")}");
                                    detalle.AppendLine($"Diferencia: {(final - inicial).ToString("C")}");
                                }
                            }
                        }
                    }

                    detalle.AppendLine("══════════════════════════════════");
                    detalle.AppendLine("   ¡Gracias por su preferencia!   ");
                    detalle.AppendLine("══════════════════════════════════");

                    // Mostrar en MessageBox con scroll
                    using (var scrollableMessageBox = new Form()
                    {
                        Width = 600,
                        Height = 500,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = $"Detalle Corte #{idCorte}",
                        StartPosition = FormStartPosition.CenterParent,
                        MaximizeBox = false,
                        MinimizeBox = false
                    })
                    {
                        var textBox = new TextBox()
                        {
                            Multiline = true,
                            Dock = DockStyle.Fill,
                            ReadOnly = true,
                            ScrollBars = ScrollBars.Vertical,
                            Text = detalle.ToString(),
                            Font = new Font("Consolas", 10)
                        };

                        scrollableMessageBox.Controls.Add(textBox);
                        scrollableMessageBox.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar detalle de corte: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnFiltrarCortes_Click(object sender, EventArgs e)
        {
            string tipoFiltro = cmbTipoFiltroCortes.SelectedItem.ToString();
            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;

            if (tipoFiltro == "Fecha específica" || tipoFiltro == "Rango de fechas")
            {
                fechaInicio = dtpFechaInicioCortes.Value.Date;

                if (tipoFiltro == "Rango de fechas")
                {
                    fechaFin = dtpFechaFinCortes.Value.Date;

                    if (fechaFin < fechaInicio)
                    {
                        MessageBox.Show("La fecha final no puede ser anterior a la fecha inicial",
                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            CargarVistaActual(tipoFiltro, fechaInicio, fechaFin);
        }

        private void BtnResetearCortes_Click(object sender, EventArgs e)
        {
            cmbTipoFiltroCortes.SelectedIndex = 0;
            CargarVistaActual();
        }

        private void DataGridViewCortes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idCorte = Convert.ToInt32(dataGridViewCortes.Rows[e.RowIndex].Cells[0].Value);
                MostrarDetalleCorte(idCorte);
            }
        }

        private void btnCerrarCortes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpFechaInicioCortes_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpFechaFinCortes_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewCortes_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCorteGeneral_Click(object sender, EventArgs e)
        {
            vistaActual = "General";
            btnCorteGeneral.BackColor = Color.LightGray;
            btnCorteTarjetas.BackColor = SystemColors.Control;
            btnCorteCaja.BackColor = SystemColors.Control;
            CargarVistaGeneral();
        }

        private void btnCorteTarjetas_Click(object sender, EventArgs e)
        {
            vistaActual = "Tarjetas";
            btnCorteGeneral.BackColor = SystemColors.Control;
            btnCorteTarjetas.BackColor = Color.LightGray;
            btnCorteCaja.BackColor = SystemColors.Control;
            CargarVistaTarjetas();
        }

        private void btnCorteCaja_Click(object sender, EventArgs e)
        {
            vistaActual = "Caja";
            btnCorteGeneral.BackColor = SystemColors.Control;
            btnCorteTarjetas.BackColor = SystemColors.Control;
            btnCorteCaja.BackColor = Color.LightGray;
            CargarVistaCaja();
        }

        private void btnFiltrarCortes_Click_1(object sender, EventArgs e)
        {
            string tipoFiltro = cmbTipoFiltroCortes.SelectedItem?.ToString() ?? "Todos";
            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;

            try
            {
                if (tipoFiltro == "Fecha específica" || tipoFiltro == "Rango de fechas")
                {
                    fechaInicio = dtpFechaInicioCortes.Value.Date;

                    if (tipoFiltro == "Rango de fechas")
                    {
                        fechaFin = dtpFechaFinCortes.Value.Date;

                        if (fechaFin < fechaInicio)
                        {
                            MessageBox.Show("La fecha final no puede ser anterior a la fecha inicial",
                                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if ((fechaFin.Value - fechaInicio.Value).TotalDays > 31)
                        {
                            MessageBox.Show("El rango de fechas no puede ser mayor a 31 días",
                                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                CargarVistaActual(tipoFiltro, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtros: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipoFiltroCortes_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void btnResetearCortes_Click_2(object sender, EventArgs e)
        {
            cmbTipoFiltroCortes.SelectedIndex = 0;
            CargarVistaActual();
        }

        private void FormHistorialCortes_Load(object sender, EventArgs e)
        {

        }
    }
}