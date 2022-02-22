namespace IDE
{
	public class saladenv
	{
		saladdefinedfolders m_definedfolders;
		public saladdefinedfolders definedfolders
		{
			get
			{
				return m_definedfolders;
			}
		}
		classmanager m_classman;
		public classmanager classman
		{
			get
			{
				return m_classman;
			}
		}
		functionmanager m_functionman;
		public functionmanager functionman
		{
			get
			{
				return m_functionman;
			}
		}
		psenv m_pse;
		public psenv pse
		{
			get
			{
				return m_pse;
			}
		}
		public saladenv(string provided_rootfolder, string provided_cmdletstr)
		{
			m_definedfolders = new saladdefinedfolders(provided_rootfolder);
			m_classman = new classmanager(m_definedfolders.codefolder);
			m_functionman = new functionmanager(m_definedfolders.scriptsfolder);
			m_pse = new psenv(provided_cmdletstr);
		}
	}
}
