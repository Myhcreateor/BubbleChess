using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//枚举名字需要和协议类名一样
public enum ProtocolEnum
{
	None = 0,
	MsgSecret = 1,
	MsgPing = 2,
	MsgRegister = 3,
	MsgLogin = 4,
	MsgMatch = 5,
	MsgStartBattle,
}

