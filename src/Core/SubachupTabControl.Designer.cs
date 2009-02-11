namespace Subachup
{
    partial class SubachupTabControl 
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
            this.SuspendLayout();
            // 
            // SubachupTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SubachupTabControl";
            this.Load += new System.EventHandler(this.SubachupTabControl_Load);
            this.Validated += new System.EventHandler(this.SubachupTabControl_Validated);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.SubachupTabControl_Validating);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
