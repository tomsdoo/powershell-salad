namespace IDE
{
	public class classcompare : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			classe myx = (classe)x;
			classe myy = (classe)y;
			return string.Compare(myx.name, myy.name);
		}
	}
	public class classe
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
		methodl m_methods;
		public methodl methods
		{
			get
			{
				return m_methods;
			}
		}
		public classe(int provided_seq, string provided_name)
		{
			m_seq = provided_seq;
			m_name = provided_name;
			m_methods = new methodl();
		}
		public classe(classe provided_obj)
		{
			m_seq = provided_obj.seq;
			m_name = provided_obj.name;
			m_methods = provided_obj.methods;
		}
		public static classe operator + (classe provided_classe, methode provided_method)
		{
			classe ret = provided_classe;
			ret.AddMethod(provided_method);
			return ret;
		}
		private void AddMethod(methode provided_method)
		{
			m_methods = m_methods + provided_method;
		}
	}
	public class classl
	{
		classe[] m_e;
		public classe[] e
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
		public classl()
		{
			m_e = null;
		}
		public classl(classl provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static classl operator + (classl provided_l, classe provided_e)
		{
			classl ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static classl operator - (classl provided_l, classe provided_e)
		{
			classl ret = new classl();
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
		public classe Fetch(string provided_name)
		{
			classe ret = null;
			try
			{
				ret = m_e[FindIndex(provided_name)];
			}
			catch
			{
			}
			return ret;
		}
		private int FindIndex(classe provided_obj)
		{
			return FindIndex(provided_obj.name);
		}
		private int FindIndex(string provided_name)
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					if(m_e[icnt].name.ToUpper() == provided_name.ToUpper())
					{
						return icnt;
					}
				}
			}
			return -1;
		}
		private void JustAdd(classe provided_obj)
		{
			if(null == m_e)
			{
				m_e = new classe[1];
				m_e[0] = provided_obj;
			}
			else
			{
				classe[] templ = new classe[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
			System.Array.Sort(m_e, new classcompare());
		}
	}
}
