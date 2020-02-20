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
            this.LoginBtn = new System.Windows.Forms.ToolStripSplitButton();
            this.AddAcountBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.AccountList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.StartWebSite = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.TxtLog = new System.Windows.Forms.RichTextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.MapleIds = new System.Windows.Forms.ToolStripDropDownButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnStartGame = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginBtn
            // 
            this.LoginBtn.AutoToolTip = false;
            this.LoginBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAcountBtn,
            this.AccountList,
            this.toolStripSeparator2,
            this.StartWebSite});
            this.LoginBtn.Image = ((System.Drawing.Image)(resources.GetObject("LoginBtn.Image")));
            this.LoginBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(88, 22);
            this.LoginBtn.Text = "登录冒险";
            this.LoginBtn.ButtonClick += new System.EventHandler(this.LoginBtn_ButtonClick);
            // 
            // AddAcountBtn
            // 
            this.AddAcountBtn.Image = ((System.Drawing.Image)(resources.GetObject("AddAcountBtn.Image")));
            this.AddAcountBtn.Name = "AddAcountBtn";
            this.AddAcountBtn.Size = new System.Drawing.Size(180, 22);
            this.AddAcountBtn.Text = "添加 Naver 账号";
            this.AddAcountBtn.Click += new System.EventHandler(this.AddAcountBtn_Click);
            // 
            // AccountList
            // 
            this.AccountList.Image = ((System.Drawing.Image)(resources.GetObject("AccountList.Image")));
            this.AccountList.Name = "AccountList";
            this.AccountList.Size = new System.Drawing.Size(167, 22);
            this.AccountList.Text = "账号列表";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // StartWebSite
            // 
            this.StartWebSite.Image = ((System.Drawing.Image)(resources.GetObject("StartWebSite.Image")));
            this.StartWebSite.Name = "StartWebSite";
            this.StartWebSite.ShowShortcutKeys = false;
            this.StartWebSite.Size = new System.Drawing.Size(167, 22);
            this.StartWebSite.Text = "网页启动游戏";
            this.StartWebSite.Click += new System.EventHandler(this.StartWebSite_Click);
            // 
            // SettingBtn
            // 
            this.SettingBtn.AutoToolTip = false;
            this.SettingBtn.Image = ((System.Drawing.Image)(resources.GetObject("SettingBtn.Image")));
            this.SettingBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SettingBtn.Name = "SettingBtn";
            this.SettingBtn.Size = new System.Drawing.Size(52, 22);
            this.SettingBtn.Text = "设置";
            this.SettingBtn.Click += new System.EventHandler(this.SettingBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnHelp
            // 
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(66, 22);
            this.btnHelp.Text = "帮助(&L)";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // TxtLog
            // 
            this.TxtLog.BackColor = System.Drawing.Color.White;
            this.TxtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtLog.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtLog.Location = new System.Drawing.Point(0, 93);
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.ReadOnly = true;
            this.TxtLog.Size = new System.Drawing.Size(428, 516);
            this.TxtLog.TabIndex = 11;
            this.TxtLog.Text = "";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "吃饱撑死了 274355068";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem2.Text = "启动游戏";
            this.toolStripMenuItem2.Visible = false;
            this.toolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem1.Text = "关闭程序";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
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
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginBtn,
            this.SettingBtn,
            this.toolStripSeparator1,
            this.btnHelp,
            this.MapleIds});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(428, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MapleIds
            // 
            this.MapleIds.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MapleIds.AutoToolTip = false;
            this.MapleIds.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MapleIds.Image = ((System.Drawing.Image)(resources.GetObject("MapleIds.Image")));
            this.MapleIds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MapleIds.Name = "MapleIds";
            this.MapleIds.Size = new System.Drawing.Size(45, 22);
            this.MapleIds.Text = "子号";
            this.MapleIds.Visible = false;
            this.MapleIds.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MapleIds_DropDownItemClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnStartGame);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(428, 68);
            this.panel1.TabIndex = 15;
            // 
            // BtnStartGame
            // 
            this.BtnStartGame.BackColor = System.Drawing.Color.White;
            this.BtnStartGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnStartGame.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnStartGame.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnStartGame.Location = new System.Drawing.Point(2, 2);
            this.BtnStartGame.Name = "BtnStartGame";
            this.BtnStartGame.Size = new System.Drawing.Size(424, 64);
            this.BtnStartGame.TabIndex = 0;
            this.BtnStartGame.Text = "启动游戏";
            this.BtnStartGame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnStartGame.UseVisualStyleBackColor = false;
            this.BtnStartGame.Click += new System.EventHandler(this.BtnStartGame_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(428, 609);
            this.Controls.Add(this.TxtLog);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.webBrowser1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "简爱冒险（Q群：908378560）";
            this.MinimumSizeChanged += new System.EventHandler(this.MainForm_MinimumSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.RichTextBox TxtLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripSplitButton LoginBtn;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripMenuItem AddAcountBtn;
        private System.Windows.Forms.ToolStripMenuItem AccountList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem StartWebSite;
        private System.Windows.Forms.ToolStripButton SettingBtn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton MapleIds;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnStartGame;
    }
}

