using System;
namespace FrameWork.MenuControl
{
	public class MenuItemBilder : ItemBilderBase<MenuItemBilder>
	{
		private MenuItemFactory menu = new MenuItemFactory();
		public MenuItemBilder(Menu item) : base(item)
		{
		}
		public override MenuItemBilder Text(string text)
		{
			base.text(text);
			return this;
		}
		public override MenuItemBilder ClassIcon(string icon)
		{
			base.classIcon(icon);
			return this;
		}
		public override MenuItemBilder Roles(string roles)
		{
			base.roles(roles);
			return this;
		}
		public override MenuItemBilder Groups(string groups)
		{
			base.groups(groups);
			return this;
		}
		public MenuItemBilder Items(Action<MenuItemFactory> addMenues)
		{
			addMenues(this.menu);
			if (this.menu.Items.Count > 0)
			{
				(this.item as Menu).Items = this.menu.Items;
			}
			return this;
		}
	}
}
