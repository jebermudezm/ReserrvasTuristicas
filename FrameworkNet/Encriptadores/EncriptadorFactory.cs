using FrameworkNet.AdministracionConexiones;
using System;
namespace FrameworkNet.Encriptadores
{
	public class EncriptadorFactory : IEncriptadorFactory
	{
		public IEncriptador CrearEncriptador()
		{
			return new Encriptador();
		}
	}
}
