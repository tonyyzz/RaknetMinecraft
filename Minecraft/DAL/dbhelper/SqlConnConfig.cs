using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class SqlConnConfig
    {
        /// <summary>
		/// 玩家数据库连接字符串（废弃不用）
		/// </summary>
		public static string ConnStr
        {
            get
            {
                INIBase ib = new INIBase("config.ini");
                string ip = ib.IniReadValue("sql", "ip");
                string port = ib.IniReadValue("sql", "port");
                string psw = ib.IniReadValue("sql", "psw");
                var connStr = string.Format(@"server={0};user={1};database={2};port={3};password={4};Charset=utf8;",
                    ip, "root", "myworld", port, psw);
                return connStr;
            }
        }
    }
}
