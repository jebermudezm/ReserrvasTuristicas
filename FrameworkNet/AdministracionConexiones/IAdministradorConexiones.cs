using System;
using System.Collections.Generic;
namespace FrameworkNet.AdministracionConexiones
{
	public interface IAdministradorConexiones
	{
		string ObtenerConexion(string baseDatos);
		IDictionary<string, string> ObtenerListaConexiones();
	}
}
