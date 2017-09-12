using BaseCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpHouseTest
{
	public partial class PlayerInfoForm : Form
	{
		public PlayerInfoForm()
		{
			InitializeComponent();
			Init();
		}

		void Init()
		{
			if (PlayerMng.player != null)
			{
				lblId.Text = PlayerMng.player.Id.ToString();
				lblName.Text = PlayerMng.player.Name.ToString();
				lblSex.Text = PlayerMng.player.Sex.ToString();
				lblMoney.Text = PlayerMng.player.Money.ToString();
			}
			GetInfoCommon.ShowHouseList();

		}

		private void PlayerInfoForm_Load(object sender, EventArgs e)
		{
			tbxHouseId.Focus();
		}

		private void PlayerInfoForm_FormClosing(object sender, FormClosingEventArgs e)
		{

			if (e.CloseReason == CloseReason.UserClosing)
			{
				DialogResult r = MessageBox.Show("确定要退出吗?", "操作提示",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
				if (r == DialogResult.OK)
				{
					e.Cancel = false;

					ThreadPool.QueueUserWorkItem(o =>
					{
						CommonForm.Option.ExitProgram();
					});
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		private void btnCreateHouse_Click(object sender, EventArgs e)
		{
			string houeseName = "playerHouse" + PlayerMng.player.Id; //房间名称
			string description = "测试房间描述" + PlayerMng.player.Id; //房间描述
			string housePwd = PlayerMng.player.Id.ToString(); //房间密码
			int tagId = 1; //传地图标签Id
			int houseSize = 6; //房间大小（1-6 的数字）
			int resourceId = 1; //资源Id  【可能是本地资源，暂放】

			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_create);
			pack.Write(houeseName);
			pack.Write(description);
			pack.Write(housePwd);
			pack.Write(tagId);
			pack.Write(houseSize);
			pack.Write(resourceId);
			TcpClientMng.tcpClient.Send(pack);

		}

		public void CreateHouseSuccess(byte flag)
		{
			if (flag == 1)
			{
				//获取房间信息
				Package pack2 = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_info);
				pack2.Write(PlayerMng.OwnerPlayerHouse.Id);
				TcpClientMng.tcpClient.Send(pack2);
			}
			else
			{
				MessageBox.Show("创建房间失败");
			}
		}

		public void ShowHouseList(List<Model.PlayerHouseModel> houseList)
		{
			tbxHouseList.Text = string.Join(" - ", "房间号", "房间名称", "房间人数", "房间Port");
			string str = "";
			foreach (var item in houseList)
			{
				str += item.Id + " - " + item.Name + " - " + item.playerCount + " - " + item.HouseUdpPort + Environment.NewLine;
			}
			tbxHouseList.Text = tbxHouseList.Text + str;

			var house = houseList.FirstOrDefault();
			if (house != null)
			{
				tbxHouseId.Text = house.Id.ToString();
			}
		}

		private void btnHouseJoin_Click(object sender, EventArgs e)
		{
			string houseIdStr = tbxHouseId.Text.Trim();
			long houseId = 0; long.TryParse(houseIdStr, out houseId);
			if (houseId <= 0)
			{
				MessageBox.Show("请输入正确的房间号");
				tbxHouseId.Text = "";
				tbxHouseId.Focus();
				return;
			}
			//房间信息同步tcp udp（先加入tcp房间）
			//加入房间tcp
			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_join);
			pack.Write(houseId);
			TcpClientMng.tcpClient.Send(pack);
		}

		public void StartUdpServer()
		{
			//CommonForm.Obj.playerInfoForm.Hide();
			CommonForm.Obj.playerInfoForm.Visible = false;
			//CommonForm.Obj.playerInfoForm?.Dispose(true);
			CommonForm.Obj.chatForm = new ChatForm();
			CommonForm.Obj.chatForm.Show();
			GC.Collect();
		}

		public void HouseJoin(int flag, int udpPort)
		{
			switch (flag)
			{
				case 1: //加入房间成功
					{
						//CommonForm.Obj.playerInfoForm.Hide();
						CommonForm.Obj.playerInfoForm.Visible = false;
						//CommonForm.Obj.playerInfoForm?.Dispose(true);
						CommonForm.Obj.chatForm = new ChatForm(udpPort);
						CommonForm.Obj.chatForm.Show();
						GC.Collect();
					}
					break;
				case 2: //房间满员
					{
						MessageBox.Show("房间满员");
					}
					break;
				case 3: //玩家不许加入自己的房间
					{
						MessageBox.Show("玩家不许加入自己的房间");
					}
					break;
				case 4: //房间不存在
					{
						MessageBox.Show("房间不存在");
					}
					break;
				default:
					{
						MessageBox.Show("flag非法");
					}
					break;
			}
		}



		private void btnRefresh_Click(object sender, EventArgs e)
		{
			GetInfoCommon.ShowHouseList();
		}
	}
}
