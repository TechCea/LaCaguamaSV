namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormInventarioBebidasAdmin
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
            this.dgvInventarioB = new System.Windows.Forms.DataGridView();
            this.gbDatosExtras = new System.Windows.Forms.GroupBox();
            this.cbxCategoria = new System.Windows.Forms.ComboBox();
            this.cbxProveedor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantida = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblPrecioExtraU = new System.Windows.Forms.Label();
            this.lblSeleccionExtra = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventarioB)).BeginInit();
            this.gbDatosExtras.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvInventarioB
            // 
            this.dgvInventarioB.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvInventarioB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventarioB.Location = new System.Drawing.Point(27, 24);
            this.dgvInventarioB.Name = "dgvInventarioB";
            this.dgvInventarioB.Size = new System.Drawing.Size(410, 372);
            this.dgvInventarioB.TabIndex = 0;
            this.dgvInventarioB.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventarioB_CellContentClick);
            // 
            // gbDatosExtras
            // 
            this.gbDatosExtras.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gbDatosExtras.Controls.Add(this.cbxCategoria);
            this.gbDatosExtras.Controls.Add(this.cbxProveedor);
            this.gbDatosExtras.Controls.Add(this.label4);
            this.gbDatosExtras.Controls.Add(this.label3);
            this.gbDatosExtras.Controls.Add(this.label2);
            this.gbDatosExtras.Controls.Add(this.txtPrecio);
            this.gbDatosExtras.Controls.Add(this.label1);
            this.gbDatosExtras.Controls.Add(this.txtCantida);
            this.gbDatosExtras.Controls.Add(this.txtNombre);
            this.gbDatosExtras.Controls.Add(this.lblPrecioExtraU);
            this.gbDatosExtras.Controls.Add(this.lblSeleccionExtra);
            this.gbDatosExtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gbDatosExtras.ForeColor = System.Drawing.SystemColors.Control;
            this.gbDatosExtras.Location = new System.Drawing.Point(458, 24);
            this.gbDatosExtras.Margin = new System.Windows.Forms.Padding(2);
            this.gbDatosExtras.Name = "gbDatosExtras";
            this.gbDatosExtras.Padding = new System.Windows.Forms.Padding(2);
            this.gbDatosExtras.Size = new System.Drawing.Size(391, 372);
            this.gbDatosExtras.TabIndex = 11;
            this.gbDatosExtras.TabStop = false;
            this.gbDatosExtras.Text = "Datos Bebidas";
            // 
            // cbxCategoria
            // 
            this.cbxCategoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxCategoria.FormattingEnabled = true;
            this.cbxCategoria.Location = new System.Drawing.Point(175, 319);
            this.cbxCategoria.Name = "cbxCategoria";
            this.cbxCategoria.Size = new System.Drawing.Size(191, 24);
            this.cbxCategoria.TabIndex = 20;
            this.cbxCategoria.SelectedIndexChanged += new System.EventHandler(this.cbxCategoria_SelectedIndexChanged);
            // 
            // cbxProveedor
            // 
            this.cbxProveedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxProveedor.FormattingEnabled = true;
            this.cbxProveedor.Location = new System.Drawing.Point(175, 261);
            this.cbxProveedor.Name = "cbxProveedor";
            this.cbxProveedor.Size = new System.Drawing.Size(191, 24);
            this.cbxProveedor.TabIndex = 19;
            this.cbxProveedor.SelectedIndexChanged += new System.EventHandler(this.cbxProveedor_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(25, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Nombre de la bebida";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(25, 326);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Categoria:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(25, 268);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Proveedor:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(175, 198);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(191, 23);
            this.txtPrecio.TabIndex = 15;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(25, 201);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Precio:";
            // 
            // txtCantida
            // 
            this.txtCantida.Location = new System.Drawing.Point(175, 143);
            this.txtCantida.Margin = new System.Windows.Forms.Padding(2);
            this.txtCantida.Name = "txtCantida";
            this.txtCantida.Size = new System.Drawing.Size(191, 23);
            this.txtCantida.TabIndex = 13;
            this.txtCantida.TextChanged += new System.EventHandler(this.txtCantida_TextChanged);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(175, 87);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(191, 23);
            this.txtNombre.TabIndex = 12;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // lblPrecioExtraU
            // 
            this.lblPrecioExtraU.AutoSize = true;
            this.lblPrecioExtraU.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPrecioExtraU.Location = new System.Drawing.Point(25, 148);
            this.lblPrecioExtraU.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrecioExtraU.Name = "lblPrecioExtraU";
            this.lblPrecioExtraU.Size = new System.Drawing.Size(68, 17);
            this.lblPrecioExtraU.TabIndex = 11;
            this.lblPrecioExtraU.Text = "Cantidad:";
            // 
            // lblSeleccionExtra
            // 
            this.lblSeleccionExtra.AutoSize = true;
            this.lblSeleccionExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionExtra.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccionExtra.Location = new System.Drawing.Point(25, 43);
            this.lblSeleccionExtra.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeleccionExtra.Name = "lblSeleccionExtra";
            this.lblSeleccionExtra.Size = new System.Drawing.Size(244, 17);
            this.lblSeleccionExtra.TabIndex = 8;
            this.lblSeleccionExtra.Text = "Selecciona un registro a editar/borrar";
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.Snow;
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegresar.Location = new System.Drawing.Point(27, 401);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(133, 38);
            this.btnRegresar.TabIndex = 12;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.Location = new System.Drawing.Point(460, 400);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(101, 51);
            this.btnAgregar.TabIndex = 16;
            this.btnAgregar.Text = "Agregar ";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActualizar.Location = new System.Drawing.Point(597, 403);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(106, 44);
            this.btnActualizar.TabIndex = 14;
            this.btnActualizar.Text = "Actualizar datos";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminar.Location = new System.Drawing.Point(754, 401);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(95, 48);
            this.btnEliminar.TabIndex = 17;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // FormInventarioBebidasAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(875, 469);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.gbDatosExtras);
            this.Controls.Add(this.dgvInventarioB);
            this.Name = "FormInventarioBebidasAdmin";
            this.Text = "FormInventarioBebidasAdmin";
            this.Load += new System.EventHandler(this.FormInventarioBebidasAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventarioB)).EndInit();
            this.gbDatosExtras.ResumeLayout(false);
            this.gbDatosExtras.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInventarioB;
        private System.Windows.Forms.GroupBox gbDatosExtras;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantida;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPrecioExtraU;
        private System.Windows.Forms.Label lblSeleccionExtra;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxCategoria;
        private System.Windows.Forms.ComboBox cbxProveedor;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
    }
}