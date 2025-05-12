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
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditarGasto = new System.Windows.Forms.Button();
            this.btnEliminarGasto = new System.Windows.Forms.Button();
            this.dtpBuscarGastos = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFondoInicial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEfectivoRecolectado = new System.Windows.Forms.TextBox();
            this.txtUtilidad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalGastos = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).BeginInit();
            this.panel2.SuspendLayout();
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
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDescripcion.Location = new System.Drawing.Point(16, 353);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(245, 76);
            this.txtDescripcion.TabIndex = 1;
            // 
            // dgvGastos
            // 
            this.dgvGastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGastos.Location = new System.Drawing.Point(366, 71);
            this.dgvGastos.Name = "dgvGastos";
            this.dgvGastos.RowHeadersWidth = 51;
            this.dgvGastos.Size = new System.Drawing.Size(486, 379);
            this.dgvGastos.TabIndex = 4;
            this.dgvGastos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGastos_CellContentClick);
            // 
            // btnAgregarGasto
            // 
            this.btnAgregarGasto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.btnAgregarGasto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregarGasto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarGasto.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAgregarGasto.Location = new System.Drawing.Point(38, 516);
            this.btnAgregarGasto.Name = "btnAgregarGasto";
            this.btnAgregarGasto.Size = new System.Drawing.Size(80, 48);
            this.btnAgregarGasto.TabIndex = 5;
            this.btnAgregarGasto.Text = "Agregar";
            this.btnAgregarGasto.UseVisualStyleBackColor = false;
            this.btnAgregarGasto.Click += new System.EventHandler(this.btnAgregarGasto_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GrayText;
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
            this.panel2.Location = new System.Drawing.Point(22, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 457);
            this.panel2.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Location = new System.Drawing.Point(19, 240);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(156, 16);
            this.label9.TabIndex = 22;
            this.label9.Text = "Agregar nuevo gasto:";
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
            // btnEditarGasto
            // 
            this.btnEditarGasto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnEditarGasto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditarGasto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditarGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarGasto.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEditarGasto.Location = new System.Drawing.Point(137, 516);
            this.btnEditarGasto.Name = "btnEditarGasto";
            this.btnEditarGasto.Size = new System.Drawing.Size(89, 48);
            this.btnEditarGasto.TabIndex = 19;
            this.btnEditarGasto.Text = "Actualizar";
            this.btnEditarGasto.UseVisualStyleBackColor = false;
            this.btnEditarGasto.Click += new System.EventHandler(this.btnEditarGasto_Click);
            // 
            // btnEliminarGasto
            // 
            this.btnEliminarGasto.BackColor = System.Drawing.Color.Red;
            this.btnEliminarGasto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminarGasto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarGasto.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEliminarGasto.Location = new System.Drawing.Point(243, 516);
            this.btnEliminarGasto.Name = "btnEliminarGasto";
            this.btnEliminarGasto.Size = new System.Drawing.Size(80, 48);
            this.btnEliminarGasto.TabIndex = 20;
            this.btnEliminarGasto.Text = "Eliminar";
            this.btnEliminarGasto.UseVisualStyleBackColor = false;
            this.btnEliminarGasto.Click += new System.EventHandler(this.btnEliminarGasto_Click);
            // 
            // dtpBuscarGastos
            // 
            this.dtpBuscarGastos.Location = new System.Drawing.Point(520, 45);
            this.dtpBuscarGastos.Name = "dtpBuscarGastos";
            this.dtpBuscarGastos.Size = new System.Drawing.Size(200, 20);
            this.dtpBuscarGastos.TabIndex = 23;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnFiltrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFiltrar.Location = new System.Drawing.Point(341, 516);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(89, 48);
            this.btnFiltrar.TabIndex = 24;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
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
            // txtUtilidad
            // 
            this.txtUtilidad.Location = new System.Drawing.Point(178, 147);
            this.txtUtilidad.Multiline = true;
            this.txtUtilidad.Name = "txtUtilidad";
            this.txtUtilidad.ReadOnly = true;
            this.txtUtilidad.Size = new System.Drawing.Size(76, 24);
            this.txtUtilidad.TabIndex = 21;
            this.txtUtilidad.Text = "$0.00";
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
            // FormAdminGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(950, 588);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.dtpBuscarGastos);
            this.Controls.Add(this.btnEliminarGasto);
            this.Controls.Add(this.btnEditarGasto);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnAgregarGasto);
            this.Controls.Add(this.dgvGastos);
            this.Name = "FormAdminGastos";
            this.Text = "Gastos de la caja";
            this.Load += new System.EventHandler(this.FormAdminGastos_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DataGridView dgvGastos;
        private System.Windows.Forms.Button btnAgregarGasto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditarGasto;
        private System.Windows.Forms.Button btnEliminarGasto;
        private System.Windows.Forms.DateTimePicker dtpBuscarGastos;
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
    }
}