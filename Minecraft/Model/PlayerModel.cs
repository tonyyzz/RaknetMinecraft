using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 玩家model
	/// </summary>
	//代码生成器生成
	public partial class PlayerModel
	{
		/// <summary>
		/// 
		/// </summary>
		public PlayerModel()
		{
			Id = 0;
			Name = "";
			HeadImg = "";
			Level = 0;
			Money = 0;
			RegistTime = new DateTime(1900, 1, 1);
			LastLoginTime = new DateTime(1900, 1, 1);
			FriendIdStr = "";
			Sex = 0;
			PlayerPoints = 0;
			EmpiricalValue = 0;
			Description = "";
			ModelSex = 0;
		}
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 玩家昵称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 玩家头像
		/// </summary>
		public string HeadImg { get; set; }
		/// <summary>
		/// 玩家等级
		/// </summary>
		public int Level { get; set; }
		/// <summary>
		/// 玩家账户金币
		/// </summary>
		public int Money { get; set; }
		/// <summary>
		/// 玩家注册时间
		/// </summary>
		public DateTime RegistTime { get; set; }
		/// <summary>
		/// 玩家最近登陆时间
		/// </summary>
		public DateTime LastLoginTime { get; set; }
		/// <summary>
		/// 玩家好友Id列表
		/// </summary>
		public string FriendIdStr { get; set; }
		/// <summary>
		/// 性别（0：保密，1：男，2：女）
		/// </summary>
		public int Sex { get; set; }
		/// <summary>
		/// 玩家积分
		/// </summary>
		public long PlayerPoints { get; set; }
		/// <summary>
		/// 经验值
		/// </summary>
		public long EmpiricalValue { get; set; }
		/// <summary>
		/// 玩家介绍
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 模型性别（1：男，2：女）默认为1
		/// </summary>
		public int ModelSex { get; set; }
	}
	/// <summary>
	/// 玩家model
	/// </summary>
	public partial class PlayerModel : BaseCommon.BasePlayer
	{
		/// <summary>
		/// 玩家的ip地址
		/// </summary>
		public string IpAddress = "";
		/// <summary>
		/// 玩家的tcp端口
		/// </summary>
		public ushort TcpPort = 0;
		/// <summary>
		/// 玩家的udp端口
		/// </summary>
		public ushort UdpPort = 0;
		/// <summary>
		/// 连接ID
		/// </summary>
		public IntPtr conID = IntPtr.Zero;
		/// <summary>
		/// 玩家是否在线
		/// </summary>
		public bool online = false;



		/// <summary>
		/// 玩家所在房间号
		/// </summary>
		public long HouseId = 0;



		/// <summary>
		/// 粉丝数量
		/// </summary>
		public int FansNum = 0;
		/// <summary>
		/// 关注数量
		/// </summary>
		public int AttentionNum = 0;
		/// <summary>
		/// 制作的地图数量
		/// </summary>
		public int MapNum = 0;
		/// <summary>
		/// 制作的图纸数量
		/// </summary>
		public int DrawSheetNum = 0;
		/// <summary>
		/// 好友数量
		/// </summary>
		public int FriendNum = 0;
		/// <summary>
		/// 与某个玩家是否是好友关系（只在好友关系中使用）
		/// </summary>
		public bool IsFriendShip = false;
		/// <summary>
		/// 该玩家是否已经关注该好友
		/// </summary>
		public bool IsAttention = false;
	}
}
