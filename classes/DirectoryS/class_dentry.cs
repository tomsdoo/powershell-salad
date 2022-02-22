namespace DirectoryS
{
	public class DEntryEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameName = "name";
		public const string AttributeNameDescription = "description";
		public const string AttributeNameSid = "sid";
		public const string AttributeNameNTName = "ntname";

		protected string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		protected string m_description;
		public string description
		{
			get
			{
				return m_description;
			}
		}
		protected string m_sid;
		public string sid
		{
			get
			{
				return m_sid;
			}
		}
		protected string m_ntname;
		public string ntname
		{
			get
			{
				return m_ntname;
			}
		}
		public DEntryEClass(int provided_seq, System.DirectoryServices.DirectoryEntry provided_entry) : base(provided_seq)
		{
			m_name = provided_entry.Name;
			m_description = string.Empty;
			try
			{
				m_description = (string)((provided_entry.Properties["Description"])[0]);
			}catch{}
			System.Security.Principal.SecurityIdentifier mysid = new System.Security.Principal.SecurityIdentifier((byte[])((provided_entry.Properties["objectSid"])[0]), 0);
			m_sid = mysid.Value;
			m_ntname = string.Empty;
			try
			{
				System.Security.Principal.NTAccount mynt = (System.Security.Principal.NTAccount)mysid.Translate(typeof(System.Security.Principal.NTAccount));
				m_ntname = mynt.Value;
			}catch{}
		}
		public DEntryEClass(DEntryEClass provided_obj) : base(provided_obj)
		{
			m_name = provided_obj.name;
			m_description = provided_obj.description;
			m_sid = provided_obj.sid;
			m_ntname = provided_obj.ntname;
		}
		public DEntryEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_name = provided_element.GetAttribute(AttributeNameName);
			m_description = provided_element.GetAttribute(AttributeNameDescription);
			m_sid = provided_element.GetAttribute(AttributeNameSid);
			m_ntname = provided_element.GetAttribute(AttributeNameNTName);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameName, m_name);
			ret.SetAttribute(AttributeNameDescription, m_description);
			ret.SetAttribute(AttributeNameSid, m_sid);
			ret.SetAttribute(AttributeNameNTName, m_ntname);
			return ret;
		}
	}
	public class DEntryLClass : AbstLib.LBaseClass
	{
		public DEntryLClass() : base(){}
		public DEntryLClass(DEntryLClass provided_obj) : base(provided_obj){}
		public DEntryLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_ele)
		{
			for(int icnt = 0; icnt < provided_ele.ChildNodes.Count; icnt++)
			{
				JustAdd(new DEntryEClass((System.Xml.XmlElement)(provided_ele.ChildNodes.Item(icnt))));
			}
		}
	}
}
