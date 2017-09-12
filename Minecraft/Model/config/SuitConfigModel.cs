using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 套装配置model
	/// </summary>
	public partial class SuitConfigModel
	{
		/// <summary>
		/// 套装中文名称
		/// </summary>
		public string CN_Name { get; set; }
		/// <summary>
		/// 套装名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 套装组成字符串
		/// </summary>
		public string Parts { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public string Sexy { get; set; }
		/// <summary>
		/// 图标
		/// </summary>
		public string Icon { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		public string Explain { get; set; }
	}
	public partial class SuitConfigModel
	{
		/// <summary>
		/// 套装组成列表
		/// </summary>
		public List<string> PartsList
		{
			get { return Parts.Split('|').ToList(); }
		}
		/// <summary>
		/// 套装名称序号
		/// </summary>
		public string NameNumber
		{
			get { return Regex.Match(Name, @"[S|s]uit(\d+)").Groups[1].Value; }
		}
		/// <summary>
		/// 套装组成部分
		/// </summary>
		public List<DisguiseModel> DisguiseList
		{
			get
			{
				List<DisguiseModel> list = new List<DisguiseModel>();
				foreach (var item in PartsList)
				{
					Match match = Regex.Match(item, @"([a-zA-Z]+)(\d*)");
					if (!match.Success)
					{
						continue;
					}
					string disguiseTypeStr = match.Groups[1].Value;
					string disguiseVal = match.Groups[2].Value;
					list.Add(new DisguiseModel() { Type = disguiseTypeStr, Value = disguiseVal });
				}
				return list;
			}
		}
	}
}
