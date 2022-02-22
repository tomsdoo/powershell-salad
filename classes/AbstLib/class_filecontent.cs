namespace AbstLib
{
	public class FileContentEClass : EBaseClass
	{
		public const string AttributeNameFileName = "filename";
		public const string AttributeNameLineL = "linel";
		protected string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		protected bool m_binitialized;
		public bool binitialized
		{
			get
			{
				return m_binitialized;
			}
		}
		protected LineLClass m_linel;
		public LineLClass linel
		{
			get
			{
				return m_linel;
			}
		}
		Util.BGFileReaderClass m_fr;
		public Util.BGFileReaderClass fr
		{
			get
			{
				return m_fr;
			}
		}
		public string absolutefilename
		{
			get
			{
				return GetAbsoluteFileName();
			}
		}
		public FileContentEClass(int provided_seq, string provided_filename, LineLClass provided_linel) : base(provided_seq)
		{
			m_filename = provided_filename;
			m_linel = provided_linel;
			m_binitialized = true;
		}
		public FileContentEClass(FileContentEClass provided_obj) : base(provided_obj)
		{
			m_filename = provided_obj.filename;
			m_linel = provided_obj.linel;
			m_fr = provided_obj.fr;
			m_binitialized = provided_obj.binitialized;
		}
		public FileContentEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_filename = provided_element.GetAttribute(AttributeNameFileName);
			string linestr = provided_element.GetAttribute(AttributeNameLineL);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(linestr);
			System.Xml.XmlElement ele = (System.Xml.XmlElement)(x.DocumentElement);
			m_linel = new AbstLib.LineLClass(ele);
			m_binitialized = true;
		}
		public FileContentEClass(int provided_seq, string provided_filename) : base(provided_seq)
		{
			m_filename = provided_filename;
			m_binitialized = false;
			if(GetType().Name == typeof(FileContentEClass).Name)
			{
				InitializeLineL();
			}
		}
		public void Write()
		{
			try
			{
				System.IO.StreamWriter writer = new System.IO.StreamWriter(GetAbsoluteFileName(), false);
				writer.Write(m_linel.strdata);
				writer.Close();
				System.Console.WriteLine("Wrote: " + GetAbsoluteFileName());
			}
			catch
			{
				System.Console.WriteLine("WriteError: " + GetAbsoluteFileName());
			}
		}
		protected virtual string GetAbsoluteFileName()
		{
			return m_filename;
		}
		protected void InitializeLineL()
		{
			m_fr = new Util.BGFileReaderClass(GetAbsoluteFileName());
			m_fr.Completed += new AbstLib.MyCompletedEventHandler(ReadCompleted);
			m_fr.DoIt(null);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameFileName, m_filename);
			ret.SetAttribute(AttributeNameLineL, m_linel.xmlstr);
			return ret;
		}
		private void ReadCompleted(object myo)
		{
			m_linel = new LineLClass();
			if(null != m_fr.resultlines)
			{
				for(int icnt = 0; icnt < m_fr.resultlines.Length; icnt++)
				{
					m_linel = (LineLClass)(m_linel + new LineEClass(m_linel.maxseq + 1, m_fr.resultlines[icnt]));
				}
			}
			m_fr = null;
			m_binitialized = true;
		}
	}
	public class FileContentLClass : AbstLib.LBaseClass
	{
		public bool binitialized
		{
			get
			{
				bool ret = true;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						FileContentEClass fe = (FileContentEClass)(m_e[icnt]);
						ret = ret & fe.binitialized;
					}
				}
				return ret;
			}
		}
		public int filecount
		{
			get
			{
				int ret = 0;
				if(null != m_e)
				{
					ret = m_e.Length;
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
					ret = new string[m_e.Length];
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret[icnt] = ((FileContentEClass)(m_e[icnt])).absolutefilename;
					}
				}
				return ret;
			}
		}
		public FileContentLClass() : base(){}
		public FileContentLClass(FileContentLClass provided_obj) : base(provided_obj){}
		public FileContentLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new FileContentEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
