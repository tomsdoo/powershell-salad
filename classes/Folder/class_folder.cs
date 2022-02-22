namespace Folder
{
	public class NTFSClass
	{
		string m_path;
		public string path
		{
			get
			{
				return m_path;
			}
		}
		ACLClass m_acl;
		public ACLClass acl
		{
			get
			{
				return m_acl;
			}
		}
		public NTFSClass(string provided_path)
		{
			m_path = provided_path;
			Initialize();
		}
		private void Initialize()
		{
			m_acl = new ACLClass();
			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(m_path);
			System.Security.AccessControl.DirectorySecurity ds = di.GetAccessControl();
			System.Security.AccessControl.AuthorizationRuleCollection coll = ds.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
			foreach(System.Security.AccessControl.FileSystemAccessRule re in coll)
			{
				m_acl = (ACLClass)(m_acl + new ACEClass(m_acl.maxseq + 1, m_path, re.AccessControlType, re.FileSystemRights, (System.Security.Principal.SecurityIdentifier)(re.IdentityReference), re.IsInherited, re.InheritanceFlags, re.PropagationFlags));
			}
		}
	}
}
