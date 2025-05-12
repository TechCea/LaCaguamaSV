namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormAdminPagos
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
            this.lblNumeroOrden = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblTotalP = new System.Windows.Forms.Label();
            this.comboMetodoPago = new System.Windows.Forms.ComboBox();
            this.dataGridViewDetalle = new System.Windows.Forms.DataGridView();
            this.panelEfectivo = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRecibido = new System.Windows.Forms.TextBox();
            this.lblCambio = new System.Windows.Forms.Label();
            this.btnProcesarPago = new System.Windows.Forms.Button();
            this.btnImprimirComprobante = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAplicarDescuento = new System.Windows.Forms.CheckBox();
            this.panelDescuentos = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDescuentoAplicado = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAplicarDescuento = new System.Windows.Forms.Button();
            this.txtMontoDescuento = new System.Windows.Forms.TextBox();
            this.cmbTipoDescuento = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalle)).BeginInit();
            this.panelEfectivo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelDescuentos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumeroOrden
            // 
            this.lblNumeroOrden.AutoSize = true;
            this.lblNumeroOrden.Location = new System.Drawing.Point(61, 39);
            this.lblNumeroOrden.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumeroOrden.Name = "lblNumeroOrden";
            this.lblNumeroOrden.Size = new System.Drawing.Size(92, 16);
            this.lblNumeroOrden.TabIndex = 0;
            this.lblNumeroOrden.Text = "NumeroOrden";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(205, 39);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 16);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente";
            this.lblCliente.Click += new System.EventHandler(this.lblCliente_Click);
            // 
            // lblTotalP
            // 
            this.lblTotalP.AutoSize = true;
            this.lblTotalP.Location = new System.Drawing.Point(193, 90);
            this.lblTotalP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalP.Name = "lblTotalP";
            this.lblTotalP.Size = new System.Drawing.Size(38, 16);
            this.lblTotalP.TabIndex = 0;
            this.lblTotalP.Text = "$0.00";
            this.lblTotalP.Click += new System.EventHandler(this.lblTotalP_Click);
            // 
            // comboMetodoPago
            // 
            this.comboMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMetodoPago.FormattingEnabled = true;
            this.comboMetodoPago.Location = new System.Drawing.Point(84, 28);
            this.comboMetodoPago.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboMetodoPago.Name = "comboMetodoPago";
            this.comboMetodoPago.Size = new System.Drawing.Size(160, 24);
            this.comboMetodoPago.TabIndex = 1;
            this.comboMetodoPago.SelectedIndexChanged += new System.EventHandler(this.comboMetodoPago_SelectedIndexChanged_1);
            // 
            // dataGridViewDetalle
            // 
            this.dataGridViewDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetalle.Location = new System.Drawing.Point(71, 76);
            this.dataGridViewDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewDetalle.Name = "dataGridViewDetalle";
            this.dataGridViewDetalle.RowHeadersWidth = 51;
            this.dataGridViewDetalle.Size = new System.Drawing.Size(632, 374);
            this.dataGridViewDetalle.TabIndex = 2;
            // 
            // panelEfectivo
            // 
            this.panelEfectivo.Controls.Add(this.label3);
            this.panelEfectivo.Controls.Add(this.txtRecibido);
            this.panelEfectivo.Location = new System.Drawing.Point(711, 159);
            this.panelEfectivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelEfectivo.Name = "panelEfectivo";
            this.panelEfectivo.Size = new System.Drawing.Size(320, 102);
            this.panelEfectivo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ingrese Monto Recibido";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtRecibido
            // 
            this.txtRecibido.Location = new System.Drawing.Point(91, 53);
            this.txtRecibido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRecibido.Name = "txtRecibido";
            this.txtRecibido.Size = new System.Drawing.Size(132, 22);
            this.txtRecibido.TabIndex = 0;
            this.txtRecibido.TextChanged += new System.EventHandler(this.txtRecibido_TextChanged);
            // 
            // lblCambio
            // 
            this.lblCambio.AutoSize = true;
            this.lblCambio.Location = new System.Drawing.Point(193, 12);
            this.lblCambio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(38, 16);
            this.lblCambio.TabIndex = 1;
            this.lblCambio.Text = "$0.00";
            // 
            // btnProcesarPago
            // 
            this.btnProcesarPago.BackColor = System.Drawing.Color.Lime;
            this.btnProcesarPago.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnProcesarPago.Location = new System.Drawing.Point(209, 480);
            this.btnProcesarPago.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProcesarPago.Name = "btnProcesarPago";
            this.btnProcesarPago.Size = new System.Drawing.Size(155, 44);
            this.btnProcesarPago.TabIndex = 4;
            this.btnProcesarPago.Text = "Procesar Pago";
            this.btnProcesarPago.UseVisualStyleBackColor = false;
            this.btnProcesarPago.Click += new System.EventHandler(this.btnProcesarPago_Click_1);
            // 
            // btnImprimirComprobante
            // 
            this.btnImprimirComprobante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnImprimirComprobante.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnImprimirComprobante.Location = new System.Drawing.Point(440, 480);
            this.btnImprimirComprobante.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImprimirComprobante.Name = "btnImprimirComprobante";
            this.btnImprimirComprobante.Size = new System.Drawing.Size(189, 44);
            this.btnImprimirComprobante.TabIndex = 5;
            this.btnImprimirComprobante.Text = "Imprimir Comprobante";
            this.btnImprimirComprobante.UseVisualStyleBackColor = false;
            this.btnImprimirComprobante.Click += new System.EventHandler(this.btnImprimirComprobante_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Forma de Pago";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Location = new System.Drawing.Point(31, 28);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1000, 41);
            this.panel5.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.comboMetodoPago);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(711, 76);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 75);
            this.panel1.TabIndex = 13;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblSubtotal);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblCambio);
            this.panel2.Controls.Add(this.lblTotalP);
            this.panel2.Location = new System.Drawing.Point(712, 500);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(319, 126);
            this.panel2.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 55);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Sub Total";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(193, 55);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(38, 16);
            this.lblSubtotal.TabIndex = 4;
            this.lblSubtotal.Text = "$0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Total a Pagar";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cambio";
            // 
            // chkAplicarDescuento
            // 
            this.chkAplicarDescuento.AutoSize = true;
            this.chkAplicarDescuento.Location = new System.Drawing.Point(72, 16);
            this.chkAplicarDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAplicarDescuento.Name = "chkAplicarDescuento";
            this.chkAplicarDescuento.Size = new System.Drawing.Size(139, 20);
            this.chkAplicarDescuento.TabIndex = 15;
            this.chkAplicarDescuento.Text = "Aplicar Descuento";
            this.chkAplicarDescuento.UseVisualStyleBackColor = true;
            this.chkAplicarDescuento.CheckedChanged += new System.EventHandler(this.chkAplicarDescuento_CheckedChanged);
            // 
            // panelDescuentos
            // 
            this.panelDescuentos.Controls.Add(this.label8);
            this.panelDescuentos.Controls.Add(this.lblDescuentoAplicado);
            this.panelDescuentos.Controls.Add(this.label6);
            this.panelDescuentos.Controls.Add(this.label5);
            this.panelDescuentos.Controls.Add(this.btnAplicarDescuento);
            this.panelDescuentos.Controls.Add(this.txtMontoDescuento);
            this.panelDescuentos.Controls.Add(this.cmbTipoDescuento);
            this.panelDescuentos.Controls.Add(this.chkAplicarDescuento);
            this.panelDescuentos.Location = new System.Drawing.Point(712, 266);
            this.panelDescuentos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelDescuentos.Name = "panelDescuentos";
            this.panelDescuentos.Size = new System.Drawing.Size(319, 226);
            this.panelDescuentos.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 140);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "Monto Descontado";
            // 
            // lblDescuentoAplicado
            // 
            this.lblDescuentoAplicado.AutoSize = true;
            this.lblDescuentoAplicado.Location = new System.Drawing.Point(176, 140);
            this.lblDescuentoAplicado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescuentoAplicado.Name = "lblDescuentoAplicado";
            this.lblDescuentoAplicado.Size = new System.Drawing.Size(31, 16);
            this.lblDescuentoAplicado.TabIndex = 16;
            this.lblDescuentoAplicado.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Monto";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 62);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Tipo Descuento";
            this.label5.Visible = false;
            // 
            // btnAplicarDescuento
            // 
            this.btnAplicarDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnAplicarDescuento.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAplicarDescuento.Location = new System.Drawing.Point(72, 171);
            this.btnAplicarDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAplicarDescuento.Name = "btnAplicarDescuento";
            this.btnAplicarDescuento.Size = new System.Drawing.Size(189, 44);
            this.btnAplicarDescuento.TabIndex = 16;
            this.btnAplicarDescuento.Text = "Aplicar Descuento";
            this.btnAplicarDescuento.UseVisualStyleBackColor = false;
            this.btnAplicarDescuento.Visible = false;
            this.btnAplicarDescuento.Click += new System.EventHandler(this.btnAplicarDescuento_Click);
            // 
            // txtMontoDescuento
            // 
            this.txtMontoDescuento.Location = new System.Drawing.Point(137, 95);
            this.txtMontoDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMontoDescuento.Name = "txtMontoDescuento";
            this.txtMontoDescuento.Size = new System.Drawing.Size(161, 22);
            this.txtMontoDescuento.TabIndex = 17;
            this.txtMontoDescuento.Visible = false;
            this.txtMontoDescuento.TextChanged += new System.EventHandler(this.txtMontoDescuento_TextChanged);
            // 
            // cmbTipoDescuento
            // 
            this.cmbTipoDescuento.FormattingEnabled = true;
            this.cmbTipoDescuento.Location = new System.Drawing.Point(139, 58);
            this.cmbTipoDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTipoDescuento.Name = "cmbTipoDescuento";
            this.cmbTipoDescuento.Size = new System.Drawing.Size(160, 24);
            this.cmbTipoDescuento.TabIndex = 16;
            this.cmbTipoDescuento.Visible = false;
            // 
            // FormAdminPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(1067, 634);
            this.Controls.Add(this.panelDescuentos);
            this.Controls.Add(this.dataGridViewDetalle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnImprimirComprobante);
            this.Controls.Add(this.btnProcesarPago);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblNumeroOrden);
            this.Controls.Add(this.panelEfectivo);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormAdminPagos";
            this.Text = "Pagos";
            this.Load += new System.EventHandler(this.FormAdminPagos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalle)).EndInit();
            this.panelEfectivo.ResumeLayout(false);
            this.panelEfectivo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelDescuentos.ResumeLayout(false);
            this.panelDescuentos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumeroOrden;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblTotalP;
        private System.Windows.Forms.ComboBox comboMetodoPago;
        private System.Windows.Forms.DataGridView dataGridViewDetalle;
        private System.Windows.Forms.Panel panelEfectivo;
        private System.Windows.Forms.TextBox txtRecibido;
        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.Button btnProcesarPago;
        private System.Windows.Forms.Button btnImprimirComprobante;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAplicarDescuento;
        private System.Windows.Forms.Panel panelDescuentos;
        private System.Windows.Forms.Button btnAplicarDescuento;
        private System.Windows.Forms.TextBox txtMontoDescuento;
        private System.Windows.Forms.ComboBox cmbTipoDescuento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDescuentoAplicado;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}