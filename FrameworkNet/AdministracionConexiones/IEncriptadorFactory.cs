using System;
namespace FrameworkNet.AdministracionConexiones
{
	public interface IEncriptadorFactory
	{
		IEncriptador CrearEncriptador();
	}
}
