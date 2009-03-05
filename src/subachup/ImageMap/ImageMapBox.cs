using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using subachup.utility;
using System.Linq;

namespace subachup
{
    /// <summary>
    /// This control shows a bitmap which has rectangular "answer regions".  It can detect when the user clicks on 
    /// one of these, and determine which answers were given (answers is plural here because one part
    /// of the picture could be the answer for multiple quiz items).  I can also draw momentary rectangles
    /// around the regions, to tell the user what the correct answer is.
    /// </summary>
    public partial class ImageMapBox : Panel, IAnswersControl
    {
        private SvgMapReader _map;
        public event subachup.utility.Proc<IEnumerable<string>> ClickedInOneOrMoreRegions;
        public event subachup.utility.Proc ClickedOutsideRegionAllRegions;
        public event Proc<IEnumerable<IQuizItem>> GaveAnAnswer;
        public IEnumerable<IQuizItem> QuizItems{get; set;}      

        private IEnumerable<IQuizItem> _quizItems;
        private List<Rectangle> _rectsToPointOut;
        private Timer _showMeTimer;
        private Pen _showMePen;

        public ImageMapBox()
        {
            components = new Container();
            _showMePen = new Pen(Color.Blue, 3);
            SetStyle(ControlStyles.DoubleBuffer,true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint,true);
       }


        public void Init(SvgMapReader map, IEnumerable<IQuizItem> quizItems)
        {
            _quizItems = quizItems;
            _map = map;

            this.MouseClick += new MouseEventHandler(OnMouseClick);
            _showMeTimer = new Timer();
            _showMeTimer.Interval = 2000;
            _showMeTimer.Tick+=new EventHandler(OnEndOfShowAnswerLocations);
        }


        public void ShowAnswerLocations(Utterance utterance)
        {
            _rectsToPointOut = new List<Rectangle>();
            _rectsToPointOut.AddRange(_map.GetRectsForUtterance(utterance.SubachupRegionId));
            _showMeTimer.Start();
            Refresh();
        }

        private void OnEndOfShowAnswerLocations(object sender, EventArgs e)
        {
            _showMeTimer.Stop();
            _rectsToPointOut = null;
            this.Refresh();
        }

        void OnMouseClick(object sender, MouseEventArgs e)
        {
            var regionsThatWereClickedIn = _map.GetRegionIds(e.X, e.Y);
 
            if (GaveAnAnswer != null)
            {
                //review... this is a kind of upside-down way to think abou this!  The
                // problem is that we have a many-to-many relationship between utterances and regions.
                //Currently this control has not idea of what the expected answer is
                List<IQuizItem> questionsForWhichThisClickIsAValidAnswer = new List<IQuizItem>();
                foreach (var regionId in regionsThatWereClickedIn)
                {
                    questionsForWhichThisClickIsAValidAnswer.Add(_quizItems.First(q=> q.SubachupRegionId == regionId));
                }
                GaveAnAnswer.Invoke(questionsForWhichThisClickIsAValidAnswer);
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var x = Bitmap.FromFile(_map.ImagePath);
            e.Graphics.DrawImage(x, 0,0, x.Width, x.Height);

           if (_rectsToPointOut != null)
            {
                foreach (var rectangle in _rectsToPointOut)
                {
                    e.Graphics.DrawRectangle(_showMePen, rectangle);
                }
            }

            base.OnPaint(e);
        }

        public void LoadContents()
        {
        }

    }
}
