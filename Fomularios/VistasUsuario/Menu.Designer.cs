namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    partial class Menu
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
            this.btnBMU = new System.Windows.Forms.Button();
            this.btnEMU = new System.Windows.Forms.Button();
            this.btnCMU = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBMU
            // 
            this.btnBMU.BackColor = System.Drawing.Color.Goldenrod;
            this.btnBMU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBMU.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBMU.Location = new System.Drawing.Point(78, 147);
            this.btnBMU.Margin = new System.Windows.Forms.Padding(4);
            this.btnBMU.Name = "btnBMU";
            this.btnBMU.Size = new System.Drawing.Size(256, 76);
            this.btnBMU.TabIndex = 1;
            this.btnBMU.Text = "Bebidas";
            this.btnBMU.UseVisualStyleBackColor = false;
            this.btnBMU.Click += new System.EventHandler(this.btnBMU_Click);
            // 
            // btnEMU
            // 
            this.btnEMU.BackColor = System.Drawing.Color.Goldenrod;
            this.btnEMU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEMU.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEMU.Location = new System.Drawing.Point(365, 147);
            this.btnEMU.Margin = new System.Windows.Forms.Padding(4);
            this.btnEMU.Name = "btnEMU";
            this.btnEMU.Size = new System.Drawing.Size(265, 76);
            this.btnEMU.TabIndex = 2;
            this.btnEMU.Text = "Extras";
            this.btnEMU.UseVisualStyleBackColor = false;
            this.btnEMU.Click += new System.EventHandler(this.btnEMU_Click);
            // 
            // btnCMU
            // 
            this.btnCMU.BackColor = System.Drawing.Color.Goldenrod;
            this.btnCMU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCMU.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCMU.Location = new System.Drawing.Point(664, 147);
            this.btnCMU.Margin = new System.Windows.Forms.Padding(4);
            this.btnCMU.Name = "btnCMU";
            this.btnCMU.Size = new System.Drawing.Size(260, 76);
            this.btnCMU.TabIndex = 3;
            this.btnCMU.Text = "Comida";
            this.btnCMU.UseVisualStyleBackColor = false;
            this.btnCMU.Click += new System.EventHandler(this.btnCMU_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Brown;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalir.Location = new System.Drawing.Point(44, 320);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(160, 53);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(39, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 26);
            this.label2.TabIndex = 9;
            this.label2.Text = "MENÚ";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(997, 420);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnCMU);
            this.Controls.Add(this.btnEMU);
            this.Controls.Add(this.btnBMU);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Menu";
            this.Text = "Menú";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBMU;
        private System.Windows.Forms.Button btnEMU;
        private System.Windows.Forms.Button btnCMU;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label2;
    }
}