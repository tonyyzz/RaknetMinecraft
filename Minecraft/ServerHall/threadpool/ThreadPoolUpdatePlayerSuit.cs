using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerHall
{
	/// <summary>
	/// 线程池 - 更新玩家的套装
	/// </summary>
	public class ThreadPoolUpdatePlayerSuit
	{
		/// <summary>
		/// 执行操作
		/// </summary>
		/// <param name="playerId">玩家Id</param>
		/// <param name="isNowExecute">是否立即执行</param>
		public static void Do(int playerId, bool isNowExecute)
		{
			if (playerId <= 0)
			{
				return;
			}
			if (isNowExecute)
			{
				Execute(playerId);
			}
			else
			{
				ThreadPool.QueueUserWorkItem(obj =>
				{
					int pid = (int)obj;
					Execute(pid);
				}, playerId);
			}
		}
		/// <summary>
		/// 执行代码
		/// </summary>
		/// <param name="pid"></param>
		private static void Execute(int pid)
		{
			//找到合适时机触发该段代码
			//进行套装检索，并设置
			var model = BLL.PlayerBackpackGoodsBLL.GetPlayerBackpackGoodsInfo(pid);
			var disguiseTypeList = new Model.PlayerDisguiseModel().GetAllPropKeys().ToList();
			disguiseTypeList.Remove("PlayerId");
			disguiseTypeList.Remove("ModelSex");
			var backpackGoodsList = new List<string>();
			foreach (var item in disguiseTypeList)
			{
				var propInfo = model.GetType().GetProperties().FirstOrDefault(m => m.Name == item + "Str");
				if (propInfo == null)
				{
					continue;
				}
				var valueStr = propInfo.GetValue(model).ToString();
				if (string.IsNullOrWhiteSpace(valueStr))
				{
					continue;
				}
				var valueList = valueStr.Split('|').ToList().ConvertAll(m =>
				{
					return (item + m).ToLower();
				});
				//将所有的背包物品装入集合
				backpackGoodsList.AddRange(valueList);
			}
			if (backpackGoodsList.Any())
			{
				//存储套装名称序号
				List<string> suitNameNumberStrList = new List<string>();
				//读取套装配置表
				foreach (var suitConfig in CSVFileConfig.suitConfigList)
				{
					var exceptList = suitConfig.PartsList.Except(backpackGoodsList).ToList();
					if (!exceptList.Any())
					{
						//如果差集列表个数为空，则表示拥有该套装
						suitNameNumberStrList.Add(suitConfig.NameNumber);
					}
				}
				if (suitNameNumberStrList.Any())
				{
					//存到数据库
					var suitModel = BLL.PlayerSuitBLL.GetPlayerSuit(pid);
					if (suitModel == null)
					{
						suitModel = new Model.PlayerSuitModel
						{
							PlayerId = pid,
							SuitStr = string.Join("|", suitNameNumberStrList.ToArray())
						};
						BLL.BaseBLL.InsertSuccess(suitModel);
					}
					else
					{
						suitModel.SuitStr = string.Join("|", suitNameNumberStrList.ToArray());
						BLL.PlayerSuitBLL.UpdatePlayerSuit(pid, suitModel.SuitStr);
					}
				}
			}
		}
	}
}
