namespace TCP
{
	public class BGPingClass : AbstLib.BackgroundWorkerBaseClass
	{
		System.Net.NetworkInformation.Ping m_ping;
		public System.Net.NetworkInformation.Ping ping
		{
			get
			{
				return m_ping;
			}
		}
		System.Net.NetworkInformation.PingReply m_reply;
		public System.Net.NetworkInformation.PingReply reply
		{
			get
			{
				return m_reply;
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
		public BGPingClass(string provided_iporname)
		{
			m_resultstatus = string.Empty;
			m_name = provided_iporname;
			DoWork += new AbstLib.MyDoWorkEventHandler(MyDo);
			Completed += new AbstLib.MyCompletedEventHandler(MyCompleted);
		}
		private void MyDo(System.ComponentModel.DoWorkEventArgs e)
		{
			m_ping = new System.Net.NetworkInformation.Ping();
			try
			{
				m_reply = m_ping.Send(m_name);
			}
			catch
			{
			}
			e.Result = m_reply;
		}
		private void MyCompleted(object provided_obj)
		{
			m_resultstatus = "Completed";
		}
	}
}
