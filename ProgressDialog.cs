﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskUsageAnalizer
{
    using System.Threading;

    public partial class ProgressDialog : Form
    {
        public ProgressDialog(string title)
        {
            InitializeComponent();
            Text = title;
        }
    }
}