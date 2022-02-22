namespace IDE
{
	public class methodcompare : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			methode myx = (methode)x;
			methode myy = (methode)y;
			return string.Compare(myx.name, myy.name);
		}
	}
	public class methode
	{
		int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		public methode(int provided_seq, string provided_name, string provided_filename)
		{
			m_seq = provided_seq;
			m_name = provided_name;
			m_filename = provided_filename;
		}
		public methode(methode provided_obj)
		{
			m_seq = provided_obj.seq;
			m_name = provided_obj.name;
			m_filename = provided_obj.filename;
		}
	}
	public class methodl
	{
		methode[] m_e;
		public methode[] e
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
		public methodl()
		{
			m_e = null;
		}
		public methodl(methodl provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static methodl operator + (methodl provided_l, methode provided_e)
		{
			methodl ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static methodl operator - (methodl provided_l, methode provided_e)
		{
			methodl ret = new methodl();
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
		public methode Fetch(string provided_name)
		{
			methode ret = null;
			try
			{
				ret = m_e[FindIndex(provided_name)];
			}
			catch
			{
			}
			return ret;
		}
		private int FindIndex(methode provided_obj)
		{
			return FindIndex(provided_obj.name);
		}
		private int FindIndex(string provided_name)
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					if(provided_name.ToUpper() == m_e[icnt].name.ToUpper())
					{
						return icnt;
					}
				}
			}
			return -1;
		}
		private void JustAdd(methode provided_method)
		{
			if(null == m_e)
			{
				m_e = new methode[1];
				m_e[0] = provided_method;
			}
			else
			{
				methode[] templ = new methode[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_method;
				m_e = templ;
			}
			System.Array.Sort(m_e, new methodcompare());
		}
	}
}




