namespace AbstLib
{
	public class EBaseClass
	{
		public const string AttributeNameSeq = "seq";
		protected int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		public string xmlstr
		{
			get
			{
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				x.AppendChild(GetXml(x));
				return x.OuterXml;
			}
		}
		public EBaseClass(int provided_seq)
		{
			m_seq = provided_seq;
		}
		public EBaseClass(EBaseClass provided_obj)
		{
			m_seq = provided_obj.seq;
		}
		public EBaseClass(System.Xml.XmlElement provided_element)
		{
			m_seq = int.Parse(provided_element.GetAttribute(AttributeNameSeq));
		}
		public System.Xml.XmlElement GetXml(System.Xml.XmlDocument provided_xml)
		{
			System.Xml.XmlElement myelement = provided_xml.CreateElement(GetType().Name);
			return SetAttribute(myelement);
		}
		protected virtual System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			provided_element.SetAttribute(AttributeNameSeq, m_seq.ToString());
			return provided_element;
		}
	}
	public class LBaseClass
	{
		protected EBaseClass[] m_e;
		public EBaseClass[] e
		{
			get
			{
				return m_e;
			}
		}
		public int maxseq
		{
			get
			{
				int ret = 0;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						if(ret < m_e[icnt].seq)
						{
							ret = m_e[icnt].seq;
						}
					}
				}
				return ret;
			}
		}
		public string xmlstr
		{
			get
			{
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				x.AppendChild(GetXml(x));
				return x.OuterXml;
			}
		}
		public EBaseClass this[EBaseClass provided_obj]
		{
			get
			{
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						if(provided_obj.seq == m_e[icnt].seq)
						{
							return m_e[icnt];
						}
					}
				}
				return null;
			}
		}
		public LBaseClass()
		{
			m_e = null;
		}
		public LBaseClass(LBaseClass provided_obj)
		{
			m_e = provided_obj.e;
		}
		public LBaseClass(System.Xml.XmlElement provided_element)
		{
			if(null == provided_element.ChildNodes)
			{
				m_e = null;
			}
			else
			{
				InitializeFromXmlElement(provided_element);
			}
		}
		protected virtual void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new EBaseClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
		public System.Xml.XmlElement GetXml(System.Xml.XmlDocument provided_xml)
		{
			System.Xml.XmlElement ret = provided_xml.CreateElement(GetType().Name);
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					ret.AppendChild(m_e[icnt].GetXml(provided_xml));
				}
			}
			return ret;
		}
		public static LBaseClass operator + (LBaseClass provided_l, EBaseClass provided_e)
		{
			LBaseClass ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static LBaseClass operator - (LBaseClass provided_l, EBaseClass provided_e)
		{
			LBaseClass templ = new LBaseClass();
			int idx = provided_l.FindIndex(provided_e);
			if(null != provided_l.e)
			{
				for(int icnt = 0; icnt < provided_l.e.Length; icnt++)
				{
					if(icnt != idx)
					{
						templ = templ + provided_l.e[icnt];
					}
				}
			}
			LBaseClass ret = provided_l;
			ret.Clear();
			if(null != templ.e)
			{
				for(int jcnt = 0; jcnt < templ.e.Length; jcnt++)
				{
					ret = ret + templ.e[jcnt];
				}
			}
			return ret;
		}
		protected virtual int FindIndex(EBaseClass provided_obj)
		{
			return FindIndex(provided_obj.seq);
		}
		protected virtual int FindIndex(int provided_seq)
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					if(provided_seq == m_e[icnt].seq)
					{
						return icnt;
					}
				}
			}
			return -1;
		}
		protected virtual void JustAdd(EBaseClass provided_obj)
		{
			if(null == m_e)
			{
				m_e = new EBaseClass[1];
				m_e[0] = provided_obj;
			}
			else
			{
				EBaseClass[] templ = new EBaseClass[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
		}
		protected void Clear()
		{
			m_e = null;
		}
	}
}
