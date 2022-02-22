namespace Util
{
	public delegate void TVSelectedEventHandler(System.Windows.Forms.TreeNode provided_selected);
	public class TVBaseClass : System.Windows.Forms.TreeView
	{
		public event TVSelectedEventHandler TVSelected;
		public TVBaseClass()
		{
			Dock = System.Windows.Forms.DockStyle.Fill;
			DoubleClick += new System.EventHandler(MyDoubleClick);
			KeyDown += new System.Windows.Forms.KeyEventHandler(MyEnter);
			if(GetType().Name == typeof(TVBaseClass).Name)
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
			if(null != TVSelected)
			{
				TVSelected(SelectedNode);
			}
		}
	}
	public class TVFormBaseClass : FormBaseClass
	{
		public event TVSelectedEventHandler TVSelected;
		protected TVBaseClass m_tv;
		public TVBaseClass tv
		{
			get
			{
				return m_tv;
			}
		}
		public TVFormBaseClass()
		{
			if(GetType().Name == typeof(TVFormBaseClass).Name)
			{
				Initialize(new TVBaseClass());
			}
		}
		protected virtual void Initialize(TVBaseClass provided_tv)
		{
			SuspendLayout();
			m_tv = provided_tv;
			m_tv.TVSelected += new TVSelectedEventHandler(TVSelectedEvent);
			Controls.Add(m_tv);
			Controls.SetChildIndex(m_tv, 0);
			ResumeLayout();
		}
		protected override void InitializeMenu(){}
		protected virtual void TVSelectedEvent(System.Windows.Forms.TreeNode provided_selected)
		{
			if(null != TVSelected)
			{
				TVSelected(provided_selected);
			}
		}
	}
}
