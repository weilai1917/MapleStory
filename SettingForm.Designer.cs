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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddMaple = new System.Windows.Forms.ToolStripButton();
            this.btnDelMaple = new System.Windows.Forms.ToolStripButton();
            this.btnSetDefault = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CbSimpleMode = new System.Windows.Forms.CheckBox();
            this.CkDeveloperMode = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAccountLst)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtMaplePath
            // 
            this.TxtMaplePath.Enabled = false;
            this.TxtMaplePath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtMaplePath.Location = new System.Drawing.Point(6, 37);
            this.TxtMaplePath.Multiline = true;
            this.TxtMaplePath.Name = "TxtMaplePath";
            this.TxtMaplePath.Size = new System.Drawing.Size(364, 41);
            this.TxtMaplePath.TabIndex = 11;
            this.TxtMaplePath.TabStop = false;
            this.TxtMaplePath.TextChanged += new System.EventHandler(this.TxtMaplePath_TextChanged);
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
            this.CkKoreaSystem.Location = new System.Drawing.Point(183, 321);
            this.CkKoreaSystem.Name = "CkKoreaSystem";
            this.CkKoreaSystem.Size = new System.Drawing.Size(159, 21);
            this.CkKoreaSystem.TabIndex = 17;
            this.CkKoreaSystem.Text = "当前是韩文语言环境系统";
            this.CkKoreaSystem.UseVisualStyleBackColor = true;
            this.CkKoreaSystem.Visible = false;
            this.CkKoreaSystem.CheckedChanged += new System.EventHandler(this.CkKoreaSystem_CheckedChanged);
            // 
            // CkProxyIsOther
            // 
            this.CkProxyIsOther.AutoSize = true;
            this.CkProxyIsOther.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkProxyIsOther.Location = new System.Drawing.Point(6, 114);
            this.CkProxyIsOther.Name = "CkProxyIsOther";
            this.CkProxyIsOther.Size = new System.Drawing.Size(99, 21);
            this.CkProxyIsOther.TabIndex = 18;
            this.CkProxyIsOther.Text = "代理是加速器";
            this.CkProxyIsOther.UseVisualStyleBackColor = true;
            this.CkProxyIsOther.CheckedChanged += new System.EventHandler(this.CkProxyIsOther_CheckedChanged);
            // 
            // MapleBtn
            // 
            this.MapleBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MapleBtn.Location = new System.Drawing.Point(257, 6);
            this.MapleBtn.Name = "MapleBtn";
            this.MapleBtn.Size = new System.Drawing.Size(113, 25);
            this.MapleBtn.TabIndex = 19;
            this.MapleBtn.Text = "请选择";
            this.MapleBtn.UseVisualStyleBackColor = true;
            this.MapleBtn.Click += new System.EventHandler(this.MapleBtn_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnReset.Location = new System.Drawing.Point(257, 84);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(113, 25);
            this.BtnReset.TabIndex = 21;
            this.BtnReset.Text = "恢复默认配置";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(8, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "卸载游戏前，请先点击右侧“恢复默认设置”";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 225);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataAccountLst);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(376, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "账号管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataAccountLst
            // 
            this.dataAccountLst.AllowUserToOrderColumns = true;
            this.dataAccountLst.BackgroundColor = System.Drawing.Color.White;
            this.dataAccountLst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataAccountLst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataAccountLst.Location = new System.Drawing.Point(3, 28);
            this.dataAccountLst.Name = "dataAccountLst";
            this.dataAccountLst.RowTemplate.Height = 23;
            this.dataAccountLst.Size = new System.Drawing.Size(370, 164);
            this.dataAccountLst.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddMaple,
            this.btnDelMaple,
            this.btnSetDefault});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(370, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddMaple
            // 
            this.btnAddMaple.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddMaple.Image = ((System.Drawing.Image)(resources.GetObject("btnAddMaple.Image")));
            this.btnAddMaple.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddMaple.Name = "btnAddMaple";
            this.btnAddMaple.Size = new System.Drawing.Size(60, 22);
            this.btnAddMaple.Text = "添加账号";
            // 
            // btnDelMaple
            // 
            this.btnDelMaple.Image = ((System.Drawing.Image)(resources.GetObject("btnDelMaple.Image")));
            this.btnDelMaple.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelMaple.Name = "btnDelMaple";
            this.btnDelMaple.Size = new System.Drawing.Size(64, 22);
            this.btnDelMaple.Text = "删除行";
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.Image = ((System.Drawing.Image)(resources.GetObject("btnSetDefault.Image")));
            this.btnSetDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(76, 22);
            this.btnSetDefault.Text = "设为默认";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CbSimpleMode);
            this.tabPage2.Controls.Add(this.CkDeveloperMode);
            this.tabPage2.Controls.Add(this.TxtMaplePath);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.BtnReset);
            this.tabPage2.Controls.Add(this.CkKoreaSystem);
            this.tabPage2.Controls.Add(this.MapleBtn);
            this.tabPage2.Controls.Add(this.CkProxyIsOther);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(376, 195);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "软件设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CbSimpleMode
            // 
            this.CbSimpleMode.AutoSize = true;
            this.CbSimpleMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CbSimpleMode.Location = new System.Drawing.Point(111, 114);
            this.CbSimpleMode.Name = "CbSimpleMode";
            this.CbSimpleMode.Size = new System.Drawing.Size(75, 21);
            this.CbSimpleMode.TabIndex = 30;
            this.CbSimpleMode.Text = "简洁模式";
            this.CbSimpleMode.UseVisualStyleBackColor = true;
            // 
            // CkDeveloperMode
            // 
            this.CkDeveloperMode.AutoSize = true;
            this.CkDeveloperMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkDeveloperMode.Location = new System.Drawing.Point(192, 114);
            this.CkDeveloperMode.Name = "CkDeveloperMode";
            this.CkDeveloperMode.Size = new System.Drawing.Size(75, 21);
            this.CkDeveloperMode.TabIndex = 29;
            this.CkDeveloperMode.Text = "调试模式";
            this.CkDeveloperMode.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 225);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配置与帮助";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAccountLst)).EndInit();
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
        private System.Windows.Forms.ToolStripButton btnAddMaple;
        private System.Windows.Forms.DataGridView dataAccountLst;
        private System.Windows.Forms.ToolStripButton btnDelMaple;
        private System.Windows.Forms.ToolStripButton btnSetDefault;
        private System.Windows.Forms.CheckBox CbSimpleMode;
        private System.Windows.Forms.CheckBox CkDeveloperMode;
    }
}