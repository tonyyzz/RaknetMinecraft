using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
	public class ResourceTagDAL : BaseDAL
	{
		/// <summary>
		/// 根据资源类型获取标签
		/// </summary>
		/// <param name="resourceTypeId">资源类型Id</param>
		public static List<Model.ResourceTagModel> GetResourceTagsByType(int resourceTypeId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();

				string sql = "select * from resourcetag where ResourceTypeId=@ResourceTypeId";
				var task = Conn.QueryAsync<Model.ResourceTagModel>(sql, new { ResourceTypeId = resourceTypeId });
				return task.Result == null ? new List<Model.ResourceTagModel>() : task.Result.ToList();
			}
		}

		/// <summary>
		/// 获取某个标签信息
		/// </summary>
		/// <param name="tagId">标签Id</param>
		/// <param name="resourceTypeId">标签类型</param>
		/// <returns></returns>
		public static Model.ResourceTagModel GetResourceTagInfo(int tagId, int resourceTypeId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();

				string sql = "select * from resourcetag where Id=@Id and ResourceTypeId=@ResourceTypeId";
				return Conn.QueryFirstOrDefault<Model.ResourceTagModel>(sql, new { Id = tagId, ResourceTypeId = resourceTypeId });
			}
		}
	}
}
