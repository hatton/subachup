using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;
using nBASS;

namespace subachup
{
    public class SoundPlayer :IDisposable
    {
        protected nBASS.BASS _player;
        protected nBASS.Stream _sound;
        protected nBASS.Stream _correctSound;
        protected nBASS.Stream _wrongSound;
        private Container components;

        public SoundPlayer(Form parentFormForPlayer)
        {
            components = new Container();
            SetupPlayer(parentFormForPlayer);
        }

        public bool Playing
        {
            get { return _sound != null && _sound.ActivityState == nBASS.State.Playing; }
        }

        private void SetupCommonSounds()
        {
            string p = Application.StartupPath;
            // p = p.Replace(@"\bin\Debug", ""); //back out 2 levels if running in VS environment
            _correctSound = _player.LoadStream(Path.Combine(p, Path.Combine("sound", "correct.wav")));
            _wrongSound = _player.LoadStream(Path.Combine(p, Path.Combine("sound", "wrong.wav")));
            System.Diagnostics.Debug.Assert(_correctSound != null);
            System.Diagnostics.Debug.Assert(_wrongSound != null);
            _player = new BASS();
        }

        private void SetupPlayer(Form parentForm)
        {

            try
            {
                this._player = new nBASS.BASS(this.components);
            }
            catch (Exception error)
            {
                MessageBox.Show("Could not start nbass player");
                Application.Exit();
            }


            ((System.ComponentModel.ISupportInitialize)(this._player)).BeginInit();

            this._player.Device = "Default";
            this._player.Frequency = 44100;
            this._player.MusicVolume = 100;
            this._player.ParentForm = parentForm;
            this._player.SampleVolume = 100;
            this._player.SetupFlags = nBASS.DeviceSetupFlags.Default;
            this._player.StreamVolume = 50;
            ((System.ComponentModel.ISupportInitialize)(this._player)).EndInit();

            _player.Start();

            SetupCommonSounds();
        }




        public void Dispose()
        {
            _player.Dispose();
        }

        public void PlayCorrectAnswerChime()
        {
            _correctSound.Play(true, nBASS.StreamPlayFlags.Default);

        }

        public void PlayWrongAnswerChime()
        {
            _wrongSound.Play(true, nBASS.StreamPlayFlags.Default);

        }

        public void Pause()
        {
            _sound.Pause();

        }

        public void Stop()
        {
            if (_sound != null)
                _sound.Stop();
        }

        public void Resume()
        {
            _sound.Resume();

        }

        public void PlayCurrentUtterace()
        {
            if (_sound != null)
                _sound.Play(true, nBASS.StreamPlayFlags.Default);

        }

        public void SetCurrentUtterance(string absolutePath)
        {
           _sound = _player.LoadStream(absolutePath);
        }
    }
}
