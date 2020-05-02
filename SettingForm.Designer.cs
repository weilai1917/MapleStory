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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.CkDeveloperMode = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAccountLst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginDataBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataAccountLst.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            this.BtnDelId});
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
            // CkDeveloperMode
            // 
            this.CkDeveloperMode.AutoSize = true;
            this.CkDeveloperMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkDeveloperMode.Location = new System.Drawing.Point(114, 87);
            this.CkDeveloperMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CkDeveloperMode.Name = "CkDeveloperMode";
            this.CkDeveloperMode.Size = new System.Drawing.Size(75, 21);
            this.CkDeveloperMode.TabIndex = 29;
            this.CkDeveloperMode.Text = "调试模式";
            this.CkDeveloperMode.UseVisualStyleBackColor = true;
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
            this.Text = "配置中心";
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
    }
}