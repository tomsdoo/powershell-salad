namespace Util
{
	public class BGFileInfoRetriever : AbstLib.BackgroundWorkerBaseClass
	{
		public const string CompletedStr = "Completed";
		System.IO.FileInfo m_resultinfo;
		public System.IO.FileInfo resultinfo
		{
			get
			{
				return m_resultinfo;
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
		string m_resultstatus;
		public string resultstatus
		{
			get
			{
				return m_resultstatus;
			}
		}
		public BGFileInfoRetriever(string provided_filename)
		{
			m_filename = provided_filename;
			m_resultinfo = null;
			m_resultstatus = string.Empty;
			DoWork += new AbstLib.MyDoWorkEventHandler(MyDo);
			Completed += new AbstLib.MyCompletedEventHandler(MyCompleted);
		}
		private void MyDo(System.ComponentModel.DoWorkEventArgs e)
		{
			m_resultinfo = new System.IO.FileInfo(m_filename);
			e.Result = m_resultinfo;
		}
		private void MyCompleted(object provided_obj)
		{
			m_resultstatus = CompletedStr;
		}
	}
}
