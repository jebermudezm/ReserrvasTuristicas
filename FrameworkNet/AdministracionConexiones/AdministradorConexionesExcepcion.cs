using System;
using System.Runtime.Serialization;
namespace FrameworkNet.AdministracionConexiones
{
	[Serializable]
	public class AdministradorConexionesExcepcion : FrameworkNetExcepcion
	{
		public AdministradorConexionesExcepcion()
		{
		}
		public AdministradorConexionesExcepcion(string message) : base(message)
		{
		}
		public AdministradorConexionesExcepcion(string message, int codigoError) : base(message, codigoError)
		{
		}
		public AdministradorConexionesExcepcion(string message, Exception inner, int codigoError) : base(message, inner, codigoError)
		{
		}
		public AdministradorConexionesExcepcion(string message, Exception inner) : base(message, inner)
		{
		}
		protected AdministradorConexionesExcepcion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
