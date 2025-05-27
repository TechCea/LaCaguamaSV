namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormAdminGastos
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
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.dgvGastos = new System.Windows.Forms.DataGridView();
            this.btnAgregarGasto = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUtilidad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotalGastos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEfectivoRecolectado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFondoInicial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaInicioGasto = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.dtpFechaFinGasto = new System.Windows.Forms.DateTimePicker();
            this.cmbTipoFiltroGasto = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnResetearFiltro = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCantidad
            // 
            this.txtCantidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtCantidad.Location = new System.Drawing.Point(162, 287);
            this.txtCantidad.Multiline = true;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(76, 24);
            this.txtCantidad.TabIndex = 0;
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDescripcion.Location = new System.Drawing.Point(16, 353);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(245, 76);
            this.txtDescripcion.TabIndex = 1;
            this.txtDescripcion.TextChanged += new System.EventHandler(this.txtDescripcion_TextChanged);
            // 
            // dgvGastos
            // 
            this.dgvGastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGastos.Location = new System.Drawing.Point(605, 195);
            this.dgvGastos.Name = "dgvGastos";
            this.dgvGastos.RowHeadersWidth = 51;
            this.dgvGastos.Size = new System.Drawing.Size(583, 372);
            this.dgvGastos.TabIndex = 4;
            this.dgvGastos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGastos_CellContentClick);
            // 
            // btnAgregarGasto
            // 
            this.btnAgregarGasto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAgregarGasto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregarGasto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarGasto.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAgregarGasto.Image = global::LaCaguamaSV.Properties.Resources.confirmar2;
            this.btnAgregarGasto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarGasto.Location = new System.Drawing.Point(195, 560);
            this.btnAgregarGasto.Name = "btnAgregarGasto";
            this.btnAgregarGasto.Size = new System.Drawing.Size(128, 47);
            this.btnAgregarGasto.TabIndex = 5;
            this.btnAgregarGasto.Text = "Agregar";
            this.btnAgregarGasto.UseVisualStyleBackColor = false;
            this.btnAgregarGasto.Click += new System.EventHandler(this.btnAgregarGasto_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GrayText;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtUtilidad);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtDescripcion);
            this.panel2.Controls.Add(this.txtCantidad);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtTotalGastos);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtEfectivoRecolectado);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtFondoInicial);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(132, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 457);
            this.panel2.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::LaCaguamaSV.Properties.Resources.cuenta_bancaria;
            this.pictureBox1.Image = global::LaCaguamaSV.Properties.Resources.cuenta_bancaria;
            this.pictureBox1.Location = new System.Drawing.Point(92, 189);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(82, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Location = new System.Drawing.Point(60, 253);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(156, 16);
            this.label9.TabIndex = 22;
            this.label9.Text = "Agregar nuevo gasto:";
            // 
            // txtUtilidad
            // 
            this.txtUtilidad.Location = new System.Drawing.Point(178, 147);
            this.txtUtilidad.Multiline = true;
            this.txtUtilidad.Name = "txtUtilidad";
            this.txtUtilidad.ReadOnly = true;
            this.txtUtilidad.Size = new System.Drawing.Size(76, 24);
            this.txtUtilidad.TabIndex = 21;
            this.txtUtilidad.Text = "$0.00";
            this.txtUtilidad.TextChanged += new System.EventHandler(this.txtUtilidad_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(19, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Total ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(84, 334);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "Descripcion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(19, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Ingrese Gastos";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(260, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "+";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(264, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "-";
            // 
            // txtTotalGastos
            // 
            this.txtTotalGastos.Location = new System.Drawing.Point(178, 109);
            this.txtTotalGastos.Multiline = true;
            this.txtTotalGastos.Name = "txtTotalGastos";
            this.txtTotalGastos.ReadOnly = true;
            this.txtTotalGastos.Size = new System.Drawing.Size(76, 24);
            this.txtTotalGastos.TabIndex = 15;
            this.txtTotalGastos.Text = "$0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(13, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Total de Gastos";
            // 
            // txtEfectivoRecolectado
            // 
            this.txtEfectivoRecolectado.Location = new System.Drawing.Point(178, 63);
            this.txtEfectivoRecolectado.Multiline = true;
            this.txtEfectivoRecolectado.Name = "txtEfectivoRecolectado";
            this.txtEfectivoRecolectado.ReadOnly = true;
            this.txtEfectivoRecolectado.Size = new System.Drawing.Size(76, 24);
            this.txtEfectivoRecolectado.TabIndex = 13;
            this.txtEfectivoRecolectado.Text = "$0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(13, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Efectivo Recolectado";
            // 
            // txtFondoInicial
            // 
            this.txtFondoInicial.Location = new System.Drawing.Point(178, 27);
            this.txtFondoInicial.Multiline = true;
            this.txtFondoInicial.Name = "txtFondoInicial";
            this.txtFondoInicial.ReadOnly = true;
            this.txtFondoInicial.Size = new System.Drawing.Size(76, 24);
            this.txtFondoInicial.TabIndex = 11;
            this.txtFondoInicial.Text = "$0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(13, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Fondo inicial";
            // 
            // dtpFechaInicioGasto
            // 
            this.dtpFechaInicioGasto.Location = new System.Drawing.Point(665, 82);
            this.dtpFechaInicioGasto.Name = "dtpFechaInicioGasto";
            this.dtpFechaInicioGasto.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicioGasto.TabIndex = 23;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnFiltrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFiltrar.Image = global::LaCaguamaSV.Properties.Resources.filtrar;
            this.btnFiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltrar.Location = new System.Drawing.Point(1180, 71);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(103, 47);
            this.btnFiltrar.TabIndex = 24;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // dtpFechaFinGasto
            // 
            this.dtpFechaFinGasto.Location = new System.Drawing.Point(913, 82);
            this.dtpFechaFinGasto.Name = "dtpFechaFinGasto";
            this.dtpFechaFinGasto.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinGasto.TabIndex = 25;
            // 
            // cmbTipoFiltroGasto
            // 
            this.cmbTipoFiltroGasto.FormattingEnabled = true;
            this.cmbTipoFiltroGasto.Location = new System.Drawing.Point(472, 41);
            this.cmbTipoFiltroGasto.Name = "cmbTipoFiltroGasto";
            this.cmbTipoFiltroGasto.Size = new System.Drawing.Size(148, 21);
            this.cmbTipoFiltroGasto.TabIndex = 41;
            this.cmbTipoFiltroGasto.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFiltroGasto_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(962, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Fecha Final";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(704, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 44;
            this.label11.Text = "Fecha de Inicio";
            // 
            // btnResetearFiltro
            // 
            this.btnResetearFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnResetearFiltro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetearFiltro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetearFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetearFiltro.ForeColor = System.Drawing.SystemColors.Control;
            this.btnResetearFiltro.Image = global::LaCaguamaSV.Properties.Resources.actualizar;
            this.btnResetearFiltro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetearFiltro.Location = new System.Drawing.Point(1060, 573);
            this.btnResetearFiltro.Name = "btnResetearFiltro";
            this.btnResetearFiltro.Size = new System.Drawing.Size(128, 47);
            this.btnResetearFiltro.TabIndex = 46;
            this.btnResetearFiltro.Text = "Reiniciar";
            this.btnResetearFiltro.UseVisualStyleBackColor = false;
            this.btnResetearFiltro.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormAdminGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1370, 711);
            this.Controls.Add(this.btnResetearFiltro);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbTipoFiltroGasto);
            this.Controls.Add(this.dtpFechaFinGasto);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.dtpFechaInicioGasto);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnAgregarGasto);
            this.Controls.Add(this.dgvGastos);
            this.Name = "FormAdminGastos";
            this.Text = "Gastos de la caja";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormAdminGastos_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DataGridView dgvGastos;
        private System.Windows.Forms.Button btnAgregarGasto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicioGasto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.TextBox txtUtilidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTotalGastos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEfectivoRecolectado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFondoInicial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaFinGasto;
        private System.Windows.Forms.ComboBox cmbTipoFiltroGasto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnResetearFiltro;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}