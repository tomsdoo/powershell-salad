namespace Package
{
	public class FunctionEClass : AbstLib.RelFileContentEClass
	{
		public const string AttributeNameName = "name";
		protected string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		public FunctionEClass(int provided_seq, string provided_rootfolder, string provided_filename, string provided_name, AbstLib.LineLClass provided_linel) : base(provided_seq, provided_rootfolder, provided_filename, provided_linel)
		{
			m_name = provided_name;
		}
		public FunctionEClass(FunctionEClass provided_obj) : base(provided_obj)
		{
			m_name = provided_obj.name;
		}
		public FunctionEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_name = provided_element.GetAttribute(AttributeNameName);
		}
		public FunctionEClass(int provided_seq, string provided_rootfolder, string provided_filename) : base(provided_seq, provided_rootfolder, provided_filename)
		{
			m_name = System.IO.Path.GetFileNameWithoutExtension(GetAbsoluteFileName());
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameName, m_name);
			return ret;
		}
	}
	public class FunctionLClass : AbstLib.RelFileContentLClass
	{
		public FunctionLClass() : base(){}
		public FunctionLClass(FunctionLClass provided_obj) : base(provided_obj){}
		public FunctionLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new FunctionEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
