using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	/// <summary>
	/// 登录返回接口
	/// </summary>
	class PlayerLoginRetPacket : Package
	{
		public PlayerLoginRetPacket() { }
		public PlayerLoginRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerLoginRetPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_login_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			string name = ReadString(); //昵称
			int sex = ReadInt(); //性别
			int money = ReadInt(); //金币

			PlayerMng.player.Name = name;
			PlayerMng.player.Sex = sex;
			PlayerMng.player.Money = money;


			CommonForm.Obj.loginForm.BeginInvoke(new Action(CommonForm.Obj.loginForm.LoginSuccess));
		}
	}
}
