namespace Package
{
	public class PackageMDIForm : Util.MDIBaseClass
	{
		ClassLClass m_classl;
		ClassTVForm m_classform;
		FunctionLClass m_functionl;
		FunctionTVForm m_functionform;
		CSharpNSLClass m_csharpnsl;
		CSharpNSTVForm m_csharpnsform;
		PackClass m_pack;
		PackTVForm m_packform;
		SALADEnvClass m_saladenv;
		public PackageMDIForm(string provided_rootfolder)
		{
			m_saladenv = new SALADEnvClass(provided_rootfolder);
			Initialize();
		}
		public PackageMDIForm(SALADEnvClass provided_env)
		{
			m_saladenv = provided_env;
			Initialize();
		}
		protected override void Initialize()
		{
			SuspendLayout();
			m_classl = m_saladenv.classl;
			m_functionl = m_saladenv.functionl;
			m_csharpnsl = m_saladenv.csharpnsl;
			m_pack = new PackClass(new ClassLClass(), new FunctionLClass(), new CSharpNSLClass());
			RefreshUSOClassForm();
			RefreshFunctionForm();
			RefreshCSharpNSForm();
			RefreshPackageForm();
			ResumeLayout();
		}
		protected override void InitializeMenu()
		{
			base.InitializeMenu();
			m_viewmenu.DropDownItems.Add(CreateViewUSOClassMenu(true));
			m_cmenu.Items.Add(CreateViewUSOClassMenu(false));
			m_viewmenu.DropDownItems.Add(CreateViewFunctionMenu(true));
			m_cmenu.Items.Add(CreateViewFunctionMenu(false));
			m_viewmenu.DropDownItems.Add(CreateViewCSharpNSMenu(true));
			m_cmenu.Items.Add(CreateViewCSharpNSMenu(false));
			m_viewmenu.DropDownItems.Add(CreateViewPackageMenu(true));
			m_cmenu.Items.Add(CreateViewPackageMenu(false));
		}
		protected override void InitializeStatus(){}
		protected virtual void InitializeUSOClassForm()
		{
			if(!ActivateChild(ClassTVForm.titlestr))
			{
				m_classform = new ClassTVForm(m_classl);
				m_classform.MdiParent = this;
				m_classform.TVSelected += new Util.TVSelectedEventHandler(ResourceTVSelectedEvent);
				m_classform.Show();
			}
		}
		protected virtual void RefreshUSOClassForm()
		{
			try
			{
				m_classform.Close();
				m_classform.Dispose();
			}
			catch{}
			finally
			{
				m_classform = null;
			}
			InitializeUSOClassForm();
		}
		protected virtual void InitializeFunctionForm()
		{
			if(!ActivateChild(FunctionTVForm.titlestr))
			{
				m_functionform = new FunctionTVForm(m_functionl);
				m_functionform.MdiParent = this;
				m_functionform.TVSelected += new Util.TVSelectedEventHandler(ResourceTVSelectedEvent);
				m_functionform.Show();
			}
		}
		protected virtual void RefreshFunctionForm()
		{
			try
			{
				m_functionform.Close();
				m_functionform.Dispose();
			}
			catch{}
			finally
			{
				m_functionform = null;
			}
			InitializeFunctionForm();
		}
		protected virtual void InitializeCSharpNSForm()
		{
			if(!ActivateChild(CSharpNSTVForm.titlestr))
			{
				m_csharpnsform = new CSharpNSTVForm(m_csharpnsl);
				m_csharpnsform.MdiParent = this;
				m_csharpnsform.TVSelected += new Util.TVSelectedEventHandler(ResourceTVSelectedEvent);
				m_csharpnsform.Show();
			}
		}
		protected virtual void RefreshCSharpNSForm()
		{
			try
			{
				m_csharpnsform.Close();
				m_csharpnsform.Dispose();
			}
			catch{}
			finally
			{
				m_csharpnsform = null;
			}
			InitializeCSharpNSForm();
		}
		protected virtual void InitializePackageForm()
		{
			if(!ActivateChild(PackTVForm.titlestr))
			{
				m_packform = new PackTVForm(m_pack);
				m_packform.MdiParent = this;
				m_packform.TVSelected += new Util.TVSelectedEventHandler(PackageTVSelectedEvent);
				m_packform.PackLoaded += new PackTVFormPackLoadedEventHandler(PackageLoadedEvent);
				m_packform.Show();
			}
		}
		protected virtual void RefreshPackageForm()
		{
			try
			{
				m_packform.Close();
				m_packform.Dispose();
			}
			catch{}
			finally
			{
				m_packform = null;
			}
			InitializePackageForm();
		}
		protected virtual ToolStripMenuItem CreateViewUSOClassMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&USOClasses", null, new System.EventHandler(ViewUSOClassMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.U;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateViewFunctionMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Functions", null, new System.EventHandler(ViewFunctionMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateViewCSharpNSMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&C#Name" + "spaces", null, new System.EventHandler(ViewCSharpNSMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.C;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateViewPackageMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Package", null, new System.EventHandler(ViewPackageMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.P;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual void ViewUSOClassMenu(object sender, System.EventArgs e)
		{
			RefreshUSOClassForm();
		}
		protected virtual void ViewFunctionMenu(object sender, System.EventArgs e)
		{
			RefreshFunctionForm();
		}
		protected virtual void ViewCSharpNSMenu(object sender, System.EventArgs e)
		{
			RefreshCSharpNSForm();
		}
		protected virtual void ViewPackageMenu(object sender, System.EventArgs e)
		{
			RefreshPackageForm();
		}
		protected virtual void ResourceTVSelectedEvent(System.Windows.Forms.TreeNode provided_selected)
		{
			ClassLClass cl = m_pack.classl;
			CSharpNSLClass nsl = m_pack.csharpnsl;
			FunctionLClass fl = m_pack.functionl;
			switch(provided_selected.GetType().Name)
			{
				case ClassTN.ClassName:
					{
						ClassTN cnode = (ClassTN)provided_selected;
						cl = (ClassLClass)(cl + cnode.classe);
						break;
					}
				case CSharpNSTN.ClassName:
					{
						CSharpNSTN nnode = (CSharpNSTN)provided_selected;
						nsl = (CSharpNSLClass)(nsl + nnode.csharpnse);
						break;
					}
				case FunctionTN.ClassName:
					{
						FunctionTN fnode = (FunctionTN)provided_selected;
						fl = (FunctionLClass)(fl + fnode.functione);
						break;
					}
				default:
					{
						break;
					}
			}
			m_pack = new PackClass(cl, fl, nsl);
			try
			{
				m_packform.FeedPack(m_pack);
			}
			catch
			{
				RefreshPackageForm();
			}
		}
		protected virtual void PackageTVSelectedEvent(System.Windows.Forms.TreeNode provided_selected)
		{
			ClassLClass cl = m_pack.classl;
			CSharpNSLClass nsl = m_pack.csharpnsl;
			FunctionLClass fl = m_pack.functionl;
			switch(provided_selected.GetType().Name)
			{
				case ClassTN.ClassName:
					{
						ClassTN cnode = (ClassTN)provided_selected;
						cl = (ClassLClass)(cl - cnode.classe);
						break;
					}
				case CSharpNSTN.ClassName:
					{
						CSharpNSTN nnode = (CSharpNSTN)provided_selected;
						nsl = (CSharpNSLClass)(nsl - nnode.csharpnse);
						break;
					}
				case FunctionTN.ClassName:
					{
						FunctionTN fnode = (FunctionTN)provided_selected;
						fl = (FunctionLClass)(fl - fnode.functione);
						break;
					}
				default:
					{
						break;
					}
			}
			m_pack = new PackClass(cl, fl, nsl);
			try
			{
				m_packform.FeedPack(m_pack);
			}
			catch
			{
				RefreshPackageForm();
			}
		}
		protected virtual void PackageLoadedEvent(PackClass provided_pack)
		{
			m_pack = provided_pack;
			m_pack.Restructure(m_saladenv.definedfolders.rootfolder);
			try
			{
				m_packform.FeedPack(m_pack);
			}
			catch
			{
				RefreshPackageForm();
			}
		}
	}
}
