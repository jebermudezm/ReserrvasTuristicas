using System;
using System.Runtime.Serialization;
namespace FrameworkNet.RsiImpl
{
	[Serializable]
	public class RepositorioRsiExcepcion : FrameworkNetExcepcion
	{
		public RepositorioRsiExcepcion()
		{
		}
		public RepositorioRsiExcepcion(string message) : base(message)
		{
		}
		public RepositorioRsiExcepcion(string message, int codigoError) : base(message, codigoError)
		{
		}
		public RepositorioRsiExcepcion(string message, Exception inner, int codigoError) : base(message, inner, codigoError)
		{
		}
		public RepositorioRsiExcepcion(string message, Exception inner) : base(message, inner)
		{
		}
		protected RepositorioRsiExcepcion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
