using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpHouseTest
{
	/// <summary>
	/// 窗体载体类
	/// </summary>
	public static class CommonForm
	{

		/// <summary>
		/// 操作对象
		/// </summary>
		public struct Obj
		{
			public static LoginForm loginForm = null;
			public static PlayerInfoForm playerInfoForm = null;
			public static ChatForm chatForm = null;
		}
		public struct Option
		{
			/// <summary>
			/// 退出应用
			/// </summary>
			public static void ExitProgram()
			{
				Environment.Exit(0);
			}
		}

	}
}
