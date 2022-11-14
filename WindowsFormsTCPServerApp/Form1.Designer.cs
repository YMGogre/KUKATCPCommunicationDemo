namespace WindowsFormsTCPServerApp
{
    partial class FormTCPServer
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmi_Start = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Setup = new System.Windows.Forms.ToolStripMenuItem();
            this.tstb_Port = new System.Windows.Forms.ToolStripTextBox();
            this.tbLog = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbSend = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Start,
            this.tsmi_Close,
            this.tsmi_Setup});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_Start
            // 
            this.tsmi_Start.Name = "tsmi_Start";
            this.tsmi_Start.Size = new System.Drawing.Size(68, 21);
            this.tsmi_Start.Text = "开始监听";
            this.tsmi_Start.Click += new System.EventHandler(this.tsmi_Start_Click);
            // 
            // tsmi_Close
            // 
            this.tsmi_Close.Name = "tsmi_Close";
            this.tsmi_Close.Size = new System.Drawing.Size(68, 21);
            this.tsmi_Close.Text = "停止监听";
            this.tsmi_Close.Click += new System.EventHandler(this.tsmi_Close_Click);
            // 
            // tsmi_Setup
            // 
            this.tsmi_Setup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstb_Port});
            this.tsmi_Setup.Name = "tsmi_Setup";
            this.tsmi_Setup.Size = new System.Drawing.Size(44, 21);
            this.tsmi_Setup.Text = "设置";
            // 
            // tstb_Port
            // 
            this.tstb_Port.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tstb_Port.Name = "tstb_Port";
            this.tstb_Port.Size = new System.Drawing.Size(100, 23);
            this.tstb_Port.Text = "60000";
            // 
            // tbLog
            // 
            this.tbLog.BackColor = System.Drawing.SystemColors.Info;
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(0, 25);
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(800, 425);
            this.tbLog.TabIndex = 5;
            this.tbLog.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Location = new System.Drawing.Point(722, 17);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 80);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbSend);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 350);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送";
            // 
            // tbSend
            // 
            this.tbSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSend.Location = new System.Drawing.Point(3, 17);
            this.tbSend.Name = "tbSend";
            this.tbSend.Size = new System.Drawing.Size(719, 80);
            this.tbSend.TabIndex = 7;
            this.tbSend.Text = "";
            // 
            // FormTCPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormTCPServer";
            this.Text = "TCP服务端";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Start;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Close;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Setup;
        private System.Windows.Forms.ToolStripTextBox tstb_Port;
        private System.Windows.Forms.RichTextBox tbLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbSend;
    }
}

