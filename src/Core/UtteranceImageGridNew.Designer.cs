namespace Subachup.Core
{
    partial class UtteranceImageGridNew
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("", 0);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("", 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UtteranceImageGridNew));
            this._imageGrid = new System.Windows.Forms.ListView();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._locusEffectsProvider = new BigMansStuff.LocusEffects.LocusEffectsProvider(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // _imageGrid
            // 
            this._imageGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this._imageGrid.LargeImageList = this._imageList;
            this._imageGrid.Location = new System.Drawing.Point(0, 0);
            this._imageGrid.Name = "_imageGrid";
            this._imageGrid.Size = new System.Drawing.Size(358, 150);
            this._imageGrid.TabIndex = 0;
            this._imageGrid.UseCompatibleStateImageBehavior = false;
            // 
            // _imageList
            // 
            this._imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._imageList.ImageSize = new System.Drawing.Size(84, 84);
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _locusEffectsProvider
            // 
            this._locusEffectsProvider.FramesPerSecond = 25;
            // 
            // listView1
            // 
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(33, 179);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(302, 89);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Eag00445.tif");
            this.imageList1.Images.SetKeyName(1, "EAG00801.tif");
            // 
            // UtteranceImageGridNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this._imageGrid);
            this.Name = "UtteranceImageGridNew";
            this.Size = new System.Drawing.Size(358, 271);
            this.Load += new System.EventHandler(this.UtteranceImageGridNew_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _imageGrid;
        private System.Windows.Forms.ImageList _imageList;
        private BigMansStuff.LocusEffects.LocusEffectsProvider _locusEffectsProvider;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
