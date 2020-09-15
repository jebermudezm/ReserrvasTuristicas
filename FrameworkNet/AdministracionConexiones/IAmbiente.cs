using System;
namespace FrameworkNet.AdministracionConexiones
{
	public interface IAmbiente
	{
		string Pais
		{
			get;
		}
		string SufijoAmbiente
		{
			get;
		}
		string SufijoPais
		{
			get;
		}
		string Tipo
		{
			get;
		}
		bool IsProd();
		bool IsTest();
		bool IsDev();
	}
}
