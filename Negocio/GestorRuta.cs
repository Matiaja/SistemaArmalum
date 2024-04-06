using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class GestorRuta
    {
       public void ActualizarOModificarLaRuta(string nombre, string nuevaRuta)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            AccesoARutas accesoARutas = new AccesoARutas(accesoDatos.RetornarConnectionString());

            accesoARutas.AgregarOActualizarRuta(nombre, nuevaRuta);

        }

        public void CrearBaseParaRutas()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            accesoDatos.CrearBaseDeDatosSiNoExistearaRutas();

        }

        public string ObtenerLaRuta(string nombre)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            AccesoARutas accesoARutas = new AccesoARutas(accesoDatos.RetornarConnectionString());

            return accesoARutas.ObtenerRutaPorNombre(nombre);

        }
    }
}
