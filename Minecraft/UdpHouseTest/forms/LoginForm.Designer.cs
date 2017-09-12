namespace UdpHouseTest
{
	partial class LoginForm
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
			this.tbxId = new System.Windows.Forms.TextBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbxId
			// 
			this.tbxId.Location = new System.Drawing.Point(117, 61);
			this.tbxId.Name = "tbxId";
			this.tbxId.Size = new System.Drawing.Size(100, 21);
			this.tbxId.TabIndex = 0;
			this.tbxId.Text = "1";
			this.tbxId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxId_KeyDown);
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(105, 110);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 1;
			this.btnLogin.Text = "登录";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(70, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "账号：";
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(290, 185);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.tbxId);
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "登录";
			this.Load += new System.EventHandler(this.LoginForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbxId;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Label label1;
	}
}

