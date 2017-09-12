using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 分页model
	/// </summary>
	public class PageInfo
	{
		/// <summary>
		/// 当前页码数值
		/// </summary>
		public int PageIndex { get; set; }
		/// <summary>
		/// 每页数据量
		/// </summary>
		public int PageSize { get; set; }
		/// <summary>
		/// 总数据量
		/// </summary>
		public int TotalCount { get; set; }
		/// <summary>
		/// 总页数
		/// </summary>
		public int PageCount
		{
			get
			{
				return Convert.ToInt32(Math.Ceiling(TotalCount * 1.0 / PageSize));
			}
		}

		/// <summary>
		/// 分页构造方法
		/// </summary>
		/// <param name="pageIndex">当前页码数值</param>
		/// <param name="pageSize">每页数据量</param>
		public PageInfo(int pageIndex, int pageSize)
		{
			if (pageIndex < 1 || pageSize < 1)
			{
				throw new Exception("分页参数非法");
			}
			PageIndex = pageIndex;
			PageSize = pageSize;
			TotalCount = 0;
		}
	}
}
