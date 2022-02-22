namespace MontyHall
{
	public class Quiz : AbstLib.EBaseClass
	{
		public const string AttributeNameDoors = "doors";
		public const string AttributeNameChosenDoor = "chosendoor";
		DoorL m_doorl;
		public DoorL doorl
		{
			get
			{
				return m_doorl;
			}
		}
		DoorE m_chosendoor;
		public DoorE chosendoor
		{
			get
			{
				return m_chosendoor;
			}
		}
		public int giftdoorseq
		{
			get
			{
				for(int icnt = 0; icnt < m_doorl.e.Length; icnt++)
				{
					DoorE myd = (DoorE)(m_doorl.e[icnt]);
					if(myd.that == DoorE.Gift)
					{
						return myd.seq;
					}
				}
				return -1;
			}
		}
		public int chosendoorseq
		{
			get
			{
				return m_chosendoor.seq;
			}
		}
		public bool bwin
		{
			get
			{
				bool ret = false;
				try
				{
					ret = (m_chosendoor.that == DoorE.Gift);
				}
				catch{}
				return ret;
			}
		}
		public Quiz(int provided_seq) : base(provided_seq)
		{
			m_doorl = new DoorL();
			System.Random r = new System.Random((int)System.DateTime.Now.Ticks);
			int idx = r.Next(3);
			for(int icnt = 0; icnt < 3; icnt++)
			{
				string val = DoorE.Goat;
				if(idx == icnt)
				{
					val = DoorE.Gift;
				}
				m_doorl = (DoorL)(m_doorl + new DoorE(m_doorl.maxseq + 1, val));
			}
		}
		public Quiz(Quiz provided_obj) : base(provided_obj)
		{
			m_chosendoor = provided_obj.chosendoor;
			m_doorl = provided_obj.doorl;
		}
		public void ChooseOne()
		{
			System.Random r = new System.Random((int)System.DateTime.Now.Ticks);
			m_chosendoor = (DoorE)(m_doorl.e[r.Next(3)]);
		}
		public void OpenOne()
		{
			System.Random r = new System.Random((int)System.DateTime.Now.Ticks);
			while(true)
			{
				int idx = r.Next(3);
				if(m_chosendoor.seq != ((DoorE)(m_doorl.e[idx])).seq)
				{
					if(((DoorE)(m_doorl.e[idx])).that != DoorE.Gift)
					{
						((DoorE)(m_doorl.e[idx])).Open();
						break;
					}
				}
			}
		}
		public void ChangeDoor()
		{
			for(int icnt = 0; icnt < m_doorl.e.Length; icnt++)
			{
				if(((DoorE)(m_doorl.e[icnt])).seq != m_chosendoor.seq)
				{
					if(!((DoorE)(m_doorl.e[icnt])).opened)
					{
						m_chosendoor = (DoorE)(m_doorl.e[icnt]);
					}
				}
			}
		}
		public Quiz(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			string chosendoorstr = provided_element.GetAttribute(AttributeNameChosenDoor);
			System.Xml.XmlDocument xc = new System.Xml.XmlDocument();
			xc.LoadXml(chosendoorstr);
			m_chosendoor = new DoorE((System.Xml.XmlElement)(xc.DocumentElement));
			string doorlstr = provided_element.GetAttribute(AttributeNameDoors);
			System.Xml.XmlDocument x = new System.Xml.XmlDocument();
			x.LoadXml(doorlstr);
			m_doorl = new DoorL((System.Xml.XmlElement)(x.DocumentElement));
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameDoors, m_doorl.xmlstr);
			ret.SetAttribute(AttributeNameChosenDoor, m_chosendoor.xmlstr);
			return ret;
		}
	}
	public class QuizL : AbstLib.LBaseClass
	{
		public QuizL() : base(){}
		public QuizL(QuizL provided_obj) : base(provided_obj){}
		public QuizL(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new Quiz((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
}
