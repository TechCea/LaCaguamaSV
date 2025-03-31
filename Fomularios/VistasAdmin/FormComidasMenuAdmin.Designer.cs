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
            this.btnaAgregarC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).BeginInit();
            this.gbDatosBebida.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnRegresarMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresarMenu.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegresarMenu.Location = new System.Drawing.Point(71, 511);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(184, 58);
            this.btnRegresarMenu.TabIndex = 5;
            this.btnRegresarMenu.Text = "Regresar Menú";
            this.btnRegresarMenu.UseVisualStyleBackColor = false;
            this.btnRegresarMenu.Click += new System.EventHandler(this.btnRegresarMenu_Click);
            // 
            // cbCategoriaC
            // 
            this.cbCategoriaC.FormattingEnabled = true;
            this.cbCategoriaC.Location = new System.Drawing.Point(692, 25);
            this.cbCategoriaC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategoriaC.Name = "cbCategoriaC";
            this.cbCategoriaC.Size = new System.Drawing.Size(121, 24);
            this.cbCategoriaC.TabIndex = 4;
            this.cbCategoriaC.SelectedIndexChanged += new System.EventHandler(this.cbCategoriaC_SelectedIndexChanged);
            // 
            // dgvComidas
            // 
            this.dgvComidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComidas.Location = new System.Drawing.Point(71, 66);
            this.dgvComidas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvComidas.Name = "dgvComidas";
            this.dgvComidas.RowHeadersWidth = 51;
            this.dgvComidas.RowTemplate.Height = 24;
            this.dgvComidas.Size = new System.Drawing.Size(741, 412);
            this.dgvComidas.TabIndex = 3;
            this.dgvComidas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComidas_CellContentClick);
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
            this.gbDatosBebida.Controls.Add(this.label1);
            this.gbDatosBebida.Controls.Add(this.txtNombreC);
            this.gbDatosBebida.Controls.Add(this.lbNombreBebida);
            this.gbDatosBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.txtDescripcionC.Location = new System.Drawing.Point(165, 223);
            this.txtDescripcionC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescripcionC.Name = "txtDescripcionC";
            this.txtDescripcionC.Size = new System.Drawing.Size(229, 26);
            this.txtDescripcionC.TabIndex = 11;
            this.txtDescripcionC.TextChanged += new System.EventHandler(this.txtDescripcionC_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Descripción:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblSeleccionBebida
            // 
            this.lblSeleccionBebida.AutoSize = true;
            this.lblSeleccionBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionBebida.Location = new System.Drawing.Point(29, 39);
            this.lblSeleccionBebida.Name = "lblSeleccionBebida";
            this.lblSeleccionBebida.Size = new System.Drawing.Size(278, 20);
            this.lblSeleccionBebida.TabIndex = 8;
            this.lblSeleccionBebida.Text = "Selecciona un platillo a editar/borrar";
            // 
            // cbCategoriaB
            // 
            this.cbCategoriaB.FormattingEnabled = true;
            this.cbCategoriaB.Location = new System.Drawing.Point(168, 149);
            this.cbCategoriaB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategoriaB.Name = "cbCategoriaB";
            this.cbCategoriaB.Size = new System.Drawing.Size(225, 28);
            this.cbCategoriaB.TabIndex = 5;
            this.cbCategoriaB.SelectedIndexChanged += new System.EventHandler(this.cbCategoriaB_SelectedIndexChanged);
            // 
            // txtPrecioU
            // 
            this.txtPrecioU.Location = new System.Drawing.Point(168, 311);
            this.txtPrecioU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPrecioU.Name = "txtPrecioU";
            this.txtPrecioU.Size = new System.Drawing.Size(227, 26);
            this.txtPrecioU.TabIndex = 4;
            this.txtPrecioU.TextChanged += new System.EventHandler(this.txtPrecioU_TextChanged);
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
            this.txtNombreC.Size = new System.Drawing.Size(227, 26);
            this.txtNombreC.TabIndex = 1;
            this.txtNombreC.TextChanged += new System.EventHandler(this.txtNombreC_TextChanged);
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
            // btnEliminarC
            // 
            this.btnEliminarC.BackColor = System.Drawing.Color.Brown;
            this.btnEliminarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEliminarC.Location = new System.Drawing.Point(1020, 511);
            this.btnEliminarC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEliminarC.Name = "btnEliminarC";
            this.btnEliminarC.Size = new System.Drawing.Size(124, 58);
            this.btnEliminarC.TabIndex = 7;
            this.btnEliminarC.Text = "Eliminar";
            this.btnEliminarC.UseVisualStyleBackColor = false;
            this.btnEliminarC.Click += new System.EventHandler(this.btnEliminarC_Click);
            // 
            // btnActualizarC
            // 
            this.btnActualizarC.BackColor = System.Drawing.Color.ForestGreen;
            this.btnActualizarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizarC.Location = new System.Drawing.Point(852, 511);
            this.btnActualizarC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnActualizarC.Name = "btnActualizarC";
            this.btnActualizarC.Size = new System.Drawing.Size(139, 58);
            this.btnActualizarC.TabIndex = 6;
            this.btnActualizarC.Text = "Actualizar datos";
            this.btnActualizarC.UseVisualStyleBackColor = false;
            this.btnActualizarC.Click += new System.EventHandler(this.btnActualizarC_Click);
            // 
            // btnaAgregarC
            // 
            this.btnaAgregarC.BackColor = System.Drawing.Color.ForestGreen;
            this.btnaAgregarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaAgregarC.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnaAgregarC.Location = new System.Drawing.Point(1172, 511);
            this.btnaAgregarC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnaAgregarC.Name = "btnaAgregarC";
            this.btnaAgregarC.Size = new System.Drawing.Size(119, 58);
            this.btnaAgregarC.TabIndex = 9;
            this.btnaAgregarC.Text = "Agregar";
            this.btnaAgregarC.UseVisualStyleBackColor = false;
            this.btnaAgregarC.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormComidasMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1321, 598);
            this.Controls.Add(this.gbDatosBebida);
            this.Controls.Add(this.btnRegresarMenu);
            this.Controls.Add(this.btnaAgregarC);
            this.Controls.Add(this.cbCategoriaC);
            this.Controls.Add(this.dgvComidas);
            this.Controls.Add(this.btnEliminarC);
            this.Controls.Add(this.btnActualizarC);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormComidasMenuAdmin";
            this.Text = "FormComidasMenuAdmin";
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
        private System.Windows.Forms.Button btnaAgregarC;
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