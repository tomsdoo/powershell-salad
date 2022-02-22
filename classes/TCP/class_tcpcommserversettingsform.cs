namespace TCP
{
	public class CommServerSettingsForm : Form
	{
		int m_port;
		public int port
		{
			get
			{
				return m_port;
			}
		}
		int m_maxcount;
		public int maxcount
		{
			get
			{
				return m_maxcount;
			}
		}
		TextBox m_portbox;
		TextBox m_maxcountbox;
		public CommServerSettingsForm(int provided_port, int provided_maxcount)
		{
			m_port = provided_port;
			m_maxcount = provided_maxcount;
			SuspendLayout();
			Initialize();
			ResumeLayout();
		}
		private void Initialize()
		{
			Text = "input port, maxcount";
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			ControlBox = false;
			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			cancelbutton.Click += new System.EventHandler(MyCancel);
			CancelButton = cancelbutton;
			Controls.Add(cancelbutton);
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			okbutton.Click += new System.EventHandler(MyOK);
			AcceptButton = okbutton;
			Controls.Add(okbutton);
			
			m_maxcountbox = new TextBox();
			m_maxcountbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_maxcountbox.Text = m_maxcount.ToString();
			Controls.Add(m_maxcountbox);
			
			m_portbox = new TextBox();
			m_portbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_portbox.Text = m_port.ToString();
			Controls.Add(m_portbox);
			
			int taborder = 0;
			m_portbox.TabIndex = taborder;
			taborder++;
			m_maxcountbox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			Height = 25;
			Height += m_portbox.Height;
			Height += m_maxcountbox.Height;
			Height += okbutton.Height;
			Height += cancelbutton.Height;
		}
		private void MyOK(object sender, System.EventArgs e)
		{
			m_port = int.Parse(m_portbox.Text);
			m_maxcount = int.Parse(m_maxcountbox.Text);
			Close();
		}
		private void MyCancel(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
