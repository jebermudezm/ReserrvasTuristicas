using FrameworkNet.AdministracionConexiones;
using System;
namespace FrameworkNet.Repositorios
{
	public class RepositorioFactory : IRepositorioFactory
	{
		public IRepositorio CrearRepositorio(string origen)
		{
			return new Repositorio(origen);
		}
	}
}
