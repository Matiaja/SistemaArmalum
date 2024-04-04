using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {
        public string NombreYApellido {  get; set; }
        public string Cuil {  get; set; }
        public string Direccion {  get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public int? DiasVigencia { get; set; }

    }
}
