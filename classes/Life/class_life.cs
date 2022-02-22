namespace Life
{
	public class CellAroundStatusClass
	{
		string m_upleftstatus;
		public string upleftstatus
		{
			get
			{
				return m_upleftstatus;
			}
		}
		string m_upstatus;
		public string upstatus
		{
			get
			{
				return m_upstatus;
			}
		}
		string m_uprightstatus;
		public string uprightstatus
		{
			get
			{
				return m_uprightstatus;
			}
		}
		string m_leftstatus;
		public string leftstatus
		{
			get
			{
				return m_leftstatus;
			}
		}
		string m_rightstatus;
		public string rightstatus
		{
			get
			{
				return m_rightstatus;
			}
		}
		string m_downleftstatus;
		public string downleftstatus
		{
			get
			{
				return m_downleftstatus;
			}
		}
		string m_downstatus;
		public string downstatus
		{
			get
			{
				return m_downstatus;
			}
		}
		string m_downrightstatus;
		public string downrightstatus
		{
			get
			{
				return m_downrightstatus;
			}
		}
		public int aroundalive
		{
			get
			{
				int ret = 0;
				ret += System.Convert.ToInt32(m_upleftstatus == CellEClass.Alive);
				ret += System.Convert.ToInt32(m_upstatus == CellEClass.Alive);
				ret += System.Convert.ToInt32(m_uprightstatus == CellEClass.Alive);
				ret += System.Convert.ToInt32(m_leftstatus == CellEClass.Alive);
				ret += System.Convert.ToInt32(m_rightstatus == CellEClass.Alive);
				ret += System.Convert.ToInt32(m_downleftstatus == CellEClass.Alive);
				ret += System.Convert.ToInt32(m_downstatus == CellEClass.Alive);
				ret += System.Convert.ToInt32(m_downrightstatus == CellEClass.Alive);
				return ret;
			}
		}
		public CellAroundStatusClass(	CellEClass provided_upleft,
										CellEClass provided_up,
										CellEClass provided_upright,
										CellEClass provided_left,
										CellEClass provided_right,
										CellEClass provided_downleft,
										CellEClass provided_down,
										CellEClass provided_downright)
		{
			m_upleftstatus = InterpretStatus(provided_upleft);
			m_upstatus = InterpretStatus(provided_up);
			m_uprightstatus = InterpretStatus(provided_upright);
			m_leftstatus = InterpretStatus(provided_left);
			m_rightstatus = InterpretStatus(provided_right);
			m_downleftstatus = InterpretStatus(provided_downleft);
			m_downstatus = InterpretStatus(provided_down);
			m_downrightstatus = InterpretStatus(provided_downright);
		}
		public CellAroundStatusClass()
		{
			m_upleftstatus = InterpretStatus(null);
			m_upstatus = InterpretStatus(null);
			m_uprightstatus = InterpretStatus(null);
			m_leftstatus = InterpretStatus(null);
			m_rightstatus = InterpretStatus(null);
			m_downleftstatus = InterpretStatus(null);
			m_downstatus = InterpretStatus(null);
			m_downrightstatus = InterpretStatus(null);
		}
		public CellAroundStatusClass(CellAroundStatusClass provided_obj)
		{
			m_upleftstatus = provided_obj.upleftstatus;
			m_upstatus = provided_obj.upstatus;
			m_uprightstatus = provided_obj.uprightstatus;
			m_leftstatus = provided_obj.leftstatus;
			m_rightstatus = provided_obj.rightstatus;
			m_downleftstatus = provided_obj.downleftstatus;
			m_downstatus = provided_obj.downstatus;
			m_downrightstatus = provided_obj.downrightstatus;
		}
		private string InterpretStatus(CellEClass provided_obj)
		{
			string ret = CellEClass.Dead;
			if(null != provided_obj)
			{
				ret = provided_obj.status;
			}
			return ret;
		}
	}
	public class CellEClass : AbstLib.EBaseClass
	{
		public const string Alive = "alive";
		public const string Dead = "dead";
		string m_status;
		public string status
		{
			get
			{
				return m_status;
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
		int m_column;
		public int column
		{
			get
			{
				return m_column;
			}
		}
		CellAroundStatusClass m_aroundstatus;
		public CellAroundStatusClass aroundstatus
		{
			get
			{
				return m_aroundstatus;
			}
		}
		public CellEClass(	int provided_seq,
							int provided_row,
							int provided_column,
							string provided_status,
							CellAroundStatusClass provided_aroundstatus)
		: base(provided_seq)
		{
			m_row = provided_row;
			m_column = provided_column;
			m_status = provided_status;
			m_aroundstatus = provided_aroundstatus;
		}
		public CellEClass(	int provided_seq,
							int provided_row,
							int provided_column,
							string provided_status)
		: base(provided_seq)
		{
			m_row = provided_row;
			m_column = provided_column;
			m_status = provided_status;
			m_aroundstatus = new CellAroundStatusClass();
		}
		public CellEClass(	int provided_seq,
							int provided_row,
							int provided_column)
		: base(provided_seq)
		{
			m_row = provided_row;
			m_column = provided_column;
			m_status = Dead;
			m_aroundstatus = new CellAroundStatusClass();
		}
		public CellEClass(CellEClass provided_obj) : base(provided_obj)
		{
			m_row = provided_obj.row;
			m_column = provided_obj.column;
			m_status = provided_obj.status;
			m_aroundstatus = provided_obj.aroundstatus;
		}
		public void Next()
		{
			int ascount = m_aroundstatus.aroundalive;
			if(m_status == Dead)
			{
				if(3 == ascount)
				{
					m_status = Alive;
				}
			}
			else
			{
				if(2 > ascount)
				{
					m_status = Dead;
				}
				if(3 < ascount)
				{
					m_status = Dead;
				}
			}
		}
		public void SetRandom(System.Random r)
		{
			m_status = Alive;
			if(1 == r.Next(2))
			{
				m_status = Dead;
			}
		}
		public void SetAround(CellAroundStatusClass provided_aroundstatus)
		{
			m_aroundstatus = provided_aroundstatus;
		}
	}
	public class CellLClass : AbstLib.LBaseClass
	{
		public CellEClass this[int provided_row, int provided_column]
		{
			get
			{
				if(null != m_e)
				{
					for(int icnt = 0; icnt < m_e.Length; icnt++)
					{
						CellEClass mycell = (CellEClass)(m_e[icnt]);
						if((mycell.row == provided_row) && (mycell.column == provided_column))
						{
							return mycell;
						}
					}
				}
				return null;
			}
		}
		public CellLClass() : base(){}
		public CellLClass(CellLClass provided_obj) : base(provided_obj){}
		public void SetRandom()
		{
			System.Random tempr = new System.Random((int)(DateTime.Now.ToBinary() % Int32.MaxValue));
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					CellEClass mycell = (CellEClass)(m_e[icnt]);
					mycell.SetRandom(tempr);
				}
			}
		}
		public void SetAround()
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					CellEClass mycell = (CellEClass)(m_e[icnt]);
					CellAroundStatusClass tempa = new CellAroundStatusClass(	this[mycell.row - 1, mycell.column - 1],
																				this[mycell.row - 1, mycell.column],
																				this[mycell.row - 1, mycell.column + 1],
																				this[mycell.row, mycell.column - 1],
																				this[mycell.row, mycell.column + 1],
																				this[mycell.row + 1, mycell.column - 1],
																				this[mycell.row + 1, mycell.column],
																				this[mycell.row + 1, mycell.column + 1]);
					((CellEClass)(m_e[icnt])).SetAround(tempa);
				}
			}
		}
		public void Next()
		{
			if(null != m_e)
			{
				for(int icnt = 0; icnt < m_e.Length; icnt++)
				{
					((CellEClass)(m_e[icnt])).Next();
				}
			}
		}
	}
	public class TableClass
	{
		int m_row;
		public int row
		{
			get
			{
				return m_row;
			}
		}
		int m_column;
		public int column
		{
			get
			{
				return m_column;
			}
		}
		CellLClass m_celll;
		public CellLClass celll
		{
			get
			{
				return m_celll;
			}
		}
		public TableClass(int provided_row, int provided_column)
		{
			m_row = provided_row;
			m_column = provided_column;
			m_celll = new CellLClass();
			for(int ri = 0; ri < m_row; ri++)
			{
				for(int ci = 0; ci < m_column; ci++)
				{
					CellEClass tempe = new CellEClass(m_celll.maxseq + 1, ri + 1, ci + 1);
					m_celll = (CellLClass)(m_celll + tempe);
				}
			}
		}
		public TableClass(CellLClass provided_celll)
		{
			m_row = 0;
			m_column = 0;
			m_celll = provided_celll;
			if(null != m_celll.e)
			{
				for(int icnt = 0; icnt < m_celll.e.Length; icnt++)
				{
					CellEClass myc = (CellEClass)(m_celll.e[icnt]);
					if(m_row < myc.row)
					{
						m_row = myc.row;
					}
					if(m_column < myc.column)
					{
						m_column = myc.column;
					}
				}
			}
		}
		public void SetRandom()
		{
			m_celll.SetRandom();
		}
		public void Display(string provided_deadchar, string provided_alivechar)
		{
			if(null != m_celll.e)
			{
				for(int ri = 0; ri < m_row; ri++)
				{
					string line = string.Empty;
					for(int ci = 0; ci < m_column; ci++)
					{
						CellEClass myc = (CellEClass)(m_celll[ri + 1, ci + 1]);
						string mystat = provided_deadchar;
						if(CellEClass.Alive == myc.status)
						{
							mystat = provided_alivechar;
						}
						line += mystat;
					}
					System.Console.WriteLine(line);
				}
			}
		}
		public void Next()
		{
			m_celll.SetAround();
			m_celll.Next();
		}
	}
}
