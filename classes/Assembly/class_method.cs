namespace Assembly
{
	public class MethodEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameIsStatic = "isstatic";
		public const string AttributeNameData = "data";
		protected bool m_isstatic;
		public bool isstatic
		{
			get
			{
				return m_isstatic;
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
		public MethodEClass(int provided_seq, System.Reflection.MethodInfo provided_mi) : base(provided_seq)
		{
			m_isstatic = provided_mi.IsStatic;
			m_data = provided_mi.ToString();
		}
		public MethodEClass(MethodEClass provided_obj) : base(provided_obj)
		{
			m_isstatic = provided_obj.isstatic;
			m_data = provided_obj.data;
		}
		public MethodEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_isstatic = bool.Parse(provided_element.GetAttribute(AttributeNameIsStatic));
			m_data = provided_element.GetAttribute(AttributeNameData);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameIsStatic, m_isstatic.ToString());
			ret.SetAttribute(AttributeNameData, m_data);
			return ret;
		}
	}
	public class MethodLClass : AbstLib.LBaseClass
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
					cap.InnerText = "Methods";
					t.AppendChild(cap);
					System.Xml.XmlElement r = x.CreateElement("tr");
					t.AppendChild(r);
					System.Xml.XmlElement isstaticd = x.CreateElement("td");
					isstaticd.InnerText = "IsStatic";
					r.AppendChild(isstaticd);
					System.Xml.XmlElement datad = x.CreateElement("td");
					datad.InnerText = "Data";
					r.AppendChild(datad);
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						MethodEClass mym = (MethodEClass)(m_e[icnt]);
						r = x.CreateElement("tr");
						t.AppendChild(r);
						isstaticd = x.CreateElement("td");
						isstaticd.InnerText = mym.isstatic.ToString();
						r.AppendChild(isstaticd);
						datad = x.CreateElement("td");
						datad.InnerText = mym.data;
						r.AppendChild(datad);
					}
					ret = x.OuterXml;
				}
				return ret;
			}
		}
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
