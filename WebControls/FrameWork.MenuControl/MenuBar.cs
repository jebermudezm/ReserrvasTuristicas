using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
namespace FrameWork.MenuControl
{
	public class MenuBar
	{
		private List<int> myResp;
		private List<int> myGruop;
		private string myPath;
		private string myAction;
		private MenuBarFactory menu = new MenuBarFactory();
		private List<ItemBase> myListItems;
		public List<ItemBase> ListItems
		{
			get
			{
				foreach (ItemBase current in this.myListItems)
				{
					this.setLink(current);
				}
				return this.myListItems;
			}
		}
		public MenuBar(HttpRequestBase request, List<int> Roles, List<int> Groups)
		{
			this.myPath = request.ApplicationPath;
			this.myAction = request.AppRelativeCurrentExecutionFilePath;
			this.myResp = Roles;
			this.myGruop = Groups;
			string[] array = this.myAction.Split(new string[]
			{
				""
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 2)
			{
				this.myAction = string.Join("", array, 0, 3);
			}
		}
		public MenuBar Items(Action<MenuBarFactory> addMenues)
		{
			addMenues(this.menu);
			this.myListItems = this.menu.Items;
			return this;
		}
		private void setLink(ItemBase item)
		{
			if (item is Item)
			{
				if ((item as Item).Link != "")
				{
					return;
				}
				string text = "";
				if ((item as Item).Paramenters is string)
				{
					text = "";
					if ((item as Item).Paramenters.ToString().IndexOf("=") > -1)
					{
						text += "?";
					}
					text += (item as Item).Paramenters.ToString();
				}
				else
				{
					if ((item as Item).Paramenters != null)
					{
						Type arg_AF_0 = (item as Item).Paramenters.GetType();
						text = "?";
						PropertyInfo[] properties = arg_AF_0.GetProperties();
						for (int i = 0; i < properties.Length; i++)
						{
							PropertyInfo propertyInfo = properties[i];
							if (text != "?")
							{
								text += "&";
							}
							text += string.Format("{0}={1}", propertyInfo.Name, propertyInfo.GetValue((item as Item).Paramenters, null).ToString());
						}
					}
				}
				string text2 = string.Format("~{0}/{1}", (item as Item).Controller, (item as Item).Action).ToLower();
				if (text2 == "~/home/index" && text == "")
				{
					text2 = "";
				}
				(item as Item).Link = this.myPath + text2.Replace("~", "") + text;
				if (text2 == this.myAction)
				{
					(item as Item).Active = true;
				}
			}
			if (item is Menu)
			{
				foreach (ItemBase current in (item as Menu).Items)
				{
					this.setLink(current);
					if (current is Item && (current as Item).Active)
					{
						(item as Menu).Active = true;
					}
					if (current is Menu && (current as Menu).Active)
					{
						(item as Menu).Active = true;
					}
				}
			}
		}
	}
}
