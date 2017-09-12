using BaseCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall
{
	/// <summary>
	/// csv文件配置
	/// </summary>
	public class CSVFileConfig
	{
		/// <summary>
		/// 套装配置列表
		/// </summary>
		public static List<Model.SuitConfigModel> suitConfigList;

		/// <summary>
		/// 安装csv配置
		/// </summary>
		public static void InstallConfig()
		{
			csvConfig.Install("config/Mod_Player_Suit.csv", out suitConfigList);
		}
	}
}
