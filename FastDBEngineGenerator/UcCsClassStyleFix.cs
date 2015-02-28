using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace FastDBEngineGenerator
{
    public partial class UcCsClassStyleFix : UserControl
    {
        public UcCsClassStyleFix()
    {
        this.InitializeComponent();
    }

    private void chkSortByName_CheckedChanged(object sender, EventArgs e)
    {
        if (this.OnchkSortByNameCheckedChanged != null)
        {
            this.OnchkSortByNameCheckedChanged(sender, e);
        }
    } 

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddEvent(EventHandler eventHandler_1)
    {
        this.OnchkSortByNameCheckedChanged = (EventHandler) Delegate.Combine(this.OnchkSortByNameCheckedChanged, eventHandler_1);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void RemoveEvent(EventHandler eventHandler_1)
    {
        this.OnchkSortByNameCheckedChanged = (EventHandler) Delegate.Remove(this.OnchkSortByNameCheckedChanged, eventHandler_1);
    }

    public CsClassStyle GetCsClassStyle()
    {
        return new CsClassStyle { MemberStyle = this.radioButton1.Checked ? CsClassMemberStyle.Field : (this.radioButton2.Checked ? CsClassMemberStyle.AutoProperty : CsClassMemberStyle.ClassicProperty), SupportWCF = this.chkWCF.Checked, SortByName = this.chkSortByName.Checked };
    }
    }
}
