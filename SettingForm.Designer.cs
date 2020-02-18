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
            this.BtnSave = new System.Windows.Forms.Button();
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
            this.TxtMaplePath.Location = new System.Drawing.Point(6, 25);
            this.TxtMaplePath.Name = "TxtMaplePath";
            this.TxtMaplePath.Size = new System.Drawing.Size(211, 23);
            this.TxtMaplePath.TabIndex = 11;
            this.TxtMaplePath.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "冒险岛路径：";
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSave.Location = new System.Drawing.Point(6, 259);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(285, 37);
            this.BtnSave.TabIndex = 14;
            this.BtnSave.Text = "保存";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CkDeveloperMode
            // 
            this.CkDeveloperMode.AutoSize = true;
            this.CkDeveloperMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkDeveloperMode.Location = new System.Drawing.Point(8, 55);
            this.CkDeveloperMode.Name = "CkDeveloperMode";
            this.CkDeveloperMode.Size = new System.Drawing.Size(75, 21);
            this.CkDeveloperMode.TabIndex = 15;
            this.CkDeveloperMode.Text = "调试模式";
            this.CkDeveloperMode.UseVisualStyleBackColor = true;
            // 
            // CkValidProgramName
            // 
            this.CkValidProgramName.AutoSize = true;
            this.CkValidProgramName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkValidProgramName.Location = new System.Drawing.Point(8, 121);
            this.CkValidProgramName.Name = "CkValidProgramName";
            this.CkValidProgramName.Size = new System.Drawing.Size(281, 21);
            this.CkValidProgramName.TabIndex = 16;
            this.CkValidProgramName.Text = "不校验启动文件名为MapleStory.exe(新人勿点)";
            this.CkValidProgramName.UseVisualStyleBackColor = true;
            // 
            // CkKoreaSystem
            // 
            this.CkKoreaSystem.AutoSize = true;
            this.CkKoreaSystem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkKoreaSystem.Location = new System.Drawing.Point(8, 77);
            this.CkKoreaSystem.Name = "CkKoreaSystem";
            this.CkKoreaSystem.Size = new System.Drawing.Size(159, 21);
            this.CkKoreaSystem.TabIndex = 17;
            this.CkKoreaSystem.Text = "当前是韩文语言环境系统";
            this.CkKoreaSystem.UseVisualStyleBackColor = true;
            // 
            // CkProxyIsOther
            // 
            this.CkProxyIsOther.AutoSize = true;
            this.CkProxyIsOther.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CkProxyIsOther.Location = new System.Drawing.Point(8, 99);
            this.CkProxyIsOther.Name = "CkProxyIsOther";
            this.CkProxyIsOther.Size = new System.Drawing.Size(219, 21);
            this.CkProxyIsOther.TabIndex = 18;
            this.CkProxyIsOther.Text = "代理是加速器，如果启动很慢可勾选";
            this.CkProxyIsOther.UseVisualStyleBackColor = true;
            // 
            // MapleBtn
            // 
            this.MapleBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MapleBtn.Location = new System.Drawing.Point(223, 24);
            this.MapleBtn.Name = "MapleBtn";
            this.MapleBtn.Size = new System.Drawing.Size(68, 25);
            this.MapleBtn.TabIndex = 19;
            this.MapleBtn.Text = "浏览";
            this.MapleBtn.UseVisualStyleBackColor = true;
            this.MapleBtn.Click += new System.EventHandler(this.MapleBtn_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnReset.Location = new System.Drawing.Point(158, 159);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(133, 37);
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
            this.label1.Location = new System.Drawing.Point(5, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 37);
            this.label1.TabIndex = 22;
            this.label1.Text = "如果需要卸载游戏，请先点击恢复默认注册表更改";
            // 
            // BtnWatchLog
            // 
            this.BtnWatchLog.Location = new System.Drawing.Point(158, 54);
            this.BtnWatchLog.Name = "BtnWatchLog";
            this.BtnWatchLog.Size = new System.Drawing.Size(133, 23);
            this.BtnWatchLog.TabIndex = 23;
            this.BtnWatchLog.Text = "查看今天的日志";
            this.BtnWatchLog.UseVisualStyleBackColor = true;
            this.BtnWatchLog.Click += new System.EventHandler(this.BtnWatchLog_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(297, 301);
            this.Controls.Add(this.BtnWatchLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.MapleBtn);
            this.Controls.Add(this.CkProxyIsOther);
            this.Controls.Add(this.CkKoreaSystem);
            this.Controls.Add(this.CkValidProgramName);
            this.Controls.Add(this.CkDeveloperMode);
            this.Controls.Add(this.BtnSave);
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
        private System.Windows.Forms.Button BtnSave;
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