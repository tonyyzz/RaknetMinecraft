using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 好友聊天接口
	/// </summary>
	class FriendChatPacket : Package
	{
		public FriendChatPacket() { }
		public FriendChatPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendChatPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_chat);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int friendId = ReadInt(); //聊天好友Id
			string chatContent = ReadString(); //聊天信息

			if (friendId <= 0)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(chatContent))
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			//目前只支持在线聊天
			var friend = PlayerManager.playerOnlineList.FirstOrDefault(m => m.Id == friendId);
			if (friend == null)
			{
				friend = BLL.PlayerBLL.QuerySingle(friendId);
				if (friend == null)
				{
					return;
				}
			}

			chatContent = chatContent.GetRemoveExcessSpaceStr();
			if (chatContent.Length > 200)
			{
				return;
			}

			Package packOther = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_chat_ret_other);
			packOther.Write(player.Id); //发送消息的玩家Id
			packOther.Write(chatContent);
			friend.SendMsg(packOther);

			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_chat_ret);
			pack.Write(key);
			pack.Write(friend.online ? 1 : 0); //对方是否在线
			player.SendMsg(pack);
		}
	}
}
