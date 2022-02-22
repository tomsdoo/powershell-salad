namespace IDE
{
	public delegate void mdimessageeventhandler(string provided_class, string provided_message);
	public class MyMDI : Form
	{
		public const string titlestr = "saladide";
		saladenv m_env;
		public saladenv env
		{
			get
			{
				return m_env;
			}
		}
		classtvform m_classtvf;
		public 
		classtvform classtvf
		{
			get
			{
				return m_classtvf;
			}
		}
		functiontvform m_functiontvf;
		public functiontvform functiontvf
		{
			get
			{
				return m_functiontvf;
			}
		}
		folderexplorerform m_foldertvf;
		public folderexplorerform foldertvf
		{
			get
			{
				return m_foldertvf;
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
		backupmanager m_bm;
		public backupmanager bm
		{
			get
			{
				return m_bm;
			}
		}
		operationlogl m_ll;
		public operationlogl ll
		{
			get
			{
				return m_ll;
			}
		}
		operationloglistform m_oplogf;
		public operationloglistform oplogf
		{
			get
			{
				return m_oplogf;
			}
		}
		string m_rootfolder;
		string m_cmdletstr;
		StatusStrip m_statusstrip;
		ToolStripStatusLabel m_encodelabel;
		ToolStripStatusLabel m_label;
		public MyMDI(string provided_rootfolder, string provided_cmdletstr)
		{
			m_rootfolder = provided_rootfolder;
			m_cmdletstr = provided_cmdletstr;
			m_ll = new operationlogl();
			reloadenv();
			reloadbackupmanager();
			m_settings = new mysettings(this.Font);
			initialize();
			messageout("hello");
		}
		private void reloadenv()
		{
			m_env = new saladenv(m_rootfolder, m_cmdletstr);
		}
		private void reloadbackupmanager()
		{
			m_bm = new backupmanager(m_env);
			m_bm.messageevent += new mdimessageeventhandler(receivemessageevent);
		}
		private void initialize()
		{
			this.SuspendLayout();
			this.IsMdiContainer = true;
			initializestatus();
			setmenu();
			showclassexplorer();
			showfunctionexplorer();
			showfolderexplorer();
			this.ResumeLayout();
			TCP.ChatForm chatform = new TCP.ChatForm();
			chatform.MdiParent = this;
			chatform.mymenu.Visible = false;
			chatform.Show();
		}
		private void initializestatus()
		{
			m_statusstrip = new System.Windows.Forms.StatusStrip();
			m_statusstrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			m_encodelabel = new System.Windows.Forms.ToolStripStatusLabel();
			m_encodelabel.Dock = System.Windows.Forms.DockStyle.Left;
			m_encodelabel.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
			m_encodelabel.ForeColor = System.Drawing.Color.Green;
			m_encodelabel.Text = m_settings.enc.WebName;
			m_statusstrip.Items.Add(m_encodelabel);
			m_label = new System.Windows.Forms.ToolStripStatusLabel();
			m_label.Dock = System.Windows.Forms.DockStyle.Left;
			m_label.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
			m_statusstrip.Items.Add(m_label);
			this.Controls.Add(m_statusstrip);
		}
		private void receivemessageevent(string provided_class, string provided_message)
		{
			operationloge loge = new operationloge(m_ll.maxseq + 1, provided_class, provided_message);
			m_ll = m_ll + loge;
			reflectmessage(provided_message);
		}
		private void reflectmessage(string provided_message)
		{
			m_label.Text = provided_message;
		}
		private void refreshclassexplorer()
		{
			try
			{
				m_classtvf.Close();
				m_classtvf.Dispose();
			}
			catch
			{
			}
			showclassexplorer();
		}
		private void showclassexplorer()
		{
			m_classtvf = null;
			m_classtvf = new classtvform(m_env.classman.cl);
			m_classtvf.MdiParent = this;
			m_classtvf.openingfile += new classtvformfileopeneventoccured(ctvftvfopenfile);
			m_classtvf.messageevent += new mdimessageeventhandler(receivemessageevent);
			m_classtvf.newmethod += new classtvformnewmethodeventoccured(newclassmethod);
			m_classtvf.refreshevent += new classtvformrefresheventoccured(refreshclassexplorer);
			m_classtvf.Show();
		}
		private void refreshfunctionexplorer()
		{
			try
			{
				m_functiontvf.Close();
				m_functiontvf.Dispose();
			}
			catch
			{
			}
			showfunctionexplorer();
		}
		private void showfunctionexplorer()
		{
			m_functiontvf = null;
			m_functiontvf = new functiontvform(m_env.functionman.fl);
			m_functiontvf.MdiParent = this;
			m_functiontvf.openingfile += new functionexpformfileopeneventoccured(ctvftvfopenfile);
			m_functiontvf.newfunction += new functionexpformnewfunctioneventoccured(newfunction);
			m_functiontvf.messageevent += new mdimessageeventhandler(receivemessageevent);
			m_functiontvf.refreshevent += new functionexpformrefresheventoccured(refreshfunctionexplorer);
			m_functiontvf.Show();
		}
		private void refreshfolderexplorer()
		{
			try
			{
				m_foldertvf.Close();
				m_foldertvf.Dispose();
			}
			catch
			{
			}
			showfolderexplorer();
		}
		private void showfolderexplorer()
		{
			m_foldertvf = new folderexplorerform(m_env.definedfolders.rootfolder);
			m_foldertvf.MdiParent = this;
			m_foldertvf.openingfile += new filelbopenevent(ctvftvfopenfile);
			m_foldertvf.messageevent += new mdimessageeventhandler(receivemessageevent);
			m_foldertvf.refreshevent += new folderexplorerformrefresheventoccured(refreshfolderexplorer);
			m_foldertvf.Show();
		}
		private void showoperationloglistform()
		{
			m_oplogf = new operationloglistform(m_ll);
			m_oplogf.MdiParent = this;
			m_oplogf.operationlogrefreshevent += new operationlogrefresheventhandler(refreshviewoperationlog);
			m_oplogf.messageevent += new mdimessageeventhandler(receivemessageevent);
			m_oplogf.Show();
		}
		private void setmenu()
		{
			MenuStrip tempm = new MenuStrip();
			ToolStripMenuItem viewmenu = new ToolStripMenuItem("&View");
			ToolStripMenuItem classexplorermenu = new ToolStripMenuItem("&ClassExplorer", null, new System.EventHandler(viewclassexplorermenu));
			classexplorermenu.ShortcutKeys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
			classexplorermenu.ShowShortcutKeys = true;
			viewmenu.DropDownItems.Add(classexplorermenu);

			ToolStripMenuItem folderexplorermenu = new ToolStripMenuItem("F&olderExplorer", null, new System.EventHandler(viewfolderexplorermenu));
			folderexplorermenu.ShortcutKeys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D;
			folderexplorermenu.ShowShortcutKeys = true;
			viewmenu.DropDownItems.Add(folderexplorermenu);

			ToolStripMenuItem functionexplorermenu = new ToolStripMenuItem("&FunctionExplorer", null, new System.EventHandler(viewfunctionexplorermenu));
			functionexplorermenu.ShortcutKeys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
			functionexplorermenu.ShowShortcutKeys = true;
			viewmenu.DropDownItems.Add(functionexplorermenu);
			
			ToolStripMenuItem oplogmenu = new ToolStripMenuItem("Event&Log", null, new System.EventHandler(viewoperationlogformmenu));
			oplogmenu.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.L;
			oplogmenu.ShowShortcutKeys = true;
			viewmenu.DropDownItems.Add(oplogmenu);

			tempm.Items.Add(viewmenu);

			ToolStripMenuItem toolmenu = new ToolStripMenuItem("&Tool");
			ToolStripMenuItem settingmenu = new ToolStripMenuItem("&Settings", null, new System.EventHandler(toolsettingmenu));
			settingmenu.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S;
			settingmenu.ShowShortcutKeys = true;
			toolmenu.DropDownItems.Add(settingmenu);
			ToolStripMenuItem findmenu = new ToolStripMenuItem("&FindInFiles", null, new System.EventHandler(toolfindmenu));
			findmenu.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F;
			findmenu.ShowShortcutKeys = true;
			toolmenu.DropDownItems.Add(findmenu);

			tempm.Items.Add(toolmenu);

			ToolStripMenuItem windowmenu = new ToolStripMenuItem("&Window");
			tempm.Items.Add(windowmenu);
			tempm.MdiWindowListItem = windowmenu;
			tempm.Dock = System.Windows.Forms.DockStyle.Top;
			this.MainMenuStrip = tempm;
			this.Controls.Add(tempm);
			this.Text = titlestr;
			this.ShowIcon = false;
		}
		private void ctvftvfopenfile(string provided_filename)
		{
			if(null != this.MdiChildren)
			{
				for(int icnt = 0; icnt < this.MdiChildren.Length; icnt++)
				{
					if(this.MdiChildren[icnt].GetType() == typeof(myeditor))
					{
						myeditor tempe = (myeditor)this.MdiChildren[icnt];
						if(tempe.filename == provided_filename)
						{
							this.MdiChildren[icnt].Activate();
							return;
						}
					}
				}
			}
			
			messageout("file opened " + provided_filename);
			myeditor ed = new myeditor(provided_filename, m_env.classman.cl, m_settings, m_env.functionman.fl, m_env.pse.cmdletl);
			ed.saveevent += new myeditorsaveeventhandler(savefile);
			ed.messageevent += new mdimessageeventhandler(receivemessageevent);
			ed.MdiParent = this;
			ed.Show();
		}
		private void newclassmethod(string provided_classname, string provided_methodname, string provided_text)
		{
			string filename = m_env.definedfolders.codefolder + provided_classname + "." + provided_methodname + ".code";
			try
			{
				StreamWriter writer = new StreamWriter(filename, false, m_settings.enc);
				writer.Write(provided_text);
				writer.Close();
				reloadenv();
				reloadbackupmanager();
				refreshclassexplorer();
				messageout("new class method file saved " + filename);
			}
			catch
			{
				messageout("new class method file saving failed " + filename);
			}
		}
		private void newfunction(string provided_functionname, string provided_contenttext)
		{
			string filename = m_env.definedfolders.scriptsfolder + provided_functionname + ".ps1";
			try
			{
				StreamWriter writer = new StreamWriter(filename, false, m_settings.enc);
				writer.Write(provided_contenttext);
				writer.Close();
				reloadenv();
				refreshfunctionexplorer();
				messageout("new function file saved " + filename);
			}
			catch
			{
				messageout("new function file saving failed " + filename);
			}
		}
		private void refreshviewoperationlog()
		{
			try
			{
				m_oplogf.feedlogl(m_ll);
			}
			catch
			{
			}
		}
		private void savefile(string provided_filename, string provided_content)
		{
			backupe tempb = m_bm.newbackup(provided_filename);
			if(null != tempb)
			{
				try
				{
					StreamWriter writer = new StreamWriter(provided_filename, false, m_settings.enc);
					writer.Write(provided_content);
					writer.Close();
					messageout("file saved " + provided_filename);
				}
				catch
				{
					messageout("file saving failed " + provided_filename);
				}
			}
		}
		private void toolsettingmenu(object sender, System.EventArgs e)
		{
			settingform sf = new settingform(m_settings);
			sf.messageevent += new mdimessageeventhandler(receivemessageevent);
			if(System.Windows.Forms.DialogResult.OK == sf.ShowDialog())
			{
				m_settings = sf.settings;
				if(null != this.MdiChildren)
				{
					for(int icnt = 0; icnt < this.MdiChildren.Length; icnt++)
					{
						if(this.MdiChildren[icnt].GetType() == typeof(myeditor))
						{
							((myeditor)(this.MdiChildren[icnt])).feedsettings(m_settings);
						}
					}
				}
				m_encodelabel.Text = m_settings.enc.WebName;
			}
			sf.Dispose();
		}
		private void toolfindmenu(object sender, System.EventArgs e)
		{
			textfinderform tf = new textfinderform();
			if(System.Windows.Forms.DialogResult.OK == tf.ShowDialog())
			{
				System.ComponentModel.BackgroundWorker bgworker = new System.ComponentModel.BackgroundWorker();
				bgworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgfindrunworkercompleted);
				bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(bgfinddowork);
				bgworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgfindprogresschanged);
				bgworker.WorkerReportsProgress = true;
				bgworker.RunWorkerAsync(tf.findstr);
				messageout("finding started " + tf.findstr);
			}
			tf.Dispose();
		}
		private void bgfinddowork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			System.ComponentModel.BackgroundWorker worker = (System.ComponentModel.BackgroundWorker)sender;
			string findstr = (string)e.Argument;
			System.Windows.Forms.ToolStripProgressBar progressbar = new System.Windows.Forms.ToolStripProgressBar();
			progressbar.Minimum = 0;
			progressbar.Maximum = 100;
			progressbar.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
			progressbar.Text = findstr;
			worker.ReportProgress(0, progressbar);

			textfoundl tfl = new textfoundl();
			textfindmanager tm = new textfindmanager(m_env.definedfolders.codefolder, findstr, m_settings);
			tfl = tfl + tm.tfl;
			worker.ReportProgress(40, progressbar);
			tm = new textfindmanager(m_env.definedfolders.scriptsfolder, findstr, m_settings);
			tfl = tfl + tm.tfl;
			worker.ReportProgress(80, progressbar);
			textfindresultform tfrf = new textfindresultform(m_env, findstr, m_settings, tfl);
			worker.ReportProgress(100, progressbar);
			e.Result = tfrf;
			messageout("finding finished " + findstr);
		}
		private void bgfindprogresschanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			System.Windows.Forms.ToolStripProgressBar myprogress = (System.Windows.Forms.ToolStripProgressBar)e.UserState;
			if(0 == e.ProgressPercentage)
			{
				m_statusstrip.Items.Add(myprogress);
			}
			myprogress.Value = e.ProgressPercentage;
			if(100 == e.ProgressPercentage)
			{
				m_statusstrip.Items.Remove(myprogress);
			}
		}
		private void bgfindrunworkercompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			textfindresultform tfrf = (textfindresultform)e.Result;
			textfindresultform mytfrf = new textfindresultform(tfrf);
			mytfrf.MdiParent = this;
			mytfrf.openingfile += new textfinderopeneventoccured(ctvftvfopenfile);
			mytfrf.Show();
		}
		private void viewclassexplorermenu(object sender, System.EventArgs e)
		{
			if(!activateform(classtvform.titlestr))
			{
				showclassexplorer();
			}
		}
		private void viewfunctionexplorermenu(object sender, System.EventArgs e)
		{
			if(!activateform(functiontvform.titlestr))
			{
				showfunctionexplorer();
			}
		}
		private void viewfolderexplorermenu(object sender, System.EventArgs e)
		{
			if(!activateform(folderexplorerform.titlestr))
			{
				showfolderexplorer();
			}
		}
		private void viewoperationlogformmenu(object sender, System.EventArgs e)
		{
			if(!activateform(operationloglistform.titlestr))
			{
				showoperationloglistform();
			}
		}
		private bool activateform(string provided_title)
		{
			if(null != this.MdiChildren)
			{
				for(int icnt = 0; icnt < this.MdiChildren.Length; icnt++)
				{
					if(provided_title == this.MdiChildren[icnt].Text)
					{
						this.MdiChildren[icnt].Activate();
						return true;
					}
				}
			}
			return false;
		}
		private void messageout(string provided_message)
		{
			receivemessageevent("MDI", provided_message);
		}
	}
}
