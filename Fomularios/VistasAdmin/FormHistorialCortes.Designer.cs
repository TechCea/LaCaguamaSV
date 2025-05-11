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
            this.lblTotalRegistrosCortes.Location = new System.Drawing.Point(559, 43);
            this.lblTotalRegistrosCortes.Name = "lblTotalRegistrosCortes";
            this.lblTotalRegistrosCortes.Size = new System.Drawing.Size(35, 13);
            this.lblTotalRegistrosCortes.TabIndex = 36;
            this.lblTotalRegistrosCortes.Text = "label1";
            // 
            // lblACortes
            // 
            this.lblACortes.AutoSize = true;
            this.lblACortes.Location = new System.Drawing.Point(99, 48);
            this.lblACortes.Name = "lblACortes";
            this.lblACortes.Size = new System.Drawing.Size(35, 13);
            this.lblACortes.TabIndex = 35;
            this.lblACortes.Text = "label1";
            // 
            // btnFiltrarCortes
            // 
            this.btnFiltrarCortes.Location = new System.Drawing.Point(350, 38);
            this.btnFiltrarCortes.Name = "btnFiltrarCortes";
            this.btnFiltrarCortes.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrarCortes.TabIndex = 33;
            this.btnFiltrarCortes.Text = "F";
            this.btnFiltrarCortes.UseVisualStyleBackColor = true;
            this.btnFiltrarCortes.Click += new System.EventHandler(this.btnFiltrarCortes_Click_1);
            // 
            // dtpFechaFinCortes
            // 
            this.dtpFechaFinCortes.Location = new System.Drawing.Point(434, 8);
            this.dtpFechaFinCortes.Name = "dtpFechaFinCortes";
            this.dtpFechaFinCortes.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinCortes.TabIndex = 32;
            this.dtpFechaFinCortes.ValueChanged += new System.EventHandler(this.dtpFechaFinCortes_ValueChanged);
            // 
            // dtpFechaInicioCortes
            // 
            this.dtpFechaInicioCortes.Location = new System.Drawing.Point(227, 8);
            this.dtpFechaInicioCortes.Name = "dtpFechaInicioCortes";
            this.dtpFechaInicioCortes.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicioCortes.TabIndex = 31;
            this.dtpFechaInicioCortes.ValueChanged += new System.EventHandler(this.dtpFechaInicioCortes_ValueChanged);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Location = new System.Drawing.Point(32, 11);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(35, 13);
            this.lblFiltro.TabIndex = 29;
            this.lblFiltro.Text = "label1";
            // 
            // dataGridViewCortes
            // 
            this.dataGridViewCortes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCortes.Location = new System.Drawing.Point(12, 147);
            this.dataGridViewCortes.Name = "dataGridViewCortes";
            this.dataGridViewCortes.RowHeadersWidth = 51;
            this.dataGridViewCortes.Size = new System.Drawing.Size(776, 291);
            this.dataGridViewCortes.TabIndex = 28;
            this.dataGridViewCortes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCortes_CellContentClick_1);
            // 
            // btnCorteGeneral
            // 
            this.btnCorteGeneral.Location = new System.Drawing.Point(35, 105);
            this.btnCorteGeneral.Name = "btnCorteGeneral";
            this.btnCorteGeneral.Size = new System.Drawing.Size(111, 32);
            this.btnCorteGeneral.TabIndex = 37;
            this.btnCorteGeneral.Text = "Corte General";
            this.btnCorteGeneral.UseVisualStyleBackColor = true;
            this.btnCorteGeneral.Click += new System.EventHandler(this.btnCorteGeneral_Click);
            // 
            // btnCorteTarjetas
            // 
            this.btnCorteTarjetas.Location = new System.Drawing.Point(378, 105);
            this.btnCorteTarjetas.Name = "btnCorteTarjetas";
            this.btnCorteTarjetas.Size = new System.Drawing.Size(105, 32);
            this.btnCorteTarjetas.TabIndex = 38;
            this.btnCorteTarjetas.Text = "Corte Tarjetas";
            this.btnCorteTarjetas.UseVisualStyleBackColor = true;
            this.btnCorteTarjetas.Click += new System.EventHandler(this.btnCorteTarjetas_Click);
            // 
            // btnCorteCaja
            // 
            this.btnCorteCaja.Location = new System.Drawing.Point(668, 105);
            this.btnCorteCaja.Name = "btnCorteCaja";
            this.btnCorteCaja.Size = new System.Drawing.Size(106, 32);
            this.btnCorteCaja.TabIndex = 39;
            this.btnCorteCaja.Text = "CorteCaja";
            this.btnCorteCaja.UseVisualStyleBackColor = true;
            this.btnCorteCaja.Click += new System.EventHandler(this.btnCorteCaja_Click);
            // 
            // cmbTipoFiltroCortes
            // 
            this.cmbTipoFiltroCortes.FormattingEnabled = true;
            this.cmbTipoFiltroCortes.Location = new System.Drawing.Point(73, 8);
            this.cmbTipoFiltroCortes.Name = "cmbTipoFiltroCortes";
            this.cmbTipoFiltroCortes.Size = new System.Drawing.Size(148, 21);
            this.cmbTipoFiltroCortes.TabIndex = 40;
            this.cmbTipoFiltroCortes.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFiltroCortes_SelectedIndexChanged_2);
            // 
            // btnResetearCortes
            // 
            this.btnResetearCortes.Location = new System.Drawing.Point(431, 38);
            this.btnResetearCortes.Name = "btnResetearCortes";
            this.btnResetearCortes.Size = new System.Drawing.Size(75, 23);
            this.btnResetearCortes.TabIndex = 41;
            this.btnResetearCortes.Text = "Resetear";
            this.btnResetearCortes.UseVisualStyleBackColor = true;
            this.btnResetearCortes.Click += new System.EventHandler(this.btnResetearCortes_Click_2);
            // 
            // FormHistorialCortes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Text = "FormHistorialCortes";
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