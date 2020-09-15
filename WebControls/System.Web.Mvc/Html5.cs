using System;
namespace System.Web.Mvc
{
	public static class Html5
	{
		private static SideBilder mySideBar = new SideBilder();
		public static SideBilder SideBar
		{
			get
			{
				return Html5.mySideBar;
			}
			set
			{
				Html5.mySideBar = value;
			}
		}
	}
}
