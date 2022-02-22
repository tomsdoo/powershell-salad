namespace Assembly
{
	public class ConstructorEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameData = "data";
		protected string m_data;
		public string data
		{
			get
			{
				return m_data;
			}
		}
		public ConstructorEClass(int provided_seq, System.Reflection.ConstructorInfo provided_ci) : base(provided_seq)
		{
			m_data = provided_ci.ToString();
		}
		public ConstructorEClass(ConstructorEClass provided_obj) : base(provided_obj)
		{
			m_data = provided_obj.data;
		}
		public ConstructorEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_data = provided_element.GetAttribute(AttributeNameData);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameData, m_data);
			return ret;
		}
	}
	public class ConstructorLClass : AbstLib.LBaseClass
	{
		public virtual string htmltable
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
					cap.InnerText = "Constructors";
					t.AppendChild(cap);
					System.Xml.XmlElement r = x.CreateElement("tr");
					t.AppendChild(r);
					System.Xml.XmlElement datad = x.CreateElement("td");
					datad.InnerText = "Data";
					r.AppendChild(datad);
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ConstructorEClass myc = (ConstructorEClass)(m_e[icnt]);
						r = x.CreateElement("tr");
						t.AppendChild(r);
						datad = x.CreateElement("td");
						datad.InnerText = myc.data;
						r.AppendChild(datad);
					}
					ret = x.OuterXml;
				}
				return ret;
			}
		}
		public ConstructorLClass() : base(){}
		public ConstructorLClass(ConstructorLClass provided_obj) : base(provided_obj){}
		public ConstructorLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_e)
		{
			for(int icnt = 0; icnt < provided_e.ChildNodes.Count; icnt++)
			{
				JustAdd(new ConstructorEClass((System.Xml.XmlElement)(provided_e.ChildNodes.Item(icnt))));
			}
		}
	}
}
