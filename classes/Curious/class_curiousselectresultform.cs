namespace Curious
{
	public class CuriousSelectResultForm : Form
	{
		public event MDIShowChangedRowEventHandler ShowChangedRow;
		public event MDIShowUpdatedRowEventHandler ShowUpdatedRow;
		public event MDIShowTableEventHandler ShowTable;
		public event MDILogMessageEventHandler LogMessage;
		const string EventBeforeString = "Before";
		const string EventAfterString = "After";
		ListBox m_lb;
		string[] m_lstr;
		CuriousSelectClass m_select;
		public CuriousSelectClass select
		{
			get
			{
				return m_select;
			}
		}
		CuriousResultLClass m_l;
		public CuriousResultLClass l
		{
			get
			{
				return m_l;
			}
		}
		ToolStripMenuItem m_startstrip;
		ToolStripMenuItem m_stopstrip;
		ToolStripMenuItem m_startcontext;
		ToolStripMenuItem m_stopcontext;
		System.Drawing.Color m_stoppingbackcolor;
		System.Drawing.Color m_startingbackcolor;
		System.Drawing.Color m_happenedbackcolor;
		public CuriousSelectResultForm(CuriousSelectClass provided_select)
		{
			m_stoppingbackcolor = System.Drawing.Color.LightGray;
			m_startingbackcolor = System.Drawing.Color.Khaki;
			m_happenedbackcolor = System.Drawing.Color.Pink;
			m_lstr = null;
			m_l = new CuriousResultLClass();
			m_select = provided_select;
			m_select.CuriousHappened += new CuriousHappenedEventHandler(MyHappened);
			Initialize();
		}
		private void Initialize()
		{
			SuspendLayout();
			Text = m_select.sortkey;
			m_lb = new ListBox();
			m_lb.Dock = System.Windows.Forms.DockStyle.Fill;
			m_lb.DoubleClick += new System.EventHandler(MyDoubleClick);
			m_lb.KeyDown += new System.Windows.Forms.KeyEventHandler(EnterKeyDownEvent);
			m_lb.DataSource = null;
			m_lb.Click += new System.EventHandler(ColorReflection);
			Activated += new System.EventHandler(ColorReflection);
			Controls.Add(m_lb);
			FormClosing += new System.Windows.Forms.FormClosingEventHandler(MyClosing);
			InitializeMenu();
			ResumeLayout();
		}
		private void InitializeMenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ContextMenuStrip cmenu = new ContextMenuStrip();
			ToolStripMenuItem monitorstrip = new ToolStripMenuItem("&Monitor");
			
			m_startstrip = CreateStartMenu(true);
			monitorstrip.DropDownItems.Add(m_startstrip);
			m_startcontext = CreateStartMenu(false);
			cmenu.Items.Add(m_startcontext);
			
			m_stopstrip = CreateStopMenu(true);
			monitorstrip.DropDownItems.Add(m_stopstrip);
			m_stopcontext = CreateStopMenu(false);
			cmenu.Items.Add(m_stopcontext);
			
			monitorstrip.DropDownItems.Add(CreateOpenChangedMenu(true));
			cmenu.Items.Add(CreateOpenChangedMenu(false));
			
			monitorstrip.DropDownItems.Add(CreateOpenCurrentTableMenu(true));
			cmenu.Items.Add(CreateOpenCurrentTableMenu(false));
			
			monitorstrip.DropDownItems.Add(CreateOpenBeforeTableMenu(true));
			cmenu.Items.Add(CreateOpenBeforeTableMenu(false));
			
			monitorstrip.DropDownItems.Add(CreateOpenAfterTableMenu(true));
			cmenu.Items.Add(CreateOpenAfterTableMenu(false));
			
			mymenu.Items.Add(monitorstrip);
			mymenu.MdiWindowListItem = monitorstrip;
			mymenu.Dock = System.Windows.Forms.DockStyle.Top;
			mymenu.AllowMerge = true;
			MainMenuStrip = mymenu;
			Controls.Add(mymenu);
			mymenu.Visible = false;
			m_lb.ContextMenuStrip = cmenu;
		}
		private ToolStripMenuItem CreateStartMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Start", null, new System.EventHandler(MonitorStartMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
				ret.ShowShortcutKeys = true;
			}
			ret.Enabled = true;
			return ret;
		}
		private ToolStripMenuItem CreateStopMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("S&top", null, new System.EventHandler(MonitorStopMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
				ret.ShowShortcutKeys = true;
			}
			ret.Enabled = false;
			return ret;
		}
		private ToolStripMenuItem CreateOpenChangedMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&OpenChangedRow", null, new System.EventHandler(OpenChangedMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		private ToolStripMenuItem CreateOpenCurrentTableMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("OpenCurrentTab&le", null, new System.EventHandler(OpenCurrentTableMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		private ToolStripMenuItem CreateOpenBeforeTableMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("Open&BeforeTable", null, new System.EventHandler(OpenBeforeTableMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		private ToolStripMenuItem CreateOpenAfterTableMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("Open&AfterTable", null, new System.EventHandler(OpenAfterTableMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		private void MonitorStartMenu(object sender, System.EventArgs e)
		{
			m_startstrip.Enabled = false;
			m_startcontext.Enabled = false;
			m_select.StartTimer();
			ColorReflectionCore();
			m_stopstrip.Enabled = true;
			m_stopcontext.Enabled = true;
			MessageOut("Monitor started");
		}
		private void MonitorStopMenu(object sender, System.EventArgs e)
		{
			m_stopstrip.Enabled = false;
			m_stopcontext.Enabled = false;
			m_select.EndTimer();
			ColorReflectionCore();
			m_startstrip.Enabled = true;
			m_startcontext.Enabled = true;
			MessageOut("Monitor stopped");
		}
		private void MyHappened(CuriousResultLClass provided_obj)
		{
			m_l += provided_obj;
			if(null != m_l.e)
			{
				m_lstr = new string[m_l.e.Length];
				for(int icnt = 0; icnt < m_l.e.Length; icnt++)
				{
					CuriousSelectResultClass myresult = (CuriousSelectResultClass)(m_l.e[icnt]);
					string mymessage = myresult.dt.ToString("yyyy/MM/dd HH:mm:ss") + "\t" + myresult.status + "\t";
					if(null != m_select.resultcaptioncolumns)
					{
						bool bf = true;
						for(int jcnt = 0; jcnt < m_select.resultcaptioncolumns.Length; jcnt++)
						{
							if(bf)
							{
								bf = false;
							}
							else
							{
								mymessage += " ";
							}
							mymessage += myresult.changedrow.coll[m_select.resultcaptioncolumns[jcnt]].ToString();
						}
					}
					m_lstr[icnt] = mymessage;
				}
				m_lb.DataSource = m_lstr;
			}
			m_lb.BackColor = m_happenedbackcolor;
			MessageOut("Something happened");
		}
		private void OpenChangedItem()
		{
			if(-1 != m_lb.SelectedIndex)
			{
				if(null != ShowChangedRow)
				{
					string title = m_lstr[m_lb.SelectedIndex];
					title = title.Replace("\t", " ") + " " + m_select.filename + " " + m_select.sql + " " + m_lb.SelectedIndex.ToString();
					CuriousSelectResultClass myr = (CuriousSelectResultClass)m_l.e[m_lb.SelectedIndex];
					switch(myr.status)
					{
						case CuriousSelectResultClass.RecordAdded:
							{
								ShowChangedRow(title, myr.changedrow);
								break;
							}
						case CuriousSelectResultClass.RecordDeleted:
							{
								ShowChangedRow(title, myr.changedrow);
								break;
							}
						case CuriousSelectResultClass.RecordUpdated:
							{
								ShowUpdatedRow(title, myr.updatedrows, myr.diffcols);
								break;
							}
						default:
							{
								break;
							}
					}
				}
			}
		}
		private void OpenChangedMenu(object sender, System.EventArgs e)
		{
			OpenChangedItem();
		}
		private void MyDoubleClick(object sender, System.EventArgs e)
		{
			OpenChangedItem();
		}
		private void EnterKeyDownEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				OpenChangedItem();
			}
		}
		private void FireShowTable(string provided_title, ACC.RowLClass provided_rl)
		{
			if(null != ShowTable)
			{
				ShowTable(provided_title, provided_rl);
			}
		}
		private void OpenCurrentTableMenu(object sender, System.EventArgs e)
		{
			string title = "CurrentView " + m_select.filename + " " + m_select.sql;
			FireShowTable(title, m_select.rl);
		}
		private void OpenEventTable(string provided_beforeorafter)
		{
			if(-1 != m_lb.SelectedIndex)
			{
				string title = m_lstr[m_lb.SelectedIndex].Replace("\t", " ") + " " + provided_beforeorafter + " " + m_select.filename + " " + m_select.sql + " " + m_lb.SelectedIndex.ToString();
				CuriousSelectResultClass myresult = (CuriousSelectResultClass)(m_l.e[m_lb.SelectedIndex]);
				switch(provided_beforeorafter)
				{
					case EventBeforeString:
						{
							FireShowTable(title, myresult.oldrl);
							break;
						}
					case EventAfterString:
						{
							FireShowTable(title, myresult.newrl);
							break;
						}
					default:
						{
							break;
						}
				}
			}
		}
		private void OpenBeforeTableMenu(object sender, System.EventArgs e)
		{
			OpenEventTable(EventBeforeString);
		}
		private void OpenAfterTableMenu(object sender, System.EventArgs e)
		{
			OpenEventTable(EventAfterString);
		}
		private void ColorReflection(object sender, System.EventArgs e)
		{
			ColorReflectionCore();
		}
		private void ColorReflectionCore()
		{
			if(m_select.working)
			{
				m_lb.BackColor = m_startingbackcolor;
			}
			else
			{
				m_lb.BackColor = m_stoppingbackcolor;
			}
		}
		private void MyClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			m_select.EndTimer();
		}
		private void MessageOut(string provided_message)
		{
			if(null != LogMessage)
			{
				LogMessage("Monitor", provided_message + " " + m_select.filename + " " + m_select.sql);
			}
		}
	}
}
