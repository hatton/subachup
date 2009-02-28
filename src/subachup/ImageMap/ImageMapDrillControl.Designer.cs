namespace subachup
{
    partial class ImageMapDrillControl
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
            this._imageBox = new subachup.ImageMapBox();
            this.SuspendLayout();
            // 
            // _imageBox
            // 
            this._imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._imageBox.Location = new System.Drawing.Point(0, 0);
            this._imageBox.Name = "_imageBox";
            this._imageBox.Size = new System.Drawing.Size(830, 524);
            this._imageBox.TabIndex = 0;
            this._imageBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this._imageBox_MouseClick);
            // 
            // ImageMapDrillControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._imageBox);
            this.Name = "ImageMapDrillControl";
            this.Size = new System.Drawing.Size(830, 524);
            this.Load += new System.EventHandler(this.ImageMapDrillControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageMapBox _imageBox;
    }
}
