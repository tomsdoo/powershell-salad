namespace Curious
{
	public class CuriousSelectWMITVFormClass : CuriousSelectTVFormClass
	{
		public new const string titlestr = "SelectExplorerWMI";
		public CuriousSelectWMITVFormClass(CuriousLClass provided_l) : base(provided_l)
		{
		}
		protected override string GetTitle()
		{
			return titlestr;
		}
	}
}
