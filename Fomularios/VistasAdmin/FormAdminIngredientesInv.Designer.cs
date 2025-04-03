namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormAdminIngredientesInv
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtDescripcionC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSeleccionBebida = new System.Windows.Forms.Label();
            this.cbProveedores = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEliminarI = new System.Windows.Forms.Button();
            this.btnActualizarC = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreC = new System.Windows.Forms.TextBox();
            this.lbNombreBebida = new System.Windows.Forms.Label();
            this.btnRegresarInventario = new System.Windows.Forms.Button();
            this.dgvIngredientes = new System.Windows.Forms.DataGridView();
            this.btnAgregarPlato = new System.Windows.Forms.Button();
            this.gbDatosIngrediente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngredientes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDatosIngrediente
            // 
            this.gbDatosIngrediente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbDatosIngrediente.Controls.Add(this.button1);
            this.gbDatosIngrediente.Controls.Add(this.txtDescripcionC);
            this.gbDatosIngrediente.Controls.Add(this.label3);
            this.gbDatosIngrediente.Controls.Add(this.lblSeleccionBebida);
            this.gbDatosIngrediente.Controls.Add(this.cbProveedores);
            this.gbDatosIngrediente.Controls.Add(this.label2);
            this.gbDatosIngrediente.Controls.Add(this.btnEliminarI);
            this.gbDatosIngrediente.Controls.Add(this.btnActualizarC);
            this.gbDatosIngrediente.Controls.Add(this.label1);
            this.gbDatosIngrediente.Controls.Add(this.txtNombreC);
            this.gbDatosIngrediente.Controls.Add(this.lbNombreBebida);
            this.gbDatosIngrediente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosIngrediente.ForeColor = System.Drawing.SystemColors.Control;
            this.gbDatosIngrediente.Location = new System.Drawing.Point(458, 47);
            this.gbDatosIngrediente.Margin = new System.Windows.Forms.Padding(2);
            this.gbDatosIngrediente.Name = "gbDatosIngrediente";
            this.gbDatosIngrediente.Padding = new System.Windows.Forms.Padding(2);
            this.gbDatosIngrediente.Size = new System.Drawing.Size(476, 335);
            this.gbDatosIngrediente.TabIndex = 11;
            this.gbDatosIngrediente.TabStop = false;
            this.gbDatosIngrediente.Text = "Datos del Ingrediente";
            this.gbDatosIngrediente.Enter += new System.EventHandler(this.gbDatosIngrediente_Enter);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(176, 258);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 47);
            this.button1.TabIndex = 14;
            this.button1.Text = "Actualizar datos el ingrediente";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // txtDescripcionC
            // 
            this.txtDescripcionC.Location = new System.Drawing.Point(188, 141);
            this.txtDescripcionC.Name = "txtDescripcionC";
            this.txtDescripcionC.Size = new System.Drawing.Size(171, 23);
            this.txtDescripcionC.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(22, 206);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Seleccione el proveedor:";
            // 
            // lblSeleccionBebida
            // 
            this.lblSeleccionBebida.AutoSize = true;
            this.lblSeleccionBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccionBebida.Location = new System.Drawing.Point(22, 41);
            this.lblSeleccionBebida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeleccionBebida.Name = "lblSeleccionBebida";
            this.lblSeleccionBebida.Size = new System.Drawing.Size(219, 17);
            this.lblSeleccionBebida.TabIndex = 8;
            this.lblSeleccionBebida.Text = "Selecciona el ingrediente a editar";
            // 
            // cbProveedores
            // 
            this.cbProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbProveedores.FormattingEnabled = true;
            this.cbProveedores.Location = new System.Drawing.Point(188, 203);
            this.cbProveedores.Margin = new System.Windows.Forms.Padding(2);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(171, 24);
            this.cbProveedores.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 3;
            // 
            // btnEliminarI
            // 
            this.btnEliminarI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnEliminarI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarI.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEliminarI.Location = new System.Drawing.Point(25, 258);
            this.btnEliminarI.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarI.Name = "btnEliminarI";
            this.btnEliminarI.Size = new System.Drawing.Size(93, 47);
            this.btnEliminarI.TabIndex = 13;
            this.btnEliminarI.Text = "Eliminar";
            this.btnEliminarI.UseVisualStyleBackColor = false;
            // 
            // btnActualizarC
            // 
            this.btnActualizarC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(207)))), ((int)(((byte)(111)))));
            this.btnActualizarC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizarC.Location = new System.Drawing.Point(326, 258);
            this.btnActualizarC.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualizarC.Name = "btnActualizarC";
            this.btnActualizarC.Size = new System.Drawing.Size(133, 47);
            this.btnActualizarC.TabIndex = 12;
            this.btnActualizarC.Text = "Agregar ingrediente";
            this.btnActualizarC.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(22, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cantidad:";
            // 
            // txtNombreC
            // 
            this.txtNombreC.Location = new System.Drawing.Point(188, 84);
            this.txtNombreC.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombreC.Name = "txtNombreC";
            this.txtNombreC.Size = new System.Drawing.Size(170, 23);
            this.txtNombreC.TabIndex = 1;
            // 
            // lbNombreBebida
            // 
            this.lbNombreBebida.AutoSize = true;
            this.lbNombreBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lbNombreBebida.Location = new System.Drawing.Point(22, 84);
            this.lbNombreBebida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNombreBebida.Name = "lbNombreBebida";
            this.lbNombreBebida.Size = new System.Drawing.Size(145, 17);
            this.lbNombreBebida.TabIndex = 0;
            this.lbNombreBebida.Text = "Nombre del producto:";
            // 
            // btnRegresarInventario
            // 
            this.btnRegresarInventario.BackColor = System.Drawing.Color.White;
            this.btnRegresarInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresarInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresarInventario.ForeColor = System.Drawing.Color.Black;
            this.btnRegresarInventario.Location = new System.Drawing.Point(31, 407);
            this.btnRegresarInventario.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegresarInventario.Name = "btnRegresarInventario";
            this.btnRegresarInventario.Size = new System.Drawing.Size(146, 47);
            this.btnRegresarInventario.TabIndex = 10;
            this.btnRegresarInventario.Text = "Regresar al inventario";
            this.btnRegresarInventario.UseVisualStyleBackColor = false;
            // 
            // dgvIngredientes
            // 
            this.dgvIngredientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIngredientes.Location = new System.Drawing.Point(31, 47);
            this.dgvIngredientes.Margin = new System.Windows.Forms.Padding(2);
            this.dgvIngredientes.Name = "dgvIngredientes";
            this.dgvIngredientes.RowHeadersWidth = 51;
            this.dgvIngredientes.RowTemplate.Height = 24;
            this.dgvIngredientes.Size = new System.Drawing.Size(406, 335);
            this.dgvIngredientes.TabIndex = 8;
            this.dgvIngredientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIngredientes_CellContentClick);
            // 
            // btnAgregarPlato
            // 
            this.btnAgregarPlato.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnAgregarPlato.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarPlato.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregarPlato.Location = new System.Drawing.Point(801, 407);
            this.btnAgregarPlato.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarPlato.Name = "btnAgregarPlato";
            this.btnAgregarPlato.Size = new System.Drawing.Size(133, 47);
            this.btnAgregarPlato.TabIndex = 13;
            this.btnAgregarPlato.Text = "Crear un plato para menú";
            this.btnAgregarPlato.UseVisualStyleBackColor = false;
            // 
            // FormAdminIngredientesInv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(969, 504);
            this.Controls.Add(this.btnAgregarPlato);
            this.Controls.Add(this.gbDatosIngrediente);
            this.Controls.Add(this.btnRegresarInventario);
            this.Controls.Add(this.dgvIngredientes);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormAdminIngredientesInv";
            this.Text = "FormAdminIngredientesInv";
            this.Load += new System.EventHandler(this.FormAdminIngredientesInv_Load);
            this.gbDatosIngrediente.ResumeLayout(false);
            this.gbDatosIngrediente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngredientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatosIngrediente;
        private System.Windows.Forms.TextBox txtDescripcionC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSeleccionBebida;
        private System.Windows.Forms.ComboBox cbProveedores;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreC;
        private System.Windows.Forms.Label lbNombreBebida;
        private System.Windows.Forms.Button btnRegresarInventario;
        private System.Windows.Forms.DataGridView dgvIngredientes;
        private System.Windows.Forms.Button btnEliminarI;
        private System.Windows.Forms.Button btnActualizarC;
        private System.Windows.Forms.Button btnAgregarPlato;
        private System.Windows.Forms.Button button1;
    }
}