using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 搜索添加好友 获取列表信息
	/// </summary>
	class FriendSearchAddListPacket : Package
	{
		public FriendSearchAddListPacket() { }
		public FriendSearchAddListPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendSearchAddListPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_searchAdd);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			string friendKeywords = ReadString(); //玩家的名称或者Id

			if (string.IsNullOrWhiteSpace(friendKeywords))
			{
				return;
			}
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			friendKeywords = friendKeywords.GetRemoveExcessSpaceStr();
			int friendId = 0; int.TryParse(friendKeywords, out friendId);
			int flag = 0;
			var friendSearchAddInfo = BLL.FriendBLL.GetFriendSearchAddIncludeFriendTop1List(player.Id, friendId, friendKeywords);
			if (friendSearchAddInfo != null)
			{
				if (PlayerManager.playerOnlineList.Any(m => m.Id == friendSearchAddInfo.FriendId))
				{
					friendSearchAddInfo.isOnline = true;
				}
				else
				{
					friendSearchAddInfo.isOnline = false;
				}
				flag = BLL.FriendBLL.IsFriendShip(player.Id, friendSearchAddInfo.FriendId) ? 1 : 2;
			}
			else
			{
				flag = 3; //玩家不存在
			}
			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_searchAdd_ret);
			pack.Write(key);
			pack.Write(flag); //返回说明：1：玩家存在并是好友关系，2：玩家存在但不是好友关系，3：玩家不存在
			if (flag != 3)
			{
				pack.Write(friendSearchAddInfo);
			}
			player.SendMsg(pack);
		}
	}
}
