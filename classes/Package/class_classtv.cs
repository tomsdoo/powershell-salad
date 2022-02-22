namespace Package
{
	public class ClassComparer : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			ClassEClass myx = (ClassEClass)x;
			ClassEClass myy = (ClassEClass)y;
			return string.Compare(myx.name, myy.name);
		}
	}
	public class ClassTN : System.Windows.Forms.TreeNode
	{
		public const string ClassName = "ClassTN";
		ClassEClass m_classe;
		public ClassEClass classe
		{
			get
			{
				return m_classe;
			}
		}
		public ClassTN(ClassEClass provided_classe)
		{
			m_classe = provided_classe;
			Initialize();
		}
		private void Initialize()
		{
			Text = m_classe.name;
			Name = m_classe.seq.ToString();
		}
	}
	public class ClassTV : Util.TVBaseClass
	{
		ClassLClass m_classl;
		public ClassLClass classl
		{
			get
			{
				return m_classl;
			}
		}
		public ClassTV(ClassLClass provided_classl) : base()
		{
			m_classl = provided_classl;
			Initialize();
		}
		protected override void Initialize()
		{
			SuspendLayout();
			BeginUpdate();
			Nodes.Clear();
			if(null != m_classl.e)
			{
				System.Array.Sort(m_classl.e, new ClassComparer());
				for(int icnt = 0; icnt < m_classl.e.Length; icnt++)
				{
					ClassEClass ce = (ClassEClass)(m_classl.e[icnt]);
					ClassTN cn = new ClassTN(ce);
					Nodes.Add(cn);
				}
			}
			EndUpdate();
			ResumeLayout();
		}
	}
	public class ClassTVForm : Util.TVFormBaseClass
	{
		public const string titlestr = "Classes";
		ClassLClass m_classl;
		public ClassLClass classl
		{
			get
			{
				return m_classl;
			}
		}
		public ClassTVForm(ClassLClass provided_classl)
		{
			Text = titlestr;
			m_classl = provided_classl;
			Initialize(new ClassTV(m_classl));
		}
	}
}
