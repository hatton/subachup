using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LQ
{
	/// <summary>
	/// Summary description for StatusDisplay.
	/// </summary>
	public class StatusDisplay : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int[] _scores;

		public StatusDisplay()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			label1.Text="";

		}

		public int[] Scores

		{
			set
			{
				_scores=value;
				this.Refresh();
			}
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
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(251, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// StatusDisplay
			// 
			this.Controls.Add(this.label1);
			this.Name = "StatusDisplay";
			this.Size = new System.Drawing.Size(251, 27);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.StatusDisplay_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		private void StatusDisplay_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			String s="";
			if(_scores == null)
				return;
			foreach(int score in _scores)
			{	
				s+=score+" ";
			}
			label1.Text=s;
            //label1.Refresh();//doesn't work
            //this.Refresh();//doesn't work
		}
	}
}
