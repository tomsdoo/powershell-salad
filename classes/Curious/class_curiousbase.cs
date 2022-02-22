namespace Curious
{
	public delegate void CuriousHappenedEventHandler(CuriousResultLClass provided_result);
	public class CuriousComparer : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			CuriousBaseClass myx = (CuriousBaseClass)x;
			CuriousBaseClass myy = (CuriousBaseClass)y;
			return string.Compare(myx.sortkey, myy.sortkey);
		}
	}
	public class CuriousResultBaseClass
	{
		protected DateTime m_dt;
		public DateTime dt
		{
			get
			{
				return m_dt;
			}
		}
		public CuriousResultBaseClass()
		{
			m_dt = DateTime.Now;
		}
		public CuriousResultBaseClass(CuriousResultBaseClass provided_obj)
		{
			m_dt = provided_obj.dt;
		}
	}
	public class CuriousResultEBaseClass : CuriousResultBaseClass
	{
		protected int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		protected string m_status;
		public string status
		{
			get
			{
				return m_status;
			}
		}
		public CuriousResultEBaseClass(int provided_seq, string provided_status) : base()
		{
			m_seq = provided_seq;
			m_status = provided_status;
		}
		public CuriousResultEBaseClass(CuriousResultEBaseClass provided_obj) : base(provided_obj)
		{
			m_seq = provided_obj.seq;
			m_status = provided_obj.status;
		}
		public void SetSeq(int provided_seq)
		{
			m_seq = provided_seq;
		}
	}
	public class CuriousResultLClass
	{
		CuriousResultEBaseClass[] m_e;
		public CuriousResultEBaseClass[] e
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
		public CuriousResultLClass()
		{
			m_e = null;
		}
		public CuriousResultLClass(CuriousResultLClass provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static CuriousResultLClass operator + (CuriousResultLClass provided_l, CuriousResultEBaseClass provided_e)
		{
			CuriousResultLClass ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static CuriousResultLClass operator - (CuriousResultLClass provided_l, CuriousResultEBaseClass provided_e)
		{
			CuriousResultLClass ret = new CuriousResultLClass();
			int idx = provided_l.FindIndex(provided_e);
			if(null != provided_l.e)
			{
				for(int icnt = 0; icnt < provided_l.e.Length; icnt++)
				{
					if(idx != icnt)
					{
						ret = ret + provided_l.e[icnt];
					}
				}
			}
			return ret;
		}
		public static CuriousResultLClass operator + (CuriousResultLClass provided_l, CuriousResultLClass provided_l2)
		{
			CuriousResultLClass ret = provided_l;
			if(null != provided_l2.e)
			{
				for(int icnt = 0; icnt < provided_l2.e.Length; icnt++)
				{
					provided_l2.e[icnt].SetSeq(ret.maxseq + 1);
					ret = ret + provided_l2.e[icnt];
				}
			}
			return ret;
		}
		private int FindIndex(CuriousResultEBaseClass provided_obj)
		{
			return FindIndex(provided_obj.seq);
		}
		private int FindIndex(int provided_seq)
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
		private void JustAdd(CuriousResultEBaseClass provided_obj)
		{
			if(null == m_e)
			{
				m_e = new CuriousResultEBaseClass[1];
				m_e[0] = provided_obj;
			}
			else
			{
				CuriousResultEBaseClass[] templ = new CuriousResultEBaseClass[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
		}
	}
	public class CuriousBaseClass
	{
		public const string FindCategoryClassName = "FindCategoryClassName";
		public event CuriousHappenedEventHandler CuriousHappened;
		public const string ClassName = "CuriousBaseClass";
		public const string XmlElementNameSeq = "Seq";
		public const string XmlElementNameSecond = "Second";
		protected int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		protected System.Threading.Timer m_timer;
		public System.Threading.Timer timer
		{
			get
			{
				return m_timer;
			}
		}
		protected int m_second;
		public int second
		{
			get
			{
				return m_second;
			}
		}
		protected bool m_working;
		public bool working
		{
			get
			{
				return m_working;
			}
		}
		protected bool m_proceeding;
		public bool proceeding
		{
			get
			{
				return m_proceeding;
			}
		}
		public virtual string sortkey
		{
			get
			{
				return string.Empty;
			}
		}
		public CuriousBaseClass(int provided_seq)
		{
			m_seq = provided_seq;
			m_second = 1;
			m_proceeding = false;
			m_timer = new System.Threading.Timer(new System.Threading.TimerCallback(MyTick));
		}
		public CuriousBaseClass(int provided_seq, int provided_second)
		{
			m_seq = provided_seq;
			m_second = provided_second;
			m_proceeding = false;
			m_timer = new System.Threading.Timer(new System.Threading.TimerCallback(MyTick));
		}
		public CuriousBaseClass(System.Xml.XmlElement provided_element)
		{
			m_seq = int.Parse(provided_element.GetAttribute(XmlElementNameSeq));
			m_second = int.Parse(provided_element.GetAttribute(XmlElementNameSecond));
			m_proceeding = false;
			m_timer = new System.Threading.Timer(new System.Threading.TimerCallback(MyTick));
		}
		public CuriousBaseClass(CuriousBaseClass provided_obj)
		{
			m_seq = provided_obj.seq;
			m_second = provided_obj.second;
			m_proceeding = provided_obj.proceeding;
			m_timer = provided_obj.timer;
		}
		public void StartTimer()
		{
			StartTimer(m_second);
		}
		public void StartTimer(int provided_second)
		{
			m_second = provided_second;
			m_working = true;
			m_proceeding = false;
			m_timer = new System.Threading.Timer(new System.Threading.TimerCallback(MyTick));
			m_timer.Change(0, m_second * 1000);
		}
		public void EndTimer()
		{
			m_working = false;
		}
		public virtual System.Xml.XmlElement GetXml(System.Xml.XmlDocument provided_xml)
		{
			System.Xml.XmlElement ret = provided_xml.CreateElement(ClassName);
			ret.SetAttribute(XmlElementNameSeq, m_seq.ToString());
			ret.SetAttribute(XmlElementNameSecond, m_second.ToString());
			return ret;
		}
		protected void MyTick(object sender)
		{
			if(m_working)
			{
				if(m_proceeding)
				{
				}
				else
				{
					m_proceeding = true;
					CuriousResultLClass templ = Proceed();
					if(null != templ.e)
					{
						if(null != CuriousHappened)
						{
							CuriousHappened(templ);
						}
					}
					m_proceeding = false;
				}
			}
			else
			{
				System.Threading.Timer mytimer = (System.Threading.Timer)sender;
				mytimer.Dispose();
			}
		}
		public virtual CuriousResultLClass Proceed()
		{
			CuriousResultLClass ret = new CuriousResultLClass();
			return ret;
		}
		public virtual bool Match(string provided_cat, string provided_data)
		{
			return false;
		}
	}
	public class CuriousLClass
	{
		public const string ClassName = "CuriousLClass";
		CuriousBaseClass[] m_e;
		public CuriousBaseClass[] e
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
		public CuriousLClass()
		{
			m_e = null;
		}
		public CuriousLClass(CuriousLClass provided_obj)
		{
			m_e = provided_obj.e;
		}
		public CuriousLClass(System.Xml.XmlElement provided_element)
		{
			if(null != provided_element.ChildNodes)
			{
				for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
				{
					System.Xml.XmlElement mye = (System.Xml.XmlElement)(provided_element.ChildNodes[icnt]);
					switch(mye.Name)
					{
						case CuriousBaseClass.ClassName:
							{
								break;
							}
						case CuriousSelectClass.ClassName:
							{
								JustAdd(new CuriousSelectClass(mye));
								break;
							}
						default:
							{
								break;
							}
					}
				}
			}
		}
		public CuriousLClass Find(string provided_cat, string provided_data)
		{
			CuriousLClass ret = new CuriousLClass();
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					if(m_e[icnt].Match(provided_cat, provided_data))
					{
						ret = ret + m_e[icnt];
					}
				}
			}
			return ret;
		}
		public System.Xml.XmlElement GetXml(System.Xml.XmlDocument provided_xml)
		{
			System.Xml.XmlElement ret = provided_xml.CreateElement(ClassName);
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					ret.AppendChild(m_e[icnt].GetXml(provided_xml));
				}
			}
			return ret;
		}
		public static CuriousLClass operator + (CuriousLClass provided_l, CuriousBaseClass provided_e)
		{
			CuriousLClass ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static CuriousLClass operator - (CuriousLClass provided_l, CuriousBaseClass provided_e)
		{
			CuriousLClass ret = new CuriousLClass();
			int idx = provided_l.FindIndex(provided_e);
			if(null != provided_l.e)
			{
				for(int icnt = 0; icnt < provided_l.e.Length; icnt++)
				{
					if(idx != icnt)
					{
						ret = ret + provided_l.e[icnt];
					}
				}
			}
			return ret;
		}
		private int FindIndex(CuriousBaseClass provided_obj)
		{
			return FindIndex(provided_obj.seq);
		}
		private int FindIndex(int provided_seq)
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
		private void JustAdd(CuriousBaseClass provided_obj)
		{
			if(null == m_e)
			{
				m_e = new CuriousBaseClass[1];
				m_e[0] = provided_obj;
			}
			else
			{
				CuriousBaseClass[] templ = new CuriousBaseClass[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
			System.Array.Sort(m_e, new CuriousComparer());
		}
	}
}
