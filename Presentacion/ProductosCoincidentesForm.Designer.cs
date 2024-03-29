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
            this.dGVProducto = new System.Windows.Forms.DataGridView();
            this.ColumnaDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblProdCoincidentes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProducto)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVProducto
            // 
            this.dGVProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGVProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVProducto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnaDescripcion,
            this.ColumnaCodigo,
            this.ColumnaPrecio});
            this.dGVProducto.Location = new System.Drawing.Point(12, 48);
            this.dGVProducto.Name = "dGVProducto";
            this.dGVProducto.RowHeadersWidth = 51;
            this.dGVProducto.RowTemplate.Height = 24;
            this.dGVProducto.Size = new System.Drawing.Size(1212, 445);
            this.dGVProducto.TabIndex = 23;
            this.dGVProducto.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVProducto_CellContentClick);
            // 
            // ColumnaDescripcion
            // 
            this.ColumnaDescripcion.HeaderText = "Descripcion";
            this.ColumnaDescripcion.MinimumWidth = 6;
            this.ColumnaDescripcion.Name = "ColumnaDescripcion";
            this.ColumnaDescripcion.Width = 125;
            // 
            // ColumnaCodigo
            // 
            this.ColumnaCodigo.HeaderText = "Codigo";
            this.ColumnaCodigo.MinimumWidth = 6;
            this.ColumnaCodigo.Name = "ColumnaCodigo";
            this.ColumnaCodigo.Width = 125;
            // 
            // ColumnaPrecio
            // 
            this.ColumnaPrecio.HeaderText = "Precio";
            this.ColumnaPrecio.MinimumWidth = 6;
            this.ColumnaPrecio.Name = "ColumnaPrecio";
            this.ColumnaPrecio.Width = 125;
            // 
            // lblProdCoincidentes
            // 
            this.lblProdCoincidentes.AutoSize = true;
            this.lblProdCoincidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdCoincidentes.Location = new System.Drawing.Point(12, 9);
            this.lblProdCoincidentes.Name = "lblProdCoincidentes";
            this.lblProdCoincidentes.Size = new System.Drawing.Size(360, 25);
            this.lblProdCoincidentes.TabIndex = 24;
            this.lblProdCoincidentes.Text = "Atencion, estos productos coinciden";
            // 
            // ProductosCoincidentesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 513);
            this.Controls.Add(this.lblProdCoincidentes);
            this.Controls.Add(this.dGVProducto);
            this.Name = "ProductosCoincidentesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductosCoincidentesForm";
            ((System.ComponentModel.ISupportInitialize)(this.dGVProducto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVProducto;
        private System.Windows.Forms.Label lblProdCoincidentes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaPrecio;
    }
}