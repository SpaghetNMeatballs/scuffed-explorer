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
using System.Threading;

namespace scuffed_explorer
{
    public partial class Form1 : Form
    {
        public bool commitFlag;
        public String tempString;
        private Button[] directoryButtons;
        private Label[] directoryLabels;
        private ContextMenuStrip[] menus;
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

            menus = new ContextMenuStrip[childDirs.Length];
            directoryButtons = new Button[total];
            directoryLabels = new Label[total];
            for (int i = 0; i < childDirs.Length; i++)
            {
                menus[i] = new ContextMenuStrip();
                ToolStripMenuItem fruitToolStripMenuItem = new ToolStripMenuItem()
                {
                    Text = "Move",
                    Name = "Move"
                };
                menus[i].Items.Add(fruitToolStripMenuItem);
                ToolStripMenuItem renamebutton = new ToolStripMenuItem()
                {
                    Text = "Rename",
                    Name = "Rename"
                };
                menus[i].Items.Add(renamebutton);
                ToolStripMenuItem copybutton = new ToolStripMenuItem()
                {
                    Text = "Copy",
                    Name = "Copy"
                };
                menus[i].Items.Add(copybutton);
                ToolStripMenuItem deletebutton = new ToolStripMenuItem()
                {
                    Text = "Delete",
                    Name = "Delete"
                };
                menus[i].Items.Add(deletebutton);
                menus[i].ItemClicked += TimerTick;

                directoryButtons[i] = new Button();
                directoryButtons[i].BackgroundImage = Image.FromFile("..\\..\\icons\\directory_scuffed.png");
                directoryButtons[i].Tag = childDirs[i];
                directoryButtons[i].BackgroundImageLayout = ImageLayout.Stretch;
                this.directoryButtons[i].Location = new System.Drawing.Point(xcur, ycur);
                this.directoryButtons[i].Size = new System.Drawing.Size(buttonsize, buttonsize);
                this.directoryButtons[i].Click += button_click;
                this.directoryButtons[i].ContextMenuStrip = menus[i];

                directoryLabels[i] = new Label();
                this.directoryLabels[i].Location = new System.Drawing.Point(xcur, ycur+buttonsize);
                this.directoryLabels[i].Size = new System.Drawing.Size(buttonsize, 30);
                directoryLabels[i].Text = childDirs[i].Substring(path.Length);

                this.groupBox1.Controls.Add(directoryButtons[i]);
                this.groupBox1.Controls.Add(directoryLabels[i]);


                xcur +=xoffset+buttonsize;
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
                directoryButtons[totalDirs + i].BackgroundImage = Image.FromFile("..\\..\\icons\\file_scuffed.png");
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

        async void TimerTick(object sender, ToolStripItemClickedEventArgs e)
        {
            System.Windows.Forms.Timer driveTimer = new System.Windows.Forms.Timer();
            driveTimer.Interval = 100;
            driveTimer.Tick += new EventHandler(kekw);
            driveTimer.Start();
            await Task.Run(() => operateMenu(sender, e));
        }

        void kekw(object sender, EventArgs e)
        {
            this.label1.Text = (int.Parse(this.label1.Text)+1).ToString();
        }


        void operateMenu(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip item = sender as ContextMenuStrip;
            string path = textBox1.Text;
            if (item != null && e.ClickedItem.Text == "Delete")
            {
                Thread.Sleep(10000000);
                String toDelete = item.SourceControl.Tag as String;
                Directory.Delete(toDelete, true);
            }
            if (item != null && e.ClickedItem.Text == "Rename")
            {
                String toRename = item.SourceControl.Tag as String;
                Form2 dialog = new Form2();
                dialog.parentForm = this;
                dialog.LabelText = "Rename field";
                dialog.TextboxInput = toRename;
                dialog.ShowDialog();
                if (this.commitFlag)
                {
                    Directory.Move(toRename, tempString);
                }
            }
            if (item != null && e.ClickedItem.Text == "Move")
            {
                String toMove = item.SourceControl.Tag as String;
                Form2 dialog = new Form2();
                dialog.LabelText = "Move field";
                dialog.TextboxInput = toMove;
                dialog.ShowDialog();
                if (this.commitFlag)
                {
                    Directory.Move(toMove, this.tempString);
                }
            }
            drawDirectory(path);
            
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
            string path = textBox1.Text.Substring(0, textBox1.Text.LastIndexOf('\\'));
            if (path[path.Length - 1] == ':')
            {
                path += "\\";
            }
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
