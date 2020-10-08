using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Hash_function
{
    public partial class Form1 : Form
    {
        List<string> timeScores=new List<string>();
        public Form1()
        {
            InitializeComponent();
            //grpHashMethods.Visible = false;
            radioButton1.Checked = true;
            txtTDESKey.Enabled = false;
        }

        private void btnEncodeStr_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;
            string outputText = DetermineEncryptionMethodString(inputText);
            timeScores.Add(Cryptography.timeScores);
            timeScores.Sort();
            textBox4.Text = "";
            foreach (string item in timeScores)
            {
                textBox4.AppendText(item + "\r\n");
            }
            Cryptography.timeScores = "";
            textBox3.Text = outputText;
            
        }

        private void btnEncodeFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFile.Text))
                return;

            //chech if file is available
            if (File.Exists(txtFile.Text))
            {
                //compute hash
                string outputText = DetermineEncryptionMethodFile(txtFile.Text);
                //save answer
                textBox3.Text = outputText;
                timeScores.Add(Cryptography.timeScores);
                timeScores.Sort();
                textBox4.Text = "";
                foreach (string item in timeScores)
                {
                    textBox4.AppendText(item + "\r\n");
                }
                Cryptography.timeScores = "";
            }
            else
            {
                MessageBox.Show("File does not exist");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
        public string DetermineEncryptionMethodString(string inputString)
        {
            if(radioButton1.Checked)
            {
                return Cryptography.ArturoHash(inputString);
            }
            else if(radioButton2.Checked)
            {
                return Cryptography.TextToMD5(inputString);
            }
            else if(radioButton3.Checked)
            {
                return Cryptography.TextToSHA256(inputString);
            }
            else if(radioButton4.Checked)
            {
                return Cryptography.TDESencodeString(inputString, txtTDESKey.Text);
            }
            else if(radioButton5.Checked)
            {
                return Cryptography.RSAencodeString(inputString);
            }
            return "";
        }
        public string DetermineEncryptionMethodFile(string inFile)
        {
            if (radioButton1.Checked)
            {
                return Cryptography.ArturoHash(inFile);
            }
            else if (radioButton2.Checked)
            {
                return Cryptography.MD5File(inFile);
            }
            else if (radioButton3.Checked)
            {
                return Cryptography.SHA256File(inFile);
            }
            else if (radioButton4.Checked)
            {
                return Cryptography.TDESencodeFile(inFile, txtTDESKey.Text);
            }
            else if (radioButton5.Checked)
            {
                MessageBox.Show("does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton4.Checked)
            {
                txtTDESKey.Enabled = true;
            }
            else
            {
                txtTDESKey.Enabled = false;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //clear textbox
            txtFile.Clear();

            //open openfiledialog
            var opf = new OpenFileDialog()
            {
                Multiselect = false
            };

            //if OK save file name
            if (opf.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = opf.FileName;
            }
        }
    }
}
