using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 玩家房间model
	/// </summary>
	public partial class PlayerHouseModel
	{
		/// <summary>
		/// 构造函数初始化
		/// </summary>
		public PlayerHouseModel()
		{
			Id = 0;
			Name = "";
			Description = "";
			HousePwd = "";
			TagName = "";
			ThumbnailImg = "";
			HouseOwnerId = 0;
			HouseOwnerName = "";
			HouseSize = 0;
			ResourceId = 0;
			CreateTime = new DateTime(1900, 1, 1);
		}
		/// <summary>
		/// 房间Id
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 房间名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 房间描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 房间密码
		/// </summary>
		public string HousePwd { get; set; }
		/// <summary>
		/// 标签Id
		/// </summary>
		public int TagId { get; set; }
		/// <summary>
		/// 标签名称
		/// </summary>
		public string TagName { get; set; }
		/// <summary>
		/// 缩略图
		/// </summary>
		public string ThumbnailImg { get; set; }
		/// <summary>
		/// 房主Id
		/// </summary>
		public int HouseOwnerId { get; set; }
		/// <summary>
		/// 房主名称
		/// </summary>
		public string HouseOwnerName { get; set; }
		/// <summary>
		/// 房主IP地址
		/// </summary>
		public string HouseIpAddress { get; set; }
		/// <summary>
		/// 房主tcp端口
		/// </summary>
		public ushort HouseTcpPort { get; set; }
		/// <summary>
		/// 该房间的udp端口
		/// </summary>
		public ushort HouseUdpPort { get; set; }
		/// <summary>
		/// 房间大小
		/// </summary>
		public int HouseSize { get; set; }
		/// <summary>
		/// 资源Id
		/// </summary>
		public int ResourceId { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }
	}

	public partial class PlayerHouseModel
	{
		/// <summary>
		/// 某个房间的所有玩家列表
		/// </summary>
		public List<PlayerModel> playerList = new List<PlayerModel>();

		/// <summary>
		/// 房间人数（客户端使用）
		/// </summary>
		public int playerCount = 0;
	}
}
