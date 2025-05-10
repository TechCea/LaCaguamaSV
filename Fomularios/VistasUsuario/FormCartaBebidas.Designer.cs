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
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartaB)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCartaB
            // 
            this.dgvCartaB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCartaB.Location = new System.Drawing.Point(56, 90);
            this.dgvCartaB.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCartaB.Name = "dgvCartaB";
            this.dgvCartaB.RowHeadersWidth = 51;
            this.dgvCartaB.Size = new System.Drawing.Size(911, 377);
            this.dgvCartaB.TabIndex = 0;
            this.dgvCartaB.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCartaB_CellContentClick);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRegresar.Location = new System.Drawing.Point(56, 496);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(176, 43);
            this.btnRegresar.TabIndex = 1;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // cbxFiltrarCB
            // 
            this.cbxFiltrarCB.FormattingEnabled = true;
            this.cbxFiltrarCB.Location = new System.Drawing.Point(805, 39);
            this.cbxFiltrarCB.Margin = new System.Windows.Forms.Padding(4);
            this.cbxFiltrarCB.Name = "cbxFiltrarCB";
            this.cbxFiltrarCB.Size = new System.Drawing.Size(160, 24);
            this.cbxFiltrarCB.TabIndex = 3;
            this.cbxFiltrarCB.SelectedIndexChanged += new System.EventHandler(this.cbxFiltrarCB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(537, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Filtrar por categoria";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(51, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 26);
            this.label2.TabIndex = 10;
            this.label2.Text = "BEBIDAS";
            // 
            // FormCartaBebidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxFiltrarCB);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.dgvCartaB);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormCartaBebidas";
            this.Text = "Bebidas del menú";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartaB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCartaB;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.ComboBox cbxFiltrarCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}