namespace Folder
{
	public class FileInfoEClass : FileSystemInfoEClass
	{
		public const string AttributeNameDirectoryName = "directoryname";
		public const string AttributeNameIsReadOnly = "isreadonly";
		public const string AttributeNameLength = "length";
		protected string m_directoryname;
		public string directoryname
		{
			get
			{
				return m_directoryname;
			}
		}
		protected bool m_isreadonly;
		public bool isreadonly
		{
			get
			{
				return m_isreadonly;
			}
		}
		protected long m_length;
		public long length
		{
			get
			{
				return m_length;
			}
		}
		public FileInfoEClass(	int provided_seq,
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
								string provided_directoryname,
								bool provided_isreadonly,
								long provided_length)
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
			m_directoryname = provided_directoryname;
			m_isreadonly = provided_isreadonly;
			m_length = provided_length;
		}
		public FileInfoEClass(int provided_seq, System.IO.FileInfo provided_fi) : base(provided_seq, provided_fi)
		{
			m_directoryname = provided_fi.DirectoryName;
			m_isreadonly = provided_fi.IsReadOnly;
			m_length = provided_fi.Length;
		}
		public FileInfoEClass(FileInfoEClass provided_obj) : base(provided_obj)
		{
			m_directoryname = provided_obj.directoryname;
			m_isreadonly = provided_obj.isreadonly;
			m_length = provided_obj.length;
		}
		public FileInfoEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_directoryname = provided_element.GetAttribute(AttributeNameDirectoryName);
			m_isreadonly = bool.Parse(provided_element.GetAttribute(AttributeNameIsReadOnly));
			m_length = long.Parse(provided_element.GetAttribute(AttributeNameLength));
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameDirectoryName, m_directoryname);
			ret.SetAttribute(AttributeNameIsReadOnly, m_isreadonly.ToString());
			ret.SetAttribute(AttributeNameLength, m_length.ToString());
			return ret;
		}
	}
	public class FileInfoLClass : FileSystemInfoLClass
	{
		public long length
		{
			get
			{
				long ret = 0;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret += ((FileInfoEClass)(m_e[icnt])).length;
					}
				}
				return ret;
			}
		}
		public FileInfoLClass() : base(){}
		public FileInfoLClass(FileInfoLClass provided_obj) : base(provided_obj){}
		public FileInfoLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new FileInfoEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
