﻿namespace LaCaguamaSV.Fomularios.VistasAdmin
{
    partial class FormCrearOrden
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMesas = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Cliente:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Location = new System.Drawing.Point(240, 65);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(100, 20);
            this.txtNombreCliente.TabIndex = 1;
            this.txtNombreCliente.TextChanged += new System.EventHandler(this.txtNombreCliente_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Seleccionar Mesa:";
            // 
            // cmbMesas
            // 
            this.cmbMesas.FormattingEnabled = true;
            this.cmbMesas.Location = new System.Drawing.Point(256, 100);
            this.cmbMesas.Name = "cmbMesas";
            this.cmbMesas.Size = new System.Drawing.Size(121, 21);
            this.cmbMesas.TabIndex = 3;
            this.cmbMesas.SelectedIndexChanged += new System.EventHandler(this.cmbMesas_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Método de Pago:";
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.FormattingEnabled = true;
            this.cmbTipoPago.Location = new System.Drawing.Point(250, 135);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoPago.TabIndex = 5;
            this.cmbTipoPago.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPago_SelectedIndexChanged);
            // 
            // btnCrearOrden
            // 
            this.btnCrearOrden.Location = new System.Drawing.Point(154, 222);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Size = new System.Drawing.Size(75, 23);
            this.btnCrearOrden.TabIndex = 6;
            this.btnCrearOrden.Text = "Crear Orden";
            this.btnCrearOrden.UseVisualStyleBackColor = true;
            this.btnCrearOrden.Click += new System.EventHandler(this.btnCrearOrden_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(256, 221);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormCrearOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrearOrden);
            this.Controls.Add(this.cmbTipoPago);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMesas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNombreCliente);
            this.Controls.Add(this.label1);
            this.Name = "FormCrearOrden";
            this.Text = "FormCrearOrden";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMesas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.Button btnCancelar;
    }
}