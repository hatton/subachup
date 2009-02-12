using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Text;
using Subachup.Core;


namespace Subachup
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;

		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private MostRecentlyUsedHandler.MRUHandler mruHandler1;
		private System.Windows.Forms.MenuItem menuMRU;
		private System.Windows.Forms.MenuItem mnuChooseFolder;
        private LQ.StatusDisplay statusDisplay1;

        private System.Windows.Forms.TabControl _modeControl;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem mnuSave;
        private System.Windows.Forms.MenuItem mnuSaveAs;
        private MenuItem menuISave;
        private MenuItem menuSaveAs;

		protected PropertyTable _propertyTable;
        private MenuItem menuItem2;
        private MenuItem mnuPaste;

        protected TabPage  _previousPage;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_propertyTable = new PropertyTable();

		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuChooseFolder = new System.Windows.Forms.MenuItem();
            this.menuISave = new System.Windows.Forms.MenuItem();
            this.menuSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuMRU = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuPaste = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mnuSave = new System.Windows.Forms.MenuItem();
            this.mnuSaveAs = new System.Windows.Forms.MenuItem();
            this.mruHandler1 = new MostRecentlyUsedHandler.MRUHandler(this.components);
            this.statusDisplay1 = new LQ.StatusDisplay();
            this._modeControl = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.mruHandler1)).BeginInit();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "C:\\";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuChooseFolder,
            this.menuISave,
            this.menuSaveAs,
            this.menuItem3,
            this.menuMRU});
            this.menuItem1.Text = "&File";
            // 
            // mnuChooseFolder
            // 
            this.mnuChooseFolder.Index = 0;
            this.mnuChooseFolder.Text = "&Choose Group...";
            this.mnuChooseFolder.Click += new System.EventHandler(this.OnChooseFile);
            // 
            // menuISave
            // 
            this.menuISave.Index = 1;
            this.menuISave.Text = "&Save";
            this.menuISave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Index = 2;
            this.menuSaveAs.Text = "Save &As...";
            this.menuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Text = "-";
            // 
            // menuMRU
            // 
            this.menuMRU.Index = 4;
            this.menuMRU.Text = "MRU";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuPaste});
            this.menuItem2.Text = "&Edit";
            // 
            // mnuPaste
            // 
            this.mnuPaste.Enabled = false;
            this.mnuPaste.Index = 0;
            this.mnuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.mnuPaste.Text = "&Paste Picture";
            this.mnuPaste.Click += new System.EventHandler(this.OnPastePicture);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = -1;
            this.menuItem5.Text = "-";
            // 
            // mnuSave
            // 
            this.mnuSave.Index = -1;
            this.mnuSave.Text = "&Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Index = -1;
            this.mnuSaveAs.Text = "Save As...";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // mruHandler1
            // 
            this.mruHandler1.MruItem = this.menuMRU;
            this.mruHandler1.MRUStyle = MostRecentlyUsedHandler.MRUStyle.Inline;
            this.mruHandler1.StorageName = "mru";
            this.mruHandler1.MRUItemClicked += new MostRecentlyUsedHandler.MRUItemClickedHandler(this.mruHandler1_MRUItemClicked);
            // 
            // statusDisplay1
            // 
            this.statusDisplay1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusDisplay1.Location = new System.Drawing.Point(0, 514);
            this.statusDisplay1.Name = "statusDisplay1";
            this.statusDisplay1.Size = new System.Drawing.Size(807, 13);
            this.statusDisplay1.TabIndex = 10;
            // 
            // _modeControl
            // 
            this._modeControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._modeControl.Location = new System.Drawing.Point(6, 9);
            this._modeControl.Name = "_modeControl";
            this._modeControl.SelectedIndex = 0;
            this._modeControl.Size = new System.Drawing.Size(779, 506);
            this._modeControl.TabIndex = 14;
            this._modeControl.SelectedIndexChanged += new System.EventHandler(this._modeControl_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(807, 527);
            this.Controls.Add(this._modeControl);
            this.Controls.Add(this.statusDisplay1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Subachup!";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.mruHandler1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion



	

		private void Form1_Load(object sender, System.EventArgs e)
		{
			_propertyTable.RestoreFromFile(null);

			if(Directory.Exists(LastUsedPlayListPath))
			{
				UseGroupFile(LastUsedPlayListPath, false);
			}

			UtteranceCollection.CurrentUtteranceSet.LoadUserStuff();


             AddTab(new ListenControl(_propertyTable), "Listen");
           AddTab(new RecognitionQuizControl(_propertyTable), "Comprehension Quiz");
          //  AddTab(new GatherTab(_propertyTable), "Gather");

            _previousPage = _modeControl.SelectedTab;
            if (_modeControl.SelectedTab != null)
                CurrentSubachupControl.Showing();

		}


        private void AddTab(SubachupTabControl  control, string label)
        {
            System.Windows.Forms.TabPage page = new System.Windows.Forms.TabPage();

            control.Dock = System.Windows.Forms.DockStyle.Fill;
            control.Location = new System.Drawing.Point(0, 0);
            control.Name = label;
            control.Size = new System.Drawing.Size(771, 481);
            control.TabIndex = 0;

            page.Controls.Add(control);
            page.Location = new System.Drawing.Point(4, 22);
            page.Name = label;
            page.Size = new System.Drawing.Size(771, 481);
            page.TabIndex = 1;
            page.Text = label;
            page.Tag = control;
            _modeControl.TabPages.Add(page);

        }


		private void UseGroupFile(string collectionFilePath,bool doRemember)
		{
			if(doRemember)
                PreviousCollectionFile =  collectionFilePath;
			if(!System.IO.File.Exists(collectionFilePath))
				return;

			UtteranceCollection.CurrentUtteranceSet.LoadFromCollectionFile(collectionFilePath);

			mruHandler1.AddRecentlyUsedFile(collectionFilePath);
			UpdateWindowCaption();

            UtteranceCollection.CurrentUtteranceSet.DidChange();
		}

        private SubachupTabControl CurrentSubachupControl
        {
            get
            {
                return ((SubachupTabControl)_modeControl.SelectedTab.Tag);
            }
        }

		private void UpdateWindowCaption()
		{
			this.Text = Application.ProductName + "   " + PreviousCollectionFile;
		}

	



		private void mruHandler1_MRUItemClicked(object sender, MostRecentlyUsedHandler.MRUItemClickedEventArgs e)
		{
			if(!File.Exists(e.File))
			{
				//TODO: add this ability mruHandler1.RemoveFile(e.File);
			}
			else
				UseGroupFile(e.File, true);		
		}


		private void OnChooseFile(object sender, System.EventArgs e)
		{
		    OpenFileDialog dialog = new OpenFileDialog();
			//try to start at the parent of the current file
			if(PreviousCollectionFile != null && File.Exists(PreviousCollectionFile))
			{
                dialog.InitialDirectory = Directory.GetParent(PreviousCollectionFile).FullName;
			}

			if(DialogResult.OK != dialog.ShowDialog())
				return;

			UseGroupFile( dialog.FileName, true);	
		}

   
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            //foreach(TabPage page in _modeControl.TabPages)
            //{
            //    SubachupTabControl tab =( SubachupTabControl)page.Tag; 
            //    tab.Hiding();
            //}
            SubachupTabControl tab = (SubachupTabControl)_previousPage.Tag;
            tab.Hiding();
			UtteranceCollection.CurrentUtteranceSet.SavePlayList(LastUsedPlayListPath);
			UtteranceCollection.CurrentUtteranceSet.SaveUserStuff();
			_propertyTable.Save(null,null);
		}

		private string LastUsedPlayListPath
		{
			get
			{				
				return Path.Combine(_propertyTable.UserSettingDirectory, "LastUsed");
			}
		}

		private void _modeControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//save the score stuff
			UtteranceCollection.CurrentUtteranceSet.SaveUserStuff();

            if (_previousPage != null)
            {
                SubachupTabControl tab = (SubachupTabControl)_previousPage.Tag;

                //tell the new guy to save anything
                if (tab.Hiding())
                {
                  _previousPage = _modeControl.SelectedTab;
                }
                else //go back
                    _modeControl.SelectedTab = _previousPage;
            }
            else
                _previousPage = _modeControl.SelectedTab;


            //tell the new guy to update
            if (_previousPage.Tag is SubachupTabControl)
                ((SubachupTabControl)_previousPage.Tag).Showing();
		}


		private void mnuSave_Click(object sender, System.EventArgs e)
		{
			UtteranceCollection.CurrentUtteranceSet.SavePlayList(PreviousCollectionFile);
		}

		private void mnuSaveAs_Click(object sender, System.EventArgs e)
		{
			this.folderBrowserDialog1.SelectedPath = SavedSetsDirectory;

			folderBrowserDialog1.ShowNewFolderButton=true;
			if(DialogResult.OK != folderBrowserDialog1.ShowDialog())
				return;
			UtteranceCollection.CurrentUtteranceSet.SavePlayList(folderBrowserDialog1.SelectedPath);
			PreviousCollectionFile = folderBrowserDialog1.SelectedPath;

			UpdateWindowCaption();
		}

		private string SavedSetsDirectory
		{
			get 
			{
				string path = @"C:\thai\SubachupSets";
				if(!Directory.Exists(path))
					Directory.CreateDirectory(path);
				return path;
			}
		}

		private string PreviousCollectionFile
		{
			get {return _propertyTable.GetStringProperty("PreviousDirectory",null);}
			set {_propertyTable.SetProperty("PreviousDirectory",value);}
		}

        private void OnPastePicture(object sender, EventArgs e)
        {
            CurrentSubachupControl.PasteImage();
        }
	}
}
