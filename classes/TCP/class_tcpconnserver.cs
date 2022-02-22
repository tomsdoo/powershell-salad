namespace TCP
{
	public delegate void TCPConnectionAcceptedEventHandler(TCPMyNameIsMessageClass provided_mynameis, System.Net.Sockets.Socket provided_soc);
	public delegate void TCPConnectionClosedEventHandler(TCPCloseMessageClass provided_closed);
	public delegate void TCPConnectionClosingFailedEventHandler(ConnectionEClass provided_conn);
	public delegate void TCPConnectionTellingPortSucceededEventHandler(ConnectionEClass provided_conn);
	public delegate void TCPConnectionTellingPortFailedEventHandler(ConnectionEClass provided_conn);
	public class TCPConnectionServerClass : TCPServerBaseClass
	{
		public event TCPConnectionAcceptedEventHandler ConnectionAccepted;
		public event TCPConnectionClosedEventHandler ConnectionClosed;
		public event TCPConnectionClosingFailedEventHandler ClosingFailed;
		public event TCPConnectionTellingPortSucceededEventHandler TellingPortSucceeded;
		public event TCPConnectionTellingPortFailedEventHandler TellingPortFailed;
		protected ConnectionLClass m_connl;
		public ConnectionLClass connl
		{
			get
			{
				return m_connl;
			}
		}
		public TCPConnectionServerClass(int provided_port, int provided_maxconnection) : base(provided_port, provided_maxconnection)
		{
			m_connl = new ConnectionLClass();
		}
		public void TellPort(ProfileEClass provided_profile, int provided_port)
		{
			ConnectionEClass myconn = (ConnectionEClass)m_connl[provided_profile];
			if(null != myconn)
			{
				try
				{
					TCPPortTellingMessageClass pt = new TCPPortTellingMessageClass(provided_port);
					SendData(myconn.soc, pt.xmlstr);
					if(null != TellingPortSucceeded)
					{
						TellingPortSucceeded(myconn);
					}
				}
				catch
				{
					if(null != TellingPortFailed)
					{
						TellingPortFailed(myconn);
					}
				}
			}
		}
		public void Close()
		{
			if(null != m_connl.e)
			{
				for(int icnt = 0; icnt < m_connl.e.Length; icnt++)
				{
					ConnectionEClass myconn = (ConnectionEClass)(m_connl.e[icnt]);
					TCPServerCloseMessageClass closem = new TCPServerCloseMessageClass(int.MinValue);
					try
					{
						SendData(myconn.soc, closem.xmlstr);
						myconn.soc.Shutdown(System.Net.Sockets.SocketShutdown.Both);
						myconn.soc.Close();
					}
					catch
					{
						if(null != ClosingFailed)
						{
							ClosingFailed(myconn);
						}
					}
				}
			}
			try
			{
				EndListen();
			}
			catch{}
		}
		protected override void AcceptedAndAct(System.Net.Sockets.Socket provided_fromsoc)
		{
			base.AcceptedAndAct(provided_fromsoc);
			ConnectionEClass conn = new ConnectionEClass(new ProfileEClass(m_connl.maxseq + 1, string.Empty, string.Empty), provided_fromsoc);
			m_connl = (ConnectionLClass)(m_connl + conn);
			TCPWhoAreYouMessageClass whoareyou = new TCPWhoAreYouMessageClass(conn.seq);
			SendData(provided_fromsoc, whoareyou.xmlstr);
		}
		protected override bool ReceivedAndAct(string provided_data, System.Net.Sockets.Socket provided_soc)
		{
			bool disposeret = base.ReceivedAndAct(provided_data, provided_soc);
			TCPMessageInterpreterClass mi = new TCPMessageInterpreterClass(provided_data);
			switch(mi.tcpmessage.command)
			{
				case TCPMyNameIsMessageClass.CommandString:
					{
						TCPMyNameIsMessageClass mynameis = (TCPMyNameIsMessageClass)(mi.tcpmessage);
						ProfileEClass prof = mynameis.profile;
						ConnectionEClass conn = new ConnectionEClass(prof, provided_soc);
						m_connl = (ConnectionLClass)(m_connl - conn);
						m_connl = (ConnectionLClass)(m_connl + conn);
						DeliverForAll(MakeProfileListMessage());
						if(null != ConnectionAccepted)
						{
							ConnectionAccepted(mynameis, provided_soc);
						}
						break;
					}
				case TCPCloseMessageClass.CommandString:
					{
						ProfileEClass sprof = ((TCPCloseMessageClass)(mi.tcpmessage)).profile;
						ConnectionEClass sconn = new ConnectionEClass(sprof, provided_soc);
						try
						{
							provided_soc.Shutdown(System.Net.Sockets.SocketShutdown.Both);
							provided_soc.Close();
						}
						catch
						{
							if(null != ClosingFailed)
							{
								ClosingFailed(sconn);
							}
						}
						m_connl = (ConnectionLClass)(m_connl - sconn);
						DeliverForAll(MakeProfileListMessage());
						if(null != ConnectionClosed)
						{
							ConnectionClosed((TCPCloseMessageClass)(mi.tcpmessage));
						}
						return false;
					}
				case TCPRequestForProfilesMessageClass.CommandString:
					{
						SendData(provided_soc, MakeProfileListMessage().xmlstr);
						break;
					}
				default:
					{
						TCPNACKMessageClass nack = new TCPNACKMessageClass(TCPErrorMessageClass.NothingParticular);
						SendData(provided_soc, nack.xmlstr);
						break;
					}
			}
			return true;
		}
		protected TCPProfileListMessageClass MakeProfileListMessage()
		{
			ProfileLClass pl = new ProfileLClass();
			if(null != m_connl.e)
			{
				for(int icnt = 0; icnt < m_connl.e.Length; icnt++)
				{
					ConnectionEClass myconn = (ConnectionEClass)(m_connl.e[icnt]);
					pl = (ProfileLClass)(pl + myconn.profile);
				}
			}
			TCPProfileListMessageClass profm = new TCPProfileListMessageClass(int.MinValue, pl);
			return profm;
		}
		protected void DeliverForAll(TCPMessageBaseClass provided_message)
		{
			if(null != m_connl.e)
			{
				for(int icnt = 0; icnt < m_connl.e.Length; icnt++)
				{
					ConnectionEClass myconn = (ConnectionEClass)(m_connl.e[icnt]);
					SendData(myconn.soc, provided_message.xmlstr);
				}
			}
		}
	}
}
