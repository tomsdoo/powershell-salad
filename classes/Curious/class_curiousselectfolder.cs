namespace Curious
{
	public class CuriousSelectFolderClass : CuriousSelectClass
	{
		public new const string ClassName = "CuriousSelectFolderClass";
		public CuriousSelectFolderClass(	int provided_seq,
											int provided_second,
											string provided_filename,
											string provided_keycolumnsstr,
											string provided_resultcaptioncolumnsstr)
		: base(	provided_seq,
				provided_second,
				provided_filename,
				("select * from " + provided_filename),
				provided_keycolumnsstr,
				provided_resultcaptioncolumnsstr)
		{
		}
		public CuriousSelectFolderClass(	int provided_seq,
											int provided_second,
											string provided_filename,
											string[] provided_keycolumns,
											string[] provided_resultcaptioncolumns)
		: base(	provided_seq,
				provided_second,
				provided_filename,
				("select * from " + provided_filename),
				provided_keycolumns,
				provided_resultcaptioncolumns)
		{
		}
		public CuriousSelectFolderClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override ACC.RowLClass MyExecuteQuery()
		{
			Folder.DirectoryInfoEClass di = new Folder.DirectoryInfoEClass(int.MinValue, m_filename);
			return di.rowl;
		}
	}
}
