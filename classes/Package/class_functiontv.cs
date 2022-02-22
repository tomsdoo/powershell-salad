namespace Package
{
	public class FunctionComparer : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			FunctionEClass myx = (FunctionEClass)x;
			FunctionEClass myy = (FunctionEClass)y;
			return string.Compare(myx.name, myy.name);
		}
	}
	public class FunctionTN : System.Windows.Forms.TreeNode
	{
		public const string ClassName = "FunctionTN";
		FunctionEClass m_functione;
		public FunctionEClass functione
		{
			get
			{
				return m_functione;
			}
		}
		public FunctionTN(FunctionEClass provided_functione)
		{
			m_functione = provided_functione;
			Initialize();
		}
		private void Initialize()
		{
			Text = m_functione.name;
			Name = m_functione.seq.ToString();
		}
	}
	public class FunctionTV : Util.TVBaseClass
	{
		FunctionLClass m_functionl;
		public FunctionLClass functionl
		{
			get
			{
				return m_functionl;
			}
		}
		public FunctionTV(FunctionLClass provided_functionl) : base()
		{
			m_functionl = provided_functionl;
			Initialize();
		}
		protected override void Initialize()
		{
			SuspendLayout();
			BeginUpdate();
			Nodes.Clear();
			if(null != m_functionl.e)
			{
				System.Array.Sort(m_functionl.e, new FunctionComparer());
				for(int icnt = 0; icnt < m_functionl.e.Length; icnt++)
				{
					FunctionEClass fe = (FunctionEClass)(m_functionl.e[icnt]);
					FunctionTN fn = new FunctionTN(fe);
					Nodes.Add(fn);
				}
			}
			EndUpdate();
			ResumeLayout();
		}
	}
	public class FunctionTVForm : Util.TVFormBaseClass
	{
		public const string titlestr = "Functions";
		FunctionLClass m_functionl;
		public FunctionLClass functionl
		{
			get
			{
				return m_functionl;
			}
		}
		public FunctionTVForm(FunctionLClass provided_functionl)
		{
			Text = titlestr;
			m_functionl = provided_functionl;
			Initialize(new FunctionTV(m_functionl));
		}
	}
}
