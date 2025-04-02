namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    partial class FormUsuarioGestionOrdenes
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
            this.btnPrecuentaU = new System.Windows.Forms.Button();
            this.flowLayoutPanelPedidosU = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTipoPagoU = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDescuentoU = new System.Windows.Forms.Label();
            this.lblTotalU = new System.Windows.Forms.Label();
            this.dataGridViewMenuU = new System.Windows.Forms.DataGridView();
            this.button12 = new System.Windows.Forms.Button();
            this.btnCargarExtrasU = new System.Windows.Forms.Button();
            this.btnCargarPlatosU = new System.Windows.Forms.Button();
            this.btnCargarBebidasU = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.comboBoxMesasU = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNombreUsuarioU = new System.Windows.Forms.Label();
            this.lblEstadoOrdenU = new System.Windows.Forms.Label();
            this.lblFechaOrdenU = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblIdOrdenU = new System.Windows.Forms.Label();
            this.lblNombreClienteU = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenuU)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrecuentaU
            // 
            this.btnPrecuentaU.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPrecuentaU.Location = new System.Drawing.Point(486, 57);
            this.btnPrecuentaU.Name = "btnPrecuentaU";
            this.btnPrecuentaU.Size = new System.Drawing.Size(81, 28);
            this.btnPrecuentaU.TabIndex = 44;
            this.btnPrecuentaU.Text = "PRECUENTA";
            this.btnPrecuentaU.UseVisualStyleBackColor = false;
            this.btnPrecuentaU.Click += new System.EventHandler(this.btnPrecuentaU_Click);
            // 
            // flowLayoutPanelPedidosU
            // 
            this.flowLayoutPanelPedidosU.AutoScroll = true;
            this.flowLayoutPanelPedidosU.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanelPedidosU.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPedidosU.Location = new System.Drawing.Point(2, 23);
            this.flowLayoutPanelPedidosU.Name = "flowLayoutPanelPedidosU";
            this.flowLayoutPanelPedidosU.Size = new System.Drawing.Size(207, 257);
            this.flowLayoutPanelPedidosU.TabIndex = 43;
            this.flowLayoutPanelPedidosU.WrapContents = false;
            this.flowLayoutPanelPedidosU.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelPedidosU_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblTipoPagoU);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblDescuentoU);
            this.panel1.Controls.Add(this.lblTotalU);
            this.panel1.Location = new System.Drawing.Point(2, 277);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 173);
            this.panel1.TabIndex = 37;
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
            // lblTipoPagoU
            // 
            this.lblTipoPagoU.AutoSize = true;
            this.lblTipoPagoU.Location = new System.Drawing.Point(115, 131);
            this.lblTipoPagoU.Name = "lblTipoPagoU";
            this.lblTipoPagoU.Size = new System.Drawing.Size(35, 13);
            this.lblTipoPagoU.TabIndex = 0;
            this.lblTipoPagoU.Text = "label1";
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
            // lblDescuentoU
            // 
            this.lblDescuentoU.AutoSize = true;
            this.lblDescuentoU.BackColor = System.Drawing.Color.AliceBlue;
            this.lblDescuentoU.Location = new System.Drawing.Point(115, 51);
            this.lblDescuentoU.Name = "lblDescuentoU";
            this.lblDescuentoU.Size = new System.Drawing.Size(35, 13);
            this.lblDescuentoU.TabIndex = 0;
            this.lblDescuentoU.Text = "label1";
            // 
            // lblTotalU
            // 
            this.lblTotalU.AutoSize = true;
            this.lblTotalU.BackColor = System.Drawing.Color.AliceBlue;
            this.lblTotalU.Location = new System.Drawing.Point(115, 21);
            this.lblTotalU.Name = "lblTotalU";
            this.lblTotalU.Size = new System.Drawing.Size(35, 13);
            this.lblTotalU.TabIndex = 0;
            this.lblTotalU.Text = "label1";
            // 
            // dataGridViewMenuU
            // 
            this.dataGridViewMenuU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMenuU.Location = new System.Drawing.Point(215, 91);
            this.dataGridViewMenuU.MultiSelect = false;
            this.dataGridViewMenuU.Name = "dataGridViewMenuU";
            this.dataGridViewMenuU.ReadOnly = true;
            this.dataGridViewMenuU.RowHeadersWidth = 51;
            this.dataGridViewMenuU.Size = new System.Drawing.Size(533, 305);
            this.dataGridViewMenuU.TabIndex = 42;
            this.dataGridViewMenuU.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMenuU_CellContentClick);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button12.Location = new System.Drawing.Point(573, 57);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(81, 28);
            this.button12.TabIndex = 41;
            this.button12.Text = "PAGAR";
            this.button12.UseVisualStyleBackColor = false;
            // 
            // btnCargarExtrasU
            // 
            this.btnCargarExtrasU.BackColor = System.Drawing.Color.Yellow;
            this.btnCargarExtrasU.Location = new System.Drawing.Point(398, 56);
            this.btnCargarExtrasU.Name = "btnCargarExtrasU";
            this.btnCargarExtrasU.Size = new System.Drawing.Size(81, 28);
            this.btnCargarExtrasU.TabIndex = 40;
            this.btnCargarExtrasU.Text = "EXTRAS";
            this.btnCargarExtrasU.UseVisualStyleBackColor = false;
            this.btnCargarExtrasU.Click += new System.EventHandler(this.btnCargarExtrasU_Click_1);
            // 
            // btnCargarPlatosU
            // 
            this.btnCargarPlatosU.BackColor = System.Drawing.Color.Yellow;
            this.btnCargarPlatosU.Location = new System.Drawing.Point(311, 57);
            this.btnCargarPlatosU.Name = "btnCargarPlatosU";
            this.btnCargarPlatosU.Size = new System.Drawing.Size(81, 28);
            this.btnCargarPlatosU.TabIndex = 39;
            this.btnCargarPlatosU.Text = "COMIDA";
            this.btnCargarPlatosU.UseVisualStyleBackColor = false;
            this.btnCargarPlatosU.Click += new System.EventHandler(this.btnCargarPlatosU_Click_1);
            // 
            // btnCargarBebidasU
            // 
            this.btnCargarBebidasU.BackColor = System.Drawing.Color.Yellow;
            this.btnCargarBebidasU.Location = new System.Drawing.Point(220, 56);
            this.btnCargarBebidasU.Name = "btnCargarBebidasU";
            this.btnCargarBebidasU.Size = new System.Drawing.Size(85, 29);
            this.btnCargarBebidasU.TabIndex = 38;
            this.btnCargarBebidasU.Text = "BEBIDAS";
            this.btnCargarBebidasU.UseVisualStyleBackColor = false;
            this.btnCargarBebidasU.Click += new System.EventHandler(this.btnCargarBebidasU_Click_1);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Controls.Add(this.comboBoxMesasU);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.lblNombreUsuarioU);
            this.panel5.Controls.Add(this.lblEstadoOrdenU);
            this.panel5.Controls.Add(this.lblFechaOrdenU);
            this.panel5.Location = new System.Drawing.Point(209, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(590, 33);
            this.panel5.TabIndex = 36;
            // 
            // comboBoxMesasU
            // 
            this.comboBoxMesasU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMesasU.FormattingEnabled = true;
            this.comboBoxMesasU.Location = new System.Drawing.Point(38, 6);
            this.comboBoxMesasU.Name = "comboBoxMesasU";
            this.comboBoxMesasU.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMesasU.TabIndex = 2;
            this.comboBoxMesasU.SelectedIndexChanged += new System.EventHandler(this.comboBoxMesasU_SelectedIndexChanged);
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
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblNombreUsuarioU
            // 
            this.lblNombreUsuarioU.AutoSize = true;
            this.lblNombreUsuarioU.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNombreUsuarioU.Location = new System.Drawing.Point(229, 9);
            this.lblNombreUsuarioU.Name = "lblNombreUsuarioU";
            this.lblNombreUsuarioU.Size = new System.Drawing.Size(35, 13);
            this.lblNombreUsuarioU.TabIndex = 0;
            this.lblNombreUsuarioU.Text = "label1";
            // 
            // lblEstadoOrdenU
            // 
            this.lblEstadoOrdenU.AutoSize = true;
            this.lblEstadoOrdenU.Location = new System.Drawing.Point(398, 12);
            this.lblEstadoOrdenU.Name = "lblEstadoOrdenU";
            this.lblEstadoOrdenU.Size = new System.Drawing.Size(35, 13);
            this.lblEstadoOrdenU.TabIndex = 0;
            this.lblEstadoOrdenU.Text = "label1";
            // 
            // lblFechaOrdenU
            // 
            this.lblFechaOrdenU.AutoSize = true;
            this.lblFechaOrdenU.Location = new System.Drawing.Point(323, 10);
            this.lblFechaOrdenU.Name = "lblFechaOrdenU";
            this.lblFechaOrdenU.Size = new System.Drawing.Size(35, 13);
            this.lblFechaOrdenU.TabIndex = 0;
            this.lblFechaOrdenU.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblIdOrdenU);
            this.panel3.Controls.Add(this.lblNombreClienteU);
            this.panel3.Location = new System.Drawing.Point(2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 24);
            this.panel3.TabIndex = 35;
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
            // lblIdOrdenU
            // 
            this.lblIdOrdenU.AutoSize = true;
            this.lblIdOrdenU.Location = new System.Drawing.Point(11, 5);
            this.lblIdOrdenU.Name = "lblIdOrdenU";
            this.lblIdOrdenU.Size = new System.Drawing.Size(24, 13);
            this.lblIdOrdenU.TabIndex = 0;
            this.lblIdOrdenU.Text = "IdO";
            // 
            // lblNombreClienteU
            // 
            this.lblNombreClienteU.AutoSize = true;
            this.lblNombreClienteU.Location = new System.Drawing.Point(94, 5);
            this.lblNombreClienteU.Name = "lblNombreClienteU";
            this.lblNombreClienteU.Size = new System.Drawing.Size(29, 13);
            this.lblNombreClienteU.TabIndex = 0;
            this.lblNombreClienteU.Text = "label";
            // 
            // FormUsuarioGestionOrdenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPrecuentaU);
            this.Controls.Add(this.flowLayoutPanelPedidosU);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridViewMenuU);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.btnCargarExtrasU);
            this.Controls.Add(this.btnCargarPlatosU);
            this.Controls.Add(this.btnCargarBebidasU);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Name = "FormUsuarioGestionOrdenes";
            this.Text = "FormUsuarioGestionOrdenes";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenuU)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrecuentaU;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPedidosU;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTipoPagoU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDescuentoU;
        private System.Windows.Forms.Label lblTotalU;
        private System.Windows.Forms.DataGridView dataGridViewMenuU;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button btnCargarExtrasU;
        private System.Windows.Forms.Button btnCargarPlatosU;
        private System.Windows.Forms.Button btnCargarBebidasU;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox comboBoxMesasU;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNombreUsuarioU;
        private System.Windows.Forms.Label lblEstadoOrdenU;
        private System.Windows.Forms.Label lblFechaOrdenU;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblIdOrdenU;
        private System.Windows.Forms.Label lblNombreClienteU;
    }
}