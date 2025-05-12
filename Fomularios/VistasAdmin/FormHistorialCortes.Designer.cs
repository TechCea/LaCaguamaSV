namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormHistorialCortes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTotalRegistrosCortes = new System.Windows.Forms.Label();
            this.lblACortes = new System.Windows.Forms.Label();
            this.btnFiltrarCortes = new System.Windows.Forms.Button();
            this.dtpFechaFinCortes = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicioCortes = new System.Windows.Forms.DateTimePicker();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.dataGridViewCortes = new System.Windows.Forms.DataGridView();
            this.btnCorteGeneral = new System.Windows.Forms.Button();
            this.btnCorteTarjetas = new System.Windows.Forms.Button();
            this.btnCorteCaja = new System.Windows.Forms.Button();
            this.cmbTipoFiltroCortes = new System.Windows.Forms.ComboBox();
            this.btnResetearCortes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCortes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalRegistrosCortes
            // 
            this.lblTotalRegistrosCortes.AutoSize = true;
            this.lblTotalRegistrosCortes.Location = new System.Drawing.Point(745, 53);
            this.lblTotalRegistrosCortes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalRegistrosCortes.Name = "lblTotalRegistrosCortes";
            this.lblTotalRegistrosCortes.Size = new System.Drawing.Size(44, 16);
            this.lblTotalRegistrosCortes.TabIndex = 36;
            this.lblTotalRegistrosCortes.Text = "label1";
            // 
            // lblACortes
            // 
            this.lblACortes.AutoSize = true;
            this.lblACortes.Location = new System.Drawing.Point(43, 59);
            this.lblACortes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblACortes.Name = "lblACortes";
            this.lblACortes.Size = new System.Drawing.Size(44, 16);
            this.lblACortes.TabIndex = 35;
            this.lblACortes.Text = "label1";
            // 
            // btnFiltrarCortes
            // 
            this.btnFiltrarCortes.Location = new System.Drawing.Point(467, 47);
            this.btnFiltrarCortes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFiltrarCortes.Name = "btnFiltrarCortes";
            this.btnFiltrarCortes.Size = new System.Drawing.Size(100, 28);
            this.btnFiltrarCortes.TabIndex = 33;
            this.btnFiltrarCortes.Text = "F";
            this.btnFiltrarCortes.UseVisualStyleBackColor = true;
            this.btnFiltrarCortes.Click += new System.EventHandler(this.btnFiltrarCortes_Click_1);
            // 
            // dtpFechaFinCortes
            // 
            this.dtpFechaFinCortes.Location = new System.Drawing.Point(579, 10);
            this.dtpFechaFinCortes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaFinCortes.Name = "dtpFechaFinCortes";
            this.dtpFechaFinCortes.Size = new System.Drawing.Size(265, 22);
            this.dtpFechaFinCortes.TabIndex = 32;
            this.dtpFechaFinCortes.ValueChanged += new System.EventHandler(this.dtpFechaFinCortes_ValueChanged);
            // 
            // dtpFechaInicioCortes
            // 
            this.dtpFechaInicioCortes.Location = new System.Drawing.Point(303, 10);
            this.dtpFechaInicioCortes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaInicioCortes.Name = "dtpFechaInicioCortes";
            this.dtpFechaInicioCortes.Size = new System.Drawing.Size(265, 22);
            this.dtpFechaInicioCortes.TabIndex = 31;
            this.dtpFechaInicioCortes.ValueChanged += new System.EventHandler(this.dtpFechaInicioCortes_ValueChanged);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Location = new System.Drawing.Point(43, 14);
            this.lblFiltro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(44, 16);
            this.lblFiltro.TabIndex = 29;
            this.lblFiltro.Text = "label1";
            this.lblFiltro.Visible = false;
            // 
            // dataGridViewCortes
            // 
            this.dataGridViewCortes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCortes.Location = new System.Drawing.Point(16, 181);
            this.dataGridViewCortes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewCortes.Name = "dataGridViewCortes";
            this.dataGridViewCortes.RowHeadersWidth = 51;
            this.dataGridViewCortes.Size = new System.Drawing.Size(1035, 358);
            this.dataGridViewCortes.TabIndex = 28;
            this.dataGridViewCortes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCortes_CellContentClick_1);
            // 
            // btnCorteGeneral
            // 
            this.btnCorteGeneral.Location = new System.Drawing.Point(47, 129);
            this.btnCorteGeneral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCorteGeneral.Name = "btnCorteGeneral";
            this.btnCorteGeneral.Size = new System.Drawing.Size(148, 39);
            this.btnCorteGeneral.TabIndex = 37;
            this.btnCorteGeneral.Text = "Corte General";
            this.btnCorteGeneral.UseVisualStyleBackColor = true;
            this.btnCorteGeneral.Click += new System.EventHandler(this.btnCorteGeneral_Click);
            // 
            // btnCorteTarjetas
            // 
            this.btnCorteTarjetas.Location = new System.Drawing.Point(504, 129);
            this.btnCorteTarjetas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCorteTarjetas.Name = "btnCorteTarjetas";
            this.btnCorteTarjetas.Size = new System.Drawing.Size(140, 39);
            this.btnCorteTarjetas.TabIndex = 38;
            this.btnCorteTarjetas.Text = "Corte Tarjetas";
            this.btnCorteTarjetas.UseVisualStyleBackColor = true;
            this.btnCorteTarjetas.Click += new System.EventHandler(this.btnCorteTarjetas_Click);
            // 
            // btnCorteCaja
            // 
            this.btnCorteCaja.Location = new System.Drawing.Point(891, 129);
            this.btnCorteCaja.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCorteCaja.Name = "btnCorteCaja";
            this.btnCorteCaja.Size = new System.Drawing.Size(141, 39);
            this.btnCorteCaja.TabIndex = 39;
            this.btnCorteCaja.Text = "CorteCaja";
            this.btnCorteCaja.UseVisualStyleBackColor = true;
            this.btnCorteCaja.Click += new System.EventHandler(this.btnCorteCaja_Click);
            // 
            // cmbTipoFiltroCortes
            // 
            this.cmbTipoFiltroCortes.FormattingEnabled = true;
            this.cmbTipoFiltroCortes.Location = new System.Drawing.Point(97, 10);
            this.cmbTipoFiltroCortes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTipoFiltroCortes.Name = "cmbTipoFiltroCortes";
            this.cmbTipoFiltroCortes.Size = new System.Drawing.Size(196, 24);
            this.cmbTipoFiltroCortes.TabIndex = 40;
            this.cmbTipoFiltroCortes.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFiltroCortes_SelectedIndexChanged_2);
            // 
            // btnResetearCortes
            // 
            this.btnResetearCortes.Location = new System.Drawing.Point(575, 47);
            this.btnResetearCortes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResetearCortes.Name = "btnResetearCortes";
            this.btnResetearCortes.Size = new System.Drawing.Size(100, 28);
            this.btnResetearCortes.TabIndex = 41;
            this.btnResetearCortes.Text = "Resetear";
            this.btnResetearCortes.UseVisualStyleBackColor = true;
            this.btnResetearCortes.Click += new System.EventHandler(this.btnResetearCortes_Click_2);
            // 
            // FormHistorialCortes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnResetearCortes);
            this.Controls.Add(this.cmbTipoFiltroCortes);
            this.Controls.Add(this.btnCorteCaja);
            this.Controls.Add(this.btnCorteTarjetas);
            this.Controls.Add(this.btnCorteGeneral);
            this.Controls.Add(this.lblTotalRegistrosCortes);
            this.Controls.Add(this.lblACortes);
            this.Controls.Add(this.btnFiltrarCortes);
            this.Controls.Add(this.dtpFechaFinCortes);
            this.Controls.Add(this.dtpFechaInicioCortes);
            this.Controls.Add(this.lblFiltro);
            this.Controls.Add(this.dataGridViewCortes);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormHistorialCortes";
            this.Text = "Historial de cortes";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCortes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalRegistrosCortes;
        private System.Windows.Forms.Label lblACortes;
        private System.Windows.Forms.Button btnFiltrarCortes;
        private System.Windows.Forms.DateTimePicker dtpFechaFinCortes;
        private System.Windows.Forms.DateTimePicker dtpFechaInicioCortes;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.DataGridView dataGridViewCortes;
        private System.Windows.Forms.Button btnCorteGeneral;
        private System.Windows.Forms.Button btnCorteTarjetas;
        private System.Windows.Forms.Button btnCorteCaja;
        private System.Windows.Forms.ComboBox cmbTipoFiltroCortes;
        private System.Windows.Forms.Button btnResetearCortes;
    }
}