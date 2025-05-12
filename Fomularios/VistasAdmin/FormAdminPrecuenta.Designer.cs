namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormAdminPrecuenta
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
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblMesa = new System.Windows.Forms.Label();
            this.lblAtendidoPor = new System.Windows.Forms.Label();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblTipoPago = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.SystemColors.Control;
            this.btnImprimir.Location = new System.Drawing.Point(709, 452);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(129, 57);
            this.btnImprimir.TabIndex = 0;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(876, 452);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(131, 55);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNumeroFactura.Location = new System.Drawing.Point(24, 15);
            this.lblNumeroFactura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(100, 16);
            this.lblNumeroFactura.TabIndex = 2;
            this.lblNumeroFactura.Text = "NumeroFactura";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.ForeColor = System.Drawing.SystemColors.Control;
            this.lblFecha.Location = new System.Drawing.Point(143, 11);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(45, 16);
            this.lblFecha.TabIndex = 3;
            this.lblFecha.Text = "Fecha";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCliente);
            this.panel1.Controls.Add(this.lblFecha);
            this.panel1.Controls.Add(this.lblHora);
            this.panel1.Controls.Add(this.lblMesa);
            this.panel1.Controls.Add(this.lblAtendidoPor);
            this.panel1.Location = new System.Drawing.Point(17, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 41);
            this.panel1.TabIndex = 4;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCliente.Location = new System.Drawing.Point(11, 11);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 16);
            this.lblCliente.TabIndex = 6;
            this.lblCliente.Text = "Cliente";
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.ForeColor = System.Drawing.SystemColors.Control;
            this.lblHora.Location = new System.Drawing.Point(256, 11);
            this.lblHora.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(37, 16);
            this.lblHora.TabIndex = 6;
            this.lblHora.Text = "Hora";
            // 
            // lblMesa
            // 
            this.lblMesa.AutoSize = true;
            this.lblMesa.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMesa.Location = new System.Drawing.Point(363, 11);
            this.lblMesa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(41, 16);
            this.lblMesa.TabIndex = 6;
            this.lblMesa.Text = "Mesa";
            // 
            // lblAtendidoPor
            // 
            this.lblAtendidoPor.AutoSize = true;
            this.lblAtendidoPor.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAtendidoPor.Location = new System.Drawing.Point(476, 11);
            this.lblAtendidoPor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAtendidoPor.Name = "lblAtendidoPor";
            this.lblAtendidoPor.Size = new System.Drawing.Size(82, 16);
            this.lblAtendidoPor.TabIndex = 6;
            this.lblAtendidoPor.Text = "AtendidoPor";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Location = new System.Drawing.Point(17, 65);
            this.dgvDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.RowHeadersWidth = 51;
            this.dgvDetalle.Size = new System.Drawing.Size(680, 314);
            this.dgvDetalle.TabIndex = 5;
            this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellContentClick);
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSubtotal.Location = new System.Drawing.Point(29, 145);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(56, 16);
            this.lblSubtotal.TabIndex = 6;
            this.lblSubtotal.Text = "Subtotal";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTotal.Location = new System.Drawing.Point(29, 180);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(38, 16);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Total";
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDescuento.Location = new System.Drawing.Point(29, 111);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(72, 16);
            this.lblDescuento.TabIndex = 6;
            this.lblDescuento.Text = "Descuento";
            // 
            // lblTipoPago
            // 
            this.lblTipoPago.AutoSize = true;
            this.lblTipoPago.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTipoPago.Location = new System.Drawing.Point(28, 78);
            this.lblTipoPago.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTipoPago.Name = "lblTipoPago";
            this.lblTipoPago.Size = new System.Drawing.Size(68, 16);
            this.lblTipoPago.TabIndex = 6;
            this.lblTipoPago.Text = "TipoPago";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, -4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(557, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _" +
    " _ _ _ _ _ _ _ _ _ _ _ _ _ _ ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(28, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "_ _ _ _ _ _ _ _ _ _ _ _ ";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(29, 214);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "_ _ _ _ _ _ _ _ _ _ _ _ ";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Controls.Add(this.lblNumeroFactura);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblTipoPago);
            this.panel2.Controls.Add(this.lblTotal);
            this.panel2.Controls.Add(this.lblDescuento);
            this.panel2.Controls.Add(this.lblSubtotal);
            this.panel2.Location = new System.Drawing.Point(740, 65);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(267, 314);
            this.panel2.TabIndex = 8;
            // 
            // FormAdminPrecuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnImprimir);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormAdminPrecuenta";
            this.Text = "Precuenta";
            this.Load += new System.EventHandler(this.FormAdminPrecuenta_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblAtendidoPor;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblTipoPago;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
    }
}