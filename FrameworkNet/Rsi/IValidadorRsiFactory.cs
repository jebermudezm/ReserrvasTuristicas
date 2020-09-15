using System;
namespace FrameworkNet.Rsi
{
	public interface IValidadorRsiFactory
	{
		IValidadorRsi CrearValidadorRsi(IRepositorioRsi repositorioBui);
	}
}
