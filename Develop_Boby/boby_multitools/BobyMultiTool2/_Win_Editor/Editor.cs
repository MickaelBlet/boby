using ScintillaNET;

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BobyMultitools
{
    public partial class Editor : Form
    {
        string file_path;
        bool current_text;

        public Editor(string file)
        {
            InitializeComponent();

            scintilla.Text = "using Aion_Game;\n\nnamespace BobyScript\n{\n    public class Script : IBobyScript\n    {\n        public void OnPlay()\n        {\n            ;\n        }\n        public void OnRun()\n        {\n            ;\n        }\n        public void OnStop()\n        {\n            ;\n        }\n    }\n}";

            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 11;
            scintilla.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            scintilla.CaretForeColor = Color.White;
            scintilla.SetSelectionBackColor(true, Color.FromArgb(73, 72, 62));

            scintilla.Styles[Style.Default].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Default].ForeColor = Color.FromArgb(248, 248, 242);

            scintilla.Styles[Style.LineNumber].BackColor = Color.FromArgb(45, 45, 45);
            scintilla.Styles[Style.LineNumber].ForeColor = Color.FromArgb(144, 144, 144);

            scintilla.Styles[Style.Cpp.Character].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Comment].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.CommentDoc].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.CommentDocKeyword].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.CommentDocKeywordError].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.CommentLine].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.CommentLineDoc].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Default].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.EscapeSequence].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.GlobalClass].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.HashQuotedString].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Identifier].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Number].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Operator].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Preprocessor].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.PreprocessorComment].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.PreprocessorCommentDoc].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Regex].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.String].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.StringEol].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.StringRaw].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.TaskMarker].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.TripleVerbatim].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.UserLiteral].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Uuid].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Verbatim].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Word].BackColor = Color.FromArgb(39, 40, 34);
            scintilla.Styles[Style.Cpp.Word2].BackColor = Color.FromArgb(39, 40, 34);

            scintilla.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(230, 219, 116);
            scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(155, 155, 155);
            scintilla.Styles[Style.Cpp.CommentDoc].ForeColor = Color.FromArgb(155, 155, 155);
            scintilla.Styles[Style.Cpp.CommentDocKeyword].ForeColor = Color.FromArgb(155, 155, 155);
            scintilla.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = Color.FromArgb(155, 155, 155);
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(155, 155, 155);
            scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(155, 155, 155);
            scintilla.Styles[Style.Cpp.Default].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.EscapeSequence].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.GlobalClass].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.HashQuotedString].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.Identifier].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.Number].ForeColor = Color.FromArgb(174, 129, 255);
            scintilla.Styles[Style.Cpp.Operator].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.Preprocessor].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.PreprocessorComment].ForeColor = Color.FromArgb(155, 155, 155);
            scintilla.Styles[Style.Cpp.PreprocessorCommentDoc].ForeColor = Color.FromArgb(155, 155, 155);
            scintilla.Styles[Style.Cpp.Regex].ForeColor = Color.FromArgb(230, 219, 116);
            scintilla.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(230, 219, 116);
            scintilla.Styles[Style.Cpp.StringEol].ForeColor = Color.FromArgb(230, 219, 116);
            scintilla.Styles[Style.Cpp.StringRaw].ForeColor = Color.FromArgb(230, 219, 116);
            scintilla.Styles[Style.Cpp.TaskMarker].ForeColor = Color.FromArgb(230, 219, 116);
            scintilla.Styles[Style.Cpp.TripleVerbatim].ForeColor = Color.FromArgb(230, 219, 116);
            scintilla.Styles[Style.Cpp.UserLiteral].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.Uuid].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.Word].ForeColor = Color.FromArgb(249, 38, 114);
            scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.FromArgb(102, 217, 239);

            scintilla.Styles[Style.Cpp.Word2].Italic = true;

            /*scintilla.Styles[Style.Cpp.Default].ForeColor = Color.FromArgb(248, 248, 242);
            scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla.Styles[Style.Cpp.Number].ForeColor = Color.FromArgb(174, 129, 255);
            scintilla.Styles[Style.Cpp.Word].ForeColor = Color.FromArgb(102, 217, 239);
            scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.FromArgb(249, 38, 114);
            scintilla.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.StringEol].ForeColor = Color.Pink;
            scintilla.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;*/

            scintilla.Lexer = Lexer.Cpp;

            // Set the keywords
            scintilla.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");

            var basic = "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void var";
            var boby = "Ability AbilityList Chat Dialog DialogList Entity EntityList Inventory InventoryList Force ForceList Player Skill SkillList Travel TravelList";

            scintilla.SetKeywords(1, basic + " " + boby);

            this.TopMost = true;

            file_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\scripts\" + file;

            StreamReader streamReader = new StreamReader(file_path);
            this.scintilla.Text = streamReader.ReadToEnd();
            streamReader.Close();
            this.current_text = true;

            this.Text = file;

            this.scintilla.AssignCmdKey(Keys.S | Keys.Control, Command.Null);

            this.Show();
        }

        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            show_line_number();

            if (!this.Text.EndsWith("*"))
            {
                this.Text += " *";
                this.current_text = false;
            }
        }

        private void scintilla_CharAdded(object sender, CharAddedEventArgs e)
        {
            // Find the word start
            var currentPos = scintilla.CurrentPosition;
            var wordStartPos = scintilla.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                var basic = "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while";
                var boby = "Ability AbilityList Chat Dialog DialogList Entity EntityList Inventory InventoryList Force ForceList Player Skill SkillList Travel TravelList";
                scintilla.AutoCShow(lenEntered, boby + " " + basic);
            }
            intellisense();
        }

        private int maxLineNumberCharLength;
        private void show_line_number()
        {
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 1;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void intellisense()
        {
            var currentPos = scintilla.CurrentPosition;
            var wordStartPos = scintilla.WordStartPosition(currentPos, true);

            while (scintilla.GetTextRange(wordStartPos - 1, 1) == ".")
                wordStartPos = scintilla.WordStartPosition(wordStartPos - 2, true);

            var lenEntered = currentPos - (wordStartPos - 1);
            if (lenEntered <= 0)
                return;

            var lastword = scintilla.GetTextRange(wordStartPos, currentPos - wordStartPos).Split('.')[0];

            List<string> list = new List<string>();

            switch (lastword)
            {
                case "Chat":
                    list.Add("Send");
                    list_autocompletion(lenEntered - 1, lastword, list);
                    break;
                case "Player":
                    list.Add("Group");
                    list.Add("Force");
                    foreach (var field in typeof(Aion_Game.Player).GetProperties())
                        list.Add(field.Name);
                    list_autocompletion(lenEntered - 1, lastword, list);
                    break;
                case "EntityList":
                    list.Add("GetEntity");
                    list_autocompletion(lenEntered - 1, lastword, list);
                    break;
            }
        }

        private void list_autocompletion(int index, string base_name, List<string> list)
        {
            string autocomplete = "";

            list.Sort();

            foreach (var elem in list)
            {
                if (elem != "")
                    autocomplete += base_name + "." + elem + " ";
            }

            autocomplete = autocomplete.TrimEnd();

            scintilla.AutoCShow(index, autocomplete);
        }

        private void HighlightWord(string list)
        {
            // Indicators 0-7 could be in use by a lexer
            // so we'll use indicator 8 to highlight words.
            const int NUM = 8;

            // Remove all uses of our indicator
            scintilla.IndicatorCurrent = NUM;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);

            // Update indicator appearance
            scintilla.Indicators[NUM].Style = IndicatorStyle.TextFore;
            scintilla.Indicators[NUM].ForeColor = Color.FromArgb(174, 129, 255);

            string[] list_array = list.Split(' ');
            // Search the document
            foreach (var text in list_array)
            {
                scintilla.TargetStart = 0;
                scintilla.TargetEnd = scintilla.TextLength;
                scintilla.SearchFlags = SearchFlags.None;
                while (scintilla.SearchInTarget(text) != -1)
                {
                    // Mark the search results with the current indicator
                    int lastpos = scintilla.TargetStart;
                    scintilla.IndicatorFillRange(scintilla.TargetStart, scintilla.TargetEnd - scintilla.TargetStart);

                    // Search the remainder of the document
                    scintilla.TargetStart = lastpos + 1;
                    scintilla.TargetEnd = scintilla.TextLength;
                }
            }
        }

        bool SaveFile()
        {
            try
            {
                if (this.Text.EndsWith("*"))
                {
                    StreamWriter streamWriter = new StreamWriter(this.file_path);
                    streamWriter.Write(this.scintilla.Text);
                    streamWriter.Close();
                }
            }
            catch
            {
                return false;
            }
            if (this.Text.EndsWith("*"))
            {
                this.Text = this.Text.Substring(0, this.Text.Length - 2);
                this.current_text = true;
            }
            return true;
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.current_text == false)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you wish to save the changes you made?", this.file_path.Substring(this.file_path.IndexOf("\\") + 1), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                    if (dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (dialogResult == DialogResult.Yes && !this.SaveFile())
                    {
                        e.Cancel = true;
                        return;
                    }
                    this.current_text = true;
                }
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                this.SaveFile();
            }
        }
    }
}
