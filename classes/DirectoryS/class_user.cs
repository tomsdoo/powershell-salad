namespace DirectoryS
{
	public class UserEClass : DEntryEClass
	{
		public UserEClass(int provided_seq, System.DirectoryServices.DirectoryEntry provided_de) : base(provided_seq, provided_de){}
		public UserEClass(UserEClass provided_obj) : base(provided_obj){}
		public UserEClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			return ret;
		}
	}
	public class UserLClass : DEntryLClass
	{
		public UserLClass() : base(){}
		public UserLClass(UserLClass provided_obj) : base(provided_obj){}
		public UserLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_ele)
		{
			for(int icnt = 0; icnt < provided_ele.ChildNodes.Count; icnt++)
			{
				JustAdd(new UserEClass((System.Xml.XmlElement)(provided_ele.ChildNodes.Item(icnt))));
			}
		}
	}
}
