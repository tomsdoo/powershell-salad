namespace IDE
{
	public class textfounde
	{
		int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		int m_row;
		public int row
		{
			get
			{
				return m_row;
			}
		}
		string m_line;
		public string line
		{
			get
			{
				return m_line;
			}
		}
		public string listline
		{
			get
			{
				string ret = string.Empty;
				ret += System.IO.Path.GetFileName(m_filename);
				ret += ("(" + m_row.ToString() + ")");
				ret += ("\t" + m_line);
				return ret;
			}
		}
		public textfounde(int provided_seq, string provided_filename, int provided_row, string provided_line)
		{
			m_seq = provided_seq;
			m_filename = provided_filename;
			m_row = provided_row;
			m_line = provided_line;
		}
		public textfounde(textfounde provided_obj)
		{
			m_seq = provided_obj.seq;
			m_filename = provided_obj.filename;
			m_row = provided_obj.row;
			m_line = provided_obj.line;
		}
	}
	public class textfoundl
	{
		textfounde[] m_e;
		public textfounde[] e
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
		public string[] listlines
		{
			get
			{
				string[] ret = null;
				if(null != m_e)
				{
					ret = new string[m_e.Length];
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						ret[icnt] = m_e[icnt].listline;
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
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				System.Xml.XmlElement h = x.CreateElement("html");
				x.AppendChild(h);
				System.Xml.XmlElement b = x.CreateElement("body");
				h.AppendChild(b);
				b.InnerXml = htmltable;
				ret = x.OuterXml;
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
					d.InnerText = "FileName";
					d = x.CreateElement("td");
					r.AppendChild(d);
					d.InnerText = "Row";
					d = x.CreateElement("td");
					r.AppendChild(d);
					d.InnerText = "Line";
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						textfounde tempe = m_e[icnt];
						r = x.CreateElement("tr");
						t.AppendChild(r);
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.filename;
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.row.ToString();
						d = x.CreateElement("td");
						r.AppendChild(d);
						d.InnerText = tempe.line;
					}
					ret = x.OuterXml;
				}
				return ret;
			}
		}
		public textfoundl()
		{
			m_e = null;
		}
		
		public textfoundl(textfoundl provided_obj)
		{
			m_e = provided_obj.e;
		}
		public static textfoundl operator + (textfoundl provided_l, textfounde provided_e)
		{
			textfoundl ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static textfoundl operator - (textfoundl provided_l, textfounde provided_e)
		{
			textfoundl ret = new textfoundl();
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
		public static textfoundl operator + (textfoundl provided_l1, textfoundl provided_l2)
		{
			textfoundl ret = provided_l1;
			if(null != provided_l2.e)
			{
				for(int icnt = 0; icnt < provided_l2.e.Length; icnt++)
				{
					textfounde tempe = provided_l2.e[icnt];
					textfounde tempealt = new textfounde(ret.maxseq + 1, tempe.filename, tempe.row, tempe.line);
					ret = ret + tempealt;
				}
			}
			return ret;
		}
		private int FindIndex(textfounde provided_obj)
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
		private void JustAdd(textfounde provided_obj)
		{
			if(null == m_e)
			{
				m_e = new textfounde[1];
				m_e[0] = provided_obj;
			}
			else
			{
				textfounde[] templ = new textfounde[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length -1] = provided_obj;
				m_e = templ;
			}
		}
	}
	public class textfindmanager
	{
		string m_rootfolder;
		public string rootfolder
		{
			get
			{
				return m_rootfolder;
			}
		}
		string m_findstr;
		public string findstr
		{
			get
			{
				return m_findstr;
			}
		}
		textfoundl m_tfl;
		public textfoundl tfl
		{
			get
			{
				return m_tfl;
			}
		}
		mysettings m_settings;
		public mysettings settings
		{
			get
			{
				return m_settings;
			}
		}
		public textfindmanager(string provided_rootfolder, string provided_findstr, mysettings provided_settings)
		{
			m_rootfolder = provided_rootfolder;
			m_findstr = provided_findstr;
			m_settings = provided_settings;
			m_tfl = new textfoundl();
			initialize();
		}
		public textfindmanager(textfindmanager provided_obj)
		{
			m_rootfolder = provided_obj.rootfolder;
			m_findstr = provided_obj.findstr;
			m_settings = provided_obj.settings;
			m_tfl = provided_obj.tfl;
		}
		private void initialize()
		{
			StreamReader reader = null;
			string[] fl = Directory.GetFiles(m_rootfolder, "*", System.IO.SearchOption.AllDirectories);
			string line = string.Empty;
			int row = 0;
			string findstr = m_findstr.ToUpper();
			foreach(string fe in fl)
			{
				reader = new StreamReader(fe, m_settings.enc);
				row = 0;
				while((line = reader.ReadLine()) != null)
				{
					if(-1 != line.ToUpper().IndexOf(findstr))
					{
						textfounde tfe = new textfounde(m_tfl.maxseq + 1, fe, row + 1, line);
						m_tfl = m_tfl + tfe;
					}
					row++;
				}
				reader.Close();
			}
		}
	}
}
