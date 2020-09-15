using FrameworkNet.AdministracionConexiones;
using System;
using System.IO;
namespace FrameworkNet.Repositorios
{
	public class Repositorio : IRepositorio
	{
		private string path;
		internal Repositorio(string path)
		{
			this.path = path;
		}
		public void Save(string cadenaEncriptada)
		{
			StreamWriter expr_12 = new StreamWriter(new FileStream(this.path, FileMode.Create, FileAccess.Write));
			expr_12.Write(cadenaEncriptada);
			expr_12.Close();
		}
		public string Open()
		{
			StreamReader expr_0B = new StreamReader(this.path);
			string result = expr_0B.ReadToEnd();
			expr_0B.Close();
			return result;
		}
	}
}
