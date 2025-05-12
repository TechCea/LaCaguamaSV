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
            this.label2 = new System.Windows.Forms.Label();
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
            this.gbDatosExtras.Location = new System.Drawing.Point(694, 91);
            this.gbDatosExtras.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosExtras.Name = "gbDatosExtras";
            this.gbDatosExtras.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosExtras.Size = new System.Drawing.Size(521, 319);
            this.gbDatosExtras.TabIndex = 10;
            this.gbDatosExtras.TabStop = false;
            this.gbDatosExtras.Text = "Datos extras";
            // 
            // txtDirecion
            // 
            this.txtDirecion.Location = new System.Drawing.Point(233, 244);
            this.txtDirecion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDirecion.Name = "txtDirecion";
            this.txtDirecion.Size = new System.Drawing.Size(253, 26);
            this.txtDirecion.TabIndex = 15;
            this.txtDirecion.TextChanged += new System.EventHandler(this.txtDirecion_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(33, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Direccion:";
            // 
            // txtContacto
            // 
            this.txtContacto.Location = new System.Drawing.Point(233, 176);
            this.txtContacto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtContacto.Name = "txtContacto";
            this.txtContacto.Size = new System.Drawing.Size(253, 26);
            this.txtContacto.TabIndex = 13;
            this.txtContacto.TextChanged += new System.EventHandler(this.txtContacto_TextChanged);
            // 
            // txtNombreP
            // 
            this.txtNombreP.Location = new System.Drawing.Point(233, 107);
            this.txtNombreP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombreP.Name = "txtNombreP";
            this.txtNombreP.Size = new System.Drawing.Size(253, 26);
            this.txtNombreP.TabIndex = 12;
            this.txtNombreP.TextChanged += new System.EventHandler(this.txtNombreP_TextChanged);
            // 
            // lblPrecioExtraU
            // 
            this.lblPrecioExtraU.AutoSize = true;
            this.lblPrecioExtraU.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPrecioExtraU.Location = new System.Drawing.Point(33, 182);
            this.lblPrecioExtraU.Name = "lblPrecioExtraU";
            this.lblPrecioExtraU.Size = new System.Drawing.Size(155, 20);
            this.lblPrecioExtraU.TabIndex = 11;
            this.lblPrecioExtraU.Text = "Numero Telefonico:";
            // 
            // lblSeleccionExtra
            // 
            this.lblSeleccionExtra.AutoSize = true;
            this.lblSeleccionExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionExtra.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccionExtra.Location = new System.Drawing.Point(33, 53);
            this.lblSeleccionExtra.Name = "lblSeleccionExtra";
            this.lblSeleccionExtra.Size = new System.Drawing.Size(304, 20);
            this.lblSeleccionExtra.TabIndex = 8;
            this.lblSeleccionExtra.Text = "Selecciona un proveedor a editar/borrar";
            // 
            // lblNombreE
            // 
            this.lblNombreE.AutoSize = true;
            this.lblNombreE.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNombreE.Location = new System.Drawing.Point(33, 111);
            this.lblNombreE.Name = "lblNombreE";
            this.lblNombreE.Size = new System.Drawing.Size(181, 20);
            this.lblNombreE.TabIndex = 10;
            this.lblNombreE.Text = "Nombre del Proveedor:";
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.White;
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegresar.Location = new System.Drawing.Point(54, 509);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(141, 47);
            this.btnRegresar.TabIndex = 9;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // dgvProveedor
            // 
            this.dgvProveedor.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvProveedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProveedor.Location = new System.Drawing.Point(40, 68);
            this.dgvProveedor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvProveedor.Name = "dgvProveedor";
            this.dgvProveedor.RowHeadersWidth = 51;
            this.dgvProveedor.RowTemplate.Height = 24;
            this.dgvProveedor.Size = new System.Drawing.Size(616, 412);
            this.dgvProveedor.TabIndex = 8;
            // 
            // btnEliminarE
            // 
            this.btnEliminarE.BackColor = System.Drawing.Color.Brown;
            this.btnEliminarE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminarE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarE.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminarE.Location = new System.Drawing.Point(1062, 427);
            this.btnEliminarE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEliminarE.Name = "btnEliminarE";
            this.btnEliminarE.Size = new System.Drawing.Size(141, 54);
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
            this.btnActualizarP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActualizarP.Location = new System.Drawing.Point(885, 427);
            this.btnActualizarP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnActualizarP.Name = "btnActualizarP";
            this.btnActualizarP.Size = new System.Drawing.Size(141, 54);
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
            this.btnAgregarP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregarP.Location = new System.Drawing.Point(708, 427);
            this.btnAgregarP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregarP.Name = "btnAgregarP";
            this.btnAgregarP.Size = new System.Drawing.Size(141, 54);
            this.btnAgregarP.TabIndex = 13;
            this.btnAgregarP.Text = "Agregar proveedor";
            this.btnAgregarP.UseVisualStyleBackColor = false;
            this.btnAgregarP.Click += new System.EventHandler(this.btnAgregarP_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(35, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 26);
            this.label2.TabIndex = 14;
            this.label2.Text = "INVENTARIO: PROVEEDORES";
            // 
            // FormGestionarProvAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1275, 583);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAgregarP);
            this.Controls.Add(this.gbDatosExtras);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.dgvProveedor);
            this.Controls.Add(this.btnEliminarE);
            this.Controls.Add(this.btnActualizarP);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormGestionarProvAdmin";
            this.Text = "Proveedores";
            this.gbDatosExtras.ResumeLayout(false);
            this.gbDatosExtras.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label2;
    }
}