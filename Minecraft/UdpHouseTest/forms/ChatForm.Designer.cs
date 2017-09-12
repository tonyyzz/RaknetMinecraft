namespace UdpHouseTest
{
	partial class ChatForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.lblHouseId = new System.Windows.Forms.Label();
			this.tbxPlayerList = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbxChatContent = new System.Windows.Forms.TextBox();
			this.tbxContent = new System.Windows.Forms.TextBox();
			this.btnSendMsg = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.lblPlayerId = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "房间Id：";
			// 
			// lblHouseId
			// 
			this.lblHouseId.AutoSize = true;
			this.lblHouseId.Location = new System.Drawing.Point(71, 30);
			this.lblHouseId.Name = "lblHouseId";
			this.lblHouseId.Size = new System.Drawing.Size(41, 12);
			this.lblHouseId.TabIndex = 1;
			this.lblHouseId.Text = "label2";
			// 
			// tbxPlayerList
			// 
			this.tbxPlayerList.Location = new System.Drawing.Point(412, 73);
			this.tbxPlayerList.Multiline = true;
			this.tbxPlayerList.Name = "tbxPlayerList";
			this.tbxPlayerList.ReadOnly = true;
			this.tbxPlayerList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbxPlayerList.Size = new System.Drawing.Size(167, 217);
			this.tbxPlayerList.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(412, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "房间玩家列表：";
			// 
			// tbxChatContent
			// 
			this.tbxChatContent.Location = new System.Drawing.Point(14, 55);
			this.tbxChatContent.Multiline = true;
			this.tbxChatContent.Name = "tbxChatContent";
			this.tbxChatContent.ReadOnly = true;
			this.tbxChatContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbxChatContent.Size = new System.Drawing.Size(392, 235);
			this.tbxChatContent.TabIndex = 4;
			// 
			// tbxContent
			// 
			this.tbxContent.Location = new System.Drawing.Point(14, 298);
			this.tbxContent.Multiline = true;
			this.tbxContent.Name = "tbxContent";
			this.tbxContent.Size = new System.Drawing.Size(298, 55);
			this.tbxContent.TabIndex = 2;
			this.tbxContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxContent_KeyDown);
			// 
			// btnSendMsg
			// 
			this.btnSendMsg.ImageKey = "(无)";
			this.btnSendMsg.Location = new System.Drawing.Point(318, 321);
			this.btnSendMsg.Name = "btnSendMsg";
			this.btnSendMsg.Size = new System.Drawing.Size(88, 32);
			this.btnSendMsg.TabIndex = 6;
			this.btnSendMsg.Text = "发送";
			this.btnSendMsg.UseVisualStyleBackColor = true;
			this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "玩家Id：";
			// 
			// lblPlayerId
			// 
			this.lblPlayerId.AutoSize = true;
			this.lblPlayerId.Location = new System.Drawing.Point(72, 9);
			this.lblPlayerId.Name = "lblPlayerId";
			this.lblPlayerId.Size = new System.Drawing.Size(41, 12);
			this.lblPlayerId.TabIndex = 7;
			this.lblPlayerId.Text = "label3";
			// 
			// ChatForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(597, 366);
			this.Controls.Add(this.lblPlayerId);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnSendMsg);
			this.Controls.Add(this.tbxContent);
			this.Controls.Add(this.tbxChatContent);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbxPlayerList);
			this.Controls.Add(this.lblHouseId);
			this.Controls.Add(this.label1);
			this.Name = "ChatForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "聊天室";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
			this.Load += new System.EventHandler(this.ChatForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatForm_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblHouseId;
		private System.Windows.Forms.TextBox tbxPlayerList;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbxChatContent;
		private System.Windows.Forms.TextBox tbxContent;
		private System.Windows.Forms.Button btnSendMsg;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblPlayerId;
	}
}