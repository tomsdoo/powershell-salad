namespace Folder
{
	public class DirectoryInfoEClass : FileSystemInfoEClass
	{
		public const string AttributeNameSubdirectories = "subdirectories";
		public const string AttributeNameFiles = "files";
		protected DirectoryInfoLClass m_subdirectories;
		public DirectoryInfoLClass subdirectories
		{
			get
			{
				return m_subdirectories;
			}
		}
		protected FileInfoLClass m_files;
		public FileInfoLClass files
		{
			get
			{
				return m_files;
			}
		}
		public long length
		{
			get
			{
				long ret = m_files.length;
				if(null != m_subdirectories.e)
				{
					for(int icnt = 0; icnt < m_subdirectories.e.Length; icnt++)
					{
						ret += ((DirectoryInfoEClass)(m_subdirectories.e[icnt])).length;
					}
				}
				return ret;
			}
		}
		public ACC.RowLClass rowl
		{
			get
			{
				ACC.RowLClass ret = new ACC.RowLClass();
				ret = (ACC.RowLClass)(ret + new ACC.RowEClass(ret.maxseq + 1, rowe.coll));
				if(null != m_files.e)
				{
					for(int icnt = 0; icnt < m_files.e.Length; icnt++)
					{
						ret = (ACC.RowLClass)(ret + new ACC.RowEClass(ret.maxseq + 1, ((FileInfoEClass)(m_files.e[icnt])).rowe.coll));
					}
				}
				if(null != m_subdirectories.e)
				{
					for(int jcnt = 0; jcnt < m_subdirectories.e.Length; jcnt++)
					{
						DirectoryInfoEClass myd = (DirectoryInfoEClass)(m_subdirectories.e[jcnt]);
						ACC.RowLClass myrowl = myd.rowl;
						if(null != myrowl.e)
						{
							for(int kcnt = 0; kcnt < myrowl.e.Length; kcnt++)
							{
								ACC.RowEClass tempre = (ACC.RowEClass)(myrowl.e[kcnt]);
								ret = (ACC.RowLClass)(ret + new ACC.RowEClass(ret.maxseq + 1, tempre.coll));
							}
						}
					}
				}
				return ret;
			}
		}
		public DirectoryInfoEClass(	int provided_seq,
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
									string provided_name,
									DirectoryInfoLClass provided_subdirectories,
									FileInfoLClass provided_files)
		: base(	provided_seq,
				provided_attributes,
				provided_creationtime,
				provided_creationtimeutc,
				provided_exists,
				provided_extension,
				provided_fullname,
				provided_lastaccesstime,
				provided_lastaccesstimeutc,
				provided_lastwritetime,
				provided_lastwritetimeutc,
				provided_name)
		{
			m_subdirectories = provided_subdirectories;
			m_files = provided_files;
		}
		public DirectoryInfoEClass(	int provided_seq,
									System.IO.DirectoryInfo provided_di,
									DirectoryInfoLClass provided_subdirectories,
									FileInfoLClass provided_files)
		: base(provided_seq, provided_di)
		{
			m_subdirectories = provided_subdirectories;
			m_files = provided_files;
		}
		public DirectoryInfoEClass(int provided_seq, System.IO.DirectoryInfo provided_di) : base(provided_seq, provided_di)
		{
			m_files = new FileInfoLClass();
			foreach(string fe in System.IO.Directory.GetFiles(provided_di.FullName, "*", System.IO.SearchOption.TopDirectoryOnly))
			{
				m_files = (FileInfoLClass)(m_files + new FileInfoEClass(m_files.maxseq + 1, new System.IO.FileInfo(fe)));
			}
			m_subdirectories = new DirectoryInfoLClass();
			foreach(string de in System.IO.Directory.GetDirectories(provided_di.FullName, "*", System.IO.SearchOption.TopDirectoryOnly))
			{
				m_subdirectories = (DirectoryInfoLClass)(m_subdirectories + new DirectoryInfoEClass(m_subdirectories.maxseq + 1, new System.IO.DirectoryInfo(de)));
			}
		}
		public DirectoryInfoEClass(DirectoryInfoEClass provided_obj) : base(provided_obj)
		{
			m_subdirectories = provided_obj.subdirectories;
			m_files = provided_obj.files;
		}
		public DirectoryInfoEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			string subdstr = provided_element.GetAttribute(AttributeNameSubdirectories);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(subdstr);
			m_subdirectories = new DirectoryInfoLClass((System.Xml.XmlElement)(x.DocumentElement));
			string fstr = provided_element.GetAttribute(AttributeNameFiles);
			System.Xml.XmlDocument fx = new System.Xml.XmlDocument();
			x.LoadXml(fstr);
			m_files = new FileInfoLClass((System.Xml.XmlElement)(x.DocumentElement));
		}
		public DirectoryInfoEClass(int provided_seq, string provided_fullname) : base(provided_seq, new System.IO.DirectoryInfo(provided_fullname))
		{
			m_files = new FileInfoLClass();
			foreach(string fe in System.IO.Directory.GetFiles(provided_fullname, "*", System.IO.SearchOption.TopDirectoryOnly))
			{
				m_files = (FileInfoLClass)(m_files + new FileInfoEClass(m_files.maxseq + 1, new System.IO.FileInfo(fe)));
			}
			m_subdirectories = new DirectoryInfoLClass();
			foreach(string de in System.IO.Directory.GetDirectories(provided_fullname, "*", System.IO.SearchOption.TopDirectoryOnly))
			{
				m_subdirectories = (DirectoryInfoLClass)(m_subdirectories + new DirectoryInfoEClass(m_subdirectories.maxseq + 1, new System.IO.DirectoryInfo(de)));
			}
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameSubdirectories, m_subdirectories.xmlstr);
			ret.SetAttribute(AttributeNameFiles, m_files.xmlstr);
			return ret;
		}
	}
	public class DirectoryInfoLClass : FileSystemInfoLClass
	{
		public DirectoryInfoLClass() : base(){}
		public DirectoryInfoLClass(DirectoryInfoLClass provided_obj) : base(provided_obj){}
		public DirectoryInfoLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new DirectoryInfoEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
