namespace Util
{
	public class LogEClass : AbstLib.EBaseClass
	{
		protected const string AttributeNameCreatedAt = "createdat";
		protected const string AttributeNameFrom = "from";
		protected const string AttributeNameMessage = "message";
		protected System.DateTime m_createdat;
		public System.DateTime createdat
		{
			get
			{
				return m_createdat;
			}
		}
		protected string m_from;
		public string from
		{
			get
			{
				return m_from;
			}
		}
		protected string m_message;
		public string message
		{
			get
			{
				return m_message;
			}
		}
		public string listtext
		{
			get
			{
				string ret = string.Empty;
				ret = m_createdat.ToString("yyyy/MM/dd HH:mm:ss");
				ret += "\t";
				ret += m_from;
				ret += "\t";
				ret += m_message;
				return ret;
			}
		}
		public LogEClass(int provided_seq, string provided_from, string provided_message) : base(provided_seq)
		{
			m_createdat = System.DateTime.Now;
			m_from = provided_from;
			m_message = provided_message;
		}
		public LogEClass(LogEClass provided_obj) : base(provided_obj)
		{
			m_createdat = provided_obj.createdat;
			m_from = provided_obj.from;
			m_message = provided_obj.message;
		}
		public LogEClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_createdat = System.DateTime.ParseExact(provided_element.GetAttribute(AttributeNameCreatedAt), "yyyy/MM/dd HH:mm:ss", null);
			m_from = provided_element.GetAttribute(AttributeNameFrom);
			m_message = provided_element.GetAttribute(AttributeNameMessage);
		}
		protected override System.Xml.XmlElement SetAttribute(System.Xml.XmlElement provided_element)
		{
			System.Xml.XmlElement ret = base.SetAttribute(provided_element);
			ret.SetAttribute(AttributeNameCreatedAt, m_createdat.ToString("yyyy/MM/dd HH:mm:ss"));
			ret.SetAttribute(AttributeNameFrom, m_from);
			ret.SetAttribute(AttributeNameMessage, m_message);
			return ret;
		}
	}
	public class LogLClass : AbstLib.LBaseClass
	{
		public string[] listtexts
		{
			get
			{
				string[] ret = null;
				if(null != m_e)
				{
					ret = new string[m_e.Length];
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret[icnt] = ((LogEClass)(m_e[icnt])).listtext;
					}
				}
				return ret;
			}
		}
		public string html
		{
			get
			{
				string ret = string.Empty;
				string mytable = htmltable;
				if(string.Empty != mytable)
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					System.Xml.XmlElement h = x.CreateElement("HTML");
					x.AppendChild(h);
					System.Xml.XmlElement b = x.CreateElement("BODY");
					h.AppendChild(b);
					b.InnerXml = mytable;
					ret = x.OuterXml;
				}
				return ret;
			}
		}
		public string htmltable
		{
			get
			{
				string ret = string.Empty;
				if(null != m_e)
				{
					System.Xml.XmlDocument x = new System.Xml.XmlDocument();
					System.Xml.XmlElement t = x.CreateElement("table");
					x.AppendChild(t);
					t.SetAttribute("border","1");
					System.Xml.XmlElement r = x.CreateElement("tr");
					t.AppendChild(r);
					System.Xml.XmlElement d = x.CreateElement("td");
					r.AppendChild(d);
					d.InnerText = "Date";
					d = x.CreateElement("td");
					r.AppendChild(d);
					d.InnerText = "From";
					d = x.CreateElement("td");
					r.AppendChild(d);
					d.InnerText = "Message";
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						LogEClass tempe = (LogEClass)(m_e[icnt]);
						r = x.CreateElement("tr");
						t.AppendChild(r);
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.createdat.ToString("yyyy/MM/dd HH:mm:ss");
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.from;
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.message;
					}
					ret = x.OuterXml;
				}
				return ret;
			}
		}
		public LogLClass() : base(){}
		public LogLClass(LogLClass provided_obj) : base(provided_obj){}
		public LogLClass(System.Xml.XmlElement provided_element) : base(provided_element){}
		protected override void InitializeFromXmlElement(System.Xml.XmlElement provided_element)
		{
			for(int icnt = 0; icnt < provided_element.ChildNodes.Count; icnt++)
			{
				JustAdd(new LogEClass((System.Xml.XmlElement)(provided_element.ChildNodes.Item(icnt))));
			}
		}
	}
	public class LogLB : System.Windows.Forms.ListBox
	{
		protected LogLClass m_ll;
		public LogLClass ll
		{
			get
			{
				return m_ll;
			}
		}
		public LogLB(LogLClass provided_ll)
		{
			m_ll = provided_ll;
			Initialize();
		}
		public void FeedLogL(LogLClass provided_ll)
		{
			m_ll = provided_ll;
			Reflect();
		}
		protected void Reflect()
		{
			DataSource = m_ll.listtexts;
		}
		protected void Initialize()
		{
			Dock = System.Windows.Forms.DockStyle.Fill;
			Reflect();
		}
	}
	public delegate void LogLFormRefreshEvehtHandler();
	public class LogLForm : Util.FormBaseClass
	{
		public event LogLFormRefreshEvehtHandler RefreshLog;
		public const string titlestr = "Events";
		protected LogLClass m_ll;
		public LogLClass ll
		{
			get
			{
				return m_ll;
			}
		}
		protected LogLB m_lb;
		public LogLB lb
		{
			get
			{
				return m_lb;
			}
		}
		public LogLForm(LogLClass provided_ll) : base()
		{
			m_ll = provided_ll;
			m_lb = new LogLB(m_ll);
			Controls.Add(m_lb);
			Initialize();
		}
		public void FeedLogL(LogLClass provided_ll)
		{
			m_ll = provided_ll;
			m_lb.FeedLogL(m_ll);
		}
		protected override void Initialize()
		{
			Text = titlestr;
		}
		protected override void InitializeMenu()
		{
			System.Windows.Forms.ToolStripMenuItem itemstrip = new System.Windows.Forms.ToolStripMenuItem("&Item");
			m_mymenu.Items.Add(itemstrip);
			itemstrip.DropDownItems.Add(CreateRefreshMenu(true));
			m_cmenu.Items.Add(CreateRefreshMenu(false));
			itemstrip.DropDownItems.Add(CreateExportMenu(true));
			m_cmenu.Items.Add(CreateExportMenu(false));
		}
		protected override void InitializeStatus(){}
		protected virtual System.Windows.Forms.ToolStripMenuItem CreateRefreshMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Refresh", null, new System.EventHandler(ItemRefreshMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.F5;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual System.Windows.Forms.ToolStripMenuItem CreateExportMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Export", null, new System.EventHandler(ItemExportMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual void ItemRefreshMenu(object sender, System.EventArgs e)
		{
			if(null != RefreshLog)
			{
				RefreshLog();
			}
		}
		protected virtual void ItemExportMenu(object sender, System.EventArgs e)
		{
			GetFileNameForm fd = new GetFileNameForm("file name?", "report.htm");
			if(System.Windows.Forms.DialogResult.OK == fd.ShowDialog())
			{
				System.IO.StreamWriter writer = new System.IO.StreamWriter(fd.filename, false);
				writer.Write(m_ll.html);
				writer.Close();
			}
			fd.Dispose();
		}
	}
}
