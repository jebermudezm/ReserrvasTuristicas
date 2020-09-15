using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;


namespace Migration
{
    public static class AccesoDatosOracle
    {

        private static OracleConnection cn { get; set; }

        public static OracleConnection getConeccion(string conexion)
        {
            try
            {
                if (cn == null)
                {
                    cn = new OracleConnection(conexion);
                }

                return cn;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}.");
                return null;
            }
           
        }

        public static string ExecuteQuery(string sql, string conexion)
        {
            var response = "";
            try
            { 
                OracleConnection cn = getConeccion(conexion);
                List<string> lista = new List<string>();
                cn.Open();
                OracleCommand cmd = new OracleCommand(sql, cn);
                cmd.ExecuteNonQuery();
                response = "Proceso Ejecutado con exito.";
            }
            catch (Exception e)
            {
                response = $"Error: {e.Message}";
            }
            finally
            {
                cn.Close();
            }
            return response;
        }

        public static List<string> ObtenerLista(string sql, string conexion)
        {
            OracleConnection cn = getConeccion(conexion);
            List<string> lista = new List<string>();
            cn.Open();
            OracleCommand cmd = new OracleCommand(sql, cn);
            OracleDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    var item = "";
                    item = dr["VALOR"].ToString();
                    lista.Add(item);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                lista = null;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
            return lista;
        }

    }
}
