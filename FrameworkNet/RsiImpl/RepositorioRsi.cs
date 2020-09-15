using FrameworkNet.AccesoDatos;
using FrameworkNet.Rsi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace FrameworkNet.RsiImpl
{
	public class RepositorioRsi : IRepositorioRsi
	{
		private readonly SqlConnection conexion;
		internal RepositorioRsi(string cadenaConexion)
		{
			this.conexion = new SqlConnection(cadenaConexion);
		}
		public Aplicacion ObtenerAplicacionPorKey(string nombre)
		{
			if (string.IsNullOrEmpty(nombre))
			{
				throw new RepositorioRsiExcepcion("La Key no puede ser un valor nulo, ni una cadena vacía.");
			}
			string sqlQuery = string.Format("SELECT * FROM aplicacion WHERE aplic_key = '{0}'", nombre);
			this.conexion.Open();
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, null);
			Aplicacion arg_45_0 = this.extraerAplicacion(dataReader);
			this.conexion.Close();
			if (arg_45_0 == null)
			{
				throw new RepositorioRsiExcepcion("No existe aplicación con esa dirección.");
			}
			return arg_45_0;
		}
		public Aplicacion ObtenerAplicacionPorNombre(string nombre)
		{
			if (string.IsNullOrEmpty(nombre))
			{
				throw new RepositorioRsiExcepcion("El nombre no puede ser un valor nulo, ni una cadena vacía.");
			}
			string sqlQuery = string.Format("SELECT * FROM aplicacion WHERE aplic_dire = '{0}' OR aplic_dire = '{0}/'", nombre);
			this.conexion.Open();
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, null);
			Aplicacion arg_45_0 = this.extraerAplicacion(dataReader);
			this.conexion.Close();
			if (arg_45_0 == null)
			{
				throw new RepositorioRsiExcepcion("No existe aplicación con esa dirección.");
			}
			return arg_45_0;
		}
		private SqlDataReader obtenerDatos(string sqlQuery, IList<ICriterioBusqueda> listaCriterios)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(sqlQuery);
			if (listaCriterios != null && listaCriterios.Count<ICriterioBusqueda>() > 0)
			{
				int num = 0;
				foreach (ICriterioBusqueda current in listaCriterios)
				{
					stringBuilder.Append((num++ == 0) ? " WHERE " : " AND ");
					stringBuilder.Append(this.sentenciaCriterio(current));
				}
			}
			SqlCommand sqlCommand = new SqlCommand(stringBuilder.ToString(), this.conexion);
			if (listaCriterios != null && listaCriterios.Count<ICriterioBusqueda>() > 0)
			{
				foreach (ICriterioBusqueda current2 in listaCriterios)
				{
					if (current2 != null && current2.Operador != OperadorCriterio.Contiene)
					{
						sqlCommand.Parameters.Add(new SqlParameter("@" + current2.Propiedad, current2.Valor));
					}
				}
			}
			return sqlCommand.ExecuteReader();
		}
		private string sentenciaCriterio(ICriterioBusqueda criterio)
		{
			switch (criterio.Operador)
			{
			case OperadorCriterio.Igual:
				return string.Format("{0} = @{1}", criterio.Propiedad, criterio.Propiedad);
			case OperadorCriterio.Contiene:
				return string.Format("{0} LIKE '%{1}%'", criterio.Propiedad, criterio.Valor.ToString());
			case OperadorCriterio.In:
				return string.Format("{0} IN ({1})", criterio.Propiedad, criterio.Valor.ToString());
			default:
				throw new InvalidOperationException("No hay operador criterio valido");
			}
		}
		private Aplicacion extraerAplicacion(SqlDataReader dataReader)
		{
			Aplicacion aplicacion = null;
			string idDuenios = "";
			while (dataReader.Read())
			{
				aplicacion = new Aplicacion
				{
					Codigo = Convert.ToInt32(dataReader["aplic_code"]),
					Nombre = dataReader["aplic_name"].ToString(),
					Version = dataReader["aplic_lastversion"].ToString(),
					Descripcion = dataReader["aplic_desc"].ToString(),
					Habilitada = dataReader["aplic_habi"].ToString() == "1"
				};
				idDuenios = dataReader["aplic_dueno"].ToString();
			}
			dataReader.Close();
			if (aplicacion != null)
			{
				aplicacion.Duenios = this.agregarDuenios(idDuenios);
			}
			return aplicacion;
		}
		private IList<Usuario> agregarDuenios(string idDuenios)
		{
			new List<Usuario>();
			if (string.IsNullOrEmpty(idDuenios))
			{
				return new List<Usuario>();
			}
			string[] arg_2F_0 = idDuenios.Split(new string[]
			{
				","
			}, StringSplitOptions.RemoveEmptyEntries);
			string text = "";
			string[] array = arg_2F_0;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				text += string.Format("'{0}',", text2.Trim());
			}
			return this.ObtenerUsuarios(new List<ICriterioBusqueda>
			{
				new CriterioBusqueda("user_code", text.Substring(0, text.Length - 1), OperadorCriterio.In)
			});
		}
		public IList<Usuario> ObtenerUsuarios(IList<ICriterioBusqueda> listaCriterios)
		{
			string sqlQuery = string.Format("select * from v_usuario_sbu", new object[0]);
			ConnectionState state = this.conexion.State;
			if (state == ConnectionState.Closed)
			{
				this.conexion.Open();
			}
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, listaCriterios);
			IList<Usuario> arg_49_0 = this.extraerUsuarios(dataReader);
			if (state == ConnectionState.Closed)
			{
				this.conexion.Close();
			}
			return arg_49_0;
		}
		private IList<Usuario> extraerUsuarios(SqlDataReader dataReader)
		{
			IList<Usuario> list = new List<Usuario>();
			while (dataReader.Read())
			{
				list.Add(this.crearUsuario(dataReader));
			}
			return list;
		}
		private Usuario crearUsuario(SqlDataReader dataReader)
		{
			Usuario usuario = new Usuario();
			usuario.user_code = Convert.ToInt32(dataReader["user_code"]);
			usuario.sistema_code = Convert.ToInt32(dataReader["sistema_code"]);
			usuario.usuario_code = Convert.ToInt32(dataReader["usuario_code"]);
			usuario.usuario_nombre = dataReader["usuario_nombre"].ToString();
			usuario.usuario_ad_name = dataReader["usuario_ad_name"].ToString();
			usuario.leg_tipodoc = Convert.ToInt32(dataReader["leg_tipodoc"]);
			usuario.leg_numdoc = dataReader["leg_numdoc"].ToString();
			if (!Convert.IsDBNull(dataReader["leg_fecnac"]))
			{
				usuario.leg_fecnac = Convert.ToDateTime(dataReader["leg_fecnac"]);
			}
			if (!Convert.IsDBNull(dataReader["leg_sexo"]))
			{
				usuario.leg_sexo = Convert.ToInt16(dataReader["leg_sexo"]);
			}
			usuario.user_habi = (dataReader["user_habi"].ToString() == "1");
			usuario.usuario_mail = dataReader["usuario_mail"].ToString();
			usuario.usuario_empresa = dataReader["usuario_empresa"].ToString();
			usuario.usuario_cargo = dataReader["usuario_cargo"].ToString();
			usuario.usuario_car_code = dataReader["usuario_car_code"].ToString();
			usuario.usuario_car_depende = dataReader["usuario_car_depende"].ToString();
			usuario.usuario_sector = dataReader["usuario_sector"].ToString();
			usuario.OS_ART = dataReader["OS_ART"].ToString();
			usuario.usuario_lpg = dataReader["usuario_lpg"].ToString();
			usuario.usuario_tel = dataReader["usuario_tel"].ToString();
			usuario.usuario_dir = dataReader["usuario_dir"].ToString();
			usuario.usuario_piso = dataReader["usuario_piso"].ToString();
			usuario.usuario_depto = dataReader["usuario_depto"].ToString();
			usuario.usuario_cp = dataReader["usuario_cp"].ToString();
			usuario.usuario_localidad = dataReader["usuario_localidad"].ToString();
			usuario.usuario_lpg_descrip = dataReader["usuario_lpg_descrip"].ToString();
			usuario.usuario_lpg_disc = dataReader["usuario_lpg_disc"].ToString();
			usuario.mpr_codigo = dataReader["mpr_codigo"].ToString();
			usuario.usuario_nom = dataReader["usuario_nom"].ToString();
			usuario.usuario_ape = dataReader["usuario_ape"].ToString();
			usuario.sindicato = dataReader["sindicato"].ToString();
			usuario.ccosto = dataReader["ccosto"].ToString();
			usuario.jornada = dataReader["jornada"].ToString();
			usuario.tel_fijo = dataReader["tel_fijo"].ToString();
			usuario.tel_interno = dataReader["tel_interno"].ToString();
			usuario.tel_celular = dataReader["tel_celular"].ToString();
			usuario.base_bej = dataReader["base_bej"].ToString();
			if (!Convert.IsDBNull(dataReader["usuario_cacc"]))
			{
				usuario.usuario_cacc = Convert.ToBoolean(dataReader["usuario_cacc"]);
			}
			if (!Convert.IsDBNull(dataReader["legajo"]))
			{
				usuario.legajo = Convert.ToInt32(dataReader["legajo"]);
			}
			usuario.sin_codigo = dataReader["sin_codigo"].ToString();
			if (!Convert.IsDBNull(dataReader["leg_fecingr"]))
			{
				usuario.leg_fecingr = Convert.ToDateTime(dataReader["leg_fecingr"]);
			}
			if (!Convert.IsDBNull(dataReader["bui_jefe_code"]))
			{
				usuario.bui_jefe_code = Convert.ToInt32(dataReader["bui_jefe_code"]);
			}
			try
			{
				usuario.NombreJefe = dataReader["nombre_jefe"].ToString();
			}
			catch (IndexOutOfRangeException arg_421_0)
			{
				string arg_426_0 = arg_421_0.Message;
				usuario.NombreJefe = string.Empty;
			}
			return usuario;
		}
		public Usuario ObtenerUsuarioPorUserCode(int userCode)
		{
			if (userCode < 0)
			{
				throw new RepositorioRsiExcepcion("El user code no puede ser negativo");
			}
			string sqlQuery = string.Format("select top 1 a.*, b.usuario_nombre as nombre_jefe from v_usuario_sbu a left join v_usuario_sbu b on a.bui_jefe_code = b.user_code where a.user_code = {0} order by user_habi desc", userCode);
			this.conexion.Open();
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, null);
			Usuario arg_46_0 = this.extraerUsuario(dataReader);
			this.conexion.Close();
			if (arg_46_0 == null)
			{
				throw new RepositorioRsiExcepcion("No existe usuario con esa cuenta de Active Directory.");
			}
			return arg_46_0;
		}
		public Usuario ObtenerUsuarioPorADName(string adName)
		{
			return this.obtenerUsuarioDesdeBUI(adName);
		}
		private Usuario obtenerUsuarioDesdeBUI(string adName)
		{
			if (string.IsNullOrEmpty(adName))
			{
				throw new RepositorioRsiExcepcion("No existe usuario con esa cuenta de Active Directory.");
			}
			string sqlQuery = string.Format("select top 1 a.*, b.usuario_nombre as nombre_jefe from v_usuario_sbu a left join v_usuario_sbu b on a.bui_jefe_code = b.user_code where a.usuario_ad_name like '{0}' order by user_habi desc", adName);
			this.conexion.Open();
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, null);
			Usuario arg_45_0 = this.extraerUsuario(dataReader);
			this.conexion.Close();
			if (arg_45_0 == null)
			{
				throw new RepositorioRsiExcepcion("No existe usuario con esa cuenta de Active Directory.");
			}
			return arg_45_0;
		}
		private Usuario extraerUsuario(SqlDataReader dataReader)
		{
			Usuario result = null;
			while (dataReader.Read())
			{
				result = this.crearUsuario(dataReader);
			}
			return result;
		}
		public IList<Responsabilidad> ObtenerResponsabilidades(int userCode, int appCode)
		{
			string sqlQuery = string.Format("SELECT DISTINCT arg.RESP_CODE,  responsabilidad.RESP_NAME ,responsabilidad.RESP_DESC FROM aplic_resp_grupo as arg INNER JOIN grupo_usuario as gu ON arg.GROUP_CODE = gu.GROUP_CODE INNER JOIN responsabilidad on responsabilidad.RESP_CODE = arg.RESP_CODE WHERE arg.APLIC_CODE = {0} AND gu.USER_CODE = {1} ORDER BY arg.RESP_CODE", appCode, userCode);
			this.conexion.Open();
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, null);
			IList<Responsabilidad> arg_3D_0 = this.extraerResponsabilidades(dataReader);
			this.conexion.Close();
			return arg_3D_0;
		}
		private IList<Responsabilidad> extraerResponsabilidades(SqlDataReader dataReader)
		{
			IList<Responsabilidad> list = new List<Responsabilidad>();
			while (dataReader.Read())
			{
				list.Add(new Responsabilidad
				{
					Resp_Code = Convert.ToInt32(dataReader["resp_code"]),
					Resp_Name = dataReader["resp_name"].ToString(),
					Resp_Desc = dataReader["resp_desc"].ToString()
				});
			}
			return list;
		}
		public IList<Grupo> ObtenerGrupos(int userCode, int appCode)
		{
			new List<Grupo>();
			string sqlQuery = string.Format("SELECT DISTINCT gr.GROUP_CODE, gr.GROUP_NAME, gr.GROUP_DESC FROM grupo as gr INNER JOIN grupo_usuario as gu ON gr.GROUP_CODE = gu.GROUP_CODE INNER JOIN aplic_resp_grupo as arg on arg.GROUP_CODE = gr.GROUP_CODE WHERE arg.APLIC_CODE = {0} AND gu.USER_CODE = {1} ORDER BY gr.GROUP_CODE", appCode, userCode);
			this.conexion.Open();
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, null);
			IList<Grupo> arg_43_0 = this.extraerGrupos(dataReader);
			this.conexion.Close();
			return arg_43_0;
		}
		private IList<Grupo> extraerGrupos(SqlDataReader dataReader)
		{
			IList<Grupo> list = new List<Grupo>();
			while (dataReader.Read())
			{
				list.Add(new Grupo
				{
					Group_Code = Convert.ToInt32(dataReader["group_code"]),
					Group_Name = dataReader["group_name"].ToString(),
					Group_Desc = dataReader["group_desc"].ToString()
				});
			}
			return list;
		}
		public IList<Sitio> ObtenerSitios()
		{
			string sqlQuery = "SELECT * from v_sitios";
			this.conexion.Open();
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, null);
			IList<Sitio> arg_2C_0 = this.extraerSitios(dataReader);
			this.conexion.Close();
			if (arg_2C_0.Count == 0)
			{
				throw new RepositorioRsiExcepcion("No se han recuperado sitios...");
			}
			return arg_2C_0;
		}
		private IList<Sitio> extraerSitios(SqlDataReader dataReader)
		{
			IList<Sitio> list = new List<Sitio>();
			while (dataReader.Read())
			{
				list.Add(new Sitio
				{
					NombreSitio = dataReader["sitio"].ToString(),
					CampoOrigen = dataReader["campo_origen"].ToString(),
					ValorOrigen = dataReader["valor_origen"].ToString()
				});
			}
			return list;
		}
		public Ad_Global ObtenerUsuarioGlobalPorADName(string adName)
		{
			if (string.IsNullOrEmpty(adName))
			{
				throw new RepositorioRsiExcepcion("No existe usuario con esa cuenta de Active Directory.");
			}
			string sqlQuery = string.Format("select * from ad_global where ad_cuenta like '{0}'", adName);
			this.conexion.Open();
			SqlDataReader dataReader = this.obtenerDatos(sqlQuery, null);
			Ad_Global arg_45_0 = this.extraerUsuarioGlobal(dataReader);
			this.conexion.Close();
			if (arg_45_0 == null)
			{
				throw new RepositorioRsiExcepcion("No existe usuario con esa cuenta de Active Directory.");
			}
			return arg_45_0;
		}
		private Ad_Global extraerUsuarioGlobal(SqlDataReader dataReader)
		{
			Ad_Global result = null;
			while (dataReader.Read())
			{
				result = new Ad_Global
				{
					ad_code = Convert.ToInt32(dataReader["ad_code"]),
					ad_nombre = dataReader["ad_nombre"].ToString(),
					ad_cuenta = dataReader["ad_cuenta"].ToString(),
					ad_path = dataReader["ad_path"].ToString(),
					ad_ultimo_cambio = dataReader["ad_ultimo_cambio"].ToString(),
					ad_pass_expira = dataReader["ad_pass_expira"].ToString(),
					ad_numdia = dataReader["ad_numdia"].ToString(),
					ad_falta = dataReader["ad_falta"].ToString(),
					ad_dominio = dataReader["ad_dominio"].ToString(),
					ad_fec_modi = dataReader["ad_fec_modi"].ToString(),
					ad_grupo = dataReader["ad_grupo"].ToString(),
					ad_bloqueado = new bool?(Convert.ToBoolean(dataReader["ad_bloqueado"])),
					ad_habilitado = new bool?(Convert.ToBoolean(dataReader["ad_habilitado"])),
					ad_ulogin = dataReader["ad_ulogin"].ToString(),
					ad_cuenta_expira = dataReader["ad_cuenta_expira"].ToString(),
					ad_vencimiento = dataReader["ad_vencimiento"].ToString(),
					ad_mail = dataReader["ad_mail"].ToString(),
					ad_dni = dataReader["ad_dni"].ToString(),
					ad_sid = dataReader["ad_sid"].ToString(),
					ad_physicalDeliveryOfficeName = dataReader["ad_physicalDeliveryOfficeName"].ToString(),
					ad_homeDrive = dataReader["ad_homeDrive"].ToString(),
					ad_manager = dataReader["ad_manager"].ToString(),
					ad_company = dataReader["ad_company"].ToString(),
					ad_Department = dataReader["ad_Department"].ToString(),
					ad_st = dataReader["ad_st"].ToString(),
					ad_telephoneNumber = dataReader["ad_telephoneNumber"].ToString(),
					ad_mobile = dataReader["ad_mobile"].ToString(),
					ad_ipphone = dataReader["ad_ipphone"].ToString(),
					ad_wwwHomePage = dataReader["ad_wwwHomePage"].ToString(),
					ad_Title = dataReader["ad_Title"].ToString()
				};
			}
			return result;
		}
	}
}
