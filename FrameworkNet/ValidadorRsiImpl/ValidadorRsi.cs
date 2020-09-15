using FrameworkNet.Rsi;
using System;
namespace FrameworkNet.ValidadorRsiImpl
{
	public class ValidadorRsi : IValidadorRsi
	{
		private readonly IRepositorioRsi repositorioRsi;
		internal ValidadorRsi(IRepositorioRsi repositorioRsi)
		{
			this.repositorioRsi = repositorioRsi;
		}
		public Aplicacion ObtenerAplicacion(string url)
		{
			Aplicacion result;
			try
			{
				result = this.repositorioRsi.ObtenerAplicacionPorNombre(url);
			}
			catch (Exception ex)
			{
				throw new ValidadorRsiExcepcion(ex.Message, ex);
			}
			return result;
		}
		public Aplicacion ObtenerAplicacionPorKey(string aplickey)
		{
			Aplicacion result;
			try
			{
				result = this.repositorioRsi.ObtenerAplicacionPorKey(aplickey);
			}
			catch (Exception ex)
			{
				throw new ValidadorRsiExcepcion(ex.Message, ex);
			}
			return result;
		}
		public Usuario ObtenerUsuario(string adName, int appCode)
		{
			Usuario usuario;
			try
			{
				usuario = this.repositorioRsi.ObtenerUsuarioPorADName(adName);
			}
			catch (Exception ex)
			{
				throw new ValidadorRsiExcepcion(ex.Message, ex, 6);
			}
			if (!usuario.user_habi)
			{
				throw new ValidadorRsiExcepcion("El usuario esta deshabilitado.", 4);
			}
			try
			{
				usuario.Responsabilidades = this.repositorioRsi.ObtenerResponsabilidades(usuario.user_code, appCode);
				usuario.Grupos = this.repositorioRsi.ObtenerGrupos(usuario.user_code, appCode);
			}
			catch (Exception ex2)
			{
				throw new ValidadorRsiExcepcion(ex2.Message, ex2, 6);
			}
			return usuario;
		}
	}
}
