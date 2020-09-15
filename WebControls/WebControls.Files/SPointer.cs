using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
namespace WebControls.Files
{
	public class SPointer : Filer
	{
		public SPointer(Stream file, string name) : base(file, name)
		{
		}
		public SPointer(string urlFile)
		{
			this.SetFile(new Uri(urlFile));
		}
		public SPointer(string WebUrlPath, string ListB, string FileName, string FolderName = null)
		{
			//using (ClientContext clientContext = new ClientContext(WebUrlPath))
			//{
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
			//	try
			//	{
			//		FileCollection files = folder.get_Files();
			//		clientContext.Load<FileCollection>(files, new Expression<Func<FileCollection, object>>[0]);
			//		clientContext.ExecuteQuery();
			//		File file = (
			//			from f in files
			//			where f.Name == FileName
			//			select f).FirstOrDefault<File>();
			//		string uriString = WebUrlPath + file.get_ServerRelativeUrl();
			//		this.SetFile(new Uri(uriString));
			//	}
			//	catch (Exception ex)
			//	{
			//		throw new Exception(ex.Message, ex);
			//	}
			//}
		}
		private void SetFile(Uri urlFile)
		{
			byte[] buffer = new WebClient
			{
				Credentials = CredentialCache.DefaultCredentials
			}.DownloadData(urlFile);
			this.myFile = new MemoryStream(buffer);
			this.myFileName = urlFile.Segments[urlFile.Segments.Length - 1];
			int num = this.myFileName.LastIndexOf('.');
			this.myName = this.myFileName.Substring(0, num);
			this.myExtension = this.myFileName.Substring(num + 1);
		}
		public bool Save(string WebUrlPath, string ListB)
		{
			return this.UpLoad(WebUrlPath, ListB, null, null);
		}
		public bool Save(string WebUrlPath, string ListB, Dictionary<string, object> Fields)
		{
			return this.UpLoad(WebUrlPath, ListB, Fields, null);
		}
		public bool Save(string WebUrlPath, string ListB, string FolderName)
		{
			return this.UpLoad(WebUrlPath, ListB, null, FolderName);
		}
		public bool Save(string WebUrlPath, string ListB, string FolderName, Dictionary<string, object> Fields)
		{
			return this.UpLoad(WebUrlPath, ListB, Fields, FolderName);
		}
		private bool UpLoad(string WebUrlPath, string ListB, Dictionary<string, object> Fields = null, string FolderName = null)
		{
			bool result = false;
			//using (ClientContext clientContext = new ClientContext(WebUrlPath))
			//{
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
			//		File.SaveBinaryDirect(clientContext, string.Format("{0}/{1}", folder.get_ServerRelativeUrl(), base.FileName), base.GetStream, true);
			//		FileCollection files = folder.get_Files();
			//		clientContext.Load<FileCollection>(files, new Expression<Func<FileCollection, object>>[0]);
			//		clientContext.ExecuteQuery();
			//		File file = (
			//			from f in files
			//			where f.Name == this.FileName
			//			select f).FirstOrDefault<File>();
			//		if (Fields != null)
			//		{
			//			foreach (KeyValuePair<string, object> current in Fields)
			//			{
			//				file.get_ListItemAllFields().set_Item(current.Key, current.Value);
			//			}
			//			file.get_ListItemAllFields().Update();
			//			clientContext.Load<File>(file, new Expression<Func<File, object>>[0]);
			//			clientContext.ExecuteQuery();
			//		}
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
