namespace Util
{
	public class GetFileNameForm : InputForm
	{
		string m_name;
		public GetFileNameForm(string provided_prompt, string provided_name) : base(provided_prompt)
		{
			m_name = provided_name;
			MyInit();
		}
		private void MyInit()
		{
			m_filebox.Text = System.IO.Path.GetTempPath() + m_name;
		}
	}
}
