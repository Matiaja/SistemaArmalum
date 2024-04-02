namespace Presentacion
{
    partial class ProductosCoincidentesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblProdCoincidentes = new System.Windows.Forms.Label();
            this.dGVProducto = new System.Windows.Forms.DataGridView();
            this.ColumnaDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblBuscador = new System.Windows.Forms.Label();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProducto)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProdCoincidentes
            // 
            this.lblProdCoincidentes.AutoSize = true;
            this.lblProdCoincidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdCoincidentes.Location = new System.Drawing.Point(25, 36);
            this.lblProdCoincidentes.Name = "lblProdCoincidentes";
            this.lblProdCoincidentes.Size = new System.Drawing.Size(360, 25);
            this.lblProdCoincidentes.TabIndex = 24;
            this.lblProdCoincidentes.Text = "Atencion, estos productos coinciden";
            // 
            // dGVProducto
            // 
            this.dGVProducto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVProducto.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dGVProducto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dGVProducto.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(230)))), ((int)(((byte)(193)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVProducto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGVProducto.ColumnHeadersHeight = 45;
            this.dGVProducto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnaDescripcion,
            this.ColumnaCodigo,
            this.ColumnaPrecio});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGVProducto.DefaultCellStyle = dataGridViewCellStyle2;
            this.dGVProducto.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dGVProducto.EnableHeadersVisualStyles = false;
            this.dGVProducto.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(30)))), ((int)(((byte)(61)))));
            this.dGVProducto.Location = new System.Drawing.Point(0, 85);
            this.dGVProducto.Name = "dGVProducto";
            this.dGVProducto.RowHeadersVisible = false;
            this.dGVProducto.RowHeadersWidth = 51;
            this.dGVProducto.RowTemplate.Height = 35;
            this.dGVProducto.Size = new System.Drawing.Size(1341, 463);
            this.dGVProducto.TabIndex = 37;
            // 
            // ColumnaDescripcion
            // 
            this.ColumnaDescripcion.FillWeight = 400F;
            this.ColumnaDescripcion.HeaderText = "Descripcion";
            this.ColumnaDescripcion.MinimumWidth = 6;
            this.ColumnaDescripcion.Name = "ColumnaDescripcion";
            // 
            // ColumnaCodigo
            // 
            this.ColumnaCodigo.FillWeight = 200F;
            this.ColumnaCodigo.HeaderText = "Codigo";
            this.ColumnaCodigo.MinimumWidth = 6;
            this.ColumnaCodigo.Name = "ColumnaCodigo";
            this.ColumnaCodigo.ReadOnly = true;
            // 
            // ColumnaPrecio
            // 
            this.ColumnaPrecio.FillWeight = 180F;
            this.ColumnaPrecio.HeaderText = "Precio";
            this.ColumnaPrecio.MinimumWidth = 6;
            this.ColumnaPrecio.Name = "ColumnaPrecio";
            // 
            // lblBuscador
            // 
            this.lblBuscador.AutoSize = true;
            this.lblBuscador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscador.Location = new System.Drawing.Point(643, 36);
            this.lblBuscador.Name = "lblBuscador";
            this.lblBuscador.Size = new System.Drawing.Size(110, 25);
            this.lblBuscador.TabIndex = 38;
            this.lblBuscador.Text = "Buscador:";
            // 
            // txtBuscador
            // 
            this.txtBuscador.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscador.Location = new System.Drawing.Point(780, 34);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(361, 30);
            this.txtBuscador.TabIndex = 39;
            this.txtBuscador.TextChanged += new System.EventHandler(this.txtBuscador_TextChanged);
            // 
            // ProductosCoincidentesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(142)))), ((int)(((byte)(165)))));
            this.ClientSize = new System.Drawing.Size(1341, 548);
            this.Controls.Add(this.txtBuscador);
            this.Controls.Add(this.lblBuscador);
            this.Controls.Add(this.dGVProducto);
            this.Controls.Add(this.lblProdCoincidentes);
            this.Name = "ProductosCoincidentesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductosCoincidentesForm";
            ((System.ComponentModel.ISupportInitialize)(this.dGVProducto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblProdCoincidentes;
        private System.Windows.Forms.DataGridView dGVProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaPrecio;
        private System.Windows.Forms.Label lblBuscador;
        private System.Windows.Forms.TextBox txtBuscador;
    }
}