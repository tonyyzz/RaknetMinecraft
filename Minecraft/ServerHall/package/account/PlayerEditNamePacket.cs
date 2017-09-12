using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 修改名称
	/// </summary>
	class PlayerEditNamePacket : Package
	{
		public PlayerEditNamePacket() { }
		public PlayerEditNamePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerEditNamePacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerEditName);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			string newName = ReadString(); //新名称

			if (string.IsNullOrWhiteSpace(newName))
			{
				return;
			}

			newName = newName.GetRemoveExcessSpaceStr();

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerEditName_ret);
			pack.Write(key);
			//修改名称需要金币
			int needMoney = 1000;
			if (player.Money < needMoney)
			{
				pack.Write(2); //钱不够
				player.SendMsg(pack);
				return;
			}
			player.Money -= needMoney;
			var flag = BLL.PlayerBLL.UpdatePlayerName(player.Id, newName, player.Money);
			pack.Write(flag ? 1 : 0); //是否更新成功
			if (flag)
			{
				pack.Write(player.Money); //更新后的金币
			}
			player.SendMsg(pack);
		}
	}
}
