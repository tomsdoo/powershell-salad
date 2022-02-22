namespace Curious
{
	public delegate void MDINewSelectEventHandler();
	public delegate void MDIEditSelectEventHandler(CuriousSelectClass provided_select);
	public delegate void MDIShowSelectEventHandler(CuriousSelectClass provided_select);
	public delegate void MDIShowChangedRowEventHandler(string provided_title, ACC.RowEClass provided_re);
	public delegate void MDIShowUpdatedRowEventHandler(string provided_title, ACC.RowLClass provided_rl, string[] provided_differentcolumns);
	public delegate void MDIShowTableEventHandler(string provided_title, ACC.RowLClass provided_rl);
	public delegate void MDISaveSelectEventHandler();
	public delegate void MDILoadSelectEventHandler();
	public delegate void MDIOpenFileEventHandler(string provided_filename);
	public delegate void MDILogMessageEventHandler(string provided_classname, string provided_message);
	public class MDIFormClass : Form
	{
		const string titlestr = "Curious";
		CuriousLClass m_selectl;
		MenuStrip m_mymenu;
		StatusStrip m_statusstrip;
		ToolStripStatusLabel m_statuslabel;
		CuriousSelectTVFormClass m_selecttvf;
		CuriousLClass m_selectwmil;
		CuriousSelectWMITVFormClass m_selectwmitvf;
		CuriousLClass m_selectfolderl;
		CuriousSelectFolderTVFormClass m_selectfoldertvf;
		OperationLogListForm m_logform;
		Util.LogLClass m_ll;
		public MDIFormClass()
		{
			m_logform = null;
			m_ll = new Util.LogLClass();
			m_selectl = new CuriousLClass();
			m_selectwmil = new CuriousLClass();
			m_selectfolderl = new CuriousLClass();
			Initialize();
		}
		private void Initialize()
		{
			SuspendLayout();
			IsMdiContainer = true;
			InitializeStatus();
			Text = titlestr;
			InitializeMenu();
			ShowIcon = false;
			ResumeLayout();
			InitializeCuriousSelectTVForm();
			InitializeCuriousSelectWMITVForm();
			InitializeCuriousSelectFolderTVForm();
		}
		private void InitializeCuriousSelectTVForm()
		{
			if(!ActivateChild(CuriousSelectTVFormClass.titlestr))
			{
				m_selecttvf = new CuriousSelectTVFormClass(m_selectl);
				m_selecttvf.NewSelect += new MDINewSelectEventHandler(NewSelectEvent);
				m_selecttvf.ShowSelect += new MDIShowSelectEventHandler(ShowSelectEvent);
				m_selecttvf.SaveSelect += new MDISaveSelectEventHandler(SaveSelectEvent);
				m_selecttvf.LoadSelect += new MDILoadSelectEventHandler(LoadSelectEvent);
				m_selecttvf.EditSelect += new MDIEditSelectEventHandler(EditSelectEvent);
				m_selecttvf.OpenFile += new MDIOpenFileEventHandler(OpenFileEvent);
				m_selecttvf.MdiParent = this;
				m_selecttvf.Show();
			}
		}
		private void InitializeCuriousSelectWMITVForm()
		{
			if(!ActivateChild(CuriousSelectWMITVFormClass.titlestr))
			{
				m_selectwmitvf = new CuriousSelectWMITVFormClass(m_selectwmil);
				m_selectwmitvf.NewSelect += new MDINewSelectEventHandler(NewSelectWMIEvent);
				m_selectwmitvf.ShowSelect += new MDIShowSelectEventHandler(ShowSelectEvent);
				m_selectwmitvf.SaveSelect += new MDISaveSelectEventHandler(SaveSelectWMIEvent);
				m_selectwmitvf.LoadSelect += new MDILoadSelectEventHandler(LoadSelectWMIEvent);
				m_selectwmitvf.EditSelect += new MDIEditSelectEventHandler(EditSelectWMIEvent);
				m_selectwmitvf.OpenFile += new MDIOpenFileEventHandler(OpenComputerEvent);
				m_selectwmitvf.MdiParent = this;
				m_selectwmitvf.Show();
			}
		}
		private void InitializeCuriousSelectFolderTVForm()
		{
			if(!ActivateChild(CuriousSelectFolderTVFormClass.titlestr))
			{
				m_selectfoldertvf = new CuriousSelectFolderTVFormClass(m_selectfolderl);
				m_selectfoldertvf.NewSelect += new MDINewSelectEventHandler(NewSelectFolderEvent);
				m_selectfoldertvf.ShowSelect += new MDIShowSelectEventHandler(ShowSelectEvent);
				m_selectfoldertvf.SaveSelect += new MDISaveSelectEventHandler(SaveSelectFolderEvent);
				m_selectfoldertvf.LoadSelect += new MDILoadSelectEventHandler(LoadSelectFolderEvent);
				m_selectfoldertvf.EditSelect += new MDIEditSelectEventHandler(EditSelectFolderEvent);
				m_selectfoldertvf.OpenFile += new MDIOpenFileEventHandler(OpenFileEvent);
				m_selectfoldertvf.MdiParent = this;
				m_selectfoldertvf.Show();
			}
		}
		private void InitializeLogForm()
		{
			if(!ActivateChild(OperationLogListForm.titlestr))
			{
				m_logform = new OperationLogListForm(m_ll);
				m_logform.MdiParent = this;
				m_logform.Show();
			}
		}
		private void RefreshCuriousSelectTVForm()
		{
			try
			{
				m_selecttvf.Close();
				m_selecttvf.Dispose();
			}
			catch
			{
			}
			finally
			{
				m_selecttvf = null;
			}
			InitializeCuriousSelectTVForm();
		}
		private void RefreshCuriousSelectWMITVForm()
		{
			try
			{
				m_selectwmitvf.Close();
				m_selectwmitvf.Dispose();
			}
			catch
			{
			}
			finally
			{
				m_selectwmitvf = null;
			}
			InitializeCuriousSelectWMITVForm();
		}
		private void RefreshCuriousSelectFolderTVForm()
		{
			try
			{
				m_selectfoldertvf.Close();
				m_selectfoldertvf.Dispose();
			}
			catch
			{
			}
			finally
			{
				m_selectfoldertvf = null;
			}
			InitializeCuriousSelectFolderTVForm();
		}
		private void RefreshLogForm()
		{
			try
			{
				m_logform.Close();
				m_logform.Dispose();
			}
			catch
			{
			}
			finally
			{
				m_logform = null;
			}
			InitializeLogForm();
		}
		private bool ActivateChild(string provided_title)
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
		private void InitializeStatus()
		{
			m_statusstrip = new StatusStrip();
			m_statusstrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			m_statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
			m_statuslabel.Dock = System.Windows.Forms.DockStyle.Left;
			m_statuslabel.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
			m_statusstrip.Items.Add(m_statuslabel);
			Controls.Add(m_statusstrip);
		}
		private void InitializeMenu()
		{
			m_mymenu = new MenuStrip();
			ContextMenuStrip cmenu = new ContextMenuStrip();
			ToolStripMenuItem viewmenu = new ToolStripMenuItem("&View");
			viewmenu.DropDownItems.Add(CreateViewSelectExplorerMenu(true));
			cmenu.Items.Add(CreateViewSelectExplorerMenu(false));
			viewmenu.DropDownItems.Add(CreateViewSelectWMIExplorerMenu(true));
			cmenu.Items.Add(CreateViewSelectWMIExplorerMenu(false));
			viewmenu.DropDownItems.Add(CreateViewSelectFolderExplorerMenu(true));
			cmenu.Items.Add(CreateViewSelectFolderExplorerMenu(false));
			viewmenu.DropDownItems.Add(CreateViewEventLogMenu(true));
			cmenu.Items.Add(CreateViewEventLogMenu(false));
			
			m_mymenu.Items.Add(viewmenu);
			ToolStripMenuItem toolmenu = new ToolStripMenuItem("&Tool");
			ToolStripMenuItem aamenu = new ToolStripMenuItem("&AA", null, new System.EventHandler(myhandler));
			toolmenu.DropDownItems.Add(aamenu);
			m_mymenu.Items.Add(toolmenu);
			ToolStripMenuItem windowmenu = new ToolStripMenuItem("&Window");
			m_mymenu.Items.Add(windowmenu);
			m_mymenu.MdiWindowListItem = windowmenu;
			m_mymenu.Dock = System.Windows.Forms.DockStyle.Top;
			MainMenuStrip = m_mymenu;
			Controls.Add(m_mymenu);
			ContextMenuStrip = cmenu;
		}
		private ToolStripMenuItem CreateViewSelectExplorerMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&SelectExplorer", null, new System.EventHandler(ViewSelectExplorerMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		private ToolStripMenuItem CreateViewSelectWMIExplorerMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("Select&WMIExplorer", null, new System.EventHandler(ViewSelectWMIExplorerMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.W;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		private ToolStripMenuItem CreateViewSelectFolderExplorerMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("Select&FolderExplorer", null, new System.EventHandler(ViewSelectFolderExplorerMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		private ToolStripMenuItem CreateViewEventLogMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("Event&Log", null, new System.EventHandler(ViewEventLogMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.L;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		private void myhandler(object sender, System.EventArgs e)
		{
			MessageBox.Show(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
		}
		private void ViewSelectExplorerMenu(object sender, System.EventArgs e)
		{
			InitializeCuriousSelectTVForm();
		}
		private void ViewSelectWMIExplorerMenu(object sender, System.EventArgs e)
		{
			InitializeCuriousSelectWMITVForm();
		}
		private void ViewSelectFolderExplorerMenu(object sender, System.EventArgs e)
		{
			InitializeCuriousSelectFolderTVForm();
		}
		private void NewSelectEvent()
		{
			CuriousSelectEditFormClass ef = new CuriousSelectEditFormClass(m_selectl.maxseq + 1);
			if(System.Windows.Forms.DialogResult.OK == ef.ShowDialog())
			{
				m_selectl += ef.select;
				RefreshCuriousSelectTVForm();
			}
		}
		private void NewSelectWMIEvent()
		{
			CuriousSelectWMIEditFormClass ef = new CuriousSelectWMIEditFormClass(m_selectwmil.maxseq + 1);
			if(System.Windows.Forms.DialogResult.OK == ef.ShowDialog())
			{
				m_selectwmil += ef.select;
				RefreshCuriousSelectWMITVForm();
			}
		}
		private void NewSelectFolderEvent()
		{
			CuriousSelectFolderEditFormClass ef = new CuriousSelectFolderEditFormClass(m_selectfolderl.maxseq + 1);
			if(System.Windows.Forms.DialogResult.OK == ef.ShowDialog())
			{
				m_selectfolderl += ef.select;
				RefreshCuriousSelectFolderTVForm();
			}
		}
		private void ShowSelectEvent(CuriousSelectClass provided_select)
		{
			if(!ActivateChild(provided_select.sortkey))
			{
				CuriousSelectResultForm f = new CuriousSelectResultForm(provided_select);
				f.ShowChangedRow += new MDIShowChangedRowEventHandler(ShowChangedRowEvent);
				f.ShowTable += new MDIShowTableEventHandler(ShowTableEvent);
				f.ShowUpdatedRow += new MDIShowUpdatedRowEventHandler(ShowUpdatedRowEvent);
				f.LogMessage += new MDILogMessageEventHandler(MessageOut);
				f.MdiParent = this;
				f.Show();
			}
		}
		private void OpenFileEvent(string provided_filename)
		{
			System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo("explorer", provided_filename);
			System.Diagnostics.Process.Start(si);
		}
		private void OpenComputerEvent(string provided_computername)
		{
			System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo("mmc", "compmgmt.msc /computer=" + provided_computername);
			System.Diagnostics.Process.Start(si);
		}
		private void ShowChangedRowEvent(string provided_title, ACC.RowEClass provided_re)
		{
			if(!ActivateChild(provided_title))
			{
				Util.RowGridFormClass f = new Util.RowGridFormClass(provided_title, provided_re);
				f.MdiParent = this;
				f.Show();
			}
		}
		private void ShowUpdatedRowEvent(string provided_title, ACC.RowLClass provided_rl, string[] provided_diffcols)
		{
			if(!ActivateChild(provided_title))
			{
				CuriousTableGridFormClass f = new CuriousTableGridFormClass(provided_title, provided_rl, provided_diffcols);
				f.MdiParent = this;
				f.Show();
			}
		}
		private void ShowTableEvent(string provided_title, ACC.RowLClass provided_rl)
		{
			if(!ActivateChild(provided_title))
			{
				CuriousTableGridFormClass f = new CuriousTableGridFormClass(provided_title, provided_rl);
				f.MdiParent = this;
				f.Show();
			}
		}
		private void SaveSelectEvent()
		{
			Util.GetFileNameForm f = new Util.GetFileNameForm("file name?", "myselect.xml");
			if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
			{
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				x.AppendChild(m_selectl.GetXml(x));
				x.Save(f.filename);
			}
		}
		private void SaveSelectWMIEvent()
		{
			Util.GetFileNameForm f = new Util.GetFileNameForm("file name?", "myselect.xml");
			try
			{
				if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					x.AppendChild(m_selectwmil.GetXml(x));
					x.Save(f.filename);
					MessageOut("Save succeeded " + f.filename);
				}
				else
				{
					MessageOut("Save cancelled");
				}
			}
			catch
			{
				MessageOut("Save failed");
			}
		}
		private void SaveSelectFolderEvent()
		{
			Util.GetFileNameForm f = new Util.GetFileNameForm("file name?", "myselect.xml");
			try
			{
				if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					x.AppendChild(m_selectfolderl.GetXml(x));
					x.Save(f.filename);
					MessageOut("Save succeeded " + f.filename);
				}
				else
				{
					MessageOut("Save cancelled");
				}
			}
			catch
			{
				MessageOut("Save failed");
			}
		}
		private void LoadSelectEvent()
		{
			Util.GetFileNameForm f = new Util.GetFileNameForm("file name?", "myselect.xml");
			try
			{
				if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					x.Load(f.filename);
					m_selectl = new CuriousLClass(x.DocumentElement);
					RefreshCuriousSelectTVForm();
					MessageOut("Load succeeded " + f.filename);
				}
				else
				{
					MessageOut("Load cancelled");
				}
			}
			catch
			{
				MessageOut("Load failed");
			}
		}
		private void LoadSelectWMIEvent()
		{
			Util.GetFileNameForm f = new Util.GetFileNameForm("file name?", "myselect.xml");
			try
			{
				if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					x.Load(f.filename);
					m_selectwmil = new CuriousLClass(x.DocumentElement);
					RefreshCuriousSelectWMITVForm();
					MessageOut("Load succeeded " + f.filename);
				}
				else
				{
					MessageOut("Load cancelled");
				}
			}
			catch
			{
				MessageOut("Load failed");
			}
		}
		private void LoadSelectFolderEvent()
		{
			Util.GetFileNameForm f = new Util.GetFileNameForm("file name?", "myselect.xml");
			try
			{
				if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					x.Load(f.filename);
					m_selectfolderl = new CuriousLClass(x.DocumentElement);
					RefreshCuriousSelectFolderTVForm();
					MessageOut("Load succeeded " + f.filename);
				}
				else
				{
					MessageOut("Load cancelled");
				}
			}
			catch
			{
				MessageOut("Load failed");
			}
		}
		private void EditSelectEvent(CuriousSelectClass provided_select)
		{
			CuriousSelectEditFormClass f = new CuriousSelectEditFormClass(provided_select);
			try
			{
				if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
				{
					m_selectl = m_selectl - f.select;
					m_selectl = m_selectl + f.select;
					RefreshCuriousSelectTVForm();
					MessageOut("Edit succeeded " + f.select.filename + " " + f.select.sql);
				}
				else
				{
					MessageOut("Edit cancelled");
				}
			}
			catch
			{
				MessageOut("Edit failed");
			}
		}
		private void EditSelectWMIEvent(CuriousSelectClass provided_select)
		{
			CuriousSelectWMIEditFormClass f = new CuriousSelectWMIEditFormClass(provided_select);
			try
			{
				if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
				{
					m_selectwmil -= f.select;
					m_selectwmil += f.select;
					RefreshCuriousSelectWMITVForm();
					MessageOut("Edit succeeded " + f.select.filename + " " + f.select.sql);
				}
				else
				{
					MessageOut("Edit cancelled");
				}
			}
			catch
			{
				MessageOut("Edit failed");
			}
		}
		private void EditSelectFolderEvent(CuriousSelectClass provided_select)
		{
			CuriousSelectFolderEditFormClass f = new CuriousSelectFolderEditFormClass((CuriousSelectFolderClass)provided_select);
			try
			{
				if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
				{
					m_selectfolderl -= f.select;
					m_selectfolderl += f.select;
					RefreshCuriousSelectFolderTVForm();
					MessageOut("Edit succeeded " + f.select.filename + " " + f.select.sql);
				}
				else
				{
					MessageOut("Edit cancelled");
				}
			}
			catch
			{
				MessageOut("Edit failed");
			}
		}
		private void ExportOperationLogEvent(string provided_html)
		{
			Util.GetFileNameForm f = new Util.GetFileNameForm("file name?", "mylog.htm");
			if(System.Windows.Forms.DialogResult.OK == f.ShowDialog())
			{
				System.IO.StreamWriter writer = new System.IO.StreamWriter(f.filename, false);
				writer.Write(provided_html);
				writer.Close();
			}
		}
		private void ViewEventLogMenu(object sender, System.EventArgs e)
		{
			RefreshLogForm();
		}
		private void MessageOut(string provided_message)
		{
			MessageOut("MDI", provided_message);
		}
		private void MessageOut(string provided_classname, string provided_message)
		{
			m_ll = (Util.LogLClass)(m_ll + new Util.LogEClass(m_ll.maxseq + 1, provided_classname, provided_message));
			try
			{
				m_statuslabel.Text = provided_message;
			}
			catch
			{
			}
		}
	}
}
