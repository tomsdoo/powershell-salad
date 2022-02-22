namespace IDE
{
	public class functioncompare : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			functione myx = (functione)x;
			functione myy = (functione)y;
			return string.Compare(myx.name, myy.name);
		}
	}
	public class functione
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
		public functione(int provided_seq, string provided_name, string provided_filename)
		{
			m_seq = provided_seq;
			m_name = provided_name;
			m_filename = provided_filename;
		}
		public functione(functione provided_obj)
		{
			m_seq = provided_obj.seq;
			m_name = provided_obj.name;
			m_filename = provided_obj.filename;
		}
	}
	public class functionl
	{
		
		functione[] m_e;
		public functione[] e
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
		public functionl()
		{
			m_e = null;
		}
		public functionl(functionl provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static functionl operator + (functionl provided_l, functione provided_e)
		{
			functionl ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static functionl operator - (functionl provided_l, functione provided_e)
		{
			functionl ret = new functionl();
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
		public functione Fetch(string provided_name)
		{
			functione ret = null;
			try
			{
				ret = m_e[FindIndex(provided_name)];
			}
			catch
			{
			}
			return ret;
		}
		private int FindIndex(functione provided_obj)
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
		private void JustAdd(functione provided_obj)
		{
			
			if(null == m_e)
			{
				m_e = new functione[1];
				m_e[0] = provided_obj;
			}
			else
			{
				functione[] templ = new functione[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
			System.Array.Sort(m_e, new functioncompare());
		}
	}
}
