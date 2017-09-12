using BaseCommon;
using SwigRaknetCS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerHall
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine(Environment.NewLine + "******************【我的世界】大厅服务器");
            Console.WriteLine("* 服务器正在启动.......");

            string errorMsg = "";
            bool flag = SwigRaknetCSPreInit.JudgeRaknetRun(out errorMsg);
            if (!flag)
            {
                Console.WriteLine("------------------------" + errorMsg);
                return;
            }
            Console.WriteLine("--------------------------Raknet测试通过");

            #region 初始化

            #region 注册包体
            PackageConfig.Register();
            #endregion

            #region csv安装配置
            CSVFileConfig.InstallConfig();
            #endregion

            #region 服务设置
            //读取配置文件
            INIBase ib = new INIBase("config.ini");
            string port_tcp = ib.IniReadValue("local", "port_tcp");
            string port_udp = ib.IniReadValue("local", "port_udp");
            //面向用户的Tcp服务
            TcpServerManager.Instance.Start(ushort.Parse(port_tcp));
            //面向用户的UdpAgent服务
            UdpAgentServerManager.Instance.Start(ushort.Parse(port_udp));
            #endregion

            #region 初始化日志
            Log.Initialize("./loglocal", "", true);
            Log.WriteInfo("初始化日志成功");
            #endregion

            #endregion

            Console.WriteLine("* 服务器启动成功.......");



            #region 测试

            #region 数据库连接测试
            var b = BLL.PlayerBLL.QuerySingle(1);
            Console.WriteLine(b?.Name);
            #endregion


            #region 创建一个测试房间
            {
                bool isTestExecute = false;
                AnalogyHouseCreate.Do(isTestExecute);
            }
            #endregion

            int count = 10000;

            #region 初始化Player

            //BLL.BaseBLL.TruncateTable(new Model.PlayerModel());
            //ThreadPool.QueueUserWorkItem(o =>
            //{
            //	Console.WriteLine(string.Format(@"Stopwatch开始初始化player表...开始时间：{0}", DateTime.Now.ToStr()));
            //	Stopwatch stopwatch = new Stopwatch();
            //	stopwatch.Start();
            //	List<Model.PlayerModel> pList = new List<Model.PlayerModel>();
            //	//int count = Convert.ToInt32(Math.Pow(10, 5));

            //	//十万用户插入所花时间为5.4779秒
            //	//一百万用户插入所花时间为36.4614秒
            //	//一千万用户插入所花时间为352.1608秒

            //	int groupCol = Convert.ToInt32(Math.Pow(10, 5)) / 2;

            //	for (int i = 1; i <= count; i++)
            //	{
            //		DateTime timeNow = DateTime.Now;
            //		Model.PlayerModel playerModel = new Model.PlayerModel()
            //		{
            //			Name = "name" + i,
            //			HeadImg = "http://img2.niutuku.com/1312/0850/0850-niutuku.com-30110.jpg",
            //			Level = 1,
            //			Money = 1000000,
            //			RegistTime = timeNow,
            //			LastLoginTime = timeNow
            //		};
            //		pList.Add(playerModel);
            //	}
            //	var groupLi = pList.GetMatrix(groupCol).ToList();
            //	foreach (var itemLi in groupLi)
            //	{
            //		BLL.BaseBLL.BatchInsert(itemLi);
            //	}

            //	stopwatch.Stop();
            //	Console.WriteLine(string.Format(@"Stopwatch初始化player表完成...结束时间：{0}", DateTime.Now.ToStr()));
            //	Console.WriteLine(string.Format("player：持续时间为：{0}秒", Math.Round(stopwatch.Elapsed.TotalSeconds, 4)));
            //});

            #endregion


            #region 初始化背包

            //BLL.BaseBLL.TruncateTable(new Model.PlayerBackpackGoodsModel());
            //ThreadPool.QueueUserWorkItem(o =>
            //{
            //	Console.WriteLine(string.Format(@"Stopwatch开始初始化PlayerBackpackGoodsModel表...开始时间：{0}", DateTime.Now.ToStr()));
            //	Stopwatch stopwatch = new Stopwatch();
            //	stopwatch.Start();
            //	List<Model.PlayerBackpackGoodsModel> list = new List<Model.PlayerBackpackGoodsModel>();

            //	int groupCol = Convert.ToInt32(Math.Pow(10, 5)) / 2;

            //	for (int i = 1; i <= count; i++)
            //	{
            //		DateTime timeNow = DateTime.Now;
            //		Model.PlayerBackpackGoodsModel backpackGoods = new Model.PlayerBackpackGoodsModel
            //		{
            //			PlayerId = i,
            //			HeadStr = "1001|2001|3001",
            //			CoatStr = "1001|2001|3001",
            //			PantStr = "1001|2001|3001"
            //		};
            //		list.Add(backpackGoods);
            //	}
            //	var groupLi = list.GetMatrix(groupCol).ToList();
            //	foreach (var itemLi in groupLi)
            //	{
            //		BLL.BaseBLL.BatchInsert(itemLi);
            //	}

            //	stopwatch.Stop();
            //	Console.WriteLine(string.Format(@"Stopwatch初始化PlayerBackpackGoodsModel表完成...结束时间：{0}", DateTime.Now.ToStr()));
            //	Console.WriteLine(string.Format("PlayerBackpackGoodsModel：持续时间为：{0}秒", Math.Round(stopwatch.Elapsed.TotalSeconds, 4)));
            //});

            #endregion

            #region 文件上传
            //文件上传
            //string fileName = @"E:\测试文件\文件上传测试.zip";
            //var buffer = fileName.File2Bytes();
            //string savePath = @"E:\MinecraftResource";
            //string newFileName = Path.Combine(savePath,
            //	Guid.NewGuid().ToString().Replace("-", "") + ".zip");
            //buffer.Bytes2File(newFileName); 
            #endregion

            //var disguiseModel = new Model.PlayerDisguiseModel
            //{
            //	PlayerId = 1
            //};

            #endregion


            while (true) ;
        }
    }
}
