using System;
using System.Collections.Generic;
namespace FrameworkNet.Rsi
{
	public class Usuario
	{
		public int user_code { get; set; }
		public int sistema_code { get; set; }
        public int usuario_code { get; set; }
        public string usuario_nombre { get; set; }
        public string usuario_ad_name { get; set; }
        public int leg_tipodoc { get; set; }
        public string leg_numdoc { get; set; }
        public DateTime leg_fecnac { get; set; }
        public short leg_sexo { get; set; }
        public bool user_habi { get; set; }
        public string usuario_empresa { get; set; }
        public string usuario_cargo { get; set; }
        public string usuario_car_code { get; set; }
        public string usuario_car_depende { get; set; }
        public string usuario_mail { get; set; }
        public string usuario_sector { get; set; }
        public string OS_ART { get; set; }
        public string usuario_lpg { get; set; }
        public string usuario_tel { get; set; }
        public string usuario_dir { get; set; }
        public string usuario_piso { get; set; }
        public string usuario_depto { get; set; }
        public string usuario_cp { get; set; }
        public string usuario_localidad { get; set; }
        public string usuario_lpg_descrip { get; set; }
        public string usuario_lpg_disc { get; set; }
        public string mpr_codigo { get; set; }
        public string usuario_nom { get; set; }
        public string usuario_ape { get; set; }
        public string sindicato { get; set; }
        public string ccosto { get; set; }
        public string jornada { get; set; }
        public string tel_fijo { get; set; }
        public string tel_interno { get; set; }
        public string tel_celular { get; set; }
        public string base_bej { get; set; }
        public bool usuario_cacc { get; set; }
        public int legajo { get; set; }
        public string sin_codigo { get; set; }
        public DateTime leg_fecingr { get; set; }
        public int bui_jefe_code { get; set; }
        public IList<Responsabilidad> Responsabilidades { get; set; }
        public IList<Grupo> Grupos { get; set; }
        public string NombreJefe { get; set; }
        public Usuario()
		{
			this.Responsabilidades = new List<Responsabilidad>();
			this.Grupos = new List<Grupo>();
		}
	}
}
