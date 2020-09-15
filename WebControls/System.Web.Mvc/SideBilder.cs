using FrameWork.MenuControl;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace System.Web.Mvc
{
	public class SideBilder
	{
		private string myClass = "";
		private string myId = "";
		private string myHeader = "";
		private List<ItemBase> myListItems;
		private string myBody;
		private string myFooter = "";
		private byte level;
		public SideBilder Class(string name)
		{
			this.myClass = name;
			return this;
		}
		public SideBilder Id(string id)
		{
			this.myId = id;
			return this;
		}
		private string getAttributes(object htmlAttributes)
		{
			Type arg_0C_0 = htmlAttributes.GetType();
			string text = "";
			PropertyInfo[] properties = arg_0C_0.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				text += string.Format(" {0}=\"{1}\"", propertyInfo.Name.Replace("_", "-"), propertyInfo.GetValue(htmlAttributes, null).ToString());
			}
			return text;
		}
		public SideBilder Header(object htmlAttributes, string header)
		{
			this.myHeader = "<div@(Attrs)>@(BodyDiv)</div>";
			this.myHeader = this.myHeader.Replace("@(Attrs)", this.getAttributes(htmlAttributes)).Replace("@(BodyDiv)", header);
			return this;
		}
		public SideBilder Header(object htmlAttributes, Func<object, object> header)
		{
			return this.Header(htmlAttributes, header(new object()).ToString());
		}
		public SideBilder ListItems(List<ItemBase> items)
		{
			this.myListItems = items;
			return this;
		}
		public SideBilder Body(object htmlAttributes)
		{
			Type arg_17_0 = htmlAttributes.GetType();
			this.myBody = "<ul@(Attrs)>@(Body)</ul>";
			string text = "";
			PropertyInfo[] properties = arg_17_0.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				text += string.Format(" {0}=\"{1}\"", propertyInfo.Name.Replace("_", "-"), propertyInfo.GetValue(htmlAttributes, null).ToString());
			}
			this.myBody = this.myBody.Replace("@(Attrs)", text);
			return this;
		}
		public SideBilder Footer(object htmlAttributes, string footer)
		{
			this.myFooter = "<div@(Attrs)>@(BodyDiv)</div>";
			this.myFooter = this.myFooter.Replace("@(Attrs)", this.getAttributes(htmlAttributes)).Replace("@(BodyDiv)", footer);
			return this;
		}
		public SideBilder Footer(object htmlAttributes, Func<object, object> footer)
		{
			return this.Footer(htmlAttributes, footer(new object()).ToString());
		}
		public HtmlString Draw()
		{
			string format = "<aside id=\"{0}\" class=\"{1}\">{2}{3}{4}</aside>";
			string text = "";
			foreach (ItemBase current in this.myListItems)
			{
				this.level += 1;
				text += this.drawItem(current, true);
				this.level -= 1;
			}
			this.myBody = this.myBody.Replace("@(Body)", text);
			return new HtmlString(string.Format(format, new object[]
			{
				this.myId,
				this.myClass,
				this.myHeader,
				this.myBody,
				this.myFooter
			}));
		}
		private string drawItem(ItemBase item, bool isBase)
		{
			string text = "";
			string text2 = "";
			string arg = "";
			string text3 = "";
			if ((item is Item || item is Menu) & isBase)
			{
				if (!string.IsNullOrEmpty(item.ClassIcon))
				{
					text2 += string.Format("<span class=\"icon\"><i class=\"{0}\" ></i></span>", item.ClassIcon);
				}
				if (item is Menu & isBase)
				{
					text2 += "<span class=\"arrow\"></span>";
				}
			}
			if (!string.IsNullOrEmpty(item.Text))
			{
				text2 += string.Format("<span class=\"text\">{0}</span>", item.Text);
			}
			if (item is Item)
			{
				if ((item as Item).Active)
				{
					arg = " class=\"active\"";
				}
				if (!string.IsNullOrEmpty((item as Item).Link))
				{
					text += string.Format("<a href=\"{0}\">{1}</a>", (item as Item).Link, text2);
				}
			}
			else
			{
				if (item is Separator)
				{
					arg = " class=\"sidebar-category\"";
					text = string.Format("<span>{0}</span><span class=\"pull-right\"><i class=\"{1}\"></i></span>", item.Text, item.ClassIcon);
				}
				else
				{
					if (item is Menu)
					{
						if (!isBase)
						{
							text2 += "<span class=\"fa fa-angle-double-right pull-right arrow\"></span>";
						}
						if ((item as Menu).Active)
						{
							text2 += "<span class=\"selected\"></span>";
						}
						text += string.Format("<a href=\"javascript:void(0);\">{0}</a>", text2);
					}
				}
			}
			if (item is Menu)
			{
				foreach (ItemBase current in (item as Menu).Items)
				{
					this.level += 1;
					text3 += this.drawItem(current, false);
					this.level -= 1;
				}
				arg = " class=\"submenu\"";
				if ((item as Menu).Active)
				{
					arg = " class=\"submenu active\"";
				}
				text += string.Format("<ul>{0}</ul>", text3);
			}
			text = string.Format("<li{0}>{1}</li>", arg, text);
			return text;
		}
	}
}
