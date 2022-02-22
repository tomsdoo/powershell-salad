namespace IDE
{
	public class classmanager
	{
		string m_rootfolder;
		public string rootfolder
		{
			get
			{
				return m_rootfolder;
			}
		}
		classl m_cl;
		public classl cl
		{
			get
			{
				return m_cl;
			}
		}
		public classmanager(string provided_rootfolder)
		{
			m_rootfolder = provided_rootfolder;
			initialize();
		}
		public classmanager(classmanager provided_obj)
		{
			m_rootfolder = provided_obj.rootfolder;
			m_cl = provided_obj.cl;
		}
		private void initialize()
		{
			m_cl = new classl();
			string[] fl = Directory.GetFiles(m_rootfolder, "*", System.IO.SearchOption.AllDirectories);
			foreach(string fe in fl)
			{
				string ft = Path.GetFileNameWithoutExtension(fe);
				string[] nm = ft.Split(System.Convert.ToChar("."));
				if(2 != nm.Length)
				{
					System.Console.WriteLine("Error:" + fe);
				}
				else
				{
					string classname = nm[0];
					string methodname = nm[1];
					classe myclass = m_cl.Fetch(classname);
					if(null == myclass)
					{
						myclass = new classe(m_cl.maxseq + 1, classname);
					}
					myclass = myclass + (new methode(myclass.methods.maxseq + 1, methodname, fe));
					m_cl = m_cl - myclass;
					m_cl = m_cl + myclass;
				}
			}
		}
	}
}
