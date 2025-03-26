namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormAdmin
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
            this.btnGestionUsuarios = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMenu = new System.Windows.Forms.Button();
            this.Ordenes = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewOrdenesAdmin = new System.Windows.Forms.DataGridView();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrdenesAdmin)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGestionUsuarios
            // 
            this.btnGestionUsuarios.Location = new System.Drawing.Point(772, 91);
            this.btnGestionUsuarios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGestionUsuarios.Name = "btnGestionUsuarios";
            this.btnGestionUsuarios.Size = new System.Drawing.Size(223, 76);
            this.btnGestionUsuarios.TabIndex = 2;
            this.btnGestionUsuarios.Text = "Administrar Usuarios";
            this.btnGestionUsuarios.UseVisualStyleBackColor = true;
            this.btnGestionUsuarios.Click += new System.EventHandler(this.btnGestionUsuarios_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(855, 511);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(100, 28);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "Cerrar";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(605, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bienvenido Admin";
            // 
            // btnMenu
            // 
            this.btnMenu.Location = new System.Drawing.Point(772, 172);
            this.btnMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(223, 64);
            this.btnMenu.TabIndex = 5;
            this.btnMenu.Text = "Gestionar Menú";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // Ordenes
            // 
            this.Ordenes.Location = new System.Drawing.Point(772, 15);
            this.Ordenes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Ordenes.Name = "Ordenes";
            this.Ordenes.Size = new System.Drawing.Size(149, 68);
            this.Ordenes.TabIndex = 6;
            this.Ordenes.Text = "Ordenes";
            this.Ordenes.UseVisualStyleBackColor = true;
            this.Ordenes.Click += new System.EventHandler(this.Ordenes_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(772, 242);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 82);
            this.button1.TabIndex = 7;
            this.button1.Text = "Funciones";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewOrdenesAdmin
            // 
            this.dataGridViewOrdenesAdmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrdenesAdmin.Location = new System.Drawing.Point(16, 46);
            this.dataGridViewOrdenesAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewOrdenesAdmin.Name = "dataGridViewOrdenesAdmin";
            this.dataGridViewOrdenesAdmin.RowHeadersWidth = 51;
            this.dataGridViewOrdenesAdmin.Size = new System.Drawing.Size(737, 464);
            this.dataGridViewOrdenesAdmin.TabIndex = 8;
            this.dataGridViewOrdenesAdmin.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrdenesAdmin_CellContentClick);
            // 
            // btnCrearOrden
            // 
            this.btnCrearOrden.Location = new System.Drawing.Point(772, 332);
            this.btnCrearOrden.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Size = new System.Drawing.Size(203, 64);
            this.btnCrearOrden.TabIndex = 9;
            this.btnCrearOrden.Text = "Crear Orden";
            this.btnCrearOrden.UseVisualStyleBackColor = true;
            this.btnCrearOrden.Click += new System.EventHandler(this.btnCrearOrden_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(777, 414);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(197, 69);
            this.button2.TabIndex = 10;
            this.button2.Text = "Gestionar mesas";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.Rectangle_52;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCrearOrden);
            this.Controls.Add(this.dataGridViewOrdenesAdmin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Ordenes);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.btnGestionUsuarios);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrdenesAdmin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGestionUsuarios;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button Ordenes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewOrdenesAdmin;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.Button button2;
    }
}