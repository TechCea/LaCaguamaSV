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
            this.btnBebidas.Location = new System.Drawing.Point(158, 172);
            this.btnBebidas.Name = "btnBebidas";
            this.btnBebidas.Size = new System.Drawing.Size(103, 52);
            this.btnBebidas.TabIndex = 0;
            this.btnBebidas.Text = "Bebidas";
            this.btnBebidas.UseVisualStyleBackColor = true;
            this.btnBebidas.Click += new System.EventHandler(this.btnBebidas_Click);
            // 
            // btnExtras
            // 
            this.btnExtras.Location = new System.Drawing.Point(346, 171);
            this.btnExtras.Name = "btnExtras";
            this.btnExtras.Size = new System.Drawing.Size(90, 53);
            this.btnExtras.TabIndex = 1;
            this.btnExtras.Text = "Extras";
            this.btnExtras.UseVisualStyleBackColor = true;
            this.btnExtras.Click += new System.EventHandler(this.btnExtras_Click);
            // 
            // btnComidas
            // 
            this.btnComidas.Location = new System.Drawing.Point(540, 172);
            this.btnComidas.Name = "btnComidas";
            this.btnComidas.Size = new System.Drawing.Size(99, 52);
            this.btnComidas.TabIndex = 2;
            this.btnComidas.Text = "Comidas";
            this.btnComidas.UseVisualStyleBackColor = true;
            this.btnComidas.Click += new System.EventHandler(this.btnComidas_Click);
            // 
            // btnRegresarAdmin
            // 
            this.btnRegresarAdmin.Location = new System.Drawing.Point(46, 383);
            this.btnRegresarAdmin.Name = "btnRegresarAdmin";
            this.btnRegresarAdmin.Size = new System.Drawing.Size(180, 25);
            this.btnRegresarAdmin.TabIndex = 3;
            this.btnRegresarAdmin.Text = "Regresar pantalla Admin";
            this.btnRegresarAdmin.UseVisualStyleBackColor = true;
            // 
            // FormMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRegresarAdmin);
            this.Controls.Add(this.btnComidas);
            this.Controls.Add(this.btnExtras);
            this.Controls.Add(this.btnBebidas);
            this.Name = "FormMenuAdmin";
            this.Text = "FormMenuAdmin";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBebidas;
        private System.Windows.Forms.Button btnExtras;
        private System.Windows.Forms.Button btnComidas;
        private System.Windows.Forms.Button btnRegresarAdmin;
    }
}