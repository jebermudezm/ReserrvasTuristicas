using System;
using System.IO;
namespace WebControls.Files
{
	public class Filer : IFiler
	{
		internal Stream myFile;
		internal string myFileName;
		internal string myName;
		internal string myExtension;
		internal string myFullPath;
		private StreamReader myReadText;
		private StreamWriter myWriteText;
		private bool myWasWrite;
		public Stream GetStream
		{
			get
			{
				return this.myFile;
			}
		}
		public bool CanRead
		{
			get
			{
				return this.myFile.CanRead;
			}
		}
		public bool CanWrite
		{
			get
			{
				return this.myFile.CanWrite;
			}
		}
		public string FileName
		{
			get
			{
				return this.myFileName;
			}
		}
		public string Name
		{
			get
			{
				return this.myName;
			}
		}
		public eFromServer From
		{
			get
			{
				return eFromServer.File;
			}
		}
		public long Size
		{
			get
			{
				return this.myFile.Length;
			}
		}
		public string Extension
		{
			get
			{
				return this.myExtension;
			}
		}
		public string Text
		{
			get
			{
				return this.myReadText.ReadToEnd();
			}
			set
			{
				this.myWriteText.Write(value);
				this.myWasWrite = true;
			}
		}
		public string FullPath
		{
			get
			{
				return this.myFullPath;
			}
		}
		public byte[] Binary
		{
			get
			{
				byte[] array = new byte[this.myFile.Length];
				this.myFile.Read(array, 0, (int)this.myFile.Length);
				return array;
			}
			set
			{
				this.myFile.Write(value, 0, (int)this.myFile.Length);
			}
		}
		public Filer()
		{
		}
		public Filer(Stream file, string name)
		{
			this.myFullPath = "";
			if (file != null)
			{
				this.myFile = file;
				FileInfo fileInfo = new FileInfo(name);
				this.myName = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.'));
				this.myExtension = fileInfo.Extension;
				this.myFileName = fileInfo.Name;
				if (this.myFile.CanRead)
				{
					this.myReadText = new StreamReader(this.myFile);
				}
				if (this.myFile.CanWrite)
				{
					this.myWriteText = new StreamWriter(this.myFile);
				}
			}
		}
		public string ReadText()
		{
			return this.myReadText.ReadToEnd().Substring(this.myReadText.Read(), 1);
		}
		public string ReadText(int From, int To)
		{
			return this.myReadText.ReadToEnd().Substring(From, To);
		}
		public string ReadTextLine()
		{
			return this.myReadText.ReadLine();
		}
		public string ReadTextLine(int Pos)
		{
			string[] array = this.myReadText.ReadToEnd().Split(new char[]
			{
				'\n'
			});
			if (Pos > array.Length)
			{
				Pos = array.Length;
			}
			return array[Pos].Replace('\r', '\0');
		}
		public void WriteText(string Text)
		{
			this.myWriteText.Write(Text);
			this.myWasWrite = true;
		}
		public void WriteTextLine(string Text)
		{
			this.myWriteText.WriteLine(Text);
			this.myWasWrite = true;
		}
		public void Close()
		{
			this.myReadText.Close();
			this.myWriteText.Close();
		}
		public void Delete()
		{
			if (this.myFullPath != "" && File.Exists(this.myFullPath))
			{
				if (this.myReadText != null)
				{
					this.myReadText.Dispose();
				}
				if (this.myWriteText != null)
				{
					this.myWriteText.Dispose();
				}
				File.Delete(this.myFullPath);
			}
		}
		public void Dispose()
		{
			if (this.myFile != null)
			{
				this.myFile.Dispose();
			}
			if (this.myReadText != null)
			{
				this.myReadText.Dispose();
			}
			if (this.myWriteText != null)
			{
				this.myWriteText.Dispose();
			}
		}
		public virtual bool Save(string filePath)
		{
			bool result = false;
			if (Directory.Exists(filePath))
			{
				if (this.myWasWrite)
				{
					this.myFile = this.myWriteText.BaseStream;
				}
				using (FileStream fileStream = new FileStream(Path.Combine(filePath, this.myFileName), FileMode.Create, FileAccess.Write))
				{
					byte[] array = new byte[this.myFile.Length];
					this.myFile.Read(array, 0, (int)this.myFile.Length);
					fileStream.Write(array, 0, array.Length);
					this.myFile.Close();
					result = true;
					return result;
				}
			}
			throw new DirectoryNotFoundException("El directorio \"" + filePath + "\" no existe");
		}
	}
}
