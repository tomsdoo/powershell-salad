namespace ACC
{
	public class ColEBaseClass
	{
		const string ClassName = "ColEBaseClass";
		protected int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		protected string m_classname;
		public string classname
		{
			get
			{
				return m_classname;
			}
		}
		protected string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		public virtual string valuestr
		{
			get
			{
				return string.Empty;
			}
		}
		public ColEBaseClass(int provided_seq, string provided_name)
		{
			m_seq = provided_seq;
			m_name = provided_name;
			m_classname = ClassName;
		}
		public ColEBaseClass(ColEBaseClass provided_obj)
		{
			m_seq = provided_obj.seq;
			m_name = provided_obj.name;
			m_classname = provided_obj.classname;
		}
		public static bool operator == (ColEBaseClass provided_obj1, ColEBaseClass provided_obj2)
		{
			return (provided_obj1.valuestr == provided_obj2.valuestr);
		}
		public static bool operator != (ColEBaseClass provided_obj1, ColEBaseClass provided_obj2)
		{
			return (!(provided_obj1 == provided_obj2));
		}
		public override bool Equals(object provided_obj)
		{
			bool ret = false;
			try
			{
				ret = (valuestr == ((ColEBaseClass)provided_obj).valuestr);
			}
			catch
			{
			}
			return ret;
		}
		public override int GetHashCode()
		{
			return m_seq;
		}
	}
	public class ColEBoolClass : ColEBaseClass
	{
		public const string ClassName = "ColEBoolClass";
		public const string TypeName = "Boolean";
		bool m_value;
		public bool value
		{
			get
			{
				return m_value;
			}
		}
		public override string valuestr
		{
			get
			{
				return m_value.ToString();
			}
		}
		public ColEBoolClass(int provided_seq, string provided_name, bool provided_value) : base(provided_seq, provided_name)
		{
			m_classname = ClassName;
			m_value = provided_value;
		}
		public ColEBoolClass(ColEBoolClass provided_obj) : base(provided_obj)
		{
			m_classname = ClassName;
			m_value = provided_obj.value;
		}
		public override string ToString()
		{
			return m_value.ToString();
		}
	}
	public class ColEIntClass : ColEBaseClass
	{
		public const string ClassName = "ColEIntClass";
		public const string TypeName = "Int32";
		int m_value;
		public int value
		{
			get
			{
				return m_value;
			}
		}
		public override string valuestr
		{
			get
			{
				string ret = string.Empty;
				ret = m_value.ToString();
				return ret;
			}
		}
		public ColEIntClass(int provided_seq, string provided_name, int provided_value) : base(provided_seq, provided_name)
		{
			m_classname = ClassName;
			m_value = provided_value;
		}
		public ColEIntClass(ColEIntClass provided_obj): base(provided_obj)
		{
			m_classname = ClassName;
			m_value = provided_obj.value;
		}
		public override string ToString()
		{
			return m_value.ToString();
		}
	}
	public class ColEDateClass : ColEBaseClass
	{
		public const string ClassName = "ColEDateClass";
		public const string TypeName = "DateTime";
		DateTime m_value;
		public DateTime value
		{
			get
			{
				return m_value;
			}
		}
		public override string valuestr
		{
			get
			{
				string ret = string.Empty;
				try
				{
					ret = m_value.ToString("yyyy/MM/dd HH:mm:ss");
				}
				catch
				{
				}
				return ret;
			}
		}
		public ColEDateClass(int provided_seq, string provided_name, DateTime provided_value) : base(provided_seq, provided_name)
		{
			m_classname = ClassName;
			m_value = provided_value;
		}
		public ColEDateClass(ColEDateClass provided_obj) : base(provided_obj)
		{
			m_classname = ClassName;
			m_value = provided_obj.value;
		}
		public override string ToString()
		{
			return valuestr;
		}
	}
	public class ColEStringClass : ColEBaseClass
	{
		public const string ClassName = "ColEStringClass";
		public const string TypeName = "String";
		string m_value;
		public string value
		{
			get
			{
				return m_value;
			}
		}
		public override string valuestr
		{
			get
			{
				string ret = string.Empty;
				ret = m_value.ToString();
				return ret;
			}
		}
		public ColEStringClass(int provided_seq, string provided_name, string provided_value) : base(provided_seq, provided_name)
		{
			m_classname = ClassName;
			m_value = provided_value;
		}
		public ColEStringClass(ColEStringClass provided_obj) : base(provided_obj)
		{
			m_classname = ClassName;
			m_value = provided_obj.value;
		}
		public override string ToString()
		{
			return m_value.ToString();
		}
	}
	public class ColLClass
	{
		ColEBaseClass[] m_e;
		public ColEBaseClass[] e
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
		public ColEBaseClass this[string provided_name]
		{
			get
			{
				ColEBaseClass ret = null;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						if(provided_name.ToUpper() == m_e[icnt].name.ToUpper())
						{
							return m_e[icnt];
						}
					}
				}
				return ret;
			}
		}
		public string[] names
		{
			get
			{
				string[] ret = null;
				if(null != m_e)
				{
					ret = new string[m_e.Length];
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret[icnt] = m_e[icnt].name;
					}
				}
				return ret;
			}
		}
		public ColLClass()
		{
			m_e = null;
		}
		public ColLClass(ColLClass provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static ColLClass operator + (ColLClass provided_l, ColEBaseClass provided_e)
		{
			ColLClass ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static ColLClass operator - (ColLClass provided_l, ColEBaseClass provided_e)
		{
			ColLClass ret = new ColLClass();
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
		public static bool operator == (ColLClass provided_obj1, ColLClass provided_obj2)
		{
			bool ret = true;
			try
			{
				foreach(string namee in provided_obj1.names)
				{
					ret = ret && (provided_obj1[namee] == provided_obj2[namee]);
				}
			}
			catch
			{
			}
			return ret;
		}
		public static bool operator != (ColLClass provided_obj1, ColLClass provided_obj2)
		{
			return (!(provided_obj1 == provided_obj2));
		}
		public override bool Equals(object provided_obj)
		{
			return ((ColLClass)provided_obj == this);
		}
		public override int GetHashCode()
		{
			return maxseq;
		}
		private int FindIndex(ColEBaseClass provided_obj)
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
		private void JustAdd(ColEBaseClass provided_obj)
		{
			if(null == m_e)
			{
				m_e = new ColEBaseClass[1];
				m_e[0] = provided_obj;
			}
			else
			{
				ColEBaseClass[] templ = new ColEBaseClass[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
		}
	}
	public class RowEClass
	{
		int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		ColLClass m_coll;
		public ColLClass coll
		{
			get
			{
				return m_coll;
			}
		}
		public System.Data.DataTable table
		{
			get
			{
				System.Data.DataTable ret = null;
				if(null != m_coll.e)
				{
					ret = new System.Data.DataTable();
					ret.Columns.Add("Name");
					ret.Columns.Add("Value");
					for(int icnt = 0; icnt < m_coll.e.Length; icnt++)
					{
						object[] myo = new object[2];
						myo[0] = m_coll.e[icnt].name;
						myo[1] = m_coll.e[icnt].ToString();
						ret.Rows.Add(myo);
					}
					ret.DefaultView.AllowNew = false;
					ret.DefaultView.AllowDelete = false;
					ret.DefaultView.AllowEdit = false;
				}
				return ret;
			}
		}
		public ColEBaseClass this[string provided_name]
		{
			get
			{
				ColEBaseClass ret = null;
				ret = m_coll[provided_name];
				return ret;
			}
		}
		public RowEClass(int provided_seq, ColLClass provided_coll)
		{
			m_seq = provided_seq;
			m_coll = provided_coll;
		}
		public RowEClass(RowEClass provided_obj)
		{
			m_seq = provided_obj.seq;
			m_coll = provided_obj.coll;
		}
		public string[] GetDifferentColumnNames(RowEClass provided_obj)
		{
			ColLClass tempr = new ColLClass();
			if(null != m_coll.e)
			{
				for(int icnt = 0; icnt < m_coll.e.Length; icnt++)
				{
					if(m_coll.e[icnt].ToString() != provided_obj[m_coll.e[icnt].name].ToString())
					{
						tempr += new ColEBaseClass(tempr.maxseq + 1, m_coll.e[icnt].name);
					}
				}
			}
			string[] ret = null;
			if(null != tempr.e)
			{
				ret = new string[tempr.e.Length];
				for(int jcnt = 0; jcnt < tempr.e.Length; jcnt++)
				{
					ret[jcnt] = tempr.e[jcnt].name;
				}
			}
			return ret;
		}
		public static bool operator == (RowEClass provided_obj1, RowEClass provided_obj2)
		{
			object po1 = (object)provided_obj1;
			object po2 = (object)provided_obj2;
			if((null == po1) && (null == po2))
			{
				return true;
			}
			else
			{
				if((null == po1) || (null == po2))
				{
					return false;
				}
				else
				{
					return (((RowEClass)provided_obj1).coll == ((RowEClass)provided_obj2).coll);
				}
			}
		}
		public static bool operator != (RowEClass provided_obj1, RowEClass provided_obj2)
		{
			return (!(provided_obj1 == provided_obj2));
		}
		public override bool Equals(object provided_obj)
		{
			if(null == provided_obj)
			{
				return false;
			}
			else
			{
				return (m_coll == ((RowEClass)provided_obj).coll);
			}
		}
		public override int GetHashCode()
		{
			return m_seq;
		}
	}
	public class RowLClass
	{
		RowEClass[] m_e;
		public RowEClass[] e
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
		public System.Data.DataTable table
		{
			get
			{
				System.Data.DataTable ret = null;
				if(null != m_e)
				{
					ret = new System.Data.DataTable();
					foreach(string myname in m_e[0].coll.names)
					{
						ret.Columns.Add(myname);
					}
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						RowEClass r = m_e[icnt];
						if(null != r.coll.e)
						{
							object[] myo = new object[r.coll.e.Length];
							if(null != r.coll.e)
							{
								for(int jcnt = 0; jcnt < r.coll.e.Length; jcnt++)
								{
									myo[jcnt] = r.coll.e[jcnt].ToString();
								}
							}
							ret.Rows.Add(myo);
						}
					}
					ret.DefaultView.AllowNew = false;
					ret.DefaultView.AllowDelete = false;
					ret.DefaultView.AllowEdit = false;
				}
				return ret;
			}
		}
		public RowLClass()
		{
			m_e = null;
		}
		public RowLClass(RowLClass provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static RowLClass operator + (RowLClass provided_l, RowEClass provided_e)
		{
			RowLClass ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static RowLClass operator - (RowLClass provided_l, RowEClass provided_e)
		{
			RowLClass ret = new RowLClass();
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
		public RowLClass Select(string provided_columnname, string provided_data)
		{
			RowLClass ret = new RowLClass();
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					RowEClass temprow = m_e[icnt];
					ColLClass tempcoll = temprow.coll;
					if(null != tempcoll.e)
					{
						for(int jcnt = 0; jcnt < tempcoll.e.Length; jcnt++)
						{
							if(tempcoll.e[jcnt].name.ToUpper() == provided_columnname.ToUpper())
							{
								if(tempcoll.e[jcnt].ToString().ToUpper() == provided_data.ToUpper())
								{
									ret += temprow;
								}
							}
						}
					}
				}
			}
			return ret;
		}
		public RowEClass Find(RowEClass provided_obj)
		{
			RowEClass ret = null;
			try
			{
				ret = m_e[FindIndex(provided_obj)];
			}
			catch
			{
			}
			return ret;
		}
		private int FindIndex(RowEClass provided_obj)
		{
			try
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					if(provided_obj == m_e[icnt])
					{
						return icnt;
					}
				}
			}
			catch
			{
				return -1;
			}
			return -1;
		}
		private void JustAdd(RowEClass provided_obj)
		{
			if(null == m_e)
			{
				m_e = new RowEClass[1];
				m_e[0] = provided_obj;
			}
			else
			{
				RowEClass[] templ = new RowEClass[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
		}
	}
}
