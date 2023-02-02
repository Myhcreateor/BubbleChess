using ProtoBuf;

[ProtoContract]
public class MsgMatch : MsgBase
{
	public MsgMatch()
	{
		ProtoType = ProtocolEnum.MsgMatch;
	}
	//�ͻ�������������͵�����
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	[ProtoMember(2)]
	public string Account;
	//��������ͻ��˷��ص�����
	[ProtoMember(3)]
	public MatchResult MatchResult;
	[ProtoMember(4)]
	public string OpponentName;
	[ProtoMember(5)]
	public int OpponentId;
}