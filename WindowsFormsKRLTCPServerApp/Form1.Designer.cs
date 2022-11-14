namespace WindowsFormsKRLTCPServerApp
{
    partial class Form1
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.richTextBox_Send = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextBox_Receive = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIP = new System.Windows.Forms.ToolStripMenuItem();
            this.tstbIP = new System.Windows.Forms.ToolStripTextBox();
            this.tsmiPort = new System.Windows.Forms.ToolStripMenuItem();
            this.tstbPort = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btn_Send);
            this.groupBox2.Controls.Add(this.richTextBox_Send);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 350);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(697, 22);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(97, 35);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清空日志框";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Send.Location = new System.Drawing.Point(697, 59);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(97, 35);
            this.btn_Send.TabIndex = 1;
            this.btn_Send.Text = "发送";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // richTextBox_Send
            // 
            this.richTextBox_Send.Location = new System.Drawing.Point(3, 17);
            this.richTextBox_Send.Name = "richTextBox_Send";
            this.richTextBox_Send.Size = new System.Drawing.Size(688, 80);
            this.richTextBox_Send.TabIndex = 0;
            this.richTextBox_Send.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextBox_Receive);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(800, 325);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "日志";
            // 
            // richTextBox_Receive
            // 
            this.richTextBox_Receive.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox_Receive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Receive.Location = new System.Drawing.Point(3, 17);
            this.richTextBox_Receive.Name = "richTextBox_Receive";
            this.richTextBox_Receive.ReadOnly = true;
            this.richTextBox_Receive.Size = new System.Drawing.Size(794, 305);
            this.richTextBox_Receive.TabIndex = 0;
            this.richTextBox_Receive.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStart,
            this.tsmiIP,
            this.tsmiPort});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiStart
            // 
            this.tsmiStart.Name = "tsmiStart";
            this.tsmiStart.Size = new System.Drawing.Size(68, 21);
            this.tsmiStart.Text = "开启监听";
            this.tsmiStart.Click += new System.EventHandler(this.tsmiStart_Click);
            // 
            // tsmiIP
            // 
            this.tsmiIP.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbIP});
            this.tsmiIP.Name = "tsmiIP";
            this.tsmiIP.Size = new System.Drawing.Size(115, 21);
            this.tsmiIP.Text = "设置服务端ip地址";
            // 
            // tstbIP
            // 
            this.tstbIP.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tstbIP.Name = "tstbIP";
            this.tstbIP.ReadOnly = true;
            this.tstbIP.Size = new System.Drawing.Size(100, 23);
            this.tstbIP.Text = "127.0.0.1";
            this.tstbIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstbIP_KeyPress);
            this.tstbIP.DoubleClick += new System.EventHandler(this.tstbIP_DoubleClick);
            // 
            // tsmiPort
            // 
            this.tsmiPort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbPort});
            this.tsmiPort.Name = "tsmiPort";
            this.tsmiPort.Size = new System.Drawing.Size(116, 21);
            this.tsmiPort.Text = "设置服务端端口号";
            // 
            // tstbPort
            // 
            this.tstbPort.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tstbPort.Name = "tstbPort";
            this.tstbPort.ReadOnly = true;
            this.tstbPort.Size = new System.Drawing.Size(100, 23);
            this.tstbPort.Text = "59152";
            this.tstbPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstbPort_KeyPress);
            this.tstbPort.DoubleClick += new System.EventHandler(this.tstbPort_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "KRL_TCP服务端";
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.RichTextBox richTextBox_Send;
        private System.Windows.Forms.RichTextBox richTextBox_Receive;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiStart;
        private System.Windows.Forms.ToolStripMenuItem tsmiIP;
        private System.Windows.Forms.ToolStripTextBox tstbIP;
        private System.Windows.Forms.ToolStripMenuItem tsmiPort;
        private System.Windows.Forms.ToolStripTextBox tstbPort;
        private System.Windows.Forms.Button btnClear;
    }
}

