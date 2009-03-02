using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            _utteranceImageGrid.QuizItems = quizItems;
            _utteranceImageGrid.GaveAnAnswer += new subachup.utility.Proc<IEnumerable<IQuizItem>>(OnGaveAnAnswer);
        }

        void OnGaveAnAnswer(IEnumerable<IQuizItem> answers)
        {
            Debug.Assert(answers.Count() == 1);
            var u = (Utterance) answers.First();
            //nBASS.Stream sound = _player.LoadStream(ResolveSoundPath(path));
            _soundPlayer.SetCurrentUtterance(u.SoundPath);
            _soundPlayer.PlayCurrentUtterace();
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
