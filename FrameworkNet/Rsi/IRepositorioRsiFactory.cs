using System;
namespace FrameworkNet.Rsi
{
	public interface IRepositorioRsiFactory
	{
		IRepositorioRsi CrearRepositorioRsi(string connectionString);
	}
}
