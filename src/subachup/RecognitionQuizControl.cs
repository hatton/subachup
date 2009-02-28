using System;
using System.Windows.Forms;

namespace Subachup
{
    public partial class RecognitionQuizControl : SubachupTabControl
    {
        protected bool _readyForNext = false;
        private QuizState _quizState = QuizState.Stopped;
        private enum QuizState { Stopped, Paused, Playing };
        private QuestionChooser _questionChooser;
        private IQuizItem _currentQuizItem;

        public RecognitionQuizControl()
        {
            InitializeComponent();
        }

       
        public RecognitionQuizControl(PropertyTable propertyTable)
            :base(propertyTable)
        {
            InitializeComponent();
            _questionChooser = new QuestionChooser(UtteranceCollection.CurrentUtteranceSet);
        }

        private void RecognitionQuizControl_Load(object sender, EventArgs e)
        {
            _focus.Value = _propertyTable.GetIntProperty("focus", 7);

            Reload();


        }

        public override bool Hiding()
        {
            _propertyTable.SetProperty("focus", _focus.Value);
            return base.Hiding();
        }

        private void utteranceImageGrid1_Clicked(object sender, EventArgs e)
        {
            QuizClick();
        }

		private void QuizOne(Utterance utterance)
		{
			_utteranceImageGrid.ClearSelectedItems();
			_readyForNext=false;
			_sound = _player.LoadStream(ResolveSoundPath(utterance.SoundPath));
		}



	

		private void UpdateDisplay ()
		{
			_statusDisplay.Scores = _questionChooser.MakeScoresArray();
			this.Refresh();//
		}


		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if(_sound ==null ||_sound.ActivityState == nBASS.State.Stopped)
			{
				if(UtteranceCollection.CurrentUtteranceSet !=null && UtteranceCollection.CurrentUtteranceSet.Count>0 && _readyForNext)
				{
				    _currentQuizItem = _questionChooser.PickNextQuizItem();
					QuizOne((Utterance)_currentQuizItem);
				}	
				if(_sound !=null)
					_sound.Play(true, nBASS.StreamPlayFlags.Default);		
			}

		}

        private void btnPlay_Click(object sender, System.EventArgs e)
		{
			if(_sound!=null && _sound.ActivityState == nBASS.State.Paused)
			{
				_sound.Resume();
				CurrentQuizState = QuizState.Playing;
			}
			else
			{
                _currentQuizItem = null;
				_readyForNext=true;
				timer1.Enabled=true;
				CurrentQuizState = QuizState.Playing;
			}


			UpdateButtons();
		}

		private void QuizClick()
		{
			if(_utteranceImageGrid.Grid.SelectedItems.Count == 0)
				return;

			if(!timer1.Enabled)
				return;//not playing


			ListViewItem item =_utteranceImageGrid.Grid.SelectedItems[0];
			Utterance utterance = (Utterance)item.Tag;

            if (_questionChooser.GaveAnswer(utterance))
            {
                _readyForNext = true;
                _correctSound.Play(true, nBASS.StreamPlayFlags.Default);
            }
            else
            {
                 _wrongSound.Play(true, nBASS.StreamPlayFlags.Default);
            }

			UpdateDisplay();
		}




		private void btnPause_Click(object sender, System.EventArgs e)
		{
			if(_sound.ActivityState == nBASS.State.Playing )
			{
				_sound.Pause();
				CurrentQuizState = QuizState.Paused;
			}
			UpdateButtons();
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			timer1.Enabled=false;
			_readyForNext=false;
			_currentQuizItem=null;
			
			if(_sound !=null)
				_sound.Stop();

			CurrentQuizState = QuizState.Stopped;
			UpdateButtons();

		}


		private QuizState CurrentQuizState
		{
			get {return _quizState;}
			set {_quizState = value;}
		}

		private void UpdateButtons()
		{
			btnPause.Enabled = CurrentQuizState== QuizState.Playing;
			btnStop.Enabled = CurrentQuizState== QuizState.Playing || CurrentQuizState== QuizState.Paused;
			btnPlay.Enabled = (UtteranceCollection.CurrentUtteranceSet !=null && UtteranceCollection.CurrentUtteranceSet.Count>0)
				&& (CurrentQuizState== QuizState.Stopped || CurrentQuizState== QuizState.Paused);

        }



        private void btnShowMe_Click(object sender, EventArgs e)
        {
            if (_currentQuizItem ==null)
                return;

            _currentQuizItem.Score = -1; // if you had to ask, we figure you don't know
            _utteranceImageGrid.PointOutUtterance((Utterance)_currentQuizItem);
        }


        public override void Reload()
        {
            _currentQuizItem = _questionChooser.PickNextQuizItem();//review
            _utteranceImageGrid.LoadGrid();
            _utteranceImageGrid.Shuffle();
            UpdateButtons();
        }


	}   
}