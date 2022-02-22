namespace Package
{
	public class PackDetailLB : Util.LBBaseClass
	{
		public PackDetailLB(string[] provided_filel)
		{
			DataSource = provided_filel;
		}
	}
	public class PackDetailLBForm : Util.LBFormBaseClass
	{
		public PackDetailLBForm(string[] provided_filel)
		{
			Text = "PackageDetail";
			Initialize(new PackDetailLB(provided_filel));
		}
	}
}
