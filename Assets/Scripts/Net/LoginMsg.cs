using ProtoBuf;

[ProtoContract]
public class MsgRegister : MsgBase
{
	public MsgRegister()
	{
		ProtoType = ProtocolEnum.MsgRegister;
	}
	//客户端向服务器发送的数据
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	[ProtoMember(2)]
	public string Account;
	[ProtoMember(3)]
	public string Password;
	[ProtoMember(4)]
	//验证码
	public string Code;
	[ProtoMember(5)]
	public RegisterType RegisterType;
	//服务器向客户端返回的数据
	[ProtoMember(6)]
	public RegisterResult Result;
}

[ProtoContract]
public class MsgLogin : MsgBase
{
	public MsgLogin()
	{
		ProtoType = ProtocolEnum.MsgLogin;
	}
	//客户端向服务器发送的数据
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	[ProtoMember(2)]
	public string Account;
	[ProtoMember(3)]
	public string Password;
	[ProtoMember(4)]
	public LoginType LoginType;
	//服务器向客户端返回的数据
	[ProtoMember(5)]
	public LoginResult Result;
	[ProtoMember(6)]
	public string Token;

}