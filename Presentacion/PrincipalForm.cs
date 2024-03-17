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

namespace Presentacion
{
    public partial class PrincipalForm : Form
    {
        public string rutaArchivo;
        public PrincipalForm()
        {
            InitializeComponent();
            rutaArchivo = @"C:\Users\matia\Downloads\Lista 15_02_24.xlsm";

            PrintPreviewControl printPreviewControl = new PrintPreviewControl();
            printPreviewControl.Dock = DockStyle.Fill;
            this.Controls.Add(printPreviewControl);
            printPreviewControl.Visible = false;

            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            // Asegúrate de que el DataGridView permite agregar filas
            dGVProducto.AllowUserToAddRows = true;

            // Agrega una columna de botón para eliminar
            DataGridViewButtonColumn eliminarButtonColumn = new DataGridViewButtonColumn();
            eliminarButtonColumn.HeaderText = "Eliminar";
            eliminarButtonColumn.Name = "ColumnaEliminar";
            eliminarButtonColumn.Text = "Eliminar";
            eliminarButtonColumn.UseColumnTextForButtonValue = true;
            dGVProducto.Columns.Add(eliminarButtonColumn);

            // Maneja el evento de clic en el botón de eliminación
            dGVProducto.CellContentClick += DGVProducto_CellContentClick;
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
                dGVProducto.Rows.Add(1,producto.Descripcion, producto.Codigo, producto.Precio);

                txtBoxDescripcion.Text = producto.Descripcion;
                txtBoxPrecio.Text = producto.Precio.ToString();

                txtboxCodigo.Clear();

                CalcularTotal();
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
                            dGVProducto.Rows.Add(1,productoSeleccionado.Descripcion, productoSeleccionado.Codigo, productoSeleccionado.Precio);
                            txtBoxDescripcion.Text = productoSeleccionado.Descripcion;
                            txtBoxPrecio.Text = productoSeleccionado.Precio.ToString();
                            txtboxCodigo.Clear();
                            CalcularTotal();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotal()
        {

            List<Producto> listaProductos = new List<Producto>();

            // Recorre cada fila del DataGridView
            foreach (DataGridViewRow fila in dGVProducto.Rows)
            {
                // Verifica si la fila no es la fila de nuevo ingreso y si la celda de la descripción no es nula
                if (!fila.IsNewRow && fila.Cells["ColumnaDescripcion"].Value != null)
                {
                    // Obtiene los valores de la fila
                    string descripcion = fila.Cells["ColumnaDescripcion"].Value.ToString();
                    string codigo = fila.Cells["ColumnaCodigo"].Value.ToString();
                    double precio = Convert.ToDouble(fila.Cells["ColumnaPrecio"].Value);

                    // Crea un nuevo producto y lo agrega a la lista
                    Producto producto = new Producto(descripcion, codigo, precio);
                    listaProductos.Add(producto);
                }
            }

            GestorProductos gestorProductos = new GestorProductos();

            double Total = gestorProductos.CalcularTotalNegocio(listaProductos);

            txtBoxTotal.Text = Total.ToString();
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
            // Verifica si el clic se realizó en la columna de eliminación
            if (e.ColumnIndex == dGVProducto.Columns["ColumnaEliminar"].Index && e.RowIndex != -1)
            {
                // Elimina la fila seleccionada del DataGridView
                dGVProducto.Rows.RemoveAt(e.RowIndex);

                // Llama a la función para recalcular el total
                CalcularTotal();
            }
        }


        // Método para manejar el evento Click del botón "Imprimir"
        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            PrintDialog printdlg = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();

            // Vista previa del documento
            printPrvDlg.Document = pd;
            printPrvDlg.ShowDialog();

            // Mostrar el cuadro de diálogo de impresión
            printdlg.Document = pd;

            if (printdlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);
            Single yPos = 100;
            Single xPos = 100;

            // Imprimir encabezados
            for (int i = 0; i < dGVProducto.Columns.Count; i++)
            {
                e.Graphics.DrawString(dGVProducto.Columns[i].HeaderText, font, brush, xPos, yPos);
                xPos += 100; // Incrementa la posición para la próxima columna
            }

            yPos += 20; // Incrementa la posición para imprimir el contenido

            // Imprimir contenido del DataGridView
            foreach (DataGridViewRow row in dGVProducto.Rows)
            {
                xPos = 100; // Restablecer la posición X al principio de la fila
                for (int i = 0; i < dGVProducto.Columns.Count; i++)
                {
                    if (row.Cells[i].Value != null)
                    {
                        e.Graphics.DrawString(row.Cells[i].Value.ToString(), font, brush, xPos, yPos);
                    }
                    xPos += 100; // Incrementa la posición para la próxima columna
                }
                yPos += 20; // Incrementa la posición para la próxima fila
            }

            // Imprimir valor total
            yPos += 20; // Incrementa la posición para imprimir el valor total debajo de la tabla
            e.Graphics.DrawString("Total: " + txtBoxTotal.Text, font, brush, 100, yPos);
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {

        }
    }
}