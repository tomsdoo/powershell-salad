namespace TCP
{
	public class ConnectionEClass : AbstLib.EBaseClass
	{
		ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		System.Net.Sockets.Socket m_soc;
		public System.Net.Sockets.Socket soc
		{
			get
			{
				return m_soc;
			}
		}
		public ConnectionEClass(ProfileEClass provided_profile, System.Net.Sockets.Socket provided_socket) : base(provided_profile.seq)
		{
			m_profile = provided_profile;
			m_soc = provided_socket;
		}
		public ConnectionEClass(CommClientEClass provided_obj) : base(provided_obj.seq)
		{
			m_profile = provided_obj.profile;
			m_soc = provided_obj.soc;
		}
	}
	public class ConnectionLClass : AbstLib.LBaseClass
	{
		public ConnectionLClass() : base(){}
		public ConnectionLClass(ConnectionLClass provided_obj) : base(provided_obj){}
	}
}
