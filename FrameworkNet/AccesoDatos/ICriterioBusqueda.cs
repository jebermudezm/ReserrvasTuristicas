using System;
namespace FrameworkNet.AccesoDatos
{
	public interface ICriterioBusqueda
	{
		OperadorCriterio Operador
		{
			get;
		}
		string Propiedad
		{
			get;
		}
		object Valor
		{
			get;
		}
	}
}
