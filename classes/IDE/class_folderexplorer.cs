namespace IDE
{
	public delegate void filelbopenevent(string provided_filename);
	public delegate void foldertvselectedevent(string[] provided_filelist);
	public delegate void folderexplorerformrefresheventoccured();

	public class filelb : ListBox
	{
		public event filelbopenevent openingfile;
		string[] m_filelist;
		public string[] filelist
		{
			get
			{
				return m_filelist;
			}
		}
		public filelb()
		{
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DoubleClick += new System.EventHandler(openitem);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(enterkeydownevent);
			m_filelist = null;
		}
		public void feedfilelist(string[] provided_filelist)
		{
			m_filelist = provided_filelist;
			if(null != m_filelist)
			{
				string[] strlist = new string[m_filelist.Length];
				for(int icnt = 0; icnt < m_filelist.Length; icnt++)
				{
					System.IO.FileInfo fi = new System.IO.FileInfo(m_filelist[icnt]);
					int size = (int)(fi.Length / 1024);
					if(0 == size)
					{
						size += 1;
					}
					strlist[icnt] = fi.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss") + "\t" + size.ToString().PadLeft(7) + "KB" + "\t" + fi.Name;
				}
				this.DataSource = strlist;
			}
		}
		private void enterkeydownevent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				if(null != openingfile)
				{
					if(-1 != this.SelectedIndex)
					{
						openingfile(m_filelist[this.SelectedIndex]);
					}
				}
			}
		}
		private void openitem(object sender, System.EventArgs e)
		{
			if(null != openingfile)
			{
				if(-1 != this.SelectedIndex)
				{
					openingfile(m_filelist[this.SelectedIndex]);
				}
			}
		}
	}
	public class foldertn : TreeNode
	{
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		public string[] listfiles
		{
			get
			{
				string[] ret = Directory.GetFiles(m_filename);
				return ret;
			}
		}
		public foldertn(string provided_filename)
		{
			m_filename = provided_filename;
			initialize();
		}
		private void initialize()
		{
			this.Text = System.IO.Path.GetFileName(m_filename);
			this.Name = m_filename;
			string[] dl = Directory.GetDirectories(m_filename);
			System.Array.Sort(dl);
			foreach(string de in dl)
			{
				foldertn child = new foldertn(de);
				this.Nodes.Add(child);
			}
		}
	}
	public class foldertv : TreeView
	{
		public event foldertvselectedevent tvselected;
		string m_rootfolder;
		public string rootfolder
		{
			get
			{
				return m_rootfolder;
			}
		}
		public foldertv(string provided_rootfolder)
		{
			m_rootfolder = provided_rootfolder;
			initialize();
		}
		private void initialize()
		{
			this.Width = 250;
			this.Dock = System.Windows.Forms.DockStyle.Left;
			this.Nodes.Add(new foldertn(m_rootfolder));
			this.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(selectedevent);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(enterkeydownevent);
		}
		private void enterkeydownevent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				selected();
			}
		}
		private void selectedevent(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			switch(e.Action)
			{
				case System.Windows.Forms.TreeViewAction.ByKeyboard:
					{
						break;
					}
				case System.Windows.Forms.TreeViewAction.ByMouse:
					{
						selected();
						break;
					}
				default:
					{
						break;
					}
			}
		}
		private void selected()
		{
			if(null != tvselected)
			{
				tvselected(Directory.GetFiles(this.SelectedNode.Name));
			}
		}
	}
	public class folderexplorerform : Form
	{
		public const string titlestr = "FolderExplorer";
		public event filelbopenevent openingfile;
		public event mdimessageeventhandler messageevent;
		public event folderexplorerformrefresheventoccured refreshevent;
		string m_rootfolder;
		public string rootfolder
		{
			get
			{
				return m_rootfolder;
			}
		}
		foldertv m_tv;
		public foldertv tv
		{
			get
			{
				return m_tv;
			}
		}
		filelb m_lb;
		public filelb lb
		{
			get
			{
				return m_lb;
			}
		}
		public folderexplorerform(string provided_rootfolder)
		{
			m_rootfolder = provided_rootfolder;
			if(System.IO.Path.GetFileName(m_rootfolder) == string.Empty)
			{
				m_rootfolder = m_rootfolder.Substring(0, m_rootfolder.Length - 1);
			}
			initialize();
		}
		private void initialize()
		{
			this.SuspendLayout();
			this.Text = titlestr;
			m_lb = new filelb();
			m_lb.openingfile += new filelbopenevent(openitem);
			this.Controls.Add(m_lb);
			m_tv = new foldertv(m_rootfolder);
			m_tv.tvselected += new foldertvselectedevent(tvselected);
			this.Width = m_tv.Width * 3;
			this.Controls.Add(m_tv);
			this.
			ShowIcon = true;
			
			int taborder = 0;
			m_tv.TabIndex = taborder;
			taborder++;
			m_lb.TabIndex = taborder;
			setmenu();
			this.ResumeLayout();
		}
		private void setmenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ToolStripMenuItem itemstrip = new ToolStripMenuItem("&Item");
			ToolStripMenuItem explorerstrip = new ToolStripMenuItem("Open&Explorer", null, new System.EventHandler(openexplorermenu));
			explorerstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
			explorerstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(explorerstrip);
			
			ToolStripMenuItem openstrip = new ToolStripMenuItem("&Open", null, new System.EventHandler(openmenu));
			openstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
			openstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(openstrip);
			
			ToolStripMenuItem refreshstrip = new ToolStripMenuItem("&Refresh", null, new System.EventHandler(refreshmenu));
			refreshstrip.ShortcutKeys = System.Windows.Forms.Keys.F5;
			refreshstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(refreshstrip);
			
			mymenu.Items.Add(itemstrip);
			mymenu.MdiWindowListItem = itemstrip;
			mymenu.AllowMerge = true;
			this.MainMenuStrip = mymenu;
			this.Controls.Add(mymenu);
			mymenu.Visible = false;
		}
		private void openexplorermenu(object sender, System.EventArgs e)
		{
			string myfolder = string.Empty;
			try
			{
				foldertn mynode = (foldertn)m_tv.SelectedNode;
				myfolder = mynode.filename;
				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer", myfolder));
				messageout("explorer opened " + myfolder);
			}
			catch
			{
				messageout("opening explorer failed " + myfolder);
			}
		}
		private void openmenu(object sender, System.EventArgs e)
		{
			if(-1 != m_lb.SelectedIndex)
			{
				openitem(m_lb.filelist[m_lb.SelectedIndex]);
			}
		}
		private void tvselected(string[] provided_filelist)
		{
			m_lb.feedfilelist(provided_filelist);
		}
		private void openitem(string provided_filename)
		{
			if(null != openingfile)
			{
				openingfile(provided_filename);
			}
		}
		private void refreshmenu(object sender, System.EventArgs e)
		{
			if(null != refreshevent)
			{
				refreshevent();
			}
		}
		private void messageout(string provided_message)
		{
			if(null != messageevent)
			{
				messageevent("FldExp", provided_message);
			}
		}
	}
}
