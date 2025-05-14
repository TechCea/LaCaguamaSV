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
            this.btnMesas = new System.Windows.Forms.Button();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.dataGridViewOrdenesUsuario = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.BtnMenu = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnFiltrarOrdenes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrdenesUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMesas
            // 
            this.btnMesas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnMesas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesas.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnMesas.Location = new System.Drawing.Point(376, 66);
            this.btnMesas.Margin = new System.Windows.Forms.Padding(2);
            this.btnMesas.Name = "btnMesas";
            this.btnMesas.Size = new System.Drawing.Size(140, 67);
            this.btnMesas.TabIndex = 20;
            this.btnMesas.Text = "Gestionar mesas";
            this.btnMesas.UseVisualStyleBackColor = false;
            this.btnMesas.Click += new System.EventHandler(this.btnMesas_Click);
            // 
            // btnCrearOrden
            // 
            this.btnCrearOrden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.btnCrearOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearOrden.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCrearOrden.Location = new System.Drawing.Point(46, 65);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Size = new System.Drawing.Size(169, 67);
            this.btnCrearOrden.TabIndex = 19;
            this.btnCrearOrden.Text = "Crear Orden";
            this.btnCrearOrden.UseVisualStyleBackColor = false;
            this.btnCrearOrden.Click += new System.EventHandler(this.btnCrearOrden_Click);
            // 
            // dataGridViewOrdenesUsuario
            // 
            this.dataGridViewOrdenesUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrdenesUsuario.Location = new System.Drawing.Point(46, 147);
            this.dataGridViewOrdenesUsuario.Name = "dataGridViewOrdenesUsuario";
            this.dataGridViewOrdenesUsuario.RowHeadersWidth = 51;
            this.dataGridViewOrdenesUsuario.Size = new System.Drawing.Size(854, 402);
            this.dataGridViewOrdenesUsuario.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(220, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 67);
            this.button1.TabIndex = 17;
            this.button1.Text = "Funciones";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(44, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
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
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // BtnMenu
            // 
            this.BtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(71)))), ((int)(((byte)(25)))));
            this.BtnMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMenu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnMenu.Location = new System.Drawing.Point(520, 65);
            this.BtnMenu.Margin = new System.Windows.Forms.Padding(2);
            this.BtnMenu.Name = "BtnMenu";
            this.BtnMenu.Size = new System.Drawing.Size(140, 67);
            this.BtnMenu.TabIndex = 21;
            this.BtnMenu.Text = "Visualizar Menu";
            this.BtnMenu.UseVisualStyleBackColor = false;
            this.BtnMenu.Click += new System.EventHandler(this.BtnMenu_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(793, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 38);
            this.button2.TabIndex = 22;
            this.button2.Text = "Cerrar sesión";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnFiltrarOrdenes
            // 
            this.btnFiltrarOrdenes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnFiltrarOrdenes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarOrdenes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFiltrarOrdenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarOrdenes.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFiltrarOrdenes.Location = new System.Drawing.Point(793, 94);
            this.btnFiltrarOrdenes.Name = "btnFiltrarOrdenes";
            this.btnFiltrarOrdenes.Size = new System.Drawing.Size(107, 38);
            this.btnFiltrarOrdenes.TabIndex = 23;
            this.btnFiltrarOrdenes.Text = "Filtrar Ordenes";
            this.btnFiltrarOrdenes.UseVisualStyleBackColor = false;
            this.btnFiltrarOrdenes.Click += new System.EventHandler(this.btnFiltrarOrdenes_Click);
            // 
            // FormUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::LaCaguamaSV.Properties.Resources.fondo;
            this.ClientSize = new System.Drawing.Size(933, 585);
            this.Controls.Add(this.btnFiltrarOrdenes);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BtnMenu);
            this.Controls.Add(this.btnMesas);
            this.Controls.Add(this.btnCrearOrden);
            this.Controls.Add(this.dataGridViewOrdenesUsuario);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCerrarSesion);
            this.Name = "FormUsuario";
            this.Text = "Menú principal";
            this.Load += new System.EventHandler(this.FormUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrdenesUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnMesas;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.DataGridView dataGridViewOrdenesUsuario;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button BtnMenu;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnFiltrarOrdenes;
    }
}