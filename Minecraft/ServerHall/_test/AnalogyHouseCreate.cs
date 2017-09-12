using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall
{
	/// <summary>
	/// 模拟房间创建
	/// </summary>
	class AnalogyHouseCreate
	{
		/// <summary>
		/// 测试代码执行
		/// </summary>
		/// <param name="isTestExecute">测试代码是否执行</param>
		public static void Do(bool isTestExecute)
		{
			if (!isTestExecute)
			{
				return;
			}
			string houeseName = "testHouse1"; //房间名称
			string description = "测试房间描述"; //房间描述
			string housePwd = "123456"; //房间密码
			int tagId = 1; //传地图标签Id
			int houseSize = 6; //房间大小（1-6 的数字）
			int resourceId = 1; //资源Id  【可能是本地资源，暂放】

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

			var player = BLL.PlayerBLL.QuerySingle(100);
			player.IpAddress = "192.168.0.222";
			player.TcpPort = 5555;

			var tagInfo = BLL.ResourceTagBLL.GetResourceTagInfo(tagId, 1); //获取某个地图标签信息
			if (tagInfo == null)
			{
				return;
			}
			var playerHouseModel = player.CreateHouse(houeseName, description, housePwd, tagInfo.Id, tagInfo.Name, houseSize, resourceId);
		}
	}
}
