using System;
namespace FrameworkNet.AdministracionConexiones
{
	public interface IRepositorio
	{
		string Open();
		void Save(string cadenaEncriptada);
	}
}
