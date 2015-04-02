using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;

namespace FastDBEngineGenerator
{
    public partial class SyntaxHighlighterControlFix : UserControl
    {
        private TextArea textArea;
        private string language;
        public SyntaxHighlighterControlFix()
        {
            this.InitializeComponent();
            if (!base.DesignMode)
            {
                this.textEditorControl1.Font = new Font("Courier New", 9.5f);
             //   this.SetReadOnly(true);
                this.SetLanguage(this.GetLanguage());
            }
            textArea = textEditorControl1.ActiveTextAreaControl.TextArea;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool hasSomethingSelected = true;//this.textEditorControl1.
            bool flag2 = this.IsReadOnly();
            this.menuUndo.Enabled = !flag2 && this.textEditorControl1.ActiveTextAreaControl.Document.UndoStack.CanUndo;
            this.menuRedo.Enabled = !flag2 && this.textEditorControl1.ActiveTextAreaControl.Document.UndoStack.CanRedo;
            this.menuCut.Enabled = !flag2 && hasSomethingSelected;
            this.menuCopy.Enabled = hasSomethingSelected;
            this.menuPaste.Enabled = !flag2 && Clipboard.ContainsText();
            this.menuDelete.Enabled = this.menuCut.Enabled;
        }

        private void menuCopy_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText(this.textEditorControl1.Text);
            textArea.AutoClearSelection = false;
            textArea.ClipboardHandler.Copy(sender, e);
        }

        private void menuCopyAll_Click(object sender, EventArgs e)
        {
            if (this.textEditorControl1.Text.Length > 0)
            {
                Clipboard.SetText(this.textEditorControl1.Text);
                //   this.textEditorControl1.
            }
        }

        private void menuCut_Click(object sender, EventArgs e)
        {
            //  Clipboard.SetText(this.textEditorControl1.Text);
            textArea.ClipboardHandler.Cut(sender, e);
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            // this.textEditorControl1.Clear();
            //  this.textEditorControl1.ExecuteAction(Keys.Delete);
            textArea.ClipboardHandler.Delete(sender, e);
        }

        private void menuFind_Click(object sender, EventArgs e)
        {
            // this.textEditorControl1.ShowSearchTextDialog();
        }

        private void menuPaste_Click(object sender, EventArgs e)
        {
            textArea.ClipboardHandler.Paste(sender, e);
            //this.textEditorControl1.ExecuteAction(Keys.Control | Keys.V);
        }

        private void menuRedo_Click(object sender, EventArgs e)
        {
            textEditorControl1.Redo();
            //this.textEditorControl1.ExecuteAction(Keys.Control | Keys.Y);
        }

        private void menuSelectAll_Click(object sender, EventArgs e)
        {
            // this.textEditorControl1.ExecuteAction(Keys.Control | Keys.A);
        }//

        private void menuUndo_Click(object sender, EventArgs e)
        {
            textEditorControl1.Undo();
            // this.textEditorControl1.ExecuteAction(Keys.Control | Keys.Z);
        }

        public void SetLanguage(string language)
        {
            if (this.language != language)
            {
                //  this.language = language;
                this.textEditorControl1.SetHighlighting(language);
            }
        }

        public string GetLanguage()
        {
            return (string.IsNullOrEmpty(this.language) ? "SQL" : this.language);
        }

        public void SetText(string text)
        {
            //this.textEditorControl1.Clear();
            this.textEditorControl1.Text = text;
        }

        public string GetText()
        {
            return this.textEditorControl1.Text;
        }

        public void SetTextEditorControlText(string text)
        {
            // this.textEditorControl1.Clear();
            this.textEditorControl1.Text = text;
        }

        public bool IsReadOnly()
        {
            return this.textEditorControl1.IsReadOnly;
        }

        public void SetReadOnly(bool readOnly)
        {
            this.textEditorControl1.IsReadOnly = readOnly;
        }
    }
}
