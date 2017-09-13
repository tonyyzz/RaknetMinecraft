using BaseCommon;
using SwigRaknetCS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpHouseTest
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            tbxId.Focus();

            string errorMsg = "";
            bool flag = SwigRaknetCSPreInit.JudgeRaknetRun(out errorMsg);
            if (!flag)
            {
                Debug.WriteLine("------------------------" + errorMsg);
                return;
            }
            Debug.WriteLine("--------------------------Raknet测试通过");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var idstr = tbxId.Text.Trim();
            int id = 0; int.TryParse(idstr, out id);
            if (id <= 0)
            {
                MessageBox.Show("请输入Id");
                tbxId.Text = "";
                tbxId.Focus();
                return;
            }

            MngToolInit.Init();

            TcpClientMng.tcpClient = new TcpClient();
            TcpClientMng.tcpClient.Start();
            TcpClientMng.tcpClient.StartClient(TcpClientMng.hall_server_ip, ushort.Parse(TcpClientMng.hall_server_port));

            Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_login);
            pack.Write(id);
            TcpClientMng.tcpClient.Send(pack);

            PlayerMng.player.Id = id;
        }

        public void LoginSuccess()
        {
            //登录成功
            CommonForm.Obj.loginForm.Hide();
            CommonForm.Obj.playerInfoForm = new PlayerInfoForm();
            CommonForm.Obj.playerInfoForm.Show();


            //登录代理服务器
            UdpAgentClientManager.InitInstance("127.0.0.1", 5558, (player) =>
            {
                ThreadPoolUploadPlayerInfoToUdpServer.Start();
            }, PlayerMng.player);
        }

        public void Islogin()
        {
            MessageBox.Show("正在登录中，请重试别的账号");
        }

        private void tbxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
