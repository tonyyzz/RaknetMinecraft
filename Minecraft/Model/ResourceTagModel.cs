using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 资源标签model
	/// </summary>
	//代码生成器生成
	public partial class ResourceTagModel
	{
		public ResourceTagModel()
		{
			Id = 0;
			ResourceTypeId = 0;
			Name = "";
			AddTime = new DateTime(1900, 1, 1);
			UpdateTime = new DateTime(1900, 1, 1);
			CompositorId = 0;
			OperatorId = 0;
		}
		/// <summary>
		/// 资源标签分类Id
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 资源类型Id
		/// </summary>
		public int ResourceTypeId { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
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

	public partial class ResourceTagModel
	{
		/// <summary>
		/// 资源列表
		/// </summary>
		public List<ResourceModel> resourceList = new List<ResourceModel>();
	}
}
