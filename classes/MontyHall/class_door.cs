namespace MontyHall
{
	public class DoorE : AbstLib.EBaseClass
	{
		public const string AttributeNameThat = "that";
		public const string AttributeNameOpened = "opened";
		public const string Goat = "goat";
		public const string Gift = "gift";
		string m_that;
		public string that
		{
			get
			{
				return m_that;
			}
		}
		bool m_opened;
		public bool opened
		{
			get
			{
				return m_opened;
			}
		}
		public DoorE(int provided_seq, string provided_that) : base(provided_seq)
		{
			m_that = provided_that;
			m_opened = false;
		}
		public DoorE(DoorE provided_obj) : base(provided_obj)
		{
			m_that = provided_obj.that;
			m_opened = provided_obj.opened;
		}
		public DoorE(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_that = provided_element.GetAttribute(AttributeNameThat);
			m_opened = bool.Parse(provided_element.GetAttribute(AttributeNameOpened));
		}
		public void Open()
		{
			m_opened = true;
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameThat, m_that);
			ret.SetAttribute(AttributeNameOpened, m_opened.ToString());
			return ret;
		}
	}
	public class DoorL : AbstLib.LBaseClass
	{
		public DoorL() : base(){}
		public DoorL(DoorL provided_obj) : base(provided_obj){}
		public DoorL(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new DoorE((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
