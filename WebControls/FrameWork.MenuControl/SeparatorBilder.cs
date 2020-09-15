using System;
namespace FrameWork.MenuControl
{
	public class SeparatorBilder : ItemBilderBase<SeparatorBilder>
	{
		public SeparatorBilder(Separator item) : base(item)
		{
		}
		public override SeparatorBilder Text(string text)
		{
			base.text(text);
			return this;
		}
		public override SeparatorBilder ClassIcon(string icon)
		{
			base.classIcon(icon);
			return this;
		}
		public override SeparatorBilder Roles(string roles)
		{
			base.roles(roles);
			return this;
		}
		public override SeparatorBilder Groups(string groups)
		{
			base.groups(groups);
			return this;
		}
	}
}
