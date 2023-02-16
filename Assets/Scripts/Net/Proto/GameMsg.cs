using ProtoBuf;

[ProtoContract]
public class MsgStartBattle : MsgBase
{
	public MsgStartBattle()
	{
		ProtoType = ProtocolEnum.MsgStartBattle;
	}
	//客户端向服务器发送的数据
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	//服务器向客户端返回的数据
	[ProtoMember(2)]
	//先手还是后手，如果是1则是先手，如果是2则是后手
	public int order;
}
[ProtoContract]
public class MsgUpdateChessBoard : MsgBase
{
	public MsgUpdateChessBoard()
	{
		ProtoType = ProtocolEnum.MsgUpdateChessBoard;
	}
	//客户端向服务器发送的数据
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	[ProtoMember(2)]
	public string trans;
	//服务器向客户端发送的数据
	[ProtoMember(3)]
	public UpdateResult UpdateResult;
}
[ProtoContract]
public class MsgCardTrigger : MsgBase
{
	public MsgCardTrigger()
	{
		ProtoType = ProtocolEnum.MsgCardTrigger;
	}
	//客户端向服务器发送的数据
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	[ProtoMember(2)]
	public string ParticleEffectPath;
	[ProtoMember(3)]
	public string trans;
	[ProtoMember(4)]
	public int[] ChessPieceLinearArray;
	//服务器向客户端发送的数据
	[ProtoMember(5)]
	public UpdateResult UpdateResult;
}

[ProtoContract]
public class MsgEndOfBattle : MsgBase
{
	public MsgEndOfBattle()
	{
		ProtoType = ProtocolEnum.MsgEndOfBattle;
	}
	//客户端向服务器发送的数据
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
}