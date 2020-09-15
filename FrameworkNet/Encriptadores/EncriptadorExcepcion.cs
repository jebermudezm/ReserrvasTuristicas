using System;
using System.Runtime.Serialization;
namespace FrameworkNet.Encriptadores
{
	[Serializable]
	public class EncriptadorExcepcion : FrameworkNetExcepcion
	{
		public EncriptadorExcepcion()
		{
		}
		public EncriptadorExcepcion(string message) : base(message)
		{
		}
		public EncriptadorExcepcion(string message, int codigoError) : base(message, codigoError)
		{
		}
		public EncriptadorExcepcion(string message, Exception inner, int codigoError) : base(message, inner, codigoError)
		{
		}
		public EncriptadorExcepcion(string message, Exception inner) : base(message, inner)
		{
		}
		protected EncriptadorExcepcion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
