namespace SocketService
{
    partial class ServerForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txt_log = new System.Windows.Forms.ListBox();
            this.txtInputMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnClientForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 36);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(141, 340);
            this.listBox1.TabIndex = 0;
            // 
            // txt_log
            // 
            this.txt_log.FormattingEnabled = true;
            this.txt_log.ItemHeight = 12;
            this.txt_log.Location = new System.Drawing.Point(160, 37);
            this.txt_log.Name = "txt_log";
            this.txt_log.Size = new System.Drawing.Size(474, 340);
            this.txt_log.TabIndex = 1;
            // 
            // txtInputMsg
            // 
            this.txtInputMsg.Location = new System.Drawing.Point(12, 383);
            this.txtInputMsg.Name = "txtInputMsg";
            this.txtInputMsg.Size = new System.Drawing.Size(534, 21);
            this.txtInputMsg.TabIndex = 2;
            this.txtInputMsg.Text = "请输入信息...";
            this.txtInputMsg.Enter += new System.EventHandler(this.txtInputMsg_Enter);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(552, 382);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(82, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发 送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "访问列表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "请求日志";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.NotifyIconClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(580, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "停止";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.BtnStopClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(520, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(54, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "开始";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.BtnStartClick);
            // 
            // btnClientForm
            // 
            this.btnClientForm.Location = new System.Drawing.Point(439, 8);
            this.btnClientForm.Name = "btnClientForm";
            this.btnClientForm.Size = new System.Drawing.Size(75, 23);
            this.btnClientForm.TabIndex = 6;
            this.btnClientForm.Text = "客户端";
            this.btnClientForm.UseVisualStyleBackColor = true;
            this.btnClientForm.Click += new System.EventHandler(this.btnClientForm_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 413);
            this.Controls.Add(this.btnClientForm);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtInputMsg);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ServerForm";
            this.ShowInTaskbar = false;
            this.Text = "服务端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox txt_log;
        private System.Windows.Forms.TextBox txtInputMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnClientForm;
    }
}