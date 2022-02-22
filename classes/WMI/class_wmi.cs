namespace WMI
{
	public class RemoteComputerClass
	{
		const string Root = "root";
		const string Cimv2 = "cimv2";
		string m_name;
		public string name
		{
			get
			{
				return m_name;
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
		System.Management.ManagementScope m_scope;
		public System.Management.ManagementScope scope
		{
			get
			{
				return m_scope;
			}
		}
		string m_yen
		{
			get
			{
				return System.IO.Path.DirectorySeparatorChar.ToString();
			}
		}
		string m_rootcimv2
		{
			get
			{
				return (Root + m_yen + Cimv2);
			}
		}
		string m_mypath
		{
			get
			{
				string ret = string.Empty;
				ret += m_yen;
				ret += m_yen;
				ret += m_name;
				ret += m_yen;
				ret += m_ns;
				return ret;
			}
		}
		string[] m_classes;
		public string[] classes
		{
			get
			{
				return m_classes;
			}
		}
		public RemoteComputerClass(string provided_name)
		{
			m_name = provided_name;
			m_ns = m_rootcimv2;
			m_scope = new System.Management.ManagementScope(m_mypath);
		}
		public RemoteComputerClass(string provided_name, string provided_ns)
		{
			m_name = provided_name;
			m_ns = provided_ns;
			m_scope = new System.Management.ManagementScope(m_mypath);
		}
		public ACC.RowLClass ExecuteQuery(string provided_wql)
		{
			ACC.RowLClass ret = new ACC.RowLClass();
			System.Management.ObjectQuery q = new System.Management.ObjectQuery(provided_wql);
			System.Management.ManagementObjectSearcher s = new System.Management.ManagementObjectSearcher(m_scope, q);
			System.Management.ManagementObjectCollection coll = s.Get();
			foreach(System.Management.ManagementObject mo in coll)
			{
				ACC.ColLClass cl = new ACC.ColLClass();
				foreach(System.Management.PropertyData prope in mo.Properties)
				{
					switch(prope.Type)
					{
						case System.Management.CimType.String:
							{
								string sval = string.Empty;
								try
								{
									sval = (mo[prope.Name.ToString()]).ToString();
								}
								catch
								{
								}
								cl += new ACC.ColEStringClass(cl.maxseq + 1, prope.Name.ToString(), sval);
								break;
							}
						case System.Management.CimType.Boolean:
							{
								bool bval = false;
								try
								{
									bval = bool.Parse((mo[prope.Name.ToString()]).ToString());
								}
								catch
								{
								}
								cl += new ACC.ColEBoolClass(cl.maxseq + 1, prope.Name.ToString(), bval);
								break;
							}
						case System.Management.CimType.SInt32:
							{
								int ival = int.MinValue;
								try
								{
									ival = int.Parse(mo[prope.Name.ToString()].ToString());
								}
								catch
								{
								}
								cl += new ACC.ColEIntClass(cl.maxseq + 1, prope.Name.ToString(), ival);
								break;
							}
						case System.Management.CimType.UInt32:
							{
								int ival = int.MinValue;
								try
								{
									ival = int.Parse(mo[prope.Name.ToString()].ToString());
								}
								catch
								{
								}
								cl += new ACC.ColEIntClass(cl.maxseq + 1, prope.Name.ToString(), ival);
								break;
							}
						case System.Management.CimType.DateTime:
							{
								DateTime dval = DateTime.MinValue;
								try
								{
									dval = DateTime.ParseExact(mo[prope.Name.ToString()].ToString().Substring(0, 14), "yyyyMMddHHmmss", null);
								}
								catch
								{
								}
								cl += new ACC.ColEDateClass(cl.maxseq + 1, prope.Name.ToString(), dval);
								break;
							}
						default:
							{
								break;
							}
					}
				}
				ret += new ACC.RowEClass(ret.maxseq + 1, cl);
			}
			return ret;
		}
		private void Initialize()
		{
			System.Management.ObjectQuery q = new System.Management.ObjectQuery("select * from meta_class");
			System.Management.ManagementObjectSearcher s = new System.Management.ManagementObjectSearcher(m_scope, q);
			System.Management.ManagementObjectCollection coll = s.Get();
			m_classes = new string[coll.Count];
			int idx = 0;
			foreach(System.Management.ManagementObject myo in coll)
			{
				m_classes[idx] = (string)((System.Management.ManagementClass)myo)["__Class"];
				idx++;
			}
			System.Array.Sort(m_classes);
		}
	}
}
