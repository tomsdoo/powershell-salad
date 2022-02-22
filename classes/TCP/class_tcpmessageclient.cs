namespace TCP
{
	public delegate void TCPMessageReceivedEventHandler(TCPMailMessageClass provided_mail);
	public delegate void TCPMailFedEventHandler(TCPFeedMailMessageClass provided_feedmail);
	public class TCPMessageClientClass : TCPClientBaseClass
	{
		public event TCPMessageReceivedEventHandler MessageReceived;
		public event TCPMailFedEventHandler MailFed;
		protected ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		protected TCPEnvelopeLClass m_envelopel;
		public TCPEnvelopeLClass envelopel
		{
			get
			{
				return m_envelopel;
			}
		}
		public TCPMessageClientClass(string provided_server, int provided_port, ProfileEClass provided_profile) : base(provided_server, provided_port)
		{
			m_profile = provided_profile;
			m_envelopel = new TCPEnvelopeLClass();
		}
		public void RequestForMail()
		{
			TCPRequestForFeedMailMessageClass req = new TCPRequestForFeedMailMessageClass(int.MinValue);
			SendData(req.xmlstr);
		}
		public void SendMessage(string provided_message)
		{
			TCPEnvelopeClass envelope = new TCPEnvelopeClass(int.MinValue, m_profile, provided_message);
			TCPMailMessageClass mail = new TCPMailMessageClass(int.MinValue, m_profile, envelope);
			SendData(mail.xmlstr);
		}
		protected override bool ReceivedAndAct(string provided_data, System.Net.Sockets.Socket provided_soc)
		{
			TCPMessageInterpreterClass mi = new TCPMessageInterpreterClass(provided_data);
			switch(mi.tcpmessage.command)
			{
				case TCPMailMessageClass.CommandString:
					{
						TCPMailMessageClass mail = (TCPMailMessageClass)(mi.tcpmessage);
						m_envelopel = (TCPEnvelopeLClass)(m_envelopel + mail.envelope);
						if(null != MessageReceived)
						{
							MessageReceived(mail);
						}
						break;
					}
				case TCPFeedMailMessageClass.CommandString:
					{
						TCPFeedMailMessageClass feedmail = (TCPFeedMailMessageClass)(mi.tcpmessage);
						m_envelopel = feedmail.envelopel;
						if(null != MailFed)
						{
							MailFed(feedmail);
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
