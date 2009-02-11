/*using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Ilan.Google.API.ImageSearch;

namespace Ilan.Test.Google.API
{
	/// <summary>
	/// Summary description for ChooseGoogleDialog.
	/// </summary>
	public class ChooseGoogleDialog : Form
	{
		public TextBox txtQuery;
		private Label label1;
		private Label label2;
		private NumericUpDown nudNumOfResults;
		private Label label3;
		private NumericUpDown nudStartPosition;
		private Button btnSearch;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private Size imageSize = new Size(150, 150);
		private ArrayList pics = new ArrayList();
		private readonly int startHeight = 70;
		private Thread picThread;

		public SearchResult _result=null;

		public ChooseGoogleDialog()
		{
			InitializeComponent();
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
			this.txtQuery = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.nudNumOfResults = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.nudStartPosition = new System.Windows.Forms.NumericUpDown();
			this.btnSearch = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudNumOfResults)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudStartPosition)).BeginInit();
			this.SuspendLayout();
			// 
			// txtQuery
			// 
			this.txtQuery.Location = new System.Drawing.Point(88, 8);
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.Size = new System.Drawing.Size(416, 20);
			this.txtQuery.TabIndex = 0;
			this.txtQuery.Text = "";
			this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Query:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Nr. of results:";
			// 
			// nudNumOfResults
			// 
			this.nudNumOfResults.Location = new System.Drawing.Point(88, 40);
			this.nudNumOfResults.Maximum = new System.Decimal(new int[] {
																			1000,
																			0,
																			0,
																			0});
			this.nudNumOfResults.Minimum = new System.Decimal(new int[] {
																			1,
																			0,
																			0,
																			0});
			this.nudNumOfResults.Name = "nudNumOfResults";
			this.nudNumOfResults.Size = new System.Drawing.Size(56, 20);
			this.nudNumOfResults.TabIndex = 3;
			this.nudNumOfResults.Value = new System.Decimal(new int[] {
																		  10,
																		  0,
																		  0,
																		  0});
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(152, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Start position:";
			// 
			// nudStartPosition
			// 
			this.nudStartPosition.Location = new System.Drawing.Point(232, 40);
			this.nudStartPosition.Maximum = new System.Decimal(new int[] {
																			 1000,
																			 0,
																			 0,
																			 0});
			this.nudStartPosition.Name = "nudStartPosition";
			this.nudStartPosition.Size = new System.Drawing.Size(48, 20);
			this.nudStartPosition.TabIndex = 5;
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(512, 8);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(80, 24);
			this.btnSearch.TabIndex = 8;
			this.btnSearch.Text = "Search!";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// ChooseGoogleDialog
			// 
			this.AcceptButton = this.btnSearch;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(624, 477);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.nudStartPosition);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.nudNumOfResults);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtQuery);
			this.Name = "ChooseGoogleDialog";
			this.Text = "Choose Image From Google";
			this.Load += new System.EventHandler(this.ChooseGoogleDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudNumOfResults)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudStartPosition)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion



		private void btnSearch_Click(object sender, EventArgs e)
		{
			SearchNow();
		}

        struct ImageQuery
        {
            public string txt;
            public int start;
            public int wanted;
        }

		private void SearchNow()
		{
			btnSearch.Enabled = false;
			foreach (PictureBox pic in pics)
			{
				pic.DoubleClick -= new EventHandler(pic_DoubleClick);
				this.Controls.Remove(pic);
			}
			pics.Clear();
            
			picThread = new Thread(new ParameterizedThreadStart(GetPictures));
			picThread.IsBackground = true;
            ImageQuery iq = new ImageQuery();
            iq.txt = txtQuery.Text;
            iq.start =  Decimal.ToInt32(nudStartPosition.Value);
            iq.wanted = Decimal.ToInt32(nudNumOfResults.Value);
            picThread.Start(iq);
		}
         
         private void GetPictures(object data)
		{
            SearchCompleteHandler searchCompleteHandler = new SearchCompleteHandler(SearchComplete);
            try
            {
                //txtQuery.Text = Regex.Replace(txtQuery.Text, @"\s{1,}", "+");
                ImageQuery iq = (ImageQuery)data;
                iq.txt = Regex.Replace(iq.txt, @"\s{1,}", "+");
                SafeSearchFiltering safeSearch = SafeSearchFiltering.Active;
                //SearchResponse response = SearchService.SearchImages(txtQuery.Text, startPosition, resultsRequested, true, safeSearch);
                SearchResponse response = SearchService.SearchImages(iq.txt, iq.start, iq.wanted, true, SafeSearchFiltering.Moderate);
                if (response.Results.Length == 0)
                {
                    MessageBox.Show("No results available");
                }

                for (int i = 0; i < response.Results.Length; i++)
                {
                    PictureBox pic = new PictureBox();
                    pic.BorderStyle = BorderStyle.Fixed3D;
                    pic.Size = imageSize;
                    pic.Location = new Point(imageSize.Width * (i % 4), (i / 4) * imageSize.Height + startHeight);
                    pic.SizeMode = PictureBoxSizeMode.CenterImage;
                    pic.Image = GetImage(response.Results[i].ThumbnailUrl);
                    pics.Add(pic);
                    pic.Tag = response.Results[i];
                    pic.DoubleClick += new EventHandler(pic_DoubleClick);
                    AddPictureBoxHandler handler = new AddPictureBoxHandler(AddPictureBox);
                    object[] args = new object[1];
                    args[0] = pic;
                    this.Invoke(handler, args);
                }

                //MessageBox.Show("Done!");
            }
            catch (ThreadAbortException)
            {
                searchCompleteHandler=null; //don't say search complete
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An exception occurred while running the query: {0}{1}{2}",
                                              ex.Message, Environment.NewLine, ex.StackTrace), "Query Aborted!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			finally
			{
                if(searchCompleteHandler!=null)
                     this.Invoke(searchCompleteHandler);
			}
		}

        private void SearchComplete()
        {
 				btnSearch.Enabled = true;
       }
		private delegate void AddPictureBoxHandler(PictureBox pic);
        private delegate void SearchCompleteHandler();

		private void AddPictureBox(PictureBox pic)
		{
			// Adjust for scrolled location
			if (AutoScrollPosition.Y != 0)
			{
				pic.Location = new Point(pic.Location.X, pic.Location.Y + AutoScrollPosition.Y);
			}
			this.Controls.Add(pic);
		}

		public Image GetImage(string url)
		{
			Image im = null;
			try
			{				
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "GET";
				request.Timeout = 15000;
				request.ProtocolVersion = HttpVersion.Version11;

				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					using (Stream responseStream = response.GetResponseStream())
					{	
						im = Image.FromStream(responseStream);
					}
				}
			}
			catch (Exception ex) 
			{
				Debug.WriteLine("Exception in getThumbnail. Url: " + url + ". Info: " + ex.Message + Environment.NewLine + "Stack: " + ex.StackTrace);
			}
			return im;
		}

		private void txtQuery_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnSearch_Click(this, null);
			}
		}

		private void pic_DoubleClick(object sender, EventArgs e)
		{
            if (picThread != null)// && picThread.ThreadState == System.Threading.ThreadState.Running)
                picThread.Abort();

			PictureBox pic = sender as PictureBox;
			_result = pic.Tag as SearchResult;
			
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void form_KeyDown(object sender, KeyEventArgs e)
		{
			Form form = sender as Form;
			if ((form != null) && (e.KeyCode == Keys.Escape))
			{
				form.Hide();
				form.Close();
			}
		}

		private void ChooseGoogleDialog_Load(object sender, System.EventArgs e)
		{
			SearchNow();		
		}
	}
}
*/