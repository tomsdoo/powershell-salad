namespace Util
{
	public class BGFileReaderClass : AbstLib.BackgroundWorkerBaseClass
	{
		System.IO.StreamReader m_reader;
		string[] m_resultlines;
		public string[] resultlines
		{
			get
			{
				return m_resultlines;
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
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		public BGFileReaderClass(string provided_filename)
		{
			m_filename = provided_filename;
			m_resultlines = null;
			m_resultstatus = string.Empty;
			DoWork += new AbstLib.MyDoWorkEventHandler(MyDo);
			Completed += new AbstLib.MyCompletedEventHandler(MyCompleted);
		}
		private void MyDo(System.ComponentModel.DoWorkEventArgs e)
		{
			string[] retlines = null;
			try
			{
				m_reader = new System.IO.StreamReader(m_filename);
				string myline = string.Empty;
				while((myline = m_reader.ReadLine()) != null)
				{
					if(null == retlines)
					{
						retlines = new string[1];
						retlines[0] = myline;
					}
					else
					{
						string[] templ = new string[retlines.Length + 1];
						for(int icnt = 0; icnt < retlines.Length; icnt++)
						{
							templ[icnt] = retlines[icnt];
						}
						templ[templ.Length - 1] = myline;
						retlines = templ;
					}
				}
				m_reader.Close();
				m_resultlines = retlines;
			}
			catch
			{
			}
			e.Result = retlines;
		}
		private void MyCompleted(object provided_obj)
		{
			//m_resultlines = (string[])provided_obj;
			m_resultstatus = "Completed";
		}
	}
}
