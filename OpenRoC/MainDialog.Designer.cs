namespace oroc
{
    partial class MainDialog
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
                DisposeAddedComponents();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuStrip = new System.Windows.Forms.ToolStrip();
            this.AddButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteButton = new System.Windows.Forms.ToolStripButton();
            this.SettingsButton = new System.Windows.Forms.ToolStripButton();
            this.LogButton = new System.Windows.Forms.ToolStripButton();
            this.AboutButton = new System.Windows.Forms.ToolStripButton();
            this.ProcessListView = new System.Windows.Forms.ListView();
            this.Process = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RightClickContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuAddButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuDeleteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuEditButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuDisableButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.MainDialogUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.StatusStrip.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.RightClickContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusText});
            this.StatusStrip.Location = new System.Drawing.Point(0, 377);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(499, 22);
            this.StatusStrip.TabIndex = 0;
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(36, 17);
            this.StatusText.Text = "ready";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButton,
            this.DeleteButton,
            this.SettingsButton,
            this.LogButton,
            this.AboutButton});
            this.MenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(499, 22);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "toolStrip1";
            // 
            // AddButton
            // 
            this.AddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddButton.Image = ((System.Drawing.Image)(resources.GetObject("AddButton.Image")));
            this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(33, 19);
            this.AddButton.Text = "Add";
            this.AddButton.Click += new System.EventHandler(this.OnAddButtonClick);
            // 
            // DeleteButton
            // 
            this.DeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteButton.Image")));
            this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(44, 19);
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.Click += new System.EventHandler(this.OnDeleteButtonClick);
            // 
            // SettingsButton
            // 
            this.SettingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SettingsButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsButton.Image")));
            this.SettingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(53, 19);
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.Click += new System.EventHandler(this.OnSettingsButtonClick);
            // 
            // LogButton
            // 
            this.LogButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LogButton.Image = ((System.Drawing.Image)(resources.GetObject("LogButton.Image")));
            this.LogButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LogButton.Name = "LogButton";
            this.LogButton.Size = new System.Drawing.Size(36, 19);
            this.LogButton.Text = "Logs";
            this.LogButton.Click += new System.EventHandler(this.OnLogButtonClick);
            // 
            // AboutButton
            // 
            this.AboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AboutButton.Image = ((System.Drawing.Image)(resources.GetObject("AboutButton.Image")));
            this.AboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(44, 19);
            this.AboutButton.Text = "About";
            this.AboutButton.Click += new System.EventHandler(this.OnAboutButtonClick);
            // 
            // ProcessListView
            // 
            this.ProcessListView.AllowDrop = true;
            this.ProcessListView.CheckBoxes = true;
            this.ProcessListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Process,
            this.Status});
            this.ProcessListView.ContextMenuStrip = this.RightClickContextMenuStrip;
            this.ProcessListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessListView.FullRowSelect = true;
            this.ProcessListView.GridLines = true;
            this.ProcessListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ProcessListView.Location = new System.Drawing.Point(0, 22);
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.ShowGroups = false;
            this.ProcessListView.Size = new System.Drawing.Size(499, 355);
            this.ProcessListView.TabIndex = 2;
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            this.ProcessListView.View = System.Windows.Forms.View.Details;
            this.ProcessListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OnProcessListViewItemCheck);
            this.ProcessListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.OnProcessListViewItemChecked);
            this.ProcessListView.SizeChanged += new System.EventHandler(this.OnProcessListViewResize);
            this.ProcessListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnProcessListViewDragDrop);
            this.ProcessListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnProcessListViewDragEnter);
            this.ProcessListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnContextMenuEditButtonClick);
            this.ProcessListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnProcessListViewMouseDown);
            this.ProcessListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnProcessListViewMouseUp);
            // 
            // Process
            // 
            this.Process.Text = "Process";
            this.Process.Width = 320;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 175;
            // 
            // RightClickContextMenuStrip
            // 
            this.RightClickContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuAddButton,
            this.ContextMenuDeleteButton,
            this.ContextMenuSeparator1,
            this.ContextMenuEditButton,
            this.ContextMenuDisableButton,
            this.ContextMenuSeparator2,
            this.ContextMenuStart,
            this.ContextMenuStop,
            this.ContextMenuShow});
            this.RightClickContextMenuStrip.Name = "ContextMenuStrip";
            this.RightClickContextMenuStrip.ShowImageMargin = false;
            this.RightClickContextMenuStrip.Size = new System.Drawing.Size(126, 170);
            // 
            // ContextMenuAddButton
            // 
            this.ContextMenuAddButton.Name = "ContextMenuAddButton";
            this.ContextMenuAddButton.Size = new System.Drawing.Size(125, 22);
            this.ContextMenuAddButton.Text = "Add";
            this.ContextMenuAddButton.Click += new System.EventHandler(this.OnAddButtonClick);
            // 
            // ContextMenuDeleteButton
            // 
            this.ContextMenuDeleteButton.Name = "ContextMenuDeleteButton";
            this.ContextMenuDeleteButton.Size = new System.Drawing.Size(125, 22);
            this.ContextMenuDeleteButton.Text = "Delete";
            this.ContextMenuDeleteButton.Click += new System.EventHandler(this.OnContextMenuDeleteButtonClick);
            // 
            // ContextMenuSeparator1
            // 
            this.ContextMenuSeparator1.Name = "ContextMenuSeparator1";
            this.ContextMenuSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // ContextMenuEditButton
            // 
            this.ContextMenuEditButton.Name = "ContextMenuEditButton";
            this.ContextMenuEditButton.Size = new System.Drawing.Size(125, 22);
            this.ContextMenuEditButton.Text = "Edit";
            this.ContextMenuEditButton.Click += new System.EventHandler(this.OnContextMenuEditButtonClick);
            // 
            // ContextMenuDisableButton
            // 
            this.ContextMenuDisableButton.Name = "ContextMenuDisableButton";
            this.ContextMenuDisableButton.Size = new System.Drawing.Size(125, 22);
            this.ContextMenuDisableButton.Text = "Disable";
            this.ContextMenuDisableButton.Click += new System.EventHandler(this.OnContextMenuDisableButtonClick);
            // 
            // ContextMenuSeparator2
            // 
            this.ContextMenuSeparator2.Name = "ContextMenuSeparator2";
            this.ContextMenuSeparator2.Size = new System.Drawing.Size(122, 6);
            // 
            // ContextMenuStart
            // 
            this.ContextMenuStart.Name = "ContextMenuStart";
            this.ContextMenuStart.Size = new System.Drawing.Size(125, 22);
            this.ContextMenuStart.Text = "Start Process";
            this.ContextMenuStart.Click += new System.EventHandler(this.OnContextMenuStartClick);
            // 
            // ContextMenuStop
            // 
            this.ContextMenuStop.Name = "ContextMenuStop";
            this.ContextMenuStop.Size = new System.Drawing.Size(125, 22);
            this.ContextMenuStop.Text = "Stop Process";
            this.ContextMenuStop.Click += new System.EventHandler(this.OnContextMenuStopClick);
            // 
            // ContextMenuShow
            // 
            this.ContextMenuShow.Name = "ContextMenuShow";
            this.ContextMenuShow.Size = new System.Drawing.Size(125, 22);
            this.ContextMenuShow.Text = "Show Window";
            this.ContextMenuShow.Click += new System.EventHandler(this.OnContextMenuShowClick);
            // 
            // MainDialogUpdateTimer
            // 
            this.MainDialogUpdateTimer.Enabled = true;
            this.MainDialogUpdateTimer.Tick += new System.EventHandler(this.OnMainDialogUpdateTimerTick);
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 399);
            this.Controls.Add(this.ProcessListView);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.StatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainDialog";
            this.Text = "OpenRoC";
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.RightClickContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.ToolStrip MenuStrip;
        private System.Windows.Forms.ListView ProcessListView;
        private System.Windows.Forms.ToolStripButton AddButton;
        private System.Windows.Forms.ToolStripButton DeleteButton;
        private System.Windows.Forms.ToolStripButton SettingsButton;
        private System.Windows.Forms.ToolStripButton LogButton;
        private System.Windows.Forms.ToolStripButton AboutButton;
        private System.Windows.Forms.ColumnHeader Process;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ContextMenuStrip RightClickContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAddButton;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuDeleteButton;
        private System.Windows.Forms.ToolStripSeparator ContextMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuEditButton;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuDisableButton;
        private System.Windows.Forms.Timer MainDialogUpdateTimer;
        private System.Windows.Forms.ToolStripSeparator ContextMenuSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuStart;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuStop;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuShow;
    }
}

