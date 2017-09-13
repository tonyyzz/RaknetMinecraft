using BaseCommon;
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
    public partial class ChatForm : Form
    {

        /// <summary>
        /// 启动udp服务器
        /// </summary>
        public ChatForm()
        {
            InitializeComponent();
            //启动udp服务器
            //房间人数，房主信息
            Debug.WriteLine("本地udp启动监听成功");
            int port = UdpServerManager.Instance.GetPort();
            //上传端口
            Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_portSet);
            pack.Write(port);
            TcpClientMng.tcpClient.Send(pack);

            ShowPlayerInfoList(PlayerMng.player.Id, PlayerMng.OwnerPlayerHouse.Id, new List<Model.PlayerModel>() { PlayerMng.player });

            this.Text += "（服务器）";
            isServer = true;

            ThreadPoolSendMsgQueue.Start();
        }



        /// <summary>
        /// 启动udp客户端
        /// </summary>
        /// <param name="udpPort"></param>
        public ChatForm(int udpPort)
        {
            InitializeComponent();
            UdpClientManager.Instance = null;
            UdpClientManager.InitInstance("127.0.0.1", udpPort,
            (player) =>
            {
                //将玩家信息上传至udp服务器
                Package pack = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgPlayerLoginUdpServer);
                pack.Write(PlayerMng.player.Id);
                pack.Write(PlayerMng.player.Name);
                UdpClientManager.Instance?.Send(pack);

            }, PlayerMng.player);
            this.Text += "（客户端）";
            isServer = false;
            ThreadPoolClientSendMsgToServer.Start();
        }

        /// <summary>
        /// 判断服务器与客户端的标志
        /// </summary>
        private static bool isServer = false;
        /// <summary>
        /// 是否是主动关闭
        /// </summary>
        private bool isInitiativeClose = true;

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            string content = tbxContent.Text.Trim();
            if (string.IsNullOrWhiteSpace(content))
            {
                tbxContent.Text = "";
                tbxContent.Focus();
                return;
            }
            tbxContent.Text = "";
            if (isServer)
            {
                AppendMsg(PlayerMng.player.Name + "(房主)", content);
                Package pack = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToClient);
                pack.Write(PlayerMng.player.Name + "(房主)");
                pack.Write(content);
                UdpServerManager.Instance.SendAllClient(pack);

                ThreadPoolSendMsgQueue.Enqueue(new Model.MsgSendQueue()
                {
                    timeSrart = DateTime.Now,
                    pack = pack,
                    sendUdpServerMainC = MainCommand.MC_P2P,
                    sendUdpServerSecondC = SecondCommand.SC_P2P_msgTextToClient,
                    receiveUdpServerMainC = MainCommand.MC_P2P,
                    receiveUdpServerSecondC = SecondCommand.SC_P2P_msgTextToClientRet,
                    sendUdpAgentMainC = MainCommand.MC_UDPAGENT,
                    sendUdpAgentSecondC = SecondCommand.SC_UDPAGENT_testUdpServerToUdpAgentServer,
                });
            }
            else
            {
                Package pack = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToServer);
                pack.Write(PlayerMng.player.Name);
                pack.Write(content);
                UdpClientManager.Instance?.Send(pack);

                pack.Write(PlayerMng.player.Id); //发送消息者的Id
                ThreadPoolSendMsgQueue.Enqueue(new Model.MsgSendQueue()
                {
                    timeSrart = DateTime.Now,
                    pack = pack,
                    sendUdpServerMainC = MainCommand.MC_P2P,
                    sendUdpServerSecondC = SecondCommand.SC_P2P_msgTextToServer,
                    receiveUdpServerMainC = MainCommand.MC_P2P,
                    receiveUdpServerSecondC = SecondCommand.SC_P2P_msgTextToClient,
                    sendUdpAgentMainC = MainCommand.MC_UDPAGENT,
                    sendUdpAgentSecondC = SecondCommand.SC_UDPAGENT_test,
                });
            }
        }


        public void AppendMsg(string playerName, string message)
        {
            tbxChatContent.AppendText(playerName + ": " + message + Environment.NewLine);//追加文本
            tbxChatContent.ScrollToCaret();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 关闭房间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isInitiativeClose)
            {
                CloseThisWindow();
            }
            else
            {
                DialogResult dr = MessageBox.Show("确定要退出房间吗?", "退出",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    ThreadPoolSendMsgQueue.Stop();
                    ThreadPoolClientSendMsgToServer.Stop();
                    e.Cancel = false;  //点击OK  
                    CloseThisWindow();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void CloseThisWindow()
        {
            Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_close);
            pack.Write((byte)(isServer ? 1 : 2));
            TcpClientMng.tcpClient.Send(pack);

            if (isServer)//房主
            {
                //通知房间内的其他玩家关闭udp服务器
                Package pack4 = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgServerCloseToClient);
                UdpServerManager.Instance.SendAllClient(pack4);
                //关闭udp服务
                UdpServerManager.Instance.Stop();
            }
            else//不是房主
            {
                //通知udp服务器
                Package pack3 = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgPlayerLeaveToServer);
                pack3.Write(PlayerMng.player.Id);
                UdpClientManager.Instance?.Send(pack3);
                //退出房间
                UdpClientManager.Instance?.Close();
                Package pack2 = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_close);
                pack2.Write((byte)(2));
                TcpClientMng.tcpClient.Send(pack2);
            }
            CommonForm.Obj.chatForm.Hide();
            CommonForm.Obj.chatForm.Dispose();
            //CommonForm.Obj.playerInfoForm?.Dispose();
            //CommonForm.Obj.playerInfoForm = null;
            //CommonForm.Obj.playerInfoForm = new PlayerInfoForm();
            //CommonForm.Obj.playerInfoForm.Show();
            CommonForm.Obj.playerInfoForm.Visible = true;

            GC.Collect();
        }

        public void ShowPlayerInfoList(int playerId, long houseId, List<Model.PlayerModel> pList)
        {
            lblPlayerId.Text = playerId.ToString();
            lblHouseId.Text = houseId.ToString();

            StringBuilder sbr = new StringBuilder();
            pList.ForEach(item =>
            {
                sbr.AppendLine(string.Format(@"{0}({1})", item.Name, item.Id));
            });
            tbxPlayerList.Text = sbr.ToString();
        }

        public void HouseClose(byte isServer)
        {
            switch (isServer)
            {
                case 1: //房主
                    {
                        OwnerCommon.RefreshPlayerList();
                    }
                    break;
                case 2: //不是房主
                    {
                        isInitiativeClose = false;
                        CommonForm.Obj.chatForm.Close();
                        CommonForm.Obj.chatForm.Dispose();
                        GC.Collect();
                    }
                    break;
            }
        }

        private void btnUdpAgentTest_Click(object sender, EventArgs e)
        {



            //Package pack = new Package(MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_test);
            //pack.Write(PlayerMng.player.Id);
            //pack.Write((byte)6);
            //pack.Write((byte)7);
            //UdpAgentClientManager.Instance.Send(pack);


        }

        private void ChatForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbxContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendMsg.PerformClick();
            }
        }
    }
}
