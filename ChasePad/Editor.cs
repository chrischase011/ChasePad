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

namespace ChasePad
{
    public partial class Editor : Form
    {
        #region Var
        private bool isFileHasContents;
        private bool isFileAlreadySaved;
        private string currentFileName;
        private FontDialog fontDialog = new FontDialog();
        private int check = 0;
        #endregion
        public Editor()
        {
            InitializeComponent();
        }
        private void Editor_Load(object sender, EventArgs e)
        {
            this.Text = "Untitled - ChasePad";
            isFileHasContents = false;
            isFileAlreadySaved = false;
            currentFileName = "";
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            newFile();
        }

        void newFile()
        {
            if (isFileHasContents)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "ChasePad", MessageBoxButtons.YesNo);
                switch (result)
                {
                    case DialogResult.Yes:
                        saveFile();
                    break;
                    case DialogResult.No:

                    break;
                }
            }

            clearRtxt();
            isFileAlreadySaved = false;
            currentFileName = "";
            
        }
        void saveFile()
        {
            if (isFileAlreadySaved)
            {
                if (Path.GetExtension(currentFileName) == ".txt" || Path.GetExtension(currentFileName) == ".html" || 
                    Path.GetExtension(currentFileName) == ".php" || Path.GetExtension(currentFileName) == ".js")
                {
                    rtBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                }
                isFileAlreadySaved = true;
                isFileHasContents = false;
                
            }
            else
            {
                if(isFileHasContents)
                {
                    saveAsFile();
                }
            }
            this.Text = this.Text.Replace("*", "");
        }
        void saveAsFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|HTML Files (*.html)|*.html|PHP Files (*.php)|*.php|" +
                "Javascript Files (*.js)|*.js|All Files (*.*)|*.*";
            DialogResult result = saveFileDialog.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    if (Path.GetExtension(currentFileName) == ".txt" || Path.GetExtension(currentFileName) == ".html" ||
                    Path.GetExtension(currentFileName) == ".php" || Path.GetExtension(currentFileName) == ".js")
                    {
                        rtBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                    }
                    isFileAlreadySaved = true;
                    isFileHasContents = false;
                    currentFileName = saveFileDialog.FileName;

                    this.Text = this.Text.Replace("*", "");
                    break;
            }

        }

        void openFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text Files (*.txt)|*.txt|HTML Files (*.html)|*.html|PHP Files (*.php)|*.php|" +
                "Javascript Files (*.js)|*.js|All Files (*.*)|*.*";
            DialogResult result = fileDialog.ShowDialog();

            switch(result)
            {
                case DialogResult.OK:

                    rtBox.LoadFile(fileDialog.FileName, RichTextBoxStreamType.PlainText);
                    isFileAlreadySaved = true;
                    isFileHasContents = false;
                    currentFileName = fileDialog.FileName;
                    // Comment
                    this.Text = fileDialog.FileName + " - ChasePad";
                    break;
            }
            
            
        }
        void clearRtxt()
        {
            rtBox.Clear();
            this.Text = "Untitled - ChasePad";
            isFileHasContents = false;
        }
        private void rtBox_TextChanged(object sender, EventArgs e)
        {
            isFileHasContents = true;
            this.Text = this.Text + "*";
            if(isFileAlreadySaved == false && rtBox.Text == "")
            {
                this.Text = this.Text.Replace("*", "");
            }
            if(this.Text.Contains("**"))
            {
                this.Text = this.Text.Replace("**", "*");
            }
            
        }

        

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }
    }
}
