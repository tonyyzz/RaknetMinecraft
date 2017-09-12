using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 编辑性别
	/// </summary>
	class PlayerEditSexPacket : Package
	{
		public PlayerEditSexPacket() { }
		public PlayerEditSexPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerEditSexPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerEditSex);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			byte newSex = ReadByte(); //新性别（0：保密，1：男，2：女）

			if (!new List<byte> { 0, 1, 2 }.Any(m => m == newSex))
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			var flag = BLL.PlayerBLL.UpdateSex(player.Id, newSex);
			Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerEditSex_ret);
			pack.Write(key);
			pack.Write((byte)(flag ? 1 : 0)); //是否更新成功
			player.SendMsg(pack);
		}
	}
}
