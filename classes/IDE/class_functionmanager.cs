namespace IDE
{
	public class functionmanager
	{
		string m_rootfolder;
		public string rootfolder
		{
			get
			{
				return m_rootfolder;
			}
		}
		functionl m_fl;
		public functionl fl
		{
			get
			{
				return m_fl;
			}
		}
		public functionmanager(string provided_rootfolder)
		{
			m_rootfolder = provided_rootfolder;
			initialize();
		}
		public functionmanager(functionmanager provided_obj)
		{
			m_rootfolder = provided_obj.rootfolder;
			m_fl = provided_obj.fl;
		}
		private void initialize()
		{
			m_fl = new functionl();
			string[] myfl = Directory.GetFiles(m_rootfolder, "*", System.IO.SearchOption.AllDirectories);
			foreach(string fe in myfl)
			{
				string ft = Path.GetFileNameWithoutExtension(fe);
				functione myf = new functione(m_fl.maxseq + 1, ft, fe);
				m_fl = m_fl - myf;
				m_fl = m_fl + myf;
			}
		}
	}
}
