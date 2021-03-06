﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using subachup.utility;

namespace subachup.Core
{
    public partial class UtteranceImageGrid : UserControl, IAnswersControl
    {
        public event System.EventHandler Clicked;
        public event Proc<IEnumerable<IQuizItem>> GaveAnAnswer;
        public IEnumerable<IQuizItem> QuizItems
        {
            get; set;
        }

        public UtteranceImageGrid()
        {
            InitializeComponent();
            _locusEffectsProvider.Initialize();
        }

        private void _imageGrid_Click(object sender, System.EventArgs e)
        {
            //Clicked.Invoke(this, e);
            if(GaveAnAnswer !=null)
            {
                if (Grid.SelectedItems.Count == 0)
                    return;
    
                ListViewItem item = Grid.SelectedItems[0];
                GaveAnAnswer.Invoke(new List<IQuizItem> {(Utterance) item.Tag});
            }
        }

        public ListView Grid
        {
            get
            {
                return _imageGrid;
            }
        }

        public void ClearSelectedItems()
        {
            _imageGrid.SelectedItems.Clear();
        }

        public void LoadContents()
        {
            _imageGrid.SuspendLayout();
            _imageList.Images.Clear();
            _imageGrid.Items.Clear();
            foreach (Utterance utterance in QuizItems)
            {
                if (utterance != null)//hack... bug in there
                    AddImageItem(utterance);
            }

            Shuffle();
            //			AddFromWordsList();
            _imageGrid.ResumeLayout(true);
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            _imageGrid.SuspendLayout();
            for (int i = 0; i < _imageGrid.Items.Count; i++)
            {
                ListViewItem item = _imageGrid.Items[i];
                _imageGrid.Items.RemoveAt(i);
                _imageGrid.Items.Insert(rnd.Next(0, _imageGrid.Items.Count - 1), item);
            }
            _imageGrid.ResumeLayout(true);

        }
        public void AddImageItem(Utterance utterance)
        {
            //prevent duplicates
            //			if(_names.Contains(name))
            //				return;
            //			_names.Add(name);


            Image thumb = utterance.GetThumbNail(_imageList.ImageSize.Width, _imageList.ImageSize.Height);// GetMatchingImage(imagesDirectory, utterance);
            ListViewItem item = new ListViewItem(utterance.Gloss);
            item.Tag = utterance;

            if (thumb == null)
            {
            }
            else
            {
                _imageList.Images.Add(thumb);
                item.ImageIndex = _imageList.Images.Count - 1;
                //???? should we ??? thumb.Dispose();
            }
            _imageGrid.Items.Add(item);
            //if (image != null)
            //    image.Dispose();
        }



        public bool PasteImage()
        {
            if ((!Clipboard.ContainsImage()) || (_imageGrid.SelectedItems.Count == 0))
                return false;
            ReplaceImage(_imageGrid.SelectedItems[0], Clipboard.GetImage());
            return true;
        }

        private void ReplaceImage(ListViewItem item, Image image)
        {
            ((Utterance)item.Tag).TheImage = image;
            LoadContents();
        }

        public ListView.SelectedListViewItemCollection SelectedItems
        {
            get
            {
                return _imageGrid.SelectedItems;
            }
        }

        protected ListViewItem FindUtterance(Utterance utterance)
        {
            foreach (ListViewItem i in _imageGrid.Items)
            {
                if (i.Tag == utterance)
                    return i;
            }
            throw new ApplicationException();
        }

        public void ShowAnswerLocations(Utterance utterance)
        {
            ListViewItem item = FindUtterance(utterance);
            item.EnsureVisible();

            Point center = new Point(item.Bounds.Left + (item.Bounds.Width / 2),
                item.Bounds.Top + (item.Bounds.Height / 2));

            _locusEffectsProvider.ShowLocusEffect(this.ParentForm,
                _imageGrid.PointToScreen(center),
                BigMansStuff.LocusEffects.LocusEffectsProvider.DefaultLocusEffectArrow);

        }

        /*  The following is from an earlier (personal) version of Subachup,
         * in which I could add images directly to the database via
         * drag n' drop or querying google images from within subachup.
         * We don't have this functionality at the moment, but some of it is 
         * useful code should we ever bring this back
         */
        /*private void handleFileDropTimer_Tick(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Assert(_fileToDrop != null);
            System.Diagnostics.Debug.Assert(_itemDroppedOn != null);

            try
            {
                Image img = Image.FromFile(_fileToDrop);
                ReplaceImage(_itemDroppedOn, img);
            }
            catch
            {
                return; // try later
            }

            //success
            handleFileDropTimer.Enabled = false;
            _itemDroppedOn = null;
            _fileToDrop = null;

        }

        private void _imageGrid_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point cp = _imageGrid.PointToClient(new Point(e.X, e.Y));
            _itemDroppedOn = _imageGrid.GetItemAt(cp.X, cp.Y);
            if (_itemDroppedOn == null)
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
                return;

            //At least from FireFox, the file is actually still empty
            //so we'll get it later
            _fileToDrop = files[0];
            handleFileDropTimer.Enabled = true;

        }
        private void _imageGrid_DoubleClick(object sender, System.EventArgs e)
        {
            ListViewItem item = _imageGrid.SelectedItems[0];
            Ilan.Test.Google.API.ChooseGoogleDialog d = new Ilan.Test.Google.API.ChooseGoogleDialog();
            d.txtQuery.Text = item.Text + " clipart";
            if (DialogResult.Cancel == d.ShowDialog())
                return;
            Image image = d.GetImage(d._result.ThumbnailUrl);// d.GetImage(d._result.ImageUrl);
            if (image == null)
                MessageBox.Show("Could not get that image.");
            else
                ReplaceImage(item, image);
        }

                 private void _imageGrid_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
                e.Effect = DragDropEffects.Copy;
        }
        */
    }
}
