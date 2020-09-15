using System;
using System.Data.SqlClient;

namespace Migration
{
    public static class AccesoDatosSql
    {
        public static void EjecutarSQL(string sql, string cadenaConexion)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            SqlCommand cmd = new SqlCommand();
            try
            {
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                cmd.Connection = conexion;
                cmd.CommandTimeout = 12000;
                conexion.Open();
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                conexion.Close();
            }
            
        }
    }
}
