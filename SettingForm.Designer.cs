namespace EasyMaple
{
    partial class SettingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.TxtMaplePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CkKoreaSystem = new System.Windows.Forms.CheckBox();
            this.CkProxyIsOther = new System.Windows.Forms.CheckBox();
            this.MapleBtn = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataAccountLst = new System.Windows.Forms.DataGridView();
            this.guidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountTagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountCookieStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isDefaultDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.loginDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnAddNaverId = new System.Windows.Forms.ToolStripButton();
            this.BtnDelId = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CkNotAutoLogin = new System.Windows.Forms.CheckBox();
            this.CkNotLoadMapleIds = new System.Windows.Forms.CheckBox();
            this.CkTestWord = new System.Windows.Forms.CheckBox();
            this.CkAutoReLogin = new System.Windows.Forms.CheckBox();
            this.CkDeveloperMode = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnUnitQQ = new System.Windows.Forms.ToolStripSplitButton();
            this.BtnAsync = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnGetServer = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnShare = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAccountLst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginDataBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtMaplePath
            // 
            this.TxtMaplePath.Enabled = false;
            this.TxtMaplePath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtMaplePath.Location = new System.Drawing.Point(3, 35);
            this.TxtMaplePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtMaplePath.Multiline = true;
            this.TxtMaplePath.Name = "TxtMaplePath";
            this.TxtMaplePath.Size = new System.Drawing.Size(370, 44);
            this.TxtMaplePath.TabIndex = 11;
            this.TxtMaplePath.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "冒险岛路径：";
            // 
            // CkKoreaSystem
            // 
            this.CkKoreaSystem.AutoSize = true;
            this.CkKoreaSystem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkKoreaSystem.Location = new System.Drawing.Point(213, 455);
            this.CkKoreaSystem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CkKoreaSystem.Name = "CkKoreaSystem";
            this.CkKoreaSystem.Size = new System.Drawing.Size(159, 21);
            this.CkKoreaSystem.TabIndex = 17;
            this.CkKoreaSystem.Text = "当前是韩文语言环境系统";
            this.CkKoreaSystem.UseVisualStyleBackColor = true;
            this.CkKoreaSystem.Visible = false;
            // 
            // CkProxyIsOther
            // 
            this.CkProxyIsOther.AutoSize = true;
            this.CkProxyIsOther.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkProxyIsOther.Location = new System.Drawing.Point(9, 87);
            this.CkProxyIsOther.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CkProxyIsOther.Name = "CkProxyIsOther";
            this.CkProxyIsOther.Size = new System.Drawing.Size(99, 21);
            this.CkProxyIsOther.TabIndex = 18;
            this.CkProxyIsOther.Text = "代理是加速器";
            this.CkProxyIsOther.UseVisualStyleBackColor = true;
            // 
            // MapleBtn
            // 
            this.MapleBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MapleBtn.Location = new System.Drawing.Point(241, 3);
            this.MapleBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MapleBtn.Name = "MapleBtn";
            this.MapleBtn.Size = new System.Drawing.Size(132, 30);
            this.MapleBtn.TabIndex = 19;
            this.MapleBtn.Text = "请选择";
            this.MapleBtn.UseVisualStyleBackColor = true;
            this.MapleBtn.Click += new System.EventHandler(this.MapleBtn_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnReset.Location = new System.Drawing.Point(241, 182);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(132, 30);
            this.BtnReset.TabIndex = 21;
            this.BtnReset.Text = "恢复注册表";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(245, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "卸载游戏前，请先点击";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 246);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataAccountLst);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(376, 216);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "账号管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataAccountLst
            // 
            this.dataAccountLst.AllowUserToOrderColumns = true;
            this.dataAccountLst.AllowUserToResizeRows = false;
            this.dataAccountLst.AutoGenerateColumns = false;
            this.dataAccountLst.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataAccountLst.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataAccountLst.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataAccountLst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataAccountLst.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.guidDataGridViewTextBoxColumn,
            this.accountTagDataGridViewTextBoxColumn,
            this.accountCookieStrDataGridViewTextBoxColumn,
            this.addTimeDataGridViewTextBoxColumn,
            this.isDefaultDataGridViewCheckBoxColumn});
            this.dataAccountLst.DataSource = this.loginDataBindingSource;
            this.dataAccountLst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataAccountLst.Location = new System.Drawing.Point(3, 29);
            this.dataAccountLst.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataAccountLst.Name = "dataAccountLst";
            this.dataAccountLst.ReadOnly = true;
            this.dataAccountLst.RowHeadersVisible = false;
            this.dataAccountLst.RowTemplate.Height = 23;
            this.dataAccountLst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataAccountLst.Size = new System.Drawing.Size(370, 183);
            this.dataAccountLst.TabIndex = 0;
            this.dataAccountLst.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataAccountLst_CellContentClick);
            this.dataAccountLst.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataAccountLst_CurrentCellDirtyStateChanged);
            // 
            // guidDataGridViewTextBoxColumn
            // 
            this.guidDataGridViewTextBoxColumn.DataPropertyName = "Guid";
            this.guidDataGridViewTextBoxColumn.HeaderText = "Guid";
            this.guidDataGridViewTextBoxColumn.Name = "guidDataGridViewTextBoxColumn";
            this.guidDataGridViewTextBoxColumn.ReadOnly = true;
            this.guidDataGridViewTextBoxColumn.Visible = false;
            // 
            // accountTagDataGridViewTextBoxColumn
            // 
            this.accountTagDataGridViewTextBoxColumn.DataPropertyName = "AccountTag";
            this.accountTagDataGridViewTextBoxColumn.HeaderText = "Naver昵称";
            this.accountTagDataGridViewTextBoxColumn.Name = "accountTagDataGridViewTextBoxColumn";
            this.accountTagDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // accountCookieStrDataGridViewTextBoxColumn
            // 
            this.accountCookieStrDataGridViewTextBoxColumn.DataPropertyName = "AccountCookieStr";
            this.accountCookieStrDataGridViewTextBoxColumn.HeaderText = "AccountCookieStr";
            this.accountCookieStrDataGridViewTextBoxColumn.Name = "accountCookieStrDataGridViewTextBoxColumn";
            this.accountCookieStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.accountCookieStrDataGridViewTextBoxColumn.Visible = false;
            // 
            // addTimeDataGridViewTextBoxColumn
            // 
            this.addTimeDataGridViewTextBoxColumn.DataPropertyName = "AddTime";
            this.addTimeDataGridViewTextBoxColumn.HeaderText = "添加时间";
            this.addTimeDataGridViewTextBoxColumn.Name = "addTimeDataGridViewTextBoxColumn";
            this.addTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isDefaultDataGridViewCheckBoxColumn
            // 
            this.isDefaultDataGridViewCheckBoxColumn.DataPropertyName = "IsDefault";
            this.isDefaultDataGridViewCheckBoxColumn.HeaderText = "是否默认";
            this.isDefaultDataGridViewCheckBoxColumn.Name = "isDefaultDataGridViewCheckBoxColumn";
            this.isDefaultDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // loginDataBindingSource
            // 
            this.loginDataBindingSource.DataSource = typeof(EasyMaple.LoginData);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnAddNaverId,
            this.BtnDelId,
            this.BtnUnitQQ});
            this.toolStrip1.Location = new System.Drawing.Point(3, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(370, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnAddNaverId
            // 
            this.BtnAddNaverId.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnAddNaverId.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnAddNaverId.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddNaverId.Image")));
            this.BtnAddNaverId.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnAddNaverId.Name = "BtnAddNaverId";
            this.BtnAddNaverId.Size = new System.Drawing.Size(60, 22);
            this.BtnAddNaverId.Text = "添加账号";
            this.BtnAddNaverId.Click += new System.EventHandler(this.BtnAddNaverId_Click);
            // 
            // BtnDelId
            // 
            this.BtnDelId.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnDelId.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelId.Image")));
            this.BtnDelId.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnDelId.Name = "BtnDelId";
            this.BtnDelId.Size = new System.Drawing.Size(48, 22);
            this.BtnDelId.Text = "删除行";
            this.BtnDelId.Click += new System.EventHandler(this.BtnDelId_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CkNotAutoLogin);
            this.tabPage2.Controls.Add(this.CkNotLoadMapleIds);
            this.tabPage2.Controls.Add(this.CkTestWord);
            this.tabPage2.Controls.Add(this.CkAutoReLogin);
            this.tabPage2.Controls.Add(this.CkDeveloperMode);
            this.tabPage2.Controls.Add(this.TxtMaplePath);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.BtnReset);
            this.tabPage2.Controls.Add(this.CkKoreaSystem);
            this.tabPage2.Controls.Add(this.MapleBtn);
            this.tabPage2.Controls.Add(this.CkProxyIsOther);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(376, 216);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "软件设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CkNotAutoLogin
            // 
            this.CkNotAutoLogin.AutoSize = true;
            this.CkNotAutoLogin.Location = new System.Drawing.Point(114, 115);
            this.CkNotAutoLogin.Name = "CkNotAutoLogin";
            this.CkNotAutoLogin.Size = new System.Drawing.Size(135, 21);
            this.CkNotAutoLogin.TabIndex = 33;
            this.CkNotAutoLogin.Text = "启动程序不自动登录";
            this.CkNotAutoLogin.UseVisualStyleBackColor = true;
            // 
            // CkNotLoadMapleIds
            // 
            this.CkNotLoadMapleIds.AutoSize = true;
            this.CkNotLoadMapleIds.Location = new System.Drawing.Point(9, 115);
            this.CkNotLoadMapleIds.Name = "CkNotLoadMapleIds";
            this.CkNotLoadMapleIds.Size = new System.Drawing.Size(87, 21);
            this.CkNotLoadMapleIds.TabIndex = 32;
            this.CkNotLoadMapleIds.Text = "不加载子号";
            this.CkNotLoadMapleIds.UseVisualStyleBackColor = true;
            // 
            // CkTestWord
            // 
            this.CkTestWord.AutoSize = true;
            this.CkTestWord.Location = new System.Drawing.Point(253, 115);
            this.CkTestWord.Name = "CkTestWord";
            this.CkTestWord.Size = new System.Drawing.Size(63, 21);
            this.CkTestWord.TabIndex = 31;
            this.CkTestWord.Text = "测试服";
            this.CkTestWord.UseVisualStyleBackColor = true;
            this.CkTestWord.Visible = false;
            // 
            // CkAutoReLogin
            // 
            this.CkAutoReLogin.AutoSize = true;
            this.CkAutoReLogin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkAutoReLogin.Location = new System.Drawing.Point(114, 87);
            this.CkAutoReLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CkAutoReLogin.Name = "CkAutoReLogin";
            this.CkAutoReLogin.Size = new System.Drawing.Size(123, 21);
            this.CkAutoReLogin.TabIndex = 30;
            this.CkAutoReLogin.Text = "启动游戏重新登录";
            this.CkAutoReLogin.UseVisualStyleBackColor = true;
            // 
            // CkDeveloperMode
            // 
            this.CkDeveloperMode.AutoSize = true;
            this.CkDeveloperMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkDeveloperMode.Location = new System.Drawing.Point(253, 87);
            this.CkDeveloperMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CkDeveloperMode.Name = "CkDeveloperMode";
            this.CkDeveloperMode.Size = new System.Drawing.Size(75, 21);
            this.CkDeveloperMode.TabIndex = 29;
            this.CkDeveloperMode.Text = "调试模式";
            this.CkDeveloperMode.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.pictureBox2);
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(376, 216);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "关于";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(365, 57);
            this.label3.TabIndex = 1;
            this.label3.Text = "    使用问题，请查看帮助文档。仍然不会使用，可进付费群交流，所以阅读好文档，可以省一点哦。当然了也欢迎大佬直接打赏，先谢谢了。软件会不定期更新点小内容，帮助新" +
    "人快速的熟悉游戏。\r\n";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(188, 71);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(145, 142);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(41, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 142);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Firebrick;
            this.label2.Location = new System.Drawing.Point(49, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "软件使用完全免费，如有人利用、收费，请鄙视他。";
            // 
            // BtnUnitQQ
            // 
            this.BtnUnitQQ.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnAsync,
            this.BtnGetServer,
            this.BtnShare});
            this.BtnUnitQQ.Image = ((System.Drawing.Image)(resources.GetObject("BtnUnitQQ.Image")));
            this.BtnUnitQQ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnUnitQQ.Name = "BtnUnitQQ";
            this.BtnUnitQQ.Size = new System.Drawing.Size(88, 22);
            this.BtnUnitQQ.Text = "账号关联";
            this.BtnUnitQQ.ButtonClick += new System.EventHandler(this.BtnUnitQQ_ButtonClick);
            // 
            // BtnAsync
            // 
            this.BtnAsync.Name = "BtnAsync";
            this.BtnAsync.Size = new System.Drawing.Size(180, 22);
            this.BtnAsync.Text = "同步设置";
            this.BtnAsync.Click += new System.EventHandler(this.BtnAsync_Click);
            // 
            // BtnGetServer
            // 
            this.BtnGetServer.Name = "BtnGetServer";
            this.BtnGetServer.Size = new System.Drawing.Size(180, 22);
            this.BtnGetServer.Text = "更新设置";
            this.BtnGetServer.Click += new System.EventHandler(this.BtnGetServer_Click);
            // 
            // BtnShare
            // 
            this.BtnShare.Name = "BtnShare";
            this.BtnShare.Size = new System.Drawing.Size(180, 22);
            this.BtnShare.Text = "分享选中账号";
            this.BtnShare.Click += new System.EventHandler(this.BtnShare_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 246);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置中心";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAccountLst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginDataBindingSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TxtMaplePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CkKoreaSystem;
        private System.Windows.Forms.CheckBox CkProxyIsOther;
        private System.Windows.Forms.Button MapleBtn;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtnAddNaverId;
        private System.Windows.Forms.DataGridView dataAccountLst;
        private System.Windows.Forms.ToolStripButton BtnDelId;
        private System.Windows.Forms.CheckBox CkDeveloperMode;
        private System.Windows.Forms.BindingSource loginDataBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn guidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountTagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountCookieStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isDefaultDataGridViewCheckBoxColumn;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox CkAutoReLogin;
        private System.Windows.Forms.CheckBox CkTestWord;
        private System.Windows.Forms.CheckBox CkNotLoadMapleIds;
        private System.Windows.Forms.CheckBox CkNotAutoLogin;
        private System.Windows.Forms.ToolStripSplitButton BtnUnitQQ;
        private System.Windows.Forms.ToolStripMenuItem BtnAsync;
        private System.Windows.Forms.ToolStripMenuItem BtnGetServer;
        private System.Windows.Forms.ToolStripMenuItem BtnShare;
    }
}