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
            this.btnCerrar_Click = new System.Windows.Forms.Button();
            this.comboBoxMesas = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCargarExtras = new System.Windows.Forms.Button();
            this.btnCargarPlatos = new System.Windows.Forms.Button();
            this.btnCargarBebidas = new System.Windows.Forms.Button();
            this.dataGridViewMenu = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanelPedidos = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPagos = new System.Windows.Forms.Button();
            this.btnPromociones = new System.Windows.Forms.Button();
            this.btnImprimirComanda = new System.Windows.Forms.Button();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIdOrden
            // 
            this.lblIdOrden.AutoSize = true;
            this.lblIdOrden.ForeColor = System.Drawing.Color.White;
            this.lblIdOrden.Location = new System.Drawing.Point(11, 5);
            this.lblIdOrden.Name = "lblIdOrden";
            this.lblIdOrden.Size = new System.Drawing.Size(24, 13);
            this.lblIdOrden.TabIndex = 0;
            this.lblIdOrden.Text = "IdO";
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.ForeColor = System.Drawing.Color.White;
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
            this.lblTotal.Location = new System.Drawing.Point(246, 132);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(28, 13);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "0.00";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.BackColor = System.Drawing.Color.AliceBlue;
            this.lblDescuento.Location = new System.Drawing.Point(573, 396);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(35, 13);
            this.lblDescuento.TabIndex = 0;
            this.lblDescuento.Text = "label1";
            this.lblDescuento.Visible = false;
            // 
            // lblFechaOrden
            // 
            this.lblFechaOrden.AutoSize = true;
            this.lblFechaOrden.ForeColor = System.Drawing.Color.White;
            this.lblFechaOrden.Location = new System.Drawing.Point(323, 10);
            this.lblFechaOrden.Name = "lblFechaOrden";
            this.lblFechaOrden.Size = new System.Drawing.Size(43, 13);
            this.lblFechaOrden.TabIndex = 0;
            this.lblFechaOrden.Text = "______";
            // 
            // lblTipoPago
            // 
            this.lblTipoPago.AutoSize = true;
            this.lblTipoPago.Location = new System.Drawing.Point(573, 476);
            this.lblTipoPago.Name = "lblTipoPago";
            this.lblTipoPago.Size = new System.Drawing.Size(35, 13);
            this.lblTipoPago.TabIndex = 0;
            this.lblTipoPago.Text = "label1";
            this.lblTipoPago.Visible = false;
            // 
            // lblEstadoOrden
            // 
            this.lblEstadoOrden.AutoSize = true;
            this.lblEstadoOrden.ForeColor = System.Drawing.Color.White;
            this.lblEstadoOrden.Location = new System.Drawing.Point(400, 9);
            this.lblEstadoOrden.Name = "lblEstadoOrden";
            this.lblEstadoOrden.Size = new System.Drawing.Size(37, 13);
            this.lblEstadoOrden.TabIndex = 0;
            this.lblEstadoOrden.Text = "_____";
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNombreUsuario.Location = new System.Drawing.Point(229, 9);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(37, 13);
            this.lblNombreUsuario.TabIndex = 0;
            this.lblNombreUsuario.Text = "_____";
            this.lblNombreUsuario.Click += new System.EventHandler(this.lblNombreUsuario_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Controls.Add(this.btnCerrar_Click);
            this.panel5.Controls.Add(this.comboBoxMesas);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.lblNombreUsuario);
            this.panel5.Controls.Add(this.lblEstadoOrden);
            this.panel5.Controls.Add(this.lblFechaOrden);
            this.panel5.Location = new System.Drawing.Point(370, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(590, 33);
            this.panel5.TabIndex = 11;
            // 
            // btnCerrar_Click
            // 
            this.btnCerrar_Click.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnCerrar_Click.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar_Click.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar_Click.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCerrar_Click.Location = new System.Drawing.Point(544, 5);
            this.btnCerrar_Click.Name = "btnCerrar_Click";
            this.btnCerrar_Click.Size = new System.Drawing.Size(43, 23);
            this.btnCerrar_Click.TabIndex = 35;
            this.btnCerrar_Click.Text = "X";
            this.btnCerrar_Click.UseVisualStyleBackColor = false;
            this.btnCerrar_Click.Click += new System.EventHandler(this.btnCerrar_Click_Click);
            // 
            // comboBoxMesas
            // 
            this.comboBoxMesas.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(186, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Cajero";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mesa";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblIdOrden);
            this.panel3.Controls.Add(this.lblNombreCliente);
            this.panel3.Location = new System.Drawing.Point(1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(363, 22);
            this.panel3.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(47, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Cuenta";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Location = new System.Drawing.Point(1, 275);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 173);
            this.panel1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(66, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Subtotal";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(470, 435);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Total";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(470, 396);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Descuentos";
            this.label6.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.AliceBlue;
            this.label1.Location = new System.Drawing.Point(573, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // btnCargarExtras
            // 
            this.btnCargarExtras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnCargarExtras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargarExtras.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCargarExtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarExtras.ForeColor = System.Drawing.Color.White;
            this.btnCargarExtras.Location = new System.Drawing.Point(491, 47);
            this.btnCargarExtras.Name = "btnCargarExtras";
            this.btnCargarExtras.Size = new System.Drawing.Size(102, 42);
            this.btnCargarExtras.TabIndex = 23;
            this.btnCargarExtras.Text = "EXTRAS";
            this.btnCargarExtras.UseVisualStyleBackColor = false;
            this.btnCargarExtras.Click += new System.EventHandler(this.btnCargarExtras_Click);
            // 
            // btnCargarPlatos
            // 
            this.btnCargarPlatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnCargarPlatos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargarPlatos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCargarPlatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarPlatos.ForeColor = System.Drawing.Color.White;
            this.btnCargarPlatos.Location = new System.Drawing.Point(602, 48);
            this.btnCargarPlatos.Name = "btnCargarPlatos";
            this.btnCargarPlatos.Size = new System.Drawing.Size(102, 41);
            this.btnCargarPlatos.TabIndex = 22;
            this.btnCargarPlatos.Text = "COMIDA";
            this.btnCargarPlatos.UseVisualStyleBackColor = false;
            this.btnCargarPlatos.Click += new System.EventHandler(this.btnCargarPlatos_Click);
            // 
            // btnCargarBebidas
            // 
            this.btnCargarBebidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnCargarBebidas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargarBebidas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCargarBebidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarBebidas.ForeColor = System.Drawing.Color.White;
            this.btnCargarBebidas.Location = new System.Drawing.Point(370, 46);
            this.btnCargarBebidas.Name = "btnCargarBebidas";
            this.btnCargarBebidas.Size = new System.Drawing.Size(102, 42);
            this.btnCargarBebidas.TabIndex = 21;
            this.btnCargarBebidas.Text = "BEBIDAS";
            this.btnCargarBebidas.UseVisualStyleBackColor = false;
            this.btnCargarBebidas.Click += new System.EventHandler(this.btnCargarBebidas_Click);
            // 
            // dataGridViewMenu
            // 
            this.dataGridViewMenu.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridViewMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMenu.Location = new System.Drawing.Point(370, 95);
            this.dataGridViewMenu.MultiSelect = false;
            this.dataGridViewMenu.Name = "dataGridViewMenu";
            this.dataGridViewMenu.ReadOnly = true;
            this.dataGridViewMenu.RowHeadersWidth = 51;
            this.dataGridViewMenu.Size = new System.Drawing.Size(578, 305);
            this.dataGridViewMenu.TabIndex = 32;
            this.dataGridViewMenu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMenu_CellDoubleClick);
            // 
            // flowLayoutPanelPedidos
            // 
            this.flowLayoutPanelPedidos.AutoScroll = true;
            this.flowLayoutPanelPedidos.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanelPedidos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPedidos.Location = new System.Drawing.Point(1, 21);
            this.flowLayoutPanelPedidos.Name = "flowLayoutPanelPedidos";
            this.flowLayoutPanelPedidos.Size = new System.Drawing.Size(363, 360);
            this.flowLayoutPanelPedidos.TabIndex = 33;
            this.flowLayoutPanelPedidos.WrapContents = false;
            this.flowLayoutPanelPedidos.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelPedidos_Paint);
            // 
            // btnPagos
            // 
            this.btnPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnPagos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPagos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagos.ForeColor = System.Drawing.Color.White;
            this.btnPagos.Location = new System.Drawing.Point(846, 46);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Size = new System.Drawing.Size(102, 42);
            this.btnPagos.TabIndex = 35;
            this.btnPagos.Text = "PAGAR";
            this.btnPagos.UseVisualStyleBackColor = false;
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            // 
            // btnPromociones
            // 
            this.btnPromociones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnPromociones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPromociones.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPromociones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPromociones.ForeColor = System.Drawing.Color.White;
            this.btnPromociones.Location = new System.Drawing.Point(720, 48);
            this.btnPromociones.Name = "btnPromociones";
            this.btnPromociones.Size = new System.Drawing.Size(109, 41);
            this.btnPromociones.TabIndex = 36;
            this.btnPromociones.Text = "PROMOS";
            this.btnPromociones.UseVisualStyleBackColor = false;
            this.btnPromociones.Click += new System.EventHandler(this.btnPromociones_Click);
            // 
            // btnImprimirComanda
            // 
            this.btnImprimirComanda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnImprimirComanda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimirComanda.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImprimirComanda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirComanda.ForeColor = System.Drawing.Color.White;
            this.btnImprimirComanda.Location = new System.Drawing.Point(748, 406);
            this.btnImprimirComanda.Name = "btnImprimirComanda";
            this.btnImprimirComanda.Size = new System.Drawing.Size(200, 41);
            this.btnImprimirComanda.TabIndex = 37;
            this.btnImprimirComanda.Text = "Imprimir Comanda";
            this.btnImprimirComanda.UseVisualStyleBackColor = false;
            this.btnImprimirComanda.Click += new System.EventHandler(this.btnImprimirComanda_Click);
            // 
            // FormGestionOrdenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(960, 450);
            this.Controls.Add(this.btnImprimirComanda);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnPromociones);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnPagos);
            this.Controls.Add(this.lblTipoPago);
            this.Controls.Add(this.flowLayoutPanelPedidos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDescuento);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridViewMenu);
            this.Controls.Add(this.btnCargarExtras);
            this.Controls.Add(this.btnCargarPlatos);
            this.Controls.Add(this.btnCargarBebidas);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Name = "FormGestionOrdenes";
            this.Text = "Editar orden";
            this.Load += new System.EventHandler(this.FormGestionOrdenes_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button btnCargarExtras;
        private System.Windows.Forms.Button btnCargarPlatos;
        private System.Windows.Forms.Button btnCargarBebidas;
        private System.Windows.Forms.DataGridView dataGridViewMenu;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPedidos;
        private System.Windows.Forms.ComboBox comboBoxMesas;
        private System.Windows.Forms.Button btnCerrar_Click;
        private System.Windows.Forms.Button btnPagos;
        private System.Windows.Forms.Button btnPromociones;
        private System.Windows.Forms.Button btnImprimirComanda;
    }
}