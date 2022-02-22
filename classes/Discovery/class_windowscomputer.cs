namespace Discovery
{
	public delegate void DiscoveryCompletedEventHandler(object provided_obj);
	public class WindowsComputerClass
	{
		public event DiscoveryCompletedEventHandler DiscoveryCompleted;
		string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		string m_pingaddress;
		public string pingaddress
		{
			get
			{
				return m_pingaddress;
			}
		}
		string m_pingstatus;
		public string pingstatus
		{
			get
			{
				return m_pingstatus;
			}
		}
		string m_computername;
		public string computername
		{
			get
			{
				return m_computername;
			}
		}
		bool m_computerexists;
		public  bool computerexists
		{
			get
			{
				return m_computerexists;
			}
		}
		bool m_canreadregistry;
		public bool canreadregistry
		{
			get
			{
				return m_canreadregistry;
			}
		}
		bool m_discovered;
		public bool discovered
		{
			get
			{
				return m_discovered;
			}
		}
		TCP.BGPingClass m_ping;
		WMI.BGOSClass m_os;
		Registry.BGRegClass m_reg;
		public WindowsComputerClass(string provided_name)
		{
			m_discovered = false;
			m_name = provided_name;
			m_pingaddress = string.Empty;
			m_pingstatus = string.Empty;
			m_computername = string.Empty;
			m_computerexists = false;
			m_canreadregistry = false;
			m_ping = null;
			m_os = null;
			m_reg = null;
		}
		public void Initialize()
		{
			m_ping = new TCP.BGPingClass(m_name);
			m_ping.Completed += new AbstLib.MyCompletedEventHandler(PingCompleted);
			m_ping.DoIt(null);

			m_os = new WMI.BGOSClass(m_name);
			m_os.Completed += new AbstLib.MyCompletedEventHandler(OSCompleted);
			m_os.DoIt(null);
		}
		private void PingCompleted(object provided_obj)
		{
			try
			{
				m_pingaddress = m_ping.reply.Address.ToString();
			}
			catch{}
			m_pingstatus = m_ping.reply.Status.ToString();
			CalculateCompletionStatus();
		}
		private void OSCompleted(object provided_obj)
		{
			m_computername = m_os.computername;
			m_computerexists = m_os.bexists;
			if(m_computerexists)
			{
				m_reg = new Registry.BGRegClass(m_computername);
				m_reg.Completed += new AbstLib.MyCompletedEventHandler(RegCompleted);
				m_reg.DoIt(null);
			}
			else
			{
				CalculateCompletionStatus();
			}
		}
		private void RegCompleted(object provided_obj)
		{
			m_canreadregistry = m_reg.bcanread;
			CalculateCompletionStatus();
		}
		private void CalculateCompletionStatus()
		{
			if(!m_discovered)
			{
				bool bok = true;
				if(null != m_ping)
				{
					bok = bok & (m_ping.resultstatus == "Completed");
				}
				if(null != m_os)
				{
					bok = bok & (m_os.resultstatus == "Completed");
				}
				if(null != m_reg)
				{
					bok = bok & (m_reg.resultstatus == "Completed");
				}
				m_discovered = bok;
				if(m_discovered)
				{
					if(null != DiscoveryCompleted)
					{
						DiscoveryCompleted(this);
					}
				}
			}
		}
	}
}
