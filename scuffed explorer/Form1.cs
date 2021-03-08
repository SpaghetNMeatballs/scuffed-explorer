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

namespace scuffed_explorer
{
    public partial class Form1 : Form
    {
        private Button[] directoryButtons;
        private Label[] directoryLabels;
        TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        public Form1()
        {
            InitializeComponent();
        }
        private void drawDirectory(String path) {
            groupBox1.Controls.Clear();
            textBox1.Text = (path);
            String[] childDirs = Directory.GetDirectories(path);
            String[] childFiles = Directory.GetFiles(path);
            int total = childDirs.Length + childFiles.Length;
            int xoffset = 20, yoffset = 30, buttonsize = 60, xcur=xoffset, ycur=yoffset;
            tableLayoutPanel1.ColumnCount = (int)(tableLayoutPanel1.Size.Width / buttonsize);
            tableLayoutPanel1.RowCount = (int)(tableLayoutPanel1.Size.Height / (buttonsize + 10 + 30));
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/ tableLayoutPanel1.ColumnCount));
            }
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / tableLayoutPanel1.RowCount));
            }
            directoryButtons = new Button[total];
            directoryLabels = new Label[total];
            for (int i = 0; i<childDirs.Length; i++)
            {
                directoryButtons[i] = new Button();
                directoryButtons[i].BackgroundImage = Image.FromFile("C:\\Users\\ghost\\source\\repos\\scuffed-explorer\\scuffed explorer\\icons\\directory_scuffed.png");
                directoryButtons[i].Tag = childDirs[i];
                directoryButtons[i].BackgroundImageLayout = ImageLayout.Stretch;
                this.directoryButtons[i].Location = new System.Drawing.Point(xcur, ycur);
                this.directoryButtons[i].Size = new System.Drawing.Size(buttonsize, buttonsize);
                this.directoryButtons[i].Click += button_click;
                this.directoryButtons[i].ContextMenuStrip = contextMenuStrip1;
                this.groupBox1.Controls.Add(directoryButtons[i]);

                directoryLabels[i] = new Label();
                this.directoryLabels[i].Location = new System.Drawing.Point(xcur, ycur+buttonsize);
                this.directoryLabels[i].Size = new System.Drawing.Size(buttonsize, 30);
                directoryLabels[i].Text = childDirs[i].Substring(path.Length);
                this.groupBox1.Controls.Add(directoryLabels[i]);


                xcur+=xoffset+buttonsize;
                if (xcur+buttonsize>=groupBox1.Size.Width)
                {
                    xcur=xoffset;
                    ycur+=yoffset+buttonsize;
                }
            }

            int totalDirs = childDirs.Length;
            for (int i = 0; i < childFiles.Length; i++)
            {
                directoryButtons[totalDirs + i] = new Button();
                directoryButtons[totalDirs + i].BackgroundImage = Image.FromFile("C:\\Users\\ghost\\source\\repos\\scuffed-explorer\\scuffed explorer\\icons\\file_scuffed.png");
                directoryButtons[totalDirs + i].BackgroundImageLayout = ImageLayout.Stretch;
                this.directoryButtons[totalDirs + i].Location = new System.Drawing.Point(xcur, ycur);
                this.directoryButtons[totalDirs + i].Size = new System.Drawing.Size(buttonsize, buttonsize);
                this.groupBox1.Controls.Add(directoryButtons[totalDirs + i]);

                directoryLabels[totalDirs + i] = new Label();
                this.directoryLabels[totalDirs + i].Location = new System.Drawing.Point(xcur, ycur+buttonsize);
                this.directoryLabels[totalDirs + i].Size = new System.Drawing.Size(buttonsize, 30);
                directoryLabels[totalDirs + i].Text = childFiles[i].Substring(path.Length);
                this.groupBox1.Controls.Add(directoryLabels[totalDirs + i]);


                xcur+=xoffset+buttonsize;
                if (xcur + buttonsize >= groupBox1.Size.Width)
                {
                    xcur=xoffset;
                    ycur += yoffset+buttonsize;
                }
            }
        }

        void button_click(object sender, EventArgs e)
        {
            drawDirectory(((sender as Button).Tag as String));
        }

        void deleteDir(object sender, EventArgs e)
        {
            ToolStripMenuItem mnu = sender as ToolStripMenuItem;
            ContextMenuStrip mcm = (ContextMenuStrip)mnu.GetCurrentParent();
            Button myButton = mcm.PlacementTarget as Button;
            drawDirectory(textBox1.Text);   
        }

        void contextMenuDrop(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Button proj = (Button)sender;
                proj.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void GoButtonClicked(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            drawDirectory(path);
        }

        private void moveUp(object sender, EventArgs e)
        {
            if (textBox1.Text.LastIndexOf('\\')==textBox1.Text.Length-1)
            {
                return;
            }
            string path = textBox1.Text.Substring(0, textBox1.Text.LastIndexOf('\\')+1);
            drawDirectory(path);
        }

        private void pageReload(object sender, MouseEventArgs e)
        {
            string path = textBox1.Text;
            drawDirectory(path);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            deleteToolStripMenuItem.Click += deleteDir;
            drawDirectory("C:\\");
        }
    }
}
