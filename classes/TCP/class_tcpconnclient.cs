namespace TCP
{
	public delegate void TCPPortToldEventHandler(TCPPortTellingMessageClass provided_tellingport);
	public delegate void TCPConnectionProfileChangedEventHandler(ProfileEClass provided_profile);
	public delegate void TCPServerClosedEventHandler();
	public delegate void TCPProfileListReceivedEventHandler(ProfileLClass provided_profilel);
	public class TCPConnectionClientClass : TCPClientBaseClass
	{
		public event TCPPortToldEventHandler PortTold;
		public event TCPConnectionProfileChangedEventHandler ProfileChanged;
		public event TCPServerClosedEventHandler ServerClosed;
		public event TCPProfileListReceivedEventHandler ProfileListReceived;
		protected ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		public TCPConnectionClientClass(string provided_server, int provided_port, ProfileEClass provided_profile) : base(provided_server, provided_port)
		{
			m_profile = provided_profile;
		}
		public void RequestForProfiles()
		{
			TCPRequestForProfilesMessageClass request = new TCPRequestForProfilesMessageClass(int.MinValue);
			SendData(request.xmlstr);
		}
		public override void Close()
		{
			try
			{
				TCPCloseMessageClass closem = new TCPCloseMessageClass(m_profile.seq, m_profile);
				SendData(closem.xmlstr);
			}
			catch{}
			base.Close();
		}
		protected override bool ReceivedAndAct(string provided_data, System.Net.Sockets.Socket provided_soc)
		{
			TCPMessageInterpreterClass mi = new TCPMessageInterpreterClass(provided_data);
			switch(mi.tcpmessage.command)
			{
				case TCPWhoAreYouMessageClass.CommandString:
					{
						m_profile = new ProfileEClass(mi.tcpmessage.seq, m_profile.name, m_profile.note);
						if(null != ProfileChanged)
						{
							ProfileChanged(m_profile);
						}
						TCPMyNameIsMessageClass mynameis = new TCPMyNameIsMessageClass(mi.tcpmessage.seq, m_profile);
						SendData(provided_soc, mynameis.xmlstr);
						break;
					}
				case TCPACKMessageClass.CommandString:
					{
						break;
					}
				case TCPServerCloseMessageClass.CommandString:
					{
						if(null != ServerClosed)
						{
							ServerClosed();
						}
						break;
					}
				case TCPProfileListMessageClass.CommandString:
					{
						TCPProfileListMessageClass profm = (TCPProfileListMessageClass)(mi.tcpmessage);
						if(null != ProfileListReceived)
						{
							ProfileListReceived(profm.profilel);
						}
						break;
					}
				case TCPPortTellingMessageClass.CommandString:
					{
						if(null != PortTold)
						{
							PortTold((TCPPortTellingMessageClass)(mi.tcpmessage));
						}
						break;
					}
				default:
					{
						break;
					}
			}
			return true;
		}
	}
}
