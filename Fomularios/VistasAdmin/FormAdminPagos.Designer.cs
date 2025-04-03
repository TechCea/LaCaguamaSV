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
            this.lblCambio = new System.Windows.Forms.Label();
            this.txtRecibido = new System.Windows.Forms.TextBox();
            this.btnProcesarPago = new System.Windows.Forms.Button();
            this.btnImprimirComprobante = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalle)).BeginInit();
            this.panelEfectivo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumeroOrden
            // 
            this.lblNumeroOrden.AutoSize = true;
            this.lblNumeroOrden.Location = new System.Drawing.Point(46, 32);
            this.lblNumeroOrden.Name = "lblNumeroOrden";
            this.lblNumeroOrden.Size = new System.Drawing.Size(73, 13);
            this.lblNumeroOrden.TabIndex = 0;
            this.lblNumeroOrden.Text = "NumeroOrden";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(154, 32);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(39, 13);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente";
            this.lblCliente.Click += new System.EventHandler(this.lblCliente_Click);
            // 
            // lblTotalP
            // 
            this.lblTotalP.AutoSize = true;
            this.lblTotalP.Location = new System.Drawing.Point(120, 41);
            this.lblTotalP.Name = "lblTotalP";
            this.lblTotalP.Size = new System.Drawing.Size(34, 13);
            this.lblTotalP.TabIndex = 0;
            this.lblTotalP.Text = "$0.00";
            this.lblTotalP.Click += new System.EventHandler(this.lblTotalP_Click);
            // 
            // comboMetodoPago
            // 
            this.comboMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMetodoPago.FormattingEnabled = true;
            this.comboMetodoPago.Location = new System.Drawing.Point(38, 24);
            this.comboMetodoPago.Name = "comboMetodoPago";
            this.comboMetodoPago.Size = new System.Drawing.Size(121, 21);
            this.comboMetodoPago.TabIndex = 1;
            this.comboMetodoPago.SelectedIndexChanged += new System.EventHandler(this.comboMetodoPago_SelectedIndexChanged_1);
            // 
            // dataGridViewDetalle
            // 
            this.dataGridViewDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetalle.Location = new System.Drawing.Point(53, 62);
            this.dataGridViewDetalle.Name = "dataGridViewDetalle";
            this.dataGridViewDetalle.Size = new System.Drawing.Size(474, 222);
            this.dataGridViewDetalle.TabIndex = 2;
            // 
            // panelEfectivo
            // 
            this.panelEfectivo.Controls.Add(this.label3);
            this.panelEfectivo.Controls.Add(this.txtRecibido);
            this.panelEfectivo.Location = new System.Drawing.Point(533, 129);
            this.panelEfectivo.Name = "panelEfectivo";
            this.panelEfectivo.Size = new System.Drawing.Size(208, 83);
            this.panelEfectivo.TabIndex = 3;
            // 
            // lblCambio
            // 
            this.lblCambio.AutoSize = true;
            this.lblCambio.Location = new System.Drawing.Point(120, 10);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(34, 13);
            this.lblCambio.TabIndex = 1;
            this.lblCambio.Text = "$0.00";
            // 
            // txtRecibido
            // 
            this.txtRecibido.Location = new System.Drawing.Point(55, 39);
            this.txtRecibido.Name = "txtRecibido";
            this.txtRecibido.Size = new System.Drawing.Size(100, 20);
            this.txtRecibido.TabIndex = 0;
            this.txtRecibido.TextChanged += new System.EventHandler(this.txtRecibido_TextChanged);
            // 
            // btnProcesarPago
            // 
            this.btnProcesarPago.BackColor = System.Drawing.Color.Lime;
            this.btnProcesarPago.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnProcesarPago.Location = new System.Drawing.Point(189, 293);
            this.btnProcesarPago.Name = "btnProcesarPago";
            this.btnProcesarPago.Size = new System.Drawing.Size(116, 36);
            this.btnProcesarPago.TabIndex = 4;
            this.btnProcesarPago.Text = "Procesar Pago";
            this.btnProcesarPago.UseVisualStyleBackColor = false;
            this.btnProcesarPago.Click += new System.EventHandler(this.btnProcesarPago_Click_1);
            // 
            // btnImprimirComprobante
            // 
            this.btnImprimirComprobante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnImprimirComprobante.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnImprimirComprobante.Location = new System.Drawing.Point(334, 293);
            this.btnImprimirComprobante.Name = "btnImprimirComprobante";
            this.btnImprimirComprobante.Size = new System.Drawing.Size(142, 36);
            this.btnImprimirComprobante.TabIndex = 5;
            this.btnImprimirComprobante.Text = "Imprimir Comprobante";
            this.btnImprimirComprobante.UseVisualStyleBackColor = false;
            this.btnImprimirComprobante.Click += new System.EventHandler(this.btnImprimirComprobante_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Forma de Pago";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ingrese Monto Recibido";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Location = new System.Drawing.Point(23, 23);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(718, 33);
            this.panel5.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.comboMetodoPago);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(533, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 61);
            this.panel1.TabIndex = 13;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblCambio);
            this.panel2.Controls.Add(this.lblTotalP);
            this.panel2.Location = new System.Drawing.Point(533, 218);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 66);
            this.panel2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cambio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Total a Pagar";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // FormAdminPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewDetalle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnImprimirComprobante);
            this.Controls.Add(this.btnProcesarPago);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblNumeroOrden);
            this.Controls.Add(this.panelEfectivo);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Name = "FormAdminPagos";
            this.Text = "FormAdminPagos";
            this.Load += new System.EventHandler(this.FormAdminPagos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalle)).EndInit();
            this.panelEfectivo.ResumeLayout(false);
            this.panelEfectivo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
    }
}