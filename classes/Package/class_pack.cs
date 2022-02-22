namespace Package
{
	public class PackClass : AbstLib.EBaseClass
	{
		public const string AttributeNameClassL = "classl";
		public const string AttributeNameFunctionL = "functionl";
		public const string AttributeNameCSharpNSL = "csharpnsl";
		ClassLClass m_classl;
		public ClassLClass classl
		{
			get
			{
				return m_classl;
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
		CSharpNSLClass m_csharpnsl;
		public CSharpNSLClass csharpnsl
		{
			get
			{
				return m_csharpnsl;
			}
		}
		public PackClass(ClassLClass provided_classl, FunctionLClass provided_functionl, CSharpNSLClass provided_csharpnsl) : base(0)
		{
			m_classl = provided_classl;
			m_functionl = provided_functionl;
			m_csharpnsl = provided_csharpnsl;
		}
		public PackClass(PackClass provided_obj) : base(provided_obj)
		{
			m_classl = provided_obj.classl;
			m_functionl = provided_obj.functionl;
			m_csharpnsl = provided_obj.csharpnsl;
		}
		public PackClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			string strclassl = provided_element.GetAttribute(AttributeNameClassL);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(strclassl);
			m_classl = new ClassLClass((System.Xml.XmlElement)(x.DocumentElement));
			string strfunctionl = provided_element.GetAttribute(AttributeNameFunctionL);
			x = new System.Xml.XmlDocument();
			x.LoadXml(strfunctionl);
			m_functionl = new FunctionLClass((System.Xml.XmlElement)(x.DocumentElement));
			string strcsharpnsl = provided_element.GetAttribute(AttributeNameCSharpNSL);
			x = new System.Xml.XmlDocument();
			x.LoadXml(strcsharpnsl);
			m_csharpnsl = new CSharpNSLClass((System.Xml.XmlElement)(x.DocumentElement));
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameClassL, m_classl.xmlstr);
			ret.SetAttribute(AttributeNameFunctionL, m_functionl.xmlstr);
			ret.SetAttribute(AttributeNameCSharpNSL, m_csharpnsl.xmlstr);
			return ret;
		}
		public void Restructure(string provided_rootfolder)
		{
			int icnt = 0;
			int jcnt = 0;
			ClassLClass tempcl = new ClassLClass();
			if(null != m_classl.e)
			{
				for(icnt = 0; icnt < m_classl.e.Length; icnt++)
				{
					ClassEClass ce = (ClassEClass)(m_classl.e[icnt]);
					MethodLClass tempml = new MethodLClass();
					if(null != ce.methodl.e)
					{
						for(jcnt = 0; jcnt < ce.methodl.e.Length; jcnt++)
						{
							MethodEClass tempm = (MethodEClass)(ce.methodl.e[jcnt]);
							MethodEClass tempme = new MethodEClass(tempm.seq, provided_rootfolder, tempm.filename, tempm.classname, tempm.name, tempm.linel);
							tempml = (MethodLClass)(tempml + tempme);
						}
					}
					ClassEClass tempce = new ClassEClass(ce.seq, ce.name, tempml);
					tempcl = (ClassLClass)(tempcl + tempce);
				}
			}
			FunctionLClass tempfl = new FunctionLClass();
			if(null != m_functionl.e)
			{
				for(icnt = 0; icnt < m_functionl.e.Length; icnt++)
				{
					FunctionEClass tempf = (FunctionEClass)(m_functionl.e[icnt]);
					FunctionEClass tempfe = new FunctionEClass(tempf.seq, provided_rootfolder, tempf.filename, tempf.name, tempf.linel);
					tempfl = (FunctionLClass)(tempfl + tempfe);
				}
			}
			CSharpNSLClass tempcnsl = new CSharpNSLClass();
			if(null != m_csharpnsl.e)
			{
				for(icnt = 0; icnt < m_csharpnsl.e.Length; icnt++)
				{
					CSharpNSEClass ne = (CSharpNSEClass)(m_csharpnsl.e[icnt]);
					AbstLib.RelFileContentLClass tempfcl = new AbstLib.RelFileContentLClass();
					if(null != ne.filecontentl.e)
					{
						for(jcnt = 0; jcnt < ne.filecontentl.e.Length; jcnt++)
						{
							AbstLib.RelFileContentEClass tempfc = (AbstLib.RelFileContentEClass)(ne.filecontentl.e[jcnt]);
							AbstLib.RelFileContentEClass tempfce = new AbstLib.RelFileContentEClass(tempfc.seq, provided_rootfolder, tempfc.filename, tempfc.linel);
							tempfcl = (AbstLib.RelFileContentLClass)(tempfcl + tempfce);
						}
					}
					CSharpNSEClass nne = new CSharpNSEClass(ne.seq, ne.rootfolder, ne.ns, tempfcl);
					tempcnsl = (CSharpNSLClass)(tempcnsl + nne);
				}
			}
			m_classl = tempcl;
			m_functionl = tempfl;
			m_csharpnsl = tempcnsl;
		}
		public void Import(string provided_rootfolder)
		{
			int icnt = 0;
			int jcnt = 0;
			if(null != m_classl.e)
			{
				for(icnt = 0; icnt < m_classl.e.Length; icnt++)
				{
					ClassEClass ce = (ClassEClass)(m_classl.e[icnt]);
					if(null != ce.methodl.e)
					{
						for(jcnt = 0; jcnt < ce.methodl.e.Length; jcnt++)
						{
							MethodEClass tempm = (MethodEClass)(ce.methodl.e[jcnt]);
							tempm.Write();
						}
					}
				}
			}
			if(null != m_functionl.e)
			{
				for(icnt = 0; icnt < m_functionl.e.Length; icnt++)
				{
					FunctionEClass tempf = (FunctionEClass)(m_functionl.e[icnt]);
					tempf.Write();
				}
			}
			if(null != m_csharpnsl.e)
			{
				for(icnt = 0; icnt < m_csharpnsl.e.Length; icnt++)
				{
					CSharpNSEClass ne = (CSharpNSEClass)(m_csharpnsl.e[icnt]);
					if(null != ne.filecontentl.e)
					{
						for(jcnt = 0; jcnt < ne.filecontentl.e.Length; jcnt++)
						{
							AbstLib.RelFileContentEClass tempfc = (AbstLib.RelFileContentEClass)(ne.filecontentl.e[jcnt]);
							tempfc.Write();
						}
					}
				}
			}
		}
	}
}
