using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
	public class OfficialAnnunciateDAL : BaseDAL
	{
		/// <summary>
		/// 异步获取所有官方通告
		/// </summary>
		public static List<Model.OfficialAnnunciateModel> GetListAll()
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "select * from officialannunciate";
				var task = Conn.QueryAsync<Model.OfficialAnnunciateModel>(sql);
				return task.Result == null ? new List<Model.OfficialAnnunciateModel>() : task.Result.ToList();

			}
		}
	}
}
