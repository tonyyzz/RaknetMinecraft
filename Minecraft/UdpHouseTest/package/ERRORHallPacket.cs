using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	/// <summary>
	/// 错误消息
	/// </summary>
	class ERRORHallPacket : Package
	{
		public ERRORHallPacket() { }
		public ERRORHallPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ERRORHallPacket(null, 0,
				MainCommand.MC_ERROR, SecondCommand.SC_ERROR_hall);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int IntVal = ReadInt(); //返回值
			switch (IntVal)
			{
				case 1:
					{
						//正在登陆中
						if (CommonForm.Obj.loginForm.IsHandleCreated)
							CommonForm.Obj.loginForm.BeginInvoke(new Action(CommonForm.Obj.loginForm.Islogin));
					}
					break;
			}
		}
	}
}
