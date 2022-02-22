namespace Package
{
	public class ClassEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameName = "name";
		public const string AttributeNameMethodL = "methodl";
		protected string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		protected MethodLClass m_methodl;
		public MethodLClass methodl
		{
			get
			{
				return m_methodl;
			}
		}
		public int filecount
		{
			get
			{
				return m_methodl.filecount;
			}
		}
		public string[] filenames
		{
			get
			{
				return m_methodl.filenames;
			}
		}
		public ClassEClass(int provided_seq, string provided_name, MethodLClass provided_methodl) : base(provided_seq)
		{
			m_name = provided_name;
			m_methodl = provided_methodl;
		}
		public ClassEClass(ClassEClass provided_obj) : base(provided_obj)
		{
			m_name = provided_obj.name;
			m_methodl = provided_obj.methodl;
		}
		public ClassEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_name = provided_element.GetAttribute(AttributeNameName);
			string methodstr = provided_element.GetAttribute(AttributeNameMethodL);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(methodstr);
			System.Xml.XmlElement ele = (System.Xml.XmlElement)(x.DocumentElement);
			m_methodl = new MethodLClass(ele);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameName, m_name);
			ret.SetAttribute(AttributeNameMethodL, m_methodl.xmlstr);
			return ret;
		}
	}
	public class ClassLClass : AbstLib.LBaseClass
	{
		public int filecount
		{
			get
			{
				int ret = 0;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret += ((ClassEClass)(m_e[icnt])).filecount;
					}
				}
				return ret;
			}
		}
		public string[] filenames
		{
			get
			{
				string[] ret = null;
				if(null != m_e)
				{
					ret = new string[filecount];
					int idx = 0;
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						string[] fnl = ((ClassEClass)(m_e[icnt])).filenames;
						if(null != fnl)
						{
							for(int jcnt = 0; jcnt < fnl.Length; jcnt++)
							{
								ret[idx] = fnl[jcnt];
								idx++;
							}
						}
					}
				}
				return ret;
			}
		}
		public ClassLClass() : base(){}
		public ClassLClass(ClassLClass provided_obj) : base(provided_obj){}
		public ClassLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new ClassEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
		public static ClassLClass operator + (ClassLClass provided_classl, MethodEClass provided_methode)
		{
			ClassLClass ret = provided_classl;
			int idx = provided_classl.FindIndex(provided_methode);
			if(-1 != idx)
			{
				ClassEClass orgc = (ClassEClass)(provided_classl.e[idx]);
				ClassEClass altc = new ClassEClass(orgc.seq, orgc.name, (MethodLClass)(orgc.methodl + provided_methode));
				ret.e[idx] = altc;
			}
			else
			{
				MethodLClass tempml = new MethodLClass();
				tempml = (MethodLClass)(tempml + provided_methode);
				ClassEClass nc = new ClassEClass(provided_classl.maxseq + 1, provided_methode.classname, tempml);
				ret.JustAdd(nc);
			}
			return ret;
		}
		private int FindIndex(MethodEClass provided_methode)
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					ClassEClass tempc = (ClassEClass)(m_e[icnt]);
					if(tempc.name.ToUpper() == provided_methode.classname.ToUpper())
					{
						return icnt;
					}
				}
			}
			return -1;
		}
	}
}
