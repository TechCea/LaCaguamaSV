namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormInventarioExtrasAdmin
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
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.gbDatosExtras = new System.Windows.Forms.GroupBox();
            this.cbb_unidad = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.cbxDisponibilidad = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxProveedor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantida = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblPrecioExtraU = new System.Windows.Forms.Label();
            this.lblSeleccionExtra = new System.Windows.Forms.Label();
            this.dgvInventarioE = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.gbDatosExtras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventarioE)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.Location = new System.Drawing.Point(1054, 572);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(141, 54);
            this.btnAgregar.TabIndex = 22;
            this.btnAgregar.Text = "Agregar ";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActualizar.Location = new System.Drawing.Point(1229, 575);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(141, 54);
            this.btnActualizar.TabIndex = 21;
            this.btnActualizar.Text = "Actualizar datos";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.Snow;
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegresar.Location = new System.Drawing.Point(50, 575);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(177, 47);
            this.btnRegresar.TabIndex = 20;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // gbDatosExtras
            // 
            this.gbDatosExtras.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gbDatosExtras.Controls.Add(this.cbb_unidad);
            this.gbDatosExtras.Controls.Add(this.label6);
            this.gbDatosExtras.Controls.Add(this.btnLimpiar);
            this.gbDatosExtras.Controls.Add(this.cbxDisponibilidad);
            this.gbDatosExtras.Controls.Add(this.label3);
            this.gbDatosExtras.Controls.Add(this.cbxProveedor);
            this.gbDatosExtras.Controls.Add(this.label4);
            this.gbDatosExtras.Controls.Add(this.label2);
            this.gbDatosExtras.Controls.Add(this.txtPrecio);
            this.gbDatosExtras.Controls.Add(this.label1);
            this.gbDatosExtras.Controls.Add(this.txtCantida);
            this.gbDatosExtras.Controls.Add(this.txtNombre);
            this.gbDatosExtras.Controls.Add(this.lblPrecioExtraU);
            this.gbDatosExtras.Controls.Add(this.lblSeleccionExtra);
            this.gbDatosExtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gbDatosExtras.ForeColor = System.Drawing.SystemColors.Control;
            this.gbDatosExtras.Location = new System.Drawing.Point(809, 87);
            this.gbDatosExtras.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosExtras.Name = "gbDatosExtras";
            this.gbDatosExtras.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosExtras.Size = new System.Drawing.Size(561, 458);
            this.gbDatosExtras.TabIndex = 19;
            this.gbDatosExtras.TabStop = false;
            this.gbDatosExtras.Text = "Datos extras";
            // 
            // cbb_unidad
            // 
            this.cbb_unidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbb_unidad.FormattingEnabled = true;
            this.cbb_unidad.Location = new System.Drawing.Point(282, 229);
            this.cbb_unidad.Margin = new System.Windows.Forms.Padding(4);
            this.cbb_unidad.Name = "cbb_unidad";
            this.cbb_unidad.Size = new System.Drawing.Size(253, 28);
            this.cbb_unidad.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(35, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 20);
            this.label6.TabIndex = 24;
            this.label6.Text = "Unidad de medida:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Snow;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLimpiar.Location = new System.Drawing.Point(358, 388);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(177, 47);
            this.btnLimpiar.TabIndex = 23;
            this.btnLimpiar.Text = "Limpiar campos";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // cbxDisponibilidad
            // 
            this.cbxDisponibilidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxDisponibilidad.FormattingEnabled = true;
            this.cbxDisponibilidad.Location = new System.Drawing.Point(282, 328);
            this.cbxDisponibilidad.Margin = new System.Windows.Forms.Padding(4);
            this.cbxDisponibilidad.Name = "cbxDisponibilidad";
            this.cbxDisponibilidad.Size = new System.Drawing.Size(253, 28);
            this.cbxDisponibilidad.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(35, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Disponibilidad:";
            // 
            // cbxProveedor
            // 
            this.cbxProveedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxProveedor.FormattingEnabled = true;
            this.cbxProveedor.Location = new System.Drawing.Point(282, 280);
            this.cbxProveedor.Margin = new System.Windows.Forms.Padding(4);
            this.cbxProveedor.Name = "cbxProveedor";
            this.cbxProveedor.Size = new System.Drawing.Size(253, 28);
            this.cbxProveedor.TabIndex = 19;
            this.cbxProveedor.SelectedIndexChanged += new System.EventHandler(this.cbxProveedor_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(35, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Nombre del extra";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(35, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Proveedor:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(282, 185);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(253, 26);
            this.txtPrecio.TabIndex = 15;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(35, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Precio:";
            // 
            // txtCantida
            // 
            this.txtCantida.Location = new System.Drawing.Point(282, 141);
            this.txtCantida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCantida.Name = "txtCantida";
            this.txtCantida.Size = new System.Drawing.Size(253, 26);
            this.txtCantida.TabIndex = 13;
            this.txtCantida.TextChanged += new System.EventHandler(this.txtCantida_TextChanged);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(282, 98);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(253, 26);
            this.txtNombre.TabIndex = 12;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // lblPrecioExtraU
            // 
            this.lblPrecioExtraU.AutoSize = true;
            this.lblPrecioExtraU.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPrecioExtraU.Location = new System.Drawing.Point(35, 141);
            this.lblPrecioExtraU.Name = "lblPrecioExtraU";
            this.lblPrecioExtraU.Size = new System.Drawing.Size(180, 20);
            this.lblPrecioExtraU.TabIndex = 11;
            this.lblPrecioExtraU.Text = "Cantidad en inventario:";
            // 
            // lblSeleccionExtra
            // 
            this.lblSeleccionExtra.AutoSize = true;
            this.lblSeleccionExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionExtra.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccionExtra.Location = new System.Drawing.Point(35, 41);
            this.lblSeleccionExtra.Name = "lblSeleccionExtra";
            this.lblSeleccionExtra.Size = new System.Drawing.Size(287, 20);
            this.lblSeleccionExtra.TabIndex = 8;
            this.lblSeleccionExtra.Text = "Selecciona un registro a editar/borrar";
            // 
            // dgvInventarioE
            // 
            this.dgvInventarioE.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvInventarioE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventarioE.Location = new System.Drawing.Point(50, 87);
            this.dgvInventarioE.Margin = new System.Windows.Forms.Padding(4);
            this.dgvInventarioE.Name = "dgvInventarioE";
            this.dgvInventarioE.RowHeadersWidth = 51;
            this.dgvInventarioE.Size = new System.Drawing.Size(733, 458);
            this.dgvInventarioE.TabIndex = 18;
            this.dgvInventarioE.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventarioE_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(45, 33);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 26);
            this.label5.TabIndex = 23;
            this.label5.Text = "INVENTARIO: EXTRAS";
            // 
            // FormInventarioExtrasAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1403, 663);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.gbDatosExtras);
            this.Controls.Add(this.dgvInventarioE);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormInventarioExtrasAdmin";
            this.Text = "Extras del inventario";
            this.gbDatosExtras.ResumeLayout(false);
            this.gbDatosExtras.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventarioE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.GroupBox gbDatosExtras;
        private System.Windows.Forms.ComboBox cbxProveedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantida;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPrecioExtraU;
        private System.Windows.Forms.Label lblSeleccionExtra;
        private System.Windows.Forms.DataGridView dgvInventarioE;
        private System.Windows.Forms.ComboBox cbxDisponibilidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbb_unidad;
        private System.Windows.Forms.Label label6;
    }
}