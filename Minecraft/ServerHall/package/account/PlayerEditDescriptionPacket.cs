using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 更新介绍
	/// </summary>
	class PlayerEditDescriptionPacket : Package
	{
		public PlayerEditDescriptionPacket() { }
		public PlayerEditDescriptionPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerEditDescriptionPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerEditDescription);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			string newDescription = ReadString(); //新介绍

			if (string.IsNullOrWhiteSpace(newDescription))
			{
				return;
			}

			newDescription = newDescription.GetRemoveExcessSpaceStr();

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerEditDescription_ret);

			if (newDescription.Length > 140)
			{
				pack.Write(2); //描述字数超出范围
				player.SendMsg(pack);
				return;
			}

			var flag = BLL.PlayerBLL.UpdateDescription(player.Id, newDescription);
			pack.Write(key);
			pack.Write(flag ? 1 : 0); //是否更新成功
			player.SendMsg(pack);
		}
	}
}
