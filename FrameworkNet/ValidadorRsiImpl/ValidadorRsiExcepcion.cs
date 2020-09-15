using System;
using System.Runtime.Serialization;
namespace FrameworkNet.ValidadorRsiImpl
{
	[Serializable]
	public class ValidadorRsiExcepcion : FrameworkNetExcepcion
	{
		public ValidadorRsiExcepcion()
		{
		}
		public ValidadorRsiExcepcion(string message) : base(message)
		{
		}
		public ValidadorRsiExcepcion(string message, int codigoError) : base(message, codigoError)
		{
		}
		public ValidadorRsiExcepcion(string message, Exception inner) : base(message, inner)
		{
		}
		public ValidadorRsiExcepcion(string message, Exception inner, int codigoError) : base(message, inner, codigoError)
		{
		}
		protected ValidadorRsiExcepcion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
