using Entidades;
using Datos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace Negocio
{
    public class GestorProductos
    {
        public delegate void ProgresoBorradoEventHandler(object sender, int progreso);


        public void ActualizarBase(string rutaArchivo)
        {
            List<Producto> productosExcel = LeerDesdeExcel(rutaArchivo);

            AccesoDatos accesoDatos = new AccesoDatos();

            accesoDatos.ActualizarBaseDeDatos(productosExcel);

        }

        public List<Producto> LeerDesdeExcel(string rutaArchivo)
        {
            List<Producto> productos = new List<Producto>();

            using (var workbook = new XLWorkbook(rutaArchivo))
            {
                var worksheet = workbook.Worksheet(1); // Suponiendo que los datos están en la primera hoja
                var rows = worksheet.RowsUsed();

                foreach (var row in rows.Skip(1)) // Se omite la primera fila si contiene encabezados
                {
                    string descripcion = row.Cell(1).Value.ToString();
                    string codigo = row.Cell(2).Value.ToString();

                    double precio = 0.0;
                    var precioCell = row.Cell(3);
                    if (!string.IsNullOrEmpty(precioCell.GetString()))
                    {
                        string valorMoneda = precioCell.GetString();
                        valorMoneda = valorMoneda.Replace("$", ""); // Eliminar el símbolo "$"
                        if (double.TryParse(valorMoneda, out double precioValue))
                        {
                            precio = precioValue;
                        }
                    }

                    Producto producto = new Producto(descripcion, codigo, precio);
                    productos.Add(producto);
                }
            }

            return productos;
        }


        public List<Producto> BuscarProducto(string codigo)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            List<Producto> productosEncontrados = accesoDatos.BuscarProductoPorCodigo(codigo);
            return productosEncontrados;

        }

        public double CalcularTotalNegocio(List<double> subtotales)
        {
            double total = 0;

            foreach (Double subtotal in subtotales)
            {
                total += subtotal;
            }

            return total;
        }

        public Dictionary<Producto, double> CalcularSubtotales(Dictionary<Producto, int> productosConCantidad)
        {
            Dictionary<Producto, double> subtotales = new Dictionary<Producto, double>();

            foreach (KeyValuePair<Producto, int> parProductoCantidad in productosConCantidad)
            {
                double subtotal = parProductoCantidad.Key.Precio * parProductoCantidad.Value;
                subtotales.Add(parProductoCantidad.Key, subtotal);
            }

            return subtotales;
        }

    }
}

