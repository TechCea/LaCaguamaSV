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
            this.btnMenu = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewOrdenesAdmin = new System.Windows.Forms.DataGridView();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnInventario = new System.Windows.Forms.Button();
            this.BtnHistorialPagos = new System.Windows.Forms.Button();
            this.buttonPromociones = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrdenesAdmin)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGestionUsuarios
            // 
            this.btnGestionUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnGestionUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGestionUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionUsuarios.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGestionUsuarios.Location = new System.Drawing.Point(39, 93);
            this.btnGestionUsuarios.Margin = new System.Windows.Forms.Padding(4);
            this.btnGestionUsuarios.Name = "btnGestionUsuarios";
            this.btnGestionUsuarios.Size = new System.Drawing.Size(219, 79);
            this.btnGestionUsuarios.TabIndex = 2;
            this.btnGestionUsuarios.Text = "Administrar Usuarios";
            this.btnGestionUsuarios.UseVisualStyleBackColor = false;
            this.btnGestionUsuarios.Click += new System.EventHandler(this.btnGestionUsuarios_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCerrarSesion.Location = new System.Drawing.Point(1167, 94);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(143, 47);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenu.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMenu.Location = new System.Drawing.Point(518, 93);
            this.btnMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(219, 79);
            this.btnMenu.TabIndex = 5;
            this.btnMenu.Text = "Gestionar Menú";
            this.btnMenu.UseVisualStyleBackColor = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(281, 187);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 67);
            this.button1.TabIndex = 7;
            this.button1.Text = "Funciones";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewOrdenesAdmin
            // 
            this.dataGridViewOrdenesAdmin.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridViewOrdenesAdmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrdenesAdmin.Location = new System.Drawing.Point(35, 275);
            this.dataGridViewOrdenesAdmin.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewOrdenesAdmin.Name = "dataGridViewOrdenesAdmin";
            this.dataGridViewOrdenesAdmin.RowHeadersWidth = 51;
            this.dataGridViewOrdenesAdmin.Size = new System.Drawing.Size(1275, 489);
            this.dataGridViewOrdenesAdmin.TabIndex = 8;
            this.dataGridViewOrdenesAdmin.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrdenesAdmin_CellContentClick);
            // 
            // btnCrearOrden
            // 
            this.btnCrearOrden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnCrearOrden.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearOrden.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCrearOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearOrden.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCrearOrden.Location = new System.Drawing.Point(39, 187);
            this.btnCrearOrden.Margin = new System.Windows.Forms.Padding(4);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Size = new System.Drawing.Size(219, 67);
            this.btnCrearOrden.TabIndex = 9;
            this.btnCrearOrden.Text = "Crear Orden";
            this.btnCrearOrden.UseVisualStyleBackColor = false;
            this.btnCrearOrden.Click += new System.EventHandler(this.btnCrearOrden_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(518, 187);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(219, 69);
            this.button2.TabIndex = 10;
            this.button2.Text = "Gestionar mesas";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnInventario
            // 
            this.btnInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventario.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventario.ForeColor = System.Drawing.SystemColors.Control;
            this.btnInventario.Location = new System.Drawing.Point(281, 94);
            this.btnInventario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(219, 76);
            this.btnInventario.TabIndex = 11;
            this.btnInventario.Text = "Gestionar inventario";
            this.btnInventario.UseVisualStyleBackColor = false;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // BtnHistorialPagos
            // 
            this.BtnHistorialPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.BtnHistorialPagos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnHistorialPagos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnHistorialPagos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHistorialPagos.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnHistorialPagos.Location = new System.Drawing.Point(758, 94);
            this.BtnHistorialPagos.Margin = new System.Windows.Forms.Padding(4);
            this.BtnHistorialPagos.Name = "BtnHistorialPagos";
            this.BtnHistorialPagos.Size = new System.Drawing.Size(219, 78);
            this.BtnHistorialPagos.TabIndex = 12;
            this.BtnHistorialPagos.Text = "Historial Pagos";
            this.BtnHistorialPagos.UseVisualStyleBackColor = false;
            this.BtnHistorialPagos.Click += new System.EventHandler(this.BtnHistorialPagos_Click);
            // 
            // buttonPromociones
            // 
            this.buttonPromociones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.buttonPromociones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPromociones.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPromociones.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPromociones.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonPromociones.Location = new System.Drawing.Point(758, 187);
            this.buttonPromociones.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPromociones.Name = "buttonPromociones";
            this.buttonPromociones.Size = new System.Drawing.Size(219, 69);
            this.buttonPromociones.TabIndex = 13;
            this.buttonPromociones.Text = "Promociones";
            this.buttonPromociones.UseVisualStyleBackColor = false;
            this.buttonPromociones.Click += new System.EventHandler(this.buttonPromociones_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(36, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Bienvenido Administrador";
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1392, 800);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPromociones);
            this.Controls.Add(this.BtnHistorialPagos);
            this.Controls.Add(this.btnInventario);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCrearOrden);
            this.Controls.Add(this.dataGridViewOrdenesAdmin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.btnGestionUsuarios);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAdmin";
            this.Text = "Menú principal Administración";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrdenesAdmin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGestionUsuarios;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewOrdenesAdmin;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button BtnHistorialPagos;
        private System.Windows.Forms.Button buttonPromociones;
        private System.Windows.Forms.Label label1;
    }
}