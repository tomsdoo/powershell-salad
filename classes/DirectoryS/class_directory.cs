namespace DirectoryS
{
	public class DirectoryClass
	{
		string m_distinguishedname;
		public string distinguishedname
		{
			get
			{
				return m_distinguishedname;
			}
		}
		System.DirectoryServices.DirectorySearcher m_searcher;
		public System.DirectoryServices.DirectorySearcher searcher
		{
			get
			{
				return m_searcher;
			}
		}
		public DirectoryClass(string provided_distinguishedname)
		{
			m_distinguishedname = provided_distinguishedname;
			Initialize();
		}
		private void Initialize()
		{
			System.DirectoryServices.DirectoryEntry sr = new System.DirectoryServices.DirectoryEntry("LDAP://" + m_distinguishedname);
			m_searcher = new System.DirectoryServices.DirectorySearcher();
			m_searcher.SearchRoot = sr;
		}
	}
}
