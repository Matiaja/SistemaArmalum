using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public Producto(string descripcion, string codigo, double precio)
        {
            
            this.Descripcion = descripcion;
            this.Codigo = codigo;
            this.Precio = precio;
        }


    }


}
