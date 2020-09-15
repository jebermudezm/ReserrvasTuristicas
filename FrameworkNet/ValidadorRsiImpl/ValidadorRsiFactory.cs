using FrameworkNet.Rsi;
using System;
namespace FrameworkNet.ValidadorRsiImpl
{
	public class ValidadorRsiFactory : IValidadorRsiFactory
	{
		public IValidadorRsi CrearValidadorRsi(IRepositorioRsi repositorioRsi)
		{
			return new ValidadorRsi(repositorioRsi);
		}
	}
}
