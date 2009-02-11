namespace Subachup
{
    partial class UtteranceImageGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UtteranceImageGrid));
            this._imageGrid = new System.Windows.Forms.ListView();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this.handleFileDropTimer = new System.Windows.Forms.Timer(this.components);
            this._locusEffectsProvider = new BigMansStuff.LocusEffects.LocusEffectsProvider(this.components);
            this.SuspendLayout();
            // 
            // _imageGrid
            // 
            this._imageGrid.AllowDrop = true;
            this._imageGrid.BackColor = System.Drawing.Color.White;
            this._imageGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._imageGrid.LargeImageList = this._imageList;
            this._imageGrid.Location = new System.Drawing.Point(0, 0);
            this._imageGrid.Name = "_imageGrid";
            this._imageGrid.Size = new System.Drawing.Size(823, 445);
            this._imageGrid.SmallImageList = this._imageList;
            this._imageGrid.TabIndex = 1;
            this._imageGrid.UseCompatibleStateImageBehavior = false;
            this._imageGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this._imageGrid_DragEnter);
            this._imageGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this._imageGrid_DragDrop);
            this._imageGrid.DoubleClick += new System.EventHandler(this._imageGrid_DoubleClick);
            this._imageGrid.Click += new System.EventHandler(this._imageGrid_Click);
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "");
            this._imageList.Images.SetKeyName(1, "");
            // 
            // handleFileDropTimer
            // 
            this.handleFileDropTimer.Interval = 5000;
            this.handleFileDropTimer.Tick += new System.EventHandler(this.handleFileDropTimer_Tick);
            // 
            // _locusEffectsProvider
            // 
            this._locusEffectsProvider.FramesPerSecond = 25;
            // 
            // UtteranceImageGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this._imageGrid);
            this.Name = "UtteranceImageGrid";
            this.Size = new System.Drawing.Size(823, 445);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _imageGrid;
        private System.Windows.Forms.Timer handleFileDropTimer;
        private BigMansStuff.LocusEffects.LocusEffectsProvider _locusEffectsProvider;
        private System.Windows.Forms.ImageList _imageList;
    }
}
