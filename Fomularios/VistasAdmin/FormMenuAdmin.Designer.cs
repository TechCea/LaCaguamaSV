namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormMenuAdmin
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
            this.btnBebidas = new System.Windows.Forms.Button();
            this.btnExtras = new System.Windows.Forms.Button();
            this.btnComidas = new System.Windows.Forms.Button();
            this.btnRegresarAdmin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBebidas
            // 
            this.btnBebidas.Location = new System.Drawing.Point(118, 140);
            this.btnBebidas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBebidas.Name = "btnBebidas";
            this.btnBebidas.Size = new System.Drawing.Size(77, 42);
            this.btnBebidas.TabIndex = 0;
            this.btnBebidas.Text = "Bebidas";
            this.btnBebidas.UseVisualStyleBackColor = true;
            this.btnBebidas.Click += new System.EventHandler(this.btnBebidas_Click);
            // 
            // btnExtras
            // 
            this.btnExtras.Location = new System.Drawing.Point(260, 139);
            this.btnExtras.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExtras.Name = "btnExtras";
            this.btnExtras.Size = new System.Drawing.Size(68, 43);
            this.btnExtras.TabIndex = 1;
            this.btnExtras.Text = "Extras";
            this.btnExtras.UseVisualStyleBackColor = true;
            this.btnExtras.Click += new System.EventHandler(this.btnExtras_Click);
            // 
            // btnComidas
            // 
            this.btnComidas.Location = new System.Drawing.Point(405, 140);
            this.btnComidas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnComidas.Name = "btnComidas";
            this.btnComidas.Size = new System.Drawing.Size(74, 42);
            this.btnComidas.TabIndex = 2;
            this.btnComidas.Text = "Comidas";
            this.btnComidas.UseVisualStyleBackColor = true;
            this.btnComidas.Click += new System.EventHandler(this.btnComidas_Click);
            // 
            // btnRegresarAdmin
            // 
            this.btnRegresarAdmin.Location = new System.Drawing.Point(34, 311);
            this.btnRegresarAdmin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRegresarAdmin.Name = "btnRegresarAdmin";
            this.btnRegresarAdmin.Size = new System.Drawing.Size(135, 20);
            this.btnRegresarAdmin.TabIndex = 3;
            this.btnRegresarAdmin.Text = "Regresar pantalla Admin";
            this.btnRegresarAdmin.UseVisualStyleBackColor = true;
            // 
            // FormMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.btnRegresarAdmin);
            this.Controls.Add(this.btnComidas);
            this.Controls.Add(this.btnExtras);
            this.Controls.Add(this.btnBebidas);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormMenuAdmin";
            this.Text = "FormMenuAdmin";
            this.Load += new System.EventHandler(this.FormMenuAdmin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBebidas;
        private System.Windows.Forms.Button btnExtras;
        private System.Windows.Forms.Button btnComidas;
        private System.Windows.Forms.Button btnRegresarAdmin;
    }
}