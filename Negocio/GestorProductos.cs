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
using DocumentFormat.OpenXml.Spreadsheet;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

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

        public int ActualizarCantidad(int cantidadExistente)
        {
            return cantidadExistente + 1;
        }


        public void ExportarAVentasMensuales(string total, string rutaVentasMensuales)
        {

            DateTime fechaActual = DateTime.Now;
            int diaActual = fechaActual.Day;
            string mesActual = GetMesEspañol(fechaActual.Month);

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;


            Excel.Workbook workbook = excelApp.Workbooks.Open(rutaVentasMensuales);
            Excel.Worksheet worksheet = workbook.Sheets[1];
            Excel.Range range = worksheet.UsedRange;

            int fila = 2;

            Excel.Range columnaDiaActual = range.Columns[diaActual];

            int filaVacia = GetPrimeraFilaVacia(columnaDiaActual, fila);

            if (filaVacia == 2)
            {
                columnaDiaActual.Cells[2].Value = $"{diaActual} de {mesActual}";
                filaVacia += 1;
            }

            if (filaVacia >= 3)
            {
                columnaDiaActual.Cells[filaVacia].Value = total;
                columnaDiaActual.AutoFit();
            }

            workbook.Save();

            ReleaseObject(worksheet);
            ReleaseObject(workbook);

            excelApp.Quit();

        }

        private int GetPrimeraFilaVacia(Excel.Range columna, int filaInicio)
        {
            int filaActual = filaInicio;
            string valorCelda = columna.Cells[filaActual].Text.Trim();

            while (!string.IsNullOrEmpty(valorCelda))
            {
                filaActual++;
                valorCelda = columna.Cells[filaActual].Text.Trim();
            }

            return filaActual;
        }

        private string GetMesEspañol(int numeroMes)
        {
            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            return meses[numeroMes - 1];
        }

        private void ReleaseObject(object obj)
        {
            if (obj != null)
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
        }

    }
}

