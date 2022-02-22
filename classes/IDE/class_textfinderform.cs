namespace IDE
{
	public delegate void textfinderopeneventoccured(string provided_filename);

	public class textfindresultlistbox : ListBox
	{
		public event textfinderopeneventoccured openingfile;

		textfoundl m_tfl;
		public textfoundl tfl
		{
			get
			{
				return m_tfl;
			}
		}
		public textfindresultlistbox(textfoundl provided_tfl)
		{
			m_tfl = provided_tfl;
			initialize();
		}
		private void initialize()
		{
			this.DataSource = m_tfl.listlines;
			this.DoubleClick += new System.EventHandler(doubleclicked);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(enterkeydownevent);
			this.Dock = System.Windows.Forms.DockStyle.Fill;
		}
		private void doubleclicked(object sender, System.EventArgs e)
		{
			if(-1 != this.SelectedIndex)
			{
				if(null != openingfile)
				{
					openingfile(m_tfl.e[this.SelectedIndex].filename);
				}
			}
		}
		private void enterkeydownevent(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				if(-1 != this.SelectedIndex)
				{
					if(null != openingfile)
					{
						openingfile(m_tfl.e[this.SelectedIndex].filename);
					}
				}
			}
		}
	}
	public class textfindresultform : Form
	{
		public event textfinderopeneventoccured openingfile;
		public event mdimessageeventhandler messageevent;
		saladenv m_env;
		public saladenv env
		{
			get
			{
				return m_env;
			}
		}
		textfoundl m_tfl;
		public textfoundl tfl
		{
			get
			{
				return m_tfl;
			}
		}
		textfindresultlistbox m_lb;
		public textfindresultlistbox lb
		{
			get
			{
				return m_lb;
			}
		}
		string m_findstr;
		public string findstr
		{
			get
			{
				return m_findstr;
			}
		}
		mysettings m_settings;
		public mysettings settings
		{
			get
			{
				return m_settings;
			}
		}
		public textfindresultform(saladenv provided_env, string provided_findstr, mysettings provided_settings)
		{
			m_findstr = provided_findstr;
			m_settings = provided_settings;
			m_env = provided_env;
			m_tfl = new textfoundl();
			textfindmanager tm = new textfindmanager(m_env.definedfolders.codefolder, m_findstr, m_settings);
			m_tfl = m_tfl + tm.tfl;
			tm = new textfindmanager(m_env.definedfolders.scriptsfolder, m_findstr, m_settings);
			m_tfl = m_tfl + tm.tfl;
			initialize();
		}
		public textfindresultform(saladenv provided_env, string provided_findstr, mysettings provided_settings, textfoundl provided_tfl)
		{
			m_env = provided_env;
			m_findstr = provided_findstr;
			m_settings = provided_settings;
			m_tfl = provided_tfl;
			initialize();
		}
		public textfindresultform(textfindresultform provided_obj)
		{
			m_env = provided_obj.env;
			m_findstr = provided_obj.findstr;
			m_settings = provided_obj.settings;
			m_tfl = provided_obj.tfl;
			initialize();
		}
		private void initialize()
		{
			this.SuspendLayout();
			m_lb = new textfindresultlistbox(m_tfl);
			m_lb.openingfile += new textfinderopeneventoccured(openfile);
			this.Controls.Add(m_lb);
			setmenu();
			int cases = 0;
			if(null != m_tfl.e)
			{
				cases = m_tfl.e.Length;
			}
			this.Text = "FoundResult : " + m_findstr + " : " + cases.ToString();
			this.ShowIcon = false;
			this.ResumeLayout();
		}
		private void openfile(string provided_filename)
		{
			if(null != openingfile)
			{
				openingfile(provided_filename);
			}
		}
		private void setmenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ToolStripMenuItem itemstrip = new ToolStripMenuItem("&Item");
			ToolStripMenuItem openstrip = new ToolStripMenuItem("&Open", null, new System.EventHandler(openfileevent));
			openstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
			openstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(openstrip);
			
			ToolStripMenuItem exportstrip = new ToolStripMenuItem("&Export", null, new System.EventHandler(exportevent));
			exportstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
			exportstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(exportstrip);
			
			mymenu.Items.Add(itemstrip);
			mymenu.MdiWindowListItem = itemstrip;
			mymenu.Dock = System.Windows.Forms.DockStyle.Top;
			mymenu.AllowMerge = true;
			this.MainMenuStrip = mymenu;
			this.Controls.Add(mymenu);
			mymenu.Visible = false;
		}
		private void openfileevent(object sender, System.EventArgs e)
		{
			if(-1 != m_lb.SelectedIndex)
			{
				if(null != openingfile)
				{
					openingfile(m_tfl.e[m_lb.SelectedIndex].filename);
				}
			}
		}
		private void exportevent(object sender, System.EventArgs e)
		{
			exportfileform ef = new exportfileform();
			if(System.Windows.Forms.DialogResult.OK == ef.ShowDialog())
			{
				try
				{
					string dn = System.IO.Path.GetDirectoryName(ef.filename);
					if(!System.IO.Directory.Exists(dn))
					{
						System.IO.Directory.CreateDirectory(dn);
					}
					StreamWriter writer = new StreamWriter(ef.filename, false);
					writer.Write(m_tfl.html);
					writer.Close();
					messageout("found result exported " + m_findstr + " " + ef.filename);
				}
				catch
				{
					messageout("found result exporting failed " + m_findstr + " " + ef.filename);
				}
			}
			ef.Dispose();
		}
		private void messageout(string provided_message)
		{
			if(null != messageevent)
			{
				messageevent("Finder", provided_message);
			}
		}
	}
	public class exportfileform : Form
	{
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		TextBox m_filebox;
		public exportfileform()
		{
			m_filename = string.Empty;
			initialize();
		}
		private void initialize()
		{
			this.SuspendLayout();
			this.Text = "input file name to export";
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			
			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.Click += new System.EventHandler(cancelevent);
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton = cancelbutton;
			this.Controls.Add(cancelbutton);
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.Click += new System.EventHandler(okevent);
			okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.AcceptButton = okbutton;
			this.Controls.Add(okbutton);
			
			m_filebox = new TextBox();
			m_filebox.Dock = System.Windows.Forms.DockStyle.Top;
			m_filebox.Text = System.IO.Path.GetTempPath() + "MyExport.htm";
			this.Controls.Add(m_filebox);
			
			int taborder = 0;
			m_filebox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			this.Height = 25;
			this.Height += m_filebox.Height;
			this.Height += okbutton.Height;
			this.Height += cancelbutton.Height;
			this.ResumeLayout();
		}
		private void okevent(object sender, System.EventArgs e)
		{
			m_filename = m_filebox.Text;
			Close();
		}
		private void cancelevent(object sender, System.EventArgs e)
		{
			Close();
		}
	}
	public class textfinderform : Form
	{
		string m_findstr;
		public string findstr
		{
			get
			{
				return m_findstr;
			}
		}
		TextBox m_findbox;
		public textfinderform()
		{
			initialize();
		}
		private void initialize()
		{
			this.SuspendLayout();
			this.Text = "input to find";
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.Click += new System.EventHandler(cancelevent);
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton = cancelbutton;
			this.Controls.Add(cancelbutton);
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.Click += new System.EventHandler(okevent);
			okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.AcceptButton = okbutton;
			this.Controls.Add(okbutton);
			
			m_findbox = new TextBox();
			m_findbox.Dock = System.Windows.Forms.DockStyle.Top;
			this.Controls.Add(m_findbox);
			
			int taborder = 0;
			m_findbox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			this.Height = 25;
			this.Height += m_findbox.Height;
			this.Height += okbutton.Height;
			this.Height += cancelbutton.Height;
			this.ResumeLayout();
		}
		private void cancelevent(object sender, System.EventArgs e)
		{
			Close();
		}
		private void okevent(object sender, System.EventArgs e)
		{
			m_findstr = m_findbox.Text;
			Close();
		}
	}
}
