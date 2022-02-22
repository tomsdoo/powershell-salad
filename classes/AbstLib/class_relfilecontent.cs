namespace AbstLib
{
	public class RelFileContentEClass : FileContentEClass
	{
		protected string m_rootfolder;
		public string rootfolder
		{
			get
			{
				return m_rootfolder;
			}
		}
		public RelFileContentEClass(int provided_seq, string provided_rootfolder, string provided_filename, LineLClass provided_linel) : base(provided_seq, provided_filename, provided_linel)
		{
			m_rootfolder = provided_rootfolder;
		}
		public RelFileContentEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_rootfolder = string.Empty;
		}
		public RelFileContentEClass(RelFileContentEClass provided_obj) : base(provided_obj)
		{
			m_rootfolder = provided_obj.rootfolder;
		}
		public RelFileContentEClass(int provided_seq, string provided_rootfolder, string provided_filename) : base(provided_seq, provided_filename)
		{
			m_rootfolder = provided_rootfolder;
			if(!string.IsNullOrEmpty(System.IO.Path.GetFileName(m_rootfolder)))
			{
				m_rootfolder += System.IO.Path.DirectorySeparatorChar.ToString();
			}
			InitializeLineL();
		}
		protected override string GetAbsoluteFileName()
		{
			return (m_rootfolder + m_filename);
		}
	}
	public class RelFileContentLClass : FileContentLClass
	{
		public RelFileContentLClass() : base(){}
		public RelFileContentLClass(RelFileContentLClass provided_obj) : base(provided_obj){}
		public RelFileContentLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new RelFileContentEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
