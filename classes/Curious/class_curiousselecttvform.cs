namespace Curious
{
	public class CuriousSelectSQLTreeNodeClass : TreeNode
	{
		CuriousSelectClass m_s;
		public CuriousSelectClass s
		{
			get
			{
				return m_s;
			}
		}
		public CuriousSelectSQLTreeNodeClass(CuriousSelectClass provided_s)
		{
			m_s = provided_s;
			Initialize();
		}
		private void Initialize()
		{
			Text = m_s.sql;
		}
	}
	public class CuriousSelectFileTreeNodeClass : TreeNode
	{
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		public CuriousSelectFileTreeNodeClass(string provided_filename)
		{
			m_filename = provided_filename;
			Initialize();
		}
		public void AddNode(CuriousSelectSQLTreeNodeClass provided_sqlnode)
		{
			Nodes.Add(provided_sqlnode);
		}
		public void AddNode(CuriousSelectClass provided_select)
		{
			Nodes.Add(new CuriousSelectSQLTreeNodeClass(provided_select));
		}
		private void Initialize()
		{
			Text = m_filename;
		}
	}
	public class CuriousSelectTreeViewClass : TreeView
	{
		public event MDIShowSelectEventHandler ShowSelect;
		public event MDIOpenFileEventHandler OpenFile;
		CuriousLClass m_l;
		public CuriousLClass l
		{
			get
			{
				return m_l;
			}
		}
		CuriousSelectFileTreeNodeClass[] m_filel;
		public CuriousSelectTreeViewClass(CuriousLClass provided_l)
		{
			m_l = provided_l;
			Initialize();
		}
		private void Initialize()
		{
			if(null != m_l.e)
			{
				for(int icnt = 0; icnt < m_l.e.Length; icnt++)
				{
					CuriousSelectClass mys = (CuriousSelectClass)(m_l.e[icnt]);
					int idx = FindFileNodeIndex(mys.filename);
					m_filel[idx].AddNode(mys);
				}
			}
			if(null!= m_filel)
			{
				for(int jcnt = 0; jcnt < m_filel.Length; jcnt++)
				{
					Nodes.Add(m_filel[jcnt]);
				}
			}
			DoubleClick += new System.EventHandler(MyDoubleClick);
			KeyDown += new System.Windows.Forms.KeyEventHandler(EnterKeyDownEvent);
		}
		private int FindFileNodeIndex(string provided_filename)
		{
			int ret = FindFileNodeIndexCore(provided_filename);
			if(-1 == ret)
			{
				CuriousSelectFileTreeNodeClass adding = new CuriousSelectFileTreeNodeClass(provided_filename);
				if(null == m_filel)
				{
					m_filel = new CuriousSelectFileTreeNodeClass[1];
					m_filel[0] = adding;
				}
				else
				{
					CuriousSelectFileTreeNodeClass[] templ = new CuriousSelectFileTreeNodeClass[m_filel.Length + 1];
					for(int icnt = 0; icnt < m_filel.Length; icnt++)
					{
						templ[icnt] = m_filel[icnt];
					}
					templ[templ.Length - 1] = adding;
					m_filel = templ;
				}
				ret = FindFileNodeIndexCore(provided_filename);
			}
			return ret;
		}
		private int FindFileNodeIndexCore(string provided_filename)
		{
			if(null != m_filel)
			{
				for(int icnt = 0; icnt < m_filel.Length; icnt++)
				{
					if(provided_filename.ToUpper() == m_filel[icnt].filename.ToUpper())
					{
						return icnt;
					}
				}
			}
			return -1;
		}
		public void FireShowSelect()
		{
			try
			{
				if(SelectedNode.GetType() == typeof(CuriousSelectSQLTreeNodeClass))
				{
					if(null != ShowSelect)
					{
						ShowSelect(((CuriousSelectSQLTreeNodeClass)(SelectedNode)).s);
					}
				}
				if(SelectedNode.GetType() == typeof(CuriousSelectFileTreeNodeClass))
				{
					if(null != OpenFile)
					{
						 OpenFile(((CuriousSelectFileTreeNodeClass)(SelectedNode)).filename);
					}
				}
			}
			catch
			{
			}
		}
		private void MyDoubleClick(object sender, System.EventArgs e)
		{
			FireShowSelect();
		}
		private void EnterKeyDownEvent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				FireShowSelect();
			}
		}
	}
	public class CuriousSelectTVFormClass : Form
	{
		public event MDIShowSelectEventHandler ShowSelect;
		public event MDINewSelectEventHandler NewSelect;
		public event MDISaveSelectEventHandler SaveSelect;
		public event MDILoadSelectEventHandler LoadSelect;
		public event MDIEditSelectEventHandler EditSelect;
		public event MDIOpenFileEventHandler OpenFile;
		public const string titlestr = "SelectExplorer";
		protected CuriousSelectTreeViewClass m_tv;
		protected CuriousLClass m_l;
		public CuriousSelectTVFormClass(CuriousLClass provided_l)
		{
			m_l = provided_l;
			Initialize();
		}
		protected virtual string GetTitle()
		{
			return titlestr;
		}
		protected void Initialize()
		{
			SuspendLayout();
			Text = GetTitle();
			m_tv = new CuriousSelectTreeViewClass(m_l);
			m_tv.Dock = System.Windows.Forms.DockStyle.Fill;
			m_tv.ShowSelect += new MDIShowSelectEventHandler(FireShowSelect);
			m_tv.OpenFile += new MDIOpenFileEventHandler(FireOpenFile);
			Controls.Add(m_tv);
			InitializeMenu();
			ResumeLayout();
		}
		protected void InitializeMenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ContextMenuStrip cmenu = new ContextMenuStrip();
			ToolStripMenuItem itemstrip = new ToolStripMenuItem("&Item");
			
			itemstrip.DropDownItems.Add(CreateItemOpenMenu(true));
			cmenu.Items.Add(CreateItemOpenMenu(false));
			
			itemstrip.DropDownItems.Add(CreateItemNewMenu(true));
			cmenu.Items.Add(CreateItemNewMenu(false));
			
			itemstrip.DropDownItems.Add(CreateItemEditMenu(true));
			cmenu.Items.Add(CreateItemEditMenu(false));
			
			itemstrip.DropDownItems.Add(CreateItemSaveMenu(true));
			cmenu.Items.Add(CreateItemSaveMenu(false));
			
			itemstrip.DropDownItems.Add(CreateItemLoadMenu(true));
			cmenu.Items.Add(CreateItemLoadMenu(false));
			
			mymenu.Items.Add(itemstrip);
			mymenu.MdiWindowListItem = itemstrip;
			mymenu.Dock = System.Windows.Forms.DockStyle.Top;
			mymenu.AllowMerge = true;
			this.MainMenuStrip = mymenu;
			Controls.Add(mymenu);
			mymenu.Visible = false;
			m_tv.ContextMenuStrip = cmenu;
		}
		protected ToolStripMenuItem CreateItemOpenMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Open", null, new System.EventHandler(ItemOpenMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected ToolStripMenuItem CreateItemNewMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&New", null, new System.EventHandler(ItemNewMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected ToolStripMenuItem CreateItemEditMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Edit", null, new System.EventHandler(ItemEditMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected ToolStripMenuItem CreateItemSaveMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Save", null, new System.EventHandler(ItemSaveMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected ToolStripMenuItem CreateItemLoadMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Load", null, new System.EventHandler(ItemLoadMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected void ItemNewMenu(object sender, System.EventArgs e)
		{
			if(null != NewSelect)
			{
				NewSelect();
			}
		}
		protected void ItemOpenMenu(object sender, System.EventArgs e)
		{
			m_tv.FireShowSelect();
		}
		protected void FireShowSelect(CuriousSelectClass provided_select)
		{
			if(null != ShowSelect)
			{
				ShowSelect(provided_select);
			}
		}
		protected void FireOpenFile(string provided_filename)
		{
			if(null != OpenFile)
			{
				OpenFile(provided_filename);
			}
		}
		protected void ItemSaveMenu(object sender, System.EventArgs e)
		{
			if(null != SaveSelect)
			{
				SaveSelect();
			}
		}
		protected void ItemLoadMenu(object sender, System.EventArgs e)
		{
			if(null != LoadSelect)
			{
				LoadSelect();
			}
		}
		protected void ItemEditMenu(object sender, System.EventArgs e)
		{
			try
			{
				if(m_tv.SelectedNode.GetType() == typeof(CuriousSelectSQLTreeNodeClass))
				{
					CuriousSelectSQLTreeNodeClass tempnode = (CuriousSelectSQLTreeNodeClass)m_tv.SelectedNode;
					if(null != EditSelect)
					{
						EditSelect(tempnode.s);
					}
				}
			}
			catch
			{
			}
		}
	}
}
