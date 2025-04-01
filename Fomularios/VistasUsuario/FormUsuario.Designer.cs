namespace LaCaguamaSV.Fomularios.VistasUsuario
{
    partial class FormUsuario
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
            this.btnInventario = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.dataGridViewOrdenesAdmin = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.Ordenes = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnGestionUsuarios = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrdenesAdmin)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInventario
            // 
            this.btnInventario.Location = new System.Drawing.Point(600, 375);
            this.btnInventario.Margin = new System.Windows.Forms.Padding(2);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(152, 56);
            this.btnInventario.TabIndex = 21;
            this.btnInventario.Text = "Gestionar inventario";
            this.btnInventario.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(600, 314);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 56);
            this.button2.TabIndex = 20;
            this.button2.Text = "Gestionar mesas";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnCrearOrden
            // 
            this.btnCrearOrden.Location = new System.Drawing.Point(600, 257);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Size = new System.Drawing.Size(152, 52);
            this.btnCrearOrden.TabIndex = 19;
            this.btnCrearOrden.Text = "Crear Orden";
            this.btnCrearOrden.UseVisualStyleBackColor = true;
            // 
            // dataGridViewOrdenesAdmin
            // 
            this.dataGridViewOrdenesAdmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrdenesAdmin.Location = new System.Drawing.Point(33, 24);
            this.dataGridViewOrdenesAdmin.Name = "dataGridViewOrdenesAdmin";
            this.dataGridViewOrdenesAdmin.RowHeadersWidth = 51;
            this.dataGridViewOrdenesAdmin.Size = new System.Drawing.Size(553, 448);
            this.dataGridViewOrdenesAdmin.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(600, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 67);
            this.button1.TabIndex = 17;
            this.button1.Text = "Funciones";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Ordenes
            // 
            this.Ordenes.Location = new System.Drawing.Point(600, -1);
            this.Ordenes.Name = "Ordenes";
            this.Ordenes.Size = new System.Drawing.Size(112, 55);
            this.Ordenes.TabIndex = 16;
            this.Ordenes.Text = "Ordenes";
            this.Ordenes.UseVisualStyleBackColor = true;
            // 
            // btnMenu
            // 
            this.btnMenu.Location = new System.Drawing.Point(600, 127);
            this.btnMenu.Margin = new System.Windows.Forms.Padding(2);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(167, 52);
            this.btnMenu.TabIndex = 15;
            this.btnMenu.Text = "Gestionar Menú";
            this.btnMenu.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(465, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Bienvenido Usuario";
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(677, 449);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(75, 23);
            this.btnCerrarSesion.TabIndex = 13;
            this.btnCerrarSesion.Text = "Cerrar";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            // 
            // btnGestionUsuarios
            // 
            this.btnGestionUsuarios.Location = new System.Drawing.Point(600, 61);
            this.btnGestionUsuarios.Name = "btnGestionUsuarios";
            this.btnGestionUsuarios.Size = new System.Drawing.Size(167, 62);
            this.btnGestionUsuarios.TabIndex = 12;
            this.btnGestionUsuarios.Text = "Administrar Usuarios";
            this.btnGestionUsuarios.UseVisualStyleBackColor = true;
            // 
            // FormUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.btnInventario);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCrearOrden);
            this.Controls.Add(this.dataGridViewOrdenesAdmin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Ordenes);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.btnGestionUsuarios);
            this.Name = "FormUsuario";
            this.Text = "FormUsuario";
            this.Load += new System.EventHandler(this.FormUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrdenesAdmin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.DataGridView dataGridViewOrdenesAdmin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Ordenes;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnGestionUsuarios;
    }
}