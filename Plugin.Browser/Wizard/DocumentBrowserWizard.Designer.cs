namespace Plugin.Browser
{
	partial class DocumentBrowserWizard
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
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Panel pnlHead;
			System.Windows.Forms.ImageList ilImages;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentBrowserWizard));
			System.Windows.Forms.Label label1;
			System.Windows.Forms.StatusStrip ssNodePath;
			System.Windows.Forms.ColumnHeader colAdvancedName;
			System.Windows.Forms.ColumnHeader colAdvancedValue;
			this.bnFfwd = new System.Windows.Forms.Button();
			this.bnBack = new System.Windows.Forms.Button();
			this.bnNavigate = new System.Windows.Forms.Button();
			this.txtNavigate = new System.Windows.Forms.TextBox();
			this.tsslNodePath = new System.Windows.Forms.ToolStripStatusLabel();
			this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
			this.splitBrowser = new System.Windows.Forms.SplitContainer();
			this.splitAdvancedEdit = new System.Windows.Forms.SplitContainer();
			this.flowNodes = new System.Windows.Forms.FlowLayoutPanel();
			this.lvAdvancedNodes = new System.Windows.Forms.ListView();
			this.tsAdvanced = new System.Windows.Forms.ToolStrip();
			this.txtAdvancedPath = new System.Windows.Forms.ToolStripTextBox();
			this.tsbnAdvancedSave = new System.Windows.Forms.ToolStripButton();
			this.tsNodes = new System.Windows.Forms.ToolStrip();
			this.tsbnNodesSave = new System.Windows.Forms.ToolStripButton();
			this.ttNodes = new System.Windows.Forms.ToolTip(this.components);
			this.cmsAdvancedTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiAdvancedEdit = new System.Windows.Forms.ToolStripMenuItem();
			pnlHead = new System.Windows.Forms.Panel();
			ilImages = new System.Windows.Forms.ImageList(this.components);
			label1 = new System.Windows.Forms.Label();
			ssNodePath = new System.Windows.Forms.StatusStrip();
			colAdvancedName = new System.Windows.Forms.ColumnHeader();
			colAdvancedValue = new System.Windows.Forms.ColumnHeader();
			pnlHead.SuspendLayout();
			ssNodePath.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitBrowser)).BeginInit();
			this.splitBrowser.Panel1.SuspendLayout();
			this.splitBrowser.Panel2.SuspendLayout();
			this.splitBrowser.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitAdvancedEdit)).BeginInit();
			this.splitAdvancedEdit.Panel1.SuspendLayout();
			this.splitAdvancedEdit.Panel2.SuspendLayout();
			this.splitAdvancedEdit.SuspendLayout();
			this.tsAdvanced.SuspendLayout();
			this.tsNodes.SuspendLayout();
			this.cmsAdvancedTemplate.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHead
			// 
			pnlHead.Controls.Add(this.bnFfwd);
			pnlHead.Controls.Add(this.bnBack);
			pnlHead.Controls.Add(this.bnNavigate);
			pnlHead.Controls.Add(this.txtNavigate);
			pnlHead.Controls.Add(label1);
			pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
			pnlHead.Location = new System.Drawing.Point(0, 0);
			pnlHead.Name = "pnlHead";
			pnlHead.Size = new System.Drawing.Size(526, 35);
			pnlHead.TabIndex = 3;
			// 
			// bnFfwd
			// 
			this.bnFfwd.Enabled = false;
			this.bnFfwd.Location = new System.Drawing.Point(36, 4);
			this.bnFfwd.Name = "bnFfwd";
			this.bnFfwd.Size = new System.Drawing.Size(30, 22);
			this.bnFfwd.TabIndex = 4;
			this.bnFfwd.Text = ">";
			this.bnFfwd.UseVisualStyleBackColor = true;
			this.bnFfwd.Click += new System.EventHandler(this.bnFfwd_Click);
			// 
			// bnBack
			// 
			this.bnBack.Enabled = false;
			this.bnBack.Location = new System.Drawing.Point(3, 4);
			this.bnBack.Name = "bnBack";
			this.bnBack.Size = new System.Drawing.Size(30, 22);
			this.bnBack.TabIndex = 3;
			this.bnBack.Text = "<";
			this.bnBack.UseVisualStyleBackColor = true;
			this.bnBack.Click += new System.EventHandler(this.bnBack_Click);
			// 
			// bnNavigate
			// 
			this.bnNavigate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnNavigate.ImageKey = "iconNavigate.bmp";
			this.bnNavigate.ImageList = ilImages;
			this.bnNavigate.Location = new System.Drawing.Point(483, 4);
			this.bnNavigate.Name = "bnNavigate";
			this.bnNavigate.Size = new System.Drawing.Size(40, 22);
			this.bnNavigate.TabIndex = 2;
			this.bnNavigate.UseVisualStyleBackColor = true;
			this.bnNavigate.Click += new System.EventHandler(this.bnNavigate_Click);
			// 
			// ilImages
			// 
			ilImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilImages.ImageStream")));
			ilImages.TransparentColor = System.Drawing.Color.Magenta;
			ilImages.Images.SetKeyName(0, "iconNavigate.bmp");
			// 
			// txtNavigate
			// 
			this.txtNavigate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtNavigate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtNavigate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
			this.txtNavigate.Location = new System.Drawing.Point(130, 6);
			this.txtNavigate.Name = "txtNavigate";
			this.txtNavigate.Size = new System.Drawing.Size(347, 20);
			this.txtNavigate.TabIndex = 1;
			this.txtNavigate.TextChanged += new System.EventHandler(this.txtNavigate_TextChanged);
			this.txtNavigate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNavigate_KeyDown);
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			label1.Location = new System.Drawing.Point(75, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(49, 13);
			label1.TabIndex = 0;
			label1.Text = "&Adress:";
			// 
			// ssNodePath
			// 
			ssNodePath.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslNodePath});
			ssNodePath.Location = new System.Drawing.Point(0, 131);
			ssNodePath.Name = "ssNodePath";
			ssNodePath.Size = new System.Drawing.Size(526, 22);
			ssNodePath.SizingGrip = false;
			ssNodePath.TabIndex = 2;
			// 
			// tsslNodePath
			// 
			this.tsslNodePath.Name = "tsslNodePath";
			this.tsslNodePath.Size = new System.Drawing.Size(0, 17);
			// 
			// colAdvancedName
			// 
			colAdvancedName.Text = "Name";
			// 
			// colAdvancedValue
			// 
			colAdvancedValue.Text = "Value";
			// 
			// webView
			// 
			this.webView.AllowExternalDrop = true;
			this.webView.CreationProperties = null;
			this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
			this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webView.Location = new System.Drawing.Point(0, 0);
			this.webView.Name = "webView";
			this.webView.Size = new System.Drawing.Size(526, 154);
			this.webView.TabIndex = 1;
			this.webView.ZoomFactor = 1D;
			this.webView.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.WebView_CoreWebView2InitializationCompleted);
			this.webView.WebMessageReceived += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs>(this.WebView_WebMessageReceived);
			this.webView.NavigationStarting += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(this.WebView_NavigationStarting);
			this.webView.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.WebView_NavigationCompleted);
			// 
			// splitBrowser
			// 
			this.splitBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitBrowser.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitBrowser.Location = new System.Drawing.Point(0, 35);
			this.splitBrowser.Name = "splitBrowser";
			this.splitBrowser.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitBrowser.Panel1
			// 
			this.splitBrowser.Panel1.Controls.Add(this.webView);
			// 
			// splitBrowser.Panel2
			// 
			this.splitBrowser.Panel2.Controls.Add(this.splitAdvancedEdit);
			this.splitBrowser.Panel2.Controls.Add(this.tsNodes);
			this.splitBrowser.Panel2.Controls.Add(ssNodePath);
			this.splitBrowser.Panel2MinSize = 22;
			this.splitBrowser.Size = new System.Drawing.Size(526, 311);
			this.splitBrowser.SplitterDistance = 154;
			this.splitBrowser.TabIndex = 4;
			// 
			// splitAdvancedEdit
			// 
			this.splitAdvancedEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitAdvancedEdit.Location = new System.Drawing.Point(32, 0);
			this.splitAdvancedEdit.Name = "splitAdvancedEdit";
			// 
			// splitAdvancedEdit.Panel1
			// 
			this.splitAdvancedEdit.Panel1.Controls.Add(this.flowNodes);
			// 
			// splitAdvancedEdit.Panel2
			// 
			this.splitAdvancedEdit.Panel2.Controls.Add(this.lvAdvancedNodes);
			this.splitAdvancedEdit.Panel2.Controls.Add(this.tsAdvanced);
			this.splitAdvancedEdit.Size = new System.Drawing.Size(494, 131);
			this.splitAdvancedEdit.SplitterDistance = 321;
			this.splitAdvancedEdit.TabIndex = 3;
			this.splitAdvancedEdit.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.splitAdvancedEdit_MouseDoubleClick);
			// 
			// flowNodes
			// 
			this.flowNodes.AutoScroll = true;
			this.flowNodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowNodes.Location = new System.Drawing.Point(0, 0);
			this.flowNodes.Name = "flowNodes";
			this.flowNodes.Size = new System.Drawing.Size(321, 131);
			this.flowNodes.TabIndex = 0;
			// 
			// lvAdvancedNodes
			// 
			this.lvAdvancedNodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            colAdvancedName,
            colAdvancedValue});
			this.lvAdvancedNodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvAdvancedNodes.FullRowSelect = true;
			this.lvAdvancedNodes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvAdvancedNodes.Location = new System.Drawing.Point(0, 25);
			this.lvAdvancedNodes.MultiSelect = false;
			this.lvAdvancedNodes.Name = "lvAdvancedNodes";
			this.lvAdvancedNodes.Size = new System.Drawing.Size(169, 106);
			this.lvAdvancedNodes.TabIndex = 1;
			this.lvAdvancedNodes.UseCompatibleStateImageBehavior = false;
			this.lvAdvancedNodes.View = System.Windows.Forms.View.Details;
			// 
			// tsAdvanced
			// 
			this.tsAdvanced.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsAdvanced.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtAdvancedPath,
            this.tsbnAdvancedSave});
			this.tsAdvanced.Location = new System.Drawing.Point(0, 0);
			this.tsAdvanced.Name = "tsAdvanced";
			this.tsAdvanced.Size = new System.Drawing.Size(169, 25);
			this.tsAdvanced.TabIndex = 0;
			this.tsAdvanced.Resize += new System.EventHandler(this.tsAdvanced_Resize);
			// 
			// txtAdvancedPath
			// 
			this.txtAdvancedPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtAdvancedPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtAdvancedPath.AutoSize = false;
			this.txtAdvancedPath.Name = "txtAdvancedPath";
			this.txtAdvancedPath.Size = new System.Drawing.Size(100, 25);
			this.txtAdvancedPath.TextChanged += new System.EventHandler(this.txtAdvancedPath_TextChanged);
			// 
			// tsbnAdvancedSave
			// 
			this.tsbnAdvancedSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbnAdvancedSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbnAdvancedSave.Image = global::Plugin.Browser.Properties.Resources.FileSave;
			this.tsbnAdvancedSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbnAdvancedSave.Name = "tsbnAdvancedSave";
			this.tsbnAdvancedSave.Size = new System.Drawing.Size(23, 22);
			this.tsbnAdvancedSave.Text = "Save";
			this.tsbnAdvancedSave.Click += new System.EventHandler(this.tsbnAdvancedSave_Click);
			// 
			// tsNodes
			// 
			this.tsNodes.AutoSize = false;
			this.tsNodes.Dock = System.Windows.Forms.DockStyle.Left;
			this.tsNodes.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsNodes.ImageScalingSize = new System.Drawing.Size(14, 14);
			this.tsNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbnNodesSave});
			this.tsNodes.Location = new System.Drawing.Point(0, 0);
			this.tsNodes.Name = "tsNodes";
			this.tsNodes.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.tsNodes.Size = new System.Drawing.Size(32, 131);
			this.tsNodes.TabIndex = 1;
			this.tsNodes.Text = "toolStrip1";
			// 
			// tsbnNodesSave
			// 
			this.tsbnNodesSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbnNodesSave.Image = global::Plugin.Browser.Properties.Resources.FileSave;
			this.tsbnNodesSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbnNodesSave.Margin = new System.Windows.Forms.Padding(0);
			this.tsbnNodesSave.Name = "tsbnNodesSave";
			this.tsbnNodesSave.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.tsbnNodesSave.Size = new System.Drawing.Size(30, 20);
			this.tsbnNodesSave.Text = "&Save";
			this.tsbnNodesSave.Click += new System.EventHandler(this.tsbnNodesSave_Click);
			// 
			// cmsAdvancedTemplate
			// 
			this.cmsAdvancedTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdvancedEdit});
			this.cmsAdvancedTemplate.Name = "cmsAdvancedTemplate";
			this.cmsAdvancedTemplate.Size = new System.Drawing.Size(104, 26);
			this.cmsAdvancedTemplate.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.cmsAdvancedTemplate_Closed);
			this.cmsAdvancedTemplate.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsAdvancedTemplate_ItemClicked);
			// 
			// tsmiAdvancedEdit
			// 
			this.tsmiAdvancedEdit.Name = "tsmiAdvancedEdit";
			this.tsmiAdvancedEdit.Size = new System.Drawing.Size(103, 22);
			this.tsmiAdvancedEdit.Text = "Edit...";
			// 
			// DocumentBrowserWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitBrowser);
			this.Controls.Add(pnlHead);
			this.Name = "DocumentBrowserWizard";
			this.Size = new System.Drawing.Size(526, 346);
			pnlHead.ResumeLayout(false);
			pnlHead.PerformLayout();
			ssNodePath.ResumeLayout(false);
			ssNodePath.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
			this.splitBrowser.Panel1.ResumeLayout(false);
			this.splitBrowser.Panel2.ResumeLayout(false);
			this.splitBrowser.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitBrowser)).EndInit();
			this.splitBrowser.ResumeLayout(false);
			this.splitAdvancedEdit.Panel1.ResumeLayout(false);
			this.splitAdvancedEdit.Panel2.ResumeLayout(false);
			this.splitAdvancedEdit.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitAdvancedEdit)).EndInit();
			this.splitAdvancedEdit.ResumeLayout(false);
			this.tsAdvanced.ResumeLayout(false);
			this.tsAdvanced.PerformLayout();
			this.tsNodes.ResumeLayout(false);
			this.tsNodes.PerformLayout();
			this.cmsAdvancedTemplate.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bnFfwd;
		private System.Windows.Forms.Button bnBack;
		private System.Windows.Forms.Button bnNavigate;
		private System.Windows.Forms.TextBox txtNavigate;
		private Microsoft.Web.WebView2.WinForms.WebView2 webView;
		private System.Windows.Forms.SplitContainer splitBrowser;
		private System.Windows.Forms.FlowLayoutPanel flowNodes;
		private System.Windows.Forms.ToolStrip tsNodes;
		private System.Windows.Forms.ToolStripButton tsbnNodesSave;
		private System.Windows.Forms.ToolStripStatusLabel tsslNodePath;
		private System.Windows.Forms.ToolTip ttNodes;
		private System.Windows.Forms.ContextMenuStrip cmsAdvancedTemplate;
		private System.Windows.Forms.ToolStripMenuItem tsmiAdvancedEdit;
		private System.Windows.Forms.SplitContainer splitAdvancedEdit;
		private System.Windows.Forms.ListView lvAdvancedNodes;
		private System.Windows.Forms.ToolStripTextBox txtAdvancedPath;
		private System.Windows.Forms.ToolStripButton tsbnAdvancedSave;
		private System.Windows.Forms.ToolStrip tsAdvanced;
	}
}
