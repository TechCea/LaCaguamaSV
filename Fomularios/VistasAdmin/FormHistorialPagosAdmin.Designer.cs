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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewHistorial
            // 
            this.dataGridViewHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistorial.Location = new System.Drawing.Point(42, 123);
            this.dataGridViewHistorial.Name = "dataGridViewHistorial";
            this.dataGridViewHistorial.RowHeadersWidth = 51;
            this.dataGridViewHistorial.Size = new System.Drawing.Size(707, 291);
            this.dataGridViewHistorial.TabIndex = 0;
            this.dataGridViewHistorial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHistorial_CellContentClick);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(42, 420);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(103, 42);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(664, 107);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(71, 13);
            this.lblTotalRegistros.TabIndex = 27;
            this.lblTotalRegistros.Text = "Mostrar todos";
            this.lblTotalRegistros.Click += new System.EventHandler(this.lblTotalRegistros_Click);
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(1, 180);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(35, 13);
            this.lblA.TabIndex = 26;
            this.lblA.Text = "label1";
            this.lblA.Click += new System.EventHandler(this.lblA_Click);
            // 
            // btnResetear
            // 
            this.btnResetear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnResetear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnResetear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnResetear.Location = new System.Drawing.Point(535, 81);
            this.btnResetear.Name = "btnResetear";
            this.btnResetear.Size = new System.Drawing.Size(88, 36);
            this.btnResetear.TabIndex = 25;
            this.btnResetear.Text = "Resetear";
            this.btnResetear.UseVisualStyleBackColor = false;
            this.btnResetear.Click += new System.EventHandler(this.btnResetear_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnFiltrar.Location = new System.Drawing.Point(192, 84);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(85, 33);
            this.btnFiltrar.TabIndex = 24;
            this.btnFiltrar.Text = "Filtrar Datos";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Location = new System.Drawing.Point(423, 54);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFin.TabIndex = 23;
            this.dtpFechaFin.ValueChanged += new System.EventHandler(this.dtpFechaFin_ValueChanged);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(192, 54);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 22;
            this.dtpFechaInicio.ValueChanged += new System.EventHandler(this.dtpFechaInicio_ValueChanged);
            // 
            // cmbTipoFiltro
            // 
            this.cmbTipoFiltro.FormattingEnabled = true;
            this.cmbTipoFiltro.Location = new System.Drawing.Point(42, 84);
            this.cmbTipoFiltro.Name = "cmbTipoFiltro";
            this.cmbTipoFiltro.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoFiltro.TabIndex = 21;
            this.cmbTipoFiltro.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFiltro_SelectedIndexChanged);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Location = new System.Drawing.Point(1, 152);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(35, 13);
            this.lblFiltro.TabIndex = 20;
            this.lblFiltro.Text = "label1";
            this.lblFiltro.Visible = false;
            this.lblFiltro.Click += new System.EventHandler(this.lblFiltro_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Selecione para Filtrar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Fecha de Inicio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(543, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Fecha Final";
            // 
            // FormHistorialPagosAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(800, 473);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            this.Text = "Historial de ordenes";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}