using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace RegexTest
{
    public partial class MainForm : Form
    {
        StreamReader sr;
        Regex r;

        private List<string> THMsgTypeStrings = new List<string>()
        {
            "1001", 
            "1002",
            "1003",
            "1004",
            "1011",
            "9001",
            "9002",
            "9003",
            "9004",
            "9011",
        };

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileNameTextBox.Text = openFileDialog1.FileName;
//                LogTextBox.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                NextButton.Enabled = true;
                LoadButton.Enabled = false;
                sr = new StreamReader(FileNameTextBox.Text);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < THMsgTypeStrings.Count(); i++)
                {
                    sb.Append(THMsgTypeStrings[i]);
                    sb.Append("|");
                }
                //                        var pattern = "( |:|\\n)" + sb.ToString().TrimEnd('|') + "([^|\\|]+)";
                string pattern = "(?<![A-F0-9])(" + sb.ToString().TrimEnd('|') + ")";
                r = new Regex(pattern);

            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            string line = sr.ReadLine();
            if (line != null){
                SelectedLineTextBox.Text = line;
            }
            Match m = r.Match(line);
            if (m.Success)
            {
                string matchVal = m.Value;
                int indexOf = line.IndexOf(matchVal);
                Match fullMatch = Regex.Match(line.Substring(indexOf), @"\w+\b");
                if (fullMatch.Length > 4)
                    StatusTextBox.Text = "Match: " + matchVal + "  " + fullMatch.Value;
                else
                    StatusTextBox.Clear();
            }
            else
            {
                StatusTextBox.Clear();            
            }
        }


    }
}
