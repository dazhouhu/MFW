namespace MFW.WF
{
    partial class CallWindow
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
            this.components = new System.ComponentModel.Container();
            this.pnlView = new System.Windows.Forms.Panel();
            this.pnlToolBars = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMore = new System.Windows.Forms.Button();
            this.btnAttender = new System.Windows.Forms.Button();
            this.btnShare = new System.Windows.Forms.Button();
            this.btnCamera = new System.Windows.Forms.Button();
            this.btnSpeaker = new System.Windows.Forms.Button();
            this.btnMic = new System.Windows.Forms.Button();
            this.tbSpeakerVolume = new System.Windows.Forms.TrackBar();
            this.moreMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemDTMF = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFECC = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSmartWin = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMicVolume = new System.Windows.Forms.TrackBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pnlView.SuspendLayout();
            this.pnlToolBars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeakerVolume)).BeginInit();
            this.moreMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMicVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlView
            // 
            this.pnlView.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnlView.Controls.Add(this.tbSpeakerVolume);
            this.pnlView.Controls.Add(this.tbMicVolume);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlView.Location = new System.Drawing.Point(0, 0);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(784, 562);
            this.pnlView.TabIndex = 0;
            // 
            // pnlToolBars
            // 
            this.pnlToolBars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlToolBars.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlToolBars.Controls.Add(this.btnExit);
            this.pnlToolBars.Controls.Add(this.btnMore);
            this.pnlToolBars.Controls.Add(this.btnAttender);
            this.pnlToolBars.Controls.Add(this.btnShare);
            this.pnlToolBars.Controls.Add(this.btnCamera);
            this.pnlToolBars.Controls.Add(this.btnSpeaker);
            this.pnlToolBars.Controls.Add(this.btnMic);
            this.pnlToolBars.Location = new System.Drawing.Point(0, 482);
            this.pnlToolBars.Name = "pnlToolBars";
            this.pnlToolBars.Size = new System.Drawing.Size(784, 80);
            this.pnlToolBars.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.Image = global::MFW.WF.Properties.Resources.exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(702, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 60);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMore
            // 
            this.btnMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnMore.FlatAppearance.BorderSize = 0;
            this.btnMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMore.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMore.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnMore.Image = global::MFW.WF.Properties.Resources.more;
            this.btnMore.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMore.Location = new System.Drawing.Point(570, 10);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(70, 60);
            this.btnMore.TabIndex = 0;
            this.btnMore.Text = "更多";
            this.btnMore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // btnAttender
            // 
            this.btnAttender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnAttender.FlatAppearance.BorderSize = 0;
            this.btnAttender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttender.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAttender.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAttender.Image = global::MFW.WF.Properties.Resources.attender;
            this.btnAttender.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAttender.Location = new System.Drawing.Point(490, 10);
            this.btnAttender.Name = "btnAttender";
            this.btnAttender.Size = new System.Drawing.Size(70, 60);
            this.btnAttender.TabIndex = 0;
            this.btnAttender.Text = "参会人";
            this.btnAttender.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAttender.UseVisualStyleBackColor = true;
            this.btnAttender.Click += new System.EventHandler(this.btnAttender_Click);
            // 
            // btnShare
            // 
            this.btnShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnShare.FlatAppearance.BorderSize = 0;
            this.btnShare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShare.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShare.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnShare.Image = global::MFW.WF.Properties.Resources.share;
            this.btnShare.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnShare.Location = new System.Drawing.Point(400, 10);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(80, 60);
            this.btnShare.TabIndex = 0;
            this.btnShare.Text = "发起共享";
            this.btnShare.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnShare.UseVisualStyleBackColor = true;
            this.btnShare.Click += new System.EventHandler(this.btnShare_Click);
            // 
            // btnCamera
            // 
            this.btnCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCamera.FlatAppearance.BorderSize = 0;
            this.btnCamera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCamera.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCamera.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCamera.Image = global::MFW.WF.Properties.Resources.camera;
            this.btnCamera.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCamera.Location = new System.Drawing.Point(320, 10);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(70, 60);
            this.btnCamera.TabIndex = 0;
            this.btnCamera.Text = "摄像头";
            this.btnCamera.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCamera.UseVisualStyleBackColor = true;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // btnSpeaker
            // 
            this.btnSpeaker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnSpeaker.FlatAppearance.BorderSize = 0;
            this.btnSpeaker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpeaker.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSpeaker.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSpeaker.Image = global::MFW.WF.Properties.Resources.speaker;
            this.btnSpeaker.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSpeaker.Location = new System.Drawing.Point(240, 10);
            this.btnSpeaker.Name = "btnSpeaker";
            this.btnSpeaker.Size = new System.Drawing.Size(70, 60);
            this.btnSpeaker.TabIndex = 0;
            this.btnSpeaker.Text = "扬声器";
            this.btnSpeaker.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSpeaker.UseVisualStyleBackColor = true;
            this.btnSpeaker.Click += new System.EventHandler(this.btnSpeaker_Click);
            // 
            // btnMic
            // 
            this.btnMic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnMic.FlatAppearance.BorderSize = 0;
            this.btnMic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMic.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMic.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnMic.Image = global::MFW.WF.Properties.Resources.mic;
            this.btnMic.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMic.Location = new System.Drawing.Point(160, 10);
            this.btnMic.Name = "btnMic";
            this.btnMic.Size = new System.Drawing.Size(70, 60);
            this.btnMic.TabIndex = 0;
            this.btnMic.Text = "麦克风";
            this.btnMic.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMic.UseVisualStyleBackColor = true;
            this.btnMic.Click += new System.EventHandler(this.btnMic_Click);
            // 
            // tbSpeakerVolume
            // 
            this.tbSpeakerVolume.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbSpeakerVolume.Location = new System.Drawing.Point(264, 362);
            this.tbSpeakerVolume.Maximum = 128;
            this.tbSpeakerVolume.Name = "tbSpeakerVolume";
            this.tbSpeakerVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbSpeakerVolume.Size = new System.Drawing.Size(45, 128);
            this.tbSpeakerVolume.TabIndex = 0;
            this.tbSpeakerVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSpeakerVolume.Visible = false;
            this.tbSpeakerVolume.ValueChanged += new System.EventHandler(this.tbSpeakerVolume_ValueChanged);
            // 
            // moreMenu
            // 
            this.moreMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDTMF,
            this.menuItemFECC,
            this.menuItemSmartWin});
            this.moreMenu.Name = "moreMenu";
            this.moreMenu.Size = new System.Drawing.Size(154, 70);
            // 
            // menuItemDTMF
            // 
            this.menuItemDTMF.Name = "menuItemDTMF";
            this.menuItemDTMF.Size = new System.Drawing.Size(153, 22);
            this.menuItemDTMF.Text = "DTMF报号盘";
            // 
            // menuItemFECC
            // 
            this.menuItemFECC.Name = "menuItemFECC";
            this.menuItemFECC.Size = new System.Drawing.Size(153, 22);
            this.menuItemFECC.Text = "FECC远程控制";
            // 
            // menuItemSmartWin
            // 
            this.menuItemSmartWin.Name = "menuItemSmartWin";
            this.menuItemSmartWin.Size = new System.Drawing.Size(153, 22);
            this.menuItemSmartWin.Text = "显示小黄页";
            // 
            // tbMicVolume
            // 
            this.tbMicVolume.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbMicVolume.Location = new System.Drawing.Point(184, 362);
            this.tbMicVolume.Maximum = 128;
            this.tbMicVolume.Name = "tbMicVolume";
            this.tbMicVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbMicVolume.Size = new System.Drawing.Size(45, 128);
            this.tbMicVolume.TabIndex = 0;
            this.tbMicVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMicVolume.Visible = false;
            this.tbMicVolume.ValueChanged += new System.EventHandler(this.tbMicVolume_ValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // CallWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pnlToolBars);
            this.Controls.Add(this.pnlView);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "CallWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CallWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CallWindow_FormClosing);
            this.Load += new System.EventHandler(this.CallWindow_Load);
            this.pnlView.ResumeLayout(false);
            this.pnlView.PerformLayout();
            this.pnlToolBars.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeakerVolume)).EndInit();
            this.moreMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbMicVolume)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlView;
        private System.Windows.Forms.Panel pnlToolBars;
        private System.Windows.Forms.Button btnMic;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Button btnAttender;
        private System.Windows.Forms.Button btnShare;
        private System.Windows.Forms.Button btnCamera;
        private System.Windows.Forms.Button btnSpeaker;
        private System.Windows.Forms.TrackBar tbSpeakerVolume;
        private System.Windows.Forms.ContextMenuStrip moreMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemDTMF;
        private System.Windows.Forms.ToolStripMenuItem menuItemFECC;
        private System.Windows.Forms.ToolStripMenuItem menuItemSmartWin;
        private System.Windows.Forms.TrackBar tbMicVolume;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}