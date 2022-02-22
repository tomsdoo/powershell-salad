namespace Assembly
{
	public class TypeEComparer : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			TypeEClass myx = (TypeEClass)x;
			TypeEClass myy = (TypeEClass)y;
			return string.Compare(myx.fullname, myy.fullname);
		}
	}
	public class TypeEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameAssemblyQualifiedName = "assemblyqualifiedname";
		public const string AttributeNameBaseTypeName = "basetypename";
		public const string AttributeNameFullName = "fullname";
		public const string AttributeNameMethods = "methods";
		public const string AttributeNameInheritedMethods = "inheritedmethods";
		public const string AttributeNameProperties = "properties";
		public const string AttributeNameInheritedProperties = "inheritedproperties";
		public const string AttributeNameConstructors = "constructors";
		string m_assemblyqualifiedname;
		public string assemblyqualifiedname
		{
			get
			{
				return m_assemblyqualifiedname;
			}
		}
		string m_basetypename;
		public string basetypename
		{
			get
			{
				return m_basetypename;
			}
		}
		string m_fullname;
		public string fullname
		{
			get
			{
				return m_fullname;
			}
		}
		MethodLClass m_methodl;
		public MethodLClass methodl
		{
			get
			{
				return m_methodl;
			}
		}
		MethodInheritedLClass m_methodinheritedl;
		public MethodInheritedLClass methodinheritedl
		{
			get
			{
				return m_methodinheritedl;
			}
		}
		PropertyLClass m_propertyl;
		public PropertyLClass propertyl
		{
			get
			{
				return m_propertyl;
			}
		}
		PropertyInheritedLClass m_propertyinheritedl;
		public PropertyInheritedLClass propertyinheritedl
		{
			get
			{
				return m_propertyinheritedl;
			}
		}
		ConstructorLClass m_constructorl;
		public ConstructorLClass constructorl
		{
			get
			{
				return m_constructorl;
			}
		}
		public string html
		{
			get
			{
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				System.Xml.XmlElement h = x.CreateElement("HTML");
				x.AppendChild(h);
				System.Xml.XmlElement b = x.CreateElement("BODY");
				h.AppendChild(b);
				string bod = htmltable;
				bod += "<br /><br />";
				bod += m_propertyl.htmltable;
				bod += "<br /><br />";
				bod += m_constructorl.htmltable;
				bod += "<br /><br />";
				bod += m_methodl.htmltable;
				bod += "<br /><br />";
				bod += m_propertyinheritedl.htmltable;
				bod += "<br /><br />";
				bod += m_methodinheritedl.htmltable;
				b.InnerXml = bod;
				return x.OuterXml;
			}
		}
		public string htmltable
		{
			get
			{
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				System.Xml.XmlElement t = x.CreateElement("TABLE");
				x.AppendChild(t);
				t.SetAttribute("border","1");
				System.Xml.XmlElement r = x.CreateElement("tr");
				t.AppendChild(r);
				System.Xml.XmlElement c = x.CreateElement("td");
				c.InnerText = "FullName";
				r.AppendChild(c);
				c = x.CreateElement("td");
				c.InnerText = m_fullname;
				r.AppendChild(c);
				r = x.CreateElement("tr");
				t.AppendChild(r);
				c = x.CreateElement("td");
				c.InnerText = "BaseType";
				r.AppendChild(c);
				c = x.CreateElement("td");
				c.InnerText = m_basetypename;
				r.AppendChild(c);
				return x.OuterXml;
			}
		}
		public TypeEClass(int provided_seq, System.Type provided_type) : base(provided_seq)
		{
			m_assemblyqualifiedname = provided_type.AssemblyQualifiedName;
			m_basetypename = provided_type.BaseType.FullName;
			m_fullname = provided_type.FullName;
			m_methodl = new MethodLClass();
			m_methodinheritedl = new MethodInheritedLClass();
			m_propertyl = new PropertyLClass();
			m_propertyinheritedl = new PropertyInheritedLClass();
			m_constructorl = new ConstructorLClass();
			foreach(System.Reflection.MemberInfo mi in provided_type.GetMembers())
			{
				switch(mi.MemberType)
				{
					case System.Reflection.MemberTypes.Constructor:
						{
							m_constructorl = (ConstructorLClass)(m_constructorl + new ConstructorEClass(m_constructorl.maxseq + 1, (System.Reflection.ConstructorInfo)(mi)));
							break;
						}
					case System.Reflection.MemberTypes.Method:
						{
							if(mi.DeclaringType.FullName == provided_type.FullName)
							{
								m_methodl = (MethodLClass)(m_methodl + new MethodEClass(m_methodl.maxseq + 1, (System.Reflection.MethodInfo)(mi)));
							}
							else
							{
								m_methodinheritedl = (MethodInheritedLClass)(m_methodinheritedl + new MethodInheritedEClass(m_methodinheritedl.maxseq + 1, (System.Reflection.MethodInfo)(mi)));
							}
							break;
						}
					case System.Reflection.MemberTypes.Property:
						{
							if(mi.DeclaringType.FullName == provided_type.FullName)
							{
								m_propertyl = (PropertyLClass)(m_propertyl + new PropertyEClass(m_propertyl.maxseq + 1, (System.Reflection.PropertyInfo)(mi)));
							}
							else
							{
								m_propertyinheritedl = (PropertyInheritedLClass)(m_propertyinheritedl + new PropertyInheritedEClass(m_propertyinheritedl.maxseq + 1, (System.Reflection.PropertyInfo)(mi)));
							}
							break;
						}
					default:
						{
							break;
						}
				}
			}
		}
		public TypeEClass(TypeEClass provided_obj) : base(provided_obj)
		{
			m_assemblyqualifiedname = provided_obj.assemblyqualifiedname;
			m_basetypename = provided_obj.basetypename;
			m_fullname = provided_obj.fullname;
			m_methodl = provided_obj.methodl;
			m_methodinheritedl = provided_obj.methodinheritedl;
			m_propertyl = provided_obj.propertyl;
			m_propertyinheritedl = provided_obj.propertyinheritedl;
			m_constructorl = provided_obj.constructorl;
		}
		public TypeEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_assemblyqualifiedname = provided_element.GetAttribute(AttributeNameAssemblyQualifiedName);
			m_basetypename = provided_element.GetAttribute(AttributeNameBaseTypeName);
			m_fullname = provided_element.GetAttribute(AttributeNameFullName);
			string methodstr = provided_element.GetAttribute(AttributeNameMethods);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(methodstr);
			m_methodl = new MethodLClass((System.Xml.XmlElement)(x.DocumentElement));
			string methodistr = provided_element.GetAttribute(AttributeNameMethods);
			System.Xml.XmlDocument xmi = new System.Xml.XmlDocument();
			xmi.LoadXml(methodistr);
			m_methodinheritedl = new MethodInheritedLClass((System.Xml.XmlElement)(xmi.DocumentElement));
			string propertystr = provided_element.GetAttribute(AttributeNameProperties);
			System.Xml.XmlDocument xp = new System.Xml.XmlDocument();
			xp.LoadXml(propertystr);
			m_propertyl = new PropertyLClass((System.Xml.XmlElement)(xp.DocumentElement));
			string propertyistr = provided_element.GetAttribute(AttributeNameProperties);
			System.Xml.XmlDocument xpi = new System.Xml.XmlDocument();
			xpi.LoadXml(propertyistr);
			m_propertyinheritedl = new PropertyInheritedLClass((System.Xml.XmlElement)(xpi.DocumentElement));
			string constructorstr = provided_element.GetAttribute(AttributeNameConstructors);
			System.Xml.XmlDocument xcn = new System.Xml.XmlDocument();
			xcn.LoadXml(constructorstr);
			m_constructorl = new ConstructorLClass((System.Xml.XmlElement)(xcn.DocumentElement));
		}
		public void OutHTML(string provided_folder, string[] provided_classnames)
		{
			try
			{
				System.IO.StreamWriter writer = new System.IO.StreamWriter(provided_folder + m_fullname + ".htm", false);
				string myhtml = html;
				if(null != provided_classnames)
				{
					for(int icnt = 0; icnt < provided_classnames.Length; icnt++)
					{
						if(provided_classnames[icnt] != m_fullname)
						{
							string findstr = provided_classnames[icnt];
							string repstr = "<a href=\"" + findstr + ".htm\" target=\"frc\">" + findstr + "</a>";
							myhtml = myhtml.Replace(findstr, repstr);
						}
					}
				}
				writer.Write(myhtml);
				writer.Close();
			}
			catch{}
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameAssemblyQualifiedName, m_assemblyqualifiedname);
			ret.SetAttribute(AttributeNameBaseTypeName, m_basetypename);
			ret.SetAttribute(AttributeNameFullName, m_fullname);
			ret.SetAttribute(AttributeNameMethods, m_methodl.xmlstr);
			ret.SetAttribute(AttributeNameInheritedMethods, m_methodinheritedl.xmlstr);
			ret.SetAttribute(AttributeNameProperties, m_propertyl.xmlstr);
			ret.SetAttribute(AttributeNameInheritedProperties, m_propertyinheritedl.xmlstr);
			ret.SetAttribute(AttributeNameConstructors, m_constructorl.xmlstr);
			return ret;
		}
	}
	public class TypeLClass : AbstLib.LBaseClass
	{
		public string[] classnames
		{
			get
			{
				string[] ret = null;
				if(null != m_e)
				{
					ret = new string[m_e.Length];
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret[icnt] = ((TypeEClass)(m_e[icnt])).fullname;
					}
				}
				return ret;
			}
		}
		public string html
		{
			get
			{
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				System.Xml.XmlElement h = x.CreateElement("HTML");
				x.AppendChild(h);
				System.Xml.XmlElement b = x.CreateElement("BODY");
				h.AppendChild(b);
				if(null != m_e)
				{
					System.Array.Sort(m_e, new TypeEComparer());
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						System.Xml.XmlElement atag = x.CreateElement("a");
						b.AppendChild(atag);
						atag.SetAttribute("target","frc");
						atag.SetAttribute("href", ((TypeEClass)(m_e[icnt])).fullname + ".htm");
						atag.InnerText = ((TypeEClass)(m_e[icnt])).fullname;
						b.AppendChild(x.CreateElement("br"));
					}
				}
				return x.OuterXml;
			}
		}
		public string mainhtml
		{
			get
			{
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				System.Xml.XmlElement h = x.CreateElement("HTML");
				x.AppendChild(h);
				System.Xml.XmlElement f = x.CreateElement("frameset");
				h.AppendChild(f);
				f.SetAttribute("cols", "30%,*");
				System.Xml.XmlElement fr = x.CreateElement("frame");
				fr.SetAttribute("src", "index.htm");
				fr.SetAttribute("name", "fri");
				f.AppendChild(fr);
				fr = x.CreateElement("frame");
				fr.SetAttribute("src", "index.htm");
				fr.SetAttribute("name", "frc");
				f.AppendChild(fr);
				return x.OuterXml;
			}
		}
		public TypeLClass() : base(){}
		public TypeLClass(TypeLClass provided_obj) : base(provided_obj){}
		public TypeLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		public void OutHTML(string provided_folder, string[] provided_classnames)
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					((TypeEClass)(m_e[icnt])).OutHTML(provided_folder, provided_classnames);
				}
			}
			System.IO.StreamWriter writer = new System.IO.StreamWriter(provided_folder + "index.htm", false);
			writer.Write(html);
			writer.Close();
			writer = new System.IO.StreamWriter(provided_folder + "__.htm", false);
			writer.Write(mainhtml);
			writer.Close();
		}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new TypeEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
