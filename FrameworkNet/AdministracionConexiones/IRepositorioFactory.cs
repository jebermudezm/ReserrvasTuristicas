using System;
namespace FrameworkNet.AdministracionConexiones
{
	public interface IRepositorioFactory
	{
		IRepositorio CrearRepositorio(string origen);
	}
}
