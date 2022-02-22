namespace Package
{
	public class PackTV : Util.TVBaseClass
	{
		PackClass m_pack;
		public PackClass pack
		{
			get
			{
				return m_pack;
			}
		}
		public PackTV(PackClass provided_pack) : base()
		{
			m_pack = provided_pack;
			Initialize();
		}
		public void FeedPack(PackClass provided_pack)
		{
			m_pack = provided_pack;
			Initialize();
		}
		protected override void Initialize()
		{
			int icnt = 0;
			SuspendLayout();
			BeginUpdate();
			Nodes.Clear();
			TreeNode csharpnsnode = new TreeNode("C# Name" + "spaces");
			Nodes.Add(csharpnsnode);
			if(null != m_pack.csharpnsl.e)
			{
				System.Array.Sort(m_pack.csharpnsl.e, new CSharpNSComparer());
				for(icnt = 0; icnt < m_pack.csharpnsl.e.Length; icnt++)
				{
					CSharpNSEClass nse = (CSharpNSEClass)(m_pack.csharpnsl.e[icnt]);
					csharpnsnode.Nodes.Add(new CSharpNSTN(nse));
				}
			}
			TreeNode functionnode = new TreeNode("Functions");
			Nodes.Add(functionnode);
			if(null != m_pack.functionl.e)
			{
				System.Array.Sort(m_pack.functionl.e, new FunctionComparer());
				for(icnt = 0; icnt < m_pack.functionl.e.Length; icnt++)
				{
					FunctionEClass fe = (FunctionEClass)(m_pack.functionl.e[icnt]);
					functionnode.Nodes.Add(new FunctionTN(fe));
				}
			}
			TreeNode classnode = new TreeNode("User-Scripted-Object classes");
			Nodes.Add(classnode);
			if(null != m_pack.classl.e)
			{
				System.Array.Sort(m_pack.classl.e, new ClassComparer());
				for(icnt = 0; icnt < m_pack.classl.e.Length; icnt++)
				{
					ClassEClass ce = (ClassEClass)(m_pack.classl.e[icnt]);
					classnode.Nodes.Add(new ClassTN(ce));
				}
			}
			EndUpdate();
			ResumeLayout();
		}
	}
	public delegate void PackTVFormPackLoadedEventHandler(PackClass provided_pack);
	public class PackTVForm : Util.TVFormBaseClass
	{
		public event PackTVFormPackLoadedEventHandler PackLoaded;
		public const string titlestr = "Package";
		PackClass m_pack;
		public PackClass pack
		{
			get
			{
				return m_pack;
			}
		}
		public PackTVForm(PackClass provided_pack)
		{
			Text = titlestr;
			m_pack = provided_pack;
			Initialize(new PackTV(m_pack));
		}
		public void FeedPack(PackClass provided_pack)
		{
			m_pack = provided_pack;
			((PackTV)m_tv).FeedPack(m_pack);
		}
		protected override void InitializeMenu()
		{
			ToolStripMenuItem packagemenu = new ToolStripMenuItem("&Package");
			m_mymenu.Items.Add(packagemenu);
			ToolStripMenuItem savem = new ToolStripMenuItem("&Save", null, new System.EventHandler(SaveMenu));
			savem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
			savem.ShowShortcutKeys = true;
			packagemenu.DropDownItems.Add(savem);
			m_cmenu.Items.Add(new ToolStripMenuItem("&Save", null, new System.EventHandler(SaveMenu)));
			ToolStripMenuItem loadm = new ToolStripMenuItem("&Load", null, new System.EventHandler(LoadMenu));
			loadm.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
			loadm.ShowShortcutKeys = true;
			packagemenu.DropDownItems.Add(loadm);
			m_cmenu.Items.Add(new ToolStripMenuItem("&Load", null, new System.EventHandler(LoadMenu)));
			ToolStripMenuItem detailm = new ToolStripMenuItem("&Detail", null, new System.EventHandler(DetailMenu));
			detailm.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D;
			detailm.ShowShortcutKeys = true;
			packagemenu.DropDownItems.Add(detailm);
			m_cmenu.Items.Add(new ToolStripMenuItem("&Detail", null, new System.EventHandler(DetailMenu)));
		}
		private void SaveMenu(object sender, System.EventArgs e)
		{
			Util.GetFileNameForm ff = new Util.GetFileNameForm("file name?", "mypackage.xml");
			if(System.Windows.Forms.DialogResult.OK == ff.ShowDialog())
			{
				System.IO.StreamWriter writer = new System.IO.StreamWriter(ff.filename, false);
				writer.Write(m_pack.xmlstr);
				writer.Close();
			}
		}
		private void LoadMenu(object sender, System.EventArgs e)
		{
			Util.GetFileNameForm ff = new Util.GetFileNameForm("file name?", "mypackage.xml");
			if(System.Windows.Forms.DialogResult.OK == ff.ShowDialog())
			{
				System.IO.StreamReader reader = new System.IO.StreamReader(ff.filename);
				string strdata = reader.ReadToEnd();
				reader.Close();
				if(!string.IsNullOrEmpty(strdata))
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					x.LoadXml(strdata);
					System.Xml.XmlElement ele = (System.Xml.XmlElement)(x.DocumentElement);
					m_pack = new PackClass(ele);
					if(null != PackLoaded)
					{
						PackLoaded(m_pack);
					}
				}
			}
		}
		private void DetailMenu(object sender, System.EventArgs e)
		{
			int count = 0;
			count += m_pack.classl.filecount;
			count += m_pack.functionl.filecount;
			count += m_pack.csharpnsl.filecount;
			string[] strl = new string[count];
			int idx = 0;
			int icnt = 0;
			string nfilestr = "New   ";
			string efilestr = "Exists";
			string myfn = string.Empty;
			string[] cfnl = m_pack.classl.filenames;
			if(null != cfnl)
			{
				for(icnt = 0; icnt < cfnl.Length; icnt++)
				{
					myfn = cfnl[icnt];
					if(System.IO.File.Exists(myfn))
					{
						strl[idx] = efilestr + "\t" + myfn;
					}
					else
					{
						strl[idx] = nfilestr + "\t" + myfn;
					}
					idx++;
				}
			}
			string[] ffnl = m_pack.functionl.filenames;
			if(null != ffnl)
			{
				for(icnt = 0; icnt < ffnl.Length; icnt++)
				{
					myfn = ffnl[icnt];
					if(System.IO.File.Exists(myfn))
					{
						strl[idx] = efilestr + "\t" + myfn;
					}
					else
					{
						strl[idx] = nfilestr + "\t" + myfn;
					}
					idx++;
				}
			}
			string[] sfnl = m_pack.csharpnsl.filenames;
			if(null != sfnl)
			{
				for(icnt = 0; icnt < sfnl.Length; icnt++)
				{
					myfn = sfnl[icnt];
					if(System.IO.File.Exists(myfn))
					{
						strl[idx] = efilestr + "\t" + myfn;
					}
					else
					{
						strl[idx] = nfilestr + "\t" + myfn;
					}
					idx++;
				}
			}
			(new PackDetailLBForm(strl)).ShowDialog();
		}
	}
}
