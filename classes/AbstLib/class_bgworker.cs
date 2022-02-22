namespace AbstLib
{
	public delegate void MyDoWorkEventHandler(System.ComponentModel.DoWorkEventArgs e);
	public delegate void MyProgressChangedEventHandler(object provided_obj);
	public delegate void MyCompletedEventHandler(object provided_obj);
	public class BackgroundWorkerBaseClass
	{
		public event MyDoWorkEventHandler DoWork;
		public event MyProgressChangedEventHandler ProgressChanged;
		public event MyCompletedEventHandler Completed;
		protected bool m_busy;
		public bool busy
		{
			get
			{
				return m_busy;
			}
		}
		protected object m_result;
		public object result
		{
			get
			{
				return m_result;
			}
		}
		protected System.ComponentModel.BackgroundWorker m_bgworker;
		public System.ComponentModel.BackgroundWorker bgworker
		{
			get
			{
				return m_bgworker;
			}
		}
		public BackgroundWorkerBaseClass()
		{
			m_bgworker = new System.ComponentModel.BackgroundWorker();
			m_bgworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgrunworkercompleted);
			m_bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(bgdowork);
			m_bgworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgprogresschanged);
			m_bgworker.WorkerReportsProgress = true;
		}
		public void DoIt(object provided_data)
		{
			if(m_busy)
			{
			}
			else
			{
				m_busy = true;
				m_bgworker.RunWorkerAsync(provided_data);
			}
		}
		protected virtual void MyDoIt(object provided_obj)
		{
			// nop
		}
		private void bgdowork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			System.ComponentModel.BackgroundWorker worker = (System.ComponentModel.BackgroundWorker)sender;
			if(null != DoWork)
			{
				DoWork(e);
			}
		}
		private void bgprogresschanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if(null != ProgressChanged)
			{
				ProgressChanged(e.UserState);
			}
		}
		private void bgrunworkercompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if(null != Completed)
			{
				Completed(e.Result);
			}
			m_busy = false;
		}
	}
}
