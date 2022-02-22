namespace Util
{
	public delegate void LBSelectedEventHandler(int provided_selectedindex);
	public class LBBaseClass : System.Windows.Forms.ListBox
	{
		public event LBSelectedEventHandler LBSelected;
		public LBBaseClass()
		{
			Dock = System.Windows.Forms.DockStyle.Fill;
			DoubleClick += new System.EventHandler(MyDoubleClick);
			KeyDown += new System.Windows.Forms.KeyEventHandler(MyEnter);
			if(GetType().Name == typeof(LBBaseClass).Name)
			{
				Initialize();
			}
		}
		protected virtual void Initialize(){}
		protected virtual void MyDoubleClick(object sender, System.EventArgs e)
		{
			MySelected();
		}
		protected virtual void MyEnter(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				MySelected();
			}
		}
		private void MySelected()
		{
			if(null != LBSelected)
			{
				LBSelected(SelectedIndex);
			}
		}
	}
	public class LBFormBaseClass : FormBaseClass
	{
		public event LBSelectedEventHandler LBSelected;
		protected LBBaseClass m_lb;
		public LBBaseClass lb
		{
			get
			{
				return m_lb;
			}
		}
		public LBFormBaseClass()
		{
			if(GetType().Name == typeof(LBFormBaseClass).Name)
			{
				Initialize(new LBBaseClass());
			}
		}
		protected virtual void Initialize(LBBaseClass provided_lb)
		{
			SuspendLayout();
			m_lb = provided_lb;
			m_lb.LBSelected += new LBSelectedEventHandler(LBSelectedEvent);
			Controls.Add(m_lb);
			Controls.SetChildIndex(m_lb, 0);
			ResumeLayout();
		}
		protected override void InitializeMenu(){}
		protected virtual void LBSelectedEvent(int provided_index)
		{
			if(null != LBSelected)
			{
				LBSelected(provided_index);
			}
		}
	}
}
