using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 资源model
	/// </summary>
	//代码生成器生成
	public partial class ResourceModel
	{
		public ResourceModel()
		{
			Id = 0;
			ResourceTypeId = 0;
			Title = "";
			Description = "";
			ThumbnailImg = "";
			FileUrl = "";
			FileSizeKb = 0;
			AddTime = new DateTime(1900, 1, 1);
			LastUpdateTime = new DateTime(1900, 1, 1);
			PublishTime = new DateTime(1900, 1, 1);
			CompositorId = 0;
			DownloadNum = 0;
			IsOfficial = false;
			PlayerId = 0;
			OperatorId = 0;
			IsOfficialRecommended = false;
			OfficialRecommendation = "";
			ScoreTotal = 0;
			ScoreNum = 0;
			PointsLikeNum = 0;
		}
		/// <summary>
		/// 资源Id
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 资源类型Id
		/// </summary>
		public int ResourceTypeId { get; set; }
		/// <summary>
		/// 资源标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 资源描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 资源缩略图
		/// </summary>
		public string ThumbnailImg { get; set; }
		/// <summary>
		/// 资源文件url
		/// </summary>
		public string FileUrl { get; set; }
		/// <summary>
		/// 资源大小（KB）
		/// </summary>
		public int FileSizeKb { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime AddTime { get; set; }
		/// <summary>
		/// 最近一次修改时间
		/// </summary>
		public DateTime LastUpdateTime { get; set; }
		/// <summary>
		/// 资源发布时间
		/// </summary>
		public DateTime PublishTime { get; set; }
		/// <summary>
		/// 排序Id
		/// </summary>
		public int CompositorId { get; set; }
		/// <summary>
		/// 下载量
		/// </summary>
		public int DownloadNum { get; set; }
		/// <summary>
		/// 是否是官方制作
		/// </summary>
		public bool IsOfficial { get; set; }
		/// <summary>
		/// 制作的玩家Id（如果是官方制作，则忽略）
		/// </summary>
		public int PlayerId { get; set; }
		/// <summary>
		/// 管理员操作Id
		/// </summary>
		public int OperatorId { get; set; }
		/// <summary>
		/// 是否是官方推荐
		/// </summary>
		public bool IsOfficialRecommended { get; set; }
		/// <summary>
		/// 官方推荐描述
		/// </summary>
		public string OfficialRecommendation { get; set; }
		/// <summary>
		/// 评分总和
		/// </summary>
		public int ScoreTotal { get; set; }
		/// <summary>
		/// 评分次数
		/// </summary>
		public int ScoreNum { get; set; }
		/// <summary>
		/// 点赞数量
		/// </summary>
		public int PointsLikeNum { get; set; }
	}
	public partial class ResourceModel
	{
		/// <summary>
		/// 资源标签列表
		/// </summary>
		public List<ResourceTagModel> resourceTagList = new List<ResourceTagModel>();
	}
}
