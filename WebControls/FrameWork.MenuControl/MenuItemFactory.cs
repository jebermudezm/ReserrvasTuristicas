using System;
using System.Collections.Generic;
namespace FrameWork.MenuControl
{
	public class MenuItemFactory
	{
		internal List<ItemBase> Items = new List<ItemBase>();
		public MenuItemBilder AddMenu()
		{
			Menu item = new Menu();
			MenuItemBilder arg_18_0 = new MenuItemBilder(item);
			this.Items.Add(item);
			return arg_18_0;
		}
		public ItemBilder AddItem()
		{
			Item item = new Item();
			ItemBilder arg_18_0 = new ItemBilder(item);
			this.Items.Add(item);
			return arg_18_0;
		}
	}
}
