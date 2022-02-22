namespace IDE
{
	public delegate void functionexpformfileopeneventoccured(string provided_filename);
	public delegate void functionexpformnewfunctioneventoccured(string provided_functionname, string provided_content);
	public delegate void functionexpformrefresheventoccured();
	
	public class functiontn : TreeNode
	{
		functione m_f;
		public functione f
		{
			get
			{
				return m_f;
			}
		}
		public functiontn(functione provided_f)
		{
			m_f = provided_f;
			initialize();
		}
		public functiontn(functiontn provided_obj)
		{
			m_f = provided_obj.f;
			initialize();
		}
		private void initialize()
		{
			this.Text = m_f.name;
			this.Name = m_f.filename;
		}
	}
	public class functiontv : TreeView
	{
		public event functionexpformfileopeneventoccured openingfile;
		functionl m_fl;
		public functionl fl
		{
			get
			{
				return m_fl;
			}
		}
		public functiontv(functionl provided_fl)
		{
			m_fl = provided_fl;
			initialize();
		}
		public functiontv(functiontv provided_obj)
		{
			
			m_fl = provided_obj.fl;
			initialize();
		}
		private void initialize()
		{
			if(null != m_fl.e)
			{
				for(int icnt = 0; icnt < m_fl.e.Length; icnt++)
				{
					functione tempf = m_fl.e[icnt];
					functiontn tempftn = new functiontn(tempf);
					this.Nodes.Add(tempftn);
				}
			}
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DoubleClick += new System.EventHandler(openitem);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(enterkeydownevent);
		}
		private void openitem(object sender, System.EventArgs e)
		{
			if(string.Empty != SelectedNode.Name)
			{
				if(null != openingfile)
				{
					openingfile(SelectedNode.Name);
				}
			}
		}
		private void enterkeydownevent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				if(null != openingfile)
				{
					openingfile(SelectedNode.Name);
				}
			}
		}
	}
	public class functiontvform : Form
	{
		public const string titlestr = "FunctionExplorer";
		public event functionexpformfileopeneventoccured openingfile;
		public event mdimessageeventhandler messageevent;
		public event functionexpformnewfunctioneventoccured newfunction;
		public event functionexpformrefresheventoccured refreshevent;
		functionl m_fl;
		public functionl fl
		{
			get
			{
				return m_fl;
			}
		}
		functiontv m_ftv;
		public functiontv ftv
		{
			get
			{
				return m_ftv;
			}
		}
		public functiontvform(functionl provided_fl)
		{
			m_fl = provided_fl;
			initialize();
		}
		public functiontvform(functiontvform provided_obj)
		{
			m_fl = provided_obj.fl;
			initialize();
		}
		private void initialize()
		{
			this.SuspendLayout();
			m_ftv = new functiontv(m_fl);
			this.Controls.Add(m_ftv);
			this.Text = titlestr;
			this.ShowIcon = true;
			m_ftv.openingfile += new functionexpformfileopeneventoccured(altopenitem);
			setmenu();
			this.ResumeLayout();
		}
		private void setmenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ToolStripMenuItem itemstrip = new ToolStripMenuItem("&Item");
			ToolStripMenuItem openstrip = new ToolStripMenuItem("&Open", null, new System.EventHandler(openitem));
			openstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
			openstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(openstrip);
			
			ToolStripMenuItem newfunctionstrip = new ToolStripMenuItem("New&Function", null, new System.EventHandler(newfunctionmenu));
			newfunctionstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
			newfunctionstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(newfunctionstrip);
			
			ToolStripMenuItem refreshstrip = new ToolStripMenuItem("&Refresh", null, new System.EventHandler(refreshmenu));
			refreshstrip.ShortcutKeys = System.Windows.Forms.Keys.F5;
			refreshstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(refreshstrip);
			
			mymenu.Items.Add(itemstrip);
			mymenu.MdiWindowListItem = itemstrip;
			mymenu.Dock = System.Windows.Forms.DockStyle.Top;
			mymenu.AllowMerge = true;
			this.MainMenuStrip = mymenu;
			this.Controls.Add(mymenu);
			mymenu.Visible = false;
		}
		private void openitem(object sender, System.EventArgs e)
		{
			if(string.Empty != m_ftv.SelectedNode.Name)
			{
				altopenitem(m_ftv.SelectedNode.Name);
			}
		}
		private void altopenitem(string provided_filename)
		{
			if(null != openingfile)
			{
				openingfile(m_ftv.SelectedNode.Name);
			}
		}
		private void newfunctionmenu(object sender, System.EventArgs e)
		{
			if(null != newfunction)
			{
				messageout("create new function");
				newfunctionform nff = new newfunctionform();
				if(System.Windows.Forms.DialogResult.OK == nff.ShowDialog())
				{
					if(string.Empty != nff.functionname)
					{
						functione tempe = m_fl.Fetch(nff.functionname);
						if(null == tempe)
						{
							newfunction(nff.functionname, nff.contenttext);
						}
						else
						{
							messageout("the function name already exists " + nff.functionname);
						}
					}
					else
					{
						messageout("the function name invalid");
					}
				}
				else
				{
					messageout("creating new function cancelled");
				}
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
				messageevent("FuncExp", provided_message);
			}
		}
	}
	public class newfunctionform : Form
	{
		string m_functionname;
		public string functionname
		{
			get
			{
				return m_functionname;
			}
		}
		string m_contenttext;
		public string contenttext
		{
			get
			{
				return m_contenttext;
			}
		}
		TextBox m_functionbox;
		public newfunctionform()
		{
			m_functionname = string.Empty;
			m_contenttext = string.Empty;
			initialize();
		}
		private void initialize()
		{
			this.Text = "input function name";
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.ControlBox = false;
			
			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton = cancelbutton;
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			cancelbutton.Click += new System.EventHandler(mycancel);
			this.Controls.Add(cancelbutton);
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.AcceptButton = okbutton;
			okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			okbutton.Click += new System.EventHandler(myok);
			this.Controls.Add(okbutton);
			
			m_functionbox = new TextBox();
			m_functionbox.Dock = System.Windows.Forms.DockStyle.Top;
			this.Controls.Add(m_functionbox);
			
			int taborder = 0;
			m_functionbox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			this.Height = 25;
			this.Height += m_functionbox.Height;
			this.Height += okbutton.Height;
			this.Height += cancelbutton.Height;
		}
		private void myok(object sender, System.EventArgs e)
		{
			m_functionname = m_functionbox.Text;
			m_contenttext = string.Empty;
			m_contenttext += "function global:" + m_functionname + "()";
			m_contenttext += "\r\n";
			m_contenttext += "#VISIBILITY:public";
			m_contenttext += "\r\n";
			m_contenttext += "{";
			m_contenttext += "\r\n";
			m_contenttext += ("\tWrite-Host \"This is a message from the function named " + m_functionname + ".\";");
			m_contenttext += "\r\n";
			m_contenttext += "}";
			m_contenttext += "\r\n";
			Close();
		}
		private void mycancel(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
