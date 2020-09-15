using FrameworkNet.AccesoDatos;
using System;
using System.Collections.Generic;
namespace FrameworkNet.Rsi
{
	public interface IRepositorioRsi
	{
		Aplicacion ObtenerAplicacionPorNombre(string nombre);
		IList<Grupo> ObtenerGrupos(int userCode, int appCode);
		IList<Responsabilidad> ObtenerResponsabilidades(int userCode, int appCode);
		IList<Sitio> ObtenerSitios();
		Ad_Global ObtenerUsuarioGlobalPorADName(string adName);
		Usuario ObtenerUsuarioPorADName(string adName);
		Usuario ObtenerUsuarioPorUserCode(int userCode);
		IList<Usuario> ObtenerUsuarios(IList<ICriterioBusqueda> listaCriterios);
		Aplicacion ObtenerAplicacionPorKey(string nombre);
	}
}
