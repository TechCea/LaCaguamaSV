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
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.Location = new System.Drawing.Point(44, 398);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(110, 28);
            this.btnRegresarMenu.TabIndex = 5;
            this.btnRegresarMenu.Text = "Regresar Menú";
            this.btnRegresarMenu.UseVisualStyleBackColor = true;
            this.btnRegresarMenu.Click += new System.EventHandler(this.btnRegresarMenu_Click);
            // 
            // cbCategoriaC
            // 
            this.cbCategoriaC.FormattingEnabled = true;
            this.cbCategoriaC.Location = new System.Drawing.Point(519, 20);
            this.cbCategoriaC.Margin = new System.Windows.Forms.Padding(2);
            this.cbCategoriaC.Name = "cbCategoriaC";
            this.cbCategoriaC.Size = new System.Drawing.Size(92, 21);
            this.cbCategoriaC.TabIndex = 4;
            this.cbCategoriaC.SelectedIndexChanged += new System.EventHandler(this.cbCategoriaC_SelectedIndexChanged);
            // 
            // dgvComidas
            // 
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
            // FormComidasMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRegresarMenu);
            this.Controls.Add(this.cbCategoriaC);
            this.Controls.Add(this.dgvComidas);
            this.Name = "FormComidasMenuAdmin";
            this.Text = "FormComidasMenuAdmin";
            ((System.ComponentModel.ISupportInitialize)(this.dgvComidas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegresarMenu;
        private System.Windows.Forms.ComboBox cbCategoriaC;
        private System.Windows.Forms.DataGridView dgvComidas;
    }
}