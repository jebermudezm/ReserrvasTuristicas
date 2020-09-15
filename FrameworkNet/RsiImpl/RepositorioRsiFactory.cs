using FrameworkNet.Rsi;
using System;
namespace FrameworkNet.RsiImpl
{
	public class RepositorioRsiFactory : IRepositorioRsiFactory
	{
		public IRepositorioRsi CrearRepositorioRsi(string connectionString)
		{
			return new RepositorioRsi(connectionString);
		}
	}
}
