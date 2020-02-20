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
            this.CkDeveloperMode = new System.Windows.Forms.CheckBox();
            this.CkValidProgramName = new System.Windows.Forms.CheckBox();
            this.CkKoreaSystem = new System.Windows.Forms.CheckBox();
            this.CkProxyIsOther = new System.Windows.Forms.CheckBox();
            this.MapleBtn = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnWatchLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtMaplePath
            // 
            this.TxtMaplePath.Enabled = false;
            this.TxtMaplePath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtMaplePath.Location = new System.Drawing.Point(8, 32);
            this.TxtMaplePath.Multiline = true;
            this.TxtMaplePath.Name = "TxtMaplePath";
            this.TxtMaplePath.Size = new System.Drawing.Size(288, 41);
            this.TxtMaplePath.TabIndex = 11;
            this.TxtMaplePath.TabStop = false;
            this.TxtMaplePath.TextChanged += new System.EventHandler(this.TxtMaplePath_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(9, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "冒险岛路径：";
            // 
            // CkDeveloperMode
            // 
            this.CkDeveloperMode.AutoSize = true;
            this.CkDeveloperMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkDeveloperMode.Location = new System.Drawing.Point(12, 81);
            this.CkDeveloperMode.Name = "CkDeveloperMode";
            this.CkDeveloperMode.Size = new System.Drawing.Size(75, 21);
            this.CkDeveloperMode.TabIndex = 15;
            this.CkDeveloperMode.Text = "调试模式";
            this.CkDeveloperMode.UseVisualStyleBackColor = true;
            this.CkDeveloperMode.CheckedChanged += new System.EventHandler(this.CkDeveloperMode_CheckedChanged);
            // 
            // CkValidProgramName
            // 
            this.CkValidProgramName.AutoSize = true;
            this.CkValidProgramName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkValidProgramName.Location = new System.Drawing.Point(12, 108);
            this.CkValidProgramName.Name = "CkValidProgramName";
            this.CkValidProgramName.Size = new System.Drawing.Size(281, 21);
            this.CkValidProgramName.TabIndex = 16;
            this.CkValidProgramName.Text = "不校验启动文件名为MapleStory.exe(新人勿点)";
            this.CkValidProgramName.UseVisualStyleBackColor = true;
            this.CkValidProgramName.CheckedChanged += new System.EventHandler(this.CkValidProgramName_CheckedChanged);
            // 
            // CkKoreaSystem
            // 
            this.CkKoreaSystem.AutoSize = true;
            this.CkKoreaSystem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkKoreaSystem.Location = new System.Drawing.Point(12, 230);
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
            this.CkProxyIsOther.Location = new System.Drawing.Point(12, 135);
            this.CkProxyIsOther.Name = "CkProxyIsOther";
            this.CkProxyIsOther.Size = new System.Drawing.Size(219, 21);
            this.CkProxyIsOther.TabIndex = 18;
            this.CkProxyIsOther.Text = "代理是加速器，如果启动很慢可勾选";
            this.CkProxyIsOther.UseVisualStyleBackColor = true;
            this.CkProxyIsOther.CheckedChanged += new System.EventHandler(this.CkProxyIsOther_CheckedChanged);
            // 
            // MapleBtn
            // 
            this.MapleBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MapleBtn.Location = new System.Drawing.Point(183, 4);
            this.MapleBtn.Name = "MapleBtn";
            this.MapleBtn.Size = new System.Drawing.Size(113, 25);
            this.MapleBtn.TabIndex = 19;
            this.MapleBtn.Text = "浏览";
            this.MapleBtn.UseVisualStyleBackColor = true;
            this.MapleBtn.Click += new System.EventHandler(this.MapleBtn_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnReset.Location = new System.Drawing.Point(183, 162);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(113, 37);
            this.BtnReset.TabIndex = 21;
            this.BtnReset.Text = "恢复默认配置";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(9, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 37);
            this.label1.TabIndex = 22;
            this.label1.Text = "如果需要卸载游戏，请先点击恢复默认注册表更改";
            // 
            // BtnWatchLog
            // 
            this.BtnWatchLog.Location = new System.Drawing.Point(183, 79);
            this.BtnWatchLog.Name = "BtnWatchLog";
            this.BtnWatchLog.Size = new System.Drawing.Size(113, 25);
            this.BtnWatchLog.TabIndex = 23;
            this.BtnWatchLog.Text = "查看日志";
            this.BtnWatchLog.UseVisualStyleBackColor = true;
            this.BtnWatchLog.Click += new System.EventHandler(this.BtnWatchLog_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(304, 301);
            this.Controls.Add(this.BtnWatchLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.MapleBtn);
            this.Controls.Add(this.CkProxyIsOther);
            this.Controls.Add(this.CkKoreaSystem);
            this.Controls.Add(this.CkValidProgramName);
            this.Controls.Add(this.CkDeveloperMode);
            this.Controls.Add(this.TxtMaplePath);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TxtMaplePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CkDeveloperMode;
        private System.Windows.Forms.CheckBox CkValidProgramName;
        private System.Windows.Forms.CheckBox CkKoreaSystem;
        private System.Windows.Forms.CheckBox CkProxyIsOther;
        private System.Windows.Forms.Button MapleBtn;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnWatchLog;
    }
}