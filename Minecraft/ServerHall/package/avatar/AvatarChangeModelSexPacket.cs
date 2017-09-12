using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 切换model性别
	/// </summary>
	class AvatarChangeModelSexPacket : Package
	{
		public AvatarChangeModelSexPacket() { }
		public AvatarChangeModelSexPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new AvatarChangeModelSexPacket(null, 0,
				MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_changeModelSex);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int modelSexInt = ReadInt(); //1：男，2：女

			if (!new List<int> { 1, 2 }.Any(m => m == modelSexInt))
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			Package pack = new Package(MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_changeModelSex_ret);
			pack.Write(key);
			//存储model显示的性别

			var flag = BLL.PlayerBLL.UpdateModelSex(player.Id, modelSexInt);

			var disguiseModel = BLL.PlayerDisguiseBLL.GetPlayerDisguise(player.Id, modelSexInt);
			pack.Write(disguiseModel != null ? 1 : 0); //disguiseModel是否存在
			if (disguiseModel != null)
			{
				pack.Write(disguiseModel);



				var disguiseTypeList = new Model.PlayerDisguiseModel().GetAllPropKeys().ToList();
				disguiseTypeList.Remove("PlayerId");
				disguiseTypeList.Remove("ModelSex");
				var disguiseList = new List<string>();

				foreach (var item in disguiseTypeList)
				{
					var propInfo = disguiseModel.GetType().GetProperties().FirstOrDefault(m => m.Name == item);
					if (propInfo == null)
					{
						continue;
					}
					var valueStr = propInfo.GetValue(disguiseModel).ToString();
					if (string.IsNullOrWhiteSpace(valueStr))
					{
						continue;
					}
					var valueList = valueStr.Split('|').ToList().ConvertAll(m =>
					{
						return (item + m).ToLower();
					});
					//将所有的背包物品装入集合
					disguiseList.AddRange(valueList);
				}

				//获取穿带时套装的信息
				var suitConfigItem = CSVFileConfig.suitConfigList.FirstOrDefault(
					m => m.Sexy == modelSexInt.ToString()
					&& !m.PartsList.Except(disguiseList).ToList().Any());

				string suitStr = "";
				if (suitConfigItem != null)
				{
					Match match = Regex.Match(suitConfigItem.Name, @"(\d+)");
					if (match.Success)
					{
						suitStr = match.Groups[1].Value;
					}
				}
				pack.Write(suitStr);
			}

			player.SendMsg(pack);
		}
	}
}
