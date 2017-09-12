using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 好友推荐列表接口
	/// </summary>
	class FriendRecommendPacket : Package
	{
		public FriendRecommendPacket() { }
		public FriendRecommendPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendRecommendPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_recommend);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int pageIndex = ReadInt(); //页码从1开始
			int pageSize = ReadInt(); //每页数量（客户端自己控制）

			if (pageIndex < 1 || pageSize < 1)
			{
				return;
			}
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			var pageInfo = new Model.PageInfo(pageIndex, pageSize);
			var friendRecommendList = BLL.FriendBLL.GetRecommendList(player.Id, ref pageInfo);
			Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_recommend_ret);
			pack.Write(key);
			pack.Write(friendRecommendList.Count());
			friendRecommendList.ForEach(item =>
			{
				pack.Write(item);
			});
			pack.Write(pageInfo.PageCount); //总页数
			player.SendMsg(pack);
		}
	}
}
