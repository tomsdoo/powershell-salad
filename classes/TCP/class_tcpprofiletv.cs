namespace TCP
{
	public delegate void ProfileTVSelectedEventHandler(ProfileEClass provided_profile);
	public class ProfileTN : TreeNode
	{
		ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		public ProfileTN(ProfileEClass provided_profile)
		{
			m_profile = provided_profile;
			Text = m_profile.name;
			Name = m_profile.seq.ToString();
		}
	}
	public class ProfileTV : TreeView
	{
		public event ProfileTVSelectedEventHandler ProfileSelected;
		ProfileLClass m_profilel;
		public ProfileLClass profilel
		{
			get
			{
				return m_profilel;
			}
		}
		public ProfileTV(ProfileLClass provided_profilel)
		{
			m_profilel = provided_profilel;
			AfterSelect += new System.Windows.Forms.TreeViewEventHandler(SelectedEvent);
			Width = 100;
			Dock = System.Windows.Forms.DockStyle.Left;
			Initialize();
		}
		public void FeedProfiles(ProfileLClass provided_profilel)
		{
			m_profilel = provided_profilel;
			Initialize();
		}
		private delegate void InitializeDele();
		private void Initialize()
		{
			if(InvokeRequired)
			{
				Invoke(new InitializeDele(Initialize));
				return;
			}
			SuspendLayout();
			BeginUpdate();
			Nodes.Clear();
			if(null != m_profilel.e)
			{
				for(int icnt = 0; icnt < m_profilel.e.Length; icnt++)
				{
					ProfileEClass myp = (ProfileEClass)(m_profilel.e[icnt]);
					ProfileTN mytn = new ProfileTN(myp);
					Nodes.Add(mytn);
				}
			}
			EndUpdate();
			ResumeLayout();
		}
		private void SelectedEvent(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(null != ProfileSelected)
			{
				int myseq = int.Parse(SelectedNode.Name);
				ProfileEClass myprof = (ProfileEClass)(m_profilel[new AbstLib.EBaseClass(myseq)]);
				ProfileSelected(myprof);
			}
		}
	}
}
