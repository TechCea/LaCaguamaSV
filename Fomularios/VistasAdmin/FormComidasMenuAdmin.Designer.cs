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
            this.btnActualizarC = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCrearPlato = new System.Windows.Forms.Button();
            this.btn_limpiarcampos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).BeginInit();
            this.gbDatosBebida.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCategoriaC
            // 
            this.cbCategoriaC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCategoriaC.FormattingEnabled = true;
            this.cbCategoriaC.Location = new System.Drawing.Point(689, 25);
            this.cbCategoriaC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategoriaC.Name = "cbCategoriaC";
            this.cbCategoriaC.Size = new System.Drawing.Size(121, 24);
            this.cbCategoriaC.TabIndex = 4;
            this.cbCategoriaC.SelectedIndexChanged += new System.EventHandler(this.cbCategoriaC_SelectedIndexChanged);
            // 
            // dgvComidas
            // 
            this.dgvComidas.BackgroundColor = System.Drawing.Color.Gray;
            this.dgvComidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComidas.Location = new System.Drawing.Point(71, 66);
            this.dgvComidas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvComidas.Name = "dgvComidas";
            this.dgvComidas.RowHeadersWidth = 51;
            this.dgvComidas.RowTemplate.Height = 24;
            this.dgvComidas.Size = new System.Drawing.Size(741, 412);
            this.dgvComidas.TabIndex = 3;
            // 
            // gbDatosBebida
            // 
            this.gbDatosBebida.BackColor = System.Drawing.Color.Gray;
            this.gbDatosBebida.Controls.Add(this.btn_limpiarcampos);
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
            this.gbDatosBebida.Location = new System.Drawing.Point(852, 66);
            this.gbDatosBebida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosBebida.Name = "gbDatosBebida";
            this.gbDatosBebida.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosBebida.Size = new System.Drawing.Size(439, 412);
            this.gbDatosBebida.TabIndex = 6;
            this.gbDatosBebida.TabStop = false;
            this.gbDatosBebida.Text = "Datos Comida";
            // 
            // txtDescripcionC
            // 
            this.txtDescripcionC.Location = new System.Drawing.Point(168, 181);
            this.txtDescripcionC.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescripcionC.Name = "txtDescripcionC";
            this.txtDescripcionC.Size = new System.Drawing.Size(227, 26);
            this.txtDescripcionC.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(29, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Descripción:";
            // 
            // lblSeleccionBebida
            // 
            this.lblSeleccionBebida.AutoSize = true;
            this.lblSeleccionBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccionBebida.Location = new System.Drawing.Point(29, 39);
            this.lblSeleccionBebida.Name = "lblSeleccionBebida";
            this.lblSeleccionBebida.Size = new System.Drawing.Size(278, 20);
            this.lblSeleccionBebida.TabIndex = 8;
            this.lblSeleccionBebida.Text = "Selecciona un platillo a editar/borrar";
            // 
            // cbCategoriaB
            // 
            this.cbCategoriaB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCategoriaB.FormattingEnabled = true;
            this.cbCategoriaB.Location = new System.Drawing.Point(168, 131);
            this.cbCategoriaB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategoriaB.Name = "cbCategoriaB";
            this.cbCategoriaB.Size = new System.Drawing.Size(225, 28);
            this.cbCategoriaB.TabIndex = 5;
            // 
            // txtPrecioU
            // 
            this.txtPrecioU.Location = new System.Drawing.Point(168, 231);
            this.txtPrecioU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPrecioU.Name = "txtPrecioU";
            this.txtPrecioU.Size = new System.Drawing.Size(227, 26);
            this.txtPrecioU.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(29, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Precio unitario:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(29, 131);
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
            this.lbNombreBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lbNombreBebida.Location = new System.Drawing.Point(29, 81);
            this.lbNombreBebida.Name = "lbNombreBebida";
            this.lbNombreBebida.Size = new System.Drawing.Size(114, 20);
            this.lbNombreBebida.TabIndex = 0;
            this.lbNombreBebida.Text = "Nombre plato:";
            // 
            // btnActualizarC
            // 
            this.btnActualizarC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnActualizarC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizarC.Location = new System.Drawing.Point(1127, 497);
            this.btnActualizarC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnActualizarC.Name = "btnActualizarC";
            this.btnActualizarC.Size = new System.Drawing.Size(164, 58);
            this.btnActualizarC.TabIndex = 6;
            this.btnActualizarC.Text = "Actualizar datos";
            this.btnActualizarC.UseVisualStyleBackColor = false;
            this.btnActualizarC.Click += new System.EventHandler(this.btnActualizarC_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(66, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 26);
            this.label4.TabIndex = 16;
            this.label4.Text = "PLATOS";
            // 
            // btnCrearPlato
            // 
            this.btnCrearPlato.BackColor = System.Drawing.Color.ForestGreen;
            this.btnCrearPlato.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearPlato.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCrearPlato.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearPlato.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCrearPlato.Location = new System.Drawing.Point(961, 497);
            this.btnCrearPlato.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCrearPlato.Name = "btnCrearPlato";
            this.btnCrearPlato.Size = new System.Drawing.Size(139, 58);
            this.btnCrearPlato.TabIndex = 17;
            this.btnCrearPlato.Text = "Agregar plato";
            this.btnCrearPlato.UseVisualStyleBackColor = false;
            this.btnCrearPlato.Click += new System.EventHandler(this.btnCrearPlato_Click);
            // 
            // btn_limpiarcampos
            // 
            this.btn_limpiarcampos.BackColor = System.Drawing.Color.White;
            this.btn_limpiarcampos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_limpiarcampos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_limpiarcampos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_limpiarcampos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_limpiarcampos.Location = new System.Drawing.Point(187, 323);
            this.btn_limpiarcampos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_limpiarcampos.Name = "btn_limpiarcampos";
            this.btn_limpiarcampos.Size = new System.Drawing.Size(208, 42);
            this.btn_limpiarcampos.TabIndex = 18;
            this.btn_limpiarcampos.Text = "Limpiar campos";
            this.btn_limpiarcampos.UseVisualStyleBackColor = false;
            this.btn_limpiarcampos.Click += new System.EventHandler(this.btn_limpiarcampos_Click);
            // 
            // FormComidasMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1321, 598);
            this.Controls.Add(this.btnCrearPlato);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gbDatosBebida);
            this.Controls.Add(this.cbCategoriaC);
            this.Controls.Add(this.dgvComidas);
            this.Controls.Add(this.btnActualizarC);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormComidasMenuAdmin";
            this.Text = "Platos del menú";
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).EndInit();
            this.gbDatosBebida.ResumeLayout(false);
            this.gbDatosBebida.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbCategoriaC;
        private System.Windows.Forms.DataGridView dgvComidas;
        private System.Windows.Forms.GroupBox gbDatosBebida;
        private System.Windows.Forms.Label lblSeleccionBebida;
        private System.Windows.Forms.Button btnActualizarC;
        private System.Windows.Forms.ComboBox cbCategoriaB;
        private System.Windows.Forms.TextBox txtPrecioU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreC;
        private System.Windows.Forms.Label lbNombreBebida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcionC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCrearPlato;
        private System.Windows.Forms.Button btn_limpiarcampos;
    }
}