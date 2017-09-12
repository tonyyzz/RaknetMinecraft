using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 删除好友
	/// </summary>
	class FriendDeletePacket : Package
	{
		public FriendDeletePacket() { }
		public FriendDeletePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendDeletePacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_delete);
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
			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_delete_ret);
			pack.Write(key);
			var isFriendShip = BLL.FriendBLL.IsFriendShip(player.Id, friendId);
			if (!isFriendShip)
			{
				pack.Write(2); //不是好友关系
			}
			else
			{
				var flag = BLL.FriendBLL.Delete(player.Id, friendId);
				pack.Write(flag ? 1 : 0); //删除成功/失败
			}
			player.SendMsg(pack);
		}
	}
}
