using System;
using System.Windows.Forms;

namespace DiskUsageAnalizer
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Web.WebSockets;

    using Timer = System.Timers.Timer;

    public partial class Form1 : Form
    {
        [Serializable]
        private class Data

        {
            public ulong TotalBytesRead = 0;

            public ulong TotalBytesWritten = 0;

            public TimeSpan TotalTimeRunning = TimeSpan.Zero;

            public List<CallbackData> CallbacksDataList = new List<CallbackData>();

            public int Index = 0;
        }

        private static readonly int _maxHistorySize = 100000;

        private static readonly int _maxOverflowBufferSize = _maxHistorySize / 2;

        private Data _data;

        private IntPtr _rtlHandle;

        private Timer _updateTotalTimeRunningTimer;

        private Timer _updateListView1Timer;

        private Dictionary<int, ListViewItem> _listViewItemCache;

        private static string _filePath;

        private SaveState _saveState;

        private event Action Saved;

        private event Action SaveFailed;

        private event Action Restored;

        private event Action RestoreFailed;

        public Form1()
        {
            InitializeComponent();

            _filePath = Properties.Settings.Default._filePath;

            _data = new Data();

            _saveState = SaveState.NotSaved;

            _updateListView1Timer = new Timer(500) { AutoReset = true };
            _updateListView1Timer.Elapsed += UpdateListView1;

            listView1.RetrieveVirtualItem += ListView1OnRetrieveVirtualItem;

            ConsumerClass.EventReceived += HandleDiskEvents;

            Disposed += (sender, args) => Cleanup();
        }

        private void UpdateListView1(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            SafeInvokeUIThread(
                               () =>
                                   {
                                       if (WindowState == FormWindowState.Minimized) return;

                                       lock (_data)
                                       {
                                           if (_data.CallbacksDataList.Count == listView1.VirtualListSize) return;

                                           if (_data.CallbacksDataList[listView1.VirtualListSize].Time - DateTime.Now > TimeSpan.FromSeconds(15))
                                           {
                                               _updateListView1Timer.Interval = 5000;
                                           }
                                           else
                                           {
                                               _updateListView1Timer.Interval = 500;
                                           }

                                           if (_data.CallbacksDataList.Count > _maxHistorySize + _maxOverflowBufferSize)
                                           {
                                               _data.CallbacksDataList.RemoveRange(0, _data.CallbacksDataList.Count - _maxHistorySize);
                                           }

                                           listView1.VirtualListSize = _data.CallbacksDataList.Count;
                                           if (_data.CallbacksDataList.Count > 0) listView1.EnsureVisible(_data.CallbacksDataList.Count - 1);
                                       }
                                   });
        }

        private void ListView1OnRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs retrieveVirtualItemEventArgs)
        {
            CallbackData callbackData;
            lock (_data)
            {
                callbackData = _data.CallbacksDataList[retrieveVirtualItemEventArgs.ItemIndex];
            }
            var listViewItem = new ListViewItem(callbackData.Index.ToString());
            listViewItem.SubItems.Add(callbackData.DiskNumber.ToString());
            listViewItem.SubItems.Add(callbackData.Time.ToLongTimeString());
            listViewItem.SubItems.Add($"{callbackData.IssuingProcessName} ({callbackData.IssuingProcessId})");
            listViewItem.SubItems.Add(Enum.GetName(typeof(DiskAction), callbackData.Action));
            listViewItem.SubItems.Add(DisplayAsBytes(callbackData.TransferSize));
            listViewItem.SubItems.Add(DisplayAsSeconds((double)callbackData.HighResResponseTime * 1 / ConsumerClass.PerformanceCounterFrequency));

            retrieveVirtualItemEventArgs.Item = listViewItem;
        }

        protected override void OnClosing(CancelEventArgs eventArgs)
        {
            if (_saveState == SaveState.Saved  || _saveState == SaveState.Failed)
            {
                base.OnClosing(eventArgs);
                return;
            }

            var result = MessageBox.Show("Save changes?", "DiskUsage Analizer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button3);
            switch (result)
            {
                case DialogResult.Yes:
                    eventArgs.Cancel = true;
                    Saved += Close;
                    SaveFailed += Close;
                    Save();
                    Hide();
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    eventArgs.Cancel = true;
                    break;
            }
            base.OnClosing(eventArgs);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            InitGUI();
            listView1.DoubleBuffered(true);

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

            if (!string.IsNullOrEmpty(_filePath) && File.Exists(_filePath)
                && MessageBox.Show("Restore session from last time?", "Restore session...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RestoreFromFile(new FileStream(_filePath, FileMode.Open));
            }
            else
            {
                _filePath = "";
            }
        }

        private void Cleanup()
        {
            ConsumerClass.EventReceived -= HandleDiskEvents;
            _updateListView1Timer?.Stop();
            _updateTotalTimeRunningTimer?.Stop();
            Properties.Settings.Default._filePath = _filePath;
            Properties.Settings.Default.Form1_ClientSize = Size;
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
            Properties.Settings.Default.listView1Header6_Width = columnHeader6.Width;
            Properties.Settings.Default.listView1Header7_Width = columnHeader7.Width;
            Properties.Settings.Default.Save();
            Ewt.deleteRTL(ref _rtlHandle);
        }

        private void HandleDiskEvents(CallbackData callbackData)
        {
            SafeInvokeUIThread(
                               delegate
                                   {
                                       try
                                       {
                                           callbackData.IssuingProcessName = Process.GetProcessById(Convert.ToInt32(callbackData.IssuingProcessId)).ProcessName;
                                       }
                                       catch (ArgumentException)
                                       {
                                           callbackData.IssuingProcessName = "Unknown";
                                       }
                                       catch
                                       {
                                           callbackData.IssuingProcessName = "Unknown";
                                       }
                                       lock (_data)
                                       {
                                           callbackData.Index = _data.Index++;
                                           _data.CallbacksDataList.Add(callbackData);
                                       }
                                       _saveState = SaveState.NotSaved;

                                       lock (_data)
                                       {
                                           switch (callbackData.Action)
                                           {
                                               case DiskAction.Read:
                                                   _data.TotalBytesRead += callbackData.TransferSize;
                                                   textBox1.Text = DisplayAsBytes(_data.TotalBytesRead);
                                                   break;
                                               case DiskAction.Write:
                                                   _data.TotalBytesWritten += callbackData.TransferSize;
                                                   textBox2.Text = DisplayAsBytes(_data.TotalBytesWritten);
                                                   break;
                                           }
                                       }
                                   });
        }

        private static string DisplayAsBytes(ulong bytes)
        {
            string prefix = "";
            double converted = 0;
            if (bytes >= Math.Pow(2, 40))
            {
                prefix = "Ti";
                converted = bytes / Math.Pow(2, 40);
            }
            else if (bytes >= Math.Pow(2, 30))
            {
                prefix = "Gi";
                converted = bytes / Math.Pow(2, 30);
            }
            else if (bytes >= Math.Pow(2, 20))
            {
                prefix = "Mi";
                converted = bytes / Math.Pow(2, 20);
            }
            else if (bytes >= Math.Pow(2, 10))
            {
                prefix = "Ki";
                converted = bytes / Math.Pow(2, 10);
            }

            return $"{converted:0.00} {prefix}B";
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
            _data.TotalTimeRunning += TimeSpan.FromSeconds(1);
            SafeInvokeUIThread(
                               delegate
                                   {
                                       lock (_data)
                                       {
                                           textBox3.Text = new DateTime(_data.TotalTimeRunning.Ticks).ToLongTimeString();
                                       }
                                   });
        }

        private void SafeInvokeUIThread(MethodInvoker target)
        {
            try
            {
                if (!IsDisposed && !Disposing && IsHandleCreated)
                {
                    BeginInvoke(target);
                }
                else if (!IsDisposed && !Disposing && !IsHandleCreated)
                {
                    Shown += (sender, args) => target.Invoke();
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
            if (_saveState != SaveState.NotSaved) return;

            _saveState = SaveState.Saving;

            if (!string.IsNullOrEmpty(_filePath) && File.Exists(_filePath))
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
            var fileDialog = new SaveFileDialog { CreatePrompt = false, OverwritePrompt = true, DefaultExt = "txt", Filter = "Data files|*.dat|Text files|*.txt|All files|*.*" };
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
                _saveState = SaveState.Failed;
                SaveFailed?.Invoke();
                return;
            }
            fileStream.SetLength(0);
            fileStream.Flush();

            if (!fileStream.CanWrite)
            {
                MessageBox.Show("Insufficient rights!", "Writing to file failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _saveState = SaveState.Failed;
                SaveFailed?.Invoke();
                return;
            }

            var bw = new BackgroundWorker();
            var progressDialog = new ProgressDialog("Saving...");

            bw.DoWork += delegate(object sender, DoWorkEventArgs args)
                {
                    try
                    {
                        var formatter = new BinaryFormatter();

                        lock (_data)
                        {
                            formatter.Serialize(fileStream, _data);
                        }
                    }
                    catch 
                    {
                        _saveState = SaveState.Failed;
                        SaveFailed?.Invoke();
                    }
                };
            bw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs args)
                {
                    progressDialog.Close();
                    _saveState = SaveState.Saved;

                    fileStream.Close();
                    Saved?.Invoke();
                };

            progressDialog.Show();
            bw.RunWorkerAsync();
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
            _saveState = SaveState.Saving;
            var fileStream = ShowSaveFileDialog();
            SaveToFile(fileStream);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            _saveState = SaveState.Restoring;
            var fileStream = ShowOpenFileDialog();
            RestoreFromFile(fileStream);
        }

        private void RestoreFromFile(Stream fileStream)
        {
            if (fileStream == null)
            {
                _saveState = SaveState.Failed;
                RestoreFailed?.Invoke();
                return;
            }
            if (!fileStream.CanRead)
            {
                MessageBox.Show("Insufficient rights!", "Reading from file failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _saveState = SaveState.Failed;
                RestoreFailed?.Invoke();
                return;
            }

            var bw = new BackgroundWorker();
            var progressDialog = new ProgressDialog("Opening...");

            bw.DoWork += delegate(object sender, DoWorkEventArgs args)
                {
                    try
                    {
                        var formatter = new BinaryFormatter();
                        _data = (Data)formatter.Deserialize(fileStream);
                    }
                    catch 
                    {
                        _saveState = SaveState.Failed;
                        RestoreFailed?.Invoke();
                    }
                };
            bw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs args)
                {
                    progressDialog.Close();
                    _saveState = SaveState.Saved;

                    fileStream.Close();

                    SafeInvokeUIThread(InitGUI);

                    Restored?.Invoke();
                };

            bw.RunWorkerAsync();
            progressDialog.Show();
        }

        private void InitGUI()
        {
            lock (_data)
            {
                listView1.VirtualListSize = _data.CallbacksDataList.Count;
                if (_data.CallbacksDataList.Count > 0) listView1.EnsureVisible(_data.CallbacksDataList.Count - 1);
                textBox1.Text = DisplayAsBytes(_data.TotalBytesRead);
                textBox2.Text = DisplayAsBytes(_data.TotalBytesWritten);
                textBox3.Text = new DateTime(_data.TotalTimeRunning.Ticks).ToLongTimeString();
            }
            _updateListView1Timer.Start();
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
                                     Filter = "Data files|*.dat|Text files|*.txt|All files|*.*"
                                 };

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                return null;
            }

            _filePath = fileDialog.FileName;

            return fileDialog.OpenFile();
        }

        private void resetDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Program.ResetForm1();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DiskUsage Analizer\n(c) 2016 Michael M.", "DiskUsage Analizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            _data = new Data();
            _listViewItemCache = new Dictionary<int, ListViewItem>();

            _filePath = string.Empty;

            InitGUI();
        }

        private void addSomeEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                HandleDiskEvents(new CallbackData() { Action = DiskAction.Read, TransferSize = 0, IssuingProcessName = "Debug", IssuingProcessId = i });
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) {}
    }

    public enum SaveState
    {
        Saved = 1,

        Saving,

        NotSaved,

        Restoring,

        Failed
    }
}