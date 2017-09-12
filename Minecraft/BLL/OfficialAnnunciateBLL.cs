using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OfficialAnnunciateBLL
    {
        /// <summary>
        /// 异步获取所有官方通告
        /// </summary>
        public static List<Model.OfficialAnnunciateModel> GetListAll()
        {
            return DAL.OfficialAnnunciateDAL.GetListAll();
        }
    }
}
