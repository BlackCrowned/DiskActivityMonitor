using System;
using System.Windows.Forms;

namespace DiskUsageAnalizer
{
    using Microsoft.Win32;

    public partial class Form1 : Form
    {

        private IntPtr _rtlHandle;

        public Form1()
        {
            InitializeComponent();

            Ewt.genRTL(out _rtlHandle);
            Ewt.rtlStartTrace(_rtlHandle);

            Disposed += (sender, args) => Ewt.deleteRTL(ref _rtlHandle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ewt.rtlStartConsumption(_rtlHandle);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ewt.rtlStopConsumption(_rtlHandle);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
