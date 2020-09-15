using FrameworkNet.AdministracionConexiones;
using System;
namespace FrameworkNet.AdministracionConexionesImpl
{
	public class AdministradorConexionesFactory : IAdministradorConexionesFactory
	{
		public IAdministradorConexiones CrearAdministradorConexiones(IAmbiente ambiente, IRepositorio repositorio, IEncriptador encriptador)
		{
			return new AdministradorConexiones(ambiente, repositorio, encriptador);
		}
	}
}
