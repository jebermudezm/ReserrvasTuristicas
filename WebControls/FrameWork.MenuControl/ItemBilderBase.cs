using System;
namespace FrameWork.MenuControl
{
	public abstract class ItemBilderBase<T> where T : class
	{
		protected ItemBase item;
		public ItemBilderBase(ItemBase item)
		{
			this.item = item;
		}
		public abstract T Text(string text);
		public abstract T ClassIcon(string icon);
		public abstract T Roles(string roles);
		public abstract T Groups(string groups);
		protected void text(string text)
		{
			this.item.Text = text;
		}
		protected void classIcon(string icon)
		{
			this.item.ClassIcon = icon;
		}
		protected void roles(string roles)
		{
			this.item.Roles = roles;
		}
		protected void groups(string groups)
		{
			this.item.Groups = groups;
		}
	}
}
