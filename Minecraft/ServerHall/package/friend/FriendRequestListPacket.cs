using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 好友申请列表信息
	/// </summary>
	class FriendRequestListPacket : Package
	{
		public FriendRequestListPacket() { }
		public FriendRequestListPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendRequestListPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_requestList);
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
			var friendReqList = BLL.FriendRequestBLL.GetFriendReqList(player.Id);
			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_requestList_ret);
			pack.Write(key);
			pack.Write(friendReqList.Count());
			friendReqList.ForEach(item =>
			{
				pack.Write(item, isExtension: true);
			});
			player.SendMsg(pack);
		}
	}
}
