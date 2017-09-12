using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 朋友列表
	/// </summary>
	class FriendListPacket : Package
	{
		public FriendListPacket() { }
		public FriendListPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendListPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_list);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			var friendList = BLL.FriendBLL.GetFriendList(player.Id);
			foreach (var friend in friendList)
			{
				if (PlayerManager.playerOnlineList.Any(m => m.Id == friend.FriendId))
				{
					friend.isOnline = true;
				}
				else
				{
					friend.isOnline = false;
				}
			}
			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_list_ret);
			pack.Write(key);
			pack.Write(friendList.Count()); //总人数
			//pack.Write(friendList.Count(m => m.isOnline)); //在线人数
			friendList.ForEach(item =>
			{
				pack.Write(item);
			});
			player.SendMsg(pack);
		}
	}
}
