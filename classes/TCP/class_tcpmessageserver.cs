namespace TCP
{
	public delegate void TCPMessageAcceptedEventHandler(TCPMailMessageClass provided_mail, System.Net.Sockets.Socket provided_soc);
	public delegate void TCPFeedingMailRequestedEventHandler(TCPMessageServerClass provided_messageserver);
	public class TCPMessageServerClass : TCPServerBaseClass
	{
		public event TCPMessageAcceptedEventHandler MessageAccepted;
		public event TCPFeedingMailRequestedEventHandler FeedingMailRequested;
		protected System.Net.Sockets.Socket m_clientsoc;
		public System.Net.Sockets.Socket clientsoc
		{
			get
			{
				return m_clientsoc;
			}
		}
		protected ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		public TCPMessageServerClass(int provided_port, int provided_maxconnection, ProfileEClass provided_profile) : base(provided_port, provided_maxconnection)
		{
			m_profile = provided_profile;
		}
		public void Deliver(TCPMailMessageClass provided_mail)
		{
			SendData(m_clientsoc, provided_mail.xmlstr);
		}
		public void Close()
		{
			EndListen();
		}
		public override void EndListen()
		{
			try
			{
				m_clientsoc.Shutdown(System.Net.Sockets.SocketShutdown.Both);
				m_clientsoc.Close();
				base.EndListen();
			}
			catch{}
		}
		public void FeedMail(TCPEnvelopeLClass provided_envelopel)
		{
			TCPFeedMailMessageClass feedmail = new TCPFeedMailMessageClass(int.MinValue, provided_envelopel);
			SendData(m_clientsoc, feedmail.xmlstr);
		}
		protected override void AcceptedAndAct(System.Net.Sockets.Socket provided_fromsoc)
		{
			base.AcceptedAndAct(provided_fromsoc);
			m_clientsoc = provided_fromsoc;
			if(null != FeedingMailRequested)
			{
				FeedingMailRequested(this);
			}
		}
		protected override bool ReceivedAndAct(string provided_data, System.Net.Sockets.Socket provided_soc)
		{
			bool disposeret = base.ReceivedAndAct(provided_data, provided_soc);
			TCPMessageInterpreterClass mi = new TCPMessageInterpreterClass(provided_data);
			switch(mi.tcpmessage.command)
			{
				case TCPMailMessageClass.CommandString:
					{
						TCPMailMessageClass mymail = (TCPMailMessageClass)(mi.tcpmessage);
						if(null != MessageAccepted)
						{
							MessageAccepted(mymail, provided_soc);
						}
						break;
					}
				case TCPRequestForFeedMailMessageClass.CommandString:
					{
						TCPRequestForFeedMailMessageClass myreq = (TCPRequestForFeedMailMessageClass)(mi.tcpmessage);
						if(null != FeedingMailRequested)
						{
							FeedingMailRequested(this);
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
	public class TCPMessageServerEClass : AbstLib.EBaseClass
	{
		protected TCPMessageServerClass m_messageserver;
		public TCPMessageServerClass messageserver
		{
			get
			{
				return m_messageserver;
			}
		}
		public TCPMessageServerEClass(TCPMessageServerClass provided_messageserver) : base(provided_messageserver.port)
		{
			m_messageserver = provided_messageserver;
		}
		public TCPMessageServerEClass(TCPMessageServerEClass provided_obj) : base(provided_obj)
		{
			m_messageserver = provided_obj.messageserver;
		}
	}
	public class TCPMessageServerLClass : AbstLib.LBaseClass
	{
		public TCPMessageServerLClass() : base(){}
		public TCPMessageServerLClass(TCPMessageServerLClass provided_obj) : base(provided_obj){}
	}
}
