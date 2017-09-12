using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 玩家信息接口（用于首页粗略信息展示）
	/// </summary>
	class PlayerInfoPacket : Package
	{
		public PlayerInfoPacket() { }
		public PlayerInfoPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerInfoPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerInfo);
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
			Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerInfo_ret);
			pack.Write(key);
			pack.Write(player.Name);
			pack.Write(player.HeadImg);
			pack.Write(player.Level);
			pack.Write(player.Money);
			player.SendMsg(pack);
		}
	}
}
