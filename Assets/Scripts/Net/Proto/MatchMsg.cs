using ProtoBuf;

[ProtoContract]
public class MsgMatch : MsgBase
{
	public MsgMatch()
	{
		ProtoType = ProtocolEnum.MsgMatch;
	}
	//客户端向服务器发送的数据
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	[ProtoMember(2)]
	public string Account;
	//服务器向客户端返回的数据
	[ProtoMember(3)]
	public MatchResult MatchResult;
	[ProtoMember(4)]
	public string OpponentName;
	[ProtoMember(5)]
	public int OpponentId;
}