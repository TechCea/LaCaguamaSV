namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormComidasMenuAdmin
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
            this.btnRegresarMenu = new System.Windows.Forms.Button();
            this.cbCategoriaC = new System.Windows.Forms.ComboBox();
            this.dgvComidas = new System.Windows.Forms.DataGridView();
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
            this.btnEliminarC = new System.Windows.Forms.Button();
            this.btnActualizarC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).BeginInit();
            this.gbDatosBebida.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.BackColor = System.Drawing.Color.White;
            this.btnRegresarMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresarMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresarMenu.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegresarMenu.Location = new System.Drawing.Point(53, 404);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(138, 47);
            this.btnRegresarMenu.TabIndex = 5;
            this.btnRegresarMenu.Text = "Regresar Menú";
            this.btnRegresarMenu.UseVisualStyleBackColor = false;
            this.btnRegresarMenu.Click += new System.EventHandler(this.btnRegresarMenu_Click);
            // 
            // cbCategoriaC
            // 
            this.cbCategoriaC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCategoriaC.FormattingEnabled = true;
            this.cbCategoriaC.Location = new System.Drawing.Point(517, 20);
            this.cbCategoriaC.Margin = new System.Windows.Forms.Padding(2);
            this.cbCategoriaC.Name = "cbCategoriaC";
            this.cbCategoriaC.Size = new System.Drawing.Size(92, 21);
            this.cbCategoriaC.TabIndex = 4;
            this.cbCategoriaC.SelectedIndexChanged += new System.EventHandler(this.cbCategoriaC_SelectedIndexChanged);
            // 
            // dgvComidas
            // 
            this.dgvComidas.BackgroundColor = System.Drawing.Color.Gray;
            this.dgvComidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComidas.Location = new System.Drawing.Point(53, 54);
            this.dgvComidas.Margin = new System.Windows.Forms.Padding(2);
            this.dgvComidas.Name = "dgvComidas";
            this.dgvComidas.RowHeadersWidth = 51;
            this.dgvComidas.RowTemplate.Height = 24;
            this.dgvComidas.Size = new System.Drawing.Size(556, 335);
            this.dgvComidas.TabIndex = 3;
            this.dgvComidas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComidas_CellContentClick);
            // 
            // gbDatosBebida
            // 
            this.gbDatosBebida.BackColor = System.Drawing.Color.Gray;
            this.gbDatosBebida.Controls.Add(this.txtDescripcionC);
            this.gbDatosBebida.Controls.Add(this.label3);
            this.gbDatosBebida.Controls.Add(this.lblSeleccionBebida);
            this.gbDatosBebida.Controls.Add(this.cbCategoriaB);
            this.gbDatosBebida.Controls.Add(this.txtPrecioU);
            this.gbDatosBebida.Controls.Add(this.label2);
            this.gbDatosBebida.Controls.Add(this.label1);
            this.gbDatosBebida.Controls.Add(this.txtNombreC);
            this.gbDatosBebida.Controls.Add(this.lbNombreBebida);
            this.gbDatosBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.gbDatosBebida.Location = new System.Drawing.Point(639, 54);
            this.gbDatosBebida.Margin = new System.Windows.Forms.Padding(2);
            this.gbDatosBebida.Name = "gbDatosBebida";
            this.gbDatosBebida.Padding = new System.Windows.Forms.Padding(2);
            this.gbDatosBebida.Size = new System.Drawing.Size(329, 335);
            this.gbDatosBebida.TabIndex = 6;
            this.gbDatosBebida.TabStop = false;
            this.gbDatosBebida.Text = "Datos Comida";
            // 
            // txtDescripcionC
            // 
            this.txtDescripcionC.Location = new System.Drawing.Point(126, 183);
            this.txtDescripcionC.Name = "txtDescripcionC";
            this.txtDescripcionC.Size = new System.Drawing.Size(171, 23);
            this.txtDescripcionC.TabIndex = 11;
            this.txtDescripcionC.TextChanged += new System.EventHandler(this.txtDescripcionC_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(22, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Descripción:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblSeleccionBebida
            // 
            this.lblSeleccionBebida.AutoSize = true;
            this.lblSeleccionBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccionBebida.Location = new System.Drawing.Point(22, 32);
            this.lblSeleccionBebida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeleccionBebida.Name = "lblSeleccionBebida";
            this.lblSeleccionBebida.Size = new System.Drawing.Size(236, 17);
            this.lblSeleccionBebida.TabIndex = 8;
            this.lblSeleccionBebida.Text = "Selecciona un platillo a editar/borrar";
            // 
            // cbCategoriaB
            // 
            this.cbCategoriaB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCategoriaB.FormattingEnabled = true;
            this.cbCategoriaB.Location = new System.Drawing.Point(126, 121);
            this.cbCategoriaB.Margin = new System.Windows.Forms.Padding(2);
            this.cbCategoriaB.Name = "cbCategoriaB";
            this.cbCategoriaB.Size = new System.Drawing.Size(170, 24);
            this.cbCategoriaB.TabIndex = 5;
            this.cbCategoriaB.SelectedIndexChanged += new System.EventHandler(this.cbCategoriaB_SelectedIndexChanged);
            // 
            // txtPrecioU
            // 
            this.txtPrecioU.Location = new System.Drawing.Point(126, 253);
            this.txtPrecioU.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecioU.Name = "txtPrecioU";
            this.txtPrecioU.Size = new System.Drawing.Size(171, 23);
            this.txtPrecioU.TabIndex = 4;
            this.txtPrecioU.TextChanged += new System.EventHandler(this.txtPrecioU_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(22, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Precio unitario:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(22, 121);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Categoría:";
            // 
            // txtNombreC
            // 
            this.txtNombreC.Location = new System.Drawing.Point(126, 66);
            this.txtNombreC.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombreC.Name = "txtNombreC";
            this.txtNombreC.Size = new System.Drawing.Size(170, 23);
            this.txtNombreC.TabIndex = 1;
            this.txtNombreC.TextChanged += new System.EventHandler(this.txtNombreC_TextChanged);
            // 
            // lbNombreBebida
            // 
            this.lbNombreBebida.AutoSize = true;
            this.lbNombreBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lbNombreBebida.Location = new System.Drawing.Point(22, 66);
            this.lbNombreBebida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNombreBebida.Name = "lbNombreBebida";
            this.lbNombreBebida.Size = new System.Drawing.Size(97, 17);
            this.lbNombreBebida.TabIndex = 0;
            this.lbNombreBebida.Text = "Nombre plato:";
            // 
            // btnEliminarC
            // 
            this.btnEliminarC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnEliminarC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEliminarC.Location = new System.Drawing.Point(875, 404);
            this.btnEliminarC.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarC.Name = "btnEliminarC";
            this.btnEliminarC.Size = new System.Drawing.Size(93, 47);
            this.btnEliminarC.TabIndex = 7;
            this.btnEliminarC.Text = "Eliminar";
            this.btnEliminarC.UseVisualStyleBackColor = false;
            this.btnEliminarC.Click += new System.EventHandler(this.btnEliminarC_Click);
            // 
            // btnActualizarC
            // 
            this.btnActualizarC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnActualizarC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizarC.Location = new System.Drawing.Point(641, 404);
            this.btnActualizarC.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualizarC.Name = "btnActualizarC";
            this.btnActualizarC.Size = new System.Drawing.Size(123, 47);
            this.btnActualizarC.TabIndex = 6;
            this.btnActualizarC.Text = "Actualizar datos";
            this.btnActualizarC.UseVisualStyleBackColor = false;
            this.btnActualizarC.Click += new System.EventHandler(this.btnActualizarC_Click);
            // 
            // FormComidasMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(991, 486);
            this.Controls.Add(this.gbDatosBebida);
            this.Controls.Add(this.btnRegresarMenu);
            this.Controls.Add(this.cbCategoriaC);
            this.Controls.Add(this.dgvComidas);
            this.Controls.Add(this.btnEliminarC);
            this.Controls.Add(this.btnActualizarC);
            this.Name = "FormComidasMenuAdmin";
            this.Text = "FormComidasMenuAdmin";
            this.Load += new System.EventHandler(this.FormComidasMenuAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).EndInit();
            this.gbDatosBebida.ResumeLayout(false);
            this.gbDatosBebida.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegresarMenu;
        private System.Windows.Forms.ComboBox cbCategoriaC;
        private System.Windows.Forms.DataGridView dgvComidas;
        private System.Windows.Forms.GroupBox gbDatosBebida;
        private System.Windows.Forms.Label lblSeleccionBebida;
        private System.Windows.Forms.Button btnEliminarC;
        private System.Windows.Forms.Button btnActualizarC;
        private System.Windows.Forms.ComboBox cbCategoriaB;
        private System.Windows.Forms.TextBox txtPrecioU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreC;
        private System.Windows.Forms.Label lbNombreBebida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcionC;
    }
}