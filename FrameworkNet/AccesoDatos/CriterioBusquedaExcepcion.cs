using System;
using System.Runtime.Serialization;
namespace FrameworkNet.AccesoDatos
{
	[Serializable]
	public class CriterioBusquedaExcepcion : FrameworkNetExcepcion
	{
		public CriterioBusquedaExcepcion()
		{
		}
		public CriterioBusquedaExcepcion(string message) : base(message)
		{
		}
		public CriterioBusquedaExcepcion(string message, int codigoError) : base(message, codigoError)
		{
		}
		public CriterioBusquedaExcepcion(string message, Exception inner, int codigoError) : base(message, inner, codigoError)
		{
		}
		public CriterioBusquedaExcepcion(string message, Exception inner) : base(message, inner)
		{
		}
		protected CriterioBusquedaExcepcion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
