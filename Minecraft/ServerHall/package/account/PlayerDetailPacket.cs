using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 玩家详情接口（用于点击头像后获取玩家详细信息，只能获取自己的）
	/// </summary>
	class PlayerDetailPacket : Package
	{
		public PlayerDetailPacket() { }
		public PlayerDetailPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerDetailPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerDetail);
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
			var playerDetail = BLL.PlayerBLL.GetPlayerDetail(player.Id);
			Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerDetail_ret);
			pack.Write(key);
			pack.Write(playerDetail.Id); //玩家Id
			pack.Write(playerDetail.Name); //玩家昵称
			pack.Write(playerDetail.HeadImg); //玩家头像
			pack.Write((byte)playerDetail.Sex); //玩家性别

			pack.Write(playerDetail.Money); //玩家金币

			pack.Write(playerDetail.EmpiricalValue); //玩家经验值
			pack.Write(playerDetail.Description); //玩家描述

			pack.Write(Math.Min(playerDetail.FriendNum, 50)); //好友数量
			pack.Write(Math.Min(playerDetail.FansNum, 999999)); //玩家粉丝数量
			pack.Write(Math.Min(playerDetail.AttentionNum, 999)); //关注数量
			pack.Write(playerDetail.MapNum); //发布的地图数量
			pack.Write(playerDetail.DrawSheetNum); //发布的图纸数量

			player.SendMsg(pack);
		}
	}
}
