using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 好友申请添加接口
	/// </summary>
	class FriendRequestAddPacket : Package
	{
		public FriendRequestAddPacket() { }
		public FriendRequestAddPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendRequestAddPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_requestAdd);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int friendId = ReadInt(); //要申请添加的好友id

			if (friendId <= 0)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			if (player.Id == friendId)
			{
				return;
			}
			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_requestAdd_ret);
			pack.Write(key);
			DateTime timeNow = DateTime.Now;
			var flag = false;
			var friendReqRecord = BLL.FriendRequestBLL.GetFriendReqRecordByLastTime(player.Id, friendId);
			if (friendReqRecord != null)
			{
				if ((timeNow - friendReqRecord.RequestTime).TotalMinutes <= 10)
				{
					pack.Write(2); //十分钟之内不允许重复邀请
					player.SendMsg(pack);
					return;
				}
				else
				{
					if (friendReqRecord.RequestState == 0)
					{
						//更新申请时间
						flag = BLL.FriendRequestBLL.UpdateReqTime(friendReqRecord.Id, timeNow);
						pack.Write(flag ? 1 : 0); //发出申请成功/失败
						player.SendMsg(pack);
						return;
					}
				}
			}
			Model.FriendRequestModel friendReqModel = new Model.FriendRequestModel
			{
				PlayerId = player.Id,
				FriendId = friendId,
				RequestState = 0,
				RequestTime = timeNow
			};
			//添加新记录
			flag = BLL.BaseBLL.Insert(friendReqModel) > 0;
			pack.Write(flag ? 1 : 0); //发出申请成功/失败
			player.SendMsg(pack);
		}
	}
}
