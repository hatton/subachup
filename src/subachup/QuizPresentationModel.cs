using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace subachup
{
    public class QuizPresentationModel : IDisposable
    {
        protected bool _readyForNext = false;
        private QuizState _quizState = QuizState.Stopped;
        private enum QuizState { Stopped, Paused, Playing };
        private QuestionChooser _questionChooser;
        public IQuizItem CurrentQuizItem{ get; set;}
        private System.Windows.Forms.Timer _timer;
        private IEnumerable<IQuizItem> _quizItems;
        private SoundPlayer _soundPlayer;

        public QuizPresentationModel(Form parentFormForPlayer, IEnumerable<IQuizItem> quizItems)
        {
            _quizItems = quizItems;
            _soundPlayer = new SoundPlayer(parentFormForPlayer);
            _questionChooser = new QuestionChooser(quizItems);
            _timer = new Timer();            
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(timer1_Tick);
        }


		private void QuizOne(Utterance utterance)
		{
			// could make some kind of callback for this: _utteranceImageGrid.ClearSelectedItems();
			_readyForNext=false;
		    _soundPlayer.SetCurrentUtterance(ResolveSoundPath(utterance.SoundPath));

		}

        private string ResolveSoundPath(string path)
        {
            return path;
        }

        public void QuizClick(IEnumerable<IQuizItem> usersAnswers)
        {

            if (!_timer.Enabled)
                return;//not playing

            if (_questionChooser.GaveAnswer(usersAnswers))
            {
                _readyForNext = true;
                _soundPlayer.PlayCorrectAnswerChime();
            }
            else
            {
                _soundPlayer.PlayWrongAnswerChime();
            }

        }


        public void PauseClicked()
		{
//			if(_sound.ActivityState == nBASS.State.Playing )
            if (CurrentQuizState == QuizState.Playing)
			{
			    _soundPlayer.Pause();
				CurrentQuizState = QuizState.Paused;
			}
		}

		public void StopClicked()
		{
			_timer.Enabled=false;
			_readyForNext=false;
			CurrentQuizItem=null;

		    _soundPlayer.Stop();

			CurrentQuizState = QuizState.Stopped;
		}


		private QuizState CurrentQuizState
		{
			get {return _quizState;}
			set {_quizState = value;}
		}

        public bool CanPause
        {
            get { return CurrentQuizState == QuizState.Playing; }
        }

        public bool CanStop
        {
            get { return CurrentQuizState == QuizState.Playing || CurrentQuizState == QuizState.Paused; }
        }

        public bool CanPlay
        {
            get
            {
                return (_quizItems != null && _quizItems.Count() > 0)
                    && (CurrentQuizState == QuizState.Stopped || CurrentQuizState == QuizState.Paused);
            }
        }


        public void PlayClicked()
        {
            //if (_sound != null && _sound.ActivityState == nBASS.State.Paused)
            if (CurrentQuizState == QuizState.Paused)
            {
                _soundPlayer.Resume();
            }
            else
            {
                CurrentQuizItem = null;
                _readyForNext = true;
                _timer.Enabled = true;
            }

           CurrentQuizState = QuizState.Playing;

        }

        public void ShowMeClicked()
        {
            CurrentQuizItem.Score = -1; // if you had to ask, we figure you don't know
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            //if (_sound == null || _sound.ActivityState == nBASS.State.Stopped)
            if (CurrentQuizState == QuizState.Playing)
            {
              //  if (UtteranceCollection.CurrentUtteranceSet != null && UtteranceCollection.CurrentUtteranceSet.Count > 0 && _readyForNext)
                if (_readyForNext)
                {
                    CurrentQuizItem = _questionChooser.PickNextQuizItem();
                    QuizOne((Utterance)CurrentQuizItem);
                }
                if (!_soundPlayer.Playing)
                {
                    _soundPlayer.PlayCurrentUtterace();
                }
            }
        }

        public int[] GetScores()
        {
            return _questionChooser.MakeScoresArray();
        }

        public void Reload()
        {
            CurrentQuizItem = _questionChooser.PickNextQuizItem();
        }

        public void Dispose()
        {
            _timer.Dispose();
            _soundPlayer.Dispose();
        }
    }
}
