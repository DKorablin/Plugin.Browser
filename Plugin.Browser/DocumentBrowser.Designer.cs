namespace Plugin.Browser
{
	partial class DocumentBrowser
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Panel pnlHead;
			System.Windows.Forms.Label label1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentBrowser));
			this.bnFfwd = new System.Windows.Forms.Button();
			this.bnBack = new System.Windows.Forms.Button();
			this.bnNavigate = new System.Windows.Forms.Button();
			this.txtNavigate = new System.Windows.Forms.TextBox();
			this.ilImages = new System.Windows.Forms.ImageList(this.components);
			this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
			this.ttNodes = new System.Windows.Forms.ToolTip(this.components);
			pnlHead = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			pnlHead.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
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
			pnlHead.TabIndex = 0;
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
			this.bnNavigate.ImageList = this.ilImages;
			this.bnNavigate.Location = new System.Drawing.Point(483, 4);
			this.bnNavigate.Name = "bnNavigate";
			this.bnNavigate.Size = new System.Drawing.Size(40, 22);
			this.bnNavigate.TabIndex = 2;
			this.bnNavigate.UseVisualStyleBackColor = true;
			this.bnNavigate.Click += new System.EventHandler(this.bnNavigate_Click);
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
			// ilImages
			// 
			this.ilImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilImages.ImageStream")));
			this.ilImages.TransparentColor = System.Drawing.Color.Magenta;
			this.ilImages.Images.SetKeyName(0, "iconNavigate.bmp");
			// 
			// webView
			// 
			this.webView.AllowExternalDrop = true;
			this.webView.CreationProperties = null;
			this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
			this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webView.Location = new System.Drawing.Point(0, 35);
			this.webView.Name = "webView";
			this.webView.Size = new System.Drawing.Size(526, 311);
			this.webView.TabIndex = 1;
			this.webView.ZoomFactor = 1D;
			this.webView.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.WebView_CoreWebView2InitializationCompleted);
			this.webView.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.WebView_NavigationCompleted);
			this.webView.NavigationStarting += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(this.WebView_NavigationStarting);
			// 
			// DocumentBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.webView);
			this.Controls.Add(pnlHead);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DocumentBrowser";
			this.Size = new System.Drawing.Size(526, 346);
			pnlHead.ResumeLayout(false);
			pnlHead.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtNavigate;
		private Microsoft.Web.WebView2.WinForms.WebView2 webView;
		private System.Windows.Forms.Button bnNavigate;
		private System.Windows.Forms.ImageList ilImages;
		private System.Windows.Forms.Button bnFfwd;
		private System.Windows.Forms.Button bnBack;
		private System.Windows.Forms.ToolTip ttNodes;
	}
}