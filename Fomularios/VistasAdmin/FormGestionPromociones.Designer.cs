namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormGestionPromociones
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewPromociones = new System.Windows.Forms.DataGridView();
            this.dataGridViewMenu = new System.Windows.Forms.DataGridView();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.dateInicio = new System.Windows.Forms.DateTimePicker();
            this.dateFin = new System.Windows.Forms.DateTimePicker();
            this.checkSinFin = new System.Windows.Forms.CheckBox();
            this.checkActiva = new System.Windows.Forms.CheckBox();
            this.btnPlatos = new System.Windows.Forms.Button();
            this.btnBebidas = new System.Windows.Forms.Button();
            this.btnExtras = new System.Windows.Forms.Button();
            this.btnGuardarPromocion = new System.Windows.Forms.Button();
            this.panelItems = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPromociones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPromociones
            // 
            this.dataGridViewPromociones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPromociones.Location = new System.Drawing.Point(13, 168);
            this.dataGridViewPromociones.Name = "dataGridViewPromociones";
            this.dataGridViewPromociones.Size = new System.Drawing.Size(240, 150);
            this.dataGridViewPromociones.TabIndex = 1;
            this.dataGridViewPromociones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPromociones_CellClick);
            // 
            // dataGridViewMenu
            // 
            this.dataGridViewMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMenu.Location = new System.Drawing.Point(506, 168);
            this.dataGridViewMenu.Name = "dataGridViewMenu";
            this.dataGridViewMenu.Size = new System.Drawing.Size(240, 150);
            this.dataGridViewMenu.TabIndex = 2;
            this.dataGridViewMenu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewMenu_CellDoubleClick);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(12, 28);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 3;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(118, 28);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(100, 20);
            this.txtDescripcion.TabIndex = 4;
            this.txtDescripcion.TextChanged += new System.EventHandler(this.txtDescripcion_TextChanged);
            // 
            // dateInicio
            // 
            this.dateInicio.Location = new System.Drawing.Point(354, 28);
            this.dateInicio.Name = "dateInicio";
            this.dateInicio.Size = new System.Drawing.Size(200, 20);
            this.dateInicio.TabIndex = 6;
            this.dateInicio.ValueChanged += new System.EventHandler(this.dateInicio_ValueChanged);
            // 
            // dateFin
            // 
            this.dateFin.Location = new System.Drawing.Point(354, 90);
            this.dateFin.Name = "dateFin";
            this.dateFin.Size = new System.Drawing.Size(200, 20);
            this.dateFin.TabIndex = 7;
            this.dateFin.ValueChanged += new System.EventHandler(this.dateFin_ValueChanged);
            // 
            // checkSinFin
            // 
            this.checkSinFin.AutoSize = true;
            this.checkSinFin.Location = new System.Drawing.Point(582, 33);
            this.checkSinFin.Name = "checkSinFin";
            this.checkSinFin.Size = new System.Drawing.Size(150, 17);
            this.checkSinFin.TabIndex = 8;
            this.checkSinFin.Text = "Sin Fecha de Finalizacion ";
            this.checkSinFin.UseVisualStyleBackColor = true;
            this.checkSinFin.CheckedChanged += new System.EventHandler(this.checkSinFin_CheckedChanged);
            // 
            // checkActiva
            // 
            this.checkActiva.AutoSize = true;
            this.checkActiva.Location = new System.Drawing.Point(582, 65);
            this.checkActiva.Name = "checkActiva";
            this.checkActiva.Size = new System.Drawing.Size(56, 17);
            this.checkActiva.TabIndex = 9;
            this.checkActiva.Text = "Activa";
            this.checkActiva.UseVisualStyleBackColor = true;
            this.checkActiva.CheckedChanged += new System.EventHandler(this.checkActiva_CheckedChanged);
            // 
            // btnPlatos
            // 
            this.btnPlatos.Location = new System.Drawing.Point(13, 125);
            this.btnPlatos.Name = "btnPlatos";
            this.btnPlatos.Size = new System.Drawing.Size(75, 23);
            this.btnPlatos.TabIndex = 10;
            this.btnPlatos.Text = "Platos";
            this.btnPlatos.UseVisualStyleBackColor = true;
            this.btnPlatos.Click += new System.EventHandler(this.btnPlatos_Click_1);
            // 
            // btnBebidas
            // 
            this.btnBebidas.Location = new System.Drawing.Point(146, 125);
            this.btnBebidas.Name = "btnBebidas";
            this.btnBebidas.Size = new System.Drawing.Size(75, 23);
            this.btnBebidas.TabIndex = 11;
            this.btnBebidas.Text = "Bebidas";
            this.btnBebidas.UseVisualStyleBackColor = true;
            this.btnBebidas.Click += new System.EventHandler(this.btnBebidas_Click_1);
            // 
            // btnExtras
            // 
            this.btnExtras.Location = new System.Drawing.Point(259, 125);
            this.btnExtras.Name = "btnExtras";
            this.btnExtras.Size = new System.Drawing.Size(75, 23);
            this.btnExtras.TabIndex = 12;
            this.btnExtras.Text = "Extras";
            this.btnExtras.UseVisualStyleBackColor = true;
            this.btnExtras.Click += new System.EventHandler(this.btnExtras_Click);
            // 
            // btnGuardarPromocion
            // 
            this.btnGuardarPromocion.Location = new System.Drawing.Point(645, 354);
            this.btnGuardarPromocion.Name = "btnGuardarPromocion";
            this.btnGuardarPromocion.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarPromocion.TabIndex = 14;
            this.btnGuardarPromocion.Text = "Guardar Promocion";
            this.btnGuardarPromocion.UseVisualStyleBackColor = true;
            this.btnGuardarPromocion.Click += new System.EventHandler(this.btnGuardarPromocion_Click_1);
            // 
            // panelItems
            // 
            this.panelItems.Location = new System.Drawing.Point(260, 168);
            this.panelItems.Name = "panelItems";
            this.panelItems.Size = new System.Drawing.Size(240, 150);
            this.panelItems.TabIndex = 15;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Nombre Promo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Descripcion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Precio de Promo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Fecha de Inicio";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(354, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Fecha de Fin";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(225, 28);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(123, 20);
            this.txtPrecio.TabIndex = 21;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            // 
            // FormGestionPromociones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelItems);
            this.Controls.Add(this.btnGuardarPromocion);
            this.Controls.Add(this.btnExtras);
            this.Controls.Add(this.btnBebidas);
            this.Controls.Add(this.btnPlatos);
            this.Controls.Add(this.checkActiva);
            this.Controls.Add(this.checkSinFin);
            this.Controls.Add(this.dateFin);
            this.Controls.Add(this.dateInicio);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.dataGridViewMenu);
            this.Controls.Add(this.dataGridViewPromociones);
            this.Name = "FormGestionPromociones";
            this.Text = "FormGestionPromociones";
            this.Load += new System.EventHandler(this.FormGestionPromociones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPromociones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewPromociones;
        private System.Windows.Forms.DataGridView dataGridViewMenu;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DateTimePicker dateInicio;
        private System.Windows.Forms.DateTimePicker dateFin;
        private System.Windows.Forms.CheckBox checkSinFin;
        private System.Windows.Forms.CheckBox checkActiva;
        private System.Windows.Forms.Button btnPlatos;
        private System.Windows.Forms.Button btnBebidas;
        private System.Windows.Forms.Button btnExtras;
        private System.Windows.Forms.Button btnGuardarPromocion;
        private System.Windows.Forms.Panel panelItems;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPrecio;
    }
}