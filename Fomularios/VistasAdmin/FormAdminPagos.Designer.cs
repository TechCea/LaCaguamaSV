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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalle)).BeginInit();
            this.panelEfectivo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumeroOrden
            // 
            this.lblNumeroOrden.AutoSize = true;
            this.lblNumeroOrden.Location = new System.Drawing.Point(46, 32);
            this.lblNumeroOrden.Name = "lblNumeroOrden";
            this.lblNumeroOrden.Size = new System.Drawing.Size(35, 13);
            this.lblNumeroOrden.TabIndex = 0;
            this.lblNumeroOrden.Text = "label1";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(121, 32);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(35, 13);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "label1";
            // 
            // lblTotalP
            // 
            this.lblTotalP.AutoSize = true;
            this.lblTotalP.Location = new System.Drawing.Point(186, 32);
            this.lblTotalP.Name = "lblTotalP";
            this.lblTotalP.Size = new System.Drawing.Size(35, 13);
            this.lblTotalP.TabIndex = 0;
            this.lblTotalP.Text = "label1";
            // 
            // comboMetodoPago
            // 
            this.comboMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMetodoPago.FormattingEnabled = true;
            this.comboMetodoPago.Location = new System.Drawing.Point(255, 32);
            this.comboMetodoPago.Name = "comboMetodoPago";
            this.comboMetodoPago.Size = new System.Drawing.Size(121, 21);
            this.comboMetodoPago.TabIndex = 1;
            this.comboMetodoPago.SelectedIndexChanged += new System.EventHandler(this.comboMetodoPago_SelectedIndexChanged_1);
            // 
            // dataGridViewDetalle
            // 
            this.dataGridViewDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetalle.Location = new System.Drawing.Point(49, 84);
            this.dataGridViewDetalle.Name = "dataGridViewDetalle";
            this.dataGridViewDetalle.Size = new System.Drawing.Size(474, 150);
            this.dataGridViewDetalle.TabIndex = 2;
            // 
            // panelEfectivo
            // 
            this.panelEfectivo.Controls.Add(this.lblCambio);
            this.panelEfectivo.Controls.Add(this.txtRecibido);
            this.panelEfectivo.Location = new System.Drawing.Point(541, 84);
            this.panelEfectivo.Name = "panelEfectivo";
            this.panelEfectivo.Size = new System.Drawing.Size(200, 100);
            this.panelEfectivo.TabIndex = 3;
            // 
            // lblCambio
            // 
            this.lblCambio.AutoSize = true;
            this.lblCambio.Location = new System.Drawing.Point(53, 66);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(35, 13);
            this.lblCambio.TabIndex = 1;
            this.lblCambio.Text = "label1";
            // 
            // txtRecibido
            // 
            this.txtRecibido.Location = new System.Drawing.Point(53, 29);
            this.txtRecibido.Name = "txtRecibido";
            this.txtRecibido.Size = new System.Drawing.Size(100, 20);
            this.txtRecibido.TabIndex = 0;
            this.txtRecibido.TextChanged += new System.EventHandler(this.txtRecibido_TextChanged);
            // 
            // btnProcesarPago
            // 
            this.btnProcesarPago.Location = new System.Drawing.Point(189, 293);
            this.btnProcesarPago.Name = "btnProcesarPago";
            this.btnProcesarPago.Size = new System.Drawing.Size(116, 36);
            this.btnProcesarPago.TabIndex = 4;
            this.btnProcesarPago.Text = "Procesar Pago";
            this.btnProcesarPago.UseVisualStyleBackColor = true;
            this.btnProcesarPago.Click += new System.EventHandler(this.btnProcesarPago_Click_1);
            // 
            // btnImprimirComprobante
            // 
            this.btnImprimirComprobante.Location = new System.Drawing.Point(312, 293);
            this.btnImprimirComprobante.Name = "btnImprimirComprobante";
            this.btnImprimirComprobante.Size = new System.Drawing.Size(142, 36);
            this.btnImprimirComprobante.TabIndex = 5;
            this.btnImprimirComprobante.Text = "Imprimir Comprobante";
            this.btnImprimirComprobante.UseVisualStyleBackColor = true;
            this.btnImprimirComprobante.Click += new System.EventHandler(this.btnImprimirComprobante_Click_1);
            // 
            // FormAdminPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnImprimirComprobante);
            this.Controls.Add(this.btnProcesarPago);
            this.Controls.Add(this.panelEfectivo);
            this.Controls.Add(this.dataGridViewDetalle);
            this.Controls.Add(this.comboMetodoPago);
            this.Controls.Add(this.lblTotalP);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblNumeroOrden);
            this.Name = "FormAdminPagos";
            this.Text = "FormAdminPagos";
            this.Load += new System.EventHandler(this.FormAdminPagos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalle)).EndInit();
            this.panelEfectivo.ResumeLayout(false);
            this.panelEfectivo.PerformLayout();
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
    }
}