using System;
using System.Windows.Forms;

namespace DiskUsageAnalizer
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Timers;

    using Timer = System.Timers.Timer;

    public partial class Form1 : Form
    {

        private IntPtr _rtlHandle;

        private UInt64 _totalBytesRead;

        private UInt64 _totalBytesWritten;

        private TimeSpan _totalTimeRunning;

        private Timer _updateTotalTimeRunningTimer;

        private Timer _updateListViewTimer;

        private List<ListViewItem> _itemBuffer;

        private static string _filePath;

        public Form1()
        {
            InitializeComponent();

            _filePath = Properties.Settings.Default._filePath;

            _totalBytesRead = 0;
            _totalBytesWritten = 0;
            _totalTimeRunning = TimeSpan.Zero;

            _itemBuffer = new List<ListViewItem>();

            if (!string.IsNullOrEmpty(_filePath) && File.Exists(_filePath))
            {
                RestoreFromFile(new FileStream(_filePath, FileMode.Open));
            }


            _updateListViewTimer = new Timer(500);
            _updateListViewTimer.AutoReset = true;
            _updateListViewTimer.Elapsed += UpdateListView;
            _updateListViewTimer.Start();

            ConsumerClass.EventReceived += HandleDiskEvents;

            Disposed += OnDisposed;
        }

        private void UpdateListView(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                if (!IsDisposed && !Disposing)
                    listView1.Invoke(
                                     new MethodInvoker(
                                         () =>
                                         {
                                             lock (_itemBuffer)
                                             {
                                                 if (_itemBuffer.Count > 0)
                                                 {
                                                     listView1.Items.AddRange(_itemBuffer.ToArray());
                                                     if (WindowState != FormWindowState.Minimized) //Prevent NullReferenceException
                                                         listView1.TopItem = _itemBuffer.Last();
                                                     _itemBuffer.Clear();
                                                 }
                                             }
                                         }));
            }
            catch (ObjectDisposedException)
            {
                //Ignore
            }
        }

        protected override void OnClosing(CancelEventArgs eventArgs)
        {
            Save();
            base.OnClosing(eventArgs);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Ewt.genRTL(out _rtlHandle);
            if (Ewt.rtlStartTrace(_rtlHandle) != 0)
            {
                var result = MessageBox.Show("Failed to open event trace.", "Critical Failure", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    //Ewt.deleteRTL(ref _rtlHandle);
                    Program.ResetForm1();
                }
            }
        }

        private void OnDisposed(object sender, EventArgs eventArgs)
        {
            Properties.Settings.Default._filePath = _filePath;
            Properties.Settings.Default.Form1_ClientSize = ClientSize;
            Properties.Settings.Default.Form1_Location = Location;
            Properties.Settings.Default.groupBox1_Location = groupBox1.Location;
            Properties.Settings.Default.groupBox1_Size = groupBox1.Size;
            Properties.Settings.Default.groupBox2_Location = groupBox2.Location;
            Properties.Settings.Default.groupBox2_Size = groupBox2.Size;
            Properties.Settings.Default.listbox1_Location = listView1.Location;
            Properties.Settings.Default.listbox1_Size = listView1.Size;
            Properties.Settings.Default.listView1Header1_Width = columnHeader1.Width;
            Properties.Settings.Default.listView1Header2_Width = columnHeader2.Width;
            Properties.Settings.Default.listView1Header3_Width = columnHeader3.Width;
            Properties.Settings.Default.listView1Header4_Width = columnHeader4.Width;
            Properties.Settings.Default.listView1Header5_Width = columnHeader5.Width;
            Properties.Settings.Default.Save();
            Ewt.deleteRTL(ref _rtlHandle);
        }

        private void HandleDiskEvents(CallbackData callbackData)
        {

            var listViewItem = new ListViewItem((listView1.Items.Count + _itemBuffer.Count).ToString());
            listViewItem.SubItems.Add(callbackData.Time.ToLongTimeString());
            try
            {
                listViewItem.SubItems.Add($"{Process.GetProcessById(Convert.ToInt32(callbackData.IssuingProcessId)).ProcessName} ({callbackData.IssuingProcessId})");
            }
            catch (ArgumentException)
            {
                listViewItem.SubItems.Add(callbackData.IssuingProcessId.ToString());
            }
            
            listViewItem.SubItems.Add(Enum.GetName(typeof(DiskAction), callbackData.Action));
            listViewItem.SubItems.Add(DisplayAsBytes(callbackData.TransferSize));
            listViewItem.SubItems.Add(DisplayAsSeconds((double)callbackData.HighResResponseTime * 1 / ConsumerClass.PerformanceCounterFrequency));

            lock (_itemBuffer)
            {
                _itemBuffer.Add(listViewItem); 
            }

            try
            {
                if (!IsDisposed && !Disposing)
                    listView1.Invoke(
                                     new MethodInvoker(
                                         () =>
                                             {
                                                 switch (callbackData.Action)
                                                 {
                                                     case DiskAction.Read:
                                                         _totalBytesRead += callbackData.TransferSize;
                                                         textBox1.Text = DisplayAsBytes(_totalBytesRead);
                                                         break;
                                                     case DiskAction.Write:
                                                         _totalBytesWritten += callbackData.TransferSize;
                                                         textBox2.Text = DisplayAsBytes(_totalBytesWritten);
                                                         break;
                                                 }
                                             }));
            }
            catch (ObjectDisposedException)
            {
                //Ignore
            }

        }

        private static string DisplayAsBytes(ulong bytes)
        {
            string prefix = "";
            double converted = 0;
            if (bytes > Math.Pow(2, 80))
            {
                prefix = "T";
                converted = bytes / Math.Pow(2, 80);
            }
            else if (bytes > Math.Pow(2, 40))
            {
                prefix = "G";
                converted = bytes / Math.Pow(2, 40);
            }
            else if (bytes > Math.Pow(2, 20))
            {
                prefix = "M";
                converted = bytes / Math.Pow(2, 20);
            }
            else if (bytes > Math.Pow(2, 10))
            {
                prefix = "K";
                converted = bytes / Math.Pow(2, 10);
            }
            
            return $"{converted:0.00} {prefix}iB";
        }

        private static string DisplayAsSeconds(double seconds)
        {
            string prefix = "";
            double converted = 0;
            if (seconds < Math.Pow(10, -7))
            {
                prefix = "n";
                converted = seconds * Math.Pow(10, 9);
            }
            else if (seconds < Math.Pow(10, -4))
            {
                prefix = "µ";
                converted = seconds * Math.Pow(10, 6);
            }
            else if (seconds < Math.Pow(10, -1))
            {
                prefix = "m";
                converted = seconds * Math.Pow(10, 3);
            }
            return $"{converted:0.00} {prefix}s";
        }

        private void UpdateTotalTimeRunning(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _totalTimeRunning += TimeSpan.FromSeconds(1);
            try
            {
                if (!IsDisposed && !Disposing)
                {
                    textBox3.Invoke(
                                    new MethodInvoker(
                                        delegate
                                            {
                                                textBox3.Text = new DateTime(_totalTimeRunning.Ticks).ToLongTimeString();
                                            }));
                }
            }
            catch (ObjectDisposedException)
            {
                //Ignore
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (!String.IsNullOrEmpty(_filePath) && File.Exists(_filePath))
            {
                SaveToFile(new FileStream(_filePath, FileMode.Truncate));
            }
            else
            {
                SaveToFile(ShowSaveFileDialog());
            }
        }

        private static Stream ShowSaveFileDialog()
        {
            var fileDialog = new SaveFileDialog { CreatePrompt = false, OverwritePrompt = true, DefaultExt = "txt", Filter = "Text files|*.txt|All files|*.*" };
            var result = fileDialog.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                return null;
            }

            _filePath = fileDialog.FileName;

            var fileStream = fileDialog.OpenFile();
            return fileStream;
        }

        private void SaveToFile(Stream fileStream)
        {
            if (fileStream == null)
            {
                return;
            }

            fileStream.SetLength(0);
            fileStream.Flush();

            var streamWriter = new StreamWriter(fileStream);
            streamWriter.AutoFlush = false;

            if (!fileStream.CanWrite)
            {
                MessageBox.Show("Insufficient rights!", "Writing to file failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            streamWriter.WriteLine($"_totalBytesRead-{_totalBytesRead}");
            streamWriter.WriteLine($"_totalBytesWritten-{_totalBytesWritten}");
            streamWriter.WriteLine($"_totalTimeRunning-{_totalTimeRunning}");

            foreach (ListViewItem listItem in listView1.Items)
            {
                streamWriter.Write("listViewEntries");
                foreach (ListViewItem.ListViewSubItem subItem in listItem.SubItems)
                {
                    streamWriter.Write("-" + subItem.Text);
                }
                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ewt.rtlStartConsumption(_rtlHandle);

            _updateTotalTimeRunningTimer?.Stop();
            _updateTotalTimeRunningTimer = new Timer(1000);
            _updateTotalTimeRunningTimer.Elapsed += UpdateTotalTimeRunning;
            _updateTotalTimeRunningTimer.AutoReset = true;
            _updateTotalTimeRunningTimer.Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ewt.rtlStopConsumption(_rtlHandle);
            _updateTotalTimeRunningTimer.Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void SaveFileAs()
        {
            var fileStream = ShowSaveFileDialog();
            SaveToFile(fileStream);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            var fileStream = ShowOpenFileDialog();
            RestoreFromFile(fileStream);
        }

        private void RestoreFromFile(Stream fileStream)
        {
            if (fileStream == null) return;
            if (!fileStream.CanRead) return;

            var streamReader = new StreamReader(fileStream);


            lock (_itemBuffer)
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    var selector = line.Substring(0, line.IndexOf('-'));
                    line = line.Remove(0, line.IndexOf('-') + 1);
                    var data = line.Split('-');

                    if (selector == "listViewEntries")
                    {
                        var listViewItem = new ListViewItem(data[0]);
                        for (var i = 1; i < data.Length; i++)
                        {
                            listViewItem.SubItems.Add(data[i]);
                        }
                        _itemBuffer.Add(listViewItem);
                    }
                    else if (selector == "_totalBytesRead")
                    {
                        _totalBytesRead = ulong.Parse(data[0]);
                        textBox1.Text = DisplayAsBytes(_totalBytesRead);
                    }
                    else if (selector == "_totalBytesWritten")
                    {
                        _totalBytesWritten = ulong.Parse(data[0]);
                        textBox2.Text = DisplayAsBytes(_totalBytesWritten);
                    }
                    else if (selector == "_totalTimeRunning")
                    {
                        _totalTimeRunning = TimeSpan.Parse(data[0]);
                        textBox3.Text = new DateTime(_totalTimeRunning.Ticks).ToLongTimeString();
                    }
                } 
            }
            streamReader.Close();
        }

        private static Stream ShowOpenFileDialog()
        {
            var fileDialog = new OpenFileDialog
                                 {
                                     CheckFileExists = true,
                                     Multiselect = false,
                                     ReadOnlyChecked = true,
                                     ShowReadOnly = true,
                                     DefaultExt = "txt",
                                     Filter = "Text files|*.txt|All files|*.*"
                                 };

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                return null;
            }

            return fileDialog.OpenFile();
        }

        private void resetDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Program.ResetForm1();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DiskUsage Analizer\n(c) 2016", "DiskUsageAnalizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsumerClass.ShowConsole();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();

            listView1.Items.Clear();
            textBox1.Text = "0 B";
            textBox2.Text = "0 B";
            textBox3.Text = "00:00:00";

            _totalBytesRead = 0;
            _totalBytesWritten = 0;
            _totalTimeRunning = TimeSpan.Zero;
            _filePath = string.Empty;
        }
    }
}
