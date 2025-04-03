namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormBebidasMenuAdmin
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
            this.dgvBebidas = new System.Windows.Forms.DataGridView();
            this.cbCategoria = new System.Windows.Forms.ComboBox();
            this.btnRegresarMenu = new System.Windows.Forms.Button();
            this.gbDatosBebida = new System.Windows.Forms.GroupBox();
            this.lblSeleccionBebida = new System.Windows.Forms.Label();
            this.cbCategoriaB = new System.Windows.Forms.ComboBox();
            this.txtPrecioU = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreB = new System.Windows.Forms.TextBox();
            this.lbNombreBebida = new System.Windows.Forms.Label();
            this.btnEliminarB = new System.Windows.Forms.Button();
            this.btnActualizarB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBebidas)).BeginInit();
            this.gbDatosBebida.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBebidas
            // 
            this.dgvBebidas.BackgroundColor = System.Drawing.Color.Gray;
            this.dgvBebidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBebidas.Location = new System.Drawing.Point(48, 54);
            this.dgvBebidas.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBebidas.Name = "dgvBebidas";
            this.dgvBebidas.RowHeadersWidth = 51;
            this.dgvBebidas.RowTemplate.Height = 24;
            this.dgvBebidas.Size = new System.Drawing.Size(436, 335);
            this.dgvBebidas.TabIndex = 0;
            this.dgvBebidas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBebidas_CellContentClick);
            // 
            // cbCategoria
            // 
            this.cbCategoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCategoria.FormattingEnabled = true;
            this.cbCategoria.Location = new System.Drawing.Point(345, 11);
            this.cbCategoria.Margin = new System.Windows.Forms.Padding(2);
            this.cbCategoria.Name = "cbCategoria";
            this.cbCategoria.Size = new System.Drawing.Size(139, 21);
            this.cbCategoria.TabIndex = 1;
            this.cbCategoria.SelectedIndexChanged += new System.EventHandler(this.cbCategoria_SelectedIndexChanged_1);
            // 
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.BackColor = System.Drawing.Color.White;
            this.btnRegresarMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresarMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRegresarMenu.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnRegresarMenu.Location = new System.Drawing.Point(48, 419);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(118, 49);
            this.btnRegresarMenu.TabIndex = 2;
            this.btnRegresarMenu.Text = "Regresar Menú";
            this.btnRegresarMenu.UseVisualStyleBackColor = false;
            this.btnRegresarMenu.Click += new System.EventHandler(this.btnRegresarMenu_Click);
            // 
            // gbDatosBebida
            // 
            this.gbDatosBebida.BackColor = System.Drawing.Color.Gray;
            this.gbDatosBebida.Controls.Add(this.lblSeleccionBebida);
            this.gbDatosBebida.Controls.Add(this.cbCategoriaB);
            this.gbDatosBebida.Controls.Add(this.txtPrecioU);
            this.gbDatosBebida.Controls.Add(this.label2);
            this.gbDatosBebida.Controls.Add(this.label1);
            this.gbDatosBebida.Controls.Add(this.txtNombreB);
            this.gbDatosBebida.Controls.Add(this.lbNombreBebida);
            this.gbDatosBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gbDatosBebida.ForeColor = System.Drawing.Color.White;
            this.gbDatosBebida.Location = new System.Drawing.Point(517, 54);
            this.gbDatosBebida.Margin = new System.Windows.Forms.Padding(2);
            this.gbDatosBebida.Name = "gbDatosBebida";
            this.gbDatosBebida.Padding = new System.Windows.Forms.Padding(2);
            this.gbDatosBebida.Size = new System.Drawing.Size(329, 284);
            this.gbDatosBebida.TabIndex = 3;
            this.gbDatosBebida.TabStop = false;
            this.gbDatosBebida.Text = "Datos bebida";
            // 
            // lblSeleccionBebida
            // 
            this.lblSeleccionBebida.AutoSize = true;
            this.lblSeleccionBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionBebida.ForeColor = System.Drawing.Color.White;
            this.lblSeleccionBebida.Location = new System.Drawing.Point(22, 32);
            this.lblSeleccionBebida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeleccionBebida.Name = "lblSeleccionBebida";
            this.lblSeleccionBebida.Size = new System.Drawing.Size(247, 17);
            this.lblSeleccionBebida.TabIndex = 8;
            this.lblSeleccionBebida.Text = "Selecciona una bebida a editar/borrar";
            // 
            // cbCategoriaB
            // 
            this.cbCategoriaB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCategoriaB.FormattingEnabled = true;
            this.cbCategoriaB.Location = new System.Drawing.Point(131, 145);
            this.cbCategoriaB.Margin = new System.Windows.Forms.Padding(2);
            this.cbCategoriaB.Name = "cbCategoriaB";
            this.cbCategoriaB.Size = new System.Drawing.Size(170, 24);
            this.cbCategoriaB.TabIndex = 5;
            this.cbCategoriaB.SelectedIndexChanged += new System.EventHandler(this.cbCategoriaB_SelectedIndexChanged);
            // 
            // txtPrecioU
            // 
            this.txtPrecioU.Location = new System.Drawing.Point(131, 193);
            this.txtPrecioU.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecioU.Name = "txtPrecioU";
            this.txtPrecioU.Size = new System.Drawing.Size(171, 23);
            this.txtPrecioU.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(28, 198);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Precio unitario:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Categoría:";
            // 
            // txtNombreB
            // 
            this.txtNombreB.Location = new System.Drawing.Point(131, 90);
            this.txtNombreB.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombreB.Name = "txtNombreB";
            this.txtNombreB.Size = new System.Drawing.Size(171, 23);
            this.txtNombreB.TabIndex = 1;
            // 
            // lbNombreBebida
            // 
            this.lbNombreBebida.AutoSize = true;
            this.lbNombreBebida.ForeColor = System.Drawing.Color.White;
            this.lbNombreBebida.Location = new System.Drawing.Point(28, 90);
            this.lbNombreBebida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNombreBebida.Name = "lbNombreBebida";
            this.lbNombreBebida.Size = new System.Drawing.Size(109, 17);
            this.lbNombreBebida.TabIndex = 0;
            this.lbNombreBebida.Text = "Nombre bebida:";
            // 
            // btnEliminarB
            // 
            this.btnEliminarB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnEliminarB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEliminarB.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnEliminarB.Location = new System.Drawing.Point(701, 353);
            this.btnEliminarB.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarB.Name = "btnEliminarB";
            this.btnEliminarB.Size = new System.Drawing.Size(118, 49);
            this.btnEliminarB.TabIndex = 7;
            this.btnEliminarB.Text = "Eliminar";
            this.btnEliminarB.UseVisualStyleBackColor = false;
            this.btnEliminarB.Click += new System.EventHandler(this.btnEliminarB_Click);
            // 
            // btnActualizarB
            // 
            this.btnActualizarB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnActualizarB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnActualizarB.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnActualizarB.Location = new System.Drawing.Point(542, 353);
            this.btnActualizarB.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualizarB.Name = "btnActualizarB";
            this.btnActualizarB.Size = new System.Drawing.Size(118, 49);
            this.btnActualizarB.TabIndex = 6;
            this.btnActualizarB.Text = "Actualizar datos";
            this.btnActualizarB.UseVisualStyleBackColor = false;
            this.btnActualizarB.Click += new System.EventHandler(this.btnActualizarB_Click);
            // 
            // FormBebidasMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(875, 505);
            this.Controls.Add(this.gbDatosBebida);
            this.Controls.Add(this.btnEliminarB);
            this.Controls.Add(this.btnRegresarMenu);
            this.Controls.Add(this.btnActualizarB);
            this.Controls.Add(this.cbCategoria);
            this.Controls.Add(this.dgvBebidas);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormBebidasMenuAdmin";
            this.Text = "FormBebidasMenuAdmin";
            this.Load += new System.EventHandler(this.FormBebidasMenuAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBebidas)).EndInit();
            this.gbDatosBebida.ResumeLayout(false);
            this.gbDatosBebida.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBebidas;
        private System.Windows.Forms.ComboBox cbCategoria;
        private System.Windows.Forms.Button btnRegresarMenu;
        private System.Windows.Forms.GroupBox gbDatosBebida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreB;
        private System.Windows.Forms.Label lbNombreBebida;
        private System.Windows.Forms.Button btnEliminarB;
        private System.Windows.Forms.Button btnActualizarB;
        private System.Windows.Forms.ComboBox cbCategoriaB;
        private System.Windows.Forms.TextBox txtPrecioU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSeleccionBebida;
    }
}