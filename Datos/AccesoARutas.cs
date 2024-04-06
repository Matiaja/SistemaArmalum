using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoARutas
    {
        private string connectionString;

        public AccesoARutas(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AgregarOActualizarRuta(string nombre, string nuevaRuta)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM ruta WHERE nombre = @nombre";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@nombre", nombre);
                object countObj = checkCommand.ExecuteScalar();
                int count = 0;
                int.TryParse(countObj?.ToString(), out count);
                if (count > 0)
                {
                    string updateQuery = "UPDATE ruta SET ruta = @ruta WHERE nombre = @nombre";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@ruta", nuevaRuta);
                    updateCommand.Parameters.AddWithValue("@nombre", nombre);
                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    string insertQuery = "INSERT INTO ruta (nombre, ruta) VALUES (@nombre, @ruta)";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@nombre", nombre);
                    insertCommand.Parameters.AddWithValue("@ruta", nuevaRuta);
                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public string ObtenerRutaPorNombre(string nombre)
        {
            string ruta = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand checkCommand = new MySqlCommand("SHOW TABLES LIKE 'ruta'", connection))
                    {
                        if (checkCommand.ExecuteNonQuery() > 0)
                        {
                            string query = "SELECT ruta FROM ruta WHERE nombre = @nombre";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@nombre", nombre);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                ruta = result.ToString();
                            }
                        }
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1146)
                {
                }
            }

            return ruta;
        }

    }
}
