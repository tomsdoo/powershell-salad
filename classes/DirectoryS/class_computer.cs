namespace DirectoryS
{
	public class ComputerEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameName = "name";
		public const string AttributeNameLocalGroups = "localgroups";
		public const string AttributeNameLocalUsers = "localusers";
		protected string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		protected GroupLClass m_localgroups;
		public GroupLClass localgroups
		{
			get
			{
				return m_localgroups;
			}
		}
		protected UserLClass m_localusers;
		public UserLClass localusers
		{
			get
			{
				return m_localusers;
			}
		}
		public ComputerEClass(int provided_seq, string provided_name) : base(provided_seq)
		{
			m_name = provided_name;
			InitializeGroups();
		}
		public ComputerEClass(ComputerEClass provided_obj) : base(provided_obj)
		{
			m_name = provided_obj.name;
			m_localgroups = provided_obj.localgroups;
			m_localusers = provided_obj.localusers;
		}
		public ComputerEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_name = provided_element.GetAttribute(AttributeNameName);
			string localgroupsstr = provided_element.GetAttribute(AttributeNameLocalGroups);
			System.Xml.XmlDocument xlg = new System.Xml.XmlDocument();
			xlg.LoadXml(localgroupsstr);
			m_localgroups = new GroupLClass((System.Xml.XmlElement)(xlg.DocumentElement));
			string localusersstr = provided_element.GetAttribute(AttributeNameLocalUsers);
			System.Xml.XmlDocument xlu = new System.Xml.XmlDocument();
			xlu.LoadXml(localusersstr);
			m_localusers = new UserLClass((System.Xml.XmlElement)(xlu.DocumentElement));
		}
		protected virtual void InitializeGroups()
		{
			System.DirectoryServices.DirectoryEntry mycomputer = new System.DirectoryServices.DirectoryEntry("WinNT://" + m_name);
			m_localgroups = new GroupLClass();
			m_localusers = new UserLClass();
			foreach(System.DirectoryServices.DirectoryEntry de in mycomputer.Children)
			{
				switch(de.SchemaClassName)
				{
					case "Group":
						{
							m_localgroups = (GroupLClass)(m_localgroups + new GroupEClass(m_localgroups.maxseq + 1, de));
							break;
						}
					case "User":
						{
							m_localusers = (UserLClass)(m_localusers + new UserEClass(m_localusers.maxseq + 1, de));
							break;
						}
					default:
						{
							break;
						}
				}
			}
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameName, m_name);
			ret.SetAttribute(AttributeNameLocalGroups, m_localgroups.xmlstr);
			ret.SetAttribute(AttributeNameLocalUsers, m_localusers.xmlstr);
			return ret;
		}
	}
	public class ComputerLClass : AbstLib.LBaseClass
	{
		public ComputerLClass() : base(){}
		public ComputerLClass(ComputerLClass provided_obj) : base(provided_obj){}
		public ComputerLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_ele)
		{
			for(int icnt = 0; icnt < provided_ele.ChildNodes.Count; icnt++)
			{
				JustAdd(new ComputerEClass((System.Xml.XmlElement)(provided_ele.ChildNodes.Item(icnt))));
			}
		}
	}
}
