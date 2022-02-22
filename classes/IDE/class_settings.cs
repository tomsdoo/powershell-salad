namespace IDE
{
	public class mysettings
	{
		System.Drawing.Color m_forecolor;
		public System.Drawing.Color forecolor
		{
			get
			{
				return m_forecolor;
			}
		}
		System.Drawing.Color m_backcolor;
		public System.Drawing.Color backcolor
		{
			get
			{
				return m_backcolor;
			}
		}
		System.Drawing.Color m_commentcolor;
		public System.Drawing.Color commentcolor
		{
			get
			{
				return m_commentcolor;
			}
		}
		System.Drawing.Color m_variablecolor;
		public System.Drawing.Color variablecolor
		{
			get
			{
				return m_variablecolor;
			}
		}
		System.Drawing.Color m_classcolor;
		public System.Drawing.Color classcolor
		{
			get
			{
				return m_classcolor;
			}
		}
		System.Drawing.Color m_literalcolor;
		public System.Drawing.Color literalcolor
		{
			get
			{
				return m_literalcolor;
			}
		}
		System.Drawing.Color m_functioncolor;
		public System.Drawing.Color functioncolor
		{
			get
			{
				return m_functioncolor;
			}
		}
		System.Drawing.Color m_cmdletcolor;
		public System.Drawing.Color cmdletcolor
		{
			get
			{
				return m_cmdletcolor;
			}
		}
		System.Drawing.Color m_foundbackcolor;
		public System.Drawing.Color foundbackcolor
		{
			get
			{
				return m_foundbackcolor;
			}
		}
		System.Drawing.Font m_myfont;
		public System.Drawing.Font myfont
		{
			get
			{
				return m_myfont;
			}
		}
		System.Text.Encoding m_enc;
		public System.Text.Encoding enc
		{
			get
			{
				return m_enc;
			}
		}
		public mysettings(System.Drawing.Font provided_font)
		{
			m_myfont = provided_font;
			m_forecolor = System.Drawing.Color.Black;
			m_backcolor = System.Drawing.Color.LightGray;
			m_commentcolor = System.Drawing.Color.Green;
			m_variablecolor = System.Drawing.Color.Purple;
			m_classcolor = System.Drawing.Color.Blue;
			m_literalcolor = System.Drawing.Color.Red;
			m_functioncolor = System.Drawing.Color.BlueViolet;
			m_cmdletcolor = System.Drawing.Color.Indigo;
			m_foundbackcolor = System.Drawing.Color.DarkKhaki;
			m_enc = System.Text.Encoding.GetEncoding(System.Text.Encoding.Default.CodePage);
		}
		public mysettings(	System.Drawing.Font provided_myfont,
							System.Drawing.Color provided_forecolor,
							System.Drawing.Color provided_backcolor,
							System.Drawing.Color provided_commentcolor,
							System.Drawing.Color provided_variablecolor,
							System.Drawing.Color provided_classcolor,
							System.Drawing.Color provided_literalcolor,
							System.Drawing.Color provided_functioncolor,
							System.Drawing.Color provided_cmdletcolor,
							System.Drawing.Color provided_foundbackcolor,
							System.Text.Encoding provided_enc)
		{
			m_myfont = provided_myfont;
			m_forecolor = provided_forecolor;
			m_backcolor = provided_backcolor;
			m_commentcolor = provided_commentcolor;
			m_variablecolor = provided_variablecolor;
			m_classcolor = provided_classcolor;
			m_literalcolor = provided_literalcolor;
			m_functioncolor = provided_functioncolor;
			m_cmdletcolor = provided_cmdletcolor;
			m_foundbackcolor = provided_foundbackcolor;
			m_enc = provided_enc;
		}
		public mysettings(mysettings provided_obj)
		{
			m_myfont = provided_obj.myfont;
			m_forecolor = provided_obj.forecolor;
			m_backcolor = provided_obj.backcolor;
			m_commentcolor = provided_obj.commentcolor;
			m_variablecolor = provided_obj.variablecolor;
			m_classcolor = provided_obj.classcolor;
			m_literalcolor = provided_obj.literalcolor;
			m_functioncolor = provided_obj.functioncolor;
			m_cmdletcolor = provided_obj.cmdletcolor;
			m_foundbackcolor = provided_obj.foundbackcolor;
			m_enc = provided_obj.enc;
		}
	}
	public class settingform : Form
	{
		public event mdimessageeventhandler messageevent;
		mysettings m_settings;
		public mysettings settings
		{
			get
			{
				return m_settings;
			}
		}
		TextBox m_editbox;
		TextBox m_commentbox;
		TextBox m_variablebox;
		TextBox m_classbox;
		TextBox m_literalbox;
		TextBox m_functionbox;
		TextBox m_cmdletbox;
		TextBox m_foundbox;
		TextBox m_encodebox;
		Button m_okbutton;
		Button m_cancelbutton;
		
		public settingform(mysettings provided_settings)
		{
			m_settings = provided_settings;
			initialize();
		}
		public settingform(settingform provided_obj)
		{
			m_settings = provided_obj.settings;
			initialize();
		}
		private void initialize()
		{
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.ControlBox = false;
			this.Text = "Click to edit";
			initbuttons();

			m_encodebox = new TextBox();
			m_encodebox.Text = m_settings.enc.WebName;
			m_encodebox.ReadOnly = true;
			m_encodebox.Dock = System.Windows.Forms.DockStyle.Top;
			m_encodebox.Click += new System.EventHandler(encodeedit);
			this.Controls.Add(m_encodebox);

			m_foundbox = new TextBox();
			m_foundbox.Text = "FoundText";
			m_foundbox.ReadOnly = true;
			m_foundbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_foundbox.Click += new System.EventHandler(foundedit);
			this.Controls.Add(m_foundbox);

			m_cmdletbox = new TextBox();
			m_cmdletbox.Text = "Commandlet";
			m_cmdletbox.ReadOnly = true;
			m_cmdletbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_cmdletbox.Click += new System.EventHandler(cmdletedit);
			this.Controls.Add(m_cmdletbox);

			m_functionbox = new TextBox();
			m_functionbox.Text = "Function";
			m_functionbox.ReadOnly = true;
			m_functionbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_functionbox.Click += new System.EventHandler(functionedit);
			this.Controls.Add(m_functionbox);

			m_literalbox = new TextBox();
			m_literalbox.Text = "\"Literal\"";
			m_literalbox.ReadOnly = true;
			m_literalbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_literalbox.Click += new System.EventHandler(literaledit);
			this.Controls.Add(m_literalbox);

			m_classbox = new TextBox();
			m_classbox.Text = "Class";
			m_classbox.ReadOnly = true;
			m_classbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_classbox.Click += new System.EventHandler(classedit);
			this.Controls.Add(m_classbox);

			m_variablebox = new TextBox();
			m_variablebox.Text = "$Variable";
			m_variablebox.ReadOnly = true;
			m_variablebox.Dock = System.Windows.Forms.DockStyle.Top;
			m_variablebox.Click += new System.EventHandler(variableedit);
			this.Controls.Add(m_variablebox);

			m_commentbox = new TextBox();
			m_commentbox.Text = "#Comment";
			m_commentbox.ReadOnly = true;
			m_commentbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_commentbox.Click += new System.EventHandler(commentedit);
			this.Controls.Add(m_commentbox);

			m_editbox = new TextBox();
			m_editbox.Text = "Edit";
			m_editbox.ReadOnly = true;
			m_editbox.Dock = System.Windows.Forms.DockStyle.Top;
			m_editbox.Click += new System.EventHandler(editedit);
			this.Controls.Add(m_editbox);

			reflectcolor();
			int taborder = 0;
			m_editbox.TabIndex = taborder;
			taborder++;
			m_commentbox.TabIndex = taborder;
			taborder++;
			m_variablebox.TabIndex = taborder;
			taborder++;
			m_classbox.TabIndex = taborder;
			taborder++;
			m_literalbox.TabIndex = taborder;
			taborder++;
			m_functionbox.TabIndex = taborder;
			taborder++;
			m_cmdletbox.TabIndex = taborder;
			taborder++;
			m_foundbox.TabIndex = taborder;
			taborder++;
			m_encodebox.TabIndex = taborder;
			taborder++;
			m_okbutton.TabIndex = taborder;
			taborder++;
			m_cancelbutton.TabIndex = taborder;
		}
		private void initbuttons()
		{
			m_cancelbutton = new Button();
			m_cancelbutton.Text = "Cancel";
			m_cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			m_cancelbutton.Click += new System.EventHandler(mycancel);
			m_cancelbutton.Dock = System.Windows.Forms.DockStyle.Top;
			this.Controls.Add(m_cancelbutton);
			this.CancelButton = m_cancelbutton;

			m_okbutton = new Button();
			m_okbutton.Text = "OK";
			m_okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			m_okbutton.Click += new System.EventHandler(myok);
			m_okbutton.Dock = System.Windows.Forms.DockStyle.Top;
			this.Controls.Add(m_okbutton);
			this.AcceptButton = m_okbutton;
		}
		private void reflectheight()
		{
			this.Height = 25;
			this.Height += m_encodebox.Height;
			this.Height += m_foundbox.Height;
			this.Height += m_cmdletbox.Height;
			this.Height += m_functionbox.Height;
			this.Height += m_literalbox.Height;
			this.Height += m_classbox.Height;
			this.Height += m_variablebox.Height;
			this.Height += m_commentbox.Height;
			this.Height += m_editbox.Height;
			this.Height += m_cancelbutton.Height;
			this.Height += m_okbutton.Height;
		}
		private void myok(object sender, System.EventArgs e)
		{
			messageout("settings changed");
			Close();
		}
		private void mycancel(object sender, System.EventArgs e)
		{
			Close();
		}
		private void reflectcolor()
		{
			reflecteditcolor();
			reflectcommentcolor();
			reflectvariablecolor();
			reflectclasscolor();
			reflectliteralcolor();
			reflectfunctioncolor();
			reflectcmdletcolor();
			reflectfoundcolor();
			reflectencoding();
			reflectheight();
		}
		private void editedit(object sender, System.EventArgs e)
		{
			System.Windows.Forms.FontDialog fd = new System.Windows.Forms.FontDialog();
			fd.Color = m_settings.forecolor;
			fd.MaxSize = 24;
			fd.MinSize = 10;
			fd.FontMustExist = true;
			fd.ShowColor = true;
			fd.ShowEffects = true;
			if(fd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
			{
				m_settings = new mysettings(	fd.Font,
												fd.Color,
												m_settings.backcolor,
												m_settings.commentcolor,
												m_settings.variablecolor,
												m_settings.classcolor,
												m_settings.literalcolor,
												m_settings.functioncolor,
												m_settings.cmdletcolor,
												m_settings.foundbackcolor,
												m_settings.enc);
				reflectcolor();
			}
			fd.Dispose();
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			cd.Color = m_settings.backcolor;
			if(cd.ShowDialog() == DialogResult.OK)
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												cd.Color,
												m_settings.commentcolor,
												m_settings.variablecolor,
												m_settings.classcolor,
												m_settings.literalcolor,
												m_settings.functioncolor,
												m_settings.cmdletcolor,
												m_settings.foundbackcolor,
												m_settings.enc);
				reflectcolor();
			}
			cd.Dispose();
		}
		private void commentedit(object sender, System.EventArgs e)
		{
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			cd.Color = m_settings.commentcolor;
			if(cd.ShowDialog() == DialogResult.OK)
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												m_settings.backcolor,
												cd.Color,
												m_settings.variablecolor,
												m_settings.classcolor,
												m_settings.literalcolor,
												m_settings.functioncolor,
												m_settings.cmdletcolor,
												m_settings.foundbackcolor,
												m_settings.enc);
				reflectcolor();
			}
			cd.Dispose();
		}
		private void variableedit(object sender, System.EventArgs e)
		{
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			cd.Color = m_settings.variablecolor;
			if(cd.ShowDialog() == DialogResult.OK)
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												m_settings.backcolor,
												m_settings.commentcolor,
												cd.Color,
												m_settings.classcolor,
												m_settings.literalcolor,
												m_settings.functioncolor,
												m_settings.cmdletcolor,
												m_settings.foundbackcolor,
												m_settings.enc);
				reflectcolor();
			}
			cd.Dispose();
		}
		private void classedit(object sender, System.EventArgs e)
		{
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			cd.Color = m_settings.classcolor;
			if(cd.ShowDialog() == DialogResult.OK)
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												m_settings.backcolor,
												m_settings.commentcolor,
												m_settings.variablecolor,
												cd.Color,
												m_settings.literalcolor,
												m_settings.functioncolor,
												m_settings.cmdletcolor,
												m_settings.foundbackcolor,
												m_settings.enc);
				reflectcolor();
			}
			cd.Dispose();
		}
		private void literaledit(object sender, System.EventArgs e)
		{
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			cd.Color = m_settings.literalcolor;
			if(cd.ShowDialog() == DialogResult.OK)
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												m_settings.backcolor,
												m_settings.commentcolor,
												m_settings.variablecolor,
												m_settings.classcolor,
												cd.Color,
												m_settings.functioncolor,
												m_settings.cmdletcolor,
												m_settings.foundbackcolor,
												m_settings.enc);
				reflectcolor();
			}
			cd.Dispose();
		}
		private void functionedit(object sender, System.EventArgs e)
		{
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			cd.Color = m_settings.functioncolor;
			if(cd.ShowDialog() == DialogResult.OK)
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												m_settings.backcolor,
												m_settings.commentcolor,
												m_settings.variablecolor,
												m_settings.classcolor,
												m_settings.literalcolor,
												cd.Color,
												m_settings.cmdletcolor,
												m_settings.foundbackcolor,
												m_settings.enc);
				reflectcolor();
			}
			cd.Dispose();
		}
		private void cmdletedit(object sender, System.EventArgs e)
		{
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			cd.Color = m_settings.cmdletcolor;
			if(cd.ShowDialog() == DialogResult.OK)
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												m_settings.backcolor,
												m_settings.commentcolor,
												m_settings.variablecolor,
												m_settings.classcolor,
												m_settings.literalcolor,
												m_settings.functioncolor,
												cd.Color,
												m_settings.foundbackcolor,
												m_settings.enc);
				reflectcolor();
			}
			cd.Dispose();
		}
		private void encodeedit(object sender, System.EventArgs e)
		{
			encodingform ef = new encodingform(m_settings.enc);
			if(System.Windows.Forms.DialogResult.OK == ef.ShowDialog())
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												m_settings.backcolor,
												m_settings.commentcolor,
												m_settings.variablecolor,
												m_settings.classcolor,
												m_settings.literalcolor,
												m_settings.functioncolor,
												m_settings.cmdletcolor,
												m_settings.foundbackcolor,
												ef.enc);
				reflectcolor();
			}
			ef.Dispose();
		}
		private void foundedit(object sender, System.EventArgs e)
		{
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			cd.Color = m_settings.foundbackcolor;
			if(cd.ShowDialog() == DialogResult.OK)
			{
				m_settings = new mysettings(	m_settings.myfont,
												m_settings.forecolor,
												m_settings.backcolor,
												m_settings.commentcolor,
												m_settings.variablecolor,
												m_settings.classcolor,
												m_settings.literalcolor,
												m_settings.functioncolor,
												m_settings.cmdletcolor,
												cd.Color,
												m_settings.enc);
				reflectcolor();
			}
			cd.Dispose();
		}
		private void reflecteditcolor()
		{
			m_editbox.Font = m_settings.myfont;
			m_editbox.ForeColor = m_settings.forecolor;
			m_editbox.BackColor = m_settings.backcolor;
		}
		private void reflectcommentcolor()
		{
			m_commentbox.Font = m_settings.myfont;
			m_commentbox.ForeColor = m_settings.commentcolor;
			m_commentbox.BackColor = m_settings.backcolor;
		}
		private void reflectvariablecolor()
		{
			m_variablebox.Font = m_settings.myfont;
			m_variablebox.ForeColor = m_settings.variablecolor;
			m_variablebox.BackColor = m_settings.backcolor;
		}
		private void reflectclasscolor()
		{
			m_classbox.Font = m_settings.myfont;
			m_classbox.ForeColor = m_settings.classcolor;
			m_classbox.BackColor = m_settings.backcolor;
		}
		private void reflectliteralcolor()
		{
			m_literalbox.Font = m_settings.myfont;
			m_literalbox.ForeColor = m_settings.literalcolor;
			m_literalbox.BackColor = m_settings.backcolor;
		}
		private void reflectfunctioncolor()
		{
			m_functionbox.Font = m_settings.myfont;
			m_functionbox.ForeColor = m_settings.functioncolor;
			m_functionbox.BackColor = m_settings.backcolor;
		}
		private void reflectcmdletcolor()
		{
			m_cmdletbox.Font = m_settings.myfont;
			m_cmdletbox.ForeColor = m_settings.cmdletcolor;
			m_cmdletbox.BackColor = m_settings.backcolor;
		}
		private void reflectfoundcolor()
		{
			m_foundbox.Font = m_settings.myfont;
			m_foundbox.ForeColor = m_settings.forecolor;
			m_foundbox.BackColor = m_settings.foundbackcolor;
		}
		private void reflectencoding()
		{
			m_encodebox.Font = m_settings.myfont;
			m_encodebox.ForeColor = m_settings.forecolor;
			m_encodebox.BackColor = m_settings.backcolor;
			m_encodebox.Text = m_settings.enc.WebName;
		}
		private void messageout(string provided_message)
		{
			if(null != messageevent)
			{
				messageevent("Settings", provided_message);
			}
		}
	}
	public class encodingform : Form
	{
		System.Text.Encoding m_enc;
		public System.Text.Encoding enc
		{
			get
			{
				return m_enc;
			}
		}
		System.Text.EncodingInfo[] m_el;
		string[] liststr
		{
			get
			{
				string[] ret = null;
				if(null != m_el)
				{
					ret = new string[m_el.Length];
					for(int icnt = 0; icnt < m_el.Length; icnt++)
					{
						ret[icnt] = m_el[icnt].CodePage.ToString() + "\t" + m_el[icnt].Name;
					}
				}
				return ret;
			}
		}
		ListBox m_lb;
		public encodingform(System.Text.Encoding provided_enc)
		{
			m_enc = provided_enc;
			m_el = System.Text.Encoding.GetEncodings();
			initialize();
		}
		private void initialize()
		{
			this.Text = "choose an encoding";
			this.ControlBox = false;
			this.Load += new System.EventHandler(onload);
			this.SuspendLayout();
			
			m_lb = new ListBox();
			m_lb.DataSource = liststr;
			m_lb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Controls.Add(m_lb);
			
			Button okbutton = new Button();
			okbutton.Text = "OK";
			okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
			okbutton.Click += new System.EventHandler(myok);
			this.AcceptButton = okbutton;
			this.Controls.Add(okbutton);
			
			Button cancelbutton = new Button();
			cancelbutton.Text = "Cancel";
			cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
			cancelbutton.Click += new System.EventHandler(mycancel);
			this.CancelButton = cancelbutton;
			this.Controls.Add(cancelbutton);
			
			int taborder = 0;
			m_lb.TabIndex = taborder;
			taborder++;
			okbutton.TabIndex = taborder;
			taborder++;
			cancelbutton.TabIndex = taborder;
			this.ResumeLayout();
		}
		private void onload(object sender, System.EventArgs e)
		{
			selectnowaenc();
		}
		private void selectnowaenc()
		{
			for(int icnt = 0; icnt < m_el.Length; icnt++)
			{
				if(m_enc.CodePage == m_el[icnt].CodePage)
				{
					m_lb.SelectedIndex = icnt;
				}
			}
		}
		private void myok(object sender, System.EventArgs e)
		{
			if(-1 != m_lb.SelectedIndex)
			{
				m_enc = System.Text.Encoding.GetEncoding(m_el[m_lb.SelectedIndex].CodePage);
			}
			Close();
		}
		private void mycancel(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
