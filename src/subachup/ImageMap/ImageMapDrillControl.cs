using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace subachup
{
    public partial class ImageMapDrillControl : SubachupTabControl
    {
        private readonly LiftProject _project;
        private readonly UtteranceCollection _utterances;
        private SvgMapReader _map;

        public ImageMapDrillControl(string svgPath, LiftProject project):base(new PropertyTable())
        {
            _project = project;
           InitializeComponent();

            _map = new SvgMapReader(svgPath);
            _imageBox.ClickedInOneOrMoreRegions += OnClickedInOneOrMoreRegions;
            _imageBox.ClickedOutsideRegionAllRegions += new subachup.utility.Proc(OnClickedOutsideRegionAllRegions);

            _utterances = new UtteranceCollection(project);
            _utterances.Load(_map);
        }

        void OnClickedOutsideRegionAllRegions()
        {
           
        }

        void OnClickedInOneOrMoreRegions(IEnumerable<string> regionIds)
        {
            
        }


        private void ImageMapDrillControl_Load(object sender, EventArgs e)
        {
            _imageBox.Init(_map, _utterances);
        }

        private void _imageBox_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
