namespace subachup
{
    partial class RecordTab
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.LinkLabel();
            this.btnStopRecording = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "under construction";
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(42, 16);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(50, 23);
            this.btnRecord.TabIndex = 16;
            this.btnRecord.TabStop = true;
            this.btnRecord.Text = "Record";
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Location = new System.Drawing.Point(117, 14);
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(50, 23);
            this.btnStopRecording.TabIndex = 15;
            this.btnStopRecording.TabStop = true;
            this.btnStopRecording.Text = "Stop";
            // 
            // RecordTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnStopRecording);
            this.Controls.Add(this.label1);
            this.Name = "RecordTab";
            this.Size = new System.Drawing.Size(637, 336);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel btnRecord;
        private System.Windows.Forms.LinkLabel btnStopRecording;
    }
}
