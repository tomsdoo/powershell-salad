namespace TCP
{
	public class ChatRoomForm : Form
	{
		protected int m_port;
		public int port
		{
			get
			{
				return m_port;
			}
		}
		protected int m_maxcount;
		public int maxcount
		{
			get
			{
				return m_maxcount;
			}
		}
		protected TextBox m_tblog;
		protected ProfileTV m_profiletv;
		protected CommunicationServerClass m_commserver;
		public CommunicationServerClass commserver
		{
			get
			{
				return m_commserver;
			}
		}
		protected MenuStrip m_menu;
		public MenuStrip mymenu
		{
			get
			{
				return m_menu;
			}
		}
		protected System.Drawing.Color m_normalforecolor;
		protected System.Drawing.Color m_normalbackcolor;
		protected System.Drawing.Color m_processingforecolor;
		protected System.Drawing.Color m_processingbackcolor;
		protected ToolStripMenuItem m_startmenu;
		protected ToolStripMenuItem m_contextstartmenu;
		protected ToolStripMenuItem m_endmenu;
		protected ToolStripMenuItem m_contextendmenu;
		protected ContextMenuStrip m_contextmenu;
		public ContextMenuStrip mycontextmenu
		{
			get
			{
				return m_contextmenu;
			}
		}
		public ChatRoomForm()
		{
			m_processingforecolor = System.Drawing.Color.LightGreen;
			m_processingbackcolor = System.Drawing.Color.Black;
			m_port = 60000;
			m_maxcount = 10;
			m_commserver = null;
			Initialize();
		}
		protected void Initialize()
		{
			SuspendLayout();
			
			m_tblog = new TextBox();
			m_tblog.Multiline = true;
			m_tblog.ReadOnly = true;
			m_tblog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			m_tblog.Dock = System.Windows.Forms.DockStyle.Fill;
			m_tblog.TextChanged += new System.EventHandler(TextChangedEvent);
			m_normalforecolor = m_tblog.ForeColor;
			m_normalbackcolor = m_tblog.BackColor;
			Controls.Add(m_tblog);
			
			m_profiletv = new ProfileTV(new ProfileLClass());
			Controls.Add(m_profiletv);
			Width = m_profiletv.Width * 4;
			
			FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormClosingEvent);
			InitializeMenu();
			ReflectTitle();
			ResumeLayout();
		}
		protected virtual void InitializeMenu()
		{
			m_menu = new MenuStrip();
			m_menu.Dock = System.Windows.Forms.DockStyle.Top;
			ToolStripMenuItem roommenu = new ToolStripMenuItem("Chat&Room");
			m_menu.Items.Add(roommenu);
			m_menu.AllowMerge = true;
			MainMenuStrip = m_menu;
			Controls.Add(m_menu);

			m_contextmenu = new ContextMenuStrip();
			ContextMenuStrip = m_contextmenu;

			roommenu.DropDownItems.Add(CreateSettingsMenu(true));
			m_contextmenu.Items.Add(CreateSettingsMenu(false));
			
			m_startmenu = CreateStartMenu(true);
			m_contextstartmenu = CreateStartMenu(false);
			roommenu.DropDownItems.Add(m_startmenu);
			m_contextmenu.Items.Add(m_contextstartmenu);
			
			m_endmenu = CreateEndMenu(true);
			m_contextendmenu = CreateEndMenu(false);
			roommenu.DropDownItems.Add(m_endmenu);
			m_contextmenu.Items.Add(m_contextendmenu);
			
			ReflectStartMenu(true);
			ReflectEndMenu(false);
		}
		protected virtual ToolStripMenuItem CreateSettingsMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Settings", null, new System.EventHandler(RoomSettingsMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateStartMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("S&tart", null, new System.EventHandler(RoomStartMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateEndMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&End", null, new System.EventHandler(RoomEndMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual void RoomSettingsMenu(object sender, System.EventArgs e)
		{
			CommServerSettingsForm f = new CommServerSettingsForm(m_port, m_maxcount);
			if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
			{
				m_port = f.port;
				m_maxcount = f.maxcount;
				ReflectTitle();
			}
			f.Dispose();
		}
		protected virtual void RoomStartMenu(object sender, System.EventArgs e)
		{
			ReflectStartMenu(false);
			m_commserver = new CommunicationServerClass(m_port, m_maxcount);
			m_commserver.AcceptStarted += new TCPServerBaseAcceptStartedEventHandler(AcceptStartedEvent);
			m_commserver.ListenStarted += new TCPServerBaseListenStartedEventHandler(ListenStartedEvent);
			m_commserver.ListenEnded += new TCPServerBaseListenEndedEventHandler(ListenEndedEvent);
			m_commserver.BaseConnectionAccepted += new TCPServerBaseConnectionAcceptedEventHandler(BaseConnectionAcceptedEvent);
			m_commserver.BaseMessageReceived += new TCPServerBaseMessageReceivedEventHandler(BaseMessageReceivedEvent);
			m_commserver.ConnectionAccepted += new TCPConnectionAcceptedEventHandler(ConnectionAcceptedEvent);
			m_commserver.ConnectionClosed += new TCPConnectionClosedEventHandler(ConnectionClosedEvent);
			m_commserver.ConnectionClosingFailed += new TCPConnectionClosingFailedEventHandler(ConnectionClosingFailedEvent);
			m_commserver.ConnectionTellingPortSucceeded += new TCPConnectionTellingPortSucceededEventHandler(ConnectionTellingPortSucceededEvent);
			m_commserver.ConnectionTellingPortFailed += new TCPConnectionTellingPortFailedEventHandler(ConnectionTellingPortFailedEvent);
			m_commserver.MessageAccepted += new TCPMessageAcceptedEventHandler(MessageAcceptedEvent);
			m_commserver.Open();
			ReflectEndMenu(true);
			m_tblog.ForeColor = m_processingforecolor;
			m_tblog.BackColor = m_processingbackcolor;
		}
		protected virtual void RoomEndMenu(object sender, System.EventArgs e)
		{
			ReflectEndMenu(false);
			m_commserver.Close();
			ReflectStartMenu(true);
			m_tblog.ForeColor = m_normalforecolor;
			m_tblog.BackColor = m_normalbackcolor;
		}
		protected virtual void ReflectStartMenu(bool provided_tof)
		{
			m_startmenu.Enabled = provided_tof;
			m_contextstartmenu.Enabled = provided_tof;
		}
		protected virtual void ReflectEndMenu(bool provided_tof)
		{
			m_endmenu.Enabled = provided_tof;
			m_contextendmenu.Enabled = provided_tof;
		}
		protected virtual void AcceptStartedEvent(TCPServerBaseClass provided_baseserver)
		{
			m_tblog.Text += ("AcceptStarted port=" + provided_baseserver.port.ToString() + "\r\n");
			AddStatus();
		}
		protected virtual void ListenStartedEvent(TCPServerBaseClass provided_baseserver)
		{
			m_tblog.Text += ("ListenStarted port=" + provided_baseserver.port.ToString() + "\r\n");
			AddStatus();
		}
		protected virtual void ListenEndedEvent(TCPServerBaseClass provided_baseserver)
		{
			m_tblog.Text += ("ListenEnded port=" + provided_baseserver.port.ToString() + "\r\n");
			AddStatus();
		}
		protected virtual void BaseConnectionAcceptedEvent(TCPServerBaseClass provided_baseserver)
		{
			m_tblog.Text += ("BaseConnectionAccepted port=" + provided_baseserver.port.ToString() + "\r\n");
			AddStatus();
		}
		protected virtual void BaseMessageReceivedEvent(TCPServerBaseClass provided_baseserver, string provided_data)
		{
			m_tblog.Text += ("BaseMessageReceived port=" + provided_baseserver.port.ToString() + " " + provided_data + "\r\n");
			AddStatus();
		}
		protected virtual void ConnectionAcceptedEvent(TCPMyNameIsMessageClass provided_mynameis, System.Net.Sockets.Socket provided_soc)
		{
			m_tblog.Text += (provided_mynameis.command + " " + provided_mynameis.profile.name + "\r\n");
			ReflectProfileTV();
			AddStatus();
		}
		protected virtual void ConnectionClosedEvent(TCPCloseMessageClass provided_closed)
		{
			m_tblog.Text += (provided_closed.command + " " + provided_closed.profile.name + "\r\n");
			ReflectProfileTV();
			AddStatus();
		}
		protected virtual void ConnectionClosingFailedEvent(ConnectionEClass provided_conn)
		{
			m_tblog.Text += ("ConnectionClosingFailed " + provided_conn.profile.name + "\r\n");
			AddStatus();
		}
		protected virtual void ConnectionTellingPortSucceededEvent(ConnectionEClass provided_conn)
		{
			m_tblog.Text += ("ConnectionTellingPortSucceeded " + provided_conn.profile.name + "\r\n");
			AddStatus();
		}
		protected virtual void ConnectionTellingPortFailedEvent(ConnectionEClass provided_conn)
		{
			m_tblog.Text += ("ConnectionTellingPortFailed " + provided_conn.profile.name + "\r\n");
			AddStatus();
		}
		protected virtual void MessageAcceptedEvent(TCPMailMessageClass provided_mail, System.Net.Sockets.Socket provided_soc)
		{
			m_tblog.Text += (provided_mail.command + " " + provided_mail.envelope.strdata + "\r\n");
			AddStatus();
		}
		private void TextChangedEvent(object sender, System.EventArgs e)
		{
			m_tblog.SelectionStart = m_tblog.Text.Length;
			m_tblog.ScrollToCaret();
		}
		private void ReflectProfileTV()
		{
			ProfileLClass pl = new ProfileLClass();
			if(null != m_commserver.connectionserver.connl.e)
			{
				for(int icnt = 0; icnt < m_commserver.connectionserver.connl.e.Length; icnt++)
				{
					ConnectionEClass myconn = (ConnectionEClass)(m_commserver.connectionserver.connl.e[icnt]);
					pl = (ProfileLClass)(pl + myconn.profile);
				}
			}
			m_profiletv.FeedProfiles(pl);
		}
		private void ReflectTitle()
		{
			Text = "@" + m_port.ToString() + "(MaxConnection=" + m_maxcount.ToString() + ")";
		}
		private void AddStatus()
		{
			return;
			// status for debugging follows
			/*
			if(null != m_commserver.connectionserver)
			{
				if(null != m_commserver.connectionserver.connl.e)
				{
					m_tblog.Text += ("ConnectionCount:" + m_commserver.connectionserver.connl.e.Length.ToString() + "\r\n");
				}
			}
			if(null != m_commserver.messageserverl)
			{
				if(null != m_commserver.messageserverl.e)
				{
					m_tblog.Text += ("MessageCount:" + m_commserver.messageserverl.e.Length.ToString() + "\r\n");
					for(int icnt = 0; icnt < m_commserver.messageserverl.e.Length; icnt++)
					{
						TCPMessageServerEClass tempserver = (TCPMessageServerEClass)(m_commserver.messageserverl.e[icnt]);
						m_tblog.Text += ("MessagePort:" + tempserver.messageserver.port.ToString() + "\r\n");
					}
				}
			}
			*/
		}
		private void FormClosingEvent(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			try
			{
				m_commserver.Close();
			}
			catch{}
		}
	}
}
