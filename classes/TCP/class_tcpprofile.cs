namespace TCP
{
	public class ProfileEClass : AbstLib.EBaseClass
	{
		public const string AttributeNameName = "Name";
		public const string AttributeNameNote = "Note";
		protected string m_name;
		public string name
		{
			get
			{
				return m_name;
			}
		}
		protected string m_note;
		public string note
		{
			get
			{
				return m_note;
			}
		}
		public ACC.RowEClass rowe
		{
			get
			{
				ACC.ColLClass coll = new ACC.ColLClass();
				coll += new ACC.ColEIntClass(coll.maxseq + 1, AttributeNameSeq, m_seq);
				coll += new ACC.ColEStringClass(coll.maxseq + 1, AttributeNameName, m_name);
				coll += new ACC.ColEStringClass(coll.maxseq + 1, AttributeNameNote, m_note);
				ACC.RowEClass ret = new ACC.RowEClass(m_seq, coll);
				return ret;
			}
		}
		public string description
		{
			get
			{
				string ret = string.Empty;
				ret += (AttributeNameName + ":" + m_name + "\r\n");
				ret += (AttributeNameNote + ":\r\n" + m_note + "\r\n");
				return ret;
			}
		}
		public ProfileEClass(int provided_seq, string provided_name, string provided_note) : base(provided_seq)
		{
			m_name = provided_name;
			m_note = provided_note;
		}
		public ProfileEClass(ProfileEClass provided_obj) : base(provided_obj)
		{
			m_name = provided_obj.name;
			m_note = provided_obj.note;
		}
		public ProfileEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_name = provided_element.GetAttribute(AttributeNameName);
			m_note = provided_element.GetAttribute(AttributeNameNote);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameName, m_name);
			ret.SetAttribute(AttributeNameNote, m_note);
			return ret;
		}
	}
	public class ProfileLClass : AbstLib.LBaseClass
	{
		public ACC.RowLClass rowl
		{
			get
			{
				ACC.RowLClass ret = new ACC.RowLClass();
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ProfileEClass myprof = (ProfileEClass)(m_e[icnt]);
						ret = ret + myprof.rowe;
					}
				}
				return ret;
			}
		}
		public ProfileLClass() : base(){}
		public ProfileLClass(ProfileLClass provided_obj) : base(provided_obj){}
		public ProfileLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new ProfileEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
	public class CommClientEClass : AbstLib.EBaseClass
	{
		ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		TCPEnvelopeLClass m_envelopel;
		public TCPEnvelopeLClass envelopel
		{
			get
			{
				return m_envelopel;
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
		public CommClientEClass(ProfileEClass provided_profile, System.Net.Sockets.Socket provided_socket) : base(provided_profile.seq)
		{
			m_profile = provided_profile;
			m_soc = provided_socket;
			m_envelopel = new TCPEnvelopeLClass();
		}
		public CommClientEClass(ProfileEClass provided_profile, System.Net.Sockets.Socket provided_socket, TCPEnvelopeLClass provided_envelopel) : base(provided_profile.seq)
		{
			m_profile = provided_profile;
			m_soc = provided_socket;
			m_envelopel = provided_envelopel;
		}
		public CommClientEClass(CommClientEClass provided_obj) : base(provided_obj.seq)
		{
			m_profile = provided_obj.profile;
			m_soc = provided_obj.soc;
			m_envelopel = provided_obj.envelopel;
		}
		public void Feed(TCPEnvelopeClass provided_envelope)
		{
			m_envelopel = (TCPEnvelopeLClass)(m_envelopel + provided_envelope);
		}
	}
	public class CommClientLClass : AbstLib.LBaseClass
	{
		public CommClientLClass() : base(){}
		public CommClientLClass(CommClientLClass provided_obj) : base(provided_obj){}
	}
}
