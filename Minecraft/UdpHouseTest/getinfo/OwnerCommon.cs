using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest
{
	/// <summary>
	/// 房主相关的功用方法
	/// </summary>
	public class OwnerCommon
	{
		/// <summary>
		/// 刷新服务器和客户端的界面信息
		/// </summary>
		public static void RefreshPlayerList(bool isServer = true)
		{
			if (isServer)
			{
				List<Model.PlayerModel> pList = new List<Model.PlayerModel>();
				//房主信息
				pList.Add(new Model.PlayerModel() { Id = PlayerMng.player.Id, Name = PlayerMng.player.Name + "(房主)" });
				//其他玩家信息
				pList.AddRange(UdpServerManager.Instance.GetAllOnlinePlayers());
				//展示在界面上

				ThreadPool.QueueUserWorkItem(o =>
				{
					if (CommonForm.Obj.chatForm.IsHandleCreated)
						CommonForm.Obj.chatForm.BeginInvoke(new Action<int, long, List<Model.PlayerModel>>(CommonForm.Obj.chatForm.ShowPlayerInfoList), new object[] {
						PlayerMng.player.Id,
						PlayerMng.OwnerPlayerHouse.Id,
						pList
					});
				});

				//发送给udp客户端
				Package pack = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgPlayerInfoToClient);
				pack.Write(PlayerMng.OwnerPlayerHouse.Id);
				pack.Write(pList.Count());
				pList.ForEach(item =>
				{
					pack.Write(item.Id);
					pack.Write(item.Name);
				});
				UdpServerManager.Instance?.SendAllClient(pack);
			}
		}
	}
}
