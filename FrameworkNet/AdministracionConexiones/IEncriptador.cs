using System;
using System.Collections.Generic;
namespace FrameworkNet.AdministracionConexiones
{
	public interface IEncriptador
	{
		IDictionary<string, string> Desencriptar(string cadenaEncriptada);
		string Encriptar(IDictionary<string, string> conexiones);
	}
}
