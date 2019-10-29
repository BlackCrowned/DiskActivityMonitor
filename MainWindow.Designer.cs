namespace DiskActivityMonitor
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DiskActivityMonitor.Properties.Settings settings1 = new DiskActivityMonitor.Properties.Settings();
            this.grpActivity = new System.Windows.Forms.GroupBox();
            this.lstDiskEventLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpSummary = new System.Windows.Forms.GroupBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalWritten = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalRead = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSomeEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpActivity.SuspendLayout();
            this.grpSummary.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpActivity
            // 
            this.grpActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpActivity.Controls.Add(this.lstDiskEventLog);
            this.grpActivity.DataBindings.Add(new System.Windows.Forms.Binding("Location", settings1, "groupBox1_Location", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.grpActivity.DataBindings.Add(new System.Windows.Forms.Binding("Size", settings1, "groupBox1_Size", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.grpActivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpActivity.Location = settings1.groupBox1_Location;
            this.grpActivity.Name = "grpActivity";
            this.grpActivity.Size = settings1.groupBox1_Size;
            this.grpActivity.TabIndex = 0;
            this.grpActivity.TabStop = false;
            this.grpActivity.Text = "Activity";
            this.grpActivity.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lstDiskEventLog
            // 
            this.lstDiskEventLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDiskEventLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstDiskEventLog.DataBindings.Add(new System.Windows.Forms.Binding("Location", settings1, "listbox1_Location", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lstDiskEventLog.DataBindings.Add(new System.Windows.Forms.Binding("Size", settings1, "listbox1_Size", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lstDiskEventLog.FullRowSelect = true;
            this.lstDiskEventLog.HideSelection = false;
            this.lstDiskEventLog.Location = settings1.listbox1_Location;
            this.lstDiskEventLog.Name = "lstDiskEventLog";
            this.lstDiskEventLog.Size = settings1.listbox1_Size;
            this.lstDiskEventLog.TabIndex = 0;
            this.lstDiskEventLog.UseCompatibleStateImageBehavior = false;
            this.lstDiskEventLog.View = System.Windows.Forms.View.Details;
            this.lstDiskEventLog.VirtualMode = true;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            settings1._filePath = "";
            settings1.Form1_ClientSize = new System.Drawing.Size(656, 421);
            settings1.Form1_Location = new System.Drawing.Point(0, 0);
            settings1.groupBox1_Location = new System.Drawing.Point(13, 27);
            settings1.groupBox1_Size = new System.Drawing.Size(479, 343);
            settings1.groupBox2_Location = new System.Drawing.Point(498, 27);
            settings1.groupBox2_Size = new System.Drawing.Size(130, 343);
            settings1.listbox1_Location = new System.Drawing.Point(6, 19);
            settings1.listbox1_Size = new System.Drawing.Size(467, 318);
            settings1.listView1Header1_Width = 34;
            settings1.listView1Header2_Width = 97;
            settings1.listView1Header3_Width = 60;
            settings1.listView1Header4_Width = 63;
            settings1.listView1Header5_Width = 72;
            settings1.listView1Header6_Width = 60;
            settings1.listView1Header7_Width = 48;
            settings1.SettingsKey = "";
            this.columnHeader1.Width = settings1.listView1Header1_Width;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Disk #";
            this.columnHeader7.Width = settings1.listView1Header7_Width;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Time";
            this.columnHeader6.Width = settings1.listView1Header6_Width;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Process";
            this.columnHeader2.Width = settings1.listView1Header2_Width;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Action";
            this.columnHeader3.Width = settings1.listView1Header3_Width;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Amount";
            this.columnHeader4.Width = settings1.listView1Header4_Width;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Duration";
            this.columnHeader5.Width = settings1.listView1Header5_Width;
            // 
            // grpSummary
            // 
            this.grpSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSummary.Controls.Add(this.txtDuration);
            this.grpSummary.Controls.Add(this.label3);
            this.grpSummary.Controls.Add(this.txtTotalWritten);
            this.grpSummary.Controls.Add(this.label2);
            this.grpSummary.Controls.Add(this.txtTotalRead);
            this.grpSummary.Controls.Add(this.label1);
            this.grpSummary.DataBindings.Add(new System.Windows.Forms.Binding("Location", settings1, "groupBox2_Location", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.grpSummary.DataBindings.Add(new System.Windows.Forms.Binding("Size", settings1, "groupBox2_Size", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.grpSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpSummary.Location = settings1.groupBox2_Location;
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Size = settings1.groupBox2_Size;
            this.grpSummary.TabIndex = 1;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "Summary";
            // 
            // txtDuration
            // 
            this.txtDuration.Enabled = false;
            this.txtDuration.Location = new System.Drawing.Point(9, 116);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(115, 20);
            this.txtDuration.TabIndex = 5;
            this.txtDuration.Text = "00:00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Location = new System.Drawing.Point(6, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Total time running:";
            // 
            // txtTotalWritten
            // 
            this.txtTotalWritten.Enabled = false;
            this.txtTotalWritten.Location = new System.Drawing.Point(9, 76);
            this.txtTotalWritten.Name = "txtTotalWritten";
            this.txtTotalWritten.Size = new System.Drawing.Size(115, 20);
            this.txtTotalWritten.TabIndex = 3;
            this.txtTotalWritten.Text = "0 B";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total written:";
            // 
            // txtTotalRead
            // 
            this.txtTotalRead.Enabled = false;
            this.txtTotalRead.Location = new System.Drawing.Point(9, 36);
            this.txtTotalRead.Name = "txtTotalRead";
            this.txtTotalRead.Size = new System.Drawing.Size(115, 20);
            this.txtTotalRead.TabIndex = 1;
            this.txtTotalRead.Text = "0 B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total read:";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.monitorToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(640, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            this.monitorToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.monitorToolStripMenuItem.Text = "Monitor";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showConsoleToolStripMenuItem,
            this.addSomeEntriesToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // showConsoleToolStripMenuItem
            // 
            this.showConsoleToolStripMenuItem.Name = "showConsoleToolStripMenuItem";
            this.showConsoleToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.showConsoleToolStripMenuItem.Text = "Show Console";
            this.showConsoleToolStripMenuItem.Click += new System.EventHandler(this.showConsoleToolStripMenuItem_Click);
            // 
            // addSomeEntriesToolStripMenuItem
            // 
            this.addSomeEntriesToolStripMenuItem.Name = "addSomeEntriesToolStripMenuItem";
            this.addSomeEntriesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addSomeEntriesToolStripMenuItem.Text = "Add some Entries";
            this.addSomeEntriesToolStripMenuItem.Click += new System.EventHandler(this.addSomeEntriesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetDefaultsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // resetDefaultsToolStripMenuItem
            // 
            this.resetDefaultsToolStripMenuItem.Name = "resetDefaultsToolStripMenuItem";
            this.resetDefaultsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.resetDefaultsToolStripMenuItem.Text = "Reset Defaults";
            this.resetDefaultsToolStripMenuItem.Click += new System.EventHandler(this.resetDefaultsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.aboutToolStripMenuItem.Text = "About Disk Activity Monitor";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 382);
            this.Controls.Add(this.grpSummary);
            this.Controls.Add(this.grpActivity);
            this.Controls.Add(this.menuStrip);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", settings1, "Form1_Location", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Size", settings1, "Form1_ClientSize", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.Location = settings1.Form1_Location;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "Disk Activity Monitor";
            this.grpActivity.ResumeLayout(false);
            this.grpSummary.ResumeLayout(false);
            this.grpSummary.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpActivity;
        private System.Windows.Forms.ListView lstDiskEventLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox grpSummary;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTotalWritten;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalRead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetDefaultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSomeEntriesToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader7;
    }
}

