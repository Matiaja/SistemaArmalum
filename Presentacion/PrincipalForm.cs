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

namespace Presentacion
{
    public partial class PrincipalForm : Form
    {
        public string rutaArchivo;
        public PrincipalForm()
        {
            InitializeComponent();
            rutaArchivo = @"C:\Users\matia\Downloads\Lista 15_02_24.xlsm";
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                ProcesarCodigo();
            }
        }

        private void ProcesarCodigo()
        {
            string codigo = txtboxCodigo.Text.Trim();

            GestorProductos gestorProductos = new GestorProductos();

            Producto producto = gestorProductos.BuscarProducto(codigo);

            if (producto != null)
            {

                dGVProducto.Rows.Add(producto.Descripcion, producto.Codigo, producto.Precio);

                txtBoxDescripcion.Text = producto.Descripcion;
                txtBoxPrecio.Text = producto.Precio.ToString();

                txtboxCodigo.Clear();

                CalcularTotal();
            }
            else
            {
                MessageBox.Show("Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow fila in dGVProducto.Rows)
            {
                if (fila.Cells["ColumnaPrecio"].Value != null)
                {
                    total += Convert.ToDecimal(fila.Cells["ColumnaPrecio"].Value);
                }
            }
            txtBoxTotal.Text = total.ToString();
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
            if (e.ColumnIndex == dGVProducto.Columns["columnaEliminar"].Index && e.RowIndex >= 0)
            {

                dGVProducto.Rows.RemoveAt(e.RowIndex);
                CalcularTotal();
            }
        }

        private void GenerarPDF(string rutaArchivo)
        {
            // Crear el documento PDF
            Document doc = new Document();
            try
            {
                // Crear un objeto PdfWriter para escribir en el archivo PDF
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));

                // Abrir el documento para agregar contenido
                doc.Open();

                // Agregar título al documento
                Paragraph titulo = new Paragraph("Lista de Productos\n\n");
                doc.Add(titulo);

                // Agregar tabla para mostrar los productos
                PdfPTable tablaProductos = new PdfPTable(3); // 3 columnas: Código, Descripción, Precio
                tablaProductos.WidthPercentage = 100; // Ancho de la tabla (en porcentaje de la página)
                tablaProductos.DefaultCell.Padding = 3; // Espaciado entre las celdas
                tablaProductos.DefaultCell.BorderWidth = 1; // Grosor del borde de las celdas

                // Encabezados de las columnas
                tablaProductos.AddCell("Descripción");
                tablaProductos.AddCell("Código");
                tablaProductos.AddCell("Precio");

                double total = 0;

                List<Producto> ListaDeProductos = new List<Producto>();

                foreach (DataGridViewRow fila in dGVProducto.Rows)
                {
                    // Verificar si la fila no está vacía y si contiene datos válidos
                    if (!fila.IsNewRow && fila.Cells["ColumnaDescripcion"].Value != null && fila.Cells["ColumnaCodigo"].Value != null && fila.Cells["ColumnaPrecio"].Value != null)
                    {
                        string descripcion = fila.Cells["ColumnaDescripcion"].Value.ToString();
                        string codigo = fila.Cells["ColumnaCodigo"].Value.ToString();
                        double precio = Convert.ToDouble(fila.Cells["ColumnaPrecio"].Value);

                        tablaProductos.AddCell(descripcion);
                        tablaProductos.AddCell(codigo);
                        tablaProductos.AddCell(precio.ToString());

                        total += precio;
                    }
                }

                doc.Add(tablaProductos);

                // Agregar el total al documento
                Paragraph totalText = new Paragraph("\nTotal de todos los productos: " + total.ToString());
                doc.Add(totalText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar el documento
                doc.Close();
            }

            //memoryStream.Seek(0, SeekOrigin.Begin);
            //pdfViewer.Load(memoryStream);
        }

        // Método para manejar el evento Click del botón "Imprimir"
        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (dGVProducto.Rows.Count > 0)
            {
                // Obtener la ruta donde se guardará el PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar PDF";
                saveFileDialog.FileName = "ListaProductos.pdf"; // Nombre predeterminado del archivo

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = saveFileDialog.FileName;

                    // Generar el PDF
                    GenerarPDF(rutaArchivo);
                }
            }
            else
            {
                MessageBox.Show("No hay datos para generar el PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}