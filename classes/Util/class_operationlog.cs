/*
namespace Util
{
	public class LogEClass
	{
		protected int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		protected DateTime m_dt;
		public DateTime dt
		{
			get
			{
				return m_dt;
			}
		}
		protected string m_classname;
		public string classname
		{
			get
			{
				return m_classname;
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
				ret = m_dt.ToString("yyyy/MM/dd HH:mm:ss");
				ret += "\t";
				ret += m_classname;
				ret += "\t";
				ret += m_message;
				return ret;
			}
		}
		public LogEClass(int provided_seq, DateTime provided_dt, string provided_classname, string provided_message)
		{
			m_seq = provided_seq;
			m_dt = provided_dt;
			m_classname = provided_classname;
			m_message = provided_message;
		}
		public LogEClass(int provided_seq, string provided_classname, string provided_message)
		{
			m_seq = provided_seq;
			m_dt = DateTime.Now;
			m_classname = provided_classname;
			m_message = provided_message;
		}
		public LogEClass(LogEClass provided_obj)
		{
			m_seq = provided_obj.seq;
			m_dt = provided_obj.dt;
			m_classname = provided_obj.classname;
			m_message = provided_obj.message;
		}
	}
	public class LogLClass
	{
		protected LogEClass[] m_e;
		public LogEClass[] e
		{
			get
			{
				return m_e;
			}
		}
		public int maxseq
		{
			get
			{
				int ret = 0;
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						if(ret < m_e[icnt].seq)
						{
							ret = m_e[icnt].seq;
						}
					}
				}
				return ret;
			}
		}
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
						ret[icnt] = m_e[icnt].listtext;
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
					System.Xml.XmlElement h = x.CreateElement("html");
					x.AppendChild(h);
					System.Xml.XmlElement b = x.CreateElement("body");
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
					d.InnerText = "Class";
					d = x.CreateElement("td");
					r.AppendChild(d);
					d.InnerText = "Message";
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						LogEClass tempe = m_e[icnt];
						r = x.CreateElement("tr");
						t.AppendChild(r);
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.dt.ToString("yyyy/MM/dd HH:mm:ss");
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.classname;
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.message;
					}
					ret = x.OuterXml;
				}
				return ret;
			}
		}
		public LogLClass()
		{
			m_e = null;
		}
		public LogLClass(LogLClass provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static LogLClass operator + (LogLClass provided_l, LogEClass provided_e)
		{
			LogLClass ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static LogLClass operator - (LogLClass provided_l, LogEClass provided_e)
		{
			LogLClass ret = new LogLClass();
			int idx = provided_l.FindIndex(provided_e);
			if(null != provided_l.e)
			{
				for(int icnt = 0; icnt < provided_l.e.Length; icnt++)
				{
					if(idx != icnt)
					{
						ret = ret + provided_l.e[icnt];
					}
				}
			}
			return ret;
		}
		protected int FindIndex(LogEClass provided_obj)
		{
			return FindIndex(provided_obj.seq);
		}
		protected int FindIndex(int provided_seq)
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					if(provided_seq == m_e[icnt].seq)
					{
						return icnt;
					}
				}
			}
			return -1;
		}
		protected void JustAdd(LogEClass provided_obj)
		{
			if(null == m_e)
			{
				m_e = new LogEClass[1];
				m_e[0] = provided_obj;
			}
			else
			{
				LogEClass[] templ = new LogEClass[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
		}
	}
	public class LogListBox : ListBox
	{
		protected LogLClass m_ll;
		public LogLClass ll
		{
			get
			{
				return m_ll;
			}
		}
		public LogListBox(LogLClass provided_ll)
		{
			m_ll = provided_ll;
			initialize();
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
		protected void initialize()
		{
			Dock = System.Windows.Forms.DockStyle.Fill;
			Reflect();
		}
	}
	public class LogListForm : Form
	{
		public const string titlestr = "Events";
		protected LogLClass m_ll;
		public LogLClass ll
		{
			get
			{
				return m_ll;
			}
		}
		protected LogListBox m_lb;
		public LogListBox lb
		{
			get
			{
				return m_lb;
			}
		}
		public LogListForm(LogLClass provided_ll)
		{
			m_ll = provided_ll;
			Initialize();
		}
		public void FeedLogL(LogLClass provided_ll)
		{
			m_ll = provided_ll;
			m_lb.FeedLogL(m_ll);
		}
		protected virtual void Initialize()
		{
			Text = titlestr;
			setmenu();
			m_lb = new LogListBox(m_ll);
			this.Controls.Add(m_lb);
		}
		protected virtual void setmenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ToolStripMenuItem itemstrip = new ToolStripMenuItem("&Item");
			itemstrip.DropDownItems.Add(CreateItemRefreshMenu(true));
			itemstrip.DropDownItems.Add(CreateItemExportMenu(true));
			
			mymenu.Items.Add(itemstrip);
			this.MainMenuStrip = mymenu;
			mymenu.Visible = false;
			this.Controls.Add(mymenu);
		}
		protected virtual ToolStripMenuItem CreateItemRefreshMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Reflesh", null, new System.EventHandler(ItemRefreshMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.F5;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected virtual ToolStripMenuItem CreateItemExportMenu(bool provided_hasshortcut)
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
		}
		protected virtual void ItemExportMenu(object sender, System.EventArgs e)
		{
		}
	}
}
*/
