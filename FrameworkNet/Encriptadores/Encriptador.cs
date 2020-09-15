using FrameworkNet.AdministracionConexiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FrameworkNet.Encriptadores
{
	public class Encriptador : IEncriptador
	{
		private string lin = "{[Lin]}";
		private string sep = "{[Sep]}";
		internal Encriptador()
		{
		}
		public string Encriptar(IDictionary<string, string> conexiones)
		{
			if (conexiones == null || conexiones.Count<KeyValuePair<string, string>>() == 0)
			{
				throw new EncriptadorExcepcion("El argumento <Conexiones> no puede ser un valor nulo, ni una lista vacía");
			}
			string text = "";
			foreach (KeyValuePair<string, string> current in conexiones)
			{
				if (text != "")
				{
					text += this.lin;
				}
				text += this.obtenerDatosConexion(current);
			}
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
		}
		private string obtenerDatosConexion(KeyValuePair<string, string> item)
		{
			if (this.datosIncompletos(item))
			{
				throw new EncriptadorExcepcion("Los datos de la conexion no están completos para realizar esta operacion");
			}
			return item.Key + this.sep + item.Value;
		}
		public IDictionary<string, string> Desencriptar(string cadenaEncriptada)
		{
			if (string.IsNullOrEmpty(cadenaEncriptada))
			{
				throw new EncriptadorExcepcion("El argumento <CadenaEncriptada> no puede ser un valor nulo, ni una lista vacía");
			}
			string[] datosConexiones = Encoding.UTF8.GetString(Convert.FromBase64String(cadenaEncriptada)).Split(new string[]
			{
				this.lin
			}, StringSplitOptions.None);
			IDictionary<string, string> result;
			try
			{
				result = this.extraerConexiones(datosConexiones);
			}
			catch (FrameworkNetExcepcion frameworkNetExcepcion)
			{
				throw new EncriptadorExcepcion(frameworkNetExcepcion.Message, frameworkNetExcepcion);
			}
			return result;
		}
		private IDictionary<string, string> extraerConexiones(string[] datosConexiones)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < datosConexiones.Length; i++)
			{
				string datosConexion = datosConexiones[i];
				dictionary.Add(this.crearConexion(datosConexion));
			}
			return dictionary;
		}
		public KeyValuePair<string, string> crearConexion(string datosConexion)
		{
			string format = "Server={0};Database={1};User ID={2};Password={3}; Connection Timeout={4}; Persist Security Info=True;";
			if (string.IsNullOrEmpty(datosConexion))
			{
				throw new EncriptadorExcepcion("El argumento <cadena> no puede ser un valor nulo, ni una cadena vacía");
			}
			string[] array = datosConexion.Split(new string[]
			{
				this.sep
			}, StringSplitOptions.None);
			if (array.Count<string>() == 2)
			{
				return new KeyValuePair<string, string>(array[0], array[1]);
			}
			if (array.Count<string>() == 5)
			{
				return new KeyValuePair<string, string>(array[2], string.Format(format, new object[]
				{
					array[1],
					array[4],
					array[3],
					array[0],
					30
				}));
			}
			throw new EncriptadorExcepcion("El argumento <cadena> no contiene todos los datos necesarios para crear una conexión");
		}
		private bool datosIncompletos(KeyValuePair<string, string> item)
		{
			return !this.valido(item.Key) || !this.valido(item.Value);
		}
		private bool valido(string propiedad)
		{
			return !string.IsNullOrEmpty(propiedad);
		}
	}
}
