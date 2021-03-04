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

        private PictureBox[] directoryButtons;
        private Label[] directoryLabels;
        TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        public Form1()
        {
            InitializeComponent();
        }
        private void drawDirectory(String path) {
            textBox1.Text = (path);
            String[] childDirs = Directory.GetDirectories(path);
            String[] childFiles = Directory.GetFiles(path);
            int total = childDirs.Length + childFiles.Length;
            int xoffset = 1, yoffset = 1, buttonsize = 60;
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
            directoryButtons = new PictureBox[total];
            directoryLabels = new Label[total];
            for (int i = 0; i<childDirs.Length; i++)
            {
                directoryButtons[i] = new PictureBox();
                directoryButtons[i].Image = Image.FromFile("C:\\Users\\ghost\\source\\repos\\scuffed explorer\\scuffed explorer\\icons\\directory_scuffed.png");
                directoryButtons[i].Tag = childDirs[i];
                this.directoryButtons[i].Location = new System.Drawing.Point(0, 0);
                this.directoryButtons[i].Size = new System.Drawing.Size(buttonsize, buttonsize);
                this.directoryButtons[i].Click += button_click;
                this.tableLayoutPanel1.Controls.Add(directoryButtons[i], xoffset, yoffset);

                directoryLabels[i] = new Label();
                this.directoryLabels[i].Location = new System.Drawing.Point(0, buttonsize);
                this.directoryLabels[i].Size = new System.Drawing.Size(buttonsize, yoffset - 10);
                directoryLabels[i].Text = childDirs[i].Substring(path.Length+1);
                this.tableLayoutPanel1.Controls.Add(directoryLabels[i], xoffset, yoffset);


                xoffset++;
                if (xoffset==tableLayoutPanel1.ColumnCount)
                {
                    xoffset=1;
                    yoffset++;
                }
            }

            int totalDirs = childDirs.Length;
            for (int i = 0; i < childFiles.Length; i++)
            {
                directoryButtons[totalDirs + i] = new PictureBox();
                directoryButtons[totalDirs + i].Image = Image.FromFile("C:\\Users\\ghost\\source\\repos\\scuffed explorer\\scuffed explorer\\icons\\file_scuffed.png");
                this.directoryButtons[totalDirs + i].Location = new System.Drawing.Point(0, 0);
                this.directoryButtons[totalDirs + i].Size = new System.Drawing.Size(buttonsize, buttonsize);
                this.tableLayoutPanel1.Controls.Add(directoryButtons[totalDirs + i], xoffset, yoffset);

                directoryLabels[totalDirs + i] = new Label();
                this.directoryLabels[totalDirs + i].Location = new System.Drawing.Point(0, buttonsize);
                this.directoryLabels[totalDirs + i].Size = new System.Drawing.Size(buttonsize, 30);
                directoryLabels[totalDirs + i].Text = childFiles[i].Substring(path.Length+1);
                this.tableLayoutPanel1.Controls.Add(directoryLabels[totalDirs + i], xoffset, yoffset);


                xoffset++;
                if (xoffset == tableLayoutPanel1.ColumnCount)
                {
                    xoffset=1;
                    yoffset++;
                }
            }
        }

        void button_click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            drawDirectory(((sender as Button).Tag as String));
        }

        private void GoButtonClicked(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            drawDirectory(path);
        }

        private void moveUp(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            drawDirectory(path);
        }

        private void pageReload(object sender, MouseEventArgs e)
        {
            string path = textBox1.Text;
            drawDirectory(path);
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            drawDirectory("C:\\");
        }
    }
}
