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
	[ProtoMember(3)]
	public bool IsMatch;
	//服务器向客户端返回的数据
	[ProtoMember(4)]
	public MatchResult MatchResult;
	[ProtoMember(5)]
	public string OpponentName;
	[ProtoMember(6)]
	public int OpponentId;
}