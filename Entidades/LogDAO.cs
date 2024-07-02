using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class LogDAO
    {
        static string connectionString;
        static SqlCommand command;
        static SqlConnection connection;
        const string COLUMNA_ID = "ID";
        const string COLUMNA_ENTRADA = "Entrada";
        const string COLUMNA_ALUMNO = "Alumno";

        static LogDAO()
        {
            connectionString = @"Data Source=.;Initial Catalog=bomberos-db;Integrated Security=True";
            command = new SqlCommand();
            connection = new SqlConnection(connectionString);
            command.CommandType = System.Data.CommandType.Text; // define el tipo de comando vas a usar, consultas = comando de tipo texto
            command.Connection = connection;
        }
        public static void Guardar(string info)
        {
            try
            {
                connection.Open();
                command.CommandText = $"INSERT INTO dbo.log ({COLUMNA_ENTRADA}, {COLUMNA_ALUMNO}) VALUES (@info, 'Pablo Guillermo')";

                command.Parameters.AddWithValue("@info", info);
                int rows = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error en la conexion a la base de datos. (Metodo GuardarLogDAO)");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }
        public static string Leer() 
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                connection.Open();
                command.CommandText = $"SELECT entrada FROM dbo.log"; //Esto es para generar una consulta
                using (SqlDataReader reader = command.ExecuteReader()) //Realiza la consulta SQL, y trae los registros
                {
                    while (reader.Read())
                    {
                        sb.AppendLine(reader[COLUMNA_ENTRADA].ToString());
                    }
                }
                return sb.ToString();
            }
            catch
            {
                throw new Exception("Ocurrio un error en la conexion a la base de datos. (Metodo LeerLogDAO)");
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
