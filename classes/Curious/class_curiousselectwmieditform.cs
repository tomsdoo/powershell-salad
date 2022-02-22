namespace Curious
{
	public class CuriousSelectWMIEditFormClass : CuriousSelectEditFormClass
	{
		public CuriousSelectWMIEditFormClass(int provided_seq) : base(provided_seq){}
		public CuriousSelectWMIEditFormClass(CuriousSelectClass provided_obj) : base(provided_obj){}
		protected override void MyOK(object sender, System.EventArgs e)
		{
			m_select = new CuriousSelectWMIClass(m_seq, int.Parse(m_secondbox.Text), m_filebox.Text, m_sqlbox.Text, m_keycolumnbox.Text, m_resultcaptioncolumnsbox.Text);
			Close();
		}
	}
}
