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
	/// 设置玩家装扮接口
	/// </summary>
	class AvatarDisguiseSetPacket : Package
	{
		public AvatarDisguiseSetPacket() { }
		public AvatarDisguiseSetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new AvatarDisguiseSetPacket(null, 0,
				MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_disguiseSet);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int modelSexInt = ReadInt(); //模型性别  1：男，2：女
			string disguiseName = ReadString(); //某个装扮名称，如果只传名称，后面没有跟序号，则表示脱下装扮的意思

			if (!new List<int> { 1, 2 }.Any(m => m == modelSexInt))
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(disguiseName))
			{
				return;
			}

			Match match = Regex.Match(disguiseName, @"([a-zA-Z]+)(\d*)");
			if (!match.Success)
			{
				return;
			}
			string disguiseTypeStr = match.Groups[1].Value;
			string disguiseVal = match.Groups[2].Value;

			var disguiseTypeList = new Model.PlayerDisguiseModel().GetAllPropKeys().ToList();
			disguiseTypeList.Remove("PlayerId");
			disguiseTypeList.Remove("ModelSex");
			disguiseTypeList.Add("Suit"); //套装穿带
			if (!disguiseTypeList.Any(m => m.ToLower() == disguiseTypeStr.ToLower()))
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			List<Model.DisguiseModel> disguiseList = new List<Model.DisguiseModel>();
			if (disguiseTypeStr.ToLower() == "Suit".ToLower())
			{
				if (string.IsNullOrWhiteSpace(disguiseVal))
				{
					return;
				}
				//找到套装
				var suitConfigModel = CSVFileConfig.suitConfigList.FirstOrDefault(m => m.Name.ToLower() == disguiseName.ToLower());
				if (suitConfigModel == null)
				{
					return;
				}
				disguiseList = suitConfigModel.DisguiseList;
			}
			else
			{
				disguiseList.Add(new Model.DisguiseModel() { Type = disguiseTypeStr, Value = disguiseVal });
			}
			//赋值插入/更新操作
			var flag = false;
			foreach (var disguise in disguiseList)
			{
				var disguiseModel = BLL.PlayerDisguiseBLL.GetPlayerDisguise(player.Id, modelSexInt);
				if (disguiseModel == null)
				{
					disguiseModel = new Model.PlayerDisguiseModel
					{
						PlayerId = player.Id,
						ModelSex = modelSexInt
					};
					var propInfo = disguiseModel.GetType().GetProperties().FirstOrDefault(m => m.Name.ToLower() == disguise.Type.ToLower());
					if (propInfo == null)
					{
						return;
					}
					propInfo.SetValue(disguiseModel, disguise.Value);
					flag = BLL.BaseBLL.InsertSuccess(disguiseModel);
				}
				else
				{
					flag = BLL.PlayerDisguiseBLL.UpdatePlayerDisguise(player.Id, modelSexInt, disguise.Type, disguise.Value);
				}
			}
			Package pack = new Package(MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_disguiseSet_ret);
			pack.Write(key);
			pack.Write(flag ? 1 : 0); //设置成功或者失败
			player.SendMsg(pack);
		}
	}
}
