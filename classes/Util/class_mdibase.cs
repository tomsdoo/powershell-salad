namespace Util
{
	public class MDIBaseClass : FormBaseClass
	{
		protected Util.LogLClass m_logl;
		public Util.LogLClass logl
		{
			get
			{
				return m_logl;
			}
		}
		protected Util.LogLForm m_logform;
		public Util.LogLForm logform
		{
			get
			{
				return m_logform;
			}
		}
		protected ToolStripMenuItem m_viewmenu;
		public MDIBaseClass()
		{
			m_logl = new Util.LogLClass();
			IsMdiContainer = true;
			if(GetType().Name == typeof(MDIBaseClass).Name)
			{
				Initialize();
			}
		}
		protected override void Initialize(){}
		protected override void InitializeMenu()
		{
			m_viewmenu = new System.Windows.Forms.ToolStripMenuItem("&View");
			m_mymenu.Items.Add(m_viewmenu);
			System.Windows.Forms.ToolStripMenuItem logm = new System.Windows.Forms.ToolStripMenuItem("&Log", null, new System.EventHandler(ViewLogMenu));
			logm.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.L;
			logm.ShowShortcutKeys = true;
			m_viewmenu.DropDownItems.Add(logm);
			System.Windows.Forms.ToolStripMenuItem windowmenu = new System.Windows.Forms.ToolStripMenuItem("&Window");
			m_mymenu.MdiWindowListItem = windowmenu;
			m_mymenu.Items.Add(windowmenu);
		}
		protected override void InitializeStatus(){}
		protected delegate void MessageOutDele(string provided_from, string provided_message);
		protected virtual void MessageOut(string provided_from, string provided_message)
		{
			if(InvokeRequired)
			{
				Invoke(new MessageOutDele(MessageOut), new object[]{provided_from, provided_message});
				return;
			}
			m_logl = (Util.LogLClass)(m_logl + new Util.LogEClass(m_logl.maxseq + 1, provided_from, provided_message));
			try{m_statuslabel.Text = provided_message;}catch{}
		}
		protected override void MessageOut(string provided_message)
		{
			MessageOut(string.Empty, provided_message);
		}
		protected virtual void ViewLogMenu(object sender, System.EventArgs e)
		{
			RefreshLogForm();
		}
		protected virtual bool ActivateChild(string provided_title)
		{
			if(null != MdiChildren)
			{
				for(int icnt = 0; icnt < MdiChildren.Length; icnt++)
				{
					if(provided_title == MdiChildren[icnt].Text)
					{
						MdiChildren[icnt].Activate();
						return true;
					}
				}
			}
			return false;
		}
		protected virtual void InitializeLogForm()
		{
			if(!ActivateChild(LogLForm.titlestr))
			{
				m_logform = new LogLForm(m_logl);
				m_logform.MdiParent = this;
				m_logform.RefreshLog += new LogLFormRefreshEvehtHandler(RefreshLogForm);
				m_logform.Show();
			}
		}
		protected virtual void RefreshLogForm()
		{
			try
			{
				m_logform.Close();
				m_logform.Dispose();
			}
			catch{}
			finally
			{
				m_logform = null;
			}
			InitializeLogForm();
		}
	}
}
