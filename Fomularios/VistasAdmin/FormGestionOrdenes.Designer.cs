using System;

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
            this.lblEstadoOrden = new System.Windows.Forms.Label();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.comboBoxMesas = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.btnCargarExtras = new System.Windows.Forms.Button();
            this.btnCargarPlatos = new System.Windows.Forms.Button();
            this.btnCargarBebidas = new System.Windows.Forms.Button();
            this.dataGridViewMenu = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanelPedidos = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPrecuenta = new System.Windows.Forms.Button();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIdOrden
            // 
            this.lblIdOrden.AutoSize = true;
            this.lblIdOrden.Location = new System.Drawing.Point(11, 5);
            this.lblIdOrden.Name = "lblIdOrden";
            this.lblIdOrden.Size = new System.Drawing.Size(24, 13);
            this.lblIdOrden.TabIndex = 0;
            this.lblIdOrden.Text = "IdO";
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Location = new System.Drawing.Point(94, 5);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(29, 13);
            this.lblNombreCliente.TabIndex = 0;
            this.lblNombreCliente.Text = "label";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.AliceBlue;
            this.lblTotal.Location = new System.Drawing.Point(115, 21);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(35, 13);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "label1";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.BackColor = System.Drawing.Color.AliceBlue;
            this.lblDescuento.Location = new System.Drawing.Point(115, 51);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(35, 13);
            this.lblDescuento.TabIndex = 0;
            this.lblDescuento.Text = "label1";
            // 
            // lblFechaOrden
            // 
            this.lblFechaOrden.AutoSize = true;
            this.lblFechaOrden.Location = new System.Drawing.Point(323, 10);
            this.lblFechaOrden.Name = "lblFechaOrden";
            this.lblFechaOrden.Size = new System.Drawing.Size(35, 13);
            this.lblFechaOrden.TabIndex = 0;
            this.lblFechaOrden.Text = "label1";
            // 
            // lblTipoPago
            // 
            this.lblTipoPago.AutoSize = true;
            this.lblTipoPago.Location = new System.Drawing.Point(115, 131);
            this.lblTipoPago.Name = "lblTipoPago";
            this.lblTipoPago.Size = new System.Drawing.Size(35, 13);
            this.lblTipoPago.TabIndex = 0;
            this.lblTipoPago.Text = "label1";
            // 
            // lblEstadoOrden
            // 
            this.lblEstadoOrden.AutoSize = true;
            this.lblEstadoOrden.Location = new System.Drawing.Point(398, 12);
            this.lblEstadoOrden.Name = "lblEstadoOrden";
            this.lblEstadoOrden.Size = new System.Drawing.Size(35, 13);
            this.lblEstadoOrden.TabIndex = 0;
            this.lblEstadoOrden.Text = "label1";
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNombreUsuario.Location = new System.Drawing.Point(229, 9);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(35, 13);
            this.lblNombreUsuario.TabIndex = 0;
            this.lblNombreUsuario.Text = "label1";
            this.lblNombreUsuario.Click += new System.EventHandler(this.lblNombreUsuario_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Controls.Add(this.comboBoxMesas);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.lblNombreUsuario);
            this.panel5.Controls.Add(this.lblEstadoOrden);
            this.panel5.Controls.Add(this.lblFechaOrden);
            this.panel5.Location = new System.Drawing.Point(208, -1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(590, 33);
            this.panel5.TabIndex = 11;
            // 
            // comboBoxMesas
            // 
            this.comboBoxMesas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMesas.FormattingEnabled = true;
            this.comboBoxMesas.Location = new System.Drawing.Point(38, 6);
            this.comboBoxMesas.Name = "comboBoxMesas";
            this.comboBoxMesas.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMesas.TabIndex = 2;
            this.comboBoxMesas.SelectedIndexChanged += new System.EventHandler(this.comboBoxMesas_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(186, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Cajero";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mesa";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblIdOrden);
            this.panel3.Controls.Add(this.lblNombreCliente);
            this.panel3.Location = new System.Drawing.Point(1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 24);
            this.panel3.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(47, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Cuenta";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblTipoPago);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblDescuento);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Location = new System.Drawing.Point(1, 275);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 173);
            this.panel1.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(12, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Total";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(12, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Descuentos";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(12, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Subtotal";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.AliceBlue;
            this.label1.Location = new System.Drawing.Point(115, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button12.Location = new System.Drawing.Point(572, 55);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(81, 28);
            this.button12.TabIndex = 30;
            this.button12.Text = "PAGAR";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // btnCargarExtras
            // 
            this.btnCargarExtras.BackColor = System.Drawing.Color.Yellow;
            this.btnCargarExtras.Location = new System.Drawing.Point(397, 54);
            this.btnCargarExtras.Name = "btnCargarExtras";
            this.btnCargarExtras.Size = new System.Drawing.Size(81, 28);
            this.btnCargarExtras.TabIndex = 23;
            this.btnCargarExtras.Text = "EXTRAS";
            this.btnCargarExtras.UseVisualStyleBackColor = false;
            this.btnCargarExtras.Click += new System.EventHandler(this.btnCargarExtras_Click);
            // 
            // btnCargarPlatos
            // 
            this.btnCargarPlatos.BackColor = System.Drawing.Color.Yellow;
            this.btnCargarPlatos.Location = new System.Drawing.Point(310, 55);
            this.btnCargarPlatos.Name = "btnCargarPlatos";
            this.btnCargarPlatos.Size = new System.Drawing.Size(81, 28);
            this.btnCargarPlatos.TabIndex = 22;
            this.btnCargarPlatos.Text = "COMIDA";
            this.btnCargarPlatos.UseVisualStyleBackColor = false;
            this.btnCargarPlatos.Click += new System.EventHandler(this.btnCargarPlatos_Click);
            // 
            // btnCargarBebidas
            // 
            this.btnCargarBebidas.BackColor = System.Drawing.Color.Yellow;
            this.btnCargarBebidas.Location = new System.Drawing.Point(219, 54);
            this.btnCargarBebidas.Name = "btnCargarBebidas";
            this.btnCargarBebidas.Size = new System.Drawing.Size(85, 29);
            this.btnCargarBebidas.TabIndex = 21;
            this.btnCargarBebidas.Text = "BEBIDAS";
            this.btnCargarBebidas.UseVisualStyleBackColor = false;
            this.btnCargarBebidas.Click += new System.EventHandler(this.btnCargarBebidas_Click);
            // 
            // dataGridViewMenu
            // 
            this.dataGridViewMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMenu.Location = new System.Drawing.Point(214, 89);
            this.dataGridViewMenu.MultiSelect = false;
            this.dataGridViewMenu.Name = "dataGridViewMenu";
            this.dataGridViewMenu.ReadOnly = true;
            this.dataGridViewMenu.RowHeadersWidth = 51;
            this.dataGridViewMenu.Size = new System.Drawing.Size(533, 305);
            this.dataGridViewMenu.TabIndex = 32;
            this.dataGridViewMenu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMenu_CellClick);
            // 
            // flowLayoutPanelPedidos
            // 
            this.flowLayoutPanelPedidos.AutoScroll = true;
            this.flowLayoutPanelPedidos.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanelPedidos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPedidos.Location = new System.Drawing.Point(1, 21);
            this.flowLayoutPanelPedidos.Name = "flowLayoutPanelPedidos";
            this.flowLayoutPanelPedidos.Size = new System.Drawing.Size(207, 257);
            this.flowLayoutPanelPedidos.TabIndex = 33;
            this.flowLayoutPanelPedidos.WrapContents = false;
            this.flowLayoutPanelPedidos.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelPedidos_Paint);
            // 
            // btnPrecuenta
            // 
            this.btnPrecuenta.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPrecuenta.Location = new System.Drawing.Point(485, 55);
            this.btnPrecuenta.Name = "btnPrecuenta";
            this.btnPrecuenta.Size = new System.Drawing.Size(81, 28);
            this.btnPrecuenta.TabIndex = 34;
            this.btnPrecuenta.Text = "PRECUENTA";
            this.btnPrecuenta.UseVisualStyleBackColor = false;
            this.btnPrecuenta.Click += new System.EventHandler(this.btnPrecuenta_Click);
            // 
            // FormGestionOrdenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPrecuenta);
            this.Controls.Add(this.flowLayoutPanelPedidos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridViewMenu);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.btnCargarExtras);
            this.Controls.Add(this.btnCargarPlatos);
            this.Controls.Add(this.btnCargarBebidas);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Name = "FormGestionOrdenes";
            this.Text = "FormGestionOrdenes";
            this.Load += new System.EventHandler(this.FormGestionOrdenes_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenu)).EndInit();
            this.ResumeLayout(false);

        }

        private void ButPrecuenta_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void comboBoxMesas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label lblIdOrden;
        private System.Windows.Forms.Label lblNombreCliente;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblFechaOrden;
        private System.Windows.Forms.Label lblTipoPago;
        private System.Windows.Forms.Label lblEstadoOrden;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button btnCargarExtras;
        private System.Windows.Forms.Button btnCargarPlatos;
        private System.Windows.Forms.Button btnCargarBebidas;
        private System.Windows.Forms.DataGridView dataGridViewMenu;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPedidos;
        private System.Windows.Forms.ComboBox comboBoxMesas;
        private System.Windows.Forms.Button btnPrecuenta;
    }
}