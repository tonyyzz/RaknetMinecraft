using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall
{
	public static class FilePathConfig
	{
		#region 根文件路径
		/// <summary>
		/// 根文件路径
		/// </summary>
		private static readonly string _rootPath = @"E:\MinecraftResource";
		#endregion

		#region 文件分类路径文件夹名称
		/// <summary>
		/// 玩家上传
		/// </summary>
		private static readonly string _playerResource = Path.Combine(_rootPath, "PlayerResource");
		#endregion

		#region 资源类型
		private static readonly string Map = "Map"; //地图
		private static readonly string DrawSheet = "DrawSheet"; //图纸
		#endregion




		#region ------------上传文件路径配置------------
		/// <summary>
		/// 玩家地图上传路径
		/// </summary>
		public static string PlayerMapPath { get { return Path.Combine(_playerResource, Map); } }
		/// <summary>
		/// 玩家图纸上传路径
		/// </summary>
		public static string PlayerDrawSheetPath { get { return Path.Combine(_playerResource, DrawSheet); } }
		#endregion
	}
}
