using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Subachup
{
    public partial class ListenControl : SubachupTabControl
    {
        public ListenControl()
        {
            InitializeComponent();
        }

        public ListenControl(PropertyTable propertyTable)
            :base(propertyTable)
        {
            InitializeComponent();
        }

        private void _utteranceImageGrid_Clicked(object sender, EventArgs e)
        {
            ListViewItem item = _utteranceImageGrid.Grid.SelectedItems[0];
            string path = ((Utterance)item.Tag).FilePath;
            nBASS.Stream sound = _player.LoadStream(ResolveSoundPath(path));
            sound.Play(true, nBASS.StreamPlayFlags.Default);		
        }

        private void ListenControl_Load(object sender, EventArgs e)
        {
            Reload();
        }

        public override void Reload()
        {
            _utteranceImageGrid.LoadGrid();
        }

        override public bool PasteImage()
        {
            //ListViewItem item = _utteranceImageGrid.Grid.SelectedItems[0];
            //Utterance utterance = (Utterance)item.Tag;
           return  _utteranceImageGrid.PasteImage();
        }
    }
}
