using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Subachup
{
    public partial class SubachupTabControl : UserControl
    {
        protected nBASS.BASS _player;
        protected nBASS.Stream _sound;
        protected nBASS.Stream _correctSound;
        protected nBASS.Stream _wrongSound;
        protected PropertyTable _propertyTable;
        protected bool _pendingCurrentSetChangedEvent=true;

        public SubachupTabControl()
        {
            InitializeComponent();
        }

        public SubachupTabControl(PropertyTable propertyTable)
        {
            InitializeComponent();
            this.components = new System.ComponentModel.Container(); //don't know why designer didn't add that
            _propertyTable = propertyTable;
           // UtteranceCollection.CurrentUtteranceSet.Changed += new EventHandler(CurrentUtteranceSet_Changed);

        }

        protected void CurrentUtteranceSet_Changed(object sender, EventArgs e)
        {
            _pendingCurrentSetChangedEvent = true;
        }

        private void SetupCommonSounds()
        {
            string p = Application.StartupPath;
           // p = p.Replace(@"\bin\Debug", ""); //back out 2 levels if running in VS environment
            _correctSound = _player.LoadStream(Path.Combine(p, "correct.wav"));
            _wrongSound = _player.LoadStream(Path.Combine(p, "wrong.wav"));
            System.Diagnostics.Debug.Assert(_correctSound != null);
            System.Diagnostics.Debug.Assert(_wrongSound != null);
        }

        private void SetupPlayer()
        {
            if(DesignMode)
                return;

            try
            {
                this._player = new nBASS.BASS(this.components);
            }
            catch(Exception error)
            {
                MessageBox.Show("Could not start nbass player");
                Application.Exit();
            }


            ((System.ComponentModel.ISupportInitialize)(this._player)).BeginInit();

            this._player.Device = "Default";
            this._player.Frequency = 44100;
            this._player.MusicVolume = 100;
            this._player.ParentForm = this.ParentForm;
            this._player.SampleVolume = 100;
            this._player.SetupFlags = nBASS.DeviceSetupFlags.Default;
            this._player.StreamVolume = 50;
            ((System.ComponentModel.ISupportInitialize)(this._player)).EndInit();

            _player.Start();

            SetupCommonSounds();
        }
        			


        public string ResolveSoundPath(string soundPath)
        {
            return soundPath;
        }

        public virtual void Reload()
        {
        }
      
        public virtual void Showing()
		{
            if (_pendingCurrentSetChangedEvent)
            {
                Reload();
                _pendingCurrentSetChangedEvent = false;
            }
          //  UtteranceCollection.CurrentUtteranceSet.Changed += new EventHandler(CurrentUtteranceSet_Changed2);
		}

        void CurrentUtteranceSet_Changed2(object sender, EventArgs e)
        {
            Reload();
            _pendingCurrentSetChangedEvent = false;
       }

        public virtual bool Hiding()
		{
         //   UtteranceCollection.CurrentUtteranceSet.Changed -= new EventHandler(CurrentUtteranceSet_Changed2);
			return true;
		}

        private void SubachupTabControl_Load(object sender, EventArgs e)
        {
            SetupPlayer();
        }


        public virtual bool PasteImage()
        {
            return false;
        }
    
    }
}
