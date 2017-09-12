using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class ResourceBLL : BaseBLL
	{
		/// <summary>
		/// 根据标签类型获取资源信息
		/// </summary>
		/// <param name="tagId">标签类型Id</param>
		/// <param name="pageIndex">页码（从1开始）</param>
		/// <param name="pageSize">每页数量</param>
		/// <returns></returns>
		public static List<Model.ResourceModel> GetResourcesByTag(int resourceTypeId, int tagId, int pageIndex, int pageSize)
		{
			return DAL.ResourceDAL.GetResourcesByTag(resourceTypeId, tagId, pageIndex, pageSize);
		}
		/// <summary>
		/// 获取资源详情
		/// </summary>
		/// <param name="resourceId">资源Id</param>
		public static Model.ResourceModel GetResourceDetail(int resourceId)
		{
			return DAL.ResourceDAL.GetResourceDetail(resourceId);
		}
		/// <summary>
		/// 更新资源标签
		/// </summary>
		/// <param name="resourceId">资源Id</param>
		/// <param name="resourceTagList">资源标签Id列表</param>
		/// <returns></returns>
		public static bool UpdateResourceTags(int resourceId, List<int> resourceTagList)
		{
			return DAL.ResourceDAL.UpdateResourceTags(resourceId, resourceTagList);
		}
		/// <summary>
		/// 更新某个资源的点赞数量+1
		/// </summary>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		public static bool UpdatePointLineNumPlusOne(int resourceId)
		{
			return DAL.ResourceDAL.UpdatePointLineNumPlusOne(resourceId);
		}
		/// <summary>
		/// 更新某个资源的下载数量+1
		/// </summary>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		public static bool UpdateDownloadNumPlusOne(int resourceId)
		{
			return DAL.ResourceDAL.UpdateDownloadNumPlusOne(resourceId);
		}
		/// <summary>
		/// 对某个资源评分
		/// </summary>
		/// <param name="resourceId"></param>
		/// <param name="score"></param>
		/// <returns></returns>
		public static bool UpdateScore(int resourceId, int score)
		{
			return DAL.ResourceDAL.UpdateScore(resourceId, score);
		}
		/// <summary>
		/// 获取推荐资源数据
		/// </summary>
		public static List<Model.ResourceModel> GetRecommendList(int resourceTypeId, int resourceTagId, ref Model.PageInfo pageInfo)
		{
			return DAL.ResourceDAL.GetRecommendList(resourceTypeId, resourceTagId, ref pageInfo);
		}
		/// <summary>
		/// 获取流行资源数据
		/// </summary>
		public static List<Model.ResourceModel> GetPopularList(int resourceTypeId, int resourceTagId, ref Model.PageInfo pageInfo)
		{
			return DAL.ResourceDAL.GetPopularList(resourceTypeId, resourceTagId, ref pageInfo);
		}
		/// <summary>
		/// 玩家关注的玩家资源列表
		/// </summary>
		/// <param name="playerId">玩家Id</param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static List<Model.ResourceModel> GetAttentionList(int playerId, int resourceTypeId, int resourceTagId, ref Model.PageInfo pageInfo)
		{
			return DAL.ResourceDAL.GetAttentionList(playerId, resourceTypeId, resourceTagId, ref pageInfo);
		}
	}
}
