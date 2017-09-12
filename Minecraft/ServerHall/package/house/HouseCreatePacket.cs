using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 玩家创建房间
	/// </summary>
	class HouseCreatePacket : Package
	{
		public HouseCreatePacket() { }
		public HouseCreatePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseCreatePacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_create);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			string houeseName = ReadString(); //房间名称
			string description = ReadString(); //房间描述
			string housePwd = ReadString(); //房间密码
			int tagId = ReadInt(); //传地图标签Id
			int houseSize = ReadInt(); //房间大小（1-6 的数字）
			int resourceId = ReadInt(); //资源Id  【可能是本地资源，暂放】

			if (string.IsNullOrWhiteSpace(houeseName))
			{
				return;
			}
			if (tagId <= 0)
			{
				return;
			}
			if (houseSize < 1 || houseSize > 6)
			{
				return;
			}
			if (resourceId <= 0)
			{
				return;
			}

			houeseName = houeseName.GetRemoveExcessSpaceStr();
			description = description.GetRemoveExcessSpaceStr();

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			var tagInfo = BLL.ResourceTagBLL.GetResourceTagInfo(tagId, 1); //获取某个地图标签信息
			if (tagInfo == null)
			{
				return;
			}
			var playerHouseModel = player.CreateHouse(houeseName, description, housePwd, tagInfo.Id, tagInfo.Name, houseSize, resourceId);
			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_create_ret);
			pack.Write((byte)(playerHouseModel != null ? 1 : 0)); //房间创建成功或者失败
			if (playerHouseModel != null)
			{
				pack.Write(playerHouseModel.Id); //房间Id
			}
			player.SendMsg(pack);
		}
	}
}
