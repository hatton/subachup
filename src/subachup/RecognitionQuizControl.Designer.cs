using Subachup.Core;

namespace Subachup
{
    partial class RecognitionQuizControl 
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
            this.btnShowMe = new System.Windows.Forms.LinkLabel();
            this.btnStop = new MyControls.AudioButton();
            this.btnPause = new MyControls.AudioButton();
            this.btnPlay = new MyControls.AudioButton();
            this.labelRandom = new System.Windows.Forms.Label();
            this._focus = new System.Windows.Forms.TrackBar();
            this.labelFocussed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._statusDisplay = new LQ.StatusDisplay();
            this._utteranceImageGrid = new UtteranceImageGridNew();
            ((System.ComponentModel.ISupportInitialize)(this._focus)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShowMe
            // 
            this.btnShowMe.Location = new System.Drawing.Point(141, 15);
            this.btnShowMe.Name = "btnShowMe";
            this.btnShowMe.Size = new System.Drawing.Size(100, 23);
            this.btnShowMe.TabIndex = 26;
            this.btnShowMe.TabStop = true;
            this.btnShowMe.Text = "Show me";
            this.btnShowMe.Click += new System.EventHandler(this.btnShowMe_Click);
            // 
            // btnStop
            // 
            this.btnStop.ButtonType = MyControls.AudioButtonType.Stop;
            this.btnStop.IconLocation = new System.Drawing.Point(8, 8);
            this.btnStop.IconSize = new System.Drawing.Size(14, 14);
            this.btnStop.Location = new System.Drawing.Point(67, 8);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(32, 32);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "btnStop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.ButtonType = MyControls.AudioButtonType.Pause;
            this.btnPause.IconLocation = new System.Drawing.Point(8, 8);
            this.btnPause.IconSize = new System.Drawing.Size(14, 14);
            this.btnPause.Location = new System.Drawing.Point(31, 8);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(32, 32);
            this.btnPause.TabIndex = 24;
            this.btnPause.Text = "btnPause";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.ButtonType = MyControls.AudioButtonType.Play;
            this.btnPlay.IconLocation = new System.Drawing.Point(8, 8);
            this.btnPlay.IconSize = new System.Drawing.Size(14, 14);
            this.btnPlay.Location = new System.Drawing.Point(1, 7);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(32, 32);
            this.btnPlay.TabIndex = 23;
            this.btnPlay.Text = "btnPlay";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // labelRandom
            // 
            this.labelRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRandom.Location = new System.Drawing.Point(501, 7);
            this.labelRandom.Name = "labelRandom";
            this.labelRandom.Size = new System.Drawing.Size(47, 23);
            this.labelRandom.TabIndex = 21;
            this.labelRandom.Text = "Random";
            // 
            // _focus
            // 
            this._focus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._focus.LargeChange = 1;
            this._focus.Location = new System.Drawing.Point(543, 4);
            this._focus.Name = "_focus";
            this._focus.Size = new System.Drawing.Size(160, 50);
            this._focus.TabIndex = 20;
            this._focus.Value = 7;
            // 
            // labelFocussed
            // 
            this.labelFocussed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFocussed.Location = new System.Drawing.Point(806, 8);
            this.labelFocussed.Name = "labelFocussed";
            this.labelFocussed.Size = new System.Drawing.Size(57, 23);
            this.labelFocussed.TabIndex = 22;
            this.labelFocussed.Text = "Focussed";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(709, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Focussed";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _statusDisplay
            // 
            this._statusDisplay.Location = new System.Drawing.Point(0, 433);
            this._statusDisplay.Name = "_statusDisplay";
            this._statusDisplay.Size = new System.Drawing.Size(251, 27);
            this._statusDisplay.TabIndex = 29;
            this._statusDisplay.Anchor=(System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom );
            // 
            // _utteranceImageGrid
            // 
            this._utteranceImageGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._utteranceImageGrid.BackColor = System.Drawing.SystemColors.Control;
            this._utteranceImageGrid.Location = new System.Drawing.Point(1, 60);
            this._utteranceImageGrid.Name = "_utteranceImageGrid";
            this._utteranceImageGrid.Size = new System.Drawing.Size(796, 375);
            this._utteranceImageGrid.TabIndex = 28;
            this._utteranceImageGrid.Clicked += new System.EventHandler(this.utteranceImageGrid1_Clicked);
            // 
            // RecognitionQuizControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._statusDisplay);
            this.Controls.Add(this._utteranceImageGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShowMe);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.labelRandom);
            this.Controls.Add(this._focus);
            this.Controls.Add(this.labelFocussed);
            this.Name = "RecognitionQuizControl";
            this.Size = new System.Drawing.Size(800, 463);
            this.Load += new System.EventHandler(this.RecognitionQuizControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this._focus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel btnShowMe;
        private MyControls.AudioButton btnStop;
        private MyControls.AudioButton btnPause;
        private MyControls.AudioButton btnPlay;
        private System.Windows.Forms.Label labelRandom;
        private System.Windows.Forms.TrackBar _focus;
        private System.Windows.Forms.Label labelFocussed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private UtteranceImageGridNew _utteranceImageGrid;
        private LQ.StatusDisplay _statusDisplay;
    }
}
