namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormExtrasMenuAdmin
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
            this.dgvExtras = new System.Windows.Forms.DataGridView();
            this.gbDatosExtras = new System.Windows.Forms.GroupBox();
            this.txtPrecioUE = new System.Windows.Forms.TextBox();
            this.txtNombreE = new System.Windows.Forms.TextBox();
            this.lblPrecioExtraU = new System.Windows.Forms.Label();
            this.lblSeleccionExtra = new System.Windows.Forms.Label();
            this.lblNombreE = new System.Windows.Forms.Label();
            this.btnEliminarE = new System.Windows.Forms.Button();
            this.btnActualizarB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtras)).BeginInit();
            this.gbDatosExtras.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnRegresarMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRegresarMenu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRegresarMenu.Location = new System.Drawing.Point(37, 538);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(177, 47);
            this.btnRegresarMenu.TabIndex = 5;
            this.btnRegresarMenu.Text = "Regresar Menú";
            this.btnRegresarMenu.UseVisualStyleBackColor = false;
            this.btnRegresarMenu.Click += new System.EventHandler(this.btnRegresarMenu_Click);
            // 
            // dgvExtras
            // 
            this.dgvExtras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtras.Location = new System.Drawing.Point(37, 62);
            this.dgvExtras.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvExtras.Name = "dgvExtras";
            this.dgvExtras.RowHeadersWidth = 51;
            this.dgvExtras.RowTemplate.Height = 24;
            this.dgvExtras.Size = new System.Drawing.Size(437, 412);
            this.dgvExtras.TabIndex = 3;
            // 
            // gbDatosExtras
            // 
            this.gbDatosExtras.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gbDatosExtras.Controls.Add(this.txtPrecioUE);
            this.gbDatosExtras.Controls.Add(this.txtNombreE);
            this.gbDatosExtras.Controls.Add(this.lblPrecioExtraU);
            this.gbDatosExtras.Controls.Add(this.lblSeleccionExtra);
            this.gbDatosExtras.Controls.Add(this.lblNombreE);
            this.gbDatosExtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gbDatosExtras.Location = new System.Drawing.Point(500, 62);
            this.gbDatosExtras.Name = "gbDatosExtras";
            this.gbDatosExtras.Size = new System.Drawing.Size(439, 289);
            this.gbDatosExtras.TabIndex = 6;
            this.gbDatosExtras.TabStop = false;
            this.gbDatosExtras.Text = "Datos extras";
            // 
            // txtPrecioUE
            // 
            this.txtPrecioUE.Location = new System.Drawing.Point(222, 165);
            this.txtPrecioUE.Name = "txtPrecioUE";
            this.txtPrecioUE.Size = new System.Drawing.Size(147, 26);
            this.txtPrecioUE.TabIndex = 13;
            // 
            // txtNombreE
            // 
            this.txtNombreE.Location = new System.Drawing.Point(222, 105);
            this.txtNombreE.Name = "txtNombreE";
            this.txtNombreE.Size = new System.Drawing.Size(147, 26);
            this.txtNombreE.TabIndex = 12;
            // 
            // lblPrecioExtraU
            // 
            this.lblPrecioExtraU.AutoSize = true;
            this.lblPrecioExtraU.Location = new System.Drawing.Point(33, 171);
            this.lblPrecioExtraU.Name = "lblPrecioExtraU";
            this.lblPrecioExtraU.Size = new System.Drawing.Size(122, 20);
            this.lblPrecioExtraU.TabIndex = 11;
            this.lblPrecioExtraU.Text = "Precio unitario:";
            // 
            // lblSeleccionExtra
            // 
            this.lblSeleccionExtra.AutoSize = true;
            this.lblSeleccionExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionExtra.Location = new System.Drawing.Point(33, 53);
            this.lblSeleccionExtra.Name = "lblSeleccionExtra";
            this.lblSeleccionExtra.Size = new System.Drawing.Size(276, 20);
            this.lblSeleccionExtra.TabIndex = 8;
            this.lblSeleccionExtra.Text = "Selecciona una extra a editar/borrar";
            // 
            // lblNombreE
            // 
            this.lblNombreE.AutoSize = true;
            this.lblNombreE.Location = new System.Drawing.Point(33, 111);
            this.lblNombreE.Name = "lblNombreE";
            this.lblNombreE.Size = new System.Drawing.Size(142, 20);
            this.lblNombreE.TabIndex = 10;
            this.lblNombreE.Text = "Nombre del extra:";
            // 
            // btnEliminarE
            // 
            this.btnEliminarE.BackColor = System.Drawing.Color.Brown;
            this.btnEliminarE.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnEliminarE.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminarE.Location = new System.Drawing.Point(500, 420);
            this.btnEliminarE.Name = "btnEliminarE";
            this.btnEliminarE.Size = new System.Drawing.Size(106, 54);
            this.btnEliminarE.TabIndex = 7;
            this.btnEliminarE.Text = "Eliminar";
            this.btnEliminarE.UseVisualStyleBackColor = false;
            this.btnEliminarE.Click += new System.EventHandler(this.btnEliminarE_Click);
            // 
            // btnActualizarB
            // 
            this.btnActualizarB.BackColor = System.Drawing.Color.ForestGreen;
            this.btnActualizarB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnActualizarB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActualizarB.Location = new System.Drawing.Point(798, 420);
            this.btnActualizarB.Name = "btnActualizarB";
            this.btnActualizarB.Size = new System.Drawing.Size(141, 54);
            this.btnActualizarB.TabIndex = 6;
            this.btnActualizarB.Text = "Actualizar datos";
            this.btnActualizarB.UseVisualStyleBackColor = false;
            this.btnActualizarB.Click += new System.EventHandler(this.btnActualizarB_Click);
            // 
            // FormExtrasMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(976, 614);
            this.Controls.Add(this.gbDatosExtras);
            this.Controls.Add(this.btnRegresarMenu);
            this.Controls.Add(this.dgvExtras);
            this.Controls.Add(this.btnEliminarE);
            this.Controls.Add(this.btnActualizarB);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormExtrasMenuAdmin";
            this.Text = "FormExtrasMenuAdmin";
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtras)).EndInit();
            this.gbDatosExtras.ResumeLayout(false);
            this.gbDatosExtras.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegresarMenu;
        private System.Windows.Forms.DataGridView dgvExtras;
        private System.Windows.Forms.GroupBox gbDatosExtras;
        private System.Windows.Forms.Label lblSeleccionExtra;
        private System.Windows.Forms.Button btnEliminarE;
        private System.Windows.Forms.Button btnActualizarB;
        private System.Windows.Forms.Label lblPrecioExtraU;
        private System.Windows.Forms.Label lblNombreE;
        private System.Windows.Forms.TextBox txtPrecioUE;
        private System.Windows.Forms.TextBox txtNombreE;
    }
}