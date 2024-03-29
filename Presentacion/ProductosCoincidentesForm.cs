using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class ProductosCoincidentesForm : Form
    {
        private List<Producto> productosCoincidentes;
        private Producto productoSeleccionado;
        public ProductosCoincidentesForm(List<Producto> productos)
        {
            InitializeComponent();
            this.productosCoincidentes = productos;
            ConfigurarDataGridView();
            MostrarProductos();
        }

        private void ConfigurarDataGridView()
        {
            // Agrega una columna de botón para agregar
            DataGridViewButtonColumn agregarButtonColumn = new DataGridViewButtonColumn();
            agregarButtonColumn.HeaderText = "Agregar";
            agregarButtonColumn.Name = "ColumnaAgregar";
            agregarButtonColumn.Text = "Agregar";
            agregarButtonColumn.UseColumnTextForButtonValue = true;
            dGVProducto.Columns.Add(agregarButtonColumn);

            // Maneja el evento de clic en el botón de agregar
            dGVProducto.CellContentClick += dGVProducto_CellContentClick;
        }

        private void MostrarProductos()
        {
            foreach (Producto producto in productosCoincidentes)
            {
                dGVProducto.Rows.Add(producto.Descripcion, producto.Codigo, producto.Precio);
            }
        }

        private void dGVProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dGVProducto.Columns["ColumnaAgregar"].Index && e.RowIndex >= 0)
            {
                productoSeleccionado = ObtenerProductoDeFila(e.RowIndex);


                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }

        private Producto ObtenerProductoDeFila(int rowIndex)
        {
            // Verificar si el índice de la fila está dentro del rango válido
            if (rowIndex >= 0 && rowIndex < dGVProducto.Rows.Count)
            {
                DataGridViewRow fila = dGVProducto.Rows[rowIndex];
                // Obtener los valores de las celdas de la fila seleccionada
                string descripcion = fila.Cells["ColumnaDescripcion"].Value.ToString();
                string codigo = fila.Cells["ColumnaCodigo"].Value.ToString();
                double precio = Convert.ToDouble(fila.Cells["ColumnaPrecio"].Value);
                // Crear un nuevo objeto Producto con los valores obtenidos
                Producto productoSeleccionado = new Producto(descripcion, codigo, precio);
                return productoSeleccionado;
            }
            else
            {
                return null;
            }
        }

        public Producto GetProductoSeleccionado()
        {
            return productoSeleccionado;
        }
    }
}
