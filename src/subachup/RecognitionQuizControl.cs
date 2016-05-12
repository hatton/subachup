using System;
using System.Collections.Generic;
using System.Windows.Forms;
using subachup.utility;

namespace subachup
{
    /// <summary>
    /// This control works with at least two kinds of quizes (grid of pictures or a single picture with hot regions.
    /// The control for the actual quiz display is provided to the constructor. 
    /// 
    /// The logic controlling this is given as the PresentationModel.
    /// </summary>
    public partial class RecognitionQuizControl : SubachupTabControl
    {
        private readonly IAnswersControl _answersControl;
        private readonly QuizPresentationModel _presentationModel;

        public RecognitionQuizControl()
        {
            InitializeComponent();
        }


        public RecognitionQuizControl(IAnswersControl answersControl ,QuizPresentationModel presentationModel, PropertyTable propertyTable)
            :base(propertyTable)
        {
            _answersControl = answersControl;
            _presentationModel = presentationModel;
            InitializeComponent();

            var asControl = answersControl as Control;
            asControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            asControl.Location = new System.Drawing.Point(1, 60);
            asControl.Size = new System.Drawing.Size(796, 375);
            Controls.Add(asControl);
            _answersControl.GaveAnAnswer += new Proc<IEnumerable<IQuizItem>>(OnAnswersControl_GaveAnAnswer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usersAnswers">plural because it may be ambigous (one part of a picture could be the answer for multiple questions)</param>
        void OnAnswersControl_GaveAnAnswer(IEnumerable<IQuizItem> usersAnswers)
        {
            _presentationModel.QuizClick(usersAnswers);
            UpdateDisplay();
        }

        private void RecognitionQuizControl_Load(object sender, EventArgs e)
        {
            _focus.Value = _propertyTable.GetIntProperty("focus", 7);
            _answersControl.QuizItems = _presentationModel.QuizItems;
            Reload();
        }

        public override bool Hiding()
        {
            _propertyTable.SetProperty("focus", _focus.Value);
            return base.Hiding();
        }

		private void UpdateDisplay ()
		{
			_statusDisplay.Scores = _presentationModel.GetScores();
			this.Refresh();//
		}

        private void btnPlay_Click(object sender, System.EventArgs e)
		{
            _presentationModel.PlayClicked();
			UpdateButtons();
		}

		private void btnPause_Click(object sender, System.EventArgs e)
		{
            _presentationModel.PauseClicked();
			UpdateButtons();
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
		    _presentationModel.StopClicked();
			UpdateButtons();
		}

		private void UpdateButtons()
		{
		    btnPause.Enabled = _presentationModel.CanPause;
		    btnStop.Enabled = _presentationModel.CanStop;
		    btnPlay.Enabled = _presentationModel.CanPlay;
        }


        private void OnShowMeClicked(object sender, EventArgs e)
        {
            if (_presentationModel.CurrentQuizItem ==null)
                return;

            _presentationModel.ShowMeClicked();
            _answersControl.ShowAnswerLocations((Utterance)_presentationModel.CurrentQuizItem);
        }


        public override void Reload()
        {
            _presentationModel.Reload();
            _answersControl.LoadContents();
            UpdateButtons();
        }
	}   
}