namespace Registry
{
	public class BGRegClass : AbstLib.BackgroundWorkerBaseClass
	{
		bool m_bcanread;
		public bool bcanread
		{
			get
			{
				return m_bcanread;
			}
		}
		string m_name;
		public string name
		{
			get
			{
				return m_name;
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
		public BGRegClass(string provided_name)
		{
			m_name = provided_name;
			DoWork += new AbstLib.MyDoWorkEventHandler(MyDo);
			Completed += new AbstLib.MyCompletedEventHandler(MyCompleted);
		}
		private void MyDo(System.ComponentModel.DoWorkEventArgs e)
		{
			m_bcanread = false;
			Microsoft.Win32.RegistryKey myb = null;
			Microsoft.Win32.RegistryKey mys = null;
			try
			{
				myb = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, m_name);
				mys = myb.OpenSubKey("Software");
				string[] kl = mys.GetSubKeyNames();
				m_bcanread = true;
			}
			catch
			{
			}
			finally
			{
				try
				{
					mys.Close();
				}
				catch
				{
				}
				try
				{
					myb.Close();
				}
				catch
				{
				}
			}
			e.Result = m_bcanread;
		}
		private void MyCompleted(object provided_obj)
		{
			m_resultstatus = "Completed";
		}
	}
}
