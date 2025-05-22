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
            this.btnRegresarInv = new System.Windows.Forms.Button();
            this.cbCategoriaC = new System.Windows.Forms.ComboBox();
            this.dgvComidas = new System.Windows.Forms.DataGridView();
            this.btnCrearEditarPlatos = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRegresarInv
            // 
            this.btnRegresarInv.BackColor = System.Drawing.Color.Snow;
            this.btnRegresarInv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresarInv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegresarInv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresarInv.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegresarInv.Location = new System.Drawing.Point(23, 595);
            this.btnRegresarInv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegresarInv.Name = "btnRegresarInv";
            this.btnRegresarInv.Size = new System.Drawing.Size(184, 58);
            this.btnRegresarInv.TabIndex = 10;
            this.btnRegresarInv.Text = "Regresar Inventario";
            this.btnRegresarInv.UseVisualStyleBackColor = false;
            this.btnRegresarInv.Click += new System.EventHandler(this.btnRegresarInv_Click);
            // 
            // cbCategoriaC
            // 
            this.cbCategoriaC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCategoriaC.FormattingEnabled = true;
            this.cbCategoriaC.Location = new System.Drawing.Point(777, 36);
            this.cbCategoriaC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategoriaC.Name = "cbCategoriaC";
            this.cbCategoriaC.Size = new System.Drawing.Size(154, 24);
            this.cbCategoriaC.TabIndex = 9;
            // 
            // dgvComidas
            // 
            this.dgvComidas.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvComidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComidas.Location = new System.Drawing.Point(22, 78);
            this.dgvComidas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvComidas.Name = "dgvComidas";
            this.dgvComidas.RowHeadersWidth = 51;
            this.dgvComidas.RowTemplate.Height = 24;
            this.dgvComidas.Size = new System.Drawing.Size(909, 479);
            this.dgvComidas.TabIndex = 8;
            // 
            // btnCrearEditarPlatos
            // 
            this.btnCrearEditarPlatos.BackColor = System.Drawing.Color.Gold;
            this.btnCrearEditarPlatos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearEditarPlatos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCrearEditarPlatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearEditarPlatos.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnCrearEditarPlatos.Location = new System.Drawing.Point(637, 595);
            this.btnCrearEditarPlatos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCrearEditarPlatos.Name = "btnCrearEditarPlatos";
            this.btnCrearEditarPlatos.Size = new System.Drawing.Size(294, 58);
            this.btnCrearEditarPlatos.TabIndex = 13;
            this.btnCrearEditarPlatos.Text = "Editar datos de los platos";
            this.btnCrearEditarPlatos.UseVisualStyleBackColor = false;
            this.btnCrearEditarPlatos.Click += new System.EventHandler(this.btnCrearEditarPlatos_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(18, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(447, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "Selecciona un plato para desplegar su receta";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Snow;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(229, 595);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 58);
            this.button1.TabIndex = 15;
            this.button1.Text = "Agregar una nueva categoría de platos";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormPlatosMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(958, 688);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRegresarInv);
            this.Controls.Add(this.cbCategoriaC);
            this.Controls.Add(this.dgvComidas);
            this.Controls.Add(this.btnCrearEditarPlatos);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormPlatosMenu";
            this.Text = "Platos del inventario";
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRegresarInv;
        private System.Windows.Forms.ComboBox cbCategoriaC;
        private System.Windows.Forms.DataGridView dgvComidas;
        private System.Windows.Forms.Button btnCrearEditarPlatos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}