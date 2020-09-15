using FrameworkNet.AdministracionConexiones;
using System;
namespace FrameworkNet.Ambientes
{
	public class AmbienteFactory : IAmbienteFactory
	{
		private string pais;
		private string sufijoPais;
		private string tipo;
		private string sufijoAmbiente;
		public IAmbiente CrearAmbiente(string url)
		{
			this.obtenerAmbiente(url);
			this.obtenerPais(url);
			return new Ambiente(this.tipo, this.sufijoAmbiente, this.pais, this.sufijoPais);
		}
		private void obtenerPais(string url)
		{
			if (url.Contains("argentina.andes.aes"))
			{
				this.pais = "ARGENTINA";
				this.sufijoPais = string.Empty;
				return;
			}
			if (url.Contains("gener.andes.aes"))
			{
				this.pais = "CHILE";
				this.sufijoPais = "_CL";
				return;
			}
			if (url.Contains("chivor.andes.aes"))
			{
				this.pais = "COLOMBIA";
				this.sufijoPais = "_CO";
				return;
			}
			if (url.Contains("azurewebsites.net"))
			{
				this.pais = "SBU";
				this.sufijoPais = string.Empty;
				return;
			}
			if (url.Contains("aesandes.com"))
			{
				this.pais = "SBU";
				this.sufijoPais = string.Empty;
				return;
			}
			if (url.Contains("aesgener.cl"))
			{
				this.pais = "SBU";
				this.sufijoPais = string.Empty;
				return;
			}
			this.pais = "ARGENTINA";
			this.sufijoPais = "";
		}
		private void obtenerAmbiente(string url)
		{
			if (url.Contains("desa.") || url.Contains("desa-") || url.Contains("localhost") || url.Contains("dev"))
			{
				this.tipo = "DESARROLLO";
				this.sufijoAmbiente = "_DS";
				return;
			}
			if (url.Contains("test.") || url.Contains("test-"))
			{
				this.tipo = "TESTING";
				this.sufijoAmbiente = "_TS";
				return;
			}
			this.tipo = "PRODUCCION";
			this.sufijoAmbiente = "";
		}
	}
}
