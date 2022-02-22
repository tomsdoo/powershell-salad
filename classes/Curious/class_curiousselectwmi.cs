namespace Curious
{
	public class CuriousSelectWMIClass : CuriousSelectClass
	{
		public new const string ClassName = "CuriousSelectWMIClass";
		public CuriousSelectWMIClass(int provided_seq, int provided_second, string provided_filename, string provided_sql, string provided_keycolumnsstr, string provided_resultcaptioncolumnsstr) : base(provided_seq, provided_second, provided_filename, provided_sql, provided_keycolumnsstr, provided_resultcaptioncolumnsstr)
		{
		}
		public CuriousSelectWMIClass(int provided_seq, int provided_second, string provided_filename, string provided_sql, string[] provided_keycolumns, string[] provided_resultcaptioncolumns) : base(provided_seq, provided_second, provided_filename, provided_sql, provided_keycolumns, provided_resultcaptioncolumns)
		{
		}
		public CuriousSelectWMIClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
		}
		protected override ACC.RowLClass MyExecuteQuery()
		{
			WMI.RemoteComputerClass rc = new WMI.RemoteComputerClass(m_filename);
			return rc.ExecuteQuery(m_sql);
		}
	}
}
