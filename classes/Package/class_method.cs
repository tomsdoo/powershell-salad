namespace Package
{
	public class MethodEClass : FunctionEClass
	{
		public const string AttributeNameClassName = "classname";
		protected string m_classname;
		public string classname
		{
			get
			{
				return m_classname;
			}
		}
		public MethodEClass(int provided_seq, string provided_rootfolder, string provided_filename, string provided_classname, string provided_name, AbstLib.LineLClass provided_linel) : base(provided_seq, provided_rootfolder, provided_filename, provided_name, provided_linel)
		{
			m_classname = provided_classname;
		}
		public MethodEClass(MethodEClass provided_obj) : base(provided_obj)
		{
			m_classname = provided_obj.classname;
		}
		public MethodEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_classname = provided_element.GetAttribute(AttributeNameClassName);
		}
		public MethodEClass(int provided_seq, string provided_rootfolder, string provided_filename) : base(provided_seq, provided_rootfolder, provided_filename)
		{
			string title = System.IO.Path.GetFileNameWithoutExtension(GetAbsoluteFileName());
			string[] classnmethod = title.Split('.');
			m_classname = classnmethod[0];
			m_name = classnmethod[1];
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameClassName, m_classname);
			return ret;
		}
	}
	public class MethodLClass : AbstLib.RelFileContentLClass
	{
		public MethodLClass() : base(){}
		public MethodLClass(MethodLClass provided_obj) : base(provided_obj){}
		public MethodLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new MethodEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
