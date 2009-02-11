using System;
using System.Windows.Forms;

namespace Subachup
{
    public partial class RecognitionQuizControl : SubachupTabControl
    {
        protected int _audioIndex;
        protected bool _readyForNext = false;
        private QuizState _quizState = QuizState.Stopped;
        private enum QuizState { Stopped, Paused, Playing };

        public RecognitionQuizControl()
        {
            InitializeComponent();
        }

       
        public RecognitionQuizControl(PropertyTable propertyTable)
            :base(propertyTable)
        {
            InitializeComponent();
 
        }

        private void RecognitionQuizControl_Load(object sender, EventArgs e)
        {
            _focus.Value = _propertyTable.GetIntProperty("focus", 7);

            Reload();

            UpdateButtons();
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
			_sound = _player.LoadStream(ResolveSoundPath(utterance.FilePath));
		}



		protected int[] RandomizeVisitOrder(int[] input)
		{
			int[] output = new int[input.Length];
			for( int j = 0; j < output.Length; j++ )
			{
				output[j]=-1;
			}

			Random rnd = new Random();
			int count = 0;

			while( count < output.Length )
			{
				int destination = rnd.Next( 0, output.Length );

				if( output[destination] == -1 )
				{
					output[destination] = count;
					count++;
				}
			}
			return output;
		}
		protected int PickNextWord()
		{
			Random rnd = new Random();
			//int pick = rnd.Next( 0, _audioFiles.Length );
			double howRandom= (10-_focus.Value); //0 to 10
			int best=-1;
			double lowScore=100000;
			int[] scores =MakeScoresArray();
			int[] visit=RandomizeVisitOrder(scores);
			for(int i=0;i<visit.Length;i++)
			{
				int whichAudio = visit[i];

				double randomizer=0.5-rnd.NextDouble(); // centered around 0
				randomizer =randomizer * howRandom; //spread out around 0 (-5 to 5?)
				double score =  randomizer + (scores[whichAudio]);
				
				//todo need something to favor those we haven't even tested
				//score += _correctCount[whichAudio]*.1;
				
				//				System.Diagnostics.Debug.WriteLine(_scores[whichAudio]+" "+score+" "+_audioFiles[whichAudio]);
				if(whichAudio == _audioIndex)
					score = 100; //very unlikely, but won't break if only one item in the quiz
				if (score <lowScore)
				{
					lowScore = score;
					best =whichAudio;
				}
			}
			//			System.Diagnostics.Debug.WriteLine("Chose "+ _audioFiles[best]);
			return best;
		}



		private int[] MakeScoresArray()
		{
			int[] scores = new int[UtteranceCollection.CurrentUtteranceSet.Count];

			for(int i=0; i< UtteranceCollection.CurrentUtteranceSet.Count;i++)
			{
				scores[i] = ((Utterance)UtteranceCollection.CurrentUtteranceSet[i]).Score;
			}
			return scores;
		}

		private void UpdateDisplay ()
		{
			_statusDisplay.Scores = MakeScoresArray();
			this.Refresh();//
		}


		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if(_sound ==null ||_sound.ActivityState == nBASS.State.Stopped)
			{
				if(UtteranceCollection.CurrentUtteranceSet !=null && UtteranceCollection.CurrentUtteranceSet.Count>0 && _readyForNext)
				{
					_audioIndex = PickNextWord();
					QuizOne((Utterance)UtteranceCollection.CurrentUtteranceSet[_audioIndex]);
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
				_audioIndex =-1;
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

			//			if(e. == MouseButtons.Right)
			//				return;

			ListViewItem item =_utteranceImageGrid.Grid.SelectedItems[0];
			Utterance utterance = (Utterance)item.Tag;

			if (utterance ==CurrentUtterance)
			{
				++utterance.Score;
				++utterance.CorrectWithoutMistakesCount;

				//_utteranceImageGrid.Grid..BackColor= System.Drawing.Color.Green;
				_readyForNext=true; //++_audioIndex;
				//_progress.Value++;
				_correctSound.Play(true, nBASS.StreamPlayFlags.Default);		
			}
			else
			{
				CurrentUtterance.CorrectWithoutMistakesCount=0;
				CurrentUtterance.Score=-2; //-=2;//you'll get one back when you click it right
				//also decrement the score of the one we mistakenly clicked on
				utterance.CorrectWithoutMistakesCount=0;
				utterance.Score=-1;

				//_utteranceImageGrid.Grid..BackColor= System.Drawing.Color.Red;
				_wrongSound.Play(true, nBASS.StreamPlayFlags.Default);		
			}

			CurrentUtterance.LastQuizzedDate = DateTime.Now;

			UpdateDisplay();
		}

		private Utterance CurrentUtterance
		{
			get{return ((Utterance)UtteranceCollection.CurrentUtteranceSet[_audioIndex]);}
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
			_audioIndex=-1;
			
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
            CurrentUtterance.Score = -1;
            Utterance u = (Utterance)UtteranceCollection.CurrentUtteranceSet[_audioIndex];
            _utteranceImageGrid.PointOutUtterance(u);
//            _utteranceImageGrid.PointOutUtterance(_audioIndex);
        }


        public override void Reload()
        {
            _audioIndex = 0;
            _utteranceImageGrid.LoadGrid();
            _utteranceImageGrid.Shuffle();
        }


	}   
}