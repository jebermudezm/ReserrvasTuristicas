using System;
namespace FrameworkNet.Rsi
{
	public interface IValidadorRsi
	{
		Aplicacion ObtenerAplicacion(string url);
		Aplicacion ObtenerAplicacionPorKey(string aplicname);
		Usuario ObtenerUsuario(string adName, int appCode);
	}
}
