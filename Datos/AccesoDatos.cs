using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoDatos
    {
        private string server = "127.0.0.1";
        private string database = "sistemaferreteria";
        private string uid = "root";
        private string password = "root";
        private string connectionString;
        public event EventHandler<int> ProgresoActualizado;
        public event EventHandler<int> ProgresoBorradoActualizado;
        public delegate void ProgresoBorradoEventHandler(int progreso);

        public AccesoDatos()
        {
            connectionString = $"SERVER={server};PORT=3306;DATABASE={database};UID={uid};PASSWORD={password};";

        }

        public string RetornarConnectionString()
        {
            return connectionString;
        }

        private void CrearBaseDeDatosSiNoExiste()
        {
            string cadenaConexionSinBaseDatos = $"SERVER={server};PORT=3306;UID={uid};PASSWORD={password};";

            using (MySqlConnection connection = new MySqlConnection(cadenaConexionSinBaseDatos))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {database}", connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.ChangeDatabase(database);

                using (MySqlCommand cmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS producto (descripcion VARCHAR(255), codigo VARCHAR(70), precio DECIMAL(18, 2))", connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmdRuta = new MySqlCommand("CREATE TABLE IF NOT EXISTS ruta (nombre VARCHAR(255), ruta VARCHAR(255))", connection))
                {
                    cmdRuta.ExecuteNonQuery();
                }
            }
        }


        public void CrearBaseDeDatosSiNoExistearaRutas()
        {
            string cadenaConexionSinBaseDatos = $"SERVER={server};PORT=3306;UID={uid};PASSWORD={password};";

            using (MySqlConnection connection = new MySqlConnection(cadenaConexionSinBaseDatos))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {database}", connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.ChangeDatabase(database);

                using (MySqlCommand cmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS producto (descripcion VARCHAR(255), codigo VARCHAR(70), precio DECIMAL(18, 2))", connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmdRuta = new MySqlCommand("CREATE TABLE IF NOT EXISTS ruta (nombre VARCHAR(255), ruta VARCHAR(255))", connection))
                {
                    cmdRuta.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarBaseDeDatos(List<Producto> productos)
        {

            CrearBaseDeDatosSiNoExiste();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                connection.Open();
                string query = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @DatabaseName";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@DatabaseName", database);
                object result = command.ExecuteScalar();

            }


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Iniciar la transacción
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Asociar la transacción con todos los comandos
                    MySqlCommand command = connection.CreateCommand();
                    command.Transaction = transaction;

                    // Borrar todos los productos de la tabla
                    BorrarTodosLosProductos(command);

                    // Insertar la lista de productos en la tabla
                    InsertarProductos(command, productos);

                    // Commit la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // En caso de error, hacer rollback de la transacción
                    transaction.Rollback();
                    // Manejar el error adecuadamente, por ejemplo, lanzar una excepción o registrar el error
                    throw ex;
                }
            }
        }



        private void BorrarTodosLosProductos(MySqlCommand command)
        {
            string query = "DELETE FROM producto";
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

    protected virtual void OnProgresoBorradoActualizado(int progreso)
        {
            ProgresoBorradoActualizado?.Invoke(this, progreso);
        }

        private int ObtenerCantidadRegistros()
        {
            int totalRegistros = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM producto";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    totalRegistros = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return totalRegistros;
        }



        private void InsertarProductos(MySqlCommand command, List<Producto> productos)
        {
            int totalProductos = productos.Count;
            int productosInsertados = 0;

            command.Parameters.Add("@Descripcion", MySqlDbType.VarChar);
            command.Parameters.Add("@Codigo", MySqlDbType.VarChar);
            command.Parameters.Add("@Precio", MySqlDbType.Double);

            foreach (Producto producto in productos)
            {
                command.Parameters["@Descripcion"].Value = producto.Descripcion;
                command.Parameters["@Codigo"].Value = producto.Codigo;
                command.Parameters["@Precio"].Value = producto.Precio;

                string query = "INSERT INTO Producto (Descripcion,Codigo, Precio) VALUES (@Descripcion, @Codigo, @Precio)";
                command.CommandText = query;
                command.ExecuteNonQuery();
                productosInsertados++;
                int progreso = (productosInsertados * 100) / totalProductos;
            }
        }

        public List<Producto> BuscarProductoPorCodigo(string codigo)
        {
            List<Producto> productosEncontrados = new List<Producto>();

            try
            {
                string query = "SELECT codigo, descripcion, precio FROM producto where codigo like @Codigo or descripcion like @Descripcion";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Codigo", "%" + codigo + "%");
                        command.Parameters.AddWithValue("@Descripcion", "%" + codigo + "%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string codigoProducto = reader.GetString("codigo");
                                string descripcion = reader.GetString("descripcion");
                                double precio = reader.GetDouble("precio");

                                Producto producto = new Producto(descripcion, codigoProducto, precio);
                                productosEncontrados.Add(producto);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar productos: " + ex.Message);
            }

            return productosEncontrados;
        }

    }
}
