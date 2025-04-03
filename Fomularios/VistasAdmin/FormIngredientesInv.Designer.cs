namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormIngredientesInv
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
            this.gbDatosIngrediente = new System.Windows.Forms.GroupBox();
            this.btnAgregarIng = new System.Windows.Forms.Button();
            this.lblSeleccion = new System.Windows.Forms.Label();
            this.cbProveedores = new System.Windows.Forms.ComboBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnActualizarB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreC = new System.Windows.Forms.TextBox();
            this.lbNombreBebida = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnRegresarMenu = new System.Windows.Forms.Button();
            this.cbFiltrarProv = new System.Windows.Forms.ComboBox();
            this.dgvIngredientes = new System.Windows.Forms.DataGridView();
            this.btncrearPlato = new System.Windows.Forms.Button();
            this.gbDatosIngrediente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngredientes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDatosIngrediente
            // 
            this.gbDatosIngrediente.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gbDatosIngrediente.Controls.Add(this.btnAgregarIng);
            this.gbDatosIngrediente.Controls.Add(this.lblSeleccion);
            this.gbDatosIngrediente.Controls.Add(this.btnEliminar);
            this.gbDatosIngrediente.Controls.Add(this.cbProveedores);
            this.gbDatosIngrediente.Controls.Add(this.txtCantidad);
            this.gbDatosIngrediente.Controls.Add(this.label2);
            this.gbDatosIngrediente.Controls.Add(this.btnActualizarB);
            this.gbDatosIngrediente.Controls.Add(this.label1);
            this.gbDatosIngrediente.Controls.Add(this.txtNombreC);
            this.gbDatosIngrediente.Controls.Add(this.lbNombreBebida);
            this.gbDatosIngrediente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gbDatosIngrediente.ForeColor = System.Drawing.SystemColors.Control;
            this.gbDatosIngrediente.Location = new System.Drawing.Point(524, 63);
            this.gbDatosIngrediente.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbDatosIngrediente.Name = "gbDatosIngrediente";
            this.gbDatosIngrediente.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbDatosIngrediente.Size = new System.Drawing.Size(364, 324);
            this.gbDatosIngrediente.TabIndex = 11;
            this.gbDatosIngrediente.TabStop = false;
            this.gbDatosIngrediente.Text = "Datos Ingrediente";
            // 
            // btnAgregarIng
            // 
            this.btnAgregarIng.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.btnAgregarIng.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarIng.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregarIng.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAgregarIng.Location = new System.Drawing.Point(25, 260);
            this.btnAgregarIng.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAgregarIng.Name = "btnAgregarIng";
            this.btnAgregarIng.Size = new System.Drawing.Size(118, 49);
            this.btnAgregarIng.TabIndex = 14;
            this.btnAgregarIng.Text = "Agregar ingrediente";
            this.btnAgregarIng.UseVisualStyleBackColor = false;
            this.btnAgregarIng.Click += new System.EventHandler(this.btnAgregarIng_Click);
            // 
            // lblSeleccion
            // 
            this.lblSeleccion.AutoSize = true;
            this.lblSeleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccion.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccion.Location = new System.Drawing.Point(22, 32);
            this.lblSeleccion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeleccion.Name = "lblSeleccion";
            this.lblSeleccion.Size = new System.Drawing.Size(275, 17);
            this.lblSeleccion.TabIndex = 8;
            this.lblSeleccion.Text = "Selecciona una ingrediente a editar/borrar";
            // 
            // cbProveedores
            // 
            this.cbProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbProveedores.FormattingEnabled = true;
            this.cbProveedores.Location = new System.Drawing.Point(159, 192);
            this.cbProveedores.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(171, 24);
            this.cbProveedores.TabIndex = 5;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(159, 141);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(171, 23);
            this.txtCantidad.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(28, 198);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Asignar proveedor:";
            // 
            // btnActualizarB
            // 
            this.btnActualizarB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnActualizarB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnActualizarB.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnActualizarB.Location = new System.Drawing.Point(147, 260);
            this.btnActualizarB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnActualizarB.Name = "btnActualizarB";
            this.btnActualizarB.Size = new System.Drawing.Size(118, 49);
            this.btnActualizarB.TabIndex = 12;
            this.btnActualizarB.Text = "Actualizar datos";
            this.btnActualizarB.UseVisualStyleBackColor = false;
            this.btnActualizarB.Click += new System.EventHandler(this.btnActualizarB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(28, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cantidad a agregar:";
            // 
            // txtNombreC
            // 
            this.txtNombreC.Location = new System.Drawing.Point(159, 85);
            this.txtNombreC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNombreC.Name = "txtNombreC";
            this.txtNombreC.Size = new System.Drawing.Size(171, 23);
            this.txtNombreC.TabIndex = 1;
            // 
            // lbNombreBebida
            // 
            this.lbNombreBebida.AutoSize = true;
            this.lbNombreBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lbNombreBebida.Location = new System.Drawing.Point(28, 90);
            this.lbNombreBebida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNombreBebida.Name = "lbNombreBebida";
            this.lbNombreBebida.Size = new System.Drawing.Size(122, 17);
            this.lbNombreBebida.TabIndex = 0;
            this.lbNombreBebida.Text = "Nombre producto:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Brown;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEliminar.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnEliminar.Location = new System.Drawing.Point(271, 260);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(89, 49);
            this.btnEliminar.TabIndex = 13;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.BackColor = System.Drawing.Color.White;
            this.btnRegresarMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresarMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRegresarMenu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRegresarMenu.Location = new System.Drawing.Point(49, 405);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(190, 41);
            this.btnRegresarMenu.TabIndex = 10;
            this.btnRegresarMenu.Text = "Regresar a inventario";
            this.btnRegresarMenu.UseVisualStyleBackColor = false;
            // 
            // cbFiltrarProv
            // 
            this.cbFiltrarProv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbFiltrarProv.FormattingEnabled = true;
            this.cbFiltrarProv.Location = new System.Drawing.Point(337, 33);
            this.cbFiltrarProv.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbFiltrarProv.Name = "cbFiltrarProv";
            this.cbFiltrarProv.Size = new System.Drawing.Size(139, 21);
            this.cbFiltrarProv.TabIndex = 9;
            // 
            // dgvIngredientes
            // 
            this.dgvIngredientes.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvIngredientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIngredientes.Location = new System.Drawing.Point(49, 63);
            this.dgvIngredientes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvIngredientes.Name = "dgvIngredientes";
            this.dgvIngredientes.RowHeadersWidth = 51;
            this.dgvIngredientes.RowTemplate.Height = 24;
            this.dgvIngredientes.Size = new System.Drawing.Size(436, 335);
            this.dgvIngredientes.TabIndex = 8;
            // 
            // btncrearPlato
            // 
            this.btncrearPlato.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.btncrearPlato.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncrearPlato.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btncrearPlato.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btncrearPlato.Location = new System.Drawing.Point(622, 405);
            this.btncrearPlato.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btncrearPlato.Name = "btncrearPlato";
            this.btncrearPlato.Size = new System.Drawing.Size(190, 41);
            this.btncrearPlato.TabIndex = 14;
            this.btncrearPlato.Text = "Crear platos y recetas";
            this.btncrearPlato.UseVisualStyleBackColor = false;
            this.btncrearPlato.Click += new System.EventHandler(this.btncrearPlato_Click);
            // 
            // FormIngredientesInv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(929, 552);
            this.Controls.Add(this.btncrearPlato);
            this.Controls.Add(this.gbDatosIngrediente);
            this.Controls.Add(this.btnRegresarMenu);
            this.Controls.Add(this.cbFiltrarProv);
            this.Controls.Add(this.dgvIngredientes);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormIngredientesInv";
            this.Text = "FormIngredientesInv";
            this.Load += new System.EventHandler(this.FormIngredientesInv_Load);
            this.gbDatosIngrediente.ResumeLayout(false);
            this.gbDatosIngrediente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngredientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatosIngrediente;
        private System.Windows.Forms.Label lblSeleccion;
        private System.Windows.Forms.ComboBox cbProveedores;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreC;
        private System.Windows.Forms.Label lbNombreBebida;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnRegresarMenu;
        private System.Windows.Forms.Button btnActualizarB;
        private System.Windows.Forms.ComboBox cbFiltrarProv;
        private System.Windows.Forms.DataGridView dgvIngredientes;
        private System.Windows.Forms.Button btnAgregarIng;
        private System.Windows.Forms.Button btncrearPlato;
    }
}