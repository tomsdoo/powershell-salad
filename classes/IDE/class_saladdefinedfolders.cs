namespace IDE
{
	public class saladdefinedfolders
	{
		string m_rootfolder;
		public string rootfolder
		{
			get
			{
				return m_rootfolder;
			}
		}
		string m_bootstrapfolder;
		public string bootstrapfolder
		{
			get
			{
				return m_bootstrapfolder;
			}
		}
		string m_classesfolder;
		public string classesfolder
		{
			get
			{
				return classesfolder;
			}
		}
		string m_logfolder;
		public string logfolder
		{
			get
			{
				return m_logfolder;
			}
		}
		string m_definitionfolder;
		public string definitionfolder
		{
			get
			{
				return m_definitionfolder;
			}
		}
		string m_systemfolder;
		public string systemfolder
		{
			get
			{
				return m_systemfolder;
			}
		}
		string m_codefolder;
		public string codefolder
		{
			get
			{
				return m_codefolder;
			}
		}
		string m_scriptsfolder;
		public string scriptsfolder
		{
			get
			{
				return m_scriptsfolder;
			}
		}
		string m_backupfolder;
		public string backupfolder
		{
			get
			{
				return m_backupfolder;
			}
		}
		const string STR_BOOTSTRAP = "bootstrap";
		const string STR_CLASSES = "classes";
		const string STR_LOG = "log";
		const string STR_DEFINITION = "definition";
		const string STR_SYSTEM = "system";
		const string STR_CODE = "code";
		const string STR_SCRIPTS = "scripts";
		const string STR_BACKUP = "backup";
		string m_sepchar
		{
			get
			{
				return System.IO.Path.DirectorySeparatorChar.ToString();
			}
		}
		public saladdefinedfolders(string provided_rootfolder)
		{
			m_rootfolder = provided_rootfolder;
			if(!string.IsNullOrEmpty(System.IO.Path.GetFileName(m_rootfolder)))
			{
				m_rootfolder += m_sepchar;
			}
			initialize();
		}
		public saladdefinedfolders(saladdefinedfolders provided_obj)
		{
			m_rootfolder = provided_obj.rootfolder;
			initialize();
		}
		private void initialize()
		{
			m_bootstrapfolder = m_rootfolder + STR_BOOTSTRAP + m_sepchar;
			m_classesfolder = m_rootfolder + STR_CLASSES + m_sepchar;
			m_logfolder = m_rootfolder + STR_LOG + m_sepchar;
			m_definitionfolder = m_rootfolder + STR_DEFINITION + m_sepchar;
			m_systemfolder = m_definitionfolder + STR_SYSTEM + m_sepchar;
			m_codefolder = m_definitionfolder + STR_CODE + m_sepchar;
			m_scriptsfolder = m_rootfolder + STR_SCRIPTS + m_sepchar;
			m_backupfolder = m_rootfolder + STR_BACKUP + m_sepchar;
		}
	}
}
