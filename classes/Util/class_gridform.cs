namespace Util
{
	public class TableGridFormClass : Form
	{
		protected System.Windows.Forms.DataGridView m_grid;
		protected ACC.RowLClass m_rl;
		public ACC.RowLClass rl
		{
			get
			{
				return m_rl;
			}
		}
		protected string[] m_differentcolumns;
		public TableGridFormClass(string provided_title, ACC.RowLClass provided_rl)
		{
			m_differentcolumns = null;
			m_rl = provided_rl;
			Initialize();
			Text = provided_title;
		}
		public TableGridFormClass(string provided_title, ACC.RowLClass provided_rl, string[] provided_differentcolumns)
		{
			m_differentcolumns = provided_differentcolumns;
			m_rl = provided_rl;
			Initialize();
			Text = provided_title;
		}
		protected void Initialize()
		{
			m_grid = new System.Windows.Forms.DataGridView();
			m_grid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.PaleGoldenrod;
			m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
			if(null != m_rl.table)
			{
				m_grid.DataSource = m_rl.table;
			}
			m_grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(MyH);
			Controls.Add(m_grid);
			InitializeMenu();
		}
		protected virtual void InitializeMenu()
		{
			// nop
		}
		protected void MyH(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
		{
			ReflectColor();
		}
		protected void ReflectColor()
		{
			if(null != m_differentcolumns)
			{
				for(int icnt = 0; icnt < m_differentcolumns.Length; icnt++)
				{
					string colname = m_differentcolumns[icnt];
					int idx = FindColIndex(colname);
					if(-1 != idx)
					{
						for(int jcnt = 0; jcnt < m_grid.Rows.Count; jcnt++)
						{
							m_grid[idx, jcnt].Style.ForeColor = System.Drawing.Color.Red;
						}
					}
				}
			}
		}
		protected int FindColIndex(string provided_colname)
		{
			for(int icnt = 0; icnt < m_grid.Columns.Count; icnt++)
			{
				if(provided_colname.ToUpper() == m_grid.Columns[icnt].Name.ToUpper())
				{
					return icnt;
				}
			}
			return -1;
		}
	}
	public class RowGridFormClass : Form
	{
		protected System.Windows.Forms.DataGridView m_grid;
		protected ACC.RowEClass m_re;
		public ACC.RowEClass re
		{
			get
			{
				return m_re;
			}
		}
		public RowGridFormClass(string provided_title, ACC.RowEClass provided_re)
		{
			m_re = provided_re;
			Initialize();
			Text = provided_title;
		}
		protected void Initialize()
		{
			m_grid = new System.Windows.Forms.DataGridView();
			m_grid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.PaleGoldenrod;
			m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
			if(null != m_re.table)
			{
				m_grid.DataSource = m_re.table;
			}
			Controls.Add(m_grid);
		}
	}
}
