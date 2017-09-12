using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace DAL
{
	public class ResourceCommentDAL : BaseDAL
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
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select count(0) from ( select* from resourcecomment where ResourceId={0}) T  
					left join player p on T.PlayerId=p.id
					left join resourcecommentreply rcr on T.Id = rcr.CommentId;",
					resourceId);
				pageInfo.TotalCount = Conn.ExecuteScalar<int>(sql);
				sql = string.Format(@"select T.*,p.Name as PlayerName,p.HeadImg as PlayerHeadImg,p.`Level` as PlayerLevel,rcr.ReplyId,rcr.Reply,rcr.ReplyTime,rcr.ReplyPersonId from ( select* from resourcecomment where ResourceId={0}) T  
					left join player p on T.PlayerId=p.id
					left join resourcecommentreply rcr on T.Id = rcr.CommentId limit {1},{2};",
					resourceId, pageInfo.PageSize * (pageInfo.PageIndex - 1), pageInfo.PageSize);
				var lookup = new Dictionary<int, Model.ResourceCommentModel>();
				Conn.QueryAsync<Model.ResourceCommentModel, Model.ResourceCommentReplyModel, Model.ResourceCommentModel>(sql, (rc, rcr) =>
				{
					Model.ResourceCommentModel resourceCommentModel = rc;
					if (!lookup.TryGetValue(rc.Id, out resourceCommentModel))
					{
						resourceCommentModel = rc;
						lookup.Add(rc.Id, resourceCommentModel);
					}
					if (rcr != null)
					{
						resourceCommentModel.resourceCommentReplyList.Add(rcr);
					}
					return resourceCommentModel;
				}, splitOn: "ReplyId");
				return lookup.Values.ToList();
			}

		}
	}
}
