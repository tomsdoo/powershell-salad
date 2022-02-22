namespace Curious
{
	public class CuriousFileResultClass : CuriousResultEBaseClass
	{
		public const string FileAppeared = "FileAppeared";
		public const string FileDisappeared = "FileDisappeared";
		public const string FileWritten = "FileWritten";
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		public CuriousFileResultClass(int provided_seq, string provided_status, string provided_filename) : base(provided_seq, provided_status)
		{
			m_filename = provided_filename;
		}
		public CuriousFileResultClass(CuriousFileResultClass provided_obj) : base(provided_obj)
		{
			m_filename = provided_obj.filename;
		}
	}
	public class CuriousFileClass : CuriousBaseClass
	{
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
		DateTime m_lastwritetime;
		public CuriousFileClass(int provided_seq, string provided_filename) : base(provided_seq)
		{
			m_filename = provided_filename;
			m_exists = System.IO.File.Exists(m_filename);
			if(m_exists)
			{
				m_lastwritetime = (new System.IO.FileInfo(m_filename)).LastWriteTime;
			}
			else
			{
				m_lastwritetime = DateTime.MinValue;
			}
			//CuriousHappened += new CuriousHappenedEventHandler(MyHappened);
		}
		public override CuriousResultLClass Proceed()
		{
			CuriousResultLClass ret = new CuriousResultLClass();
			bool myexists = System.IO.File.Exists(m_filename);
			string mystatus = string.Empty;
			if(m_exists != myexists)
			{
				m_exists = myexists;
				if(m_exists)
				{
					mystatus = CuriousFileResultClass.FileAppeared;
				}
				else
				{
					mystatus = CuriousFileResultClass.FileDisappeared;
				}
				ret += new CuriousFileResultClass(ret.maxseq + 1, mystatus, m_filename);
			}
			
			if(m_exists)
			{
				System.IO.FileInfo fi = new System.IO.FileInfo(m_filename);
				if(fi.LastWriteTime != m_lastwritetime)
				{
					mystatus = CuriousFileResultClass.FileWritten;
					m_lastwritetime = fi.LastWriteTime;
					ret += new CuriousFileResultClass(ret.maxseq + 1, mystatus, m_filename);
				}
			}
			return ret;
		}
	}
	public class CuriousFileDriverClass
	{
		CuriousFileClass m_c;
		public CuriousFileDriverClass(string provided_filename, int provided_second)
		{
			int dummyseq = -1;
			m_c = new CuriousFileClass(dummyseq, provided_filename);
			m_c.CuriousHappened += new CuriousHappenedEventHandler(MyHappened);
			m_c.StartTimer(provided_second);
		}
		public void End()
		{
			m_c.EndTimer();
		}
		protected void MyHappened(CuriousResultLClass provided_obj)
		{
			if(null != provided_obj.e)
			{
				for(int icnt = 0; icnt < provided_obj.e.Length; icnt++)
				{
					if(provided_obj.e[icnt].GetType() == typeof(CuriousFileResultClass))
					{
						CuriousFileResultClass myresult = (CuriousFileResultClass)(provided_obj.e[icnt]);
						string message = string.Empty;
						message += myresult.dt.ToString("yyyy/MM/dd HH:mm:ss");
						message += " ";
						message += myresult.status;
						message += " ";
						message += myresult.filename;
						Console.WriteLine(message);
					}
				}
			}
		}
	}
}
