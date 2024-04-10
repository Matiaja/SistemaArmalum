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
using Excel = Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Asn1.X509;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iText.Forms.Xfdf;

namespace Presentacion
{
    public partial class PrincipalForm : Form
    {
        public string rutaArchivo;
        public string rutaExcelVentasMensuales;
        private DataGridViewCellStyle defaultCellStyle;
        private Cliente cliente = new Cliente();
        public PrincipalForm()
        {
            InitializeComponent();
            GestorRuta gestorRuta = new GestorRuta();
            rutaArchivo = gestorRuta.ObtenerLaRuta("Lista de precios");
            rutaExcelVentasMensuales = gestorRuta.ObtenerLaRuta("Ventas Mensuales");

            PrintPreviewControl printPreviewControl = new PrintPreviewControl();
            printPreviewControl.Dock = DockStyle.Fill;
            this.Controls.Add(printPreviewControl);
            printPreviewControl.Visible = false;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            ConfigurarDataGridView();
            defaultCellStyle = dGVProducto.DefaultCellStyle;

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
            dGVProducto.Columns.Insert(0, buttonColumnMenos);

            DataGridViewButtonColumn buttonColumnMas = new DataGridViewButtonColumn();
            buttonColumnMas.HeaderText = "+";
            buttonColumnMas.Name = "ColumnaMas";
            buttonColumnMas.Text = "+";
            buttonColumnMas.UseColumnTextForButtonValue = true;

            dGVProducto.Columns.Insert(2, buttonColumnMas);
            dGVProducto.CellContentClick += DGVProducto_CellContentClick;
            dGVProducto.CellValueChanged += DGVProducto_CellValueChanged;

        }

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
                        float cantidadExistente = (float)Convert.ToDouble(row.Cells["ColumnaCantidad"].Value);

                        float nuevaCantidad = gestorProductos.ActualizarCantidad(cantidadExistente);

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
                    newRow.Cells[dGVProducto.Columns["ColumnaPrecio"].Index].Value = producto.Precio.ToString("C", CultureInfo.CreateSpecificCulture("es-AR"));
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
                                    float cantidadExistente = (float)Convert.ToDouble(row.Cells["ColumnaCantidad"].Value);

                                    float nuevaCantidad = gestorProductos.ActualizarCantidad(cantidadExistente);

                                    row.Cells["ColumnaCantidad"].Value = nuevaCantidad;

                                    row.Height = 35;

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
                                newRow.Cells[dGVProducto.Columns["ColumnaPrecio"].Index].Value = productoSeleccionado.Precio.ToString("C", CultureInfo.CreateSpecificCulture("es-AR"));
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
            Dictionary<Producto, float> productosConCantidad = new Dictionary<Producto, float>();

            foreach (DataGridViewRow fila in dGVProducto.Rows)
            {
                if (!fila.IsNewRow && fila.Cells["ColumnaDescripcion"].Value != null)
                {
                    string descripcion = fila.Cells["ColumnaDescripcion"].Value?.ToString();
                    string codigo = fila.Cells["ColumnaCodigo"].Value?.ToString();
                    string precioConFormato = (string)fila.Cells["ColumnaPrecio"].Value;

                    // Convertir el precio directamente a un valor numérico (decimal)
                    decimal precioDecimal;
                    if (decimal.TryParse(precioConFormato, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-AR"), out precioDecimal))
                    {
                        float cantidad = (float)Convert.ToDouble(fila.Cells["ColumnaCantidad"].Value);

                        Producto producto = new Producto(descripcion, codigo, (double)precioDecimal);
                        productosConCantidad.Add(producto, cantidad);
                    }
                    else
                    {
                        MessageBox.Show("Error: Formato de precio incorrecto en la fila " + fila.Index + ". Corrija el valor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                        fila.Cells["ColumnaSubtotal"].Value = parProductoSubtotal.Value.ToString("C", CultureInfo.CreateSpecificCulture("es-AR"));
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
                    string subtotalConFormato = (string)fila.Cells["ColumnaSubtotal"].Value;

                    decimal subtotalDecimal;
                    if (decimal.TryParse(subtotalConFormato, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-AR"), out subtotalDecimal))
                    {
                        subtotales.Add((double)subtotalDecimal);
                    }
                    else
                    {
                        MessageBox.Show("Error: Formato de subtotal incorrecto en la fila " + fila.Index + ". Corrija el valor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            GestorProductos gestorProductos = new GestorProductos();

            double Total = gestorProductos.CalcularTotalNegocio(subtotales);

            txtBoxTotal.Text = Total.ToString("C", CultureInfo.CreateSpecificCulture("en-AR"));
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dGVProducto.Rows.Clear();
            dGVProducto.DefaultCellStyle = defaultCellStyle;
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
                    float cantidad = (float)Convert.ToDouble(fila.Cells["ColumnaCantidad"].Value);

                    // Verifica si se hizo clic en el botón "-" y la cantidad es mayor que 1
                    if (e.ColumnIndex == dGVProducto.Columns["ColumnaMenos"].Index && cantidad -1 > 0)
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
            if (e.ColumnIndex == dGVProducto.Columns["ColumnaCantidad"].Index && e.RowIndex >= 0)
            {
                // Llama al método para calcular el subtotal
                CalcularSubtotal();

                CalcularTotal();
            }
        }


        // Método para manejar el evento Click del botón "Imprimir"
        private void btnImprimirPDF_Click(object sender, EventArgs e)
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
            btnImprimirPDF.BackColor = Color.FromArgb(33, 230, 193);
            // btnImprimir.BackColor = Color.FromArgb(33, 230, 193);
            btnCargaACtaCte.BackColor = Color.FromArgb(33, 230, 193);
            btnVentasMensuales.BackColor = Color.FromArgb(33, 230, 193);

            ResizeDataGridViewColumns();
        }

        private void enlazarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Seleccionar archivo Excel";

            openFileDialog.Filter = "Archivos Excel|*.xlsm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                GestorRuta gestorRuta = new GestorRuta();
                string selectedFilePath = openFileDialog.FileName;

                rutaArchivo = selectedFilePath;

                gestorRuta.CrearBaseParaRutas();

                gestorRuta.ActualizarOModificarLaRuta("Lista de precios", rutaArchivo);

            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int originalMarginLeft = e.MarginBounds.Left;
            int marginLeft = e.MarginBounds.Left;
            int marginTop = e.MarginBounds.Top;
            int cellHeight = 0;

            int[] columnWidths = { 40, 350, 100, 80, 80 };

            System.Drawing.Image encabezadoImage = Properties.Resources.Logo_Armalum;

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
                    e.Graphics.DrawString("Nombre:", dGVProducto.Font, Brushes.Black, marginLeft, marginTop);
                    e.Graphics.DrawString($"{cliente.NombreYApellido}", dGVProducto.Font, Brushes.Black, marginLeft + 100, marginTop);
                    marginTop += 20;
                }

                if (!string.IsNullOrEmpty(cliente.Cuil))
                {
                    e.Graphics.DrawString("Cuil:", dGVProducto.Font, Brushes.Black, marginLeft, marginTop);
                    e.Graphics.DrawString($"{cliente.Cuil}", dGVProducto.Font, Brushes.Black, marginLeft + 100, marginTop);
                    marginTop += 20;
                }

                if (!string.IsNullOrEmpty(cliente.Direccion))
                {

                    e.Graphics.DrawString("Dirección:", dGVProducto.Font, Brushes.Black, marginLeft, marginTop);
                    e.Graphics.DrawString($"{cliente.Direccion}", dGVProducto.Font, Brushes.Black, marginLeft + 100, marginTop);
                    marginTop += 20;

                }

                if (!string.IsNullOrEmpty(cliente.Localidad))
                {

                    e.Graphics.DrawString("Localidad:", dGVProducto.Font, Brushes.Black, marginLeft, marginTop);
                    e.Graphics.DrawString($"{cliente.Localidad}", dGVProducto.Font, Brushes.Black, marginLeft + 100, marginTop);
                    marginTop += 20;

                }

                if (!string.IsNullOrEmpty(cliente.Telefono))
                {
                    e.Graphics.DrawString("Teléfono:", dGVProducto.Font, Brushes.Black, marginLeft, marginTop);
                    e.Graphics.DrawString($"{cliente.Telefono}", dGVProducto.Font, Brushes.Black, marginLeft + 100, marginTop);
                    marginTop += 20;
                }

                if (cliente.DiasVigencia != null)
                {
                    DateTime fechaHoy = DateTime.Today;
                    e.Graphics.DrawString("Vigencia desde: " + fechaHoy.ToString("dd/MM/yy"), dGVProducto.Font, Brushes.Black, e.MarginBounds.Left, marginTop);

                    float fechaVigenciaHastaX = e.MarginBounds.Right - e.Graphics.MeasureString("Vigencia hasta: " + fechaHoy.ToString("dd/MM/yy"), dGVProducto.Font).Width;

                    DateTime fechaVigenciaHasta = fechaHoy.AddDays(cliente.DiasVigencia.Value);
                    e.Graphics.DrawString("Vigencia hasta: " + fechaVigenciaHasta.ToString("dd/MM/yy"), dGVProducto.Font, Brushes.Black, fechaVigenciaHastaX, marginTop);
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
                marginLeft = originalMarginLeft;  // Reset margin left for each row
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

            float extraWidth = 5;
            float extraHeight = 5;

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

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;

            Excel.Workbook workbook = excelApp.Workbooks.Open(rutaArchivoExcel);
            Excel.Worksheet worksheet = workbook.Sheets[1];
            Excel.Range range = worksheet.UsedRange;

            //int filaInicio = 8;
            //while (worksheet.Cells[filaInicio, 3].Value != null)
            //{
            //    filaInicio++;
            //}

            int numFilasDataGridView = dataGridView.Rows.Count - 1; // Restar 1 para excluir la fila de nuevo

            int filaInicio = EncontrarFilaVaciaContigua(worksheet, numFilasDataGridView);

            if (filaInicio == -1)
            {
                MessageBox.Show("No se encontraron suficientes filas vacías en el documento de Excel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            worksheet.Cells[filaInicio, 1].NumberFormat = "dd/MM/yyyy";
            worksheet.Cells[filaInicio, 1].Value = DateTime.Today;

            int fila = 0;
            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
            {
                if (!dataGridViewRow.IsNewRow)
                {
                    fila++;

                    string cantidad = dataGridViewRow.Cells["ColumnaCantidad"].Value?.ToString();
                    string descripcion = dataGridViewRow.Cells["ColumnaDescripcion"].Value?.ToString();
                    string codigo = dataGridViewRow.Cells["ColumnaCodigo"].Value?.ToString();
                    string precioConFormato = dataGridViewRow.Cells["ColumnaPrecio"].Value?.ToString();

                    decimal precioDecimal;
                    if (decimal.TryParse(precioConFormato, NumberStyles.Currency, CultureInfo.GetCultureInfo("es-AR"), out precioDecimal))
                    {
                        worksheet.Cells[fila + filaInicio - 1, 2].Value = double.TryParse(cantidad, out double cantidadValue) ? cantidadValue : 0;
                        worksheet.Cells[fila + filaInicio - 1, 3].Value = descripcion;
                        worksheet.Cells[fila + filaInicio - 1, 4].Value = "'" + codigo;
                        worksheet.Cells[fila + filaInicio - 1, 5].Value = (double)precioDecimal;
                    }


                }
            }
            workbook.Save();

            ReleaseObject(worksheet);
            ReleaseObject(workbook);

            MessageBox.Show("Los datos se han exportado correctamente a Excel.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private int EncontrarFilaVaciaContigua(Excel.Worksheet worksheet, int numFilas)
        {
            int filaInicio = 8;
            int filaActual = 8;
            int filasVaciasContiguas = 0;

            while (filaActual <= worksheet.Rows.Count && filasVaciasContiguas < numFilas)
            {
                if (worksheet.Cells[filaActual, 3].Value == null)
                {
                    filasVaciasContiguas++;
                }
                else
                {
                    filasVaciasContiguas = 0;
                }

                filaActual++;
            }

            if (filasVaciasContiguas >= numFilas)
            {
                filaInicio = filaActual - numFilas;
            }
            else
            {
                filaInicio = -1;
            }

            return filaInicio;
        }


        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Error al liberar objeto Excel: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GC.Collect();
            }
        }


        private void dGVProducto_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dGVProducto.Columns["ColumnaPrecio"].Index)
            {
                CalcularSubtotal();

                CalcularTotal();
            }
        }

        private void dGVProducto_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in dGVProducto.Rows)
            {
                row.Height = 35;
            }
        }

        private void btnCargaACtaCte_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Archivos Excel|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string rutaArchivoExcel = openFileDialog.FileName;

                    ExportarDataGridViewAExcel(dGVProducto, rutaArchivoExcel);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar los datos a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void ResizeDataGridViewColumns()
        {
            int anchoTotal = dGVProducto.ClientSize.Width;
            double margen = 0.1;

            double porcentajeDescripcion = 0.348;
            double porcentajeCodigo = 0.15;
            double porcentajeCantidad = 0.07;
            double porcentajePrecio = 0.1;
            double porcentajeSubtotal = 0.1;
            double porcentajeEliminar = 0.1;
            double porcentajeBoton = 0.05;

            double anchoDisponible = anchoTotal * (1 - margen);

            int anchoDescripcion = (int)(anchoDisponible * porcentajeDescripcion);
            int anchoCodigo = (int)(anchoDisponible * porcentajeCodigo);
            int anchoCantidad = (int)(anchoDisponible * porcentajeCantidad);
            int anchoPrecio = (int)(anchoDisponible * porcentajePrecio);
            int anchoSubtotal = (int)(anchoDisponible * porcentajeSubtotal);
            int anchoEliminar = (int)(anchoDisponible * porcentajeEliminar);
            int anchoBoton = (int)(anchoDisponible * porcentajeBoton);

            ColumnaDescripcion.Width = anchoDescripcion;
            ColumnaCodigo.Width = anchoCodigo;
            ColumnaCantidad.Width = anchoCantidad;
            ColumnaPrecio.Width = anchoPrecio;
            ColumnaSubtotal.Width = anchoSubtotal;
            dGVProducto.Columns["ColumnaEliminar"].Width = anchoEliminar;
            dGVProducto.Columns["ColumnaMas"].Width = anchoBoton;
            dGVProducto.Columns["ColumnaMenos"].Width = anchoBoton;
        }

        public void GenerarPDF()
        {
            Document doc = new Document(PageSize.A4);


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.Title = "Guardar archivo PDF";
            saveFileDialog.FileName = "output.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string filePath = saveFileDialog.FileName;

                try
                {
                    PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                    doc.Open();
                    Paragraph imageParagraph = new Paragraph();
                    imageParagraph.Alignment = Element.ALIGN_CENTER;
                    byte[] imageBytes = ImageToBytes(Properties.Resources.Logo_Armalum);

                    iTextSharp.text.Image encabezadoImage = iTextSharp.text.Image.GetInstance(imageBytes);
                    int encabezadoWidth = 300;
                    float encabezadoHeight = encabezadoImage.Height * encabezadoWidth / encabezadoImage.Width;
                    float startX = (doc.PageSize.Width - encabezadoWidth) / 2;

                    //doc.Add(new Paragraph(" "));
                    encabezadoImage.ScaleToFit(encabezadoWidth, encabezadoHeight);
                    encabezadoImage.SetAbsolutePosition(startX, doc.PageSize.Height - encabezadoHeight - 20);
                    doc.Add(encabezadoImage);
                    imageParagraph.SpacingBefore = encabezadoHeight;

                    doc.Add(imageParagraph);
                    // doc.Add(new Paragraph(" "));

                    if (cliente != null && (!string.IsNullOrEmpty(cliente.NombreYApellido) ||
                        !string.IsNullOrEmpty(cliente.Cuil) ||
                        !string.IsNullOrEmpty(cliente.Direccion) ||
                        !string.IsNullOrEmpty(cliente.Telefono) ||
                        cliente.DiasVigencia != null))
                    {

                        if (cliente != null && !string.IsNullOrEmpty(cliente.NombreYApellido))
                        {
                            Paragraph NombreYApellido = new Paragraph();
                            NombreYApellido.Alignment = Element.ALIGN_LEFT;
                            NombreYApellido.Add(new Phrase("Nombre y apellido: ", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                            NombreYApellido.Add(new Phrase(cliente.NombreYApellido, FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL)));
                            doc.Add(NombreYApellido);
                        }

                        if (!string.IsNullOrEmpty(cliente.Cuil))
                        {
                            Paragraph cuilParrafo = new Paragraph();
                            cuilParrafo.Alignment = Element.ALIGN_LEFT;
                            cuilParrafo.Add(new Phrase("CUIL: ", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                            cuilParrafo.Add(new Phrase(cliente.Cuil ?? "", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL)));
                            doc.Add(cuilParrafo);
                        }

                        if (!string.IsNullOrEmpty(cliente.Direccion))
                        {
                            Paragraph direccionParrafo = new Paragraph();
                            direccionParrafo.Alignment = Element.ALIGN_LEFT;
                            direccionParrafo.Add(new Phrase("Dirección: ", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                            direccionParrafo.Add(new Phrase(cliente.Direccion ?? "", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL)));
                            doc.Add(direccionParrafo);
                        }

                        if (!string.IsNullOrEmpty(cliente.Localidad))
                        {
                            Paragraph localidadParrafo = new Paragraph();
                            localidadParrafo.Alignment = Element.ALIGN_LEFT;
                            localidadParrafo.Add(new Phrase("Localidad: ", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                            localidadParrafo.Add(new Phrase(cliente.Localidad ?? "", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL)));
                            doc.Add(localidadParrafo);
                        }

                        if (!string.IsNullOrEmpty(cliente.Telefono))
                        {
                            Paragraph telefonoParrafo = new Paragraph();
                            telefonoParrafo.Alignment = Element.ALIGN_LEFT;
                            telefonoParrafo.Add(new Phrase("Teléfono: ", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                            telefonoParrafo.Add(new Phrase(cliente.Telefono ?? "", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL)));
                            doc.Add(telefonoParrafo);
                        }


                        if (cliente.DiasVigencia != null)
                        {
                            Paragraph fechasParrafo = new Paragraph();
                            fechasParrafo.Alignment = Element.ALIGN_LEFT;

                            DateTime fechaHoy = DateTime.Today;
                            DateTime fechaVigenciaHasta = fechaHoy.AddDays(cliente.DiasVigencia.Value);

                            fechasParrafo.Add(new Phrase("Vigencia desde: ", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                            fechasParrafo.Add(new Phrase(fechaHoy.ToString("dd/MM/yy"), FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL)));

                            fechasParrafo.Add(new Chunk("    ", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL)));

                            fechasParrafo.Add(new Phrase("Vigencia hasta: ", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                            fechasParrafo.Add(new Phrase(fechaVigenciaHasta.ToString("dd/MM/yy"), FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL)));

                            doc.Add(fechasParrafo);
                        
                    }

                        doc.Add(new Paragraph(" "));

                    }

                    PdfPTable table = new PdfPTable(5);

                    //float[] widths = new float[] { 40f, 350f, 100f, 80f, 80f };

                    //table.SetWidths(widths);

                    table.TotalWidth = doc.PageSize.Width;

                    table.AddCell(new PdfPCell(new Phrase("Cantidad")));
                    table.AddCell(new PdfPCell(new Phrase("Descripción")));
                    table.AddCell(new PdfPCell(new Phrase("Código")));
                    table.AddCell(new PdfPCell(new Phrase("Precio")));
                    table.AddCell(new PdfPCell(new Phrase("Subtotal")));

                    foreach (DataGridViewRow row in dGVProducto.Rows)
                    {
                        if (row.Cells["ColumnaCantidad"].Value != null)
                        {
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["ColumnaCantidad"].Value.ToString())));
                        }
                        else
                        {
                            table.AddCell(new PdfPCell(new Phrase(""))); 
                        }

                        if (row.Cells["ColumnaDescripcion"].Value != null)
                        {
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["ColumnaDescripcion"].Value.ToString())));
                        }
                        else
                        {
                            table.AddCell(new PdfPCell(new Phrase("")));
                        }

                        if (row.Cells["ColumnaCodigo"].Value != null)
                        {
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["ColumnaCodigo"].Value.ToString())));
                        }
                        else
                        {
                            table.AddCell(new PdfPCell(new Phrase("")));
                        }

                        if (row.Cells["ColumnaPrecio"].Value != null)
                        {
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["ColumnaPrecio"].Value.ToString())));
                        }
                        else
                        {
                            table.AddCell(new PdfPCell(new Phrase("")));
                        }

                        if (row.Cells["ColumnaSubtotal"].Value != null)
                        {
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["ColumnaSubtotal"].Value.ToString())));
                        }
                        else
                        {
                            table.AddCell(new PdfPCell(new Phrase("")));
                        }

                    }

                    doc.Add(table);

                    Paragraph totalParagraph = new Paragraph();
                    totalParagraph.Add(new Phrase("Total: " + txtBoxTotal.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    totalParagraph.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(totalParagraph);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar PDF: " + ex.Message);
                }
                finally
                {
                    doc.Close();
                }
            }
        }
        private byte[] ImageToBytes(System.Drawing.Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            GenerarPDF();
        }

        private void enlazarExcelVentasMensualesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Seleccionar archivo Excel de ventas Mensuales";

            openFileDialog.Filter = "Archivos Excel|*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                GestorRuta gestorRuta = new GestorRuta();
                string selectedFilePath = openFileDialog.FileName;

                rutaExcelVentasMensuales = selectedFilePath;

                gestorRuta.CrearBaseParaRutas();

                gestorRuta.ActualizarOModificarLaRuta("Ventas Mensuales", rutaExcelVentasMensuales);

            }
        }

        private void btnVentasMensuales_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rutaExcelVentasMensuales))
            {
                MessageBox.Show("Primero selecciona un archivo Excel de ventas mensuales.", "Archivo no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {
                GestorProductos gestorProductos = new GestorProductos();
                gestorProductos.ExportarAVentasMensuales(txtBoxTotal.Text, rutaExcelVentasMensuales);
            }
        }

    }
}