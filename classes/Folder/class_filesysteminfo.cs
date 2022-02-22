namespace Folder
{
	public class FileSystemInfoEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameAttributes = "attributes";
		public const string AttributeNameCreationTime = "creationtime";
		public const string AttributeNameCreationTimeUtc = "creationtimeutc";
		public const string AttributeNameExists = "exists";
		public const string AttributeNameExtension = "extension";
		public const string AttributeNameFullName = "fullname";
		public const string AttributeNameLastAccessTime = "lastaccesstime";
		public const string AttributeNameLastAccessTimeUtc = "lastaccesstimeutc";
		public const string AttributeNameLastWriteTime = "lastwritetime";
		public const string AttributeNameLastWriteTimeUtc = "lastwritetimeutc";
		public const string AttributeNameName = "name";
		protected System.IO.FileAttributes m_attributes;
		public System.IO.FileAttributes attributes
		{
			get
			{
				return m_attributes;
			}
		}
		protected System.DateTime m_creationtime;
		public System.DateTime creationtime
		{
			get
			{
				return m_creationtime;
			}
		}
		protected System.DateTime m_creationtimeutc;
		public System.DateTime creationtimeutc
		{
			get
			{
				return m_creationtimeutc;
			}
		}
		protected bool m_exists;
		public bool exists
		{
			get
			{
				return m_exists;
			}
		}
		protected string m_extension;
		public string extension
		{
			get
			{
				return m_extension;
			}
		}
		protected string m_fullname;
		public string fullname
		{
			get
			{
				return m_fullname;
			}
		}
		protected System.DateTime m_lastaccesstime;
		public System.DateTime lastaccesstime
		{
			get
			{
				return m_lastaccesstime;
			}
		}
		protected System.DateTime m_lastaccesstimeutc;
		public System.DateTime lastaccesstimeutc
		{
			get
			{
				return m_lastaccesstimeutc;
			}
		}
		protected System.DateTime m_lastwritetime;
		public System.DateTime lastwritetime
		{
			get
			{
				return m_lastwritetime;
			}
		}
		protected System.DateTime m_lastwritetimeutc;
		public System.DateTime lastwritetimeutc
		{
			get
			{
				return m_lastwritetimeutc;
			}
		}
		protected string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		public virtual ACC.RowEClass rowe
		{
			get
			{
				ACC.ColLClass coll = new ACC.ColLClass();
				coll = (ACC.ColLClass)(coll + new ACC.ColEIntClass(coll.maxseq + 1, AttributeNameAttributes, (int)m_attributes));
				coll = (ACC.ColLClass)(coll + new ACC.ColEDateClass(coll.maxseq + 1, AttributeNameCreationTime, m_creationtime));
				coll = (ACC.ColLClass)(coll + new ACC.ColEDateClass(coll.maxseq + 1, AttributeNameCreationTimeUtc, m_creationtimeutc));
				coll = (ACC.ColLClass)(coll + new ACC.ColEBoolClass(coll.maxseq + 1, AttributeNameExists, m_exists));
				coll = (ACC.ColLClass)(coll + new ACC.ColEStringClass(coll.maxseq + 1, AttributeNameExtension, m_extension));
				coll = (ACC.ColLClass)(coll + new ACC.ColEStringClass(coll.maxseq + 1, AttributeNameFullName, m_fullname));
				coll = (ACC.ColLClass)(coll + new ACC.ColEDateClass(coll.maxseq + 1, AttributeNameLastAccessTime, m_lastaccesstime));
				coll = (ACC.ColLClass)(coll + new ACC.ColEDateClass(coll.maxseq + 1, AttributeNameLastAccessTimeUtc, m_lastaccesstimeutc));
				coll = (ACC.ColLClass)(coll + new ACC.ColEDateClass(coll.maxseq + 1, AttributeNameLastWriteTime, m_lastwritetime));
				coll = (ACC.ColLClass)(coll + new ACC.ColEDateClass(coll.maxseq + 1, AttributeNameLastWriteTimeUtc, m_lastwritetimeutc));
				coll = (ACC.ColLClass)(coll + new ACC.ColEStringClass(coll.maxseq + 1, AttributeNameName, m_name));
				return new ACC.RowEClass(m_seq, coll);
			}
		}
		public FileSystemInfoEClass(	int provided_seq,
										System.IO.FileAttributes provided_attributes,
										System.DateTime provided_creationtime,
										System.DateTime provided_creationtimeutc,
										bool provided_exists,
										string provided_extension,
										string provided_fullname,
										System.DateTime provided_lastaccesstime,
										System.DateTime provided_lastaccesstimeutc,
										System.DateTime provided_lastwritetime,
										System.DateTime provided_lastwritetimeutc,
										string provided_name)
		: base(provided_seq)
		{
			m_attributes = provided_attributes;
			m_creationtime = provided_creationtime;
			m_creationtimeutc = provided_creationtimeutc;
			m_exists = provided_exists;
			m_extension = provided_extension;
			m_fullname = provided_fullname;
			m_lastaccesstime = provided_lastaccesstime;
			m_lastaccesstimeutc = provided_lastaccesstimeutc;
			m_lastwritetime = provided_lastwritetime;
			m_lastwritetimeutc = provided_lastwritetimeutc;
			m_name = provided_name;
		}
		public FileSystemInfoEClass(int provided_seq, System.IO.FileSystemInfo provided_i) : base(provided_seq)
		{
			m_attributes = provided_i.Attributes;
			m_creationtime = provided_i.CreationTime;
			m_creationtimeutc = provided_i.CreationTimeUtc;
			m_exists = provided_i.Exists;
			m_extension = provided_i.Extension;
			m_fullname = provided_i.FullName;
			m_lastaccesstime = provided_i.LastAccessTime;
			m_lastaccesstimeutc = provided_i.LastAccessTimeUtc;
			m_lastwritetime = provided_i.LastWriteTime;
			m_lastwritetimeutc = provided_i.LastWriteTimeUtc;
			m_name = provided_i.Name;
		}
		public FileSystemInfoEClass(FileSystemInfoEClass provided_obj) : base(provided_obj)
		{
			m_attributes = provided_obj.attributes;
			m_creationtime = provided_obj.creationtime;
			m_creationtimeutc = provided_obj.creationtimeutc;
			m_exists = provided_obj.exists;
			m_extension = provided_obj.extension;
			m_fullname = provided_obj.fullname;
			m_lastaccesstime = provided_obj.lastaccesstime;
			m_lastaccesstimeutc = provided_obj.lastaccesstimeutc;
			m_lastwritetime = provided_obj.lastwritetime;
			m_lastwritetimeutc = provided_obj.lastwritetimeutc;
			m_name = provided_obj.name;
		}
		public FileSystemInfoEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_attributes = (System.IO.FileAttributes)(int.Parse(provided_element.GetAttribute(AttributeNameAttributes)));
			m_creationtime = System.DateTime.ParseExact(provided_element.GetAttribute(AttributeNameCreationTime), "yyyy/MM/dd HH:mm:ss", null);
			m_creationtimeutc = System.DateTime.ParseExact(provided_element.GetAttribute(AttributeNameCreationTimeUtc), "yyyy/MM/dd HH:mm:ss", null);
			m_exists = bool.Parse(provided_element.GetAttribute(AttributeNameExists));
			m_extension = provided_element.GetAttribute(AttributeNameExtension);
			m_fullname = provided_element.GetAttribute(AttributeNameFullName);
			m_lastaccesstime = System.DateTime.ParseExact(provided_element.GetAttribute(AttributeNameLastAccessTime), "yyyy/MM/dd HH:m:ss", null);
			m_lastaccesstimeutc = System.DateTime.ParseExact(provided_element.GetAttribute(AttributeNameLastAccessTimeUtc), "yyyy/MM/dd HH:m:ss", null);
			m_lastwritetime = System.DateTime.ParseExact(provided_element.GetAttribute(AttributeNameLastWriteTime), "yyyy/MM/dd HH:m:ss", null);
			m_lastwritetimeutc = System.DateTime.ParseExact(provided_element.GetAttribute(AttributeNameLastWriteTimeUtc), "yyyy/MM/dd HH:m:ss", null);
			m_name = provided_element.GetAttribute(AttributeNameName);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameAttributes, ((int)m_attributes).ToString());
			ret.SetAttribute(AttributeNameCreationTime, m_creationtime.ToString("yyyy/MM/dd HH:mm:ss"));
			ret.SetAttribute(AttributeNameCreationTimeUtc, m_creationtimeutc.ToString("yyyy/MM/dd HH:mm:ss"));
			ret.SetAttribute(AttributeNameExists, m_exists.ToString());
			ret.SetAttribute(AttributeNameExtension, m_extension.ToString());
			ret.SetAttribute(AttributeNameFullName, m_fullname);
			ret.SetAttribute(AttributeNameLastAccessTime, m_lastaccesstime.ToString("yyyy/MM/dd HH:mm:ss"));
			ret.SetAttribute(AttributeNameLastAccessTimeUtc, m_lastaccesstimeutc.ToString("yyyy/MM/dd HH:mm:ss"));
			ret.SetAttribute(AttributeNameLastWriteTime, m_lastwritetime.ToString("yyyy/MM/dd HH:mm:ss"));
			ret.SetAttribute(AttributeNameLastWriteTimeUtc, m_lastwritetimeutc.ToString("yyyy/MM/dd HH:mm:ss"));
			ret.SetAttribute(AttributeNameName, m_name);
			return ret;
		}
	}
	public class FileSystemInfoLClass : AbstLib.LBaseClass
	{
		public virtual ACC.RowLClass rowl
		{
			get
			{
				ACC.RowLClass ret = new ACC.RowLClass();
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret = (ACC.RowLClass)(ret + ((FileSystemInfoEClass)(m_e[icnt])).rowe);
					}
				}
				return ret;
			}
		}
		public FileSystemInfoLClass() : base(){}
		public FileSystemInfoLClass(FileSystemInfoLClass provided_obj) : base(provided_obj){}
		public FileSystemInfoLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new FileSystemInfoEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
