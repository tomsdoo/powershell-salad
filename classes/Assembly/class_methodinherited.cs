namespace Assembly
{
	public class MethodInheritedEClass : MethodEClass
	{
		public const string AttributeNameInheritedFrom = "inheritedfrom";
		protected string m_inheritedfrom;
		public string inheritedfrom
		{
			get
			{
				return m_inheritedfrom;
			}
		}
		public MethodInheritedEClass(int provided_seq, System.Reflection.MethodInfo provided_mi) : base(provided_seq, provided_mi)
		{
			m_inheritedfrom = provided_mi.DeclaringType.FullName;
		}
		public MethodInheritedEClass(MethodInheritedEClass provided_obj) : base(provided_obj)
		{
			m_inheritedfrom = provided_obj.inheritedfrom;
		}
		public MethodInheritedEClass(System.Xml.XmlElement provided_element) : base(provided_element)
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
	public class MethodInheritedLClass : AbstLib.LBaseClass
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
					cap.InnerText = "Inherited Methods";
					t.AppendChild(cap);
					System.Xml.XmlElement r = x.CreateElement("tr");
					t.AppendChild(r);
					System.Xml.XmlElement inheritedfromd = x.CreateElement("td");
					inheritedfromd.InnerText = "InheritedFrom";
					r.AppendChild(inheritedfromd);
					System.Xml.XmlElement isstaticd = x.CreateElement("td");
					isstaticd.InnerText = "IsStatic";
					r.AppendChild(isstaticd);
					System.Xml.XmlElement datad = x.CreateElement("td");
					datad.InnerText = "Data";
					r.AppendChild(datad);
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						MethodInheritedEClass mym = (MethodInheritedEClass)(m_e[icnt]);
						r = x.CreateElement("tr");
						t.AppendChild(r);
						inheritedfromd = x.CreateElement("td");
						inheritedfromd.InnerText = mym.inheritedfrom;
						r.AppendChild(inheritedfromd);
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
		public MethodInheritedLClass() : base(){}
		public MethodInheritedLClass(MethodInheritedLClass provided_obj) : base(provided_obj){}
		public MethodInheritedLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new MethodInheritedEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
