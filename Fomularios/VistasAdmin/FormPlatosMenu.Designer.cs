namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormPlatosMenu
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
            this.btnActualizarPlato = new System.Windows.Forms.Button();
            this.btnAgregarIng = new System.Windows.Forms.Button();
            this.btnRegresarInventario = new System.Windows.Forms.Button();
            this.dgvComidas = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.gbDatosBebida = new System.Windows.Forms.GroupBox();
            this.txtDescripcionC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSeleccionBebida = new System.Windows.Forms.Label();
            this.cbCategoriaB = new System.Windows.Forms.ComboBox();
            this.txtPrecioU = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreC = new System.Windows.Forms.TextBox();
            this.lbNombreBebida = new System.Windows.Forms.Label();
            this.cbCategoriaC = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).BeginInit();
            this.gbDatosBebida.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnActualizarPlato
            // 
            this.btnActualizarPlato.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnActualizarPlato.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarPlato.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizarPlato.Location = new System.Drawing.Point(738, 587);
            this.btnActualizarPlato.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnActualizarPlato.Name = "btnActualizarPlato";
            this.btnActualizarPlato.Size = new System.Drawing.Size(182, 58);
            this.btnActualizarPlato.TabIndex = 14;
            this.btnActualizarPlato.Text = "Editar datos el plato";
            this.btnActualizarPlato.UseVisualStyleBackColor = false;
            this.btnActualizarPlato.Click += new System.EventHandler(this.btnActualizarPlato_Click);
            // 
            // btnAgregarIng
            // 
            this.btnAgregarIng.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAgregarIng.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarIng.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAgregarIng.Location = new System.Drawing.Point(254, 374);
            this.btnAgregarIng.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregarIng.Name = "btnAgregarIng";
            this.btnAgregarIng.Size = new System.Drawing.Size(141, 58);
            this.btnAgregarIng.TabIndex = 12;
            this.btnAgregarIng.Text = "Agregar plato";
            this.btnAgregarIng.UseVisualStyleBackColor = false;
            this.btnAgregarIng.Click += new System.EventHandler(this.btnAgregarIng_Click);
            // 
            // btnRegresarInventario
            // 
            this.btnRegresarInventario.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnRegresarInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresarInventario.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegresarInventario.Location = new System.Drawing.Point(41, 587);
            this.btnRegresarInventario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegresarInventario.Name = "btnRegresarInventario";
            this.btnRegresarInventario.Size = new System.Drawing.Size(184, 58);
            this.btnRegresarInventario.TabIndex = 17;
            this.btnRegresarInventario.Text = "Regresar al inventario";
            this.btnRegresarInventario.UseVisualStyleBackColor = false;
            // 
            // dgvComidas
            // 
            this.dgvComidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComidas.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvComidas.Location = new System.Drawing.Point(41, 116);
            this.dgvComidas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvComidas.Name = "dgvComidas";
            this.dgvComidas.RowHeadersWidth = 51;
            this.dgvComidas.RowTemplate.Height = 24;
            this.dgvComidas.Size = new System.Drawing.Size(644, 448);
            this.dgvComidas.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(37, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(357, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Selecciona un plato para ver o editar su receta";
            // 
            // gbDatosBebida
            // 
            this.gbDatosBebida.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gbDatosBebida.Controls.Add(this.txtDescripcionC);
            this.gbDatosBebida.Controls.Add(this.label3);
            this.gbDatosBebida.Controls.Add(this.lblSeleccionBebida);
            this.gbDatosBebida.Controls.Add(this.cbCategoriaB);
            this.gbDatosBebida.Controls.Add(this.txtPrecioU);
            this.gbDatosBebida.Controls.Add(this.label2);
            this.gbDatosBebida.Controls.Add(this.btnAgregarIng);
            this.gbDatosBebida.Controls.Add(this.label1);
            this.gbDatosBebida.Controls.Add(this.txtNombreC);
            this.gbDatosBebida.Controls.Add(this.lbNombreBebida);
            this.gbDatosBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosBebida.Location = new System.Drawing.Point(738, 116);
            this.gbDatosBebida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosBebida.Name = "gbDatosBebida";
            this.gbDatosBebida.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosBebida.Size = new System.Drawing.Size(441, 448);
            this.gbDatosBebida.TabIndex = 21;
            this.gbDatosBebida.TabStop = false;
            this.gbDatosBebida.Text = "Datos Comida";
            // 
            // txtDescripcionC
            // 
            this.txtDescripcionC.Location = new System.Drawing.Point(168, 225);
            this.txtDescripcionC.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescripcionC.Name = "txtDescripcionC";
            this.txtDescripcionC.Size = new System.Drawing.Size(227, 26);
            this.txtDescripcionC.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Descripción:";
            // 
            // lblSeleccionBebida
            // 
            this.lblSeleccionBebida.AutoSize = true;
            this.lblSeleccionBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionBebida.Location = new System.Drawing.Point(29, 39);
            this.lblSeleccionBebida.Name = "lblSeleccionBebida";
            this.lblSeleccionBebida.Size = new System.Drawing.Size(173, 20);
            this.lblSeleccionBebida.TabIndex = 8;
            this.lblSeleccionBebida.Text = "Agregar datos el plato";
            // 
            // cbCategoriaB
            // 
            this.cbCategoriaB.FormattingEnabled = true;
            this.cbCategoriaB.Location = new System.Drawing.Point(168, 149);
            this.cbCategoriaB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategoriaB.Name = "cbCategoriaB";
            this.cbCategoriaB.Size = new System.Drawing.Size(225, 28);
            this.cbCategoriaB.TabIndex = 5;
            // 
            // txtPrecioU
            // 
            this.txtPrecioU.Location = new System.Drawing.Point(168, 311);
            this.txtPrecioU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPrecioU.Name = "txtPrecioU";
            this.txtPrecioU.Size = new System.Drawing.Size(227, 26);
            this.txtPrecioU.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Precio unitario:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Categoría:";
            // 
            // txtNombreC
            // 
            this.txtNombreC.Location = new System.Drawing.Point(168, 81);
            this.txtNombreC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombreC.Name = "txtNombreC";
            this.txtNombreC.Size = new System.Drawing.Size(225, 26);
            this.txtNombreC.TabIndex = 1;
            // 
            // lbNombreBebida
            // 
            this.lbNombreBebida.AutoSize = true;
            this.lbNombreBebida.Location = new System.Drawing.Point(29, 81);
            this.lbNombreBebida.Name = "lbNombreBebida";
            this.lbNombreBebida.Size = new System.Drawing.Size(127, 20);
            this.lbNombreBebida.TabIndex = 0;
            this.lbNombreBebida.Text = "Nombre bebida:";
            // 
            // cbCategoriaC
            // 
            this.cbCategoriaC.FormattingEnabled = true;
            this.cbCategoriaC.Location = new System.Drawing.Point(496, 73);
            this.cbCategoriaC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategoriaC.Name = "cbCategoriaC";
            this.cbCategoriaC.Size = new System.Drawing.Size(189, 24);
            this.cbCategoriaC.TabIndex = 20;
            // 
            // FormPlatosMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1229, 698);
            this.Controls.Add(this.gbDatosBebida);
            this.Controls.Add(this.btnActualizarPlato);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbCategoriaC);
            this.Controls.Add(this.btnRegresarInventario);
            this.Controls.Add(this.dgvComidas);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "FormPlatosMenu";
            this.Text = "FormPlatosMenú";
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).EndInit();
            this.gbDatosBebida.ResumeLayout(false);
            this.gbDatosBebida.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnActualizarPlato;
        private System.Windows.Forms.Button btnAgregarIng;
        private System.Windows.Forms.Button btnRegresarInventario;
        private System.Windows.Forms.DataGridView dgvComidas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbDatosBebida;
        private System.Windows.Forms.TextBox txtDescripcionC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSeleccionBebida;
        private System.Windows.Forms.ComboBox cbCategoriaB;
        private System.Windows.Forms.TextBox txtPrecioU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreC;
        private System.Windows.Forms.Label lbNombreBebida;
        private System.Windows.Forms.ComboBox cbCategoriaC;
    }
}