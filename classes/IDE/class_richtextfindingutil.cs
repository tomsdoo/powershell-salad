namespace IDE
{
	public class founde
	{
		int m_seq;
		public int seq
		{
			get
			{
				return m_seq;
			}
		}
		int m_selectionstart;
		public int selectionstart
		{
			get
			{
				return m_selectionstart;
			}
		}
		int m_selectionlength;
		public int selectionlength
		{
			get
			{
				return m_selectionlength;
			}
		}
		public founde(int provided_seq, int provided_start, int provided_length)
		{
			m_seq = provided_seq;
			m_selectionstart = provided_start;
			m_selectionlength = provided_length;
		}
		public founde(founde provided_obj)
		{
			m_seq = provided_obj.seq;
			m_selectionstart = provided_obj.selectionstart;
			m_selectionlength = provided_obj.selectionlength;
		}
	}
	public class foundl
	{
		founde[] m_e;
		public founde[] e
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
		public foundl()
		{
			m_e = null;
		}
		public foundl(foundl provided_obj)
		{
			m_e = provided_obj.e;
		}
		
		public static foundl operator + (foundl provided_l, founde provided_e)
		{
			foundl ret = provided_l;
			if(-1 == provided_l.FindIndex(provided_e))
			{
				
				ret.JustAdd(provided_e);
			}
			return ret;
		}
		public static foundl operator - (foundl provided_l, founde provided_e)
		{
			foundl ret = new foundl();
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
		private int FindIndex(founde provided_obj)
		{
			return FindIndex(provided_obj.seq);
		}
		private int FindIndex(int provided_seq)
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					if(m_e[icnt].seq == provided_seq)
					{
						return icnt;
					}
				}
			}
			return -1;
		}
		private void JustAdd(founde provided_obj)
		{
			if(null == m_e)
			{
				m_e = new founde[1];
				m_e[0] = provided_obj;
			}
			else
			{
				founde[] templ = new founde[m_e.Length + 1];
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					templ[icnt] = m_e[icnt];
				}
				templ[templ.Length -1] = provided_obj;
				m_e = templ;
			}
		}
	}
	public class findutil
	{
		myrichtextbox m_tb;
		public myrichtextbox tb
		{
			get
			{
				return m_tb;
			}
		}
		public foundl commentfound
		{
			get
			{
				foundl ret = new foundl();
				if(0 < m_tb.Text.Length)
				{
					int icnt = 0;
					while(true)
					{
						if(icnt < m_tb.Text.Length)
						{
							int found = myfind("#", icnt);
							if(found >= 0)
							{
								int endfound = myfind("\r", found + 1);
								if(endfound >= 0)
								{
									founde myf = new founde(ret.maxseq + 1, found, endfound - found + 1);
									ret = ret + myf;
									icnt = endfound + 1;
								}
							}
							else
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
				}
				return ret;
			}
		}
		public foundl variablefound
		{
			get
			{
				foundl ret = new foundl();
				if(0 < m_tb.Text.Length)
				{
					int icnt = 0;
					while(true)
					{
						if(icnt < m_tb.Text.Length)
						{
							int found = myfind("$", icnt);
							if(found >= 0)
							{
								int[] endl = new int[8];
								endl[0] = myfind(".", found + 1);
								endl[1] = myfind(" ", found + 1);
								endl[2] = myfind(",", found + 1);
								endl[3] = myfind(")", found + 1);
								endl[4] = myfind(";", found + 1);
								endl[5] = myfind(":", found + 1);
								endl[6] = myfind("}", found + 1);
								endl[7] = myfind("\r", found + 1);
								System.Array.Sort(endl);
								int endfound = -1;
								foreach(int myi in endl)
								{
									if(myi >= 0)
									{
										endfound = myi;
										break;
									}
								}
								if(endfound >= 0)
								{
									founde myf = new founde(ret.maxseq + 1, found, endfound - found);
									ret = ret + myf;
									icnt = endfound + 1;
								}
							}
							else
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
				}
				return ret;
			}
		}
		public foundl classfound
		{
			get
			{
				foundl ret = new foundl();
				if(null != m_tb.cl.e)
				{
					foreach(classe tempc in m_tb.cl.e)
					{
						if(0 < m_tb.Text.Length)
						{
							int icnt = 0;
							while(true)
							{
								if(icnt < m_tb.Text.Length)
								{
									int found = myfind(tempc.name, icnt);
									if(found >= 0)
									{
										founde myf = new founde(ret.maxseq + 1, found, tempc.name.Length);
										ret = ret + myf;
										icnt = found + tempc.name.Length;
									}
									else
									{
										break;
									}
								}
								else
								{
									break;
								}
							}
						}
					}
				}
				return ret;
			}
		}
		public foundl cmdletfound
		{
			get
			{
				foundl ret = new foundl();
				if(null != m_tb.cmdletl)
				{
					foreach(string tempc in m_tb.cmdletl)
					{
						if(0 < m_tb.Text.Length)
						{
							int icnt = 0;
							while(true)
							{
								if(icnt < m_tb.Text.Length)
								{
									int found = myfind(tempc, icnt);
									if(found >= 0)
									{
										founde myf = new founde(ret.maxseq + 1, found, tempc.Length);
										ret = ret + myf;
										icnt = found + tempc.Length;
									}
									else
									{
										break;
									}
								}
								else
								{
									break;
								}
							}
						}
					}
				}
				return ret;
			}
		}
		public foundl functionfound
		{
			get
			{
				foundl ret = new foundl();
				if(null != m_tb.fl.e)
				{
					foreach(functione tempf in m_tb.fl.e)
					{
						if(0 < m_tb.Text.Length)
						{
							int icnt = 0;
							while(true)
							{
								if(icnt < m_tb.Text.Length)
								{
									int found = myfind(tempf.name, icnt);
									if(found >= 0)
									{
										founde myf = new founde(ret.maxseq + 1, found, tempf.name.Length);
										ret = ret + myf;
										icnt = found + tempf.name.Length;
									}
									else
									{
										break;
									}
								}
								else
								{
									break;
								}
							}
						}
					}
				}
				return ret;
			}
		}
		public foundl literalfound
		{
			get
			{
				foundl ret = new foundl();
				if(0 < m_tb.Text.Length)
				{
					int icnt = 0;
					while(true)
					{
						if(icnt < m_tb.Text.Length)
						{
							int found = myfind("\"", icnt);
							if(found >= 0)
							{
								int tempfound = found;
								int endfound = myfind("\"", tempfound + 1);
								while(true)
								{
									if(endfound < 0)
									{
										break;
									}
									if(m_tb.Text[endfound -1].ToString() != "`")
									{
										break;
									}
									tempfound = endfound;
									endfound = myfind("\"", endfound + 1);
								}
								{
								}
								if(endfound >= 0)
								{
									founde myf = new founde(ret.maxseq + 1, found, endfound - found + 1);
									ret = ret + myf;
									icnt = endfound + 1;
								}
							}
							else
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
				}
				return ret;
			}
		}
		public findutil(myrichtextbox provided_obj)
		{
			m_tb = provided_obj;
		}
		public findutil(findutil provided_obj)
		{
			m_tb = provided_obj.tb;
		}
		public foundl findstr(string provided_str)
		{
				foundl ret = new foundl();
				if(0 < m_tb.Text.Length)
				{
					int icnt = 0;
					while(true)
					{
						if(icnt < m_tb.Text.Length)
						{
							int found = myfind(provided_str, icnt);
							if(found >= 0)
							{
								founde myf = new founde(ret.maxseq + 1, found, provided_str.Length);
								ret = ret + myf;
								icnt = found + provided_str.Length + 1;
							}
							else
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
				}
				return ret;
		}
		private int myfind(string provided_findstr, int provided_start)
		{
			return m_tb.Find(provided_findstr, provided_start, System.Windows.Forms.RichTextBoxFinds.None);
		}
	}
	public class rtfindform : Form
	{
		public const bool findonly = false;
		public const bool findandreplace = true;
		string m_titlestr;
		string m_findstr;
		public string findstr
		{
			get
			{
				return m_findstr;
			}
		}
		string m_replacestr;
		public string replacestr
		{
			get
			{
				return m_replacestr;
			}
		}
		bool m_breplace;
		public bool breplace
		{
			get
			{
				return m_breplace;
			}
		}
		public rtfindform(bool provided_breplace)
		{
			m_breplace = provided_breplace;
			m_titlestr = "input string to find";
			if(m_breplace)
			{
				m_titlestr += " and replace";
			}
			m_findstr = string.Empty;
			m_replacestr = string.Empty;
			initialize();
		}
		TextBox m_findbox;
		TextBox m_replacebox;
		Button okbutton;
		Button cancelbutton;
		private void initialize()
		{
			this.SuspendLayout();
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Text = m_titlestr;
			this.Height = 25;
			
			cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			cancelbutton.Click += new System.EventHandler(canceleventhandler);
			this.CancelButton = cancelbutton;
			this.Controls.Add(cancelbutton);
			this.Height += cancelbutton.Height;
			
			okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			okbutton.Click += new System.EventHandler(okeventhandler);
			this.AcceptButton = okbutton;
			this.Controls.Add(okbutton);
			this.Height += okbutton.Height;
			
			setcontrols();
			settaborder();
			
			this.ResumeLayout();
		}
		private void setcontrols()
		{
			if(m_breplace)
			{
				m_replacebox = new TextBox();
				m_replacebox.Dock = System.Windows.Forms.DockStyle.Top;
				this.Controls.Add(m_replacebox);
				this.Height += m_replacebox.Height;
			}

			m_findbox = new TextBox();
			m_findbox.Dock = System.Windows.Forms.DockStyle.Top;
			this.Controls.Add(m_findbox);
			this.Height += m_findbox.Height;
		}
		private void settaborder()
		{
			int taborder = 0;
			m_findbox.TabIndex = taborder;
			if(m_breplace)
			{
				taborder++;
				m_replacebox.TabIndex = taborder;
			}
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
		}
		private void canceleventhandler(object sender, System.EventArgs e)
		{
			Close();
		}
		private void okeventhandler(object sender, System.EventArgs e)
		{
			m_findstr = m_findbox.Text;
			if(m_breplace)
			{
				m_replacestr = m_replacebox.Text;
			}
			Close();
		}
	}
}
