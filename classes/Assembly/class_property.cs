namespace Assembly
{
	public class PropertyEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameCanWrite = "canwrite";
		public const string AttributeNameData = "data";
		protected bool m_canwrite;
		public bool canwrite
		{
			get
			{
				return m_canwrite;
			}
		}
		protected string m_data;
		public string data
		{
			get
			{
				return m_data;
			}
		}
		public PropertyEClass(int provided_seq, System.Reflection.PropertyInfo provided_pi) : base(provided_seq)
		{
			m_canwrite = provided_pi.CanWrite;
			m_data = provided_pi.ToString();
		}
		public PropertyEClass(PropertyEClass provided_obj) : base(provided_obj)
		{
			m_canwrite = provided_obj.canwrite;
			m_data = provided_obj.data;
		}
		public PropertyEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_canwrite = bool.Parse(provided_element.GetAttribute(AttributeNameCanWrite));
			m_data = provided_element.GetAttribute(AttributeNameData);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameCanWrite, m_canwrite.ToString());
			ret.SetAttribute(AttributeNameData, m_data);
			return ret;
		}
	}
	public class PropertyLClass : AbstLib.LBaseClass
	{
		public string htmltable
		{
			get
			{
				string ret = string.Empty;
				if(null != m_e)
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					System.Xml.XmlElement t = x.CreateElement("table");
					x.AppendChild(t);
					t.SetAttribute("border","1");
					System.Xml.XmlElement cap = x.CreateElement("caption");
					cap.InnerText = "Properties";
					t.AppendChild(cap);
					System.Xml.XmlElement r = x.CreateElement("tr");
					t.AppendChild(r);
					System.Xml.XmlElement canwrited = x.CreateElement("td");
					canwrited.InnerText = "CanWrite";
					r.AppendChild(canwrited);
					System.Xml.XmlElement datad = x.CreateElement("td");
					datad.InnerText = "Data";
					r.AppendChild(datad);
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						PropertyEClass myp = (PropertyEClass)(m_e[icnt]);
						r = x.CreateElement("tr");
						t.AppendChild(r);
						canwrited = x.CreateElement("td");
						canwrited.InnerText = myp.canwrite.ToString();
						r.AppendChild(canwrited);
						datad = x.CreateElement("td");
						datad.InnerText = myp.data;
						r.AppendChild(datad);
					}
					ret = x.OuterXml;
				}
				return ret;
			}
		}
		public PropertyLClass() : base(){}
		public PropertyLClass(PropertyLClass provided_obj) : base(provided_obj){}
		public PropertyLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new PropertyEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
