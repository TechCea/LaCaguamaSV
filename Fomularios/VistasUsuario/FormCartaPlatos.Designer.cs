namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    partial class FormCartaPlatos
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
            this.cbxFiltrarCP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.dgvCartaP = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartaP)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxFiltrarCP
            // 
            this.cbxFiltrarCP.FormattingEnabled = true;
            this.cbxFiltrarCP.Location = new System.Drawing.Point(828, 27);
            this.cbxFiltrarCP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxFiltrarCP.Name = "cbxFiltrarCP";
            this.cbxFiltrarCP.Size = new System.Drawing.Size(160, 24);
            this.cbxFiltrarCP.TabIndex = 7;
            this.cbxFiltrarCP.SelectedIndexChanged += new System.EventHandler(this.cbxFiltrarCP_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(572, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Filtrar por categoria";
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegresar.Location = new System.Drawing.Point(79, 484);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(176, 43);
            this.btnRegresar.TabIndex = 5;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // dgvCartaP
            // 
            this.dgvCartaP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCartaP.Location = new System.Drawing.Point(79, 78);
            this.dgvCartaP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvCartaP.Name = "dgvCartaP";
            this.dgvCartaP.RowHeadersWidth = 51;
            this.dgvCartaP.Size = new System.Drawing.Size(911, 377);
            this.dgvCartaP.TabIndex = 4;
            this.dgvCartaP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCartaP_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(74, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "PLATOS";
            // 
            // FormCartaPlatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxFiltrarCP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.dgvCartaP);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormCartaPlatos";
            this.Text = "Platos del menú";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartaP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxFiltrarCP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.DataGridView dgvCartaP;
        private System.Windows.Forms.Label label2;
    }
}