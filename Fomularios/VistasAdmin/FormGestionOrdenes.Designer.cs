namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormGestionOrdenes
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
            this.lblIdOrden = new System.Windows.Forms.Label();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblFechaOrden = new System.Windows.Forms.Label();
            this.lblTipoPago = new System.Windows.Forms.Label();
            this.lblNumeroMesa = new System.Windows.Forms.Label();
            this.lblEstadoOrden = new System.Windows.Forms.Label();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblIdOrden
            // 
            this.lblIdOrden.AutoSize = true;
            this.lblIdOrden.Location = new System.Drawing.Point(35, 50);
            this.lblIdOrden.Name = "lblIdOrden";
            this.lblIdOrden.Size = new System.Drawing.Size(35, 13);
            this.lblIdOrden.TabIndex = 0;
            this.lblIdOrden.Text = "label1";
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Location = new System.Drawing.Point(35, 97);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(35, 13);
            this.lblNombreCliente.TabIndex = 0;
            this.lblNombreCliente.Text = "label1";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(48, 171);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(35, 13);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "label1";
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Location = new System.Drawing.Point(57, 211);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(35, 13);
            this.lblDescuento.TabIndex = 0;
            this.lblDescuento.Text = "label1";
            // 
            // lblFechaOrden
            // 
            this.lblFechaOrden.AutoSize = true;
            this.lblFechaOrden.Location = new System.Drawing.Point(156, 50);
            this.lblFechaOrden.Name = "lblFechaOrden";
            this.lblFechaOrden.Size = new System.Drawing.Size(35, 13);
            this.lblFechaOrden.TabIndex = 0;
            this.lblFechaOrden.Text = "label1";
            // 
            // lblTipoPago
            // 
            this.lblTipoPago.AutoSize = true;
            this.lblTipoPago.Location = new System.Drawing.Point(167, 183);
            this.lblTipoPago.Name = "lblTipoPago";
            this.lblTipoPago.Size = new System.Drawing.Size(35, 13);
            this.lblTipoPago.TabIndex = 0;
            this.lblTipoPago.Text = "label1";
            // 
            // lblNumeroMesa
            // 
            this.lblNumeroMesa.AutoSize = true;
            this.lblNumeroMesa.Location = new System.Drawing.Point(156, 120);
            this.lblNumeroMesa.Name = "lblNumeroMesa";
            this.lblNumeroMesa.Size = new System.Drawing.Size(35, 13);
            this.lblNumeroMesa.TabIndex = 0;
            this.lblNumeroMesa.Text = "label1";
            // 
            // lblEstadoOrden
            // 
            this.lblEstadoOrden.AutoSize = true;
            this.lblEstadoOrden.Location = new System.Drawing.Point(167, 229);
            this.lblEstadoOrden.Name = "lblEstadoOrden";
            this.lblEstadoOrden.Size = new System.Drawing.Size(35, 13);
            this.lblEstadoOrden.TabIndex = 0;
            this.lblEstadoOrden.Text = "label1";
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.Location = new System.Drawing.Point(387, 50);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(35, 13);
            this.lblNombreUsuario.TabIndex = 0;
            this.lblNombreUsuario.Text = "label1";
            // 
            // FormGestionOrdenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblEstadoOrden);
            this.Controls.Add(this.lblDescuento);
            this.Controls.Add(this.lblNumeroMesa);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTipoPago);
            this.Controls.Add(this.lblNombreCliente);
            this.Controls.Add(this.lblNombreUsuario);
            this.Controls.Add(this.lblFechaOrden);
            this.Controls.Add(this.lblIdOrden);
            this.Name = "FormGestionOrdenes";
            this.Text = "FormGestionOrdenes";
            this.Load += new System.EventHandler(this.FormGestionOrdenes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIdOrden;
        private System.Windows.Forms.Label lblNombreCliente;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblFechaOrden;
        private System.Windows.Forms.Label lblTipoPago;
        private System.Windows.Forms.Label lblNumeroMesa;
        private System.Windows.Forms.Label lblEstadoOrden;
        private System.Windows.Forms.Label lblNombreUsuario;
    }
}