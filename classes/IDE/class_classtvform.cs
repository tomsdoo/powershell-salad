namespace IDE
{
	public delegate void classtvformfileopeneventoccured(string provided_filename);
	public delegate void classtvformnewmethodeventoccured(string provided_classname, string provided_methodname, string provided_text);
	public delegate void classtvformrefresheventoccured();
	
	public class methodtn : TreeNode
	{
		methode m_m;
		public methode m
		{
			get
			{
				
				return m_m;
			}
		}
		public methodtn(methode provided_m)
		{
			m_m = provided_m;
			initialize();
		}
		public methodtn(methodtn provided_obj)
		{
			m_m = provided_obj.m;
			initialize();
		}
		private void initialize()
		{
			this.Text = m_m.name;
			this.Name = m_m.filename;
		}
	}
	
	public class classtn : TreeNode
	{
		classe m_c;
		public classe c
		{
			get
			{
				return m_c;
			}
		}
		public classtn(classe provided_c)
		{
			m_c = provided_c;
			initialize();
		}
		public classtn(classtn provided_obj)
		{
			m_c = provided_obj.c;
			initialize();
		}
		private void initialize()
		{
			this.Text = m_c.name;
			if(null != m_c.methods.e)
			{
				for(int icnt = 0; icnt < m_c.methods.e.Length; icnt++)
				{
					methode tempm = m_c.methods.e[icnt];
					methodtn tempmtn = new methodtn(tempm);
					this.Nodes.Add(tempmtn);
				}
			}
		}
	}
	
	public class classtv : TreeView
	{
		public event classtvformfileopeneventoccured openingfile;
		classl m_cl;
		public classl cl
		{
			get
			{
				return m_cl;
			}
		}
		public classtv(classl provided_cl)
		{
			m_cl = provided_cl;
			initialize();
		}
		public classtv(classtv provided_obj)
		{
			m_cl = provided_obj.cl;
			initialize();
		}
		private void initialize()
		{
			if(null != m_cl.e)
			{
				for(int icnt = 0; icnt < m_cl.e.Length; icnt++)
				{
					classe tempc = m_cl.e[icnt];
					classtn tempctn = new classtn(tempc);
					this.Nodes.Add(tempctn);
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
				if(string.Empty != SelectedNode.Name)
				{
					if(null != openingfile)
					{
						openingfile(SelectedNode.Name);
					}
				}
			}
		}
	}
	
	public class classtvform : Form
	{
		public const string titlestr = "ClassExplorer";
		public event classtvformfileopeneventoccured openingfile;
		public event classtvformnewmethodeventoccured newmethod;
		public event mdimessageeventhandler messageevent;
		public event classtvformrefresheventoccured refreshevent;
		classl m_cl;
		public classl cl
		{
			get
			{
				return m_cl;
			}
		}
		classtv m_ctv;
		public classtv ctv
		{
			get
			{
				return m_ctv;
			}
		}
		public string defaulttext
		{
			get
			{
				string ret = string.Empty;
				ret += "param()";
				ret += "\r\n";
				ret += "$this;";
				ret += "\r\n";
				return ret;
			}
		}
		public classtvform(classl provided_cl)
		{
			m_cl = provided_cl;
			initialize();
		}
		public classtvform(classtvform provided_obj)
		{
			m_cl = provided_obj.cl;
			initialize();
		}
		private void initialize()
		{
			this.SuspendLayout();
			m_ctv = new classtv(m_cl);
			this.Controls.Add(m_ctv);
			this.Text = titlestr;
			this.ShowIcon = true;
			m_ctv.openingfile += new classtvformfileopeneventoccured(altopenitem);
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
			
			ToolStripMenuItem newclassstrip = new ToolStripMenuItem("New&Class", null, new System.EventHandler(newclassmenu));
			itemstrip.DropDownItems.Add(newclassstrip);
			
			ToolStripMenuItem newmethodstrip = new ToolStripMenuItem("New&Method", null, new System.EventHandler(newmethodmenu));
			newmethodstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
			newmethodstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(newmethodstrip);
			
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
			if(string.Empty != m_ctv.SelectedNode.Name)
			{
				altopenitem(m_ctv.SelectedNode.Name);
			}
		}
		private void altopenitem(string provided_filename)
		{
			if(null != openingfile)
			{
				openingfile(m_ctv.SelectedNode.Name);
			}
		}
		private void newclassmenu(object sender, System.EventArgs e)
		{
			if(null != newmethod)
			{
				messageout("create new class");
				newclassform ncf = new newclassform();
				if(System.Windows.Forms.DialogResult.OK == ncf.ShowDialog())
				{
					if(string.Empty != ncf.classname)
					{
						if(string.Empty != ncf.methodname)
						{
							classe tempe = m_cl.Fetch(ncf.classname);
							if(null == tempe)
							{
								newmethod(ncf.classname, ncf.methodname, defaulttext);
							}
							else
							{
								messageout("the class name already exists " + ncf.classname);
							}
						}
					}
					else
					{
						messageout("the class name invalid");
					}
				}
				else
				{
					messageout("creating new class cancelled");
				}
				ncf.Dispose();
			}
		}
		private void newmethodmenu(object sender, System.EventArgs e)
		{
			if(null != newmethod)
			{
				if(string.Empty == m_ctv.SelectedNode.Name)
				{
					messageout("create new method");
					newclassform ncf = new newclassform(m_ctv.SelectedNode.Text);
					if(System.Windows.Forms.DialogResult.OK == ncf.ShowDialog())
					{
						if(string.Empty != ncf.classname)
						{
							if(string.Empty != ncf.methodname)
							{
								classe tempe = m_cl.Fetch(ncf.classname);
								if(null != tempe)
								{
									methode tempm = tempe.methods.Fetch(ncf.methodname);
									if(null == tempm)
									{
										newmethod(ncf.classname, ncf.methodname, defaulttext);
									}
									else
									{
										messageout("the method name already exists " + ncf.classname + ncf.methodname);
									}
								}
							}
							else
							{
								messageout("the method name invalid");
							}
						}
					}
					else
					{
						messageout("creating new method cancelled");
					}
					ncf.Dispose();
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
				messageevent("ClassExp", provided_message);
			}
		}
	}
	public class newclassform : Form
	{
		string m_classname;
		public string classname
		{
			get
			{
				return m_classname;
			}
		}
		string m_methodname;
		public string methodname
		{
			get
			{
				return m_methodname;
			}
		}
		TextBox m_classbox;
		TextBox m_methodbox;
		public newclassform()
		{
			m_classname = string.Empty;
			m_methodname = string.Empty;
			initialize();
			reflectvalue();
		}
		public newclassform(string provided_classname)
		{
			m_classname = provided_classname;
			m_methodname = string.Empty;
			initialize();
			reflectvalue();
		}
		private void initialize()
		{
			this.Text = "input class name and method name";
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.ControlBox = false;

			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			cancelbutton.Click += new System.EventHandler(mycancel);
			this.CancelButton = cancelbutton;
			this.Controls.Add(cancelbutton);
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			okbutton.Click += new System.EventHandler(myok);
			this.AcceptButton = okbutton;
			this.Controls.Add(okbutton);
			
			m_methodbox = new TextBox();
			m_methodbox.Dock = System.Windows.Forms.DockStyle.Top;
			this.Controls.Add(m_methodbox);
			
			m_classbox = new TextBox();
			m_classbox.Dock = System.Windows.Forms.DockStyle.Top;
			this.Controls.Add(m_classbox);
			
			int taborder = 0;
			m_classbox.TabIndex = taborder;
			taborder++;
			m_methodbox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			this.Height = 25;
			this.Height += m_classbox.Height;
			this.Height += m_methodbox.Height;
			this.Height += okbutton.Height;
			this.Height += cancelbutton.Height;
		}
		private void reflectvalue()
		{
			m_classbox.Text = m_classname;
			m_methodbox.Text = m_methodname;
			if(string.Empty == m_classname)
			{
				m_methodbox.Text = "Constructor";
				m_methodbox.ReadOnly = true;
			}
			else
			{
				if(string.Empty == m_methodname)
				{
					m_classbox.ReadOnly = true;
				}
			}
		}
		private void myok(object sender, System.EventArgs e)
		{
			m_classname = m_classbox.Text;
			m_methodname = m_methodbox.Text;
			Close();
		}
		private void mycancel(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
