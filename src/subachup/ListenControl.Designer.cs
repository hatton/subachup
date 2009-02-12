using Subachup.Core;

namespace Subachup
{
    partial class ListenControl
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
            this._utteranceImageGrid = new UtteranceImageGridNew();
            this.SuspendLayout();
            // 
            // _utteranceImageGrid
            // 
            this._utteranceImageGrid.BackColor = System.Drawing.SystemColors.Control;
            this._utteranceImageGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._utteranceImageGrid.Location = new System.Drawing.Point(0, 0);
            this._utteranceImageGrid.Name = "_utteranceImageGrid";
            this._utteranceImageGrid.Size = new System.Drawing.Size(850, 585);
            this._utteranceImageGrid.TabIndex = 0;
            this._utteranceImageGrid.Clicked += new System.EventHandler(this._utteranceImageGrid_Clicked);
            // 
            // ListenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._utteranceImageGrid);
            this.Name = "ListenControl";
            this.Size = new System.Drawing.Size(850, 585);
            this.Load += new System.EventHandler(this.ListenControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UtteranceImageGridNew _utteranceImageGrid;
    }
}
