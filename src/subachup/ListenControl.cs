using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace subachup
{
    public partial class ListenControl : SubachupTabControl
    {
        private SoundPlayer _soundPlayer;

        public ListenControl(Form parentFormForPlayer)
        {
            InitializeComponent();
        }

        public ListenControl(PropertyTable propertyTable, Form parentFormForPlayer, IEnumerable<IQuizItem> quizItems)
            :base(propertyTable)
        {
            _soundPlayer = new SoundPlayer(parentFormForPlayer);
            InitializeComponent();
            _utteranceImageGrid.CurrentUtterances = quizItems;
        }

        private void _utteranceImageGrid_Clicked(object sender, EventArgs e)
        {
            if (_utteranceImageGrid.Grid.SelectedItems.Count ==0)
                return;
            ListViewItem item = _utteranceImageGrid.Grid.SelectedItems[0];
            string path = ((Utterance)item.Tag).SoundPath;
            //nBASS.Stream sound = _player.LoadStream(ResolveSoundPath(path));
            _soundPlayer.SetCurrentUtterance(path);
            _soundPlayer.PlayCurrentUtterace();
            //sound.Play(true, nBASS.StreamPlayFlags.Default);		
        }

        private void ListenControl_Load(object sender, EventArgs e)
        {
            Reload();
        }

        public override void Reload()
        {
            _utteranceImageGrid.LoadContents();
        }

        override public bool PasteImage()
        {
            //ListViewItem item = _utteranceImageGrid.Grid.SelectedItems[0];
            //Utterance utterance = (Utterance)item.Tag;
           return  _utteranceImageGrid.PasteImage();
        }
    }
}
