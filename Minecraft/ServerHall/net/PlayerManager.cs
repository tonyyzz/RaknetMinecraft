using BaseCommon;
using BLL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall
{
	public static class PlayerManager
	{
		/// <summary>
		/// 存储在线用户集合
		/// </summary>
		public volatile static List<Model.PlayerModel> playerOnlineList = new List<Model.PlayerModel>();
		/// <summary>
		/// 玩家上线
		/// </summary>
		/// <param name="player"></param>
		/// <param name="cid"></param>
		public static void Online(this Model.PlayerModel player, Session session)
		{
			player.online = true;
			player.conID = session.ConnId;
			player.IpAddress = session.IpAddress;
			player.TcpPort = session.Port;
			player.LastLoginTime = DateTime.Now;

			playerOnlineList.RemoveAll(m => m.Id == player.Id);
			playerOnlineList.Add(player);

			#region 注释掉
			//if (!playerOnlineDict.ContainsKey(player.pid))
			//{
			//    playerOnlineDict.TryAdd(player.pid, player);
			//}


			//DateTime now = DateTime.Now;
			//DateTime dt = new DateTime(now.Year, now.Month, now.Day);
			//long offset = dt.Ticks - player.lastLoginTime;
			//if (offset > 0 && offset < csvFile.one_day_ticks)
			//{//昨天有登陆
			//    player.continu_login_day++;
			//}
			//else
			//    player.continu_login_day = 1;

			//if (player.last_online_award_tick == 0)
			//    player.last_online_award_tick = now.Ticks;

			//if (offset > 0)
			//{//今天第一次登陆
			//    player.frist_login_daily = true;
			//    player.lottery_spinMoneyEvday = 0;
			//    player.lottery_bonusNum = 0;
			//}

			//player.lastLoginTime = now.Ticks;

			//Friend friend = null;
			//if (friendDict.TryGetValue(player.pid, out friend))
			//{
			//    friend.online = 1;
			//} 
			#endregion
		}

		/// <summary>
		/// 向玩家发送消息
		/// </summary>
		/// <param name="player"></param>
		/// <param name="pack"></param>
		public static void SendMsg(this Model.PlayerModel player, Package pack)
		{
			if (player == null)
			{
				throw new Exception("玩家实例为空");
			}
			TcpServerManager.Instance.Send(player.conID, pack);
		}

		/// <summary>
		/// 玩家下线
		/// </summary>
		/// <param name="player"></param>
		public static void Offline(this Model.PlayerModel player)
		{
			if (player == null)
			{
				return;
			}

			player.WithdrawFromHouse();

			playerOnlineList.RemoveAll(m => m.Id == player.Id);
			PlayerBLL.UpdateLastLoginTime(player);
			TcpServerManager.Instance.Disconnect(player.conID);
			player.online = false;
			player.conID = IntPtr.Zero;
			player.IpAddress = "";
			player.TcpPort = 0;

			#region 注释掉
			//if (playerOnlineDict.ContainsKey(player.pid))
			//{
			//    Player p = null;
			//    playerOnlineDict.TryRemove(player.pid, out p);
			//}

			//Friend friend = null;
			//if (friendDict.TryGetValue(player.pid, out friend))
			//{
			//    friend.online = 0;
			//}
			/////保存数据
			//SaveThreadManager.Instance.allocSaveThread(player); 
			#endregion
		}
	}
}
