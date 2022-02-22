namespace AbstLib
{
	public class LineEClass : EBaseClass
	{
		public const string AttributeNameLine = "line";
		protected string m_line;
		public string line
		{
			get
			{
				return m_line;
			}
		}
		public LineEClass(int provided_seq, string provided_line) : base(provided_seq)
		{
			m_line = provided_line;
		}
		public LineEClass(LineEClass provided_obj) : base(provided_obj)
		{
			m_line = provided_obj.line;
		}
		public LineEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_line = provided_element.GetAttribute(AttributeNameLine);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameLine, m_line);
			return ret;
		}
	}
	public class LineLClass : LBaseClass
	{
		public string strdata
		{
			get
			{
				string ret = string.Empty;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret += (((AbstLib.LineEClass)(m_e[icnt])).line + System.Environment.NewLine);
					}
				}
				return ret;
			}
		}
		public LineLClass() : base(){}
		public LineLClass(LineLClass provided_obj) : base(provided_obj){}
		public LineLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new LineEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
