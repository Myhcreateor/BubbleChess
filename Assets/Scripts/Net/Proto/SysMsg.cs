using ProtoBuf;

[ProtoContract]
public class MsgSecret : MsgBase
{
	public MsgSecret()
	{
		ProtoType = ProtocolEnum.MsgSecret;
	}
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	[ProtoMember(2)]
	public string secret;
}

[ProtoContract]
public class MsgPing : MsgBase
{
	public MsgPing()
	{
		ProtoType = ProtocolEnum.MsgPing;
	}
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
}