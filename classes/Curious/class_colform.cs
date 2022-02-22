namespace Curious
{
	public class ColFormClass : Form
	{
		ACC.ColEBaseClass m_myobj;
		TextBox m_seqbox;
		TextBox m_namebox;
		TextBox m_valuebox;
		TextBox m_classbox;
		public ColFormClass(ACC.ColEBaseClass provided_obj)
		{
			m_myobj = provided_obj;
			Initialize();
		}
		private void Initialize()
		{
			m_classbox = new TextBox();
			m_classbox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_classbox);
			m_classbox.Text = m_myobj.classname;
			
			m_seqbox = new TextBox();
			m_seqbox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_seqbox);
			m_seqbox.Text = m_myobj.seq.ToString();

			m_namebox = new TextBox();
			m_namebox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_namebox);
			m_namebox.Text = m_myobj.name;

			m_valuebox = new TextBox();
			m_valuebox.Dock = System.Windows.Forms.DockStyle.Top;
			Controls.Add(m_valuebox);
			switch(m_myobj.classname)
			{
				case ACC.ColEIntClass.ClassName:
					{
						m_valuebox.Text = ((ACC.ColEIntClass)m_myobj).value.ToString();
						break;
					}
				case ACC.ColEStringClass.ClassName:
					{
						m_valuebox.Text = ((ACC.ColEStringClass)m_myobj).value;
						break;
					}
				default:
					{
						break;
					}
			}
		}
	}
}
