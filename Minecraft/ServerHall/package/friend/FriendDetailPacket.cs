using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 好友详情
	/// </summary>
	class FriendDetailPacket : Package
	{
		public FriendDetailPacket() { }
		public FriendDetailPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendDetailPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_detail);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int friendId = ReadInt(); //好友Id
			if (friendId <= 0)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			if (friendId == player.Id)
			{
				return;
			}

			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_detail_ret);
			pack.Write(key);
			var friendInfo = BLL.FriendBLL.GetFriendDetail(player.Id, friendId);
			pack.Write(friendInfo != null ? 1 : 0); //好友是否存在
			if (friendInfo != null)
			{
				pack.Write(friendInfo.Id); //玩家Id
				pack.Write(friendInfo.Name); //玩家昵称
				pack.Write(friendInfo.HeadImg); //玩家头像
				pack.Write((byte)friendInfo.Sex); //玩家性别

				//pack.Write(friendInfo.Money); //玩家金币

				//pack.Write(friendInfo.EmpiricalValue); //玩家经验值

				pack.Write(friendInfo.Description); //玩家描述

				pack.Write(Math.Min(friendInfo.FansNum, 999999)); //玩家粉丝数量
				pack.Write(Math.Min(friendInfo.AttentionNum, 999)); //关注数量
				pack.Write(friendInfo.MapNum); //发布的地图数量
				pack.Write(friendInfo.DrawSheetNum); //发布的图纸数量

				pack.Write(friendInfo.IsFriendShip ? 1 : 0); //该好友与该玩家是否是好友关系
				pack.Write(friendInfo.IsAttention ? 1 : 0); //该玩家是否已经关注该好友
			}
			player.SendMsg(pack);
		}
	}
}
