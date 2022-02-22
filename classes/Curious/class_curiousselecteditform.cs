namespace Curious
{
	public class CuriousSelectEditFormClass : Form
	{
		protected int m_seq;
		protected CuriousSelectClass m_select;
		public CuriousSelectClass select
		{
			get
			{
				return m_select;
			}
		}
		protected TextBox m_filebox;
		protected TextBox m_sqlbox;
		protected TextBox m_secondbox;
		protected TextBox m_keycolumnbox;
		protected TextBox m_resultcaptioncolumnsbox;
		public CuriousSelectEditFormClass(int provided_seq)
		{
			m_seq = provided_seq;
			m_select = null;
			Initialize();
		}
		public CuriousSelectEditFormClass(CuriousSelectClass provided_select)
		{
			m_seq = provided_select.seq;
			m_select = provided_select;
			Initialize();
		}
		protected virtual void Initialize()
		{
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			Text = "input mdb filename, sql, interval seconds, key columns, caption columns";
			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelbutton.Click += new System.EventHandler(MyCancel);
			Controls.Add(cancelbutton);
			CancelButton = cancelbutton;
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okbutton.Click += new System.EventHandler(MyOK);
			Controls.Add(okbutton);
			AcceptButton = okbutton;
			
			m_resultcaptioncolumnsbox = new TextBox();
			m_resultcaptioncolumnsbox.Text = "input caption display column names separated with comma";
			m_resultcaptioncolumnsbox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_resultcaptioncolumnsbox);
			
			m_keycolumnbox = new TextBox();
			m_keycolumnbox.Text = "input key column names separated with comma";
			m_keycolumnbox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_keycolumnbox);
			
			m_secondbox = new TextBox();
			m_secondbox.Text = "10";
			m_secondbox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_secondbox);
			
			m_sqlbox = new TextBox();
			m_sqlbox.Text = "input sql";
			m_sqlbox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_sqlbox);
			
			m_filebox = new TextBox();
			m_filebox.Text = "input filename";
			m_filebox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_filebox);
			
			Height = 25;
			Height += m_filebox.Height;
			Height += m_sqlbox.Height;
			Height += m_secondbox.Height;
			Height += m_keycolumnbox.Height;
			Height += m_resultcaptioncolumnsbox.Height;
			Height += okbutton.Height;
			Height += cancelbutton.Height;
			
			int taborder = 0;
			m_filebox.TabIndex = taborder;
			taborder++;
			m_sqlbox.TabIndex = taborder;
			taborder++;
			m_secondbox.TabIndex = taborder;
			taborder++;
			m_keycolumnbox.TabIndex = taborder;
			taborder++;
			m_resultcaptioncolumnsbox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			if(null != m_select)
			{
				m_filebox.Text = m_select.filename;
				m_sqlbox.Text = m_select.sql;
				m_secondbox.Text = m_select.second.ToString();
				m_keycolumnbox.Text = m_select.keycolumnsstr;
				m_resultcaptioncolumnsbox.Text = m_select.resultcaptioncolumnsstr;
			}
		}
		protected virtual void MyOK(object sender, System.EventArgs e)
		{
			m_select = new CuriousSelectClass(m_seq, int.Parse(m_secondbox.Text), m_filebox.Text, m_sqlbox.Text, m_keycolumnbox.Text, m_resultcaptioncolumnsbox.Text);
			Close();
		}
		private void MyCancel(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
