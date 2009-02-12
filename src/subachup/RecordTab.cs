using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using nBASS;

namespace Subachup
{
    public partial class RecordTab : SubachupTabControl
    {
        nBASS.Record _recorder;

        public RecordTab()
        {
            InitializeComponent();
        }

        public RecordTab(PropertyTable propertyTable)
            :base(propertyTable)
        {
            InitializeComponent();
        }

        static private WMAEncoder enc;


        private static int RecordCallback(IntPtr pbuffer, int length, int user)
        {
            short[] buffer = new short[length];
            //Trace.WriteLine(pbuffer, "IntPtr pbuffer");
            System.Runtime.InteropServices.Marshal.Copy(pbuffer, buffer, 0, length / System.Runtime.InteropServices.Marshal.SizeOf(typeof(short)));
            try
            {
                //Trace.WriteLine(buffer.Length, "Buffer length");
                //Trace.WriteLine(length, "Callback length");
                enc.Write(buffer, length);
            }
            catch (BASSException )
            {
//                Trace.WriteLine(ex.Message, "Record callback exception");
                enc.Close();
                return 0;
            }
            //Trace.WriteLine(++COUNT, "caLLBACK count");
            return 1;
        }

        private void btnRecord_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //			_recorder = new Record(-1);
            //
            //			for (int i = 0; i < _recorder.InputCount; i++)
            //			{
            //			Debug.WriteLine(_recorder.GetInputName(i));
            //			}

            int[] rates = WMAEncoder.GetRates(44100, RateFlags.Default);

            //			enc = WMAEncoder.OpenEncoderFile(44100, 
            //				EncoderFlags.Mono,rates[0],@"C:\testing.wma");
            //
            //			enc.SetTag(WMATag.Author, "leppie");
            //			enc.SetTagDone();

            //record.SetInput(this.comboBox2.SelectedIndex, InputFlags.ON); //mic is 4, on mine anyways :)

            _recorder = new nBASS.Record(0);
            _recorder.Start(44100, RecordFlags.Mono, new nBASS.GetRecordCallBack(RecordTab.RecordCallback), 0);
        }

        private void btnStopRecording_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            _recorder.Stop();
          //  UpdateButtons();
        }
    }
}
