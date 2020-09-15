using System;
namespace FrameWork.MenuControl
{
	public class Item : ItemBase
	{
		public string Link
		{
			get;
			set;
		}
		public string Action
		{
			get;
			set;
		}
		public string Controller
		{
			get;
			set;
		}
		public object Paramenters
		{
			get;
			set;
		}
		public bool Active
		{
			get;
			set;
		}
	}
}
