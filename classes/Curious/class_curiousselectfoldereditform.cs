namespace Curious
{
	public class CuriousSelectFolderEditFormClass : CuriousSelectEditFormClass
	{
		public CuriousSelectFolderEditFormClass(int provided_seq) : base(provided_seq){}
		public CuriousSelectFolderEditFormClass(CuriousSelectFolderClass provided_obj) : base(provided_obj){}
		protected override void Initialize()
		{
			base.Initialize();
			m_sqlbox.Visible = false;
		}
		protected override void MyOK(object sender, System.EventArgs e)
		{
			m_select = new CuriousSelectFolderClass(m_seq, int.Parse(m_secondbox.Text), m_filebox.Text, m_keycolumnbox.Text, m_resultcaptioncolumnsbox.Text);
			Close();
		}
	}
}
