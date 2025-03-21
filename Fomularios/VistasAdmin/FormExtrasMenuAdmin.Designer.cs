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
            this.lblSeleccionExtra = new System.Windows.Forms.Label();
            this.btnEliminarE = new System.Windows.Forms.Button();
            this.btnActualizarB = new System.Windows.Forms.Button();
            this.btnCrearExtra = new System.Windows.Forms.Button();
            this.lblNombreE = new System.Windows.Forms.Label();
            this.lblPrecioExtraU = new System.Windows.Forms.Label();
            this.txtNombreE = new System.Windows.Forms.TextBox();
            this.txtPrecioUE = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtras)).BeginInit();
            this.gbDatosExtras.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.Location = new System.Drawing.Point(37, 551);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(147, 34);
            this.btnRegresarMenu.TabIndex = 5;
            this.btnRegresarMenu.Text = "Regresar Menú";
            this.btnRegresarMenu.UseVisualStyleBackColor = true;
            // 
            // dgvExtras
            // 
            this.dgvExtras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtras.Location = new System.Drawing.Point(37, 101);
            this.dgvExtras.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvExtras.Name = "dgvExtras";
            this.dgvExtras.RowHeadersWidth = 51;
            this.dgvExtras.RowTemplate.Height = 24;
            this.dgvExtras.Size = new System.Drawing.Size(437, 412);
            this.dgvExtras.TabIndex = 3;
            // 
            // gbDatosExtras
            // 
            this.gbDatosExtras.Controls.Add(this.txtPrecioUE);
            this.gbDatosExtras.Controls.Add(this.txtNombreE);
            this.gbDatosExtras.Controls.Add(this.lblPrecioExtraU);
            this.gbDatosExtras.Controls.Add(this.lblSeleccionExtra);
            this.gbDatosExtras.Controls.Add(this.lblNombreE);
            this.gbDatosExtras.Controls.Add(this.btnCrearExtra);
            this.gbDatosExtras.Controls.Add(this.btnEliminarE);
            this.gbDatosExtras.Controls.Add(this.btnActualizarB);
            this.gbDatosExtras.Location = new System.Drawing.Point(500, 62);
            this.gbDatosExtras.Name = "gbDatosExtras";
            this.gbDatosExtras.Size = new System.Drawing.Size(439, 451);
            this.gbDatosExtras.TabIndex = 6;
            this.gbDatosExtras.TabStop = false;
            this.gbDatosExtras.Text = "Datos extras";
            // 
            // lblSeleccionExtra
            // 
            this.lblSeleccionExtra.AutoSize = true;
            this.lblSeleccionExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblSeleccionExtra.Location = new System.Drawing.Point(33, 53);
            this.lblSeleccionExtra.Name = "lblSeleccionExtra";
            this.lblSeleccionExtra.Size = new System.Drawing.Size(241, 18);
            this.lblSeleccionExtra.TabIndex = 8;
            this.lblSeleccionExtra.Text = "Selecciona una extra a editar/borrar";
            // 
            // btnEliminarE
            // 
            this.btnEliminarE.Location = new System.Drawing.Point(312, 367);
            this.btnEliminarE.Name = "btnEliminarE";
            this.btnEliminarE.Size = new System.Drawing.Size(103, 43);
            this.btnEliminarE.TabIndex = 7;
            this.btnEliminarE.Text = "Eliminar";
            this.btnEliminarE.UseVisualStyleBackColor = true;
            this.btnEliminarE.Click += new System.EventHandler(this.btnEliminarE_Click);
            // 
            // btnActualizarB
            // 
            this.btnActualizarB.Location = new System.Drawing.Point(177, 367);
            this.btnActualizarB.Name = "btnActualizarB";
            this.btnActualizarB.Size = new System.Drawing.Size(126, 43);
            this.btnActualizarB.TabIndex = 6;
            this.btnActualizarB.Text = "Actualizar datos";
            this.btnActualizarB.UseVisualStyleBackColor = true;
            this.btnActualizarB.Click += new System.EventHandler(this.btnActualizarB_Click);
            // 
            // btnCrearExtra
            // 
            this.btnCrearExtra.Location = new System.Drawing.Point(39, 367);
            this.btnCrearExtra.Name = "btnCrearExtra";
            this.btnCrearExtra.Size = new System.Drawing.Size(129, 43);
            this.btnCrearExtra.TabIndex = 9;
            this.btnCrearExtra.Text = "Añadir nuevo extra";
            this.btnCrearExtra.UseVisualStyleBackColor = true;
            this.btnCrearExtra.Click += new System.EventHandler(this.btnCrearExtra_Click);
            // 
            // lblNombreE
            // 
            this.lblNombreE.AutoSize = true;
            this.lblNombreE.Location = new System.Drawing.Point(33, 111);
            this.lblNombreE.Name = "lblNombreE";
            this.lblNombreE.Size = new System.Drawing.Size(113, 16);
            this.lblNombreE.TabIndex = 10;
            this.lblNombreE.Text = "Nombre del extra:";
            // 
            // lblPrecioExtraU
            // 
            this.lblPrecioExtraU.AutoSize = true;
            this.lblPrecioExtraU.Location = new System.Drawing.Point(36, 171);
            this.lblPrecioExtraU.Name = "lblPrecioExtraU";
            this.lblPrecioExtraU.Size = new System.Drawing.Size(95, 16);
            this.lblPrecioExtraU.TabIndex = 11;
            this.lblPrecioExtraU.Text = "Precio unitario:";
            // 
            // txtNombreE
            // 
            this.txtNombreE.Location = new System.Drawing.Point(222, 105);
            this.txtNombreE.Name = "txtNombreE";
            this.txtNombreE.Size = new System.Drawing.Size(147, 22);
            this.txtNombreE.TabIndex = 12;
            // 
            // txtPrecioUE
            // 
            this.txtPrecioUE.Location = new System.Drawing.Point(222, 165);
            this.txtPrecioUE.Name = "txtPrecioUE";
            this.txtPrecioUE.Size = new System.Drawing.Size(147, 22);
            this.txtPrecioUE.TabIndex = 13;
            // 
            // FormExtrasMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 614);
            this.Controls.Add(this.gbDatosExtras);
            this.Controls.Add(this.btnRegresarMenu);
            this.Controls.Add(this.dgvExtras);
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
        private System.Windows.Forms.Button btnCrearExtra;
        private System.Windows.Forms.Label lblPrecioExtraU;
        private System.Windows.Forms.Label lblNombreE;
        private System.Windows.Forms.TextBox txtPrecioUE;
        private System.Windows.Forms.TextBox txtNombreE;
    }
}