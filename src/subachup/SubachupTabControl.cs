using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace subachup
{
    public partial class SubachupTabControl : UserControl
    {
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


        public virtual bool Hiding()
		{
         //   UtteranceCollection.CurrentUtteranceSet.Changed -= new EventHandler(CurrentUtteranceSet_Changed2);
			return true;
		}


        public virtual bool PasteImage()
        {
            return false;
        }
    
    }
}
