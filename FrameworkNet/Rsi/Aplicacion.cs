using System;
using System.Collections.Generic;
namespace FrameworkNet.Rsi
{
	public class Aplicacion
	{
		public int Codigo { get; set; }
		public string Nombre { get; set; }
        public string Descripcion { get; set;}
		public string Direccion { get; set; }
        public bool Habilitada { get; set; }
        public string Icono { get; set; }
        public int Priori { get; set; }
        public string Version { get; set; }
        public IList<Usuario> Duenios { get; set; }
    }
}
