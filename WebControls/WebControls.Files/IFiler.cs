using System;
namespace WebControls.Files
{
	public interface IFiler
	{
		string Name
		{
			get;
		}
		eFromServer From
		{
			get;
		}
		long Size
		{
			get;
		}
		string Text
		{
			get;
			set;
		}
		byte[] Binary
		{
			get;
			set;
		}
		string Extension
		{
			get;
		}
		string ReadText();
		string ReadText(int From, int To);
		string ReadTextLine(int Pos);
		void WriteText(string Text);
		void WriteTextLine(string Text);
		void Dispose();
		bool Save(string filePath);
	}
}
