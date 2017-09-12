using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
	public class ResourceDAL : BaseDAL
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
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "";
				if (tagId > 0)
				{
					sql = string.Format(@"select * from(select b.* from( select * from resourcetagrelation where ResourceTagId={0})T1 
					left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={1}
					limit {2},{3};",
					   tagId, resourceTypeId, pageSize * (pageIndex - 1), pageSize);
				}
				else
				{
					sql = string.Format(@"select DISTINCT * from(select b.* from resourcetagrelation T1 
					left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={0}
					limit {1},{2};",
					   resourceTypeId, pageSize * (pageIndex - 1), pageSize);
				}
				var resourceModelList = Conn.Query<Model.ResourceModel>(sql).ToList();
				foreach (var item in resourceModelList)
				{
					string sql2 = string.Format(@"select b.* from( select * from resourcetagrelation where ResourceId={0})T1 
					left join resourcetag b on b.Id=T1.ResourceTagId;",
						item.Id);
					item.resourceTagList = Conn.Query<Model.ResourceTagModel>(sql2).ToList();
				}
				return resourceModelList;
			}

		}

		/// <summary>
		/// 获取资源详情
		/// </summary>
		/// <param name="resourceId">资源Id</param>
		public static Model.ResourceModel GetResourceDetail(int resourceId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select T1.*,c.* from( select * from resource where id={0}) T1 
				left join resourcetagrelation b on T1.Id=b.ResourceId 
				left join resourcetag c on b.ResourceTagId=c.id;",
				resourceId);
				var lookup = new Dictionary<int, Model.ResourceModel>();
				Conn.QueryAsync<Model.ResourceModel, Model.ResourceTagModel, Model.ResourceModel>(sql, (r, rt) =>
				{
					Model.ResourceModel resourceModel = null;
					if (!lookup.TryGetValue(r.Id, out resourceModel))
					{
						resourceModel = r;
						lookup.Add(r.Id, resourceModel);
					}
					if (rt != null)
					{
						resourceModel.resourceTagList.Add(rt);
					}
					return resourceModel;
				}, splitOn: "Id");
				return lookup.Values.ToList().FirstOrDefault();
			}

		}
		/// <summary>
		/// 更新资源标签
		/// </summary>
		/// <param name="resourceId">资源Id</param>
		/// <param name="resourceTagList">资源标签Id列表</param>
		/// <returns></returns>
		public static bool UpdateResourceTags(int resourceId, List<int> resourceTagList)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				StringBuilder sbr = new StringBuilder();
				sbr.AppendFormat("delete from ResourceTagRelation where ResourceId={0};", resourceId);
				DateTime timeNow = DateTime.Now;
				resourceTagList.ForEach(item =>
				{
					sbr.AppendFormat(@"insert into ResourceTagRelation(ResourceTagId,ResourceId,AddTime) values({0},{1},'{2}');",
						item, resourceId, timeNow.ToString("yyyy/MM/dd HH:mm:ss"));
				});
				var task = Conn.ExecuteAsync(sbr.ToString());
				return task.Result > 0;
			}

		}
		/// <summary>
		/// 更新某个资源的点赞数量+1
		/// </summary>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		public static bool UpdatePointLineNumPlusOne(int resourceId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update Resource set PointsLikeNum=PointsLikeNum+1 where Id = @Id;";
				var task = Conn.ExecuteAsync(sql, new { Id = resourceId });
				return task.Result > 0;
			}

		}
		/// <summary>
		/// 更新某个资源的下载数量+1
		/// </summary>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		public static bool UpdateDownloadNumPlusOne(int resourceId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update Resource set DownloadNum=DownloadNum+1 where Id = @Id;";
				var task = Conn.ExecuteAsync(sql, new { Id = resourceId });
				return task.Result > 0;
			}

		}
		/// <summary>
		/// 对某个资源评分
		/// </summary>
		/// <param name="resourceId"></param>
		/// <param name="score"></param>
		/// <returns></returns>
		public static bool UpdateScore(int resourceId, int score)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update Resource set ScoreNum=ScoreNum+1,ScoreTotal=ScoreTotal+@Score where Id = @Id;";
				var task = Conn.ExecuteAsync(sql, new { Id = resourceId, Score = score });
				return task.Result > 0;
			}
		}
		/// <summary>
		/// 获取推荐资源数据
		/// </summary>
		public static List<Model.ResourceModel> GetRecommendList(int resourceTypeId, int resourceTagId, ref Model.PageInfo pageInfo)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "";
				if (resourceTagId > 0)
				{
					sql = string.Format(@"select count(0) from(select b.* from( select * from resourcetagrelation where ResourceTagId={0})T1 
						left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={1};",
						resourceTagId, resourceTypeId);
					pageInfo.TotalCount = Conn.ExecuteScalar<int>(sql);
					sql = string.Format(@"select * from(select b.* from( select * from resourcetagrelation where ResourceTagId={0})T1 
						left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={1} ORDER BY T2.LastUpdateTime DESC
						limit {2},{3};",
					   resourceTagId, resourceTypeId, pageInfo.PageSize * (pageInfo.PageIndex - 1), pageInfo.PageSize);
				}
				else
				{
					sql = string.Format(@"select count(0) from ( select DISTINCT * from(select b.* from resourcetagrelation T1 
						left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={0}) T3;",
						resourceTypeId);
					pageInfo.TotalCount = Conn.ExecuteScalar<int>(sql);
					sql = string.Format(@"select DISTINCT * from(select b.* from resourcetagrelation T1 
						left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={0} ORDER BY T2.LastUpdateTime DESC
						limit {1},{2};",
					   resourceTypeId, pageInfo.PageSize * (pageInfo.PageIndex - 1), pageInfo.PageSize);
				}
				var resourceModelList = Conn.Query<Model.ResourceModel>(sql).ToList();
				foreach (var item in resourceModelList)
				{
					string sql2 = string.Format(@"select b.* from( select * from resourcetagrelation where ResourceId={0})T1 
					left join resourcetag b on b.Id=T1.ResourceTagId;",
						item.Id);
					item.resourceTagList = Conn.Query<Model.ResourceTagModel>(sql2).ToList();
				}
				return resourceModelList;
			}
		}
		/// <summary>
		/// 获取流行资源数据
		/// </summary>
		public static List<Model.ResourceModel> GetPopularList(int resourceTypeId, int resourceTagId, ref Model.PageInfo pageInfo)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "";
				if (resourceTagId > 0)
				{
					sql = string.Format(@"select count(0) from(select b.* from( select * from resourcetagrelation where ResourceTagId={0})T1 
						left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={1};",
						resourceTagId, resourceTypeId);
					pageInfo.TotalCount = Conn.ExecuteScalar<int>(sql);
					sql = string.Format(@"select * from(select b.* from( select * from resourcetagrelation where ResourceTagId={0})T1 
						left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={1} ORDER BY T2.LastUpdateTime DESC
						limit {2},{3};",
					   resourceTagId, resourceTypeId, pageInfo.PageSize * (pageInfo.PageIndex - 1), pageInfo.PageSize);
				}
				else
				{
					sql = string.Format(@"select count(0) from ( select DISTINCT * from(select b.* from resourcetagrelation T1 
						left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={0}) T3;",
						resourceTypeId);
					pageInfo.TotalCount = Conn.ExecuteScalar<int>(sql);
					sql = string.Format(@"select DISTINCT * from(select b.* from resourcetagrelation T1 
						left join resource b on b.id=T1.ResourceId) T2 where T2.ResourceTypeId={0} ORDER BY T2.LastUpdateTime DESC
						limit {1},{2};",
					   resourceTypeId, pageInfo.PageSize * (pageInfo.PageIndex - 1), pageInfo.PageSize);
				}
				var resourceModelList = Conn.Query<Model.ResourceModel>(sql).ToList();
				foreach (var item in resourceModelList)
				{
					string sql2 = string.Format(@"select b.* from( select * from resourcetagrelation where ResourceId={0})T1 
					left join resourcetag b on b.Id=T1.ResourceTagId;",
						item.Id);
					item.resourceTagList = Conn.Query<Model.ResourceTagModel>(sql2).ToList();
				}
				return resourceModelList;
			}
		}
		/// <summary>
		/// 玩家关注的玩家资源列表（注意：与推荐|流行数据不一样，增加了玩家关注的玩家资源数据）
		/// </summary>
		/// <param name="playerId">玩家Id</param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static List<Model.ResourceModel> GetAttentionList(int playerId, int resourceTypeId, int resourceTagId, ref Model.PageInfo pageInfo)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();

				string sql = "";
				if (resourceTagId > 0)
				{
					sql = string.Format(@"select count(0) from
				(select b.* from (select * from resourcetagrelation where ResourceTagId={0})T1 left join resource b on b.id=T1.ResourceId) T2
								where T2.ResourceTypeId={1} and PlayerId in (select playerAttentionId from playerAttention where playerId ={2});",
								resourceTagId, resourceTypeId, playerId);
					pageInfo.TotalCount = Conn.ExecuteScalar<int>(sql);
					sql = string.Format(@"
			select * from
				(select b.* from (select * from resourcetagrelation where ResourceTagId={0})T1 left join resource b on b.id=T1.ResourceId) T2
								where T2.ResourceTypeId={1} and PlayerId in (select playerAttentionId from playerAttention where playerId ={2})
					ORDER BY T2.PublishTime DESC limit {3},{4};",
						   resourceTagId, resourceTypeId, playerId, pageInfo.PageSize * (pageInfo.PageIndex - 1), pageInfo.PageSize);
				}
				else
				{
					sql = string.Format(@"select count(0) from resource T2
								where T2.ResourceTypeId={0} and PlayerId in (select playerAttentionId from playerAttention where playerId ={1});",
								resourceTypeId, playerId);
					pageInfo.TotalCount = Conn.ExecuteScalar<int>(sql);
					sql = string.Format(@"
			select * from resource T2
								where T2.ResourceTypeId={0} and PlayerId in (select playerAttentionId from playerAttention where playerId ={1})
					ORDER BY T2.PublishTime DESC limit {2},{3};",
						resourceTypeId, playerId, pageInfo.PageSize * (pageInfo.PageIndex - 1), pageInfo.PageSize);
				}
				var resourceModelList = Conn.Query<Model.ResourceModel>(sql).ToList();
				foreach (var item in resourceModelList)
				{
					string sql2 = string.Format(@"select b.* from( select * from resourcetagrelation where ResourceId={0})T1 
					left join resourcetag b on b.Id=T1.ResourceTagId;",
						item.Id);
					item.resourceTagList = Conn.Query<Model.ResourceTagModel>(sql2).ToList();
				}
				return resourceModelList;
			}
		}
	}
}
