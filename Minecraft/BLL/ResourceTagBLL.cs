using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class ResourceTagBLL
	{
		/// <summary>
		/// 根据资源类型获取标签
		/// </summary>
		/// <param name="resourceTypeId">资源类型Id</param>
		public static List<Model.ResourceTagModel> GetResourceTagsByType(int resourceTypeId)
		{
			return DAL.ResourceTagDAL.GetResourceTagsByType(resourceTypeId);
		}
		/// <summary>
		/// 获取某个标签信息
		/// </summary>
		/// <param name="tagId">标签Id</param>
		/// <param name="resourceTypeId">标签类型</param>
		/// <returns></returns>
		public static Model.ResourceTagModel GetResourceTagInfo(int tagId, int resourceTypeId)
		{
			return DAL.ResourceTagDAL.GetResourceTagInfo(tagId, resourceTypeId);
		}
	}
}
