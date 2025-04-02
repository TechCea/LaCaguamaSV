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
            this.txtDescripcionC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSeleccionBebida = new System.Windows.Forms.Label();
            this.cbProveedores = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreC = new System.Windows.Forms.TextBox();
            this.lbNombreBebida = new System.Windows.Forms.Label();
            this.btnRegresarInventario = new System.Windows.Forms.Button();
            this.dgvIngredientes = new System.Windows.Forms.DataGridView();
            this.btnEliminarI = new System.Windows.Forms.Button();
            this.btnActualizarC = new System.Windows.Forms.Button();
            this.btnAgregarPlato = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbDatosIngrediente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngredientes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDatosIngrediente
            // 
            this.gbDatosIngrediente.BackColor = System.Drawing.SystemColors.ControlDark;
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
            this.gbDatosIngrediente.Location = new System.Drawing.Point(611, 58);
            this.gbDatosIngrediente.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosIngrediente.Name = "gbDatosIngrediente";
            this.gbDatosIngrediente.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosIngrediente.Size = new System.Drawing.Size(634, 412);
            this.gbDatosIngrediente.TabIndex = 11;
            this.gbDatosIngrediente.TabStop = false;
            this.gbDatosIngrediente.Text = "Datos del Ingrediente";
            // 
            // txtDescripcionC
            // 
            this.txtDescripcionC.Location = new System.Drawing.Point(250, 173);
            this.txtDescripcionC.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescripcionC.Name = "txtDescripcionC";
            this.txtDescripcionC.Size = new System.Drawing.Size(227, 26);
            this.txtDescripcionC.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Seleccione el proveedor:";
            // 
            // lblSeleccionBebida
            // 
            this.lblSeleccionBebida.AutoSize = true;
            this.lblSeleccionBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionBebida.Location = new System.Drawing.Point(29, 51);
            this.lblSeleccionBebida.Name = "lblSeleccionBebida";
            this.lblSeleccionBebida.Size = new System.Drawing.Size(257, 20);
            this.lblSeleccionBebida.TabIndex = 8;
            this.lblSeleccionBebida.Text = "Selecciona el ingrediente a editar";
            // 
            // cbProveedores
            // 
            this.cbProveedores.FormattingEnabled = true;
            this.cbProveedores.Location = new System.Drawing.Point(250, 250);
            this.cbProveedores.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(227, 28);
            this.cbProveedores.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cantidad:";
            // 
            // txtNombreC
            // 
            this.txtNombreC.Location = new System.Drawing.Point(250, 103);
            this.txtNombreC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombreC.Name = "txtNombreC";
            this.txtNombreC.Size = new System.Drawing.Size(225, 26);
            this.txtNombreC.TabIndex = 1;
            // 
            // lbNombreBebida
            // 
            this.lbNombreBebida.AutoSize = true;
            this.lbNombreBebida.Location = new System.Drawing.Point(30, 103);
            this.lbNombreBebida.Name = "lbNombreBebida";
            this.lbNombreBebida.Size = new System.Drawing.Size(170, 20);
            this.lbNombreBebida.TabIndex = 0;
            this.lbNombreBebida.Text = "Nombre del producto:";
            // 
            // btnRegresarInventario
            // 
            this.btnRegresarInventario.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnRegresarInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresarInventario.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegresarInventario.Location = new System.Drawing.Point(41, 503);
            this.btnRegresarInventario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegresarInventario.Name = "btnRegresarInventario";
            this.btnRegresarInventario.Size = new System.Drawing.Size(184, 58);
            this.btnRegresarInventario.TabIndex = 10;
            this.btnRegresarInventario.Text = "Regresar al inventario";
            this.btnRegresarInventario.UseVisualStyleBackColor = false;
            // 
            // dgvIngredientes
            // 
            this.dgvIngredientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIngredientes.Location = new System.Drawing.Point(41, 58);
            this.dgvIngredientes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvIngredientes.Name = "dgvIngredientes";
            this.dgvIngredientes.RowHeadersWidth = 51;
            this.dgvIngredientes.RowTemplate.Height = 24;
            this.dgvIngredientes.Size = new System.Drawing.Size(541, 412);
            this.dgvIngredientes.TabIndex = 8;
            // 
            // btnEliminarI
            // 
            this.btnEliminarI.BackColor = System.Drawing.Color.Brown;
            this.btnEliminarI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarI.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEliminarI.Location = new System.Drawing.Point(33, 318);
            this.btnEliminarI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEliminarI.Name = "btnEliminarI";
            this.btnEliminarI.Size = new System.Drawing.Size(124, 58);
            this.btnEliminarI.TabIndex = 13;
            this.btnEliminarI.Text = "Eliminar";
            this.btnEliminarI.UseVisualStyleBackColor = false;
            // 
            // btnActualizarC
            // 
            this.btnActualizarC.BackColor = System.Drawing.Color.ForestGreen;
            this.btnActualizarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizarC.Location = new System.Drawing.Point(434, 318);
            this.btnActualizarC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnActualizarC.Name = "btnActualizarC";
            this.btnActualizarC.Size = new System.Drawing.Size(177, 58);
            this.btnActualizarC.TabIndex = 12;
            this.btnActualizarC.Text = "Agregar ingrediente";
            this.btnActualizarC.UseVisualStyleBackColor = false;
            // 
            // btnAgregarPlato
            // 
            this.btnAgregarPlato.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAgregarPlato.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarPlato.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregarPlato.Location = new System.Drawing.Point(1045, 514);
            this.btnAgregarPlato.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregarPlato.Name = "btnAgregarPlato";
            this.btnAgregarPlato.Size = new System.Drawing.Size(177, 58);
            this.btnAgregarPlato.TabIndex = 13;
            this.btnAgregarPlato.Text = "Crear un plato para menú";
            this.btnAgregarPlato.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.ForestGreen;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(234, 318);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 58);
            this.button1.TabIndex = 14;
            this.button1.Text = "Actualizar datos el ingrediente";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // FormAdminIngredientesInv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1292, 620);
            this.Controls.Add(this.btnAgregarPlato);
            this.Controls.Add(this.gbDatosIngrediente);
            this.Controls.Add(this.btnRegresarInventario);
            this.Controls.Add(this.dgvIngredientes);
            this.Name = "FormAdminIngredientesInv";
            this.Text = "FormAdminIngredientesInv";
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