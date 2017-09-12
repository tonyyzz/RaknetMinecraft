using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 房间列表
	/// </summary>
	class HouseListPacket : Package
	{
		public HouseListPacket() { }
		public HouseListPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseListPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_list);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int isWifiInt = ReadInt(); //wifi联机：1， 局域网：2
			int tagId = ReadInt(); //传地图标签Id（0表示全部）
			string keywords = ReadString(); //关键字（传 房主名称 或者 房主Id ）（可以传空字符串，即双引号）
			int pageIndex = ReadInt(); //页码从1开始
			int pageSize = ReadInt(); //每页数量（客户端自己控制）

			if (!new List<int>() { 1, 2 }.Any(m => m == isWifiInt))
			{
				return;
			}
			if (tagId < 0)
			{
				return;
			}

			if (pageIndex < 1 || pageSize < 1)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			keywords = keywords.GetRemoveExcessSpaceStr();
			long houseOwnerId = 0; long.TryParse(keywords, out houseOwnerId);

			//根据多个条件进行筛选
			#region 根据多个条件进行筛选
			Func<Model.PlayerHouseModel, bool> isWifiFunc = null; //局域网筛选条件
			if (isWifiInt == 2)
			{
				isWifiFunc = m =>
				m.HouseIpAddress.Substring(0, m.HouseIpAddress.LastIndexOf('.'))
				== player.IpAddress.Substring(0, player.IpAddress.LastIndexOf('.'));
			}
			Func<Model.PlayerHouseModel, bool> tagFunc = null; //房间标签筛选条件
			if (tagId > 0)
			{
				tagFunc = m => m.TagId == tagId;
			}
			Func<Model.PlayerHouseModel, bool> keywordsFunc = null; //关键字筛选条件
			if (!string.IsNullOrWhiteSpace(keywords))
			{
				keywordsFunc = m => m.HouseOwnerId == houseOwnerId || m.HouseOwnerName == keywords;
			}
			List<Func<Model.PlayerHouseModel, bool>> houseFuncList = new List<Func<Model.PlayerHouseModel, bool>>();
			houseFuncList.Add(isWifiFunc);
			houseFuncList.Add(tagFunc);
			houseFuncList.Add(keywordsFunc);
			var houseLi = PlayerHouseManager.GetPlayerHouseList();
			foreach (var func in houseFuncList)
			{
				houseLi = houseLi.WhereOrOriginalList(func);
			} 
			#endregion

			var pageInfo = new Model.PageInfo(pageIndex, pageSize);
			pageInfo.TotalCount = houseLi.Count();
			var houseMatrix = houseLi.GetMatrix(pageSize).ToList();
			List<Model.PlayerHouseModel> playerHouseList = new List<Model.PlayerHouseModel>();
			if (houseMatrix.Count() >= pageIndex)
			{
				playerHouseList = houseMatrix[pageIndex - 1];
			}
			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_list_ret);
			pack.Write(playerHouseList.Count());
			playerHouseList.ForEach(item =>
			{
				pack.Write(item);
			});
			pack.Write(pageInfo.PageCount); //总页数
			player.SendMsg(pack);
		}
	}
}
