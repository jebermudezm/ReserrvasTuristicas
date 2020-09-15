using System;
namespace FrameWork.MenuControl
{
	public class ItemBilder : ItemBilderBase<ItemBilder>
	{
		public ItemBilder(Item item) : base(item)
		{
		}
		public ItemBilder Link(string link)
		{
			if (!link.StartsWith("~/") && !link.StartsWith("http://") && !link.StartsWith("https://") && link != "#")
			{
				throw new Exception("The Url is not valid");
			}
			if (link.StartsWith("~/"))
			{
				string[] array = link.Replace("~/", "").Split(new string[]
				{
					"/",
					"?"
				}, StringSplitOptions.None);
				if (array.Length < 2 && array.Length > 3)
				{
					throw new Exception("The Url is not valid, must have a Controller and an Action");
				}
				(this.item as Item).Controller = array[0];
				(this.item as Item).Action = array[1];
				if (array.Length == 3)
				{
					(this.item as Item).Paramenters = array[2];
				}
				link = "";
			}
			(this.item as Item).Link = link;
			return this;
		}
		public ItemBilder Action(string controller, string action)
		{
			(this.item as Item).Controller = controller;
			(this.item as Item).Action = action;
			(this.item as Item).Link = "";
			return this;
		}
		public ItemBilder Action(string controller, string action, object paramenters)
		{
			(this.item as Item).Paramenters = paramenters;
			this.Action(controller, action);
			return this;
		}
		public override ItemBilder Text(string text)
		{
			base.text(text);
			return this;
		}
		public override ItemBilder ClassIcon(string icon)
		{
			base.classIcon(icon);
			return this;
		}
		public override ItemBilder Roles(string roles)
		{
			base.roles(roles);
			return this;
		}
		public override ItemBilder Groups(string groups)
		{
			base.groups(groups);
			return this;
		}
	}
}
