using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class ResourceCommentBLL : BaseBLL
	{
		/// <summary>
		/// 异步分页获取资源评论列表
		/// </summary>
		/// <param name="resourceId">资源Id</param>
		/// <param name="pageIndex">页码（从1开始）</param>
		/// <param name="pageSize">每页数量</param>
		/// <returns></returns>
		public static List<Model.ResourceCommentModel> GetConnentListByPage(int resourceId, ref Model.PageInfo pageInfo)
		{
			return DAL.ResourceCommentDAL.GetConnentListByPage(resourceId, ref pageInfo);
		}
	}
}
