namespace Curious
{
	public class CuriousTableGridFormClass : Util.TableGridFormClass
	{
		public CuriousTableGridFormClass(string provided_title, ACC.RowLClass provided_rl) : base(provided_title, provided_rl){}
		public CuriousTableGridFormClass(string provided_title, ACC.RowLClass provided_rl, string[] provided_differentcolumns) : base(provided_title, provided_rl, provided_differentcolumns){}
		protected override void InitializeMenu()
		{
			MenuStrip mymenu = new MenuStrip();
			ContextMenuStrip cmenu = new ContextMenuStrip();
			ToolStripMenuItem itemmenu = new ToolStripMenuItem("&Item");
			itemmenu.DropDownItems.Add(CreateItemExportMenu(true));
			cmenu.Items.Add(CreateItemExportMenu(false));
			mymenu.Items.Add(itemmenu);
			MainMenuStrip = mymenu;
			mymenu.AllowMerge = true;
			Controls.Add(mymenu);
			ContextMenuStrip = cmenu;
			mymenu.Visible = false;
		}
		protected ToolStripMenuItem CreateItemExportMenu(bool provided_hasshortcut)
		{
			ToolStripMenuItem ret = new ToolStripMenuItem("&Export", null, new System.EventHandler(ItemExportMenu));
			if(provided_hasshortcut)
			{
				ret.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
				ret.ShowShortcutKeys = true;
			}
			return ret;
		}
		protected void ItemExportMenu(object sender, System.EventArgs e)
		{
			MessageBox.Show(DateTime.Now.ToString());
		}
	}
}
