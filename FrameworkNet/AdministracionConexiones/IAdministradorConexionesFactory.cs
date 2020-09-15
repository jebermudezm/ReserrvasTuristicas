using System;
namespace FrameworkNet.AdministracionConexiones
{
	public interface IAdministradorConexionesFactory
	{
		IAdministradorConexiones CrearAdministradorConexiones(IAmbiente ambiente, IRepositorio repositorio, IEncriptador encriptador);
	}
}
