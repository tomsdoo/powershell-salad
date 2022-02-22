namespace Util
{
	public class InputForm : Form
	{
		protected string m_prompt;
		protected string m_filename;
		public string filename
		{
			get
			{
				return m_filename;
			}
		}
		protected TextBox m_filebox;
		public InputForm(string provided_prompt)
		{
			m_prompt = provided_prompt;
			Initialize();
		}
		private void Initialize()
		{
			SuspendLayout();
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			ControlBox = false;
			Text = m_prompt;
			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			CancelButton = cancelbutton;
			cancelbutton.Click += new System.EventHandler(MyCancel);
			Controls.Add(cancelbutton);
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			AcceptButton = okbutton;
			okbutton.Click += new System.EventHandler(MyOK);
			Controls.Add(okbutton);
			
			m_filebox = new TextBox();
			m_filebox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_filebox);
			
			Height = 25;
			Height += m_filebox.Height;
			Height += okbutton.Height;
			Height += cancelbutton.Height;
			
			int taborder = 0;
			m_filebox.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			
			ResumeLayout();
		}
		private void MyOK(object sender, System.EventArgs e)
		{
			m_filename = m_filebox.Text;
			Close();
		}
		private void MyCancel(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
