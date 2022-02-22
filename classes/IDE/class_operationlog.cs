namespace IDE
{
	public delegate void operationlogrefresheventhandler();
	public class operationloge
	{
		int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		DateTime m_dt;
		public DateTime dt
		{
			get
			{
				return m_dt;
			}
		}
		string m_classname;
		public string classname
		{
			get
			{
				return m_classname;
			}
		}
		string m_message;
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
		public operationloge(int provided_seq, DateTime provided_dt, string provided_classname, string provided_message)
		{
			m_seq = provided_seq;
			m_dt = provided_dt;
			m_classname = provided_classname;
			m_message = provided_message;
		}
		public operationloge(int provided_seq, string provided_classname, string provided_message)
		{
			m_seq = provided_seq;
			m_dt = DateTime.Now;
			m_classname = provided_classname;
			m_message = provided_message;
		}
		public operationloge(operationloge provided_obj)
		{
			m_seq = provided_obj.seq;
			m_dt = provided_obj.dt;
			m_classname = provided_obj.classname;
			m_message = provided_obj.message;
		}
	}
	public class operationlogl
	{
		operationloge[] m_e;
		public operationloge[] e
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
						operationloge tempe = m_e[icnt];
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
		public operationlogl()
		{
			m_e = null;
		}
		public operationlogl(operationlogl provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static operationlogl operator + (operationlogl provided_l, operationloge provided_e)
		{
			operationlogl ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static operationlogl operator - (operationlogl provided_l, operationloge provided_e)
		{
			operationlogl ret = new operationlogl();
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
		private int FindIndex(operationloge provided_obj)
		{
			return FindIndex(provided_obj.seq);
		}
		private int FindIndex(int provided_seq)
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
		private void JustAdd(operationloge provided_obj)
		{
			if(null == m_e)
			{
				m_e = new operationloge[1];
				m_e[0] = provided_obj;
			}
			else
			{
				operationloge[] templ = new operationloge[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length - 1] = provided_obj;
				m_e = templ;
			}
		}
	}
	public class operationloglistbox : ListBox
	{
		operationlogl m_ll;
		public operationlogl ll
		{
			get
			{
				return m_ll;
			}
		}
		public operationloglistbox(operationlogl provided_ll)
		{
			m_ll = provided_ll;
			initialize();
		}
		public void feedlogl(operationlogl provided_ll)
		{
			m_ll = provided_ll;
			reflect();
		}
		private void reflect()
		{
			this.DataSource = m_ll.listtexts;
		}
		private void initialize()
		{
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			reflect();
		}
	}
	public class operationloglistform : Form
	{
		public const string titlestr = "Events";
		public event operationlogrefresheventhandler operationlogrefreshevent;
		public event mdimessageeventhandler messageevent;
		operationlogl m_ll;
		public operationlogl ll
		{
			get
			{
				return m_ll;
			}
		}
		operationloglistbox m_lb;
		public operationloglistbox lb
		{
			get
			{
				return m_lb;
			}
		}
		public operationloglistform(operationlogl provided_ll)
		{
			m_ll = provided_ll;
			initialize();
		}
		public void feedlogl(operationlogl provided_ll)
		{
			m_ll = provided_ll;
			m_lb.feedlogl(m_ll);
		}
		private void initialize()
		{
			this.Text = titlestr;
			setmenu();
			m_lb = new operationloglistbox(m_ll);
			this.Controls.Add(m_lb);
		}
		private void setmenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ToolStripMenuItem itemstrip = new ToolStripMenuItem("&Item");
			ToolStripMenuItem refreshstrip = new ToolStripMenuItem("&Reflesh", null, new System.EventHandler(itemrefreshmenu));
			refreshstrip.ShortcutKeys = System.Windows.Forms.Keys.F5;
			refreshstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(refreshstrip);
			
			ToolStripMenuItem exportstrip = new ToolStripMenuItem("&Export", null, new System.EventHandler(exportmenu));
			exportstrip.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
			exportstrip.ShowShortcutKeys = true;
			itemstrip.DropDownItems.Add(exportstrip);
			
			mymenu.Items.Add(itemstrip);
			this.MainMenuStrip = mymenu;
			mymenu.Visible = false;
			this.Controls.Add(mymenu);
		}
		private void itemrefreshmenu(object sender, System.EventArgs e)
		{
			if(null != operationlogrefreshevent)
			{
				operationlogrefreshevent();
			}
		}
		private void exportmenu(object sender, System.EventArgs e)
		{
			exportfileform ef = new exportfileform();
			if(System.Windows.Forms.DialogResult.OK == ef.ShowDialog())
			{
				try
				{
					string dn = System.IO.Path.GetDirectoryName(ef.filename);
					if(!System.IO.Directory.Exists(dn))
					{
						System.IO.Directory.CreateDirectory(dn);
					}
					StreamWriter writer = new StreamWriter(ef.filename, false);
					writer.Write(m_ll.html);
					writer.Close();
					messageout("event exported " + ef.filename);
				}
				catch
				{
					messageout("event exporting failed " + ef.filename);
				}
			}
			ef.Dispose();
		}
		private void messageout(string provided_message)
		{
			if(null != messageevent)
			{
				messageevent("Event", provided_message);
			}
		}
	}
}
