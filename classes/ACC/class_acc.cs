namespace ACC
{
	public class AccessClientSharpClass
	{
		public const string ProviderJet = "Microsoft.Jet.OLEDB.4.0";
		public const string ProviderAce = "Microsoft.ACE.OLEDB.12.0";
		string m_provider;
		public string provider
		{
			get
			{
				return m_provider;
			}
		}
		string m_mdbfilename;
		public string mdbfilename
		{
			get
			{
				return m_mdbfilename;
			}
		}
		public string connectionstr
		{
			get
			{
				string ret = string.Empty;
				ret = "Provider=" + m_provider + ";Data Source=" + m_mdbfilename;
				return ret;
			}
		}
		RowLClass m_schemarl;
		public RowLClass schemarl
		{
			get
			{
				return m_schemarl;
			}
		}
		bool m_schemainitialized;
		public bool schemainitialized
		{
			get
			{
				return m_schemainitialized;
			}
		}
		public AccessClientSharpClass(string provided_mdbfilename)
		{
			m_schemainitialized = false;
			m_schemarl = null;
			m_provider = ProviderJet;
			m_mdbfilename = provided_mdbfilename;
		}
		public AccessClientSharpClass(string provided_provider, string provided_mdbfilename)
		{
			m_schemarl = null;
			m_provider = provided_provider;
			m_mdbfilename = provided_mdbfilename;
		}
		public RowLClass ExecuteQuery(string provided_sql)
		{
			RowLClass ret = new RowLClass();
			System.Data.OleDb.OleDbConnection db = null;
			System.Data.OleDb.OleDbCommand comm = null;
			db = new System.Data.OleDb.OleDbConnection(connectionstr);
			comm = new System.Data.OleDb.OleDbCommand(provided_sql, db);
			db.Open();
			System.Data.OleDb.OleDbDataReader reader = null;
			reader = comm.ExecuteReader();
			while(reader.Read())
			{
				ColLClass tempcoll = new ColLClass();
				for(int icnt = 0; icnt < reader.FieldCount; icnt++)
				{
					string fieldname = reader.GetName(icnt);
					System.Type mytype = reader.GetFieldType(icnt);
					switch(mytype.Name)
					{
						case ColEBoolClass.TypeName:
							{
								bool bval = false;
								try
								{
									bval = reader.GetBoolean(icnt);
								}
								catch
								{
								}
								tempcoll += new ColEBoolClass(tempcoll.maxseq + 1, fieldname, bval);
								break;
							}
						case ColEIntClass.TypeName:
							{
								int ival = int.MinValue;
								try
								{
									ival = reader.GetInt32(icnt);
								}
								catch
								{
								}
								tempcoll += new ColEIntClass(tempcoll.maxseq + 1, fieldname, ival);
								break;
							}
						case ColEDateClass.TypeName:
							{
								DateTime dval = DateTime.MinValue;
								try
								{
									dval = reader.GetDateTime(icnt);
								}
								catch
								{
								}
								tempcoll += new ColEDateClass(tempcoll.maxseq + 1, fieldname, dval);
								break;
							}
						case ColEStringClass.TypeName:
							{
								string sval = string.Empty;
								try
								{
									sval = reader.GetString(icnt);
								}
								catch
								{
								}
								tempcoll += new ColEStringClass(tempcoll.maxseq + 1, fieldname, sval);
								break;
							}
						default:
							{
								tempcoll += new ColEBaseClass(tempcoll.maxseq + 1, fieldname);
								break;
							}
					}
				}
				ret += new RowEClass(ret.maxseq + 1, tempcoll);
			}
			reader.Close();
			db.Close();
			return ret;
		}
		public int ExecuteNonQuery(string provided_sql)
		{
			int ret = 0;
			System.Data.OleDb.OleDbConnection db = null;
			System.Data.OleDb.OleDbCommand comm = null;
			try
			{
				db = new System.Data.OleDb.OleDbConnection(connectionstr);
				comm = new System.Data.OleDb.OleDbCommand(provided_sql, db);
				db.Open();
				ret = comm.ExecuteNonQuery();
			}
			catch
			{
				throw;
			}
			finally
			{
				comm.Dispose();
				db.Close();
			}
			return ret;
		}
		public string[] GetColumns(string provided_tablename)
		{
			InitializeSchema();
			RowLClass trl = m_schemarl.Select("TableName", provided_tablename);
			ColLClass cl = new ColLClass();
			if(null != trl.e)
			{
				for(int icnt = 0; icnt < trl.e.Length; icnt++)
				{
					ColEStringClass mye = (ColEStringClass)(trl.e[icnt].coll["ColumnName"]);
					cl += new ColEStringClass(cl.maxseq + 1, mye.name, mye.value);
				}
			}
			string[] ret = null;
			if(null != cl.e)
			{
				ret = new string[cl.e.Length];
				for(int jcnt = 0; jcnt < cl.e.Length; jcnt++)
				{
					ColEStringClass tempe = (ColEStringClass)cl.e[jcnt];
					ret[jcnt] = tempe.value;
				}
			}
			return ret;
		}
		private void InitializeSchema()
		{
			if(!m_schemainitialized)
			{
				try
				{
					m_schemarl = new RowLClass();
					System.Data.OleDb.OleDbConnection db = null;
					db = new System.Data.OleDb.OleDbConnection(connectionstr);
					db.Open();
					System.Data.DataTable t = db.GetSchema("columns");
					System.Data.DataRow[] rl = t.Select();
					for(int icnt = 0; icnt < rl.Length; icnt++)
					{
						ColLClass coll = new ColLClass();
						coll += new ColEStringClass(coll.maxseq + 1, "TableName", rl[icnt]["TABLE_NAME"].ToString());
						coll += new ColEStringClass(coll.maxseq + 1, "ColumnName", rl[icnt]["COLUMN_NAME"].ToString());
						m_schemarl += new RowEClass(m_schemarl.maxseq + 1, coll);
					}
					db.Close();
					m_schemainitialized = true;
				}
				catch
				{
				}
			}
		}
	}
}
