namespace Assembly
{
	public class AssemblyEClass : AbstLib.EBaseClass
	{
		TypeLClass m_typel;
		public TypeLClass typel
		{
			get
			{
				return m_typel;
			}
		}
		public AssemblyEClass(int provided_seq, System.Reflection.Assembly provided_assembly) : base(provided_seq)
		{
			m_typel = new TypeLClass();
			foreach(System.Type mt in provided_assembly.GetTypes())
			{
				if(mt.IsClass)
				{
					m_typel = (TypeLClass)(m_typel + new TypeEClass(m_typel.maxseq + 1, mt));
				}
			}
		}
		public void OutHTML(string provided_folder)
		{
			string myfolder = provided_folder;
			if(!string.IsNullOrEmpty(System.IO.Path.GetFileName(myfolder)))
			{
				myfolder += System.IO.Path.DirectorySeparatorChar.ToString();
			}
			m_typel.OutHTML(myfolder, m_typel.classnames);
		}
	}
}
