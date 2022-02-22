namespace Curious
{
	public class CuriousSelectFolderTVFormClass : CuriousSelectTVFormClass
	{
		public new const string titlestr = "SelectExplorerFolder";
		public CuriousSelectFolderTVFormClass(CuriousLClass provided_l) : base(provided_l){}
		protected override string GetTitle()
		{
			return titlestr;
		}
	}
}
