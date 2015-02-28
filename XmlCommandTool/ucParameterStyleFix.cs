using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace XmlCommandTool
{
    public partial class ucParameterStyleFix : UserControl
    {
        public ucParameterStyleFix()
        {
            InitializeComponent();
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDelegate(EventHandler eventHandler)
        {
            this.OnCheckedChangedEvent = (EventHandler)Delegate.Combine(this.OnCheckedChangedEvent, eventHandler);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDelegate(EventHandler eventHandler)
        {
            this.OnCheckedChangedEvent = (EventHandler)Delegate.Remove(this.OnCheckedChangedEvent, eventHandler);
        }

        public bool RadioIsChecked()
        {
            return this.rbtnNamed.Checked;
        }

        private void rbtnNamed_CheckedChanged(object sender, EventArgs e)
        {
            if (this.OnCheckedChangedEvent != null)
            {
                this.OnCheckedChangedEvent(sender, e);
            }
        }
    }
}
