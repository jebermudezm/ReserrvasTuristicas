using System;
using System.Runtime.Serialization;
namespace FrameworkNet
{
	[Serializable]
	public abstract class FrameworkNetExcepcion : Exception
	{
		public int CodigoError
		{
			get;
			private set;
		}
		public FrameworkNetExcepcion()
		{
		}
		public FrameworkNetExcepcion(string message) : base(message)
		{
		}
		public FrameworkNetExcepcion(string message, int codigoError) : base(message)
		{
			this.CodigoError = codigoError;
		}
		public FrameworkNetExcepcion(string message, Exception inner) : base(message, inner)
		{
		}
		public FrameworkNetExcepcion(string message, Exception inner, int codigoError) : base(message, inner)
		{
			this.CodigoError = codigoError;
		}
		protected FrameworkNetExcepcion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
