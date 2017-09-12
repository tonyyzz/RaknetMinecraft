using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 房间信息接口
	/// </summary>
	class HouseInfoPacket : Package
	{
		public HouseInfoPacket() { }
		public HouseInfoPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseInfoPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_info);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			long houseId = ReadLong(); //房间Id
			if (houseId <= 0)
			{
				return;
			}
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			var houseInfo = PlayerHouseManager.GetPlayerHouseInfo(houseId);

			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_info_ret);
			pack.Write((byte)(houseInfo != null ? 1 : 0)); //房间是否存在
			if (houseInfo != null)
			{
				pack.Write(houseInfo.Id); //房间Id
				pack.Write(houseInfo.Name); //房间名称
				pack.Write(houseInfo.TagName); //标签名称
				pack.Write(houseInfo.Description); //房间描述
				pack.Write(houseInfo.ThumbnailImg); //缩略图
				pack.Write(houseInfo.HousePwd); //房间密码
				pack.Write(houseInfo.HouseSize); //房间大小
				pack.Write(houseInfo.playerList.Count()); //房间人数
			}
			player.SendMsg(pack);
		}
	}
}
