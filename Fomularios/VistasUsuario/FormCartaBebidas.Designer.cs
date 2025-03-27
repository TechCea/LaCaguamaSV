namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    partial class FormCartaBebidas
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
            this.dgvCartaB = new System.Windows.Forms.DataGridView();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.cbxFiltrarCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartaB)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCartaB
            // 
            this.dgvCartaB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCartaB.Location = new System.Drawing.Point(42, 73);
            this.dgvCartaB.Name = "dgvCartaB";
            this.dgvCartaB.Size = new System.Drawing.Size(683, 306);
            this.dgvCartaB.TabIndex = 0;
            this.dgvCartaB.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCartaB_CellContentClick);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegresar.Location = new System.Drawing.Point(42, 403);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(132, 35);
            this.btnRegresar.TabIndex = 1;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // cbxFiltrarCB
            // 
            this.cbxFiltrarCB.FormattingEnabled = true;
            this.cbxFiltrarCB.Location = new System.Drawing.Point(604, 32);
            this.cbxFiltrarCB.Name = "cbxFiltrarCB";
            this.cbxFiltrarCB.Size = new System.Drawing.Size(121, 21);
            this.cbxFiltrarCB.TabIndex = 3;
            this.cbxFiltrarCB.SelectedIndexChanged += new System.EventHandler(this.cbxFiltrarCB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(403, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "Filtrar por categoria";
            // 
            // FormCartaBebidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxFiltrarCB);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.dgvCartaB);
            this.Name = "FormCartaBebidas";
            this.Text = "FormCartaBebidas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartaB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCartaB;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.ComboBox cbxFiltrarCB;
        private System.Windows.Forms.Label label1;
    }
}