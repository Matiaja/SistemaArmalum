namespace Presentacion
{
    partial class PrincipalForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrincipalForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtBoxTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtBoxPrecio = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtBoxDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtboxCodigo = new System.Windows.Forms.TextBox();
            this.btnImprimirPDF = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.enlazarExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enlazarExcelVentasMensualesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnDatosCliente = new System.Windows.Forms.Button();
            this.dGVProducto = new System.Windows.Forms.DataGridView();
            this.ColumnaCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCargaACtaCte = new System.Windows.Forms.Button();
            this.btnGenerarPDF = new System.Windows.Forms.Button();
            this.btnVentasMensuales = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProducto)).BeginInit();
            this.SuspendLayout();
            // 
            // btnActualizar
            // 
            this.btnActualizar.Font = new System.Drawing.Font("Arial", 13.8F);
            this.btnActualizar.Location = new System.Drawing.Point(997, 114);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(199, 48);
            this.btnActualizar.TabIndex = 32;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(765, 46);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(199, 48);
            this.btnLimpiar.TabIndex = 31;
            this.btnLimpiar.Text = "Limpiar Todo";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtBoxTotal
            // 
            this.txtBoxTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxTotal.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxTotal.Location = new System.Drawing.Point(864, 177);
            this.txtBoxTotal.Name = "txtBoxTotal";
            this.txtBoxTotal.ReadOnly = true;
            this.txtBoxTotal.Size = new System.Drawing.Size(740, 39);
            this.txtBoxTotal.TabIndex = 30;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(753, 181);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(81, 33);
            this.lblTotal.TabIndex = 29;
            this.lblTotal.Text = "Total";
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblPrecio.Location = new System.Drawing.Point(22, 167);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(70, 24);
            this.lblPrecio.TabIndex = 28;
            this.lblPrecio.Text = "Precio";
            // 
            // txtBoxPrecio
            // 
            this.txtBoxPrecio.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPrecio.Location = new System.Drawing.Point(166, 163);
            this.txtBoxPrecio.Name = "txtBoxPrecio";
            this.txtBoxPrecio.ReadOnly = true;
            this.txtBoxPrecio.Size = new System.Drawing.Size(361, 30);
            this.txtBoxPrecio.TabIndex = 27;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblDescripcion.Location = new System.Drawing.Point(22, 114);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(122, 24);
            this.lblDescripcion.TabIndex = 26;
            this.lblDescripcion.Text = "Descripción";
            // 
            // txtBoxDescripcion
            // 
            this.txtBoxDescripcion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxDescripcion.Location = new System.Drawing.Point(166, 110);
            this.txtBoxDescripcion.Name = "txtBoxDescripcion";
            this.txtBoxDescripcion.ReadOnly = true;
            this.txtBoxDescripcion.Size = new System.Drawing.Size(361, 30);
            this.txtBoxDescripcion.TabIndex = 25;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.Location = new System.Drawing.Point(22, 60);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(77, 24);
            this.lblCodigo.TabIndex = 24;
            this.lblCodigo.Text = "Código";
            // 
            // txtboxCodigo
            // 
            this.txtboxCodigo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtboxCodigo.Location = new System.Drawing.Point(166, 56);
            this.txtboxCodigo.Name = "txtboxCodigo";
            this.txtboxCodigo.Size = new System.Drawing.Size(361, 30);
            this.txtboxCodigo.TabIndex = 23;
            this.txtboxCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxCodigo_KeyPress);
            // 
            // btnImprimirPDF
            // 
            this.btnImprimirPDF.Font = new System.Drawing.Font("Arial", 13.8F);
            this.btnImprimirPDF.Location = new System.Drawing.Point(1230, 46);
            this.btnImprimirPDF.Name = "btnImprimirPDF";
            this.btnImprimirPDF.Size = new System.Drawing.Size(199, 48);
            this.btnImprimirPDF.TabIndex = 33;
            this.btnImprimirPDF.Text = "Imprimir PDF";
            this.btnImprimirPDF.UseVisualStyleBackColor = true;
            this.btnImprimirPDF.Click += new System.EventHandler(this.btnImprimirPDF_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(30)))), ((int)(((byte)(61)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enlazarExcelToolStripMenuItem,
            this.enlazarExcelVentasMensualesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1706, 27);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // enlazarExcelToolStripMenuItem
            // 
            this.enlazarExcelToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enlazarExcelToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.enlazarExcelToolStripMenuItem.Name = "enlazarExcelToolStripMenuItem";
            this.enlazarExcelToolStripMenuItem.Size = new System.Drawing.Size(243, 23);
            this.enlazarExcelToolStripMenuItem.Text = "Enlazar Excel Lista de precios";
            this.enlazarExcelToolStripMenuItem.Click += new System.EventHandler(this.enlazarExcelToolStripMenuItem_Click);
            // 
            // enlazarExcelVentasMensualesToolStripMenuItem
            // 
            this.enlazarExcelVentasMensualesToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F);
            this.enlazarExcelVentasMensualesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.enlazarExcelVentasMensualesToolStripMenuItem.Name = "enlazarExcelVentasMensualesToolStripMenuItem";
            this.enlazarExcelVentasMensualesToolStripMenuItem.Size = new System.Drawing.Size(258, 23);
            this.enlazarExcelVentasMensualesToolStripMenuItem.Text = "Enlazar Excel Ventas Mensuales";
            this.enlazarExcelVentasMensualesToolStripMenuItem.Click += new System.EventHandler(this.enlazarExcelVentasMensualesToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btnDatosCliente
            // 
            this.btnDatosCliente.Font = new System.Drawing.Font("Arial", 13.8F);
            this.btnDatosCliente.Location = new System.Drawing.Point(997, 46);
            this.btnDatosCliente.Name = "btnDatosCliente";
            this.btnDatosCliente.Size = new System.Drawing.Size(199, 48);
            this.btnDatosCliente.TabIndex = 35;
            this.btnDatosCliente.Text = "Datos Cliente";
            this.btnDatosCliente.UseVisualStyleBackColor = true;
            this.btnDatosCliente.Click += new System.EventHandler(this.btnDatosCliente_Click);
            // 
            // dGVProducto
            // 
            this.dGVProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGVProducto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVProducto.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dGVProducto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dGVProducto.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(230)))), ((int)(((byte)(193)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVProducto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dGVProducto.ColumnHeadersHeight = 45;
            this.dGVProducto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnaCantidad,
            this.ColumnaDescripcion,
            this.ColumnaCodigo,
            this.ColumnaPrecio,
            this.ColumnaSubtotal});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGVProducto.DefaultCellStyle = dataGridViewCellStyle4;
            this.dGVProducto.EnableHeadersVisualStyles = false;
            this.dGVProducto.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(30)))), ((int)(((byte)(61)))));
            this.dGVProducto.Location = new System.Drawing.Point(0, 239);
            this.dGVProducto.Name = "dGVProducto";
            this.dGVProducto.RowHeadersVisible = false;
            this.dGVProducto.RowHeadersWidth = 51;
            this.dGVProducto.RowTemplate.Height = 35;
            this.dGVProducto.Size = new System.Drawing.Size(1706, 494);
            this.dGVProducto.TabIndex = 36;
            this.dGVProducto.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVProducto_CellValueChanged_1);
            this.dGVProducto.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dGVProducto_RowsAdded);
            // 
            // ColumnaCantidad
            // 
            this.ColumnaCantidad.HeaderText = "Cantidad";
            this.ColumnaCantidad.MinimumWidth = 6;
            this.ColumnaCantidad.Name = "ColumnaCantidad";
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
            // 
            // ColumnaPrecio
            // 
            this.ColumnaPrecio.FillWeight = 180F;
            this.ColumnaPrecio.HeaderText = "Precio";
            this.ColumnaPrecio.MinimumWidth = 6;
            this.ColumnaPrecio.Name = "ColumnaPrecio";
            // 
            // ColumnaSubtotal
            // 
            this.ColumnaSubtotal.FillWeight = 180F;
            this.ColumnaSubtotal.HeaderText = "Subtotal";
            this.ColumnaSubtotal.MinimumWidth = 6;
            this.ColumnaSubtotal.Name = "ColumnaSubtotal";
            this.ColumnaSubtotal.ReadOnly = true;
            // 
            // btnCargaACtaCte
            // 
            this.btnCargaACtaCte.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargaACtaCte.Location = new System.Drawing.Point(765, 114);
            this.btnCargaACtaCte.Name = "btnCargaACtaCte";
            this.btnCargaACtaCte.Size = new System.Drawing.Size(199, 48);
            this.btnCargaACtaCte.TabIndex = 38;
            this.btnCargaACtaCte.Text = "Cargar en Cta Cte";
            this.btnCargaACtaCte.UseVisualStyleBackColor = true;
            this.btnCargaACtaCte.Click += new System.EventHandler(this.btnCargaACtaCte_Click);
            // 
            // btnGenerarPDF
            // 
            this.btnGenerarPDF.Font = new System.Drawing.Font("Arial", 13.8F);
            this.btnGenerarPDF.Location = new System.Drawing.Point(1231, 113);
            this.btnGenerarPDF.Name = "btnGenerarPDF";
            this.btnGenerarPDF.Size = new System.Drawing.Size(199, 48);
            this.btnGenerarPDF.TabIndex = 39;
            this.btnGenerarPDF.Text = "Generar PDF";
            this.btnGenerarPDF.UseVisualStyleBackColor = true;
            this.btnGenerarPDF.Click += new System.EventHandler(this.btnGenerarPDF_Click);
            // 
            // btnVentasMensuales
            // 
            this.btnVentasMensuales.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentasMensuales.Location = new System.Drawing.Point(1460, 46);
            this.btnVentasMensuales.Name = "btnVentasMensuales";
            this.btnVentasMensuales.Size = new System.Drawing.Size(199, 48);
            this.btnVentasMensuales.TabIndex = 40;
            this.btnVentasMensuales.Text = "Ventas Mensuales";
            this.btnVentasMensuales.UseVisualStyleBackColor = true;
            this.btnVentasMensuales.Click += new System.EventHandler(this.btnVentasMensuales_Click);
            // 
            // PrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(142)))), ((int)(((byte)(165)))));
            this.ClientSize = new System.Drawing.Size(1706, 732);
            this.Controls.Add(this.btnVentasMensuales);
            this.Controls.Add(this.btnGenerarPDF);
            this.Controls.Add(this.btnCargaACtaCte);
            this.Controls.Add(this.dGVProducto);
            this.Controls.Add(this.btnDatosCliente);
            this.Controls.Add(this.btnImprimirPDF);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.txtBoxTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtBoxPrecio);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtBoxDescripcion);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtboxCodigo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PrincipalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PrincipalForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVProducto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TextBox txtBoxTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtBoxPrecio;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtBoxDescripcion;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtboxCodigo;
        private System.Windows.Forms.Button btnImprimirPDF;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem enlazarExcelToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button btnDatosCliente;
        private System.Windows.Forms.DataGridView dGVProducto;
        private System.Windows.Forms.Button btnCargaACtaCte;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaSubtotal;
        private System.Windows.Forms.Button btnGenerarPDF;
        private System.Windows.Forms.ToolStripMenuItem enlazarExcelVentasMensualesToolStripMenuItem;
        private System.Windows.Forms.Button btnVentasMensuales;
    }
}

