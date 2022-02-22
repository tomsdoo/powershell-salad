namespace Folder
{
	public class FolderUtil
	{
		public string[] rights
		{
			get
			{
				return System.Enum.GetNames(typeof(System.Security.AccessControl.FileSystemRights));
			}
		}
		public string[] actype
		{
			get
			{
				return System.Enum.GetNames(typeof(System.Security.AccessControl.AccessControlType));
			}
		}
		public string[] inheritanceflags
		{
			get
			{
				return System.Enum.GetNames(typeof(System.Security.AccessControl.InheritanceFlags));
			}
		}
		public string[] propagationflags
		{
			get
			{
				return System.Enum.GetNames(typeof(System.Security.AccessControl.PropagationFlags));
			}
		}
		public FolderUtil()
		{
		}
	}
	public class ACEClass : AbstLib.EBaseClass
	{
		public const string AttributeNamePath = "path";
		public const string AttributeNameACType = "actype";
		public const string AttributeNameRights = "rights";
		public const string AttributeNameSid = "sid";
		public const string AttributeNameInheritanceFlags = "inheritanceflags";
		public const string AttributeNameInherited = "inherited";
		public const string AttributeNamePropagationFlags = "propagationflags";
		string m_path;
		public string path
		{
			get
			{
				return m_path;
			}
		}
		System.Security.AccessControl.AccessControlType m_actype;
		public System.Security.AccessControl.AccessControlType actype
		{
			get
			{
				return m_actype;
			}
		}
		System.Security.AccessControl.FileSystemRights m_rights;
		public System.Security.AccessControl.FileSystemRights rights
		{
			get
			{
				return m_rights;
			}
		}
		System.Security.Principal.SecurityIdentifier m_sid;
		public System.Security.Principal.SecurityIdentifier sid
		{
			get
			{
				return m_sid;
			}
		}
		System.Security.AccessControl.InheritanceFlags m_inheritanceflags;
		public System.Security.AccessControl.InheritanceFlags inheritanceflags
		{
			get
			{
				return m_inheritanceflags;
			}
		}
		bool m_inherited;
		public bool inherited
		{
			get
			{
				return m_inherited;
			}
		}
		System.Security.AccessControl.PropagationFlags m_propagationflags;
		public System.Security.AccessControl.PropagationFlags propagationflags
		{
			get
			{
				return m_propagationflags;
			}
		}
		public System.Security.AccessControl.FileSystemAccessRule rule
		{
			get
			{
				return new System.Security.AccessControl.FileSystemAccessRule(m_sid, m_rights, m_inheritanceflags, m_propagationflags, m_actype);
			}
		}
		public ACEClass(
			int provided_seq,
			string provided_path,
			System.Security.AccessControl.AccessControlType provided_actype,
			System.Security.AccessControl.FileSystemRights provided_rights,
			System.Security.Principal.SecurityIdentifier provided_sid,
			bool provided_inherited,
			System.Security.AccessControl.InheritanceFlags provided_inheritanceflags,
			System.Security.AccessControl.PropagationFlags provided_propagationflags
		) : base(provided_seq)
		{
			m_path = provided_path;
			m_actype = provided_actype;
			m_rights = provided_rights;
			m_sid = provided_sid;
			m_inherited = provided_inherited;
			m_inheritanceflags = provided_inheritanceflags;
			m_propagationflags = provided_propagationflags;
		}
		public ACEClass(
			int provided_seq,
			string provided_path,
			int provided_actype,
			int provided_rights,
			string provided_sid,
			bool provided_inherited,
			int provided_inheritanceflags,
			int provided_propagationflags
		) : base(provided_seq)
		{
			m_path = provided_path;
			m_actype = (System.Security.AccessControl.AccessControlType)provided_actype;
			m_rights = (System.Security.AccessControl.FileSystemRights)provided_rights;
			m_sid = new System.Security.Principal.SecurityIdentifier(provided_sid);
			m_inherited = provided_inherited;
			m_inheritanceflags = (System.Security.AccessControl.InheritanceFlags)provided_inheritanceflags;
			m_propagationflags = (System.Security.AccessControl.PropagationFlags)provided_propagationflags;
		}
		public ACEClass(
			int provided_seq,
			string provided_path,
			string provided_actype,
			string provided_rights,
			string provided_sid,
			bool provided_inherited,
			string provided_inheritanceflags,
			string provided_propagationflags
		) : base(provided_seq)
		{
			m_path = provided_path;
			m_actype = (System.Security.AccessControl.AccessControlType)(System.Enum.Parse(typeof(System.Security.AccessControl.AccessControlType), provided_actype));
			m_rights = (System.Security.AccessControl.FileSystemRights)(System.Enum.Parse(typeof(System.Security.AccessControl.FileSystemRights), provided_rights));
			m_sid = new System.Security.Principal.SecurityIdentifier(provided_sid);
			m_inherited = provided_inherited;
			m_inheritanceflags = (System.Security.AccessControl.InheritanceFlags)(System.Enum.Parse(typeof(System.Security.AccessControl.InheritanceFlags), provided_inheritanceflags));
			m_propagationflags = (System.Security.AccessControl.PropagationFlags)(System.Enum.Parse(typeof(System.Security.AccessControl.PropagationFlags), provided_propagationflags));
		}
		public ACEClass(ACEClass provided_obj) : base(provided_obj)
		{
			m_path = provided_obj.path;
			m_actype = provided_obj.actype;
			m_rights = provided_obj.rights;
			m_sid = provided_obj.sid;
			m_inherited = provided_obj.inherited;
			m_inheritanceflags = provided_obj.inheritanceflags;
			m_propagationflags = provided_obj.propagationflags;
		}
		public ACEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_path = provided_element.GetAttribute(AttributeNamePath);
			m_actype = (System.Security.AccessControl.AccessControlType)(System.Enum.Parse(typeof(System.Security.AccessControl.AccessControlType), provided_element.GetAttribute(AttributeNameACType)));
			m_rights = (System.Security.AccessControl.FileSystemRights)(System.Enum.Parse(typeof(System.Security.AccessControl.FileSystemRights), provided_element.GetAttribute(AttributeNameRights)));
			m_sid = new System.Security.Principal.SecurityIdentifier(provided_element.GetAttribute(AttributeNameSid));
			m_inherited = System.Convert.ToBoolean(provided_element.GetAttribute(AttributeNameInherited));
			m_inheritanceflags = (System.Security.AccessControl.InheritanceFlags)(System.Enum.Parse(typeof(System.Security.AccessControl.InheritanceFlags), provided_element.GetAttribute(AttributeNameInheritanceFlags)));
			m_propagationflags = (System.Security.AccessControl.PropagationFlags)(System.Enum.Parse(typeof(System.Security.AccessControl.PropagationFlags), provided_element.GetAttribute(AttributeNamePropagationFlags)));
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNamePath, m_path);
			ret.SetAttribute(AttributeNameACType, m_actype.ToString());
			ret.SetAttribute(AttributeNameRights, m_rights.ToString());
			ret.SetAttribute(AttributeNameSid, m_sid.Value);
			ret.SetAttribute(AttributeNameInherited, m_inherited.ToString());
			ret.SetAttribute(AttributeNameInheritanceFlags, m_inheritanceflags.ToString());
			ret.SetAttribute(AttributeNamePropagationFlags, m_propagationflags.ToString());
			return ret;
		}
	}
	public class ACLClass : AbstLib.LBaseClass
	{
		public ACLClass() : base(){}
		public ACLClass(ACLClass provided_obj) : base(provided_obj){}
		public ACLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new ACEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
