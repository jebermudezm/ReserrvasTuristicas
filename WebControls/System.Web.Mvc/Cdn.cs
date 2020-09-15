using System;
using System.IO;
namespace System.Web.Mvc
{
	public static class Cdn
	{
		public static HtmlString Image(string PathFileName, bool GetExtension)
		{
			return new HtmlString(Cdn.SetImagePath(PathFileName, null, GetExtension));
		}
		public static HtmlString Image(string Path, string FileName)
		{
			return new HtmlString(Cdn.SetImagePath(Path, FileName, false));
		}
		public static HtmlString Image(string Path, string FileName, bool GetExtension)
		{
			return new HtmlString(Cdn.SetImagePath(Path, FileName, GetExtension));
		}
		private static string SetImagePath(string Path, string File, bool GetExtension = false)
		{
            //string arg_A2_0 = "http://desa-cdn.andes.aes/img/";
            //if (Path.StartsWith("/"))
            //{
            //	Path = Path.Substring(1);
            //}
            //if (File == null)
            //{
            //	File = "/" + Path.GetFileName(Path);
            //	Path = Path.Replace(File, "");
            //}
            //if (!File.StartsWith("/"))
            //{
            //	File = "/" + File.Substring(1);
            //}
            //if (GetExtension)
            //{
            //	File = "/" + Path.GetExtension(File).Replace(".", "") + ".png";
            //}
            //return arg_A2_0 + Convert.ToString(Path + File).Replace("//", "/");
            return "";
		}
	}
}
