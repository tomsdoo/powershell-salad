namespace Package
{
	public class CSharpNSEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameNS = "ns";
		public const string AttributeNameFC = "fc";
		AbstLib.RelFileContentLClass m_filecontentl;
		public AbstLib.RelFileContentLClass filecontentl
		{
			get
			{
				return m_filecontentl;
			}
		}
		string m_rootfolder;
		public string rootfolder
		{
			get
			{
				return m_rootfolder;
			}
		}
		string m_ns;
		public string ns
		{
			get
			{
				return m_ns;
			}
		}
		public int filecount
		{
			get
			{
				return m_filecontentl.filecount;
			}
		}
		public string[] filenames
		{
			get
			{
				return m_filecontentl.filenames;
			}
		}
		public CSharpNSEClass(int provided_seq, string provided_rootfolder, string provided_folder) : base(provided_seq)
		{
			m_ns = System.IO.Path.GetFileName(provided_folder);
			m_filecontentl = new AbstLib.RelFileContentLClass();
			m_rootfolder = provided_rootfolder;
			foreach(string fn in System.IO.Directory.GetFiles(provided_folder, "*.cs", System.IO.SearchOption.TopDirectoryOnly))
			{
				m_filecontentl = (AbstLib.RelFileContentLClass)(m_filecontentl + new AbstLib.RelFileContentEClass(m_filecontentl.maxseq + 1, m_rootfolder, fn.Replace(m_rootfolder, string.Empty)));
			}
			while(true)
			{
				System.Threading.Thread.Sleep(10);
				if(m_filecontentl.binitialized)
				{
					break;
				}
			}
		}
		public CSharpNSEClass(int provided_seq, string provided_rootfolder, string provided_ns, AbstLib.RelFileContentLClass provided_filecontentl) : base(provided_seq)
		{
			m_rootfolder = provided_rootfolder;
			m_ns = provided_ns;
			m_filecontentl = provided_filecontentl;
		}
		public CSharpNSEClass(CSharpNSEClass provided_obj) : base(provided_obj)
		{
			m_rootfolder = provided_obj.rootfolder;
			m_ns = provided_obj.ns;
			m_filecontentl = provided_obj.filecontentl;
		}
		public CSharpNSEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_rootfolder = string.Empty;
			m_ns = provided_element.GetAttribute(AttributeNameNS);
			string fcstr = provided_element.GetAttribute(AttributeNameFC);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(fcstr);
			System.Xml.XmlElement ele = (System.Xml.XmlElement)(x.DocumentElement);
			m_filecontentl = new AbstLib.RelFileContentLClass(ele);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameNS, m_ns);
			ret.SetAttribute(AttributeNameFC, m_filecontentl.xmlstr);
			return ret;
		}
	}
	public class CSharpNSLClass : AbstLib.LBaseClass
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
						ret += ((CSharpNSEClass)(m_e[icnt])).filecount;
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
						string[] fnl = ((CSharpNSEClass)(m_e[icnt])).filenames;
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
		public CSharpNSLClass() : base(){}
		public CSharpNSLClass(CSharpNSLClass provided_obj) : base(provided_obj){}
		public CSharpNSLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new CSharpNSEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
