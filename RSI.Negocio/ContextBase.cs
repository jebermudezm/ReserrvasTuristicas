using RSI.Modelo.RepositorioImpl;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace RSI.Negocio
{
    public class ContextBase
    {
        public RSIModelContextDB _context;
        public ContextBase()
        {
            _context = new RSIModelContextDB(ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString);
        }



        private SqlConnection GetConnection(string Name)
        {
            var sqlConection = new SqlConnection(Name);
            return sqlConection;
        }


        protected string DesEncriptar(string textoEncriptado)
        {
            if (textoEncriptado == null)
                return string.Empty;

            if (textoEncriptado.Trim() == String.Empty)
                return string.Empty;

            byte[] vector = Convert.FromBase64String(textoEncriptado);
            UTF8Encoding auxEncodig = new UTF8Encoding();
            return auxEncodig.GetString(vector);
        }

        protected string Encriptar(string textoAEncriptar)
        {
            if (textoAEncriptar == null)
                return string.Empty;

            if (textoAEncriptar.Trim() == String.Empty)
                return string.Empty;

            UTF8Encoding auxEncodig = new UTF8Encoding();
            byte[] vector = auxEncodig.GetBytes(textoAEncriptar);
            return Convert.ToBase64String(vector);
        }

        public string GetAllExeption(Exception ex)
        {
            if (ex == null) return "";
            return ex.Message + "<br/>" + GetAllExeption(ex.InnerException);
        }


        public DateTime ValidarFecha(string fecha)
        {
            try
            {
                var fechaRetorno = DateTime.Today;
                if (!string.IsNullOrEmpty(fecha))
                {
                    fechaRetorno = DateTime.Parse(fecha);
                }
                return fechaRetorno;
            }
            catch (Exception)
            {

                return DateTime.Today;
            }

        }
    }
}
