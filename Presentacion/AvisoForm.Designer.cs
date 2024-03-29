namespace Presentacion
{
    partial class AvisoForm
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblProdCoincidentes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(651, 178);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(124, 45);
            this.btnCerrar.TabIndex = 32;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblProdCoincidentes
            // 
            this.lblProdCoincidentes.AutoSize = true;
            this.lblProdCoincidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdCoincidentes.Location = new System.Drawing.Point(106, 97);
            this.lblProdCoincidentes.Name = "lblProdCoincidentes";
            this.lblProdCoincidentes.Size = new System.Drawing.Size(551, 25);
            this.lblProdCoincidentes.TabIndex = 33;
            this.lblProdCoincidentes.Text = "La base de datos se esta actualizando, por favor espere";
            // 
            // AvisoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(142)))), ((int)(((byte)(165)))));
            this.ClientSize = new System.Drawing.Size(800, 257);
            this.Controls.Add(this.lblProdCoincidentes);
            this.Controls.Add(this.btnCerrar);
            this.Name = "AvisoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AvisoForm";
            this.Load += new System.EventHandler(this.AvisoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblProdCoincidentes;
    }
}