using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall
{
	/// <summary>
	/// 锁配置
	/// </summary>
	public static class LockConfig
	{
		#region ------------资源操作锁------------
		/// <summary>
		/// 资源点赞操作锁
		/// </summary>
		public static readonly object lock_resourcePointLike = new object();
		/// <summary>
		/// 资源评分锁
		/// </summary>
		public static readonly object lock_resourceScore = new object();
		/// <summary>
		/// 资源上传锁
		/// </summary>
		public static readonly object lock_resourceUpload = new object();
		/// <summary>
		/// 资源下载锁
		/// </summary>
		public static readonly object lock_resourceDownload = new object();
		#endregion
	}
}
