namespace MontyHall
{
	public class QuizShow
	{
		QuizL m_ql;
		public QuizL ql
		{
			get
			{
				return m_ql;
			}
		}
		public QuizShow(int provided_count)
		{
			m_ql = new QuizL();
			for(int icnt = 0; icnt < provided_count; icnt++)
			{
				m_ql = (QuizL)(m_ql + new Quiz(m_ql.maxseq + 1));
			}
		}
		public void Choose()
		{
			for(int icnt = 0; icnt < m_ql.e.Length; icnt++)
			{
				((Quiz)(m_ql.e[icnt])).ChooseOne();
			}
		}
		public void Open()
		{
			for(int icnt = 0; icnt < m_ql.e.Length; icnt++)
			{
				((Quiz)(m_ql.e[icnt])).OpenOne();
			}
		}
		public void Change()
		{
			for(int icnt = 0; icnt < m_ql.e.Length; icnt++)
			{
				((Quiz)(m_ql.e[icnt])).ChangeDoor();
			}
		}
	}
}
