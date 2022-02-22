namespace Package
{
	public class CSharpNSComparer : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			CSharpNSEClass myx = (CSharpNSEClass)x;
			CSharpNSEClass myy = (CSharpNSEClass)y;
			return string.Compare(myx.ns, myy.ns);
		}
	}
	public class CSharpNSTN : System.Windows.Forms.TreeNode
	{
		public const string ClassName = "CSharpNSTN";
		CSharpNSEClass m_csharpnse;
		public CSharpNSEClass csharpnse
		{
			get
			{
				return m_csharpnse;
			}
		}
		public CSharpNSTN(CSharpNSEClass provided_csharpnse)
		{
			m_csharpnse = provided_csharpnse;
			Initialize();
		}
		private void Initialize()
		{
			Text = m_csharpnse.ns;
			Name = m_csharpnse.seq.ToString();
		}
	}
	public class CSharpNSTV : Util.TVBaseClass
	{
		CSharpNSLClass m_csharpnsl;
		public CSharpNSLClass csharpnsl
		{
			get
			{
				return m_csharpnsl;
			}
		}
		public CSharpNSTV(CSharpNSLClass provided_csharpnsl) : base()
		{
			m_csharpnsl = provided_csharpnsl;
			Initialize();
		}
		protected override void Initialize()
		{
			SuspendLayout();
			BeginUpdate();
			Nodes.Clear();
			if(null != m_csharpnsl.e)
			{
				System.Array.Sort(m_csharpnsl.e, new CSharpNSComparer());
				for(int icnt = 0; icnt < m_csharpnsl.e.Length; icnt++)
				{
					CSharpNSEClass nse = (CSharpNSEClass)(m_csharpnsl.e[icnt]);
					CSharpNSTN nstn = new CSharpNSTN(nse);
					Nodes.Add(nstn);
				}
			}
			EndUpdate();
			ResumeLayout();
		}
	}
	public class CSharpNSTVForm : Util.TVFormBaseClass
	{
		public const string titlestr = "C#Name" + "spaces";
		CSharpNSLClass m_csharpnsl;
		public CSharpNSLClass csharpnsl
		{
			get
			{
				return m_csharpnsl;
			}
		}
		public CSharpNSTVForm(CSharpNSLClass provided_csharpnsl)
		{
			Text = titlestr;
			m_csharpnsl = provided_csharpnsl;
			Initialize(new CSharpNSTV(m_csharpnsl));
		}
	}
}
