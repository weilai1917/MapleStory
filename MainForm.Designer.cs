namespace EasyMaple
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.MenuCenter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BtnStartGameT = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnStartWeb = new System.Windows.Forms.ToolStripMenuItem();
            this.t3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.t1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.t2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnStart = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.BtnStartGame = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tp1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MapleIds = new System.Windows.Forms.ToolStripDropDownButton();
            this.DefaultAccount = new System.Windows.Forms.Label();
            this.MenuCenter.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.MenuCenter;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "吃饱撑死了 274355068";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1_DoubleClick);
            // 
            // MenuCenter
            // 
            this.MenuCenter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnStartGameT,
            this.BtnStartWeb,
            this.t3,
            this.BtnHelp,
            this.BtnSetting,
            this.t1,
            this.BtnClose,
            this.t2,
            this.BtnLogin,
            this.BtnStart});
            this.MenuCenter.Name = "contextMenuStrip1";
            this.MenuCenter.Size = new System.Drawing.Size(181, 198);
            // 
            // BtnStartGameT
            // 
            this.BtnStartGameT.Name = "BtnStartGameT";
            this.BtnStartGameT.Size = new System.Drawing.Size(180, 22);
            this.BtnStartGameT.Text = "启动测试服";
            this.BtnStartGameT.Click += new System.EventHandler(this.BtnStartGameT_Click);
            // 
            // BtnStartWeb
            // 
            this.BtnStartWeb.Name = "BtnStartWeb";
            this.BtnStartWeb.Size = new System.Drawing.Size(180, 22);
            this.BtnStartWeb.Text = "网页启动";
            this.BtnStartWeb.Click += new System.EventHandler(this.BtnStartWeb_Click);
            // 
            // t3
            // 
            this.t3.Name = "t3";
            this.t3.Size = new System.Drawing.Size(177, 6);
            // 
            // BtnHelp
            // 
            this.BtnHelp.Name = "BtnHelp";
            this.BtnHelp.Size = new System.Drawing.Size(180, 22);
            this.BtnHelp.Text = "帮助文档";
            this.BtnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // BtnSetting
            // 
            this.BtnSetting.Name = "BtnSetting";
            this.BtnSetting.Size = new System.Drawing.Size(180, 22);
            this.BtnSetting.Text = "设置中心";
            this.BtnSetting.Click += new System.EventHandler(this.BtnSetting_Click);
            // 
            // t1
            // 
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(177, 6);
            // 
            // BtnClose
            // 
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(180, 22);
            this.BtnClose.Text = "关闭程序";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // t2
            // 
            this.t2.Name = "t2";
            this.t2.Size = new System.Drawing.Size(177, 6);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(180, 22);
            this.BtnLogin.Text = "重新登录";
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(180, 22);
            this.BtnStart.Text = "启动游戏";
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(762, 124);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 250);
            this.webBrowser1.TabIndex = 13;
            this.webBrowser1.Visible = false;
            // 
            // BtnStartGame
            // 
            this.BtnStartGame.BackColor = System.Drawing.Color.White;
            this.BtnStartGame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnStartGame.BackgroundImage")));
            this.BtnStartGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnStartGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnStartGame.Enabled = false;
            this.BtnStartGame.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnStartGame.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnStartGame.Location = new System.Drawing.Point(0, 0);
            this.BtnStartGame.Margin = new System.Windows.Forms.Padding(0);
            this.BtnStartGame.Name = "BtnStartGame";
            this.BtnStartGame.Size = new System.Drawing.Size(284, 119);
            this.BtnStartGame.TabIndex = 0;
            this.BtnStartGame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnStartGame.UseVisualStyleBackColor = false;
            this.BtnStartGame.Click += new System.EventHandler(this.BtnStartGame_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblStatus,
            this.tp1,
            this.MapleIds});
            this.statusStrip1.Location = new System.Drawing.Point(0, 119);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LblStatus
            // 
            this.LblStatus.BackColor = System.Drawing.Color.White;
            this.LblStatus.ForeColor = System.Drawing.Color.Black;
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(56, 17);
            this.LblStatus.Text = "状态消息";
            // 
            // tp1
            // 
            this.tp1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tp1.Name = "tp1";
            this.tp1.Size = new System.Drawing.Size(184, 17);
            this.tp1.Spring = true;
            // 
            // MapleIds
            // 
            this.MapleIds.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MapleIds.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MapleIds.Image = ((System.Drawing.Image)(resources.GetObject("MapleIds.Image")));
            this.MapleIds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MapleIds.Name = "MapleIds";
            this.MapleIds.Size = new System.Drawing.Size(29, 20);
            this.MapleIds.ToolTipText = "点击快速切换";
            this.MapleIds.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MapleIds_DropDownItemClicked);
            // 
            // DefaultAccount
            // 
            this.DefaultAccount.AutoSize = true;
            this.DefaultAccount.BackColor = System.Drawing.Color.Transparent;
            this.DefaultAccount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DefaultAccount.ForeColor = System.Drawing.Color.Black;
            this.DefaultAccount.Location = new System.Drawing.Point(12, 9);
            this.DefaultAccount.Name = "DefaultAccount";
            this.DefaultAccount.Size = new System.Drawing.Size(56, 17);
            this.DefaultAccount.TabIndex = 17;
            this.DefaultAccount.Text = "简爱冒险";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.DefaultAccount);
            this.Controls.Add(this.BtnStartGame);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.webBrowser1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "简爱冒险（Q群：908378560）";
            this.MinimumSizeChanged += new System.EventHandler(this.MainForm_MinimumSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MenuCenter.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip MenuCenter;
        private System.Windows.Forms.ToolStripMenuItem BtnClose;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button BtnStartGame;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LblStatus;
        private System.Windows.Forms.ToolStripMenuItem BtnSetting;
        private System.Windows.Forms.ToolStripMenuItem BtnLogin;
        private System.Windows.Forms.ToolStripSeparator t2;
        private System.Windows.Forms.ToolStripMenuItem BtnStart;
        private System.Windows.Forms.Label DefaultAccount;
        private System.Windows.Forms.ToolStripSeparator t1;
        private System.Windows.Forms.ToolStripMenuItem BtnStartWeb;
        private System.Windows.Forms.ToolStripMenuItem BtnHelp;
        private System.Windows.Forms.ToolStripMenuItem BtnStartGameT;
        private System.Windows.Forms.ToolStripSeparator t3;
        private System.Windows.Forms.ToolStripDropDownButton MapleIds;
        private System.Windows.Forms.ToolStripStatusLabel tp1;
    }
}

