namespace IDE
{
	public delegate void myeditorsaveeventhandler(string provided_filename, string provided_content);
	public class mytextbox : TextBox
	{
		string m_currenttext;
		public string currenttext
		{
			get
			{
				return m_currenttext;
			}
		}
		public mytextbox(string provided_text)
		{
			m_currenttext = provided_text;
			initialize();
		}
		public mytextbox(mytextbox provided_obj)
		{
			m_currenttext = provided_obj.currenttext;
			initialize();
		}
		private void initialize()
		{
			this.Multiline = true;
			this.Text = m_currenttext;
			this.Dock = System.Windows.Forms.DockStyle.Fill;
		}
	}
	public class myrichtextbox : RichTextBox
	{
		public bool dirty
		{
			get
			{
				return (m_originaltext == Text);
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
		classl m_cl;
		public classl cl
		{
			get
			{
				return m_cl;
			}
		}
		functionl m_fl;
		public functionl fl
		{
			get
			{
				return m_fl;
			}
		}
		string[] m_cmdletl;
		public string[] cmdletl
		{
			get
			{
				return m_cmdletl;
			}
		}
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		string m_originaltext;
		public string originaltext
		{
			get
			{
				return m_originaltext;
			}
		}
		string m_currenttext;
		public string currenttext
		{
			get
			{
				return m_currenttext;
			}
		}
		int m_currentfoundindex;
		founde m_currentfounde;
		foundl m_currentfoundl;
		public myrichtextbox(string provided_filename, string provided_text, classl provided_cl, mysettings provided_settings, functionl provided_fl, string[] provided_cmdletl)
		{
			m_filename = provided_filename;
			m_originaltext = provided_text;
			m_currenttext = provided_text;
			m_cl = provided_cl;
			m_settings =provided_settings;
			m_fl = provided_fl;
			m_cmdletl = provided_cmdletl;
			initialize();
		}
		public myrichtextbox(myrichtextbox provided_obj)
		{
			m_filename = provided_obj.filename;
			m_originaltext = provided_obj.originaltext;
			m_currenttext = provided_obj.currenttext;
			m_cl = provided_obj.cl;
			m_settings = provided_obj.settings;
			m_fl = provided_obj.fl;
			m_cmdletl = provided_obj.cmdletl;
			initialize();
		}
		public void recolor()
		{
			this.SuspendLayout();
			reflectcolors();
			this.ResumeLayout();
		}
		public void feedsettings(mysettings provided_settings)
		{
			m_settings = provided_settings;
			reflectcolors();
		}
		public void feedtext(string provided_currenttext)
		{
			m_currenttext = provided_currenttext;
			this.Text = m_currenttext;
			reflectcolors();
		}
		public void find(string provided_str)
		{
			findutil fu = new findutil(this);
			m_currentfoundl = fu.findstr(provided_str);
			if(null != m_currentfoundl.e)
			{
				foreach(founde fe in m_currentfoundl.e)
				{
					this.SelectionStart = fe.selectionstart;
					this.SelectionLength = fe.selectionlength;
					this.SelectionBackColor = m_settings.foundbackcolor;
				}
				m_currentfoundindex = 0;
				m_currentfounde = m_currentfoundl.e[m_currentfoundindex];
				this.SelectionStart = m_currentfounde.selectionstart;
				this.SelectionLength = m_currentfounde.selectionlength;
			}
		}
		public void findnext()
		{
			if(null != m_currentfoundl)
			{
				m_currentfoundindex++;
				try
				{
					m_currentfounde = m_currentfoundl.e[m_currentfoundindex];
				}
				catch
				{
					m_currentfoundindex = 0;
					m_currentfounde = m_currentfoundl.e[m_currentfoundindex];
				}
				this.SelectionStart = m_currentfounde.selectionstart;
				this.SelectionLength = m_currentfounde.selectionlength;
			}
		}
		private void initialize()
		{
			this.Multiline = true;
			this.AcceptsTab = true;
			this.Text = m_currenttext;
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			reflectcolors();
			//this.TextChanged += new System.EventHandler(changedtodo);
			//this.KeyUp += new System.Windows.Forms.KeyEventHandler(enterkeyup);
		}
		private void enterkeyup(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				this.SuspendLayout();
				reflectcolors();
				this.ResumeLayout();
			}
		}
		private void changedtodo(object sender, System.EventArgs e)
		{
			this.SuspendLayout();
			reflectcolors();
			this.ResumeLayout();
		}
		private bool isthispowershellcode()
		{
			string ext = System.IO.Path.GetExtension(m_filename).ToUpper();
			bool ret = false;
			ret = ret | (ext == ".CODE");
			ret = ret | (ext == ".PS1");
			return ret;
		}
		private void reflectcolors()
		{
			this.Font = m_settings.myfont;
			this.BackColor = m_settings.backcolor;
			
			this.Text = this.Text;

			int backupselectionstart = this.SelectionStart;
			int backupselectionlength = this.SelectionLength;

			resetcolor();
			if(isthispowershellcode())
			{
				setclasscolor();
				setfunctioncolor();
				setcmdletcolor();
				setvariablecolor();
				setcommentcolor();
				setliteralcolor();
			}

			this.SelectionStart = backupselectionstart;
			this.SelectionLength = backupselectionlength;
		}
		private void resetcolor()
		{
			if(Text.Length > 0)
			{
				this.SelectionStart = 0;
				this.SelectionLength = Text.Length;
				this.SelectionColor = m_settings.forecolor;

				this.SelectAll();
				this.SelectionBackColor = m_settings.backcolor;
			}
		}
		private void setcommentcolor()
		{
			findutil fu = new findutil(this);
			if(null != fu.commentfound.e)
			{
				foreach(founde fe in fu.commentfound.e)
				{
					this.SelectionStart = fe.selectionstart;
					this.SelectionLength = fe.selectionlength;
					this.SelectionColor = m_settings.commentcolor;
				}
			}
		}
		private void setvariablecolor()
		{
			findutil fu = new findutil(this);
			if(null != fu.variablefound.e)
			{
				foreach(founde fe in fu.variablefound.e)
				{
					this.SelectionStart = fe.selectionstart;
					this.SelectionLength = fe.selectionlength;
					this.SelectionColor = m_settings.variablecolor;
				}
			}
		}
		private void setclasscolor()
		{
			findutil fu = new findutil(this);
			if(null != fu.classfound.e)
			{
				foreach(founde fe in fu.classfound.e)
				{
					this.SelectionStart = fe.selectionstart;
					this.SelectionLength = fe.selectionlength;
					this.SelectionColor = m_settings.classcolor;
				}
			}
		}
		private void setfunctioncolor()
		{
			findutil fu = new findutil(this);
			if(null != fu.functionfound.e)
			{
				foreach(founde fe in fu.functionfound.e)
				{
					this.SelectionStart = fe.selectionstart;
					this.SelectionLength = fe.selectionlength;
					this.SelectionColor = m_settings.functioncolor;
				}
			}
		}
		private void setliteralcolor()
		{
			findutil fu = new findutil(this);
			if(null != fu.literalfound.e)
			{
				foreach(founde fe in fu.literalfound.e)
				{
					this.SelectionStart = fe.selectionstart;
					this.SelectionLength = fe.selectionlength;
					this.SelectionColor = m_settings.literalcolor;
				}
			}
		}
		private void setcmdletcolor()
		{
			findutil fu = new findutil(this);
			if(null != fu.cmdletfound.e)
			{
				foreach(founde fe in fu.cmdletfound.e)
				{
					this.SelectionStart = fe.selectionstart;
					this.SelectionLength = fe.selectionlength;
					this.SelectionColor = m_settings.cmdletcolor;
				}
			}
		}
	}
	public class myeditor : Form
	{
		public event mdimessageeventhandler messageevent;
		public event myeditorsaveeventhandler saveevent;
		string[] m_cmdletl;
		public string[] cmdletl
		{
			get
			{
				return m_cmdletl;
			}
		}
		functionl m_fl;
		public functionl fl
		{
			get
			{
				return m_fl;
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
		classl m_cl;
		public classl cl
		{
			get
			{
				return m_cl;
			}
		}
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		myrichtextbox m_t;
		public myrichtextbox t
		{
			get
			{
				return m_t;
			}
		}
		StreamReader m_reader;
		public myeditor(string provided_filename, classl provided_cl, mysettings provided_settings, functionl provided_fl, string[] provided_cmdletl)
		{
			m_filename = provided_filename;
			m_cl = provided_cl;
			m_settings = provided_settings;
			m_fl = provided_fl;
			m_cmdletl = provided_cmdletl;
			initialize();
		}
		public myeditor(myeditor provided_obj)
		{
			m_filename = provided_obj.filename;
			m_cl = provided_obj.cl;
			m_settings = provided_obj.settings;
			m_fl = provided_obj.fl;
			m_cmdletl = provided_obj.cmdletl;
			initialize();
		}
		public void feedsettings(mysettings provided_settings)
		{
			m_settings = provided_settings;
			m_t.feedsettings(m_settings);
		}
		private void initialize()
		{
			this.SuspendLayout();
			m_reader = null;
			string currenttext = string.Empty;
			try
			{
				m_reader = new StreamReader(m_filename, m_settings.enc);
				string line = string.Empty;
				while((line = m_reader.ReadLine()) != null)
				{
					currenttext += line;
					currenttext += "\r\n";
				}
				m_reader.Close();
			}
			catch
			{
			}
			m_t = new myrichtextbox(m_filename, currenttext, m_cl, m_settings, m_fl, m_cmdletl);
			this.Controls.Add(m_t);
			this.Text = System.IO.Path.GetFileName(m_filename);
			this.
			ShowIcon = true;
			setmenu();
			this.ResumeLayout();
			messageout("initialized " + m_filename);
		}
		private void reflecttext()
		{
			this.SuspendLayout();
			m_reader = null;
			string currenttext = string.Empty;
			try
			{
				m_reader = new StreamReader(m_filename, m_settings.enc);
				string line = string.Empty;
				while((line = m_reader.ReadLine()) != null)
				{
					currenttext += line;
					currenttext += "\r\n";
				}
				m_reader.Close();
				m_reader = null;
			}
			catch
			{
			}
			m_t.feedtext(currenttext);
			this.ResumeLayout();
		}
		private void setmenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ToolStripMenuItem editstrip = new ToolStripMenuItem("&Edit");
			
			ToolStripMenuItem findstrip = new ToolStripMenuItem("&Find", null, new System.EventHandler(editfindmenu));
			findstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F;
			findstrip.ShowShortcutKeys = true;
			editstrip.DropDownItems.Add(findstrip);
			
			ToolStripMenuItem findnextstrip = new ToolStripMenuItem("Find &Next", null, new System.EventHandler(editfindnextmenu));
			findnextstrip.ShortcutKeys = System.Windows.Forms.Keys.F3;
			findnextstrip.ShowShortcutKeys = true;
			editstrip.DropDownItems.Add(findnextstrip);
			
			ToolStripMenuItem replacestrip = new ToolStripMenuItem("&Replace", null, new System.EventHandler(editreplacemenu));
			replacestrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H;
			replacestrip.ShowShortcutKeys = true;
			editstrip.DropDownItems.Add(replacestrip);
			
			ToolStripMenuItem reopenstrip = new ToolStripMenuItem("Re-&open", null, new System.EventHandler(editreopenmenu));
			reopenstrip.ShortcutKeys = System.Windows.Forms.Keys.F5;
			reopenstrip.ShowShortcutKeys = true;
			editstrip.DropDownItems.Add(reopenstrip);
			
			ToolStripMenuItem savestrip = new ToolStripMenuItem("&Save", null, new System.EventHandler(editsavemenu));
			savestrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
			savestrip.ShowShortcutKeys = true;
			editstrip.DropDownItems.Add(savestrip);
			
			ToolStripMenuItem closestrip = new ToolStripMenuItem("C&lose", null, new System.EventHandler(editclosemenu));
			closestrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L;
			closestrip.ShowShortcutKeys = true;
			editstrip.DropDownItems.Add(closestrip);
			
			ToolStripMenuItem recolorstrip = new ToolStripMenuItem("Re-&color", null, new System.EventHandler(editrecolormenu));
			editstrip.DropDownItems.Add(recolorstrip);
			
			mymenu.Items.Add(editstrip);
			mymenu.MdiWindowListItem = editstrip;
			mymenu.Dock = System.Windows.Forms.DockStyle.Top;
			mymenu.AllowMerge = true;
			this.MainMenuStrip = mymenu;
			this.Controls.Add(mymenu);
			mymenu.Visible = false;
		}
		private void editrecolormenu(object sender, System.EventArgs e)
		{
			m_t.recolor();
		}
		private void editreopenmenu(object sender, System.EventArgs e)
		{
			reflecttext();
		}
		private void editfindmenu(object sender, System.EventArgs e)
		{
			findstr();
		}
		
		private void editfindnextmenu(object sender, System.EventArgs e)
		{
			findnext();
		}
		private void editreplacemenu(object sender, System.EventArgs e)
		{
			replacestr();
		}
		private void editsavemenu(object sender, System.EventArgs e)
		{
			if(null != saveevent)
			{
				saveevent(m_filename, m_t.Text.Replace("\r\n","\n").Replace("\n","\r\n"));
			}
		}
		private void editclosemenu(object sender, System.EventArgs e)
		{
			messageout("file closed " + m_filename);
			Close();
		}
		private void findstr()
		{
			rtfindform myf = new rtfindform(rtfindform.findonly);
			if(System.Windows.Forms.DialogResult.OK == myf.ShowDialog())
			{
				m_t.find(myf.findstr);
				messageout("text found " + myf.findstr + " " + m_filename);
			}
			myf.Dispose();
		}
		private void findnext()
		{
			m_t.findnext();
			messageout("text found next " + m_filename);
		}
		private void replacestr()
		{
			rtfindform myf = new rtfindform(rtfindform.findandreplace);
			if(System.Windows.Forms.DialogResult.OK == myf.ShowDialog())
			{
				m_t.feedtext(System.Text.RegularExpressions.Regex.Replace(m_t.Text, myf.findstr, myf.replacestr, System.Text.RegularExpressions.RegexOptions.IgnoreCase));
				messageout("text replaced " + myf.findstr + " -> " + myf.replacestr + " " + m_filename);
			}
			myf.Dispose();
		}
		private void messageout(string provided_message)
		{
			if(null != messageevent)
			{
				messageevent("Editor", provided_message);
			}
		}
	}
}
