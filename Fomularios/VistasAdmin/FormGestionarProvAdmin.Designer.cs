namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormGestionarProvAdmin
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
            this.gbDatosExtras = new System.Windows.Forms.GroupBox();
            this.txtDirecion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContacto = new System.Windows.Forms.TextBox();
            this.txtNombreP = new System.Windows.Forms.TextBox();
            this.lblPrecioExtraU = new System.Windows.Forms.Label();
            this.lblSeleccionExtra = new System.Windows.Forms.Label();
            this.lblNombreE = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.dgvProveedor = new System.Windows.Forms.DataGridView();
            this.btnEliminarE = new System.Windows.Forms.Button();
            this.btnActualizarP = new System.Windows.Forms.Button();
            this.btnAgregarP = new System.Windows.Forms.Button();
            this.gbDatosExtras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedor)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDatosExtras
            // 
            this.gbDatosExtras.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gbDatosExtras.Controls.Add(this.txtDirecion);
            this.gbDatosExtras.Controls.Add(this.label1);
            this.gbDatosExtras.Controls.Add(this.txtContacto);
            this.gbDatosExtras.Controls.Add(this.txtNombreP);
            this.gbDatosExtras.Controls.Add(this.lblPrecioExtraU);
            this.gbDatosExtras.Controls.Add(this.lblSeleccionExtra);
            this.gbDatosExtras.Controls.Add(this.lblNombreE);
            this.gbDatosExtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gbDatosExtras.ForeColor = System.Drawing.SystemColors.Control;
            this.gbDatosExtras.Location = new System.Drawing.Point(520, 33);
            this.gbDatosExtras.Margin = new System.Windows.Forms.Padding(2);
            this.gbDatosExtras.Name = "gbDatosExtras";
            this.gbDatosExtras.Padding = new System.Windows.Forms.Padding(2);
            this.gbDatosExtras.Size = new System.Drawing.Size(391, 259);
            this.gbDatosExtras.TabIndex = 10;
            this.gbDatosExtras.TabStop = false;
            this.gbDatosExtras.Text = "Datos extras";
            // 
            // txtDirecion
            // 
            this.txtDirecion.Location = new System.Drawing.Point(175, 198);
            this.txtDirecion.Margin = new System.Windows.Forms.Padding(2);
            this.txtDirecion.Name = "txtDirecion";
            this.txtDirecion.Size = new System.Drawing.Size(191, 23);
            this.txtDirecion.TabIndex = 15;
            this.txtDirecion.TextChanged += new System.EventHandler(this.txtDirecion_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(25, 201);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Direccion:";
            // 
            // txtContacto
            // 
            this.txtContacto.Location = new System.Drawing.Point(175, 143);
            this.txtContacto.Margin = new System.Windows.Forms.Padding(2);
            this.txtContacto.Name = "txtContacto";
            this.txtContacto.Size = new System.Drawing.Size(191, 23);
            this.txtContacto.TabIndex = 13;
            this.txtContacto.TextChanged += new System.EventHandler(this.txtContacto_TextChanged);
            // 
            // txtNombreP
            // 
            this.txtNombreP.Location = new System.Drawing.Point(175, 87);
            this.txtNombreP.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombreP.Name = "txtNombreP";
            this.txtNombreP.Size = new System.Drawing.Size(191, 23);
            this.txtNombreP.TabIndex = 12;
            this.txtNombreP.TextChanged += new System.EventHandler(this.txtNombreP_TextChanged);
            // 
            // lblPrecioExtraU
            // 
            this.lblPrecioExtraU.AutoSize = true;
            this.lblPrecioExtraU.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPrecioExtraU.Location = new System.Drawing.Point(25, 148);
            this.lblPrecioExtraU.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrecioExtraU.Name = "lblPrecioExtraU";
            this.lblPrecioExtraU.Size = new System.Drawing.Size(132, 17);
            this.lblPrecioExtraU.TabIndex = 11;
            this.lblPrecioExtraU.Text = "Numero Telefonico:";
            // 
            // lblSeleccionExtra
            // 
            this.lblSeleccionExtra.AutoSize = true;
            this.lblSeleccionExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionExtra.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccionExtra.Location = new System.Drawing.Point(25, 43);
            this.lblSeleccionExtra.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeleccionExtra.Name = "lblSeleccionExtra";
            this.lblSeleccionExtra.Size = new System.Drawing.Size(261, 17);
            this.lblSeleccionExtra.TabIndex = 8;
            this.lblSeleccionExtra.Text = "Selecciona un proveedor a editar/borrar";
            // 
            // lblNombreE
            // 
            this.lblNombreE.AutoSize = true;
            this.lblNombreE.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNombreE.Location = new System.Drawing.Point(25, 90);
            this.lblNombreE.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreE.Name = "lblNombreE";
            this.lblNombreE.Size = new System.Drawing.Size(155, 17);
            this.lblNombreE.TabIndex = 10;
            this.lblNombreE.Text = "Nombre del Proveedor:";
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.White;
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegresar.Location = new System.Drawing.Point(40, 373);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(106, 38);
            this.btnRegresar.TabIndex = 9;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // dgvProveedor
            // 
            this.dgvProveedor.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvProveedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProveedor.Location = new System.Drawing.Point(29, 15);
            this.dgvProveedor.Margin = new System.Windows.Forms.Padding(2);
            this.dgvProveedor.Name = "dgvProveedor";
            this.dgvProveedor.RowHeadersWidth = 51;
            this.dgvProveedor.RowTemplate.Height = 24;
            this.dgvProveedor.Size = new System.Drawing.Size(462, 335);
            this.dgvProveedor.TabIndex = 8;
            // 
            // btnEliminarE
            // 
            this.btnEliminarE.BackColor = System.Drawing.Color.Brown;
            this.btnEliminarE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminarE.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEliminarE.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminarE.Location = new System.Drawing.Point(796, 306);
            this.btnEliminarE.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarE.Name = "btnEliminarE";
            this.btnEliminarE.Size = new System.Drawing.Size(106, 44);
            this.btnEliminarE.TabIndex = 12;
            this.btnEliminarE.Text = "Eliminar";
            this.btnEliminarE.UseVisualStyleBackColor = false;
            this.btnEliminarE.Click += new System.EventHandler(this.btnEliminarE_Click);
            // 
            // btnActualizarP
            // 
            this.btnActualizarP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnActualizarP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizarP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnActualizarP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActualizarP.Location = new System.Drawing.Point(663, 306);
            this.btnActualizarP.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualizarP.Name = "btnActualizarP";
            this.btnActualizarP.Size = new System.Drawing.Size(106, 44);
            this.btnActualizarP.TabIndex = 11;
            this.btnActualizarP.Text = "Actualizar datos";
            this.btnActualizarP.UseVisualStyleBackColor = false;
            this.btnActualizarP.Click += new System.EventHandler(this.btnActualizarP_Click);
            // 
            // btnAgregarP
            // 
            this.btnAgregarP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.btnAgregarP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregarP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregarP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregarP.Location = new System.Drawing.Point(530, 306);
            this.btnAgregarP.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarP.Name = "btnAgregarP";
            this.btnAgregarP.Size = new System.Drawing.Size(106, 44);
            this.btnAgregarP.TabIndex = 13;
            this.btnAgregarP.Text = "Agregar proveedor";
            this.btnAgregarP.UseVisualStyleBackColor = false;
            this.btnAgregarP.Click += new System.EventHandler(this.btnAgregarP_Click);
            // 
            // FormGestionarProvAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(956, 474);
            this.Controls.Add(this.btnAgregarP);
            this.Controls.Add(this.gbDatosExtras);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.dgvProveedor);
            this.Controls.Add(this.btnEliminarE);
            this.Controls.Add(this.btnActualizarP);
            this.Name = "FormGestionarProvAdmin";
            this.Text = "FormGestionarProvAdmin";
            this.Load += new System.EventHandler(this.FormGestionarProvAdmin_Load);
            this.gbDatosExtras.ResumeLayout(false);
            this.gbDatosExtras.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatosExtras;
        private System.Windows.Forms.TextBox txtContacto;
        private System.Windows.Forms.TextBox txtNombreP;
        private System.Windows.Forms.Label lblPrecioExtraU;
        private System.Windows.Forms.Label lblSeleccionExtra;
        private System.Windows.Forms.Label lblNombreE;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.DataGridView dgvProveedor;
        private System.Windows.Forms.Button btnEliminarE;
        private System.Windows.Forms.Button btnActualizarP;
        private System.Windows.Forms.TextBox txtDirecion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregarP;
    }
}