using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
namespace WebControls.Files
{
	public class fFiler
	{
		private Stream myFile;
		private string myName;
		public byte[] GetBytes
		{
			get
			{
				byte[] array = new byte[this.myFile.Length];
				this.myFile.Read(array, 0, (int)this.myFile.Length);
				return array;
			}
		}
		public FileStream GetFile
		{
			get
			{
				return this.myFile as FileStream;
			}
		}
		public string Read
		{
			get
			{
				return new StreamReader(this.myFile, Encoding.UTF8).ReadToEnd();
			}
		}
		public fFiler()
		{
		}
		public fFiler(string FilePath)
		{
			if (System.IO.File.Exists(FilePath))
			{
				this.myFile = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
				this.myName = (this.myFile as FileStream).Name;
				return;
			}
			throw new FileNotFoundException("El archivo no existe en le directorio \"" + FilePath + "\"");
		}
		public fFiler(FileStream file)
		{
			if (file != null)
			{
				this.myFile = file;
				this.myName = file.Name;
			}
		}
		public fFiler(HttpPostedFileBase file)
		{
			if (file != null)
			{
				this.myFile = file.InputStream;
				this.myName = Path.GetFileName(file.FileName);
			}
		}
		public bool Save(string FilePath)
		{
			bool result = false;
			if (Directory.Exists(FilePath))
			{
				using (FileStream fileStream = new FileStream(Path.Combine(FilePath, this.myName), FileMode.Create, FileAccess.Write))
				{
					byte[] array = new byte[this.myFile.Length];
					this.myFile.Read(array, 0, (int)this.myFile.Length);
					fileStream.Write(array, 0, array.Length);
					this.myFile.Close();
					result = true;
					return result;
				}
			}
			throw new DirectoryNotFoundException("El directorio \"" + FilePath + "\" no existe");
		}
		public bool UpSP(string WebUrlPath, string ListB)
		{
			bool result = false;
			//using (ClientContext clientContext = new ClientContext(WebUrlPath))
			//{
			//	FileInfo fileInfo = new FileInfo(this.myName);
			//	List byTitle = clientContext.get_Web().get_Lists().GetByTitle(ListB);
			//	clientContext.Load<Folder>(byTitle.get_RootFolder(), new Expression<Func<Folder, object>>[0]);
			//	try
			//	{
			//		clientContext.ExecuteQuery();
			//		File.SaveBinaryDirect(clientContext, string.Format("{0}/{1}", byTitle.get_RootFolder().get_ServerRelativeUrl(), fileInfo.Name), this.myFile, true);
			//		result = true;
			//	}
			//	catch (Exception)
			//	{
			//		result = false;
			//	}
			//}
			return result;
		}
		public bool UpSP(string WebUrlPath, string ListB, string FolderName)
		{
			bool result = false;
			//using (ClientContext clientContext = new ClientContext(WebUrlPath))
			//{
			//	FileInfo fileInfo = new FileInfo(this.myName);
			//	List byTitle = clientContext.get_Web().get_Lists().GetByTitle(ListB);
			//	FolderCollection folders = byTitle.get_RootFolder().get_Folders();
			//	clientContext.Load<FolderCollection>(folders, new Expression<Func<FolderCollection, object>>[0]);
			//	clientContext.ExecuteQuery();
			//	Folder folder = byTitle.get_RootFolder();
			//	folder = (
			//		from f in folders
			//		where f.Name == FolderName
			//		select f).FirstOrDefault<Folder>();
			//	clientContext.Load<Folder>(folder, new Expression<Func<Folder, object>>[0]);
			//	try
			//	{
			//		clientContext.ExecuteQuery();
			//		File.SaveBinaryDirect(clientContext, string.Format("{0}/{1}", folder.get_ServerRelativeUrl(), fileInfo.Name), this.myFile, true);
			//		result = true;
			//	}
			//	catch (Exception)
			//	{
			//		result = false;
			//	}
			//}
			return result;
		}
		public bool UpSP(string WebUrlPath, string ListB, Dictionary<string, object> Fields = null, string FolderName = null)
		{
			bool result = false;
			//using (ClientContext clientContext = new ClientContext(WebUrlPath))
			//{
			//	FileInfo fi = new FileInfo(this.myName);
			//	List byTitle = clientContext.get_Web().get_Lists().GetByTitle(ListB);
			//	FolderCollection folders = byTitle.get_RootFolder().get_Folders();
			//	clientContext.Load<FolderCollection>(folders, new Expression<Func<FolderCollection, object>>[0]);
			//	clientContext.ExecuteQuery();
			//	Folder folder = (
			//		from f in folders
			//		where f.Name == FolderName
			//		select f).FirstOrDefault<Folder>();
			//	if (folder == null)
			//	{
			//		folder = byTitle.get_RootFolder();
			//	}
			//	clientContext.Load<Folder>(folder, new Expression<Func<Folder, object>>[0]);
			//	try
			//	{
			//		clientContext.ExecuteQuery();
			//		File.SaveBinaryDirect(clientContext, string.Format("{0}/{1}", folder.get_ServerRelativeUrl(), fi.Name), this.myFile, true);
			//		FileCollection files = folder.get_Files();
			//		clientContext.Load<FileCollection>(files, new Expression<Func<FileCollection, object>>[0]);
			//		clientContext.ExecuteQuery();
			//		File file = (
			//			from f in files
			//			where f.Name == fi.Name
			//			select f).FirstOrDefault<File>();
			//		if (Fields != null)
			//		{
			//			foreach (KeyValuePair<string, object> current in Fields)
			//			{
			//				file.get_ListItemAllFields().set_Item(current.Key, current.Value);
			//			}
			//			file.get_ListItemAllFields().Update();
			//		}
			//		clientContext.Load<File>(file, new Expression<Func<File, object>>[0]);
			//		clientContext.ExecuteQuery();
			//		result = true;
			//	}
			//	catch (Exception)
			//	{
			//		result = false;
			//	}
			//}
			return result;
		}
	}
}
