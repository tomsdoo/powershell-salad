namespace TCP
{
	public class MyIP
	{
		System.Net.IPAddress m_ip;
		public System.Net.IPAddress ip
		{
			get
			{
				return m_ip;
			}
		}
		System.Byte[] m_addressbytes;
		public System.Byte[] addressbytes
		{
			get
			{
				return m_addressbytes;
			}
		}
		public MyIP(System.Net.IPAddress provided_ip)
		{
			m_ip = provided_ip;
			m_addressbytes = m_ip.GetAddressBytes();
		}
		public MyIP(string provided_str)
		{
			m_ip = System.Net.IPAddress.Parse(provided_str);
			m_addressbytes = m_ip.GetAddressBytes();
		}
		public MyIP(System.Byte[] provided_bytes)
		{
			m_ip = new System.Net.IPAddress(provided_bytes);
			m_addressbytes = m_ip.GetAddressBytes();
		}
		public MyIP(MyIP provided_obj)
		{
			m_ip = provided_obj.ip;
			m_addressbytes = provided_obj.addressbytes;
		}
		public static MyIP operator + (MyIP provided_obj, int provided_i)
		{
			MyIP ret = provided_obj;
			ret.JustAdd(provided_i);
			return ret;
		}
		public static MyIP operator & (MyIP provided_obj1, MyIP provided_obj2)
		{
			System.Byte[] newbytes = new System.Byte[4];
			for(int icnt = 0; icnt < newbytes.Length; icnt++)
			{
				newbytes[icnt] = (System.Byte)(provided_obj1.addressbytes[icnt] & provided_obj2.addressbytes[icnt]);
			}
			return new MyIP(newbytes);
		}
		public static MyIP operator ^ (MyIP provided_obj1, MyIP provided_obj2)
		{
			System.Byte[] newbytes = new System.Byte[4];
			for(int icnt = 0; icnt < newbytes.Length; icnt++)
			{
				newbytes[icnt] = (System.Byte)(provided_obj1.addressbytes[icnt] ^ provided_obj2.addressbytes[icnt]);
			}
			return new MyIP(newbytes);
		}
		public static MyIP operator | (MyIP provided_obj1, MyIP provided_obj2)
		{
			System.Byte[] newbytes = new System.Byte[4];
			for(int icnt = 0; icnt < newbytes.Length; icnt++)
			{
				newbytes[icnt] = (System.Byte)(provided_obj1.addressbytes[icnt] | provided_obj2.addressbytes[icnt]);
			}
			return new MyIP(newbytes);
		}
		public static bool operator < (MyIP provided_obj1, MyIP provided_obj2)
		{
			for(int icnt = 0; icnt < 4; icnt++)
			{
				if(provided_obj1.addressbytes[icnt] != provided_obj2.addressbytes[icnt])
				{
					return (provided_obj1.addressbytes[icnt] < provided_obj2.addressbytes[icnt]);
				}
			}
			return false;
		}
		public static bool operator > (MyIP provided_obj1, MyIP provided_obj2)
		{
			for(int icnt = 0; icnt < 4; icnt++)
			{
				if(provided_obj1.addressbytes[icnt] != provided_obj2.addressbytes[icnt])
				{
					return (provided_obj1.addressbytes[icnt] > provided_obj2.addressbytes[icnt]);
				}
			}
			return false;
		}
		private void JustAdd(int provided_i)
		{
			int[] il = new int[4];
			for(int icnt = 0; icnt < il.Length; icnt++)
			{
				il[icnt] = (int)m_addressbytes[icnt];
			}
			int maxv = (int)(System.Byte.MaxValue);
			il[3] = il[3] + provided_i;
			if(il[3] > maxv)
			{
				il[3] -= maxv;
				il[2]++;
				if(il[2] > maxv)
				{
					il[2] -= maxv;
					il[1]++;
					if(il[1] > maxv)
					{
						il[1] -= maxv;
						il[0]++;
					}
				}
			}
			for(int jcnt = 0; jcnt < il.Length; jcnt++)
			{
				m_addressbytes[jcnt] = (System.Byte)(il[jcnt]);
			}
			m_ip = new System.Net.IPAddress(m_addressbytes);
		}
		public override string ToString()
		{
			return m_ip.ToString();
		}
	}
	public class MyIPEnvClass
	{
		System.Net.IPAddress m_ipaddress;
		public System.Net.IPAddress ipaddress
		{
			get
			{
				return m_ipaddress;
			}
		}
		System.Net.IPHostEntry m_entry;
		string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		System.Net.IPAddress m_gateway;
		public System.Net.IPAddress gateway
		{
			get
			{
				return m_gateway;
			}
		}
		System.Net.IPAddress m_subnet;
		public System.Net.IPAddress subnet
		{
			get
			{
				return m_subnet;
			}
		}
		string[] m_friendipl;
		public string[] friendipl
		{
			get
			{
				return m_friendipl;
			}
		}
		public MyIPEnvClass()
		{
			System.Net.IPHostEntry h = System.Net.Dns.GetHostEntry("localhost");
			m_entry = System.Net.Dns.GetHostEntry(h.HostName);
			m_name = m_entry.HostName;
			foreach(System.Net.IPAddress addr in m_entry.AddressList)
			{
				if(System.Net.Sockets.AddressFamily.InterNetwork == addr.AddressFamily)
				{
					m_ipaddress = addr;
				}
			}
			Initialize();
		}
		private void Initialize()
		{
			System.Management.ManagementScope scope = new System.Management.ManagementScope("\\\\" + m_name + "\\root\\cimv2");
			System.Management.ObjectQuery oq = new System.Management.ObjectQuery("select * from Win32_NetworkAdapterConfiguration where IPEnabled = true");
			System.Management.ManagementObjectSearcher os = new System.Management.ManagementObjectSearcher(scope, oq);
			System.Management.ManagementObjectCollection coll = os.Get();
			foreach(System.Management.ManagementObject mo in coll)
			{
				foreach(System.Management.PropertyData prope in mo.Properties)
				{
					switch(prope.Name.ToString().ToUpper())
					{
						case "DEFAULTIPGATEWAY":
							{
								string[] gateways = (string[])(mo[prope.Name.ToString()]);
								foreach(string ge in gateways)
								{
									if(!string.IsNullOrEmpty(ge))
									{
										if(-1 != ge.IndexOf("."))
										{
											m_gateway = System.Net.IPAddress.Parse(ge);
										}
									}
								}
								break;
							}
						case "IPSUBNET":
							{
								string[] subnets = (string[])(mo[prope.Name.ToString()]);
								foreach(string se in subnets)
								{
									if(!string.IsNullOrEmpty(se))
									{
										if(-1 != se.IndexOf("."))
										{
											m_subnet = System.Net.IPAddress.Parse(se);
										}
									}
								}
								break;
							}
						default:
							{
								break;
							}
					}
					
				}
			}
			System.Net.IPAddress alloneip = System.Net.IPAddress.Parse("255.255.255.255");
			MyIP allone = new MyIP(alloneip);
			MyIP xorip = allone ^ (new MyIP(m_subnet));
			System.Net.IPAddress allzeroip = System.Net.IPAddress.Parse("0.0.0.0");
			MyIP allzero = new MyIP(allzeroip);
			MyIP nowaip = allzero;
			MyIP baseip = (new MyIP(m_subnet)) & (new MyIP(m_gateway));
			m_friendipl = null;
			while(true)
			{
				MyIP currfriend = baseip | nowaip;
				string currfriendstr = currfriend.ToString();
				if(null == m_friendipl)
				{
					m_friendipl = new string[1];
					m_friendipl[0] = currfriendstr;
				}
				else
				{
					string[] templ = new string[m_friendipl.Length + 1];
					for(int icnt = 0; icnt < m_friendipl.Length; icnt++)
					{
						templ[icnt] = m_friendipl[icnt];
					}
					templ[templ.Length - 1] = currfriendstr;
					m_friendipl = templ;
				}

				if(nowaip.ToString() == xorip.ToString())
				{
					break;
				}
				nowaip = nowaip + 1;
			}
		}
	}
}
