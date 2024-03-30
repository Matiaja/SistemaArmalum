using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using PdfiumViewer;
using System.Windows.Media.Animation;
using System.Drawing.Printing;
using static System.Net.WebRequestMethods;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Presentacion
{
    public partial class PrincipalForm : Form
    {
        public string rutaArchivo;
        private Cliente cliente = new Cliente();
        public PrincipalForm()
        {
            InitializeComponent();
            rutaArchivo = Properties.Settings.Default.RutaArchivo;

            PrintPreviewControl printPreviewControl = new PrintPreviewControl();
            printPreviewControl.Dock = DockStyle.Fill;
            this.Controls.Add(printPreviewControl);
            printPreviewControl.Visible = false;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            ConfigurarDataGridView();
        }


        public void ConfigurarDataGridView()
        {
            dGVProducto.AllowUserToAddRows = true;

            DataGridViewButtonColumn eliminarButtonColumn = new DataGridViewButtonColumn();
            eliminarButtonColumn.HeaderText = "Eliminar";
            eliminarButtonColumn.Name = "ColumnaEliminar";
            eliminarButtonColumn.Text = "Eliminar";
            eliminarButtonColumn.UseColumnTextForButtonValue = true;
            dGVProducto.Columns.Add(eliminarButtonColumn);

            DataGridViewButtonColumn buttonColumnMenos = new DataGridViewButtonColumn();
            buttonColumnMenos.HeaderText = "-";
            buttonColumnMenos.Name = "ColumnaMenos";
            buttonColumnMenos.Text = "-";
            buttonColumnMenos.UseColumnTextForButtonValue = true;
            buttonColumnMenos.Width = 40;
            dGVProducto.Columns.Insert(0, buttonColumnMenos);

            DataGridViewButtonColumn buttonColumnMas = new DataGridViewButtonColumn();
            buttonColumnMas.HeaderText = "+";
            buttonColumnMas.Name = "ColumnaMas";
            buttonColumnMas.Text = "+";
            buttonColumnMas.UseColumnTextForButtonValue = true;
            buttonColumnMas.Width = 40;

            dGVProducto.Columns.Insert(2, buttonColumnMas);

            ColumnaDescripcion.Width = 400;

            ColumnaCantidad.Width = 80;

            ColumnaPrecio.Width = 100;
            ColumnaSubtotal.Width = 100;

            dGVProducto.CellContentClick += DGVProducto_CellContentClick;
            dGVProducto.CellValueChanged += DGVProducto_CellValueChanged;

        }

        //private void dGVProducto_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        //{
        //    FormatearCeldas();
        //}

        //private void FormatearCeldas()
        //{
        //    foreach (DataGridViewRow row in dGVProducto.Rows)
        //    {
        //        FormatearCelda(row.Cells["ColumnaPrecio"]);
        //        FormatearCelda(row.Cells["ColumnaSubtotal"]);
        //        FormatearCelda(row.Cells["ColumnaTotal"]);
        //    }
        //}

        //private void FormatearCelda(DataGridViewCell cell)
        //{
        //    if (cell != null && cell.Value != null && double.TryParse(cell.Value.ToString(), out double valor))
        //    {
        //        cell.Value = valor.ToString("C");
        //    }
        //}


        private void btnActualizar_Click(object sender, EventArgs e)
        {

            GestorProductos negocio = new GestorProductos();

            try
            {
                AvisoForm aviso = new AvisoForm();
                aviso.Show();
                negocio.ActualizarBase(rutaArchivo);
                aviso.Close();

                MessageBox.Show("La base de datos ha sido actualizada correctamente.", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al actualizar la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtboxCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtboxCodigo.Text != "")
            {
                ProcesarCodigo();
            }
        }

        private void ProcesarCodigo()
        {
            string codigo = txtboxCodigo.Text.Trim();

            GestorProductos gestorProductos = new GestorProductos();

            List<Producto> productos = gestorProductos.BuscarProducto(codigo);

            if (productos != null && productos.Count == 1)
            {

                Producto producto = productos[0];
                bool productoExistente = false;

                foreach (DataGridViewRow row in dGVProducto.Rows)
                {
                    if (row.Cells["ColumnaCodigo"].Value != null && row.Cells["ColumnaCodigo"].Value.ToString() == producto.Codigo)
                    {
                        int cantidadExistente = Convert.ToInt32(row.Cells["ColumnaCantidad"].Value);

                        int nuevaCantidad = gestorProductos.ActualizarCantidad(cantidadExistente);

                        row.Cells["ColumnaCantidad"].Value = nuevaCantidad;

                        productoExistente = true;
                        txtboxCodigo.Clear();
                        break;
                    }
                }

                if (!productoExistente)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(dGVProducto);
                    newRow.Cells[dGVProducto.Columns["ColumnaDescripcion"].Index].Value = producto.Descripcion;
                    newRow.Cells[dGVProducto.Columns["ColumnaCodigo"].Index].Value = producto.Codigo;
                    newRow.Cells[dGVProducto.Columns["ColumnaPrecio"].Index].Value = producto.Precio;
                    newRow.Cells[dGVProducto.Columns["ColumnaCantidad"].Index].Value = 1;
                    dGVProducto.Rows.Add(newRow);

                    txtBoxDescripcion.Text = producto.Descripcion;
                    txtBoxPrecio.Text = producto.Precio.ToString();

                    CalcularSubtotal();

                    txtboxCodigo.Clear();

                    CalcularTotal();
                }
                
            }
            else if (productos.Count > 1)
            {
                using (ProductosCoincidentesForm productosForm = new ProductosCoincidentesForm(productos))
                {
                    if (productosForm.ShowDialog() == DialogResult.OK)
                    {
                        Producto productoSeleccionado = productosForm.GetProductoSeleccionado();
                        if (productoSeleccionado != null)
                        {
                            bool productoExistente = false;

                            foreach (DataGridViewRow row in dGVProducto.Rows)
                            {
                                if (row.Cells["ColumnaCodigo"].Value != null && row.Cells["ColumnaCodigo"].Value.ToString() == productoSeleccionado.Codigo)
                                {
                                    int cantidadExistente = Convert.ToInt32(row.Cells["ColumnaCantidad"].Value);

                                    int nuevaCantidad = gestorProductos.ActualizarCantidad(cantidadExistente);

                                    row.Cells["ColumnaCantidad"].Value = nuevaCantidad;

                                    productoExistente = true;
                                    txtboxCodigo.Clear();
                                    break;
                                }
                            }


                            if (!productoExistente)
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dGVProducto);
                                newRow.Cells[dGVProducto.Columns["ColumnaDescripcion"].Index].Value = productoSeleccionado.Descripcion;
                                newRow.Cells[dGVProducto.Columns["ColumnaCodigo"].Index].Value = productoSeleccionado.Codigo;
                                newRow.Cells[dGVProducto.Columns["ColumnaPrecio"].Index].Value = productoSeleccionado.Precio;
                                newRow.Cells[dGVProducto.Columns["ColumnaCantidad"].Index].Value = 1;
                                dGVProducto.Rows.Add(newRow);
                                txtBoxDescripcion.Text = productoSeleccionado.Descripcion;
                                txtBoxPrecio.Text = productoSeleccionado.Precio.ToString();
                                CalcularSubtotal();
                                txtboxCodigo.Clear();
                                CalcularTotal();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularSubtotal()
        {
            Dictionary<Producto, int> productosConCantidad = new Dictionary<Producto, int>();

            foreach (DataGridViewRow fila in dGVProducto.Rows)
            {
                if (!fila.IsNewRow && fila.Cells["ColumnaDescripcion"].Value != null)
                {
                    string descripcion = fila.Cells["ColumnaDescripcion"].Value?.ToString();
                    string codigo = fila.Cells["ColumnaCodigo"].Value?.ToString();
                    double precio = Convert.ToDouble(fila.Cells["ColumnaPrecio"].Value);
                    int cantidad = Convert.ToInt32(fila.Cells["ColumnaCantidad"].Value);

                    Producto producto = new Producto(descripcion, codigo, precio);

                    productosConCantidad.Add(producto, cantidad);
                }
            }

            GestorProductos gestorProductos = new GestorProductos();
            Dictionary<Producto, double> subtotales = gestorProductos.CalcularSubtotales(productosConCantidad);

            foreach (KeyValuePair<Producto, double> parProductoSubtotal in subtotales)
            {
                foreach (DataGridViewRow fila in dGVProducto.Rows)
                {
                    if (!fila.IsNewRow && fila.Cells["ColumnaDescripcion"].Value.ToString() == parProductoSubtotal.Key.Descripcion)
                    {
                        fila.Cells["ColumnaSubtotal"].Value = parProductoSubtotal.Value;
                        break;
                    }
                }
            }
        }


        private void CalcularTotal()
        {

            List<double> subtotales = new List<double>();

            // Recorre cada fila del DataGridView
            foreach (DataGridViewRow fila in dGVProducto.Rows)
            {
                // Verifica si la fila no es la fila de nuevo ingreso y si la celda de la descripción no es nula
                if (!fila.IsNewRow && fila.Cells["ColumnaSubtotal"].Value != null)
                {
                    // Obtiene los valores de la fila
                    double subtotal = Convert.ToDouble(fila.Cells["ColumnaSubtotal"].Value);

                    subtotales.Add(subtotal);
                }
            }

            GestorProductos gestorProductos = new GestorProductos();

            double Total = gestorProductos.CalcularTotalNegocio(subtotales);

            txtBoxTotal.Text = Total.ToString("C");
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dGVProducto.Rows.Clear();
            txtBoxDescripcion.Clear();
            txtboxCodigo.Clear();
            txtBoxPrecio.Clear();
            txtBoxTotal.Clear();
        }

        private void dGVProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DGVProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dGVProducto.Columns["ColumnaEliminar"].Index && e.RowIndex != -1)
            {

                if (!dGVProducto.Rows[e.RowIndex].IsNewRow)
                {
                    dGVProducto.Rows.RemoveAt(e.RowIndex);

                    CalcularTotal();
                }
            }

            if (e.RowIndex >= 0 && (e.ColumnIndex == dGVProducto.Columns["ColumnaMenos"].Index || e.ColumnIndex == dGVProducto.Columns["ColumnaMas"].Index))
            {
                DataGridViewRow fila = dGVProducto.Rows[e.RowIndex];

                // Verifica si la celda de la cantidad no es nula
                if (fila.Cells["ColumnaCantidad"].Value != null)
                {
                    int cantidad = Convert.ToInt32(fila.Cells["ColumnaCantidad"].Value);

                    // Verifica si se hizo clic en el botón "-" y la cantidad es mayor que 1
                    if (e.ColumnIndex == dGVProducto.Columns["ColumnaMenos"].Index && cantidad > 1)
                    {
                        cantidad--;
                    }
                    // Verifica si se hizo clic en el botón "+"
                    else if (e.ColumnIndex == dGVProducto.Columns["ColumnaMas"].Index)
                    {
                        cantidad++;
                    }

                    // Actualiza el valor de la cantidad en la celda correspondiente
                    fila.Cells["ColumnaCantidad"].Value = cantidad;

                    // Calcula el subtotal
                    CalcularSubtotal();
                }
            }

        }

        private void DGVProducto_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si la celda modificada es la de la columna de cantidad
            if (e.ColumnIndex == dGVProducto.Columns["ColumnaCantidad"].Index && e.RowIndex >= 0)
            {
                // Llama al método para calcular el subtotal
                CalcularSubtotal();

                CalcularTotal();
            }
        }


        // Método para manejar el evento Click del botón "Imprimir"
        private void btnGuardarPDF_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument1;

            // Show the print preview dialog
            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
            {
                // User clicked OK, initiate printing
                printDocument1.Print();
            }
        }
        

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            btnLimpiar.BackColor = Color.FromArgb(33, 230, 193);
            btnDatosCliente.BackColor = Color.FromArgb(33, 230, 193);
            btnActualizar.BackColor = Color.FromArgb(33, 230, 193);
            btnGuardarPDF.BackColor = Color.FromArgb(33, 230, 193);
        }

        private void enlazarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Seleccionar archivo Excel";

            openFileDialog.Filter = "Archivos Excel|*.xlsm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                
                rutaArchivo = selectedFilePath;

                Properties.Settings.Default.RutaArchivo = rutaArchivo;

                Properties.Settings.Default.Save();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int marginLeft = e.MarginBounds.Left;
            int marginTop = e.MarginBounds.Top;
            int cellHeight = 0;

            int[] columnWidths = { 40, 350, 100, 80, 80 };

            System.Drawing.Image encabezadoImage = System.Drawing.Image.FromFile("C:\\Users\\matia\\Desktop\\SistemaParaFerreteria2024\\Logo_Armalum.png");

            int encabezadoWidth = 300;
            int encabezadoHeight = encabezadoImage.Height * encabezadoWidth / encabezadoImage.Width;
            int startX = (e.MarginBounds.Width - encabezadoWidth) / 2 + e.MarginBounds.Left;


            e.Graphics.DrawImage(encabezadoImage, new System.Drawing.Rectangle(startX, e.MarginBounds.Top, encabezadoWidth, encabezadoHeight));

            marginTop += 130;

            if (cliente != null && (!string.IsNullOrEmpty(cliente.NombreYApellido) ||
                        !string.IsNullOrEmpty(cliente.Cuil) ||
                        !string.IsNullOrEmpty(cliente.Direccion) ||
                        !string.IsNullOrEmpty(cliente.Telefono) ||
                        cliente.DiasVigencia != null))
            {
                if (!string.IsNullOrEmpty(cliente.NombreYApellido))
                {
                    e.Graphics.DrawString("Nombre:", dGVProducto.Font, Brushes.Black, e.MarginBounds.Left, marginTop);
                    e.Graphics.DrawString($"{cliente.NombreYApellido}", dGVProducto.Font, Brushes.Black, e.MarginBounds.Left + 100, marginTop);
                    marginTop += 20;
                }

                if (!string.IsNullOrEmpty(cliente.Cuil))
                {
                    e.Graphics.DrawString("Cuil:", dGVProducto.Font, Brushes.Black, e.MarginBounds.Left, marginTop);
                    e.Graphics.DrawString($"{cliente.Cuil}", dGVProducto.Font, Brushes.Black, e.MarginBounds.Left + 100, marginTop);
                    marginTop += 20;
                }

                if (!string.IsNullOrEmpty(cliente.Direccion))
                {
                    e.Graphics.DrawString("Dirección:", dGVProducto.Font, Brushes.Black, e.MarginBounds.Left, marginTop);
                    e.Graphics.DrawString($"{cliente.Direccion}", dGVProducto.Font, Brushes.Black, e.MarginBounds.Left + 100, marginTop);
                    marginTop += 20;
                }

                if (!string.IsNullOrEmpty(cliente.Telefono))
                {
                    e.Graphics.DrawString("Teléfono:", dGVProducto.Font, Brushes.Black, e.MarginBounds.Left, marginTop);
                    e.Graphics.DrawString($"{cliente.Telefono}", dGVProducto.Font, Brushes.Black, e.MarginBounds.Left + 100, marginTop);
                    marginTop += 20;
                }

                if (cliente.DiasVigencia != null)
                {
                    DateTime fechaHoy = DateTime.Today;
                    e.Graphics.DrawString("Vigencia desde: " + fechaHoy.ToString("dd/MM/yy"), dGVProducto.Font, Brushes.Black, e.MarginBounds.Left, marginTop);
                    marginTop += 20;

                    DateTime fechaVigenciaHasta = fechaHoy.AddDays(cliente.DiasVigencia.Value);
                    e.Graphics.DrawString("Vigencia hasta: " + fechaVigenciaHasta.ToString("dd/MM/yy"), dGVProducto.Font, Brushes.Black, e.MarginBounds.Left, marginTop);
                    marginTop += 20;
                }
                e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, marginTop, e.MarginBounds.Right, marginTop);
                marginTop += 40;
            }            

            // Draw table header
            string[] headers = { "Cant", "Descripción", "Código", "Precio", "Subtotal" };
            int headerIndex = 0;
            for (int i = 0; i < dGVProducto.Columns.Count; i++)
            {
                DataGridViewColumn column = dGVProducto.Columns[i];
                if (column.Name == "ColumnaCantidad" ||
                    column.Name == "ColumnaDescripcion" ||
                    column.Name == "ColumnaCodigo" ||
                    column.Name == "ColumnaPrecio" ||
                    column.Name == "ColumnaSubtotal")
                {

                    e.Graphics.DrawString(headers[headerIndex], dGVProducto.Font, Brushes.Black, marginLeft, marginTop);
                    marginLeft += columnWidths[headerIndex];
                    headerIndex++;
                }
            }
            marginTop += dGVProducto.Rows[0].Height;

            // Draw each row
            foreach (DataGridViewRow row in dGVProducto.Rows)
            {
                marginLeft = e.MarginBounds.Left; // Reset margin left for each row
                cellHeight = 0; // Reset cell height for each row

                // Draw each desired column
                int columnIndex = 0;
                for (int i = 0; i < dGVProducto.Columns.Count; i++)
                {
                    DataGridViewColumn column = dGVProducto.Columns[i];
                    if (column.Name == "ColumnaCantidad" ||
                        column.Name == "ColumnaDescripcion" ||
                        column.Name == "ColumnaCodigo" ||
                        column.Name == "ColumnaPrecio" ||
                        column.Name == "ColumnaSubtotal")
                    {
                        string cellValue = row.Cells[column.Index].FormattedValue.ToString();

                        // Truncate the description if it exceeds 300 characters
                        if (column.Name == "ColumnaDescripcion" && cellValue.Length > 60)
                        {
                            cellValue = cellValue.Substring(0, 60) + "...";
                        }

                        if (column.Name == "ColumnaCodigo" && cellValue.Length > 15)
                        {
                            cellValue = cellValue.Substring(0, 15) + "...";
                        }

                        e.Graphics.DrawString(cellValue, dGVProducto.Font, Brushes.Black, marginLeft, marginTop);

                        marginLeft += columnWidths[columnIndex];
                        cellHeight = Math.Max(cellHeight, row.Cells[column.Index].Size.Height);
                        columnIndex++;
                    }
                }

                marginTop += cellHeight;
            }

            marginTop += 10;
            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, marginTop, e.MarginBounds.Right, marginTop);

            marginLeft = e.MarginBounds.Left + columnWidths.Sum() - columnWidths.Last();

            // Calcula el ancho del total
            int totalWidth = columnWidths.Last();

            // Calcula la posición vertical del total
            int totalTop = marginTop + cellHeight;

            SizeF totalTextSize = e.Graphics.MeasureString("Total: " + txtBoxTotal.Text, dGVProducto.Font);

            // Define el ancho y la altura adicionales para el rectángulo
            float extraWidth = 5; // Espacio adicional a la izquierda y a la derecha del texto
            float extraHeight = 5; // Espacio adicional encima y debajo del texto

            // Dibuja el total con un rectángulo que rodea
            RectangleF totalRect = new RectangleF(marginLeft - extraWidth, totalTop, totalTextSize.Width + (2 * extraWidth), totalTextSize.Height + (2 * extraHeight));

            e.Graphics.DrawString("Total: " + txtBoxTotal.Text, new System.Drawing.Font(dGVProducto.Font, FontStyle.Bold), Brushes.Black, totalRect, new StringFormat { LineAlignment = StringAlignment.Center });
            e.Graphics.DrawRectangle(Pens.Black, System.Drawing.Rectangle.Round(totalRect));
        }

        private void btnDatosCliente_Click(object sender, EventArgs e)
        {
            DatosClientesForm datosClientesForm = new DatosClientesForm(cliente);
            datosClientesForm.ShowDialog();

            if (datosClientesForm.DialogResult == DialogResult.OK)
            {
                cliente = datosClientesForm.cliente;
            }
        }

        public void ExportarDataGridViewAExcel(DataGridView dataGridView, string rutaArchivoExcel)
        {
            
            if (!System.IO.File.Exists(rutaArchivoExcel))
            {
                MessageBox.Show("El archivo de Excel seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileInfo existingFile = new FileInfo(rutaArchivoExcel);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                
                int filaInicio = 8;
                while (!string.IsNullOrEmpty(worksheet.Cells[filaInicio, 3].Text)
                       
                       )
                {
                    filaInicio++;
                }
                worksheet.Cells[filaInicio, 1].Value = DateTime.Today.ToString("dd/MM/yyyy");

                Console.WriteLine(filaInicio);

                for (int fila = 0; fila < dataGridView.Rows.Count; fila++)
                {
                    string cantidad = dataGridView.Rows[fila].Cells["ColumnaCantidad"].Value?.ToString();
                    string descripcion = dataGridView.Rows[fila].Cells["ColumnaDescripcion"].Value?.ToString();
                    string codigo = dataGridView.Rows[fila].Cells["ColumnaCodigo"].Value?.ToString();
                    string precio = dataGridView.Rows[fila].Cells["ColumnaPrecio"].Value?.ToString();

                    worksheet.Cells[fila + filaInicio, 2].Value = double.TryParse(cantidad, out double cantidadValue) ? cantidadValue : 0;
                    worksheet.Cells[fila + filaInicio, 3].Value = descripcion;
                    worksheet.Cells[fila + filaInicio, 4].Value = codigo;
                    worksheet.Cells[fila + filaInicio, 5].Value = double.TryParse(precio, out double precioValue) ? precioValue : 0;
                }

                worksheet.Cells[filaInicio, 2, filaInicio + dataGridView.Rows.Count - 1, 2].Style.Numberformat.Format = "_-* #,##0_-;-* #,##0_-;_-* \"-\"??_-;_-@_-";
                worksheet.Cells[filaInicio, 5, filaInicio + dataGridView.Rows.Count - 1, 5].Style.Numberformat.Format = "_-* #,##0.00_-;-* #,##0.00_-;_-* \"-\"??_-;_-@_-";

                for (int fila = filaInicio; fila < filaInicio + dataGridView.Rows.Count; fila++)
                {
                    worksheet.Row(fila).Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Row(fila).Style.Fill.BackgroundColor.SetColor(Color.White);
                }

                package.Save();
            }
        }

        private void cargarAExcelDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Archivos Excel|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string rutaArchivoExcel = openFileDialog.FileName;

                    ExportarDataGridViewAExcel(dGVProducto, rutaArchivoExcel);

                    MessageBox.Show("Los datos se han exportado correctamente a Excel.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar los datos a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}