namespace Assembly
{
	public class PropertyInheritedEClass : PropertyEClass
	{
		public const string AttributeNameInheritedFrom = "inheritedfrom";
		string m_inheritedfrom;
		public string inheritedfrom
		{
			get
			{
				return m_inheritedfrom;
			}
		}
		public PropertyInheritedEClass(int provided_seq, System.Reflection.PropertyInfo provided_pi) : base(provided_seq, provided_pi)
		{
			m_inheritedfrom = provided_pi.DeclaringType.FullName;
		}
		public PropertyInheritedEClass(PropertyInheritedEClass provided_obj) : base(provided_obj)
		{
			m_inheritedfrom = provided_obj.inheritedfrom;
		}
		public PropertyInheritedEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_inheritedfrom = provided_element.GetAttribute(AttributeNameInheritedFrom);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameInheritedFrom, m_inheritedfrom);
			return ret;
		}
	}
	public class PropertyInheritedLClass : AbstLib.LBaseClass
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
					cap.InnerText = "Inherited Properties";
					t.AppendChild(cap);
					System.Xml.XmlElement r = x.CreateElement("tr");
					t.AppendChild(r);
					System.Xml.XmlElement inheritedfromd = x.CreateElement("td");
					inheritedfromd.InnerText = "InheritedFrom";
					r.AppendChild(inheritedfromd);
					System.Xml.XmlElement canwrited = x.CreateElement("td");
					canwrited.InnerText = "CanWrite";
					r.AppendChild(canwrited);
					System.Xml.XmlElement datad = x.CreateElement("td");
					datad.InnerText = "Data";
					r.AppendChild(datad);
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						PropertyInheritedEClass myp = (PropertyInheritedEClass)(m_e[icnt]);
						r = x.CreateElement("tr");
						t.AppendChild(r);
						inheritedfromd = x.CreateElement("td");
						inheritedfromd.InnerText = myp.inheritedfrom;
						r.AppendChild(inheritedfromd);
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
		public PropertyInheritedLClass() : base(){}
		public PropertyInheritedLClass(PropertyInheritedLClass provided_obj) : base(provided_obj){}
		public PropertyInheritedLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new PropertyInheritedEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
