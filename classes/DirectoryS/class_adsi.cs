namespace DirectoryS
{
	public class ComputerClass
	{
		string m_computername;
		public string computername
		{
			get
			{
				return m_computername;
			}
		}
		GroupLClass m_localgroups;
		public GroupLClass localgroups
		{
			get
			{
				return m_localgroups;
			}
		}
		UserLClass m_localusers;
		public UserLClass localusers
		{
			get
			{
				return m_localusers;
			}
		}
		public ComputerClass(string provided_computername)
		{
			m_computername = provided_computername;
			Initialize();
		}
		private void Initialize()
		{
			System.DirectoryServices.DirectoryEntry myc = new System.DirectoryServices.DirectoryEntry("WinNT://" + m_computername);
			m_localgroups = new GroupLClass();
			m_localusers = new UserLClass();
			foreach(System.DirectoryServices.DirectoryEntry de in myc.Children)
			{
				switch(de.SchemaClassName)
				{
					case "Group":
						{
							m_localgroups = (GroupLClass)(m_localgroups + new GroupEClass(m_localgroups.maxseq + 1, de));
							break;
						}
					case "User":
						{
							m_localusers = (UserLClass)(m_localusers + new UserEClass(m_localusers.maxseq + 1, de));
							break;
						}
					default:
						{
							break;
						}
				}
			}
			System.DirectoryServices.DirectoryEntry mye = new System.DirectoryServices.DirectoryEntry("WinNT://" + m_computername + "/Administrators");
			foreach(string prope in mye.Properties.PropertyNames)
			{
				System.Console.WriteLine(prope + mye.Properties[prope].Value);
				if(prope == "objectSid")
				{
					System.Security.Principal.SecurityIdentifier mysid = new System.Security.Principal.SecurityIdentifier((System.Byte[])mye.Properties[prope].Value, 0);
					System.Console.WriteLine(mysid.Value);
				}
			}
		}
	}
}
