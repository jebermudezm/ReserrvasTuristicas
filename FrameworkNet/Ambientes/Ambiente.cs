using FrameworkNet.AdministracionConexiones;
using System;
namespace FrameworkNet.Ambientes
{
	public class Ambiente : IAmbiente
	{
		public string Tipo { get; private set; }
		public string SufijoAmbiente { get; private set; }
        public string Pais { get; private set; }
        public string SufijoPais { get; private set; }
        public Ambiente(string tipo, string sufijoAmbiente, string pais, string sufijoPais)
		{
			this.Tipo = tipo;
			this.SufijoAmbiente = sufijoAmbiente;
			this.Pais = pais;
			this.SufijoPais = sufijoPais;
		}
		public bool IsDev()
		{
			return this.Tipo == "DESARROLLO";
		}
		public bool IsProd()
		{
			return this.Tipo == "PRODUCCION";
		}
		public bool IsTest()
		{
			return this.Tipo == "TESTING";
		}
	}
}
