using System;
using System.Collections.Generic;
namespace FrameWork.MenuControl
{
	public class Menu : ItemBase
	{
		public List<ItemBase> Items
		{
			get;
			set;
		}
		public bool Active
		{
			get;
			set;
		}
		public Menu()
		{
			this.Items = new List<ItemBase>();
		}
	}
}
