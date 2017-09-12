using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 房主关闭房间
	/// </summary>
	class HouseClosePacket : Package
	{
		public HouseClosePacket() { }
		public HouseClosePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseClosePacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_close);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			byte isServer = ReadByte(); //1：房主关闭房间，2：玩家退出房间

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			player.WithdrawFromHouse();
			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_close_ret);
			pack.Write(isServer);
			player.SendMsg(pack);
		}
	}
}
