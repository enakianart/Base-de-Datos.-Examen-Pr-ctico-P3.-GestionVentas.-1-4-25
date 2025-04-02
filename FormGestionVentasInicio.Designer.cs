namespace Base_de_Datos.Examen_Práctico_P3.GestionVentas._1_4_25
{
    partial class FormGestionVentasInicio
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
            this.RegistrarClienteBTN = new System.Windows.Forms.Button();
            this.RegistrarVentasBTN = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Agency FB", 35F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(27, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 168);
            this.label1.TabIndex = 1;
            this.label1.Text = "Base de Datos.\r\n GestionVentas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RegistrarClienteBTN
            // 
            this.RegistrarClienteBTN.BackColor = System.Drawing.Color.White;
            this.RegistrarClienteBTN.Font = new System.Drawing.Font("Agency FB", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegistrarClienteBTN.Location = new System.Drawing.Point(70, 300);
            this.RegistrarClienteBTN.Name = "RegistrarClienteBTN";
            this.RegistrarClienteBTN.Size = new System.Drawing.Size(254, 108);
            this.RegistrarClienteBTN.TabIndex = 2;
            this.RegistrarClienteBTN.Text = "Registrar Cliente";
            this.RegistrarClienteBTN.UseVisualStyleBackColor = false;
            this.RegistrarClienteBTN.Click += new System.EventHandler(this.RegistrarClienteBTN_Click);
            // 
            // RegistrarVentasBTN
            // 
            this.RegistrarVentasBTN.BackColor = System.Drawing.Color.White;
            this.RegistrarVentasBTN.Font = new System.Drawing.Font("Agency FB", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegistrarVentasBTN.Location = new System.Drawing.Point(440, 300);
            this.RegistrarVentasBTN.Name = "RegistrarVentasBTN";
            this.RegistrarVentasBTN.Size = new System.Drawing.Size(244, 108);
            this.RegistrarVentasBTN.TabIndex = 3;
            this.RegistrarVentasBTN.Text = "Registrar Ventas";
            this.RegistrarVentasBTN.UseVisualStyleBackColor = false;
            this.RegistrarVentasBTN.Click += new System.EventHandler(this.RegistrarVentasBTN_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Base_de_Datos.Examen_Práctico_P3.GestionVentas._1_4_25.Properties.Resources.Hiki_feli_normal_tamanio_grandote;
            this.pictureBox1.Location = new System.Drawing.Point(483, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(274, 213);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FormGestionVentasInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(811, 469);
            this.Controls.Add(this.RegistrarVentasBTN);
            this.Controls.Add(this.RegistrarClienteBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormGestionVentasInicio";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RegistrarClienteBTN;
        private System.Windows.Forms.Button RegistrarVentasBTN;
    }
}

