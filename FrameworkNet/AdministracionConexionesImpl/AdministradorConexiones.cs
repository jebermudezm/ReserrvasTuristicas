using FrameworkNet.AdministracionConexiones;
using System;
using System.Collections.Generic;
using System.Linq;
namespace FrameworkNet.AdministracionConexionesImpl
{
	public class AdministradorConexiones : IAdministradorConexiones
	{
		private IAmbiente ambiente;
		private IRepositorio repositorio;
		private IEncriptador encriptador;
		internal AdministradorConexiones(IAmbiente ambiente, IRepositorio repositorio, IEncriptador encriptador)
		{
			if (ambiente == null)
			{
				throw new AdministradorConexionesExcepcion("El argumento <Ambiente> no puede ser un valor nulo");
			}
			if (repositorio == null)
			{
				throw new AdministradorConexionesExcepcion("El argumento <Repositorio> no puede ser un valor nulo");
			}
			if (encriptador == null)
			{
				throw new AdministradorConexionesExcepcion("El argumento <Encriptador> no puede ser un valor nulo");
			}
			this.encriptador = encriptador;
			this.ambiente = ambiente;
			this.repositorio = repositorio;
		}
		public string ObtenerConexion(string nombreDB)
		{
			nombreDB = nombreDB + this.ambiente.SufijoAmbiente + this.ambiente.SufijoPais;
			nombreDB = nombreDB.ToUpper();
			if (string.IsNullOrEmpty(nombreDB))
			{
				throw new AdministradorConexionesExcepcion("El argumento <NombreDB> no puede ser un valor nulo, ni una cadena vacía");
			}
			string expr_81 = (
				from c in this.tryDesencriptar()
				where c.Key.ToUpper() == nombreDB
				select c).FirstOrDefault<KeyValuePair<string, string>>().Value;
			if (expr_81 == null)
			{
				throw new AdministradorConexionesExcepcion(string.Format("No existe conexion con el nombre <{0}>", nombreDB));
			}
			return expr_81;
		}
		private IDictionary<string, string> tryDesencriptar()
		{
			string cadenaEncriptada = this.repositorio.Open();
			IDictionary<string, string> result;
			try
			{
				result = this.encriptador.Desencriptar(cadenaEncriptada);
			}
			catch (FrameworkNetExcepcion frameworkNetExcepcion)
			{
				throw new AdministradorConexionesExcepcion(frameworkNetExcepcion.Message, frameworkNetExcepcion);
			}
			return result;
		}
		public IDictionary<string, string> ObtenerListaConexiones()
		{
			return this.tryDesencriptar();
		}
	}
}
