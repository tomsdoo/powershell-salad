namespace DirectoryS
{
	public class GroupEClass : DEntryEClass
	{
		public const string AttributeNameMembers = "members";
		protected UserLClass m_members;
		public UserLClass members
		{
			get
			{
				return m_members;
			}
		}
		public GroupEClass(int provided_seq, System.DirectoryServices.DirectoryEntry provided_entry) : base(provided_seq, provided_entry)
		{
			m_members = new UserLClass();
			foreach(object myo in (System.Collections.IEnumerable)(provided_entry.Invoke("members")))
			{
				m_members = (UserLClass)(m_members + new UserEClass(m_members.maxseq + 1, new System.DirectoryServices.DirectoryEntry(myo)));
			}
		}
		public GroupEClass(GroupEClass provided_obj) : base(provided_obj)
		{
			m_members = provided_obj.members;
		}
		public GroupEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			string memberstr = provided_element.GetAttribute(AttributeNameMembers);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(memberstr);
			m_members = new UserLClass((System.Xml.XmlElement)(x.DocumentElement));
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameMembers, m_members.xmlstr);
			return ret;
		}
	}
	public class GroupLClass : DEntryLClass
	{
		public GroupLClass() : base(){}
		public GroupLClass(GroupLClass provided_obj) : base(provided_obj){}
		public GroupLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_ele)
		{
			for(int icnt = 0; icnt < provided_ele.ChildNodes.Count; icnt++)
			{
				JustAdd(new GroupEClass((System.Xml.XmlElement)(provided_ele.ChildNodes.Item(icnt))));
			}
		}
	}
}
