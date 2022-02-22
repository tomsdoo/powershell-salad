namespace TCP
{
	public class TCPEnvelopeClass : AbstLib.EBaseClass
	{
		public const string AttributeNameMessage = "message";
		public const string AttributeNameFrom = "from";
		public const string AttributeNameCreatedAt = "createdat";
		public const string DateTimeFormatString = "yyyy/MM/dd HH:mm:ss";
		protected ProfileEClass m_from;
		public ProfileEClass from
		{
			get
			{
				return m_from;
			}
		}
		protected DateTime m_createdat;
		public DateTime createdat
		{
			get
			{
				return m_createdat;
			}
		}
		protected string m_message;
		public string message
		{
			get
			{
				return m_message;
			}
		}
		public string strdata
		{
			get
			{
				return (m_createdat.ToString(DateTimeFormatString) + " " + m_from.name + " > " + m_message);
			}
		}
		public TCPEnvelopeClass(int provided_seq, ProfileEClass provided_from, string provided_message) : base(provided_seq)
		{
			m_from = provided_from;
			m_message = provided_message;
			m_createdat = DateTime.Now;
		}
		public TCPEnvelopeClass(TCPEnvelopeClass provided_obj) : base(provided_obj)
		{
			m_from = provided_obj.from;
			m_message = provided_obj.message;
			m_createdat = provided_obj.createdat;
		}
		public TCPEnvelopeClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			string fromxml = provided_element.GetAttribute(AttributeNameFrom);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(fromxml);
			m_from = new ProfileEClass(x.DocumentElement);
			m_message = provided_element.GetAttribute(AttributeNameMessage);
			m_createdat = DateTime.ParseExact(provided_element.GetAttribute(AttributeNameCreatedAt), DateTimeFormatString, null);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameFrom, m_from.xmlstr);
			ret.SetAttribute(AttributeNameMessage, m_message);
			ret.SetAttribute(AttributeNameCreatedAt, m_createdat.ToString(DateTimeFormatString));
			return ret;
		}
	}
	public class TCPEnvelopeLClass : AbstLib.LBaseClass
	{
		public string strdata
		{
			get
			{
				string ret = string.Empty;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						TCPEnvelopeClass myenvelope = (TCPEnvelopeClass)(m_e[icnt]);
						ret += (myenvelope.strdata + "\r\n");
					}
				}
				return ret;
			}
		}
		public TCPEnvelopeLClass() : base(){}
		public TCPEnvelopeLClass(TCPEnvelopeLClass provided_obj) : base(){}
		public TCPEnvelopeLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new TCPEnvelopeClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
