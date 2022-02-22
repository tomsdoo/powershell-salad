namespace TCP
{
	public class CommunicationClientClass
	{
		public event TCPMessageReceivedEventHandler MessageReceived;
		public event TCPServerClosedEventHandler ServerClosed;
		public event TCPProfileListReceivedEventHandler ProfileListReceived;
		public event TCPMailFedEventHandler MailFed;
		string m_server;
		public string server
		{
			get
			{
				return m_server;
			}
		}
		int m_connectionport;
		public int connectionport
		{
			get
			{
				return m_connectionport;
			}
		}
		ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		TCPConnectionClientClass m_connectionclient;
		public TCPConnectionClientClass connectionclient
		{
			get
			{
				return m_connectionclient;
			}
		}
		TCPMessageClientClass m_messageclient;
		public TCPMessageClientClass messageclient
		{
			get
			{
				return m_messageclient;
			}
		}
		public CommunicationClientClass(string provided_server, string provided_name)
		{
			m_server = provided_server;
			m_connectionport = 60000;
			m_profile = new ProfileEClass(int.MinValue, provided_name, string.Empty);
			Initialize();
		}
		public CommunicationClientClass(string provided_server, int provided_connectionport, ProfileEClass provided_profile)
		{
			m_server = provided_server;
			m_connectionport = provided_connectionport;
			m_profile = provided_profile;
			Initialize();
		}
		private void Initialize()
		{
			m_connectionclient = null;
			m_connectionclient = new TCPConnectionClientClass(m_server, m_connectionport, m_profile);
			m_connectionclient.PortTold += new TCPPortToldEventHandler(PortToldEvent);
			m_connectionclient.ProfileChanged += new TCPConnectionProfileChangedEventHandler(ProfileChangedEvent);
			m_connectionclient.ServerClosed += new TCPServerClosedEventHandler(ServerClosedEvent);
			m_connectionclient.ProfileListReceived += new TCPProfileListReceivedEventHandler(ProfileListReceivedEvent);
		}
		public void Connect()
		{
			m_connectionclient.Connect();
		}
		public void Close()
		{
			m_connectionclient.Close();
			m_messageclient.Close();
		}
		public void RequestForProfiles()
		{
			m_connectionclient.RequestForProfiles();
		}
		public void RequestForMail()
		{
			m_messageclient.RequestForMail();
		}
		private void PortToldEvent(TCPPortTellingMessageClass provided_tellingport)
		{
			m_messageclient = null;
			m_messageclient = new TCPMessageClientClass(m_server, provided_tellingport.seq, m_profile);
			m_messageclient.MessageReceived += new TCPMessageReceivedEventHandler(MessageReceivedEvent);
			m_messageclient.MailFed += new TCPMailFedEventHandler(MailFedEvent);
			m_messageclient.Connect();
		}
		private void MessageReceivedEvent(TCPMailMessageClass provided_mail)
		{
			if(null != MessageReceived)
			{
				MessageReceived(provided_mail);
			}
		}
		private void ProfileChangedEvent(ProfileEClass provided_profile)
		{
			m_profile = provided_profile;
		}
		private void ServerClosedEvent()
		{
			Close();
			if(null != ServerClosed)
			{
				ServerClosed();
			}
		}
		private void ProfileListReceivedEvent(ProfileLClass provided_profilel)
		{
			if(null != ProfileListReceived)
			{
				ProfileListReceived(provided_profilel);
			}
		}
		private void MailFedEvent(TCPFeedMailMessageClass provided_feedmail)
		{
			if(null != MailFed)
			{
				MailFed(provided_feedmail);
			}
		}
	}
}
