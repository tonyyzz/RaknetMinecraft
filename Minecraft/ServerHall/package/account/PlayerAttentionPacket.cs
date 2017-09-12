using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 玩家关注
	/// </summary>
	class PlayerAttentionPacket : Package
	{
		public PlayerAttentionPacket() { }
		public PlayerAttentionPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerAttentionPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerAttention);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int playerAttentionId = ReadInt(); //玩家关注的朋友Id

			if (playerAttentionId <= 0)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			if (playerAttentionId == player.Id)
			{
				return;
			}

			Model.PlayerAttentionModel playerAttentionModel = new Model.PlayerAttentionModel
			{
				PlayerId = player.Id,
				PlayerAttentionId = playerAttentionId,
				AttentionTime = DateTime.Now
			};
			var flag = BLL.PlayerBLL.AttentionPlayer(playerAttentionModel);
			Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerAttention_ret);
			pack.Write(key);
			pack.Write((byte)(flag ? 1 : 0));
			player.SendMsg(pack);
		}
	}
}
