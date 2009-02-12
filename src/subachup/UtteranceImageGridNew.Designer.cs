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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UtteranceImageGridNew));
            this._imageGrid = new System.Windows.Forms.ListView();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._locusEffectsProvider = new BigMansStuff.LocusEffects.LocusEffectsProvider(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // _imageGrid
            // 
            this._imageGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._imageGrid.LargeImageList = this._imageList;
            this._imageGrid.Location = new System.Drawing.Point(0, 0);
            this._imageGrid.Name = "_imageGrid";
            this._imageGrid.Size = new System.Drawing.Size(358, 271);
            this._imageGrid.TabIndex = 0;
            this._imageGrid.UseCompatibleStateImageBehavior = false;
            this._imageGrid.SelectedIndexChanged += new System.EventHandler(this._imageGrid_Click);
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
        private System.Windows.Forms.ImageList imageList1;
    }
}
