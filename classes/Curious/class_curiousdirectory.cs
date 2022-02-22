namespace Curious
{
	public class CuriousDirectoryResultClass : CuriousResultEBaseClass
	{
		public const string DirectoryAppeared = "DirectoryAppeared";
		public const string DirectoryDisappeared = "DirectoryDisappeared";
		public const string DirectoryUpdated = "DirectoryUpdated";
		public const string SubdirectoryAdded = "SubdirectoryAdded";
		public const string SubdirectoryDeleted = "SubdirectoryDeleted";
		public const string FileAdded = "FileAdded";
		public const string FileDeleted = "FileDeleted";
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		string m_changeditemname;
		public string changeditemname
		{
			get
			{
				return m_changeditemname;
			}
		}
		public CuriousDirectoryResultClass(int provided_seq, string provided_status, string provided_filename, string provided_changeditemname) : base(provided_seq, provided_status)
		{
			m_filename = provided_filename;
			m_changeditemname = provided_changeditemname;
		}
		public CuriousDirectoryResultClass(CuriousDirectoryResultClass provided_obj) : base(provided_obj)
		{
			m_filename = provided_obj.filename;
			m_changeditemname = provided_obj.changeditemname;
		}
	}
	public class CuriousDirectoryClass :CuriousBaseClass
	{
		public const string SearchRecursively = "SearchRecursively";
		public const string SearchTopOnly = "SearchTopOnly";
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		bool m_exists;
		public bool exists
		{
			get
			{
				return m_exists;
			}
		}
		string m_searchoption;
		public string searchoption
		{
			get
			{
				return m_searchoption;
			}
		}
		DateTime m_lastwritetime;
		string[] m_filel;
		string[] m_directoryl;
		public CuriousDirectoryClass(int provided_seq, string provided_filename, string provided_searchoption) : base(provided_seq)
		{
			m_filename = provided_filename;
			m_searchoption = provided_searchoption;
			Initialize();
		}
		public override CuriousResultLClass Proceed()
		{
			CuriousResultLClass ret = new CuriousResultLClass();
			bool myexists = System.IO.Directory.Exists(m_filename);
			string mystatus = string.Empty;
			if(m_exists != myexists)
			{
				m_exists = myexists;
				if(m_exists)
				{
					mystatus = CuriousDirectoryResultClass.DirectoryAppeared;
				}
				else
				{
					mystatus = CuriousDirectoryResultClass.DirectoryDisappeared;
				}
				ret += new CuriousDirectoryResultClass(ret.maxseq + 1, mystatus, m_filename, string.Empty);
			}
			string[] myfilel = null;
			string[] mydirectoryl = null;
			if(m_exists)
			{
				myfilel = MyGetFiles();
				mydirectoryl = MyGetDirectories();
			}
			if(null != m_filel)
			{
				for(int icnt = 0; icnt < m_filel.Length; icnt++)
				{
					if(!StringExists(myfilel, m_filel[icnt]))
					{
						mystatus = CuriousDirectoryResultClass.FileDeleted;
						ret += new CuriousDirectoryResultClass(ret.maxseq + 1, mystatus, m_filename, m_filel[icnt]);
					}
				}
			}
			if(null != myfilel)
			{
				for(int jcnt = 0; jcnt < myfilel.Length; jcnt++)
				{
					if(!StringExists(m_filel, myfilel[jcnt]))
					{
						mystatus = CuriousDirectoryResultClass.FileAdded;
						ret += new CuriousDirectoryResultClass(ret.maxseq + 1, mystatus, m_filename, myfilel[jcnt]);
					}
				}
			}
			if(null != m_directoryl)
			{
				for(int kcnt = 0; kcnt < m_directoryl.Length; kcnt++)
				{
					if(!StringExists(mydirectoryl, m_directoryl[kcnt]))
					{
						mystatus = CuriousDirectoryResultClass.SubdirectoryDeleted;
						ret += new CuriousDirectoryResultClass(ret.maxseq + 1, mystatus, m_filename, m_directoryl[kcnt]);
					}
				}
			}
			if(null != mydirectoryl)
			{
				for(int lcnt = 0; lcnt < mydirectoryl.Length; lcnt++)
				{
					if(!StringExists(m_directoryl, mydirectoryl[lcnt]))
					{
						mystatus = CuriousDirectoryResultClass.SubdirectoryAdded;
						ret += new CuriousDirectoryResultClass(ret.maxseq + 1, mystatus, m_filename, mydirectoryl[lcnt]);
					}
				}
			}
			m_filel = myfilel;
			m_directoryl = mydirectoryl;
			return ret;
		}
		private bool StringExists(string[] provided_l, string provided_e)
		{
			if(null != provided_l)
			{
				for(int icnt = 0; icnt < provided_l.Length; icnt++)
				{
					if(provided_e == provided_l[icnt])
					{
						return true;
					}
				}
			}
			return false;
		}
		private void Initialize()
		{
			m_exists = System.IO.Directory.Exists(m_filename);
			m_filel = null;
			m_directoryl = null;
			m_lastwritetime = DateTime.MinValue;
			if(m_exists)
			{
				m_lastwritetime = (new System.IO.DirectoryInfo(m_filename)).LastWriteTime;
				m_filel = MyGetFiles();
				m_directoryl = MyGetDirectories();
			}
		}
		private string[] MyGetDirectories()
		{
			string[] ret = null;
			switch(m_searchoption)
			{
				case SearchRecursively:
					{
						ret = System.IO.Directory.GetDirectories(m_filename, "*", System.IO.SearchOption.AllDirectories);
						break;
					}
				case SearchTopOnly:
					{
						ret = System.IO.Directory.GetDirectories(m_filename, "*", System.IO.SearchOption.TopDirectoryOnly);
						break;
					}
				default:
					{
						break;
					}
			}
			return ret;
		}
		private string[] MyGetFiles()
		{
			string[] ret = null;
			switch(m_searchoption)
			{
				case SearchRecursively:
					{
						ret = System.IO.Directory.GetFiles(m_filename, "*", System.IO.SearchOption.AllDirectories);
						break;
					}
				case SearchTopOnly:
					{
						ret = System.IO.Directory.GetFiles(m_filename, "*", System.IO.SearchOption.TopDirectoryOnly);
						break;
					}
				default:
					{
						break;
					}
			}
			return ret;
		}
	}
	public class CuriousDirectoryDriverClass
	{
		CuriousDirectoryClass m_d;
		public CuriousDirectoryDriverClass(string provided_filename, int provided_second)
		{
			int dummyseq = -1;
			m_d = new CuriousDirectoryClass(dummyseq, provided_filename, CuriousDirectoryClass.SearchTopOnly);
			m_d.CuriousHappened += new CuriousHappenedEventHandler(MyHappened);
			m_d.StartTimer(provided_second);
		}
		public void End()
		{
			m_d.EndTimer();
		}
		protected void MyHappened(CuriousResultLClass provided_obj)
		{
			if(null != provided_obj.e)
			{
				for(int icnt = 0; icnt < provided_obj.e.Length; icnt++)
				{
					if(provided_obj.e[icnt].GetType() == typeof(CuriousDirectoryResultClass))
					{
						CuriousDirectoryResultClass myresult = (CuriousDirectoryResultClass)provided_obj.e[icnt];
						string message = string.Empty;
						message += myresult.dt.ToString("yyyy/MM/dd HH:mm:ss");
						message += " ";
						message += myresult.status;
						message += " ";
						message += myresult.filename;
						message += " ";
						message += myresult.changeditemname;
						Console.WriteLine(message);
					}
				}
			}
		}
	}
}
