namespace Package
{
	public class SALADEnvClass
	{
		SALADDefinedFoldersClass m_definedfolders;
		public SALADDefinedFoldersClass definedfolders
		{
			get
			{
				return m_definedfolders;
			}
		}
		FunctionLClass m_functionl;
		public FunctionLClass functionl
		{
			get
			{
				return m_functionl;
			}
		}
		ClassLClass m_classl;
		public ClassLClass classl
		{
			get
			{
				return m_classl;
			}
		}
		NetClassLClass m_netclassl;
		public NetClassLClass netclassl
		{
			get
			{
				return m_netclassl;
			}
		}
		CSharpNSLClass m_csharpnsl;
		public CSharpNSLClass csharpnsl
		{
			get
			{
				return m_csharpnsl;
			}
		}
		public SALADEnvClass(string provided_rootfolder)
		{
			m_definedfolders = new SALADDefinedFoldersClass(provided_rootfolder);
			InitializeCSharpNSL();
			InitializeFunctionL();
			InitializeClassL();
			InitializeNetClassL();
		}
		private void InitializeFunctionL()
		{
			m_functionl = new FunctionLClass();
			foreach(string fn in System.IO.Directory.GetFiles(m_definedfolders.scriptsfolder, "*.ps1", System.IO.SearchOption.AllDirectories))
			{
					m_functionl = (FunctionLClass)(m_functionl + new FunctionEClass(m_functionl.maxseq + 1, m_definedfolders.rootfolder, fn.Replace(m_definedfolders.rootfolder, string.Empty)));
			}
			while(true)
			{
				System.Threading.Thread.Sleep(10);
				if(m_functionl.binitialized)
				{
					break;
				}
			}
		}
		private void InitializeClassL()
		{
			MethodLClass tempml = new MethodLClass();
			foreach(string fn in System.IO.Directory.GetFiles(m_definedfolders.codefolder, "*.code", System.IO.SearchOption.AllDirectories))
			{
				tempml = (MethodLClass)(tempml + new MethodEClass(tempml.maxseq + 1, m_definedfolders.rootfolder, fn.Replace(m_definedfolders.rootfolder, string.Empty)));
			}
			while(true)
			{
				System.Threading.Thread.Sleep(10);
				if(tempml.binitialized)
				{
					break;
				}
			}
			m_classl = new ClassLClass();
			if(null != tempml.e)
			{
				for(int icnt = 0; icnt < tempml.e.Length; icnt++)
				{
					MethodEClass tempm = (MethodEClass)(tempml.e[icnt]);
					m_classl = (ClassLClass)(m_classl + tempm);
				}
			}
		}
		private void InitializeNetClassL()
		{
			m_netclassl = new NetClassLClass();
			AbstLib.FileContentLClass fcl = new AbstLib.FileContentLClass();
			foreach(string fn in System.IO.Directory.GetFiles(m_definedfolders.classesfolder, "*.cs", System.IO.SearchOption.AllDirectories))
			{
				fcl = (AbstLib.FileContentLClass)(fcl + new AbstLib.FileContentEClass(fcl.maxseq + 1, fn));
			}
			while(true)
			{
				System.Threading.Thread.Sleep(10);
				if(fcl.binitialized)
				{
					break;
				}
			}
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			if(null != fcl.e)
			{
				for(int icnt = 0; icnt < fcl.e.Length; icnt++)
				{
					AbstLib.FileContentEClass fe = (AbstLib.FileContentEClass)(fcl.e[icnt]);
					sb.Append(fe.linel.strdata);
				}
			}
			string saladstr = "SALAD";
			string periodstr = ".";
			string nsstr = "name" + "space";
			string pubcstr = "public " + "class ";
			string currentns = string.Empty;
			foreach(string line in (sb.ToString().Split(System.Environment.NewLine.ToCharArray())))
			{
				if(-1 != line.IndexOf(nsstr))
				{
					if(nsstr == line.Substring(0, nsstr.Length))
					{
						string myns = line.Replace((nsstr + " "), string.Empty);
						currentns = saladstr + periodstr + myns + periodstr;
					}
				}
				else
				{
					if(-1 != line.IndexOf(pubcstr))
					{
						string myl = line.Replace("\t", string.Empty).Replace(pubcstr, string.Empty).Replace(" ", string.Empty);
						if(-1 != myl.IndexOf(':'))
						{
							myl = myl.Substring(0, myl.IndexOf(':'));
						}
						string fullnam = currentns + myl;
						m_netclassl = (NetClassLClass)(m_netclassl + new NetClassEClass(m_netclassl.maxseq + 1, fullnam));
					}
				}
			}
		}
		private void InitializeCSharpNSL()
		{
			m_csharpnsl = new CSharpNSLClass();
			foreach(string de in System.IO.Directory.GetDirectories(m_definedfolders.classesfolder))
			{
				m_csharpnsl = (CSharpNSLClass)(m_csharpnsl + new CSharpNSEClass(m_csharpnsl.maxseq + 1, m_definedfolders.rootfolder, de));
			}
		}
	}
}
