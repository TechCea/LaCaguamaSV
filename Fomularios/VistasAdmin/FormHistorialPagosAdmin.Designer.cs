namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormHistorialPagosAdmin
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
            this.dataGridViewHistorial = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.lblA = new System.Windows.Forms.Label();
            this.btnResetear = new System.Windows.Forms.Button();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.cmbTipoFiltro = new System.Windows.Forms.ComboBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewHistorial
            // 
            this.dataGridViewHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistorial.Location = new System.Drawing.Point(12, 67);
            this.dataGridViewHistorial.Name = "dataGridViewHistorial";
            this.dataGridViewHistorial.Size = new System.Drawing.Size(707, 291);
            this.dataGridViewHistorial.TabIndex = 0;
            this.dataGridViewHistorial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHistorial_CellContentClick);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(12, 364);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(540, 49);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(35, 13);
            this.lblTotalRegistros.TabIndex = 27;
            this.lblTotalRegistros.Text = "label1";
            this.lblTotalRegistros.Click += new System.EventHandler(this.lblTotalRegistros_Click);
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(82, 49);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(35, 13);
            this.lblA.TabIndex = 26;
            this.lblA.Text = "label1";
            this.lblA.Click += new System.EventHandler(this.lblA_Click);
            // 
            // btnResetear
            // 
            this.btnResetear.Location = new System.Drawing.Point(416, 39);
            this.btnResetear.Name = "btnResetear";
            this.btnResetear.Size = new System.Drawing.Size(75, 23);
            this.btnResetear.TabIndex = 25;
            this.btnResetear.Text = "Resetear";
            this.btnResetear.UseVisualStyleBackColor = true;
            this.btnResetear.Click += new System.EventHandler(this.btnResetear_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(333, 39);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 24;
            this.btnFiltrar.Text = "F";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Location = new System.Drawing.Point(416, 9);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFin.TabIndex = 23;
            this.dtpFechaFin.ValueChanged += new System.EventHandler(this.dtpFechaFin_ValueChanged);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(209, 9);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 22;
            this.dtpFechaInicio.ValueChanged += new System.EventHandler(this.dtpFechaInicio_ValueChanged);
            // 
            // cmbTipoFiltro
            // 
            this.cmbTipoFiltro.FormattingEnabled = true;
            this.cmbTipoFiltro.Location = new System.Drawing.Point(82, 12);
            this.cmbTipoFiltro.Name = "cmbTipoFiltro";
            this.cmbTipoFiltro.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoFiltro.TabIndex = 21;
            this.cmbTipoFiltro.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFiltro_SelectedIndexChanged);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Location = new System.Drawing.Point(14, 12);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(35, 13);
            this.lblFiltro.TabIndex = 20;
            this.lblFiltro.Text = "label1";
            this.lblFiltro.Click += new System.EventHandler(this.lblFiltro_Click);
            // 
            // FormHistorialPagosAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTotalRegistros);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.btnResetear);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.cmbTipoFiltro);
            this.Controls.Add(this.lblFiltro);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dataGridViewHistorial);
            this.Name = "FormHistorialPagosAdmin";
            this.Text = "FormHistorialPagosAdmin";
            this.Load += new System.EventHandler(this.FormHistorialPagosAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewHistorial;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Button btnResetear;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.ComboBox cmbTipoFiltro;
        private System.Windows.Forms.Label lblFiltro;
    }
}