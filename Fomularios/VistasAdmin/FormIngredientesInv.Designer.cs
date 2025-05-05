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
            this.btnRegresarMenu = new System.Windows.Forms.Button();
            this.cbFiltrarProv = new System.Windows.Forms.ComboBox();
            this.dgvIngredientes = new System.Windows.Forms.DataGridView();
            this.btncrearPlato = new System.Windows.Forms.Button();
            this.cbc_disponibilidad = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_limpiarcampos = new System.Windows.Forms.Button();
            this.gbDatosIngrediente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngredientes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDatosIngrediente
            // 
            this.gbDatosIngrediente.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gbDatosIngrediente.Controls.Add(this.btn_limpiarcampos);
            this.gbDatosIngrediente.Controls.Add(this.cbc_disponibilidad);
            this.gbDatosIngrediente.Controls.Add(this.label5);
            this.gbDatosIngrediente.Controls.Add(this.lblSeleccion);
            this.gbDatosIngrediente.Controls.Add(this.cbProveedores);
            this.gbDatosIngrediente.Controls.Add(this.txtCantidad);
            this.gbDatosIngrediente.Controls.Add(this.label2);
            this.gbDatosIngrediente.Controls.Add(this.label1);
            this.gbDatosIngrediente.Controls.Add(this.txtNombreC);
            this.gbDatosIngrediente.Controls.Add(this.lbNombreBebida);
            this.gbDatosIngrediente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gbDatosIngrediente.ForeColor = System.Drawing.SystemColors.Control;
            this.gbDatosIngrediente.Location = new System.Drawing.Point(524, 63);
            this.gbDatosIngrediente.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbDatosIngrediente.Name = "gbDatosIngrediente";
            this.gbDatosIngrediente.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbDatosIngrediente.Size = new System.Drawing.Size(364, 363);
            this.gbDatosIngrediente.TabIndex = 11;
            this.gbDatosIngrediente.TabStop = false;
            this.gbDatosIngrediente.Text = "Datos Ingrediente";
            // 
            // btnAgregarIng
            // 
            this.btnAgregarIng.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.btnAgregarIng.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarIng.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregarIng.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarIng.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAgregarIng.Location = new System.Drawing.Point(569, 443);
            this.btnAgregarIng.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAgregarIng.Name = "btnAgregarIng";
            this.btnAgregarIng.Size = new System.Drawing.Size(160, 34);
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
            this.btnActualizarB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizarB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarB.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnActualizarB.Location = new System.Drawing.Point(749, 443);
            this.btnActualizarB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnActualizarB.Name = "btnActualizarB";
            this.btnActualizarB.Size = new System.Drawing.Size(139, 34);
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
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.BackColor = System.Drawing.Color.White;
            this.btnRegresarMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresarMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegresarMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresarMenu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRegresarMenu.Location = new System.Drawing.Point(49, 443);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(190, 34);
            this.btnRegresarMenu.TabIndex = 10;
            this.btnRegresarMenu.Text = "Regresar a inventario";
            this.btnRegresarMenu.UseVisualStyleBackColor = false;
            this.btnRegresarMenu.Click += new System.EventHandler(this.btnRegresarMenu_Click);
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
            this.dgvIngredientes.Size = new System.Drawing.Size(436, 363);
            this.dgvIngredientes.TabIndex = 8;
            // 
            // btncrearPlato
            // 
            this.btncrearPlato.BackColor = System.Drawing.Color.Gold;
            this.btncrearPlato.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncrearPlato.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btncrearPlato.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncrearPlato.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btncrearPlato.Location = new System.Drawing.Point(698, 500);
            this.btncrearPlato.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btncrearPlato.Name = "btncrearPlato";
            this.btncrearPlato.Size = new System.Drawing.Size(190, 41);
            this.btncrearPlato.TabIndex = 14;
            this.btncrearPlato.Text = "Crear platos y recetas";
            this.btncrearPlato.UseVisualStyleBackColor = false;
            this.btncrearPlato.Click += new System.EventHandler(this.btncrearPlato_Click);
            // 
            // cbc_disponibilidad
            // 
            this.cbc_disponibilidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbc_disponibilidad.FormattingEnabled = true;
            this.cbc_disponibilidad.Location = new System.Drawing.Point(159, 240);
            this.cbc_disponibilidad.Name = "cbc_disponibilidad";
            this.cbc_disponibilidad.Size = new System.Drawing.Size(171, 24);
            this.cbc_disponibilidad.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(28, 243);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "Disponibilidad:";
            // 
            // btn_limpiarcampos
            // 
            this.btn_limpiarcampos.BackColor = System.Drawing.Color.White;
            this.btn_limpiarcampos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_limpiarcampos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_limpiarcampos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_limpiarcampos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_limpiarcampos.Location = new System.Drawing.Point(174, 292);
            this.btn_limpiarcampos.Margin = new System.Windows.Forms.Padding(2);
            this.btn_limpiarcampos.Name = "btn_limpiarcampos";
            this.btn_limpiarcampos.Size = new System.Drawing.Size(156, 34);
            this.btn_limpiarcampos.TabIndex = 15;
            this.btn_limpiarcampos.Text = "Limpiar campos";
            this.btn_limpiarcampos.UseVisualStyleBackColor = false;
            this.btn_limpiarcampos.Click += new System.EventHandler(this.btn_limpiarcampos_Click);
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
            this.Controls.Add(this.btnAgregarIng);
            this.Controls.Add(this.gbDatosIngrediente);
            this.Controls.Add(this.btnRegresarMenu);
            this.Controls.Add(this.cbFiltrarProv);
            this.Controls.Add(this.dgvIngredientes);
            this.Controls.Add(this.btnActualizarB);
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
        private System.Windows.Forms.Button btnRegresarMenu;
        private System.Windows.Forms.Button btnActualizarB;
        private System.Windows.Forms.ComboBox cbFiltrarProv;
        private System.Windows.Forms.DataGridView dgvIngredientes;
        private System.Windows.Forms.Button btnAgregarIng;
        private System.Windows.Forms.Button btncrearPlato;
        private System.Windows.Forms.ComboBox cbc_disponibilidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_limpiarcampos;
    }
}