/*using System;
using System. Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace Subachup
{
	/// <summary>
	/// Summary description for GatherTab.
	/// </summary>
    public class GatherTab : SubachupTabControl
	{

		private UtteranceCollection _utterances;
		private MyControls.AudioButton btnPlay;

		private System.Windows.Forms.LinkLabel btnCheckAllVisible;
		private System.Windows.Forms.LinkLabel btnClearAllVisible;
		private System.Windows.Forms.LinkLabel btnClearAll;
	
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuOpenImageWith;
		private System.Windows.Forms.MenuItem mnuOpenSoundWith;
		private DevExpress.XtraGrid.GridControl _grid;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private System.Windows.Forms.MenuItem mnuEditGloss;
		private System.Windows.Forms.MenuItem menuItem2;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GatherTab()
		{
			_utterances = new UtteranceCollection();

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

        public GatherTab(PropertyTable propertyTable)
            :base(propertyTable)
        {
			_utterances = new UtteranceCollection();
            InitializeComponent();
        }


		public nBASS.BASS Player
		{
			set { _player = value;}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.mnuOpenSoundWith = new System.Windows.Forms.MenuItem();
			this.menuOpenImageWith = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuEditGloss = new System.Windows.Forms.MenuItem();
			this.btnPlay = new MyControls.AudioButton();
			this.btnCheckAllVisible = new System.Windows.Forms.LinkLabel();
			this.btnClearAllVisible = new System.Windows.Forms.LinkLabel();
			this.btnClearAll = new System.Windows.Forms.LinkLabel();
			this._grid = new DevExpress.XtraGrid.GridControl();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
			this.SuspendLayout();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuOpenSoundWith,
																						 this.menuOpenImageWith,
																						 this.menuItem2,
																						 this.mnuEditGloss});
			// 
			// mnuOpenSoundWith
			// 
			this.mnuOpenSoundWith.Index = 0;
			this.mnuOpenSoundWith.Text = "&Open Sound WIth...";
			this.mnuOpenSoundWith.Click += new System.EventHandler(this.mnuOpenSoundWith_Click);
			// 
			// menuOpenImageWith
			// 
			this.menuOpenImageWith.Index = 1;
			this.menuOpenImageWith.Text = "Open &Image WIth...";
			this.menuOpenImageWith.Click += new System.EventHandler(this.menuOpenImageWith_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "-";
			// 
			// mnuEditGloss
			// 
			this.mnuEditGloss.Index = 3;
			this.mnuEditGloss.Text = "Edit &Gloss...";
			this.mnuEditGloss.Click += new System.EventHandler(this.mnuEditGloss_Click);
			// 
			// btnPlay
			// 
			this.btnPlay.ButtonType = MyControls.AudioButtonType.Play;
			this.btnPlay.IconLocation = new System.Drawing.Point(8, 8);
			this.btnPlay.IconSize = new System.Drawing.Size(14, 14);
			this.btnPlay.Location = new System.Drawing.Point(8, 9);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(32, 32);
			this.btnPlay.TabIndex = 16;
			this.btnPlay.Text = "btnPlay";
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// btnCheckAllVisible
			// 
			this.btnCheckAllVisible.Location = new System.Drawing.Point(66, 12);
			this.btnCheckAllVisible.Name = "btnCheckAllVisible";
			this.btnCheckAllVisible.Size = new System.Drawing.Size(95, 23);
			this.btnCheckAllVisible.TabIndex = 17;
			this.btnCheckAllVisible.TabStop = true;
			this.btnCheckAllVisible.Text = "Check All Visible";
			this.btnCheckAllVisible.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCheckAllVisible_LinkClicked);
			// 
			// btnClearAllVisible
			// 
			this.btnClearAllVisible.Location = new System.Drawing.Point(190, 12);
			this.btnClearAllVisible.Name = "btnClearAllVisible";
			this.btnClearAllVisible.Size = new System.Drawing.Size(95, 23);
			this.btnClearAllVisible.TabIndex = 18;
			this.btnClearAllVisible.TabStop = true;
			this.btnClearAllVisible.Text = "Clear All Visible";
			this.btnClearAllVisible.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClearAllVisible_LinkClicked);
			// 
			// btnClearAll
			// 
			this.btnClearAll.Location = new System.Drawing.Point(317, 12);
			this.btnClearAll.Name = "btnClearAll";
			this.btnClearAll.Size = new System.Drawing.Size(95, 23);
			this.btnClearAll.TabIndex = 19;
			this.btnClearAll.TabStop = true;
			this.btnClearAll.Text = "Clear All";
			this.btnClearAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClearAll_LinkClicked);
			// 
			// _grid
			// 
			this._grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._grid.ContextMenu = this.contextMenu1;
			// 
			// _grid.EmbeddedNavigator
			// 
			this._grid.EmbeddedNavigator.Buttons.Append.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.Edit.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.First.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.Last.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.Next.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.NextPage.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.Prev.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
			this._grid.EmbeddedNavigator.Buttons.Remove.Visible = false;
			this._grid.EmbeddedNavigator.Name = "";
			this._grid.Location = new System.Drawing.Point(1, 54);
			this._grid.MainView = this.gridView2;
			this._grid.Name = "_grid";
			this._grid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
																										   this.repositoryItemPictureEdit1});
			this._grid.Size = new System.Drawing.Size(560, 530);
			this._grid.TabIndex = 20;
			this._grid.UseEmbeddedNavigator = true;
			this._grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
																								 this.gridView2});
			this._grid.Load += new System.EventHandler(this._grid_Load);
			// 
			// gridView2
			// 
			this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
																							 this.gridColumn1});
			this.gridView2.GridControl = this._grid;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsBehavior.AllowIncrementalSearch = true;
			this.gridView2.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gridView2_BeforeLeaveRow);
			this.gridView2.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "gridColumn1";
			this.gridColumn1.ColumnEdit = this.repositoryItemPictureEdit1;
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			// 
			// repositoryItemPictureEdit1
			// 
			this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
			this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
			// 
			// GatherTab
			// 
			this.Controls.Add(this._grid);
			this.Controls.Add(this.btnClearAll);
			this.Controls.Add(this.btnClearAllVisible);
			this.Controls.Add(this.btnCheckAllVisible);
			this.Controls.Add(this.btnPlay);
			this.Name = "GatherTab";
			this.Size = new System.Drawing.Size(571, 602);
			this.VisibleChanged += new System.EventHandler(this.GatherTab_VisibleChanged);
			this.Load += new System.EventHandler(this.GatherTab_Load);
			((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void GatherTab_Load(object sender, System.EventArgs e)
		{
			//			((TabControl) this.Parent.Parent ).SelectedIndexChanged +=new EventHandler(GatherTab_SelectedIndexChanged);

			//			string directory = @"C:\THAI";
			//			Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			//			_utterances = new UtteranceCollection();
			//			_utterances.LoadFromDirectory(directory, false);
			//			_gatherGrid.DataSource=_utterances;
			//			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}

		private void btnPlay_Click(object sender, System.EventArgs e)
		{
			//I wonder if there is a better way to get the utterance attached to the row
			Utterance utterance =GetUtteranceFromRow(View.FocusedRowHandle);
			utterance.Play(_player);
		}

		private Utterance GetUtteranceFromRow(int rowHandle)
		{
			return View.GetRow(rowHandle) as Utterance;
			//			object o = View.GetRowCellValue(rowHandle, View.Columns["Self"]);
			//			if(o != null && o != DBNull.Value) 
			//			{
			//				return (Utterance)o;
			//			}
			//			return null;
		}

		private GridView View
		{
			get
			{ 
				DevExpress.XtraGrid.Views.Grid.GridView view = ((DevExpress.XtraGrid.Views.Grid.GridView)_grid.DefaultView);
				return view;
			}
		}

		private void gridView1_DoubleClick(object sender, System.EventArgs e)
		{
			if(!View.IsGroupRow(View.FocusedRowHandle))
				btnPlay_Click(sender,e);
//			else
//			{
//				//				ArrayList chi = new ArrayList();
//				
//				UtteranceCollection.CurrentUtteranceSet.Clear();
//				getChildRows(View, View.FocusedRowHandle, UtteranceCollection.CurrentUtteranceSet);
//				//				foreach(int handle in childRows)
//				//				{
//				//					UtteranceCollection.CurrentUtteranceSet.Add(GetUtteranceFromRow(handle));
//				//				}
//			}
		}

		//Returns the child data rows for the given group row
		public void getChildRows(GridView view, int groupRowHandle, ArrayList childRows) 
		{
			if(!view.IsGroupRow(groupRowHandle)) return;
			//Get the number of immediate children
			int childCount = view.GetChildRowCount(groupRowHandle);
			for(int i=0; i<childCount; i++)
			{
				//Get the handle of a child row with the required index
				int childHandle = view.GetChildRowHandle(groupRowHandle, i);
				//If the child is a group row, then add its children to the list
				if(view.IsGroupRow(childHandle))
					getChildRows(view, childHandle, childRows);
				else 
				{                    
					//The child is a data row. Add it to the childRows as long as the row wasn't added before
					object row = view.GetRow(childHandle);
					if(!childRows.Contains(row))
						childRows.Add(row);
				}       
			}
		}


		private void btnUseVisible_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
	
			//			UtteranceCollection.CurrentUtteranceSet.Clear();
			//			for(int i = View.GetNextVisibleRow(-1); i>-1 ;i=View.GetNextVisibleRow(i))
			//			{
			//				if(View.IsGroupRow(i))
			//					continue;
			//				Utterance utterance =  GetUtteranceFromRow(i);
			//				UtteranceCollection.CurrentUtteranceSet.Add(GetUtteranceFromRow(i));
			//			}

		}

		private void UpdateCurrentSetFromCheckboxes()
		{
			UtteranceCollection.CurrentUtteranceSet.Clear();
			foreach(Utterance utterance in _utterances)
			{
				if(utterance.Checked)
					UtteranceCollection.CurrentUtteranceSet.Add(utterance);
			}
            //todo: currently causes this object to reload, too, even though it is the one making the change.
            UtteranceCollection.CurrentUtteranceSet.DidChange();
		}



		private void btnCheckAllVisible_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			for(int i = View.GetNextVisibleRow(-1); i>-1 ;i=View.GetNextVisibleRow(i))
			{
				if(View.IsGroupRow(i))
					continue;
				GetUtteranceFromRow(i).Checked=true;
			}		
			_grid.RefreshDataSource();
		}

		private void btnClearAllVisible_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			for(int i = View.GetNextVisibleRow(-1); i>-1 ;i=View.GetNextVisibleRow(i))
			{
				if(View.IsGroupRow(i))
					continue;
				GetUtteranceFromRow(i).Checked=false;
			}			
			_grid.RefreshDataSource();
		}

		private void btnClearAll_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			foreach(Utterance utterance in _utterances)
			{
				utterance.Checked=false;
			}			
			_grid.RefreshDataSource();
		}


		/// <summary>
		/// set the checkboxes of each row to match whether it is in the current set or not
		/// </summary>
		private void UpdateGridFromCurrentSet()
		{		
			foreach(Utterance utterance in _utterances)
			{
				int i = UtteranceCollection.CurrentUtteranceSet.IndexOf(utterance);
				if(i>-1)
					utterance.Checked=true;
			}
		}

		private void GatherTab_VisibleChanged(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks> this was initially an event handler on the tab control.  But it would get called too late.</remarks>
		/// <param name="selectedTab"></param>
        //public void SelectedIndexChanged(TabPage selectedTab)
        //{
        //    if(selectedTab == this.Parent)
        //    {
        //        _isSelectedTab=true;
        //        UpdateGridFromCurrentSet();
        //        _grid.RefreshDataSource();			
        //    }
        //    else if(_isSelectedTab)
        //    {
        //        UpdateCurrentSetFromCheckboxes();
        //        _isSelectedTab = false;
        //    }

        //}

		private void _gatherGrid_DoubleClick(object sender, System.EventArgs e)
		{
		
		}

		public override bool Hiding()
		{
            UpdateCurrentSetFromCheckboxes();
			SaveLayout(LayoutFilePath);
            return base.Hiding();
		}

		protected string LayoutFilePath
		{
			get
			{
				string directory =	PropertyTable.Singleton.UserSettingDirectory;
				return System.IO.Path.Combine(directory,  this.GetType().Name+"_Layout.xml"); 
			}
		}

		protected void RestoreLayout (string path)
		{
			//			//try to just save it once per run
			//			if (!System.IO.File.Exists(LayoutFilePath))
			//			{
			//				SaveLayout(DefaultLayoutFilePath);
			//				//todo: delete old ones or when we quit, or 
			//				// in dispose, which will slow us down because
			//				//we'll be constantly saving everytime we enter the tool.
			//			}
			if(System.IO.File.Exists(path))
				_grid.DefaultView.RestoreLayoutFromXml(path);
		}

		protected void SaveLayout (string path)
		{
			DevExpress.Utils.OptionsLayoutGrid options= new DevExpress.Utils.OptionsLayoutGrid();
			options.LayoutVersion = Application.ProductVersion;
			options.StoreAllOptions = true;
			_grid.DefaultView.SaveLayoutToXml(path,options);			
		}

		private void menuOpenImageWith_Click(object sender, System.EventArgs e)
		{
			Utterance utterance = GetUtteranceFromRow(View.FocusedRowHandle);
			utterance.DoShellOpenImageWith();
		
		}

		private void mnuOpenSoundWith_Click(object sender, System.EventArgs e)
		{
			Utterance utterance = GetUtteranceFromRow(View.FocusedRowHandle);
			utterance.DoShellOpenSoundWith();
		
		}

		private void _grid_Load(object sender, System.EventArgs e)
		{
			string directory = @"C:\THAI";
			Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			_utterances = new UtteranceCollection();
			_utterances.LoadFromDirectory(directory, false);
			_utterances.LoadUserStuff();

			_grid.DataSource=_utterances;
			_grid.MainView.PopulateColumns();

			LoadDefaultColumnProperties();
			RestoreLayout(LayoutFilePath);
			LoadColumnConstraintsAndSettings();


            UpdateGridFromCurrentSet();
            _grid.RefreshDataSource();			


			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}

		/// <summary>
		/// this one is for before a layout has been restored, i.e., it can be overridden by user preferences
		/// </summary>
		private void LoadDefaultColumnProperties()
		{
			Utterance dummy= new Utterance();

			foreach(DevExpress.XtraGrid.Columns.GridColumn c in ((GridView)_grid.DefaultView).Columns)
			{
				c.Visible = false;
				object[] attrs = dummy.GetType().GetProperty(c.FieldName).GetCustomAttributes(true);//(typeof(MRUAttribute), true);
				foreach(object attribute in attrs)
				{
					switch (attribute.GetType().Name)
					{
						default: break;
						case "DefaultVisibleAttribute":
							//should only have an effective there is no saved layout being loaded later
							c.Visible = true;
							int i =((DefaultVisibleAttribute)attribute)._order;
							if(i>-1)
								c.VisibleIndex = i;
							break;
					}
				}
			}
		}

		/// <summary>
		/// this one is for after a layout has been restored
		/// </summary>
		private void LoadColumnConstraintsAndSettings()
		{
			Utterance dummy= new Utterance();
			
			//for images
			((GridView)_grid.DefaultView).OptionsView.RowAutoHeight = true;

			foreach(GridColumn c in ((GridView)_grid.DefaultView).Columns)
			{
				c.OptionsColumn.ReadOnly = false;
				c.OptionsColumn.AllowEdit  = true;

				object[] attrs = dummy.GetType().GetProperty(c.FieldName).GetCustomAttributes(true);//(typeof(MRUAttribute), true);
				foreach(object attribute in attrs)
				{
					switch (attribute.GetType().Name)
					{
						default: break;
						case "NeverVisibleAttribute":
							c.OptionsColumn.ShowInCustomizationForm = false;
							break;
						case "NormallyReadOnlyAttribute":
							c.OptionsColumn.ReadOnly = true;
							c.OptionsColumn.AllowEdit  = false;
							c.OptionsColumn.AllowIncrementalSearch = true;
							break;
						case "MRUAttribute":
							DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit editor= new DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit();
							_grid.RepositoryItems.Add(editor);
							c.ColumnEdit = editor;
							break;
						case "ImageAttribute":
							
							DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit editor2= new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
							_grid.RepositoryItems.Add(editor2);
							editor2.AutoHeight = true;
							c.ColumnEdit = editor2;
							break;
					}
				}
			}
		}

		private void mnuEditGloss_Click(object sender, System.EventArgs e)
		{
			GridColumn c= ((GridView)_grid.DefaultView).Columns["Gloss"];
			c.OptionsColumn.ReadOnly = false;
			c.OptionsColumn.AllowEdit = true;
			
		}

		private void gridView2_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
		{
			GridColumn c= ((GridView)_grid.DefaultView).Columns["Gloss"];
			c.OptionsColumn.ReadOnly = true;
			c.OptionsColumn.AllowEdit = false;
		
		}

        
        public override void Reload()
        {
            UpdateGridFromCurrentSet();
            _grid.RefreshDataSource();
        }


	}
}
*/