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
            this.gbDatosBebida = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbDatosBebida.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBebidas
            // 
            this.btnBebidas.BackColor = System.Drawing.Color.Yellow;
            this.btnBebidas.Location = new System.Drawing.Point(296, 167);
            this.btnBebidas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBebidas.Name = "btnBebidas";
            this.btnBebidas.Size = new System.Drawing.Size(121, 69);
            this.btnBebidas.TabIndex = 0;
            this.btnBebidas.Text = "Bebidas";
            this.btnBebidas.UseVisualStyleBackColor = false;
            this.btnBebidas.Click += new System.EventHandler(this.btnBebidas_Click);
            // 
            // btnExtras
            // 
            this.btnExtras.BackColor = System.Drawing.Color.Yellow;
            this.btnExtras.Location = new System.Drawing.Point(538, 167);
            this.btnExtras.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExtras.Name = "btnExtras";
            this.btnExtras.Size = new System.Drawing.Size(109, 70);
            this.btnExtras.TabIndex = 1;
            this.btnExtras.Text = "Extras";
            this.btnExtras.UseVisualStyleBackColor = false;
            this.btnExtras.Click += new System.EventHandler(this.btnExtras_Click);
            // 
            // btnComidas
            // 
            this.btnComidas.BackColor = System.Drawing.Color.Yellow;
            this.btnComidas.Location = new System.Drawing.Point(67, 168);
            this.btnComidas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnComidas.Name = "btnComidas";
            this.btnComidas.Size = new System.Drawing.Size(117, 69);
            this.btnComidas.TabIndex = 2;
            this.btnComidas.Text = "Comidas";
            this.btnComidas.UseVisualStyleBackColor = false;
            this.btnComidas.Click += new System.EventHandler(this.btnComidas_Click);
            // 
            // gbDatosBebida
            // 
            this.gbDatosBebida.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gbDatosBebida.Controls.Add(this.label1);
            this.gbDatosBebida.Controls.Add(this.btnComidas);
            this.gbDatosBebida.Controls.Add(this.btnBebidas);
            this.gbDatosBebida.Controls.Add(this.btnExtras);
            this.gbDatosBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosBebida.Location = new System.Drawing.Point(33, 40);
            this.gbDatosBebida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosBebida.Name = "gbDatosBebida";
            this.gbDatosBebida.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosBebida.Size = new System.Drawing.Size(711, 355);
            this.gbDatosBebida.TabIndex = 7;
            this.gbDatosBebida.TabStop = false;
            this.gbDatosBebida.Text = "Menú";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label1.Location = new System.Drawing.Point(150, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seleccione la parte del menú a gestionar";
            // 
            // FormMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbDatosBebida);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMenuAdmin";
            this.Text = "FormMenuAdmin";
            this.Load += new System.EventHandler(this.FormMenuAdmin_Load);
            this.gbDatosBebida.ResumeLayout(false);
            this.gbDatosBebida.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBebidas;
        private System.Windows.Forms.Button btnExtras;
        private System.Windows.Forms.Button btnComidas;
        private System.Windows.Forms.GroupBox gbDatosBebida;
        private System.Windows.Forms.Label label1;
    }
}