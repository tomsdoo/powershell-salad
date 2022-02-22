namespace Util
{
	public class FormBaseClass : System.Windows.Forms.Form
	{
		protected System.Windows.Forms.MenuStrip m_mymenu;
		public System.Windows.Forms.MenuStrip mymenu
		{
			get
			{
				return m_mymenu;
			}
		}
		protected System.Windows.Forms.StatusStrip m_mystatusstrip;
		public System.Windows.Forms.StatusStrip mystatusstrip
		{
			get
			{
				return m_mystatusstrip;
			}
		}
		protected System.Windows.Forms.ToolStripStatusLabel m_statuslabel;
		public System.Windows.Forms.ToolStripStatusLabel statuslabel
		{
			get
			{
				return m_statuslabel;
			}
		}
		protected System.Windows.Forms.ContextMenuStrip m_cmenu;
		public System.Windows.Forms.ContextMenuStrip cmenu
		{
			get
			{
				return m_cmenu;
			}
		}
		public FormBaseClass()
		{
			Load += new System.EventHandler(OnLoad);
			if(GetType().Name == typeof(FormBaseClass).Name)
			{
				Initialize();
			}
			m_mymenu = new System.Windows.Forms.MenuStrip();
			m_mymenu.Dock = System.Windows.Forms.DockStyle.Top;
			m_mymenu.AllowMerge = true;
			MainMenuStrip = m_mymenu;
			Controls.Add(m_mymenu);
			m_cmenu = new System.Windows.Forms.ContextMenuStrip();
			ContextMenuStrip = m_cmenu;
			InitializeMenu();
			m_mystatusstrip = new System.Windows.Forms.StatusStrip();
			Controls.Add(m_mystatusstrip);
			m_statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
			m_mystatusstrip.Items.Add(m_statuslabel);
			InitializeStatus();
		}
		protected virtual void Initialize(){}
		protected virtual void InitializeMenu(){}
		protected virtual void InitializeStatus(){}
		protected virtual void OnLoad(object sender, System.EventArgs e)
		{
			m_mymenu.Visible = (null == MdiParent);
		}
		protected virtual void MessageOut(string provided_message)
		{
			try{m_statuslabel.Text = provided_message;}catch{}
		}
	}
}
