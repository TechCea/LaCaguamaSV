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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCortes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalRegistrosCortes
            // 
            this.lblTotalRegistrosCortes.AutoSize = true;
            this.lblTotalRegistrosCortes.Location = new System.Drawing.Point(1081, 180);
            this.lblTotalRegistrosCortes.Name = "lblTotalRegistrosCortes";
            this.lblTotalRegistrosCortes.Size = new System.Drawing.Size(35, 13);
            this.lblTotalRegistrosCortes.TabIndex = 36;
            this.lblTotalRegistrosCortes.Text = "label1";
            // 
            // lblACortes
            // 
            this.lblACortes.AutoSize = true;
            this.lblACortes.Location = new System.Drawing.Point(177, 180);
            this.lblACortes.Name = "lblACortes";
            this.lblACortes.Size = new System.Drawing.Size(35, 13);
            this.lblACortes.TabIndex = 35;
            this.lblACortes.Text = "label1";
            this.lblACortes.Visible = false;
            // 
            // btnFiltrarCortes
            // 
            this.btnFiltrarCortes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnFiltrarCortes.Location = new System.Drawing.Point(916, 236);
            this.btnFiltrarCortes.Name = "btnFiltrarCortes";
            this.btnFiltrarCortes.Size = new System.Drawing.Size(114, 46);
            this.btnFiltrarCortes.TabIndex = 33;
            this.btnFiltrarCortes.Text = "Filtrar Datos";
            this.btnFiltrarCortes.UseVisualStyleBackColor = false;
            this.btnFiltrarCortes.Click += new System.EventHandler(this.btnFiltrarCortes_Click_1);
            // 
            // dtpFechaFinCortes
            // 
            this.dtpFechaFinCortes.Location = new System.Drawing.Point(956, 131);
            this.dtpFechaFinCortes.Name = "dtpFechaFinCortes";
            this.dtpFechaFinCortes.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinCortes.TabIndex = 32;
            this.dtpFechaFinCortes.ValueChanged += new System.EventHandler(this.dtpFechaFinCortes_ValueChanged);
            // 
            // dtpFechaInicioCortes
            // 
            this.dtpFechaInicioCortes.Location = new System.Drawing.Point(440, 131);
            this.dtpFechaInicioCortes.Name = "dtpFechaInicioCortes";
            this.dtpFechaInicioCortes.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicioCortes.TabIndex = 31;
            this.dtpFechaInicioCortes.ValueChanged += new System.EventHandler(this.dtpFechaInicioCortes_ValueChanged);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Location = new System.Drawing.Point(177, 103);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(35, 13);
            this.lblFiltro.TabIndex = 29;
            this.lblFiltro.Text = "label1";
            this.lblFiltro.Visible = false;
            // 
            // dataGridViewCortes
            // 
            this.dataGridViewCortes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCortes.Location = new System.Drawing.Point(180, 288);
            this.dataGridViewCortes.Name = "dataGridViewCortes";
            this.dataGridViewCortes.RowHeadersWidth = 51;
            this.dataGridViewCortes.Size = new System.Drawing.Size(976, 380);
            this.dataGridViewCortes.TabIndex = 28;
            this.dataGridViewCortes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCortes_CellContentClick_1);
            // 
            // btnCorteGeneral
            // 
            this.btnCorteGeneral.BackColor = System.Drawing.Color.Coral;
            this.btnCorteGeneral.Location = new System.Drawing.Point(180, 250);
            this.btnCorteGeneral.Name = "btnCorteGeneral";
            this.btnCorteGeneral.Size = new System.Drawing.Size(111, 32);
            this.btnCorteGeneral.TabIndex = 37;
            this.btnCorteGeneral.Text = "Corte General";
            this.btnCorteGeneral.UseVisualStyleBackColor = false;
            this.btnCorteGeneral.Click += new System.EventHandler(this.btnCorteGeneral_Click);
            // 
            // btnCorteTarjetas
            // 
            this.btnCorteTarjetas.BackColor = System.Drawing.Color.Coral;
            this.btnCorteTarjetas.Location = new System.Drawing.Point(320, 250);
            this.btnCorteTarjetas.Name = "btnCorteTarjetas";
            this.btnCorteTarjetas.Size = new System.Drawing.Size(105, 32);
            this.btnCorteTarjetas.TabIndex = 38;
            this.btnCorteTarjetas.Text = "Corte Tarjetas";
            this.btnCorteTarjetas.UseVisualStyleBackColor = false;
            this.btnCorteTarjetas.Click += new System.EventHandler(this.btnCorteTarjetas_Click);
            // 
            // btnCorteCaja
            // 
            this.btnCorteCaja.BackColor = System.Drawing.Color.Coral;
            this.btnCorteCaja.Location = new System.Drawing.Point(457, 250);
            this.btnCorteCaja.Name = "btnCorteCaja";
            this.btnCorteCaja.Size = new System.Drawing.Size(106, 32);
            this.btnCorteCaja.TabIndex = 39;
            this.btnCorteCaja.Text = "CorteCaja";
            this.btnCorteCaja.UseVisualStyleBackColor = false;
            this.btnCorteCaja.Click += new System.EventHandler(this.btnCorteCaja_Click);
            // 
            // cmbTipoFiltroCortes
            // 
            this.cmbTipoFiltroCortes.FormattingEnabled = true;
            this.cmbTipoFiltroCortes.Location = new System.Drawing.Point(171, 130);
            this.cmbTipoFiltroCortes.Name = "cmbTipoFiltroCortes";
            this.cmbTipoFiltroCortes.Size = new System.Drawing.Size(148, 21);
            this.cmbTipoFiltroCortes.TabIndex = 40;
            this.cmbTipoFiltroCortes.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFiltroCortes_SelectedIndexChanged_2);
            // 
            // btnResetearCortes
            // 
            this.btnResetearCortes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnResetearCortes.Location = new System.Drawing.Point(1054, 236);
            this.btnResetearCortes.Name = "btnResetearCortes";
            this.btnResetearCortes.Size = new System.Drawing.Size(102, 46);
            this.btnResetearCortes.TabIndex = 41;
            this.btnResetearCortes.Text = "Resetear";
            this.btnResetearCortes.UseVisualStyleBackColor = false;
            this.btnResetearCortes.Click += new System.EventHandler(this.btnResetearCortes_Click_2);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1094, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Fecha Final";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Fecha de Inicio";
            // 
            // FormHistorialCortes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
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
            this.Name = "FormHistorialCortes";
            this.Text = "Historial de cortes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormHistorialCortes_Load);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}