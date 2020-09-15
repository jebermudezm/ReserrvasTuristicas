using System;
using System.IO;
using System.Web;
namespace WebControls.Files
{
	public static class Archiver
	{
		public static Filer File(string FilePath)
		{
			return new Filer(Archiver.createFile(FilePath), Path.GetFileName(FilePath))
			{
				myFullPath = FilePath
			};
		}
		public static Filer File(string Path, string Name)
		{
			if (!Path.EndsWith("\\"))
			{
				Path += "\\";
			}
			return new Filer(Archiver.createFile(Path + Name), Name)
			{
				myFullPath = Path + Name
			};
		}
		private static Stream createFile(string FilePath)
		{
			if (System.IO.File.Exists(FilePath))
			{
				return new FileStream(FilePath, FileMode.Open, FileAccess.Read);
			}
			throw new FileNotFoundException("El archivo no existe en le directorio \"" + FilePath + "\"");
		}
		public static Filer File(Stream file, string fileName)
		{
			if (file != null)
			{
				return new Filer(file, fileName);
			}
			throw new FileNotFoundException("No se pudo encontrar el archivo");
		}
		public static Filer File(FileStream file)
		{
			Stream stream = null;
			if (file != null)
			{
				file.CopyTo(stream);
				return new Filer(stream, file.Name);
			}
			throw new FileNotFoundException("No se pudo encontrar el archivo");
		}
		public static Filer File(HttpPostedFileBase file)
		{
			if (file != null)
			{
				return new Filer(file.InputStream, file.FileName);
			}
			throw new FileNotFoundException("No se pudo encontrar el archivo");
		}
		public static SPointer SPFile(string FilePath)
		{
			if (System.IO.File.Exists(FilePath))
			{
				FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
				Stream stream = null;
				fileStream.CopyTo(stream);
				return new SPointer(stream, fileStream.Name);
			}
			if (FilePath.StartsWith("http://") || FilePath.StartsWith("https://"))
			{
				return new SPointer(FilePath);
			}
			throw new FileNotFoundException("El archivo no existe en le directorio \"" + FilePath + "\"");
		}
		public static SPointer SPFile(string WebUrlPath, string ListB, string FileName, string FolderName = null)
		{
			return new SPointer(WebUrlPath, ListB, FileName, FolderName);
		}
		public static SPointer SPFile(FileStream file)
		{
			Stream stream = null;
			if (file != null)
			{
				file.CopyTo(stream);
				return new SPointer(stream, file.Name);
			}
			throw new FileNotFoundException("No se pudo encontrar el archivo");
		}
		public static SPointer SPFile(HttpPostedFileBase file)
		{
			if (file != null)
			{
				return new SPointer(file.InputStream, file.FileName);
			}
			throw new FileNotFoundException("No se pudo encontrar el archivo");
		}
	}
}
