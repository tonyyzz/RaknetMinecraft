using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 某个资源标签Id集合表
	/// </summary>
	public class ResourceTagRelationModel
	{
		public ResourceTagRelationModel()
		{
			ResourceTagId = 0;
			ResourceId = 0;
			AddTime = new DateTime(1900, 1, 1);
		}
		/// <summary>
		/// 资源标签Id
		/// </summary>
		public int ResourceTagId { get; set; }
		/// <summary>
		/// 资源Id
		/// </summary>
		public int ResourceId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime AddTime { get; set; }
	}
}
