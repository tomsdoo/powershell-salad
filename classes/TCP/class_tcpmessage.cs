namespace TCP
{
	public class TCPErrorMessageClass
	{
		public const int NothingParticular = 0;
	}
	public class TCPMessageBaseClass : AbstLib.EBaseClass
	{
		public const string AttributeNameCommand = "command";
		public const string AttributeNameCreatedAt = "createdat";
		public const string DateTimeFormatString = "yyyy/MM/dd HH:mm:ss";
		protected string m_command;
		public string command
		{
			get
			{
				return m_command;
			}
		}
		protected DateTime m_createdat;
		public DateTime createdat
		{
			get
			{
				return m_createdat;
			}
		}
		public TCPMessageBaseClass(int provided_seq, string provided_command) : base(provided_seq)
		{
			m_command = provided_command;
			m_createdat = DateTime.Now;
		}
		public TCPMessageBaseClass(TCPMessageBaseClass provided_obj) : base(provided_obj)
		{
			m_command = provided_obj.command;
			m_createdat = provided_obj.createdat;
		}
		public TCPMessageBaseClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_command = provided_element.GetAttribute(AttributeNameCommand);
			m_createdat = DateTime.ParseExact(provided_element.GetAttribute(AttributeNameCreatedAt), DateTimeFormatString, null);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameCommand, m_command);
			ret.SetAttribute(AttributeNameCreatedAt, m_createdat.ToString(DateTimeFormatString));
			return ret;
		}
	}
	public class TCPMessageLClass : AbstLib.LBaseClass
	{
		public TCPMessageLClass() : base(){}
		public TCPMessageLClass(TCPMessageLClass provided_obj) : base(provided_obj){}
		public TCPMessageLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				System.Xml.XmlElement ele = (System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt));
				JustAdd((new TCPMessageInterpreterClass(ele)).tcpmessage);
			}
		}
	}
	public class TCPPersonalInfoMessageClass : TCPMessageBaseClass
	{
		public const string AttributeNameProfile = "profile";
		protected ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		public TCPPersonalInfoMessageClass(int provided_seq, string provided_command, ProfileEClass provided_profile) : base(provided_seq, provided_command)
		{
			m_profile = provided_profile;
		}
		public TCPPersonalInfoMessageClass(TCPPersonalInfoMessageClass provided_obj) : base(provided_obj)
		{
			m_profile = provided_obj.profile;
		}
		public TCPPersonalInfoMessageClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			string profilexml = provided_element.GetAttribute(AttributeNameProfile);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(profilexml);
			m_profile = new ProfileEClass(x.DocumentElement);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameProfile, m_profile.xmlstr);
			return ret;
		}
	}
	public class TCPWhoAreYouMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "WhoAreYouCommand";
		public TCPWhoAreYouMessageClass(int provided_seq) : base(provided_seq, CommandString){}
		public TCPWhoAreYouMessageClass(TCPWhoAreYouMessageClass provided_obj) : base(provided_obj){}
		public TCPWhoAreYouMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPACKMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "ACKCommand";
		public TCPACKMessageClass(int provided_seq) : base(provided_seq, CommandString){}
		public TCPACKMessageClass(TCPACKMessageClass provided_obj) : base(provided_obj){}
		public TCPACKMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPPortTellingMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "PortTellingCommand";
		public TCPPortTellingMessageClass(int provided_port) : base(provided_port, CommandString){}
		public TCPPortTellingMessageClass(TCPPortTellingMessageClass provided_obj) : base(provided_obj){}
		public TCPPortTellingMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPNACKMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "NACKCommand";
		public TCPNACKMessageClass(int provided_seq) : base(provided_seq, CommandString){}
		public TCPNACKMessageClass(TCPNACKMessageClass provided_obj) : base(provided_obj){}
		public TCPNACKMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPServerCloseMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "ServerCloseCommand";
		public TCPServerCloseMessageClass(int provided_seq) : base(provided_seq, CommandString){}
		public TCPServerCloseMessageClass(TCPServerCloseMessageClass provided_obj) : base(provided_obj){}
		public TCPServerCloseMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPRequestForProfilesMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "RequestForProfilesCommand";
		public TCPRequestForProfilesMessageClass(int provided_seq) : base(provided_seq, CommandString){}
		public TCPRequestForProfilesMessageClass(TCPRequestForProfilesMessageClass provided_obj) : base(provided_obj){}
		public TCPRequestForProfilesMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPRequestForFeedMailMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "RequestForFeedMailCommand";
		public TCPRequestForFeedMailMessageClass(int provided_seq) : base(provided_seq, CommandString){}
		public TCPRequestForFeedMailMessageClass(TCPRequestForFeedMailMessageClass provided_obj) : base(provided_obj){}
		public TCPRequestForFeedMailMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPCloseMessageClass : TCPPersonalInfoMessageClass
	{
		public const string CommandString = "CloseCommand";
		public TCPCloseMessageClass(int provided_seq, ProfileEClass provided_profile) : base(provided_seq, CommandString, provided_profile){}
		public TCPCloseMessageClass(TCPCloseMessageClass provided_obj) : base(provided_obj){}
		public TCPCloseMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPMyNameIsMessageClass : TCPPersonalInfoMessageClass
	{
		public const string CommandString = "MyNameIsCommand";
		public TCPMyNameIsMessageClass(int provided_seq, ProfileEClass provided_profile) : base(provided_seq, CommandString, provided_profile){}
		public TCPMyNameIsMessageClass(TCPMyNameIsMessageClass provided_obj) : base(provided_obj){}
		public TCPMyNameIsMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPNewComerMessageClass : TCPPersonalInfoMessageClass
	{
		public const string CommandString = "NewComerCommand";
		public TCPNewComerMessageClass(int provided_seq, ProfileEClass provided_profile) : base(provided_seq, CommandString, provided_profile){}
		public TCPNewComerMessageClass(TCPNewComerMessageClass provided_obj) : base(provided_obj){}
		public TCPNewComerMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele){}
	}
	public class TCPMailMessageClass : TCPPersonalInfoMessageClass
	{
		public const string CommandString = "MailCommand";
		public const string AttributeNameEnvelope = "Envelope";
		protected TCPEnvelopeClass m_envelope;
		public TCPEnvelopeClass envelope
		{
			get
			{
				return m_envelope;
			}
		}
		public TCPMailMessageClass(int provided_seq, ProfileEClass provided_profile, TCPEnvelopeClass provided_envelope) : base(provided_seq, CommandString, provided_profile)
		{
			m_envelope = provided_envelope;
		}
		public TCPMailMessageClass(TCPMailMessageClass provided_obj) : base(provided_obj)
		{
			m_envelope = provided_obj.envelope;
		}
		public TCPMailMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele)
		{
			string mystr = provided_ele.GetAttribute(AttributeNameEnvelope);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(mystr);
			System.Xml.XmlElement myele = (System.Xml.XmlElement)(x.DocumentElement);
			m_envelope = new TCPEnvelopeClass(myele);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameEnvelope, m_envelope.xmlstr);
			return ret;
		}
	}
	public class TCPProfileListMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "ProfileListCommand";
		public const string AttributeNameProfileList = "ProfileList";
		protected ProfileLClass m_profilel;
		public ProfileLClass profilel
		{
			get
			{
				return m_profilel;
			}
		}
		public TCPProfileListMessageClass(int provided_seq, ProfileLClass provided_profilel) : base(provided_seq, CommandString)
		{
			m_profilel = provided_profilel;
		}
		public TCPProfileListMessageClass(TCPProfileListMessageClass provided_obj) : base(provided_obj)
		{
			m_profilel = provided_obj.profilel;
		}
		public TCPProfileListMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele)
		{
			string mystr = provided_ele.GetAttribute(AttributeNameProfileList);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(mystr);
			System.Xml.XmlElement myele = (System.Xml.XmlElement)(x.DocumentElement);
			m_profilel = new ProfileLClass(myele);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameProfileList, m_profilel.xmlstr);
			return ret;
		}
	}
	public class TCPFeedMailMessageClass : TCPMessageBaseClass
	{
		public const string CommandString = "FeedMailCommand";
		public const string AttributeNameMailList = "MailList";
		protected TCPEnvelopeLClass m_envelopel;
		public TCPEnvelopeLClass envelopel
		{
			get
			{
				return m_envelopel;
			}
		}
		public TCPFeedMailMessageClass(int provided_seq, TCPEnvelopeLClass provided_envelopel) : base(provided_seq, CommandString)
		{
			m_envelopel = provided_envelopel;
		}
		public TCPFeedMailMessageClass(TCPFeedMailMessageClass provided_obj) : base(provided_obj)
		{
			m_envelopel = provided_obj.envelopel;
		}
		public TCPFeedMailMessageClass(System.Xml.XmlElement provided_ele) : base(provided_ele)
		{
			string mystr = provided_ele.GetAttribute(AttributeNameMailList);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(mystr);
			System.Xml.XmlElement myele = (System.Xml.XmlElement)(x.DocumentElement);
			m_envelopel = new TCPEnvelopeLClass(myele);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameMailList, m_envelopel.xmlstr);
			return ret;
		}
	}
	public class TCPMessageInterpreterClass
	{
		TCPMessageBaseClass m_tcpmessage;
		public TCPMessageBaseClass tcpmessage
		{
			get
			{
				return m_tcpmessage;
			}
		}
		public TCPMessageInterpreterClass(string provided_str)
		{
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(provided_str);
			System.Xml.XmlElement ele = (System.Xml.XmlElement)(x.DocumentElement);
			InitializeFromElement(ele);
		}
		public TCPMessageInterpreterClass(System.Xml.XmlElement provided_element)
		{
			InitializeFromElement(provided_element);
		}
		private void InitializeFromElement(System.Xml.XmlElement ele)
		{
			TCPMessageBaseClass mb = new TCPMessageBaseClass(ele);
			switch(mb.command)
			{
				case TCPWhoAreYouMessageClass.CommandString:
					{
						m_tcpmessage = new TCPWhoAreYouMessageClass(ele);
						break;
					}
				case TCPACKMessageClass.CommandString:
					{
						m_tcpmessage = new TCPACKMessageClass(ele);
						break;
					}
				case TCPNACKMessageClass.CommandString:
					{
						m_tcpmessage = new TCPNACKMessageClass(ele);
						break;
					}
				case TCPPortTellingMessageClass.CommandString:
					{
						m_tcpmessage = new TCPPortTellingMessageClass(ele);
						break;
					}
				case TCPCloseMessageClass.CommandString:
					{
						m_tcpmessage = new TCPCloseMessageClass(ele);
						break;
					}
				case TCPServerCloseMessageClass.CommandString:
					{
						m_tcpmessage = new TCPServerCloseMessageClass(ele);
						break;
					}
				case TCPRequestForProfilesMessageClass.CommandString:
					{
						m_tcpmessage = new TCPRequestForProfilesMessageClass(ele);
						break;
					}
				case TCPRequestForFeedMailMessageClass.CommandString:
					{
						m_tcpmessage = new TCPRequestForFeedMailMessageClass(ele);
						break;
					}
				case TCPMyNameIsMessageClass.CommandString:
					{
						m_tcpmessage = new TCPMyNameIsMessageClass(ele);
						break;
					}
				case TCPNewComerMessageClass.CommandString:
					{
						m_tcpmessage = new TCPNewComerMessageClass(ele);
						break;
					}
				case TCPMailMessageClass.CommandString:
					{
						m_tcpmessage = new TCPMailMessageClass(ele);
						break;
					}
				case TCPProfileListMessageClass.CommandString:
					{
						m_tcpmessage = new TCPProfileListMessageClass(ele);
						break;
					}
				case TCPFeedMailMessageClass.CommandString:
					{
						m_tcpmessage = new TCPFeedMailMessageClass(ele);
						break;
					}
				default:
					{
						m_tcpmessage = mb;
						break;
					}
			}
		}
	}
}
