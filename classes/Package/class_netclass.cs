namespace Package
{
	public class NetClassEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameFullName = "fullname";
		public const string AttributeNameName = "name";
		public const string AttributeNameNS = "ns";
		protected string m_ns;
		public string ns
		{
			get
			{
				return m_ns;
			}
		}
		protected string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		protected string m_fullname;
		public string fullname
		{
			get
			{
				return m_fullname;
			}
		}
		public NetClassEClass(int provided_seq, string provided_fullname) : base(provided_seq)
		{
			m_fullname = provided_fullname;
			m_ns = System.IO.Path.GetFileNameWithoutExtension(m_fullname);
			m_name = System.IO.Path.GetExtension(m_fullname).Substring(1);
		}
		public NetClassEClass(NetClassEClass provided_obj) : base(provided_obj)
		{
			m_fullname = provided_obj.fullname;
			m_ns = provided_obj.ns;
			m_name = provided_obj.name;
		}
		public NetClassEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_fullname = provided_element.GetAttribute(AttributeNameFullName);
			m_ns = provided_element.GetAttribute(AttributeNameNS);
			m_name = provided_element.GetAttribute(AttributeNameName);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameFullName, m_fullname);
			ret.SetAttribute(AttributeNameNS, m_ns);
			ret.SetAttribute(AttributeNameName, m_name);
			return ret;
		}
	}
	public class NetClassLClass : AbstLib.LBaseClass
	{
		public NetClassLClass() : base(){}
		public NetClassLClass(NetClassLClass provided_obj) : base(provided_obj){}
		public NetClassLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new NetClassEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
