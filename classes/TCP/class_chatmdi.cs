namespace TCP
{
	public class ChatMDI : Form
	{
		const string titlestr = "Chat";
		MenuStrip m_mymenu;
		public ChatMDI()
		{
			Initialize();
		}
		private void Initialize()
		{
			SuspendLayout();
			IsMdiContainer = true;
			ChatForm c = new ChatForm();
			c.ProfileListReceived += new TCPProfileListReceivedEventHandler(ProfileListReceivedEvent);
			c.MdiParent = this;
			c.Show();
			ChatRoomForm r = new ChatRoomForm();
			r.MdiParent = this;
			r.Show();
			ResumeLayout();
		}
		private void InitializeMenu()
		{
			m_mymenu = new MenuStrip();
			m_mymenu.Dock = System.Windows.Forms.DockStyle.Top;
			ToolStripMenuItem windowmenu = new ToolStripMenuItem("&Window");
			m_mymenu.Items.Add(windowmenu);
			m_mymenu.MdiWindowListItem = windowmenu;
			MainMenuStrip = m_mymenu;
			Controls.Add(m_mymenu);
		}
		private void ProfileListReceivedEvent(ProfileLClass provided_profilel)
		{
			// ?
		}
	}
}
