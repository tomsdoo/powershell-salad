namespace TCP
{
	public class CommunicationServerClass
	{
		public event TCPServerBaseAcceptStartedEventHandler AcceptStarted;
		public event TCPServerBaseListenStartedEventHandler ListenStarted;
		public event TCPServerBaseListenEndedEventHandler ListenEnded;
		public event TCPServerBaseConnectionAcceptedEventHandler BaseConnectionAccepted;
		public event TCPServerBaseMessageReceivedEventHandler BaseMessageReceived;
		public event TCPConnectionAcceptedEventHandler ConnectionAccepted;
		public event TCPConnectionClosedEventHandler ConnectionClosed;
		public event TCPConnectionClosingFailedEventHandler ConnectionClosingFailed;
		public event TCPConnectionTellingPortSucceededEventHandler ConnectionTellingPortSucceeded;
		public event TCPConnectionTellingPortFailedEventHandler ConnectionTellingPortFailed;
		public event TCPMessageAcceptedEventHandler MessageAccepted;
		TCPConnectionServerClass m_connectionserver;
		public TCPConnectionServerClass connectionserver
		{
			get
			{
				return m_connectionserver;
			}
		}
		TCPMessageServerLClass m_messageserverl;
		public TCPMessageServerLClass messageserverl
		{
			get
			{
				return m_messageserverl;
			}
		}
		TCPEnvelopeLClass m_envelopel;
		public TCPEnvelopeLClass envelopel
		{
			get
			{
				return m_envelopel;
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
		int m_maxconnectioncount;
		public int maxconnectioncount
		{
			get
			{
				return m_maxconnectioncount;
			}
		}
		bool m_bmessagebusy;
		public CommunicationServerClass(int provided_connectionport, int provided_maxconnectioncount)
		{
			m_bmessagebusy = false;
			m_messageserverl = new TCPMessageServerLClass();
			m_envelopel = new TCPEnvelopeLClass();
			m_connectionport = provided_connectionport;
			m_maxconnectioncount = provided_maxconnectioncount;
			m_connectionserver = new TCPConnectionServerClass(m_connectionport, m_maxconnectioncount);
			m_connectionserver.AcceptStarted += new TCPServerBaseAcceptStartedEventHandler(AcceptStartedEvent);
			m_connectionserver.ListenStarted += new TCPServerBaseListenStartedEventHandler(ListenStartedEvent);
			m_connectionserver.ListenEnded += new TCPServerBaseListenEndedEventHandler(ListenEndedEvent);
			m_connectionserver.BaseConnectionAccepted += new TCPServerBaseConnectionAcceptedEventHandler(BaseConnectionAcceptedEvent);
			m_connectionserver.BaseMessageReceived += new TCPServerBaseMessageReceivedEventHandler(BaseMessageReceivedEvent);
			m_connectionserver.ConnectionAccepted += new TCPConnectionAcceptedEventHandler(ConnectionAcceptedEvent);
			m_connectionserver.ConnectionClosed += new TCPConnectionClosedEventHandler(ConnectionClosedEvent);
			m_connectionserver.ClosingFailed += new TCPConnectionClosingFailedEventHandler(ConnectionClosingFailedEvent);
			m_connectionserver.TellingPortSucceeded += new TCPConnectionTellingPortSucceededEventHandler(ConnectionTellingPortSucceededEvent);
			m_connectionserver.TellingPortFailed += new TCPConnectionTellingPortFailedEventHandler(ConnectionTellingPortFailedEvent);
		}
		public void Open()
		{
			m_connectionserver.StartListen();
			m_connectionserver.StartAccept();
		}
		public void Close()
		{
			m_connectionserver.Close();
			if(null != m_messageserverl.e)
			{
				for(int icnt = 0; icnt < m_messageserverl.e.Length; icnt++)
				{
					TCPMessageServerEClass messageservere = (TCPMessageServerEClass)(m_messageserverl.e[icnt]);
					messageservere.messageserver.Close();
				}
			}
		}
		private void AcceptStartedEvent(TCPServerBaseClass provided_baseserver)
		{
			if(null != AcceptStarted)
			{
				AcceptStarted(provided_baseserver);
			}
		}
		private void ListenStartedEvent(TCPServerBaseClass provided_baseserver)
		{
			if(null != ListenStarted)
			{
				ListenStarted(provided_baseserver);
			}
		}
		private void ListenEndedEvent(TCPServerBaseClass provided_baseserver)
		{
			if(null != ListenEnded)
			{
				ListenEnded(provided_baseserver);
			}
		}
		private void BaseConnectionAcceptedEvent(TCPServerBaseClass provided_baseserver)
		{
			if(null != BaseConnectionAccepted)
			{
				BaseConnectionAccepted(provided_baseserver);
			}
		}
		private void BaseMessageReceivedEvent(TCPServerBaseClass provided_baseserver, string provided_data)
		{
			if(null != BaseMessageReceived)
			{
				BaseMessageReceived(provided_baseserver, provided_data);
			}
		}
		
		private void ConnectionAcceptedEvent(TCPMyNameIsMessageClass provided_mynameis, System.Net.Sockets.Socket provided_soc)
		{
			int port = m_messageserverl.maxseq + 1;
			if(1 == port)
			{
				port = m_connectionport + 1;
			}
			if(null != ConnectionAccepted)
			{
				ConnectionAccepted(provided_mynameis, provided_soc);
			}
			TCPMessageServerClass ms = null;
			while(true)
			{
				bool bOK = false;
				try
				{
					ms = new TCPMessageServerClass(port, 1, provided_mynameis.profile);
					ms.AcceptStarted += new TCPServerBaseAcceptStartedEventHandler(AcceptStartedEvent);
					ms.ListenStarted += new TCPServerBaseListenStartedEventHandler(ListenStartedEvent);
					ms.ListenEnded += new TCPServerBaseListenEndedEventHandler(ListenEndedEvent);
					ms.BaseConnectionAccepted += new TCPServerBaseConnectionAcceptedEventHandler(BaseConnectionAcceptedEvent);
					ms.BaseMessageReceived += new TCPServerBaseMessageReceivedEventHandler(BaseMessageReceivedEvent);
					ms.MessageAccepted += new TCPMessageAcceptedEventHandler(MessageAcceptedEvent);
					ms.FeedingMailRequested += new TCPFeedingMailRequestedEventHandler(FeedingMailRequestedEvent);
					ms.StartListen();
					ms.StartAccept();
					bOK = true;
				}
				catch
				{
					ms = null;
				}
				if(bOK)
				{
					break;
				}
				else
				{
					if(65535 == port)
					{
						break;
					}
					port++;
				}
			}
			m_messageserverl = (TCPMessageServerLClass)(m_messageserverl + new TCPMessageServerEClass(ms));
			m_connectionserver.TellPort(provided_mynameis.profile, ms.port);
		}
		private void ConnectionClosedEvent(TCPCloseMessageClass provided_closed)
		{
			if(null != ConnectionClosed)
			{
				ConnectionClosed(provided_closed);
			}
			if(null != m_messageserverl.e)
			{
				for(int icnt = 0; icnt < m_messageserverl.e.Length; icnt++)
				{
					TCPMessageServerEClass tempserver = (TCPMessageServerEClass)(m_messageserverl.e[icnt]);
					if(tempserver.messageserver.profile.seq == provided_closed.profile.seq)
					{
						tempserver.messageserver.Close();
						m_messageserverl = (TCPMessageServerLClass)(m_messageserverl - tempserver);
						return;
					}
				}
			}
		}
		private void ConnectionClosingFailedEvent(ConnectionEClass provided_conn)
		{
			if(null != ConnectionClosingFailed)
			{
				ConnectionClosingFailed(provided_conn);
			}
		}
		private void ConnectionTellingPortSucceededEvent(ConnectionEClass provided_conn)
		{
			if(null != ConnectionTellingPortSucceeded)
			{
				ConnectionTellingPortSucceeded(provided_conn);
			}
		}
		private void ConnectionTellingPortFailedEvent(ConnectionEClass provided_conn)
		{
			if(null != ConnectionTellingPortFailed)
			{
				ConnectionTellingPortFailed(provided_conn);
			}
		}
		private void MessageAcceptedEvent(TCPMailMessageClass provided_mail, System.Net.Sockets.Socket provided_soc)
		{
			if(m_bmessagebusy)
			{
				System.Threading.Thread.Sleep(10);
				MessageAcceptedEvent(provided_mail, provided_soc);
			}
			else
			{
				m_bmessagebusy = true;
				TCPEnvelopeClass myenvelope = new TCPEnvelopeClass(m_envelopel.maxseq + 1, provided_mail.envelope.from, provided_mail.envelope.message);
				m_envelopel = (TCPEnvelopeLClass)(m_envelopel + myenvelope);
				TCPMailMessageClass mymail = new TCPMailMessageClass(provided_mail.seq, provided_mail.profile, myenvelope);
				if(null != m_messageserverl.e)
				{
					for(int icnt = 0; icnt < m_messageserverl.e.Length; icnt++)
					{
						TCPMessageServerEClass messageservere = (TCPMessageServerEClass)(m_messageserverl.e[icnt]);
						messageservere.messageserver.Deliver(mymail);
					}
				}
				if(null != MessageAccepted)
				{
					MessageAccepted(mymail, provided_soc);
				}
				m_bmessagebusy = false;
			}
		}
		private void FeedingMailRequestedEvent(TCPMessageServerClass provided_ms)
		{
			provided_ms.FeedMail(m_envelopel);
		}
	}
}
