using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest
{
	class MngToolInit
	{
		//private static Server server = null;
		public static void Init()
		{
			//RegisterPackage();
			PackageConfig.Register();

			//! 读取配置文件
			//INIBase ib = new INIBase("config.ini");
			//string port = ib.IniReadValue("server", "port");
			//server = new Server(6);
			//server.Start(ushort.Parse(port));
			///连接中心服务器
			//string hall_server_ip = ib.IniReadValue("hallserver", "ip");
			//string hall_server_port = ib.IniReadValue("hallserver", "port");
			//server.StartClient(hall_server_ip, ushort.Parse(hall_server_port));

			///初始化日志
			//Log.Initialize("./loglocal", "", true);
			//Log.WriteInfo("初始化日志成功");

			

		}

		
	}
}
