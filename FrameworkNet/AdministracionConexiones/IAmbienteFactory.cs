using System;
namespace FrameworkNet.AdministracionConexiones
{
	public interface IAmbienteFactory
	{
		IAmbiente CrearAmbiente(string url);
	}
}
