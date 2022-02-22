namespace WMI
{
	public class BGOSClass : AbstLib.BackgroundWorkerBaseClass
	{
		string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		string m_computername;
		public string computername
		{
			get
			{
				return m_computername;
			}
		}
		bool m_bexists;
		public bool bexists
		{
			get
			{
				return m_bexists;
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
		public BGOSClass(string provided_name)
		{
			m_name = provided_name;
			m_bexists = false;
			m_computername = string.Empty;
			m_resultstatus = string.Empty;
			DoWork += new AbstLib.MyDoWorkEventHandler(MyDo);
			Completed += new AbstLib.MyCompletedEventHandler(MyCompleted);
		}
		private void MyDo(System.ComponentModel.DoWorkEventArgs e)
		{
			RemoteComputerClass rc = new RemoteComputerClass(m_name);
			try
			{
				ACC.RowLClass rl = rc.ExecuteQuery("select CSName from Win32_OperatingSystem");
				m_computername = ((ACC.ColEStringClass)(rl.e[0].coll.e[0])).value;
				m_bexists = true;
			}
			catch
			{
			}
			e.Result = null;
		}
		private void MyCompleted(object provided_obj)
		{
			m_resultstatus = "Completed";
		}
	}
}
