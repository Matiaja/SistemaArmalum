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

            ColumnaDescripcion.Width = 300;

            ColumnaCantidad.Width = 50;

            ColumnaPrecio.Width = 70;
            ColumnaSubtotal.Width = 70;

            // Maneja el evento de clic en el botón de eliminación
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
            else if (productos.Count > 1)
            {
                using (ProductosCoincidentesForm productosForm = new ProductosCoincidentesForm(productos))
                {
                    if (productosForm.ShowDialog() == DialogResult.OK)
                    {
                        Producto productoSeleccionado = productosForm.GetProductoSeleccionado();
                        if (productoSeleccionado != null)
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