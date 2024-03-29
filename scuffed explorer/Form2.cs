﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scuffed_explorer
{
    public partial class Form2 : Form
    {
        public Form1 parentForm;
        public string LabelText
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }

        public string TextboxInput
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void commit(object sender, EventArgs e)
        {
            parentForm.commitFlag = true;
            parentForm.tempString = this.textBox1.Text;
            this.Close();
        }

        private void close(object sender, EventArgs e)
        {
            parentForm.commitFlag = false;
            this.Close();
        }
    }
}
