namespace Presentacion
{
    partial class DatosClientesForm
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
            this.lblNombreApellido = new System.Windows.Forms.Label();
            this.txtBoxNombreApellido = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtBoxDireccion = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtBoxTelefono = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtBoxCuil = new System.Windows.Forms.TextBox();
            this.lblCuil = new System.Windows.Forms.Label();
            this.txtBoxDiasVigencia = new System.Windows.Forms.TextBox();
            this.lblDiasVigencia = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtBoxLocalidad = new System.Windows.Forms.TextBox();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNombreApellido
            // 
            this.lblNombreApellido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombreApellido.AutoSize = true;
            this.lblNombreApellido.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblNombreApellido.Location = new System.Drawing.Point(115, 81);
            this.lblNombreApellido.Name = "lblNombreApellido";
            this.lblNombreApellido.Size = new System.Drawing.Size(181, 24);
            this.lblNombreApellido.TabIndex = 0;
            this.lblNombreApellido.Text = "Nombre y Apellido";
            // 
            // txtBoxNombreApellido
            // 
            this.txtBoxNombreApellido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxNombreApellido.Font = new System.Drawing.Font("Arial", 12F);
            this.txtBoxNombreApellido.Location = new System.Drawing.Point(367, 81);
            this.txtBoxNombreApellido.Name = "txtBoxNombreApellido";
            this.txtBoxNombreApellido.Size = new System.Drawing.Size(221, 30);
            this.txtBoxNombreApellido.TabIndex = 1;
            this.txtBoxNombreApellido.TextChanged += new System.EventHandler(this.txtBoxNombreApellido_TextChanged);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.Font = new System.Drawing.Font("Arial", 13.8F);
            this.btnLimpiar.Location = new System.Drawing.Point(521, 506);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(122, 51);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtBoxDireccion
            // 
            this.txtBoxDireccion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxDireccion.Font = new System.Drawing.Font("Arial", 12F);
            this.txtBoxDireccion.Location = new System.Drawing.Point(367, 149);
            this.txtBoxDireccion.Name = "txtBoxDireccion";
            this.txtBoxDireccion.Size = new System.Drawing.Size(221, 30);
            this.txtBoxDireccion.TabIndex = 2;
            this.txtBoxDireccion.TextChanged += new System.EventHandler(this.txtBoxDireccion_TextChanged);
            // 
            // lblDireccion
            // 
            this.lblDireccion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblDireccion.Location = new System.Drawing.Point(115, 149);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(99, 24);
            this.lblDireccion.TabIndex = 3;
            this.lblDireccion.Text = "Direccion";
            // 
            // txtBoxTelefono
            // 
            this.txtBoxTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxTelefono.Font = new System.Drawing.Font("Arial", 12F);
            this.txtBoxTelefono.Location = new System.Drawing.Point(367, 289);
            this.txtBoxTelefono.Name = "txtBoxTelefono";
            this.txtBoxTelefono.Size = new System.Drawing.Size(221, 30);
            this.txtBoxTelefono.TabIndex = 4;
            this.txtBoxTelefono.TextChanged += new System.EventHandler(this.txtBoxTelefono_TextChanged);
            // 
            // lblTelefono
            // 
            this.lblTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTelefono.Location = new System.Drawing.Point(115, 289);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(92, 24);
            this.lblTelefono.TabIndex = 5;
            this.lblTelefono.Text = "Telefono";
            // 
            // txtBoxCuil
            // 
            this.txtBoxCuil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxCuil.Font = new System.Drawing.Font("Arial", 12F);
            this.txtBoxCuil.Location = new System.Drawing.Point(367, 365);
            this.txtBoxCuil.Name = "txtBoxCuil";
            this.txtBoxCuil.Size = new System.Drawing.Size(221, 30);
            this.txtBoxCuil.TabIndex = 5;
            this.txtBoxCuil.TextChanged += new System.EventHandler(this.txtBoxCuil_TextChanged);
            // 
            // lblCuil
            // 
            this.lblCuil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCuil.AutoSize = true;
            this.lblCuil.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblCuil.Location = new System.Drawing.Point(115, 365);
            this.lblCuil.Name = "lblCuil";
            this.lblCuil.Size = new System.Drawing.Size(46, 24);
            this.lblCuil.TabIndex = 7;
            this.lblCuil.Text = "Cuil";
            // 
            // txtBoxDiasVigencia
            // 
            this.txtBoxDiasVigencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxDiasVigencia.Font = new System.Drawing.Font("Arial", 12F);
            this.txtBoxDiasVigencia.Location = new System.Drawing.Point(367, 442);
            this.txtBoxDiasVigencia.Name = "txtBoxDiasVigencia";
            this.txtBoxDiasVigencia.Size = new System.Drawing.Size(221, 30);
            this.txtBoxDiasVigencia.TabIndex = 6;
            // 
            // lblDiasVigencia
            // 
            this.lblDiasVigencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiasVigencia.AutoSize = true;
            this.lblDiasVigencia.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblDiasVigencia.Location = new System.Drawing.Point(115, 442);
            this.lblDiasVigencia.Name = "lblDiasVigencia";
            this.lblDiasVigencia.Size = new System.Drawing.Size(164, 24);
            this.lblDiasVigencia.TabIndex = 9;
            this.lblDiasVigencia.Text = "Dias de vigencia";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAceptar.Font = new System.Drawing.Font("Arial", 13.8F);
            this.btnAceptar.Location = new System.Drawing.Point(661, 506);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(122, 51);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtBoxLocalidad
            // 
            this.txtBoxLocalidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxLocalidad.Font = new System.Drawing.Font("Arial", 12F);
            this.txtBoxLocalidad.Location = new System.Drawing.Point(367, 221);
            this.txtBoxLocalidad.Name = "txtBoxLocalidad";
            this.txtBoxLocalidad.Size = new System.Drawing.Size(221, 30);
            this.txtBoxLocalidad.TabIndex = 3;
            this.txtBoxLocalidad.TextChanged += new System.EventHandler(this.txtBoxLocalidad_TextChanged);
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblLocalidad.Location = new System.Drawing.Point(115, 221);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(101, 24);
            this.lblLocalidad.TabIndex = 12;
            this.lblLocalidad.Text = "Localidad";
            // 
            // DatosClientesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(142)))), ((int)(((byte)(165)))));
            this.ClientSize = new System.Drawing.Size(795, 584);
            this.Controls.Add(this.txtBoxLocalidad);
            this.Controls.Add(this.lblLocalidad);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtBoxDiasVigencia);
            this.Controls.Add(this.lblDiasVigencia);
            this.Controls.Add(this.txtBoxCuil);
            this.Controls.Add(this.lblCuil);
            this.Controls.Add(this.txtBoxTelefono);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtBoxDireccion);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.txtBoxNombreApellido);
            this.Controls.Add(this.lblNombreApellido);
            this.Name = "DatosClientesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datos del cliente";
            this.Load += new System.EventHandler(this.DatosClientesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombreApellido;
        private System.Windows.Forms.TextBox txtBoxNombreApellido;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TextBox txtBoxDireccion;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtBoxTelefono;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtBoxCuil;
        private System.Windows.Forms.Label lblCuil;
        private System.Windows.Forms.TextBox txtBoxDiasVigencia;
        private System.Windows.Forms.Label lblDiasVigencia;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtBoxLocalidad;
        private System.Windows.Forms.Label lblLocalidad;
    }
}