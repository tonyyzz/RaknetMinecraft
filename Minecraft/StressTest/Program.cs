using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 压力测试
/// </summary>
namespace StressTest
{
	class Program
	{
		static void Main(string[] args)
		{
			StressTestInit.Init();

			while (true) ;
		}
	}
}
