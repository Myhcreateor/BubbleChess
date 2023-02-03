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
	[ProtoMember(3)]
	public bool IsMatch;
	//��������ͻ��˷��ص�����
	[ProtoMember(4)]
	public MatchResult MatchResult;
	[ProtoMember(5)]
	public string OpponentName;
	[ProtoMember(6)]
	public int OpponentId;
}