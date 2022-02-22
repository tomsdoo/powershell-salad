namespace TCP
{
	public class CommClientSettingsForm : Form
	{
		string m_server;
		public string server
		{
			get
			{
				return m_server;
			}
		}
		int m_connectionport;
		public int connectionport
		{
			get
			{
				return m_connectionport;
			}
		}
		TextBox m_serverbox;
		TextBox m_portbox;
		public CommClientSettingsForm(string provided_server, int provided_connectionport)
		{
			m_server = provided_server;
			m_connectionport = provided_connectionport;
			SuspendLayout();
			Initialize();
			ResumeLayout();
		}
		private void Initialize()
		{
			Text = "input server name and port";
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			ControlBox = false;
			Button cancelbutton = new Button();
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelbutton.Text = "Cancel";
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
			
			m_portbox = new TextBox();
			m_portbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_portbox.Text = m_connectionport.ToString();
			Controls.Add(m_portbox);
			
			m_serverbox = new TextBox();
			m_serverbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_serverbox.Text = m_server;
			Controls.Add(m_serverbox);
			
			int taborder = 0;
			m_serverbox.TabIndex = taborder;
			taborder++;
			m_portbox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			Height = 25;
			Height += m_serverbox.Height;
			Height += m_portbox.Height;
			Height += okbutton.Height;
			Height += cancelbutton.Height;
		}
		private void MyOK(object sender, System.EventArgs e)
		{
			m_server = m_serverbox.Text;
			m_connectionport = int.Parse(m_portbox.Text);
			Close();
		}
		private void MyCancel(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
