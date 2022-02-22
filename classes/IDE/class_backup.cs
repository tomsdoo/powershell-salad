namespace IDE
{
	public class backupcompare : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			backupe myx = (backupe)x;
			backupe myy = (backupe)y;
			return DateTime.Compare(myx.lastwritetime, myy.lastwritetime);
		}
	}
	public class backupe
	{
		int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		saladenv m_env;
		public saladenv env
		{
			get
			{
				return m_env;
			}
		}
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		string m_originalpath;
		public string originalpath
		{
			get
			{
				return m_originalpath;
			}
		}
		DateTime m_lastwritetime;
		public DateTime lastwritetime
		{
			get
			{
				return m_lastwritetime;
			}
		}
		public backupe(int provided_seq, saladenv provided_env, string provided_originalpath)
		{
			m_seq = provided_seq;
			m_env = provided_env;
			m_originalpath = provided_originalpath;
			System.IO.FileInfo fi = new System.IO.FileInfo(m_originalpath);
			m_lastwritetime = fi.LastWriteTime;
			m_filename = m_originalpath.Replace(m_env.definedfolders.rootfolder, m_env.definedfolders.backupfolder);
			m_filename = m_filename + "." + m_lastwritetime.ToString("yyyyMMddHHmmss");
		}
		public backupe(int provided_seq, saladenv provided_env, string provided_filename, DateTime provided_lastwritetime)
		{
			m_seq = provided_seq;
			m_env = provided_env;
			m_filename = provided_filename;
			m_lastwritetime = provided_lastwritetime;
			m_originalpath = m_filename.Replace(m_env.definedfolders.backupfolder, m_env.definedfolders.rootfolder);
			m_originalpath = System.IO.Path.GetDirectoryName(m_originalpath) + System.IO.Path.DirectorySeparatorChar.ToString() + System.IO.Path.GetFileNameWithoutExtension(m_originalpath);
		}
		public backupe(backupe provided_obj)
		{
			m_seq = provided_obj.seq;
			m_env = provided_obj.env;
			m_filename = provided_obj.filename;
			m_lastwritetime = provided_obj.lastwritetime;
		}
	}
	public class backupl
	{
		public const string FINDFILENAME = "FINDFILENAME";
		public const string FINDLASTWRITETIME = "FINDLASTWRITETIME";
		backupe[] m_e;
		public backupe[] e
		{
			get
			{
				return m_e;
			}
		}
		public backupl()
		{
			m_e = null;
		}
		public int maxseq
		{
			get
			{
				int ret = 0;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						if(ret < m_e[icnt].seq)
						{
							ret = m_e[icnt].seq;
						}
					}
				}
				return ret;
			}
		}
		public backupl(backupl provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static backupl operator + (backupl provided_l, backupe provided_e)
		{
			backupl ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static backupl operator - (backupl provided_l, backupe provided_e)
		{
			backupl ret = new backupl();
			int idx = provided_l.FindIndex(provided_e);
			if(null != provided_l.e)
			{
				for(int icnt = 0; icnt < provided_l.e.Length; icnt++)
				{
					if(idx != icnt)
					{
						ret = ret + provided_l.e[icnt];
					}
				}
			}
			return ret;
		}
		public backupl Find(string provided_c, string provided_d)
		{
			backupl ret = new backupl();
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					bool badd = false;
					backupe tempe = m_e[icnt];
					switch(provided_c)
					{
						case FINDFILENAME:
							{
								badd = (tempe.filename.ToUpper() == provided_d.ToUpper());
								break;
							}
						case FINDLASTWRITETIME:
							{
								badd = (tempe.lastwritetime.ToString("yyyyMMddHHmmss") == provided_d.ToUpper());
								break;
							}
						default:
							{
								break;
							}
					}
					if(badd)
					{
						ret = ret + tempe;
					}
				}
			}
			return ret;
		}
		private int FindIndex(backupe provided_obj)
		{
			return FindIndex(provided_obj.seq);
		}
		private int FindIndex(int provided_seq)
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					if(provided_seq == m_e[icnt].seq)
					{
						return icnt;
					}
				}
			}
			return -1;
		}
		private void JustAdd(backupe provided_obj)
		{
			if(null == m_e)
			{
				m_e = new backupe[1];
				m_e[0] = provided_obj;
			}
			else
			{
				backupe[] templ = new backupe[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
			System.Array.Sort(m_e, new backupcompare());
		}
	}
	public class backupmanager
	{
		public event mdimessageeventhandler messageevent;
		saladenv m_env;
		public saladenv env
		{
			get
			{
				return m_env;
			}
		}
		backupl m_bl;
		public backupl bl
		{
			get
			{
				return m_bl;
			}
		}
		public backupmanager(saladenv provided_env)
		{
			m_env = provided_env;
			initialize();
		}
		public backupmanager(backupmanager provided_obj)
		{
			m_env = provided_obj.env;
			m_bl = provided_obj.bl;
		}
		public backupe newbackup(string provided_originalpath)
		{
			backupe ret = null;
			try
			{
				ret = new backupe(m_bl.maxseq + 1, m_env, provided_originalpath);
				backupproc(ret);
				m_bl = m_bl + ret;
			}
			catch
			{
				messageout("backup failed ");
				ret = null;
			}
			return ret;
		}
		private void backupproc(backupe provided_e)
		{
			if(!System.IO.File.Exists(provided_e.filename))
			{
				string myd = System.IO.Path.GetDirectoryName(provided_e.filename);
				if(!System.IO.Directory.Exists(myd))
				{
					System.IO.Directory.CreateDirectory(myd);
					messageout("directory created " + myd);
				}
				System.IO.File.Copy(provided_e.originalpath, provided_e.filename);
				messageout("file copied " + provided_e.originalpath + " -> " + provided_e.filename);
			}
		}
		private void initialize()
		{
			m_bl = new backupl();

			if(!Directory.Exists(m_env.definedfolders.backupfolder))
			{
				Directory.CreateDirectory(m_env.definedfolders.backupfolder);
				messageout("directory created " + m_env.definedfolders.backupfolder);
			}
			string[] fl = Directory.GetFiles(m_env.definedfolders.backupfolder, "*", System.IO.SearchOption.AllDirectories);
			foreach(string fe in fl)
			{
				System.IO.FileInfo fi = new System.IO.FileInfo(fe);
				backupe myb = new backupe(m_bl.maxseq + 1, m_env, fe, fi.LastWriteTime);
				m_bl = m_bl + myb;
			}
			messageout("initialized");
		}
		private void messageout(string provided_message)
		{
			if(null != messageevent)
			{
				messageevent("Backup", provided_message);
			}
		}
	}
}
