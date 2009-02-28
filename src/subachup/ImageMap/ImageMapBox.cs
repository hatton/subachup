using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BigMansStuff.LocusEffects;

namespace subachup
{
    public partial class ImageMapBox : Panel
    {
        private SvgMapReader _map;
        private PictureBox _pictureBox;
        public event subachup.utility.Proc<IEnumerable<string>> ClickedInOneOrMoreRegions;
        public event subachup.utility.Proc ClickedOutsideRegionAllRegions;

        BigMansStuff.LocusEffects.LocusEffectsProvider _locusEffectsProvider;
        public ImageMapBox()
        {
            // InitializeComponent();
            components = new Container();
            _locusEffectsProvider = new LocusEffectsProvider(this.components);
            _locusEffectsProvider.Initialize();
         }


        public void SetMap(SvgMapReader map)
        {
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
            var regionsThatWereClickedIn = new List<string>();
            foreach (var regionId in _map.GetRegionIds())
            {
                var region = _map.GetRegion(regionId);
                if (region.Contains(e.X, e.Y))
                {
                    regionsThatWereClickedIn.Add(regionId);
                    break;
                }
            }

            if (regionsThatWereClickedIn.Count == 0)
            {
                if(ClickedOutsideRegionAllRegions !=null)
                {
                    ClickedOutsideRegionAllRegions.Invoke();
                }
            }
            else if(ClickedInOneOrMoreRegions!=null)
            {
                ClickedInOneOrMoreRegions.Invoke(regionsThatWereClickedIn);
            }


        }

        private void HighlightRegions(IEnumerable<string> regionIds)
        {
            foreach (var id in regionIds)
            {
                var region = _map.GetRegion(id);
                region = this.RectangleToScreen(region);

                _locusEffectsProvider.StopActiveLocusEffect();//else, it crashes if there is another one!
                _locusEffectsProvider.ShowLocusEffect(GetFormAncestor(), region, 
                                                      BigMansStuff.LocusEffects.LocusEffectsProvider.DefaultLocusEffectBeacon);

                break;//TODO currently we can't handle more than one, using this method (locuseffects crashes)
                    
            }
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
    }
}
