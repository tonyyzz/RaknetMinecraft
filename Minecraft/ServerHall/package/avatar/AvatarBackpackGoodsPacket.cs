using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ServerHall.package
{
	/// <summary>
	/// 玩家背包物品信息
	/// </summary>
	class AvatarBackpackGoodsPacket : Package
	{
		public AvatarBackpackGoodsPacket() { }
		public AvatarBackpackGoodsPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new AvatarBackpackGoodsPacket(null, 0,
				MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_backpackGoods);
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
			Package pack = new Package(MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_backpackGoods_ret);

			var model = BLL.PlayerBackpackGoodsBLL.GetPlayerBackpackGoodsInfo(player.Id);
			pack.Write(key);
			pack.Write(model != null ? 1 : 0); //背包是否存在，即背包是否为空
			if (model != null)
			{
				pack.Write(model.HeadStr); //根据‘|’分割
				pack.Write(model.CoatStr);
				pack.Write(model.PantStr);

				//获取玩家套装信息
				bool isNowExecute = true; //是否立即执行，如果为true，则不采用线程池，直接单线程执行
				if (isNowExecute)
				{
					ThreadPoolUpdatePlayerSuit.Do(player.Id, isNowExecute);
					var suitModel = BLL.PlayerSuitBLL.GetPlayerSuit(player.Id);
					pack.Write(suitModel != null ? 1 : 0); //是否拥有套装
					if (suitModel != null)
					{
						pack.Write(suitModel.SuitStr);
					}
				}
				else
				{
					var suitModel = BLL.PlayerSuitBLL.GetPlayerSuit(player.Id);
					pack.Write(suitModel != null ? 1 : 0); //是否拥有套装
					if (suitModel != null)
					{
						pack.Write(suitModel.SuitStr);
					}
					else
					{
						ThreadPoolUpdatePlayerSuit.Do(player.Id, isNowExecute);
					}
				}
			}
			player.SendMsg(pack);
		}
	}
}
