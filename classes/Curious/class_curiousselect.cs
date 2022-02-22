namespace Curious
{
	public class CuriousSelectResultClass : CuriousResultEBaseClass
	{
		public const string RecordAdded = "RecordAdded";
		public const string RecordDeleted = "RecordDeleted";
		public const string RecordUpdated = "RecordUpdated";
		string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		string m_sql;
		public string sql
		{
			get
			{
				return m_sql;
			}
		}
		ACC.RowLClass m_oldrl;
		public ACC.RowLClass oldrl
		{
			get
			{
				return m_oldrl;
			}
		}
		ACC.RowLClass m_newrl;
		public ACC.RowLClass newrl
		{
			get
			{
				return m_newrl;
			}
		}
		ACC.RowEClass m_changedrow;
		public ACC.RowEClass changedrow
		{
			get
			{
				return m_changedrow;
			}
		}
		ACC.RowLClass m_updatedrows;
		public ACC.RowLClass updatedrows
		{
			get
			{
				return m_updatedrows;
			}
		}
		string[] m_diffcols;
		public string[] diffcols
		{
			get
			{
				return m_diffcols;
			}
		}
		public CuriousSelectResultClass(	int provided_seq,
											string provided_status,
											string provided_filename,
											string provided_sql,
											ACC.RowLClass provided_oldrl,
											ACC.RowLClass provided_newrl,
											ACC.RowEClass provided_changedrow,
											ACC.RowLClass provided_updatedrows,
											string[] provided_diffcols) : base(provided_seq, provided_status)
		{
			m_filename = provided_filename;
			m_sql = provided_sql;
			m_oldrl = provided_oldrl;
			m_newrl = provided_newrl;
			m_changedrow = provided_changedrow;
			m_updatedrows = provided_updatedrows;
			m_diffcols = provided_diffcols;
		}
		public CuriousSelectResultClass(CuriousSelectResultClass provided_obj) : base(provided_obj)
		{
			m_filename = provided_obj.filename;
			m_sql = provided_obj.sql;
			m_oldrl = provided_obj.oldrl;
			m_newrl = provided_obj.newrl;
			m_changedrow = provided_obj.changedrow;
			m_updatedrows = provided_obj.updatedrows;
			m_diffcols = provided_obj.diffcols;
		}
	}
	public class CuriousSelectClass : CuriousBaseClass
	{
		public const string XmlElementNameFileName = "FileName";
		public const string XmlElementNameSQL = "Sql";
		public const string XmlElementNameResultCaptionColumns = "ResultCaptionColumns";
		public const string XmlElementNameKeyColumns = "KeyColumns";
		public new const string ClassName = "CuriousSelectClass";
		protected string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		protected string m_sql;
		public string sql
		{
			get
			{
				return m_sql;
			}
		}
		protected ACC.RowLClass m_rl;
		public ACC.RowLClass rl
		{
			get
			{
				return m_rl;
			}
		}
		protected string[] m_keycolumns;
		public string[] keycolumns
		{
			get
			{
				return m_keycolumns;
			}
		}
		public string keycolumnsstr
		{
			get
			{
				string ret = string.Empty;
				if(null != m_keycolumns)
				{
					bool bFirst = true;
					for(int icnt = 0; icnt < m_keycolumns.Length; icnt++)
					{
						if(bFirst)
						{
							bFirst = false;
						}
						else
						{
							ret += ",";
						}
						ret += m_keycolumns[icnt];
					}
				}
				return ret;
			}
		}
		protected string[] m_resultcaptioncolumns;
		public string[] resultcaptioncolumns
		{
			get
			{
				return m_resultcaptioncolumns;
			}
		}
		public string resultcaptioncolumnsstr
		{
			get
			{
				string ret = string.Empty;
				if(null != m_resultcaptioncolumns)
				{
					bool bFirst = true;
					for(int icnt = 0; icnt < m_resultcaptioncolumns.Length; icnt++)
					{
						if(bFirst)
						{
							bFirst = false;
						}
						else
						{
							ret += ",";
						}
						ret += m_resultcaptioncolumns[icnt];
					}
				}
				return ret;
			}
		}
		public override string sortkey
		{
			get
			{
				return (m_filename + " " + m_sql);
			}
		}
		public CuriousSelectClass(	int provided_seq,
									int provided_second,
									string provided_filename,
									string provided_sql,
									string provided_keycolumnsstr,
									string provided_resultcaptioncolumnsstr) : base(provided_seq, provided_second)
		{
			m_filename = provided_filename;
			m_sql = provided_sql;
			if(string.Empty == provided_resultcaptioncolumnsstr)
			{
				m_resultcaptioncolumns = null;
			}
			else
			{
				m_resultcaptioncolumns = provided_resultcaptioncolumnsstr.Split(',');
			}
			if(string.Empty == provided_keycolumnsstr)
			{
				m_keycolumns = null;
			}
			else
			{
				m_keycolumns = provided_keycolumnsstr.Split(',');
			}
			Initialize();
		}
		public CuriousSelectClass(	int provided_seq,
									int provided_second,
									string provided_filename,
									string provided_sql,
									string[] provided_keycolumns,
									string[] provided_resultcaptioncolumns) : base(provided_seq, provided_second)
		{
			m_filename = provided_filename;
			m_sql = provided_sql;
			m_resultcaptioncolumns = provided_resultcaptioncolumns;
			m_keycolumns = provided_keycolumns;
			Initialize();
		}
		public CuriousSelectClass(System.Xml.XmlElement provided_element) : base(provided_element)
		{
			m_filename = provided_element.GetAttribute(XmlElementNameFileName);
			m_sql = provided_element.GetAttribute(XmlElementNameSQL);
			string tempresultcolumnsstr = provided_element.GetAttribute(XmlElementNameResultCaptionColumns);
			if(string.Empty == tempresultcolumnsstr)
			{
				m_resultcaptioncolumns = null;
			}
			else
			{
				m_resultcaptioncolumns = tempresultcolumnsstr.Split(',');
			}
			string tempkeycolumnsstr = provided_element.GetAttribute(XmlElementNameKeyColumns);
			if(string.Empty == tempkeycolumnsstr)
			{
				m_keycolumns = null;
			}
			else
			{
				m_keycolumns = tempkeycolumnsstr.Split(',');
			}
			Initialize();
		}
		public override System.Xml.XmlElement GetXml(System.Xml.XmlDocument provided_xml)
		{
			System.Xml.XmlElement ret = provided_xml.CreateElement(ClassName);
			ret.SetAttribute(XmlElementNameSeq, m_seq.ToString());
			ret.SetAttribute(XmlElementNameSecond, m_second.ToString());
			ret.SetAttribute(XmlElementNameFileName, m_filename);
			ret.SetAttribute(XmlElementNameSQL, m_sql);
			ret.SetAttribute(XmlElementNameKeyColumns, keycolumnsstr);
			ret.SetAttribute(XmlElementNameResultCaptionColumns, resultcaptioncolumnsstr);
			return ret;
		}
		public override CuriousResultLClass Proceed()
		{
			CuriousResultLClass ret = new CuriousResultLClass();
			ACC.RowLClass myrl = MyExecuteQuery();
			ACC.RowLClass tempbeforerl = m_rl;
			ACC.RowLClass tempafterrl = myrl;
			ACC.RowLClass minustargetbeforerl = new ACC.RowLClass();
			ACC.RowLClass minustargetafterrl = new ACC.RowLClass();
			if(null != m_keycolumns)
			{
				if(null != tempbeforerl.e)
				{
					for(int lcnt = 0; lcnt < tempbeforerl.e.Length; lcnt++)
					{
						ACC.RowEClass myrow = tempbeforerl.e[lcnt];
						ACC.RowLClass tempselection = tempafterrl;
						for(int kcnt = 0; kcnt < m_keycolumns.Length; kcnt++)
						{
							string colname = m_keycolumns[kcnt];
							tempselection = tempselection.Select(colname, myrow[colname].ToString());
						}
						if(null != tempselection.e)
						{
							if(1 == tempselection.e.Length)
							{
								string[] diffcols = myrow.GetDifferentColumnNames(tempselection.e[0]);
								if(null != diffcols)
								{
									ACC.RowLClass updatedrl = new ACC.RowLClass();
									updatedrl += new ACC.RowEClass(updatedrl.maxseq + 1, myrow.coll);
									updatedrl += new ACC.RowEClass(updatedrl.maxseq + 1, tempselection.e[0].coll);
									ret += new CuriousSelectResultClass(	ret.maxseq + 1,
																			CuriousSelectResultClass.RecordUpdated,
																			m_filename,
																			m_sql,
																			m_rl,
																			myrl,
																			myrow,
																			updatedrl,
																			diffcols);
									minustargetbeforerl += myrow;
									minustargetafterrl += tempselection.e[0];
								}
							}
						}
					}
					if(null != minustargetbeforerl.e)
					{
						for(int minuscnt = 0; minuscnt < minustargetbeforerl.e.Length; minuscnt++)
						{
							tempbeforerl -= minustargetbeforerl.e[minuscnt];
						}
					}
					if(null != minustargetafterrl.e)
					{
						for(int minusacnt = 0; minusacnt < minustargetafterrl.e.Length; minusacnt++)
						{
							tempafterrl -= minustargetafterrl.e[minusacnt];
						}
					}
				}
			}
			
			if(null != tempafterrl.e)
			{
				for(int icnt = 0; icnt < tempafterrl.e.Length; icnt++)
				{
					ACC.RowEClass tempre = tempbeforerl.Find(tempafterrl.e[icnt]);
					if(null == tempre)
					{
						ret += new CuriousSelectResultClass(	ret.maxseq + 1,
																CuriousSelectResultClass.RecordAdded,
																m_filename,
																m_sql,
																m_rl,
																myrl,
																tempafterrl.e[icnt],
																new ACC.RowLClass(),
																null);
					}
				}
			}
			if(null != tempbeforerl.e)
			{
				for(int jcnt = 0; jcnt < tempbeforerl.e.Length; jcnt++)
				{
					if(null == tempafterrl.Find(tempbeforerl.e[jcnt]))
					{
						ret += new CuriousSelectResultClass(	ret.maxseq + 1,
																CuriousSelectResultClass.RecordDeleted,
																m_filename,
																m_sql,
																m_rl,
																myrl,
																tempbeforerl.e[jcnt],
																new ACC.RowLClass(),
																null);
					}
				}
			}
			m_rl = myrl;
			return ret;
		}
		protected virtual void Initialize()
		{
			m_rl = MyExecuteQuery();
		}
		protected virtual ACC.RowLClass MyExecuteQuery()
		{
			ACC.AccessClientSharpClass acc = new ACC.AccessClientSharpClass(m_filename);
			return acc.ExecuteQuery(m_sql);
		}
	}
	public class CuriousSelectDriverClass
	{
		CuriousSelectClass m_s;
		public CuriousSelectDriverClass(string provided_filename, string provided_sql, int provided_second)
		{
			int dummyseq = -1;
			m_s = new CuriousSelectClass(dummyseq, 5, provided_filename, provided_sql, "asdf","asdf");
			m_s.CuriousHappened += new CuriousHappenedEventHandler(MyHappened);
			m_s.StartTimer(provided_second);
		}
		public void End()
		{
			m_s.EndTimer();
		}
		protected void MyHappened(CuriousResultLClass provided_obj)
		{
			if(null != provided_obj.e)
			{
				for(int icnt = 0; icnt < provided_obj.e.Length; icnt++)
				{
					CuriousSelectResultClass myresult = (CuriousSelectResultClass)provided_obj.e[icnt];
					string message = string.Empty;
					message += myresult.dt.ToString("yyyy/MM/dd HH:mm:ss");
					message += " ";
					message += myresult.status;
					message += " ";
					message += myresult.filename;
					message += " ";
					message += myresult.sql;
					Console.WriteLine(message);
				}
			}
		}
	}
}
