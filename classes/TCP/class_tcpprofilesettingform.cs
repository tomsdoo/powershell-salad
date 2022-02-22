namespace TCP
{
	public class CommClientProfileSettingsForm : Form
	{
		ProfileEClass m_profile;
		public ProfileEClass profile
		{
			get
			{
				return m_profile;
			}
		}
		TextBox m_namebox;
		TextBox m_notebox;
		public CommClientProfileSettingsForm(ProfileEClass provided_profile)
		{
			m_profile = provided_profile;
			SuspendLayout();
			Initialize();
			ResumeLayout();
		}
		private void Initialize()
		{
			Text = "input name";
			//FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			ControlBox = false;
			
			m_notebox = new TextBox();
			m_notebox.Dock = System.Windows.Forms.DockStyle.Fill;
			m_notebox.Multiline = true;
			m_notebox.Text = m_profile.note;
			Controls.Add(m_notebox);
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
			okbutton.Click += new System.EventHandler(MyOK);
			AcceptButton = okbutton;
			Controls.Add(okbutton);

			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
			cancelbutton.Click += new System.EventHandler(MyCancel);
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			CancelButton = cancelbutton;
			Controls.Add(cancelbutton);
			
			m_namebox = new TextBox();
			m_namebox.Dock = System.Windows.Forms.DockStyle.Top;
			m_namebox.Text = m_profile.name;
			Controls.Add(m_namebox);
			
			int taborder = 0;
			m_namebox.TabIndex = taborder;
			taborder++;
			m_notebox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			Height = 25;
			Height += m_namebox.Height;
			Height += m_notebox.Height;
			Height += okbutton.Height;
			Height += cancelbutton.Height;
		}
		private void MyOK(object sender, System.EventArgs e)
		{
			m_profile = new ProfileEClass(int.MinValue, m_namebox.Text, m_notebox.Text);
			Close();
		}
		private void MyCancel(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
