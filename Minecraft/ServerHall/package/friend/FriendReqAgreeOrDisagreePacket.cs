using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 接受/拒绝好友申请
	/// </summary>
	class FriendReqAgreeOrDisagreePacket : Package
	{
		public FriendReqAgreeOrDisagreePacket() { }
		public FriendReqAgreeOrDisagreePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendReqAgreeOrDisagreePacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_requestAgreeOrDisagree);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int friendId = ReadInt(); //申请者Id
			int operateInt = ReadInt(); //1：接受，2：拒绝

			if (!new List<int>() { 1, 2 }.Any(m => m == operateInt))
			{
				return;
			}

			if (friendId <= 0)
			{
				return;
			}
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_requestAgreeOrDisagree_ret);
			pack.Write(key);
			var friendreqRecord = BLL.FriendRequestBLL.GetFriendRequestingRcdByLastTime(friendId, player.Id);
			if (friendreqRecord == null)
			{
				//不存在的申请记录，非法操作
				pack.Write(3);
				player.SendMsg(pack);
				return;
			}
			friendreqRecord.RequestState = operateInt;
			var operateFalg = false;
			if (friendreqRecord.RequestState == 1)
			{
				//更新状态并添加好友数据
				operateFalg = BLL.FriendRequestBLL.UpdateReqStateAndAddFriend(friendreqRecord);
			}
			else
			{
				//只更新状态
				operateFalg = BLL.FriendRequestBLL.UpdateReqState(friendreqRecord.Id, friendreqRecord.RequestState);
			}
			pack.Write(friendreqRecord.RequestState); //接受或者拒绝的操作（返回1或者2）
			pack.Write(operateFalg ? 1 : 0); //接受或者拒绝  的状态修改成功/失败
			player.SendMsg(pack);
		}
	}
}
