using ProtoBuf;

[ProtoContract]
public class MsgStartBattle : MsgBase
{
	public MsgStartBattle()
	{
		ProtoType = ProtocolEnum.MsgStartBattle;
	}
	//�ͻ�������������͵�����
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	//��������ͻ��˷��ص�����
	[ProtoMember(2)]
	//���ֻ��Ǻ��֣������1�������֣������2���Ǻ���
	public int order;
}
[ProtoContract]
public class MsgUpdateChessBoard : MsgBase
{
	public MsgUpdateChessBoard()
	{
		ProtoType = ProtocolEnum.MsgUpdateChessBoard;
	}
	//�ͻ�������������͵�����
	[ProtoMember(1)]
	public override ProtocolEnum ProtoType { get; set; }
	[ProtoMember(2)]
	public string trans;
	//��������ͻ��˷��͵�����
	[ProtoMember(3)]
	public UpdateResult UpdateResult;
}