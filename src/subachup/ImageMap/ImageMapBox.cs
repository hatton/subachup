using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BigMansStuff.LocusEffects;
using subachup.utility;
using System.Linq;

namespace subachup
{
    public partial class ImageMapBox : Panel, IAnswersControl
    {
        private SvgMapReader _map;
        private PictureBox _pictureBox;
        public event subachup.utility.Proc<IEnumerable<string>> ClickedInOneOrMoreRegions;
        public event subachup.utility.Proc ClickedOutsideRegionAllRegions;
        public event Proc<IEnumerable<IQuizItem>> GaveAnAnswer;
        public IEnumerable<IQuizItem> QuizItems{get; set;}      

        BigMansStuff.LocusEffects.LocusEffectsProvider _locusEffectsProvider;
        private IEnumerable<IQuizItem> _quizItems;

        public ImageMapBox()
        {
            // InitializeComponent();
            components = new Container();
            _locusEffectsProvider = new LocusEffectsProvider(this.components);
            _locusEffectsProvider.Initialize();
         }


        public void Init(SvgMapReader map, IEnumerable<IQuizItem> quizItems)
        {
            _quizItems = quizItems;
            _map = map;
            _pictureBox = new PictureBox();
            //nb: we don't use "zoom" yet because we have to figure out how to also zoom the hit regions
            //we can get the original image size and the current size of the box; if we figure out whether
            //it is the width or height that is the limitting factor, then we can determine the zoom %, then
            //on the other axis, we have to offset (because it is centered)
           _pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            _pictureBox.Dock = DockStyle.Fill;
            _pictureBox.Load(_map.ImagePath);
            _pictureBox.MouseClick += new MouseEventHandler(pictureBox_MouseClick);

            this.Controls.Add(_pictureBox);
        }

        void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var regionsThatWereClickedIn = _map.GetRegionIds(e.X, e.Y);
 
            if (regionsThatWereClickedIn.Count() == 0)
            {
//                if(ClickedOutsideRegionAllRegions !=null)
//                {
//                    ClickedOutsideRegionAllRegions.Invoke();
//                }
            }
            else if(ClickedInOneOrMoreRegions!=null)
            {
//                ClickedInOneOrMoreRegions.Invoke(regionsThatWereClickedIn);
            }
            else if (GaveAnAnswer != null)
            {
                //review... this is a kind of upside-down way to think abou this!  The
                // problem is that we have a many-to-many relationship between utterances and regions.
                //Currently this control has not idea of what the expected answer is
                List<IQuizItem> questionsForWhichThisClickIsAValidAnswer = new List<IQuizItem>();
                foreach (var regionId in regionsThatWereClickedIn)
                {
                    questionsForWhichThisClickIsAValidAnswer.Add(_quizItems.First(q=> q.SubachupRegion == regionId));
                }
                GaveAnAnswer.Invoke(questionsForWhichThisClickIsAValidAnswer);
            }

        }

        private void HighlightRegions(IEnumerable<string> regionIds)
        {
//            foreach (var id in regionIds)
//            {
//                var region = _map.GetRegion(id);
//                region = this.RectangleToScreen(region);
//
//                _locusEffectsProvider.StopActiveLocusEffect();//else, it crashes if there is another one!
//                _locusEffectsProvider.ShowLocusEffect(GetFormAncestor(), region, 
//                                                      BigMansStuff.LocusEffects.LocusEffectsProvider.DefaultLocusEffectBeacon);
//
//                break;//TODO currently we can't handle more than one, using this method (locuseffects crashes)
//                    
//            }
        }

        private Form GetFormAncestor()
        {
            var formAncestor = this.Parent;
            while(!(formAncestor is Form))
            {
                formAncestor = formAncestor.Parent;
            }
            return (Form)formAncestor;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ImageMapBox
            // 
            this.ResumeLayout(false);

        }


//        protected override void OnPaint(PaintEventArgs e)
//        {
//            base.OnPaint(e);
//            if (Map == null)
//                return;
//            using (var g = CreateGraphics())
//            {
//                foreach (var regionId in Map.GetRegionIds())
//                {
//                    var r = Map.GetRegion(regionId);
//                    double zoom = Map.ImageWidth / Width;
//
//                    ///TODO zoom algorithm
//                    r = new Rectangle((int)(r.Left * zoom), (int)(r.Top * zoom), (int)(r.Width * zoom), (int)(r.Height * zoom));
//                    g.FillRectangle(Brushes.Blue, r);
//                }
//            }
//        }
        public void LoadContents()//IEnumerable<IQuizItem> choices)
        {
        }

        public void PointOutUtterance(Utterance utterance)
        {
        }
    }
}
