namespace ACC
{
	public class BGAccClass : AbstLib.BackgroundWorkerBaseClass
	{
		AccessClientSharpClass m_acc;
		public BGAccClass(string provided_filename)
		{
			m_acc = new AccessClientSharpClass(provided_filename);
			DoWork += new AbstLib.MyDoWorkEventHandler(MyDo);
			Completed += new AbstLib.MyCompletedEventHandler(MyCompleted);
		}
		private void MyDo(System.ComponentModel.DoWorkEventArgs e)
		{
			string sql = (string)(e.Argument);
			e.Result = m_acc.ExecuteQuery(sql);
		}
		private void MyCompleted(object provided_obj)
		{
			Util.TableGridFormClass gf = new Util.TableGridFormClass(string.Empty, (RowLClass)provided_obj);
			gf.ShowDialog();
		}
	}
}
