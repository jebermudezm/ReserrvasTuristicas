using System;
namespace FrameworkNet.AccesoDatos
{
	public class CriterioBusqueda : ICriterioBusqueda
	{
		private readonly string propiedad;
		private readonly object valor;
		private readonly OperadorCriterio operador;
		public string Propiedad
		{
			get
			{
				return this.propiedad;
			}
		}
		public object Valor
		{
			get
			{
				return this.valor;
			}
		}
		public OperadorCriterio Operador
		{
			get
			{
				return this.operador;
			}
		}
		public CriterioBusqueda(string propiedad, object valor, OperadorCriterio operador)
		{
			if (string.IsNullOrEmpty(propiedad) || string.IsNullOrWhiteSpace(propiedad))
			{
				throw new CriterioBusquedaExcepcion("El argument <propiedad> no puede ser un valor nulo ni una cadena vacía.");
			}
			if (valor == null)
			{
				throw new CriterioBusquedaExcepcion("El argument <valor> no puede ser nulo.");
			}
			this.propiedad = propiedad;
			this.valor = valor;
			this.operador = operador;
		}
	}
}
