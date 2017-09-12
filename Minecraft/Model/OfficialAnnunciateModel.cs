using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 官方通告model
	/// </summary>
	//代码生成器生成
	public class OfficialAnnunciateModel
	{
		public OfficialAnnunciateModel()
		{
			Id = 0;
			Title = "";
			Description = "";
			Img = "";
			LinkType = 0;
			LinkUrl = "";
			UrlProtocolId = 0;
			AddTime = new DateTime(1900, 1, 1);
			UpdateTime = new DateTime(1900, 1, 1);
			CompositorId = 0;
			OperatorId = 0;
		}
		/// <summary>
		/// 通告Id
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 通告标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 通告描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 通告图片链接地址
		/// </summary>
		public string Img { get; set; }
		/// <summary>
		/// 通告链接类型（1表示外部链接；2表示内部协议）
		/// </summary>
		public int LinkType { get; set; }
		/// <summary>
		/// 通告链接Url
		/// </summary>
		public string LinkUrl { get; set; }
		/// <summary>
		/// 通告内部协议Id
		/// </summary>
		public int UrlProtocolId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime AddTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateTime { get; set; }
		/// <summary>
		/// 排序Id
		/// </summary>
		public int CompositorId { get; set; }
		/// <summary>
		/// 操作人Id
		/// </summary>
		public int OperatorId { get; set; }
	}
}
