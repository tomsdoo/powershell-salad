namespace IDE
{
	public class psenv
	{
		string[] m_cmdletl;
		public string[] cmdletl
		{
			get
			{
				return m_cmdletl;
			}
		}
		public psenv(string provided_str)
		{
			Interpret(provided_str);
		}
		public psenv(string[] provided_cmdletl)
		{
			m_cmdletl = provided_cmdletl;
		}
		public psenv(psenv provided_obj)
		{
			m_cmdletl = provided_obj.cmdletl;
		}
		private void Interpret(string provided_str)
		{
			foreach(string tempe in provided_str.Split('/'))
			{
				if(tempe != string.Empty)
				{
					JustAdd(tempe);
				}
			}
		}
		private void JustAdd(string provided_str)
		{
			if(null == m_cmdletl)
			{
				m_cmdletl = new string[1];
				m_cmdletl[0] = provided_str;
			}
			else
			{
				string[] templ = new string[m_cmdletl.Length + 1];
				for(int icnt = 0; icnt < m_cmdletl.Length; icnt++)
				{
					templ[icnt] = m_cmdletl[icnt];
				}
				templ[templ.Length - 1] = provided_str;
				m_cmdletl = templ;
			}
		}
	}
}
