namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormAgregarUnidades
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
            this.label4 = new System.Windows.Forms.Label();
            this.gbDatosBebida = new System.Windows.Forms.GroupBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCrearU = new System.Windows.Forms.Button();
            this.lblSeleccionBebida = new System.Windows.Forms.Label();
            this.txtNuevaU = new System.Windows.Forms.TextBox();
            this.lbNombreBebida = new System.Windows.Forms.Label();
            this.dgvUnidades = new System.Windows.Forms.DataGridView();
            this.gbDatosBebida.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnidades)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(34, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(331, 25);
            this.label4.TabIndex = 21;
            this.label4.Text = "Agregar nueva unidad de medida";
            // 
            // gbDatosBebida
            // 
            this.gbDatosBebida.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gbDatosBebida.Controls.Add(this.btnActualizar);
            this.gbDatosBebida.Controls.Add(this.btnCrearU);
            this.gbDatosBebida.Controls.Add(this.lblSeleccionBebida);
            this.gbDatosBebida.Controls.Add(this.txtNuevaU);
            this.gbDatosBebida.Controls.Add(this.lbNombreBebida);
            this.gbDatosBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.gbDatosBebida.Location = new System.Drawing.Point(513, 106);
            this.gbDatosBebida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosBebida.Name = "gbDatosBebida";
            this.gbDatosBebida.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDatosBebida.Size = new System.Drawing.Size(512, 314);
            this.gbDatosBebida.TabIndex = 20;
            this.gbDatosBebida.TabStop = false;
            this.gbDatosBebida.Text = "Agregar unidad de medida para inventario";
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActualizar.Location = new System.Drawing.Point(120, 220);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(160, 58);
            this.btnActualizar.TabIndex = 22;
            this.btnActualizar.Text = "Actualizar unidad";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnCrearU
            // 
            this.btnCrearU.BackColor = System.Drawing.Color.ForestGreen;
            this.btnCrearU.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearU.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCrearU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearU.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCrearU.Location = new System.Drawing.Point(308, 220);
            this.btnCrearU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCrearU.Name = "btnCrearU";
            this.btnCrearU.Size = new System.Drawing.Size(175, 58);
            this.btnCrearU.TabIndex = 13;
            this.btnCrearU.Text = "Agregar unidad";
            this.btnCrearU.UseVisualStyleBackColor = false;
            this.btnCrearU.Click += new System.EventHandler(this.btnCrearU_Click);
            // 
            // lblSeleccionBebida
            // 
            this.lblSeleccionBebida.AutoSize = true;
            this.lblSeleccionBebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSeleccionBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSeleccionBebida.Location = new System.Drawing.Point(25, 56);
            this.lblSeleccionBebida.Name = "lblSeleccionBebida";
            this.lblSeleccionBebida.Size = new System.Drawing.Size(375, 20);
            this.lblSeleccionBebida.TabIndex = 8;
            this.lblSeleccionBebida.Text = "Agregué el nombre de la nueva unidad a agregar:";
            // 
            // txtNuevaU
            // 
            this.txtNuevaU.Location = new System.Drawing.Point(245, 139);
            this.txtNuevaU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNuevaU.Name = "txtNuevaU";
            this.txtNuevaU.Size = new System.Drawing.Size(238, 26);
            this.txtNuevaU.TabIndex = 1;
            // 
            // lbNombreBebida
            // 
            this.lbNombreBebida.AutoSize = true;
            this.lbNombreBebida.ForeColor = System.Drawing.SystemColors.Control;
            this.lbNombreBebida.Location = new System.Drawing.Point(29, 139);
            this.lbNombreBebida.Name = "lbNombreBebida";
            this.lbNombreBebida.Size = new System.Drawing.Size(127, 20);
            this.lbNombreBebida.TabIndex = 0;
            this.lbNombreBebida.Text = "Nombre unidad:";
            // 
            // dgvUnidades
            // 
            this.dgvUnidades.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvUnidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnidades.Location = new System.Drawing.Point(39, 106);
            this.dgvUnidades.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvUnidades.Name = "dgvUnidades";
            this.dgvUnidades.RowHeadersWidth = 51;
            this.dgvUnidades.RowTemplate.Height = 24;
            this.dgvUnidades.Size = new System.Drawing.Size(457, 314);
            this.dgvUnidades.TabIndex = 19;
            // 
            // FormAgregarUnidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(1076, 505);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gbDatosBebida);
            this.Controls.Add(this.dgvUnidades);
            this.Name = "FormAgregarUnidades";
            this.Text = "Agregar Unidades de Medida";
            this.gbDatosBebida.ResumeLayout(false);
            this.gbDatosBebida.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbDatosBebida;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnCrearU;
        private System.Windows.Forms.Label lblSeleccionBebida;
        private System.Windows.Forms.TextBox txtNuevaU;
        private System.Windows.Forms.Label lbNombreBebida;
        private System.Windows.Forms.DataGridView dgvUnidades;
    }
}