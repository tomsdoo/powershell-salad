namespace TCP
{
	public class ChatForm : Form
	{
		public event TCPProfileListReceivedEventHandler ProfileListReceived;
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
		ProfileTV m_tv;
		TextBox m_tblog;
		TextBox m_tbsend;
		TextBox m_tbprof;
		Button m_okbutton;
		MenuStrip m_menu;
		public MenuStrip mymenu
		{
			get
			{
				return m_menu;
			}
		}
		ContextMenuStrip m_contextmenu;
		ToolStripMenuItem m_connectmenu;
		ToolStripMenuItem m_contextconnectmenu;
		ToolStripMenuItem m_disconnectmenu;
		ToolStripMenuItem m_contextdisconnectmenu;
		ToolStripMenuItem m_retrieveprofilesmenu;
		ToolStripMenuItem m_contextretrieveprofilesmenu;
		ToolStripMenuItem m_profilemenu;
		ToolStripMenuItem m_contextprofilemenu;
		ToolStripMenuItem m_retrievemessagemenu;
		ToolStripMenuItem m_contextretrievemessagemenu;
		ToolStripMenuItem m_connectionsettingmenu;
		ToolStripMenuItem m_contextconnectionsettingmenu;
		StatusStrip m_statusstrip;
		ToolStripStatusLabel m_statuslabel;
		CommunicationClientClass m_commclient;
		System.Drawing.Color m_normalforecolor;
		System.Drawing.Color m_normalbackcolor;
		System.Drawing.Color m_processingforecolor;
		System.Drawing.Color m_processingbackcolor;
		public ChatForm()
		{
			m_processingbackcolor = System.Drawing.Color.Black;
			m_processingforecolor = System.Drawing.Color.LightGreen;
			m_server = System.Net.Dns.GetHostName();
			m_connectionport = 60000;
			m_profile = new ProfileEClass(int.MinValue, System.Environment.UserName, "Hi.");
			Initialize();
		}
		protected virtual void Initialize()
		{
			SuspendLayout();
			m_tblog = new TextBox();
			m_tblog.Multiline = true;
			m_tblog.ReadOnly = true;
			m_tblog.Dock = System.Windows.Forms.DockStyle.Fill;
			m_normalbackcolor = m_tblog.BackColor;
			m_normalforecolor = m_tblog.ForeColor;
			m_tblog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			m_tblog.TextChanged += new System.EventHandler(TextChangedEvent);
			Controls.Add(m_tblog);
			
			m_tbprof = new TextBox();
			m_tbprof.Multiline = true;
			m_tbprof.TabStop = false;
			m_tbprof.ReadOnly = true;
			m_tbprof.Dock = System.Windows.Forms.DockStyle.Left;
			m_tbprof.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			Controls.Add(m_tbprof);
			
			m_tv = new ProfileTV(new ProfileLClass());
			m_tv.ProfileSelected += new ProfileTVSelectedEventHandler(ProfileSelectedEvent);
			Controls.Add(m_tv);
			m_tbprof.Width = (int)(m_tv.Width * 1.5);
			Width = m_tv.Width * 5;

			m_tbsend = new TextBox();
			m_tbsend.Dock = System.Windows.Forms.DockStyle.Bottom;
			Controls.Add(m_tbsend);
			
			m_okbutton = new Button();
			m_okbutton.Text = "Send";
			m_okbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
			AcceptButton = m_okbutton;
			m_okbutton.Click += new System.EventHandler(MyOK);
			Controls.Add(m_okbutton);
			m_okbutton.Enabled = false;
			
			int taborder = 0;
			m_tv.TabIndex = taborder;
			taborder++;
			m_tblog.TabIndex = taborder;
			taborder++;
			m_tbsend.TabIndex = taborder;
			taborder++;
			m_okbutton.TabIndex = taborder;
			
			FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormClosingEvent);
			InitializeMenu();
			InitializeStatus();
			
			ReflectTitle();
			ResumeLayout();
		}
		protected virtual void InitializeMenu()
		{
			m_menu = new MenuStrip();
			m_menu.Dock = System.Windows.Forms.DockStyle.Top;
			ToolStripMenuItem chatmenu = new ToolStripMenuItem("&Chat");
			m_menu.Items.Add(chatmenu);
			m_menu.AllowMerge = true;
			MainMenuStrip = m_menu;
			Controls.Add(m_menu);
			
			m_contextmenu = new ContextMenuStrip();
			ContextMenuStrip = m_contextmenu;
			
			m_profilemenu = CreateProfileSettingMenu(true);
			m_contextprofilemenu = CreateProfileSettingMenu(false);
			chatmenu.DropDownItems.Add(m_profilemenu);
			m_contextmenu.Items.Add(m_contextprofilemenu);
			
			m_connectionsettingmenu = CreateConnectionSettingMenu(true);
			m_contextconnectionsettingmenu = CreateConnectionSettingMenu(false);
			chatmenu.DropDownItems.Add(m_connectionsettingmenu);
			m_contextmenu.Items.Add(m_contextconnectionsettingmenu);

			m_connectmenu = CreateConnectMenu(true);
			m_contextconnectmenu = CreateConnectMenu(false);
			chatmenu.DropDownItems.Add(m_connectmenu);
			m_contextmenu.Items.Add(m_contextconnectmenu);

			m_disconnectmenu = CreateDisconnectMenu(true);
			m_contextdisconnectmenu = CreateDisconnectMenu(false);
			chatmenu.DropDownItems.Add(m_disconnectmenu);
			m_contextmenu.Items.Add(m_contextdisconnectmenu);
			
			m_retrieveprofilesmenu = CreateRetrieveProfilesMenu(true);
			m_contextretrieveprofilesmenu = CreateRetrieveProfilesMenu(false);
			//chatmenu.DropDownItems.Add(m_retrieveprofilesmenu);
			//m_contextmenu.Items.Add(m_contextretrieveprofilesmenu);
			
			m_retrievemessagemenu = CreateRetrieveMessageMenu(true);
			m_contextretrievemessagemenu = CreateRetrieveMessageMenu(false);
			//chatmenu.DropDownItems.Add(m_retrievemessagemenu);
			//m_contextmenu.Items.Add(m_contextretrievemessagemenu);
			
			ReflectConnectMenu(true);
			ReflectDisconnectMenu(false);
		}
		protected virtual void InitializeStatus()
		{
			m_statusstrip = new StatusStrip();
			m_statusstrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			m_statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
			m_statuslabel.Dock = System.Windows.Forms.DockStyle.Left;
			m_statuslabel.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
			m_statusstrip.Items.Add(m_statuslabel);
			Controls.Add(m_statusstrip);
		}
		protected virtual void MessageOut(string provided_message)
		{
			try
			{
				m_statuslabel.Text = provided_message;
			}
			catch{}
		}
		protected virtual ToolStripMenuItem CreateProfileSettingMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Profile", null, new System.EventHandler(ProfileSettingMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateConnectionSettingMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Connection", null, new System.EventHandler(ConnectionSettingMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateConnectMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("Co&nnect", null, new System.EventHandler(ConnectMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateDisconnectMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Disconnect", null, new System.EventHandler(DisconnectMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateRetrieveProfilesMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&RetrieveProfiles", null, new System.EventHandler(RetrieveProfilesMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateRetrieveMessageMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("Retrieve&Messages", null, new System.EventHandler(RetrieveMessageMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual void ProfileSettingMenu(object sender, System.EventArgs e)
		{
			CommClientProfileSettingsForm f = new CommClientProfileSettingsForm(m_profile);
			if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
			{
				m_profile = f.profile;
				ReflectTitle();
			}
			f.Dispose();
		}
		protected virtual void ConnectionSettingMenu(object sender, System.EventArgs e)
		{
			CommClientSettingsForm f = new CommClientSettingsForm(m_server, m_connectionport);
			if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
			{
				m_server = f.server;
				m_connectionport = f.connectionport;
				ReflectTitle();
			}
			f.Dispose();
		}
		protected virtual void ConnectMenu(object sender, System.EventArgs e)
		{
			ReflectConnectMenu(false);
			try
			{
				m_commclient = new CommunicationClientClass(m_server, m_connectionport, m_profile);
				m_commclient.MessageReceived += new TCPMessageReceivedEventHandler(MessageReceivedEvent);
				m_commclient.ServerClosed += new TCPServerClosedEventHandler(ServerClosedEvent);
				m_commclient.ProfileListReceived += new TCPProfileListReceivedEventHandler(ProfileListReceivedEvent);
				m_commclient.MailFed += new TCPMailFedEventHandler(MailFedEvent);
				m_commclient.Connect();
			}
			catch
			{
				ReflectConnectMenu(true);
				MessageOut("connection failed");
				return;
			}
			m_tblog.BackColor = m_processingbackcolor;
			m_tblog.ForeColor = m_processingforecolor;
			ReflectDisconnectMenu(true);
			MessageOut("connected");
		}
		protected virtual void DisconnectMenu(object sender, System.EventArgs e)
		{
			ReflectDisconnectMenu(false);
			m_commclient.Close();
			m_tblog.BackColor = m_normalbackcolor;
			m_tblog.ForeColor = m_normalforecolor;
			ReflectConnectMenu(true);
			MessageOut("disconnected");
		}
		protected virtual void RetrieveProfilesMenu(object sender, System.EventArgs e)
		{
			m_commclient.RequestForProfiles();
			MessageOut("requested for profiles");
		}
		protected virtual void RetrieveMessageMenu(object sender, System.EventArgs e)
		{
			m_commclient.RequestForMail();
			MessageOut("requested for massages");
		}
		protected void ReflectConnectMenu(bool provided_tof)
		{
			m_connectmenu.Enabled = provided_tof;
			m_contextconnectmenu.Enabled = provided_tof;
			m_profilemenu.Enabled = provided_tof;
			m_contextprofilemenu.Enabled = provided_tof;
			m_connectionsettingmenu.Enabled = provided_tof;
			m_contextconnectionsettingmenu.Enabled = provided_tof;
		}
		protected void ReflectDisconnectMenu(bool provided_tof)
		{
			m_disconnectmenu.Enabled = provided_tof;
			m_contextdisconnectmenu.Enabled = provided_tof;
			m_okbutton.Enabled = provided_tof;
			m_retrieveprofilesmenu.Enabled = provided_tof;
			m_contextretrieveprofilesmenu.Enabled = provided_tof;
			m_retrievemessagemenu.Enabled = provided_tof;
			m_contextretrievemessagemenu.Enabled = provided_tof;
		}
		protected void MyOK(object sender, System.EventArgs e)
		{
			try
			{
				if(string.Empty != m_tbsend.Text)
				{
					m_commclient.messageclient.SendMessage(m_tbsend.Text);
					m_tbsend.Text = string.Empty;
				}
			}
			catch{}
		}
		protected void MessageReceivedEvent(TCPMailMessageClass provided_mail)
		{
			string message = provided_mail.envelope.strdata + "\r\n";
			m_tblog.Text = m_tblog.Text + message;
		}
		protected void ServerClosedEvent()
		{
			m_tblog.BackColor = m_normalbackcolor;
			m_tblog.ForeColor = m_normalforecolor;
			ReflectDisconnectMenu(false);
			ReflectConnectMenu(true);
			MessageOut("server closed");
		}
		protected void ProfileListReceivedEvent(ProfileLClass provided_profilel)
		{
			if(null != ProfileListReceived)
			{
				ProfileListReceived(provided_profilel);
			}
			m_tv.FeedProfiles(provided_profilel);
		}
		protected void ProfileSelectedEvent(ProfileEClass provided_profile)
		{
			m_tbprof.Text = provided_profile.description;
		}
		protected void MailFedEvent(TCPFeedMailMessageClass provided_feedmail)
		{
			string message = provided_feedmail.envelopel.strdata;
			m_tblog.Text = message;
		}
		private void ReflectTitle()
		{
			Text = m_profile.name + " -> " + m_server + ":" + m_connectionport.ToString();
		}
		private void TextChangedEvent(object sender, System.EventArgs e)
		{
			m_tblog.SelectionStart = m_tblog.Text.Length;
			m_tblog.ScrollToCaret();
		}
		private void FormClosingEvent(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			try
			{
				m_commclient.Close();
			}
			catch{}
		}
	}
}
