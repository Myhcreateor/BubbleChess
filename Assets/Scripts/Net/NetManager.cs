using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetManager : Singleton<NetManager>
{
	public enum NetEvent
	{
		ConnentSucc = 1,
		ConnentFail, Close
	}
	public string PublicKey = "SimpleServer";
	public string SercetKey { get; private set; }
	private Socket socket;
	private ByteArray readBuff;
	private string m_ip;
	private int m_port;
	//����״̬
	private bool isConnecting = false;
	private bool isClosing = false;

	private Thread msgThread;
	private Thread heartThread;
	private List<MsgBase> msgList;
	private List<MsgBase> unityMsgList;
	private int msgCount = 0;

	static long lastPongTime;
	static long lastPingTime;
	private static long pingInterval = 30;
	public delegate void EventListener(string str);
	Dictionary<NetEvent, EventListener> listenerDic = new Dictionary<NetEvent, EventListener>();
	public delegate void ProtoListener(MsgBase msgBase);
	Dictionary<ProtocolEnum, ProtoListener> protoDic = new Dictionary<ProtocolEnum, ProtoListener>();

	private bool isLostConnection = false;
	//�Ƿ����ӳɹ���
	private bool isConnectSuccessed = false;
	private bool isReconnect = false;
	private Queue<ByteArray> writeQueue;

	private NetworkReachability currentNetwork = NetworkReachability.NotReachable;
	protected override void Awake()
	{
		base.Awake();
	}
	void InitState()
	{
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		readBuff = new ByteArray();
		writeQueue = new Queue<ByteArray>();
		isConnecting = false;
		isClosing = false;
		msgList = new List<MsgBase>();
		unityMsgList = new List<MsgBase>();
		msgCount = 0;
		lastPingTime = GetTimeStamp();
		lastPongTime = GetTimeStamp();
	}

	public IEnumerator CheckNet()
	{
		currentNetwork = Application.internetReachability;
		while (true)
		{
			yield return new WaitForSeconds(1);
			if (isConnectSuccessed)
			{
				if (currentNetwork != Application.internetReachability)
				{
					ReConnect();
					currentNetwork = Application.internetReachability;
				}
			}
		}
	}
	//���������¼�
	public void AddEventListener(NetEvent netEvent, EventListener listener)
	{
		if (listenerDic.ContainsKey(netEvent))
		{
			listenerDic[netEvent] += listener;
		}
		else
		{
			listenerDic[netEvent] = listener;
		}
	}
	public void RemoveEventListener(NetEvent netEvent, EventListener listener)
	{
		if (listenerDic.ContainsKey(netEvent))
		{
			listenerDic[netEvent] -= listener;
			if (listenerDic[netEvent] == null)
			{
				listenerDic.Remove(netEvent);
			}
		}
	}

	void FirstEvent(NetEvent netEvent, string str)
	{
		if (listenerDic.ContainsKey(netEvent))
		{
			listenerDic[netEvent](str);
		}
	}

	public void AddProtoListener(ProtocolEnum protocolEnum, ProtoListener protoListener)
	{
		protoDic[protocolEnum] = protoListener;
	}
	public void FirstProto(ProtocolEnum protocolEnum, MsgBase msgBase)
	{
		if (protoDic.ContainsKey(protocolEnum))
		{
			protoDic[protocolEnum](msgBase);
			//Debug.Log("������" + msgBase.ToString());
		}
		else
		{
			if(msgBase is MsgUpdateChessBoard)
			{
				AddProtoListener(ProtocolEnum.MsgUpdateChessBoard, (resmsg) =>
				{
					MsgUpdateChessBoard msgUpdateChessBroad = (MsgUpdateChessBoard)resmsg;
					string s = msgUpdateChessBroad.trans;
					ChessBoardController.Instance.UpdateChessPieceArrays(int.Parse(s.Split(',')[0]), int.Parse(s.Split(',')[1]), GameController.Instance.GetOpponent());
					EventHandler.CallUpdateChessBoardEvent();
					ChessBoardController.Instance.IsPlayeChess = true;

				});
				protoDic[protocolEnum](msgBase);
			}
			if (msgBase is MsgCardTrigger)
			{
				AddProtoListener(ProtocolEnum.MsgCardTrigger, (resmsg) =>
				{
					MsgCardTrigger msgCardTrigger = (MsgCardTrigger)resmsg;
					for (int i = 0; i < msgCardTrigger. ChessPieceLinearArray.Length; i++)
					{
						ChessBoardController.Instance.chessPieceArrays[i / 8][i % 8] = msgCardTrigger.ChessPieceLinearArray[i];
					}
					EventHandler.CallGenerateParticleEffectEvent(msgCardTrigger.trans, msgCardTrigger.ParticleEffectPath);
					EventHandler.CallUpdateChessBoardEvent();
				});
				protoDic[protocolEnum](msgBase);
			}
			if (msgBase is MsgEndOfBattle)
			{
				AddProtoListener(ProtocolEnum.MsgEndOfBattle, (resmsg) =>
				{
					MsgEndOfBattle msgEndOfBattle = (MsgEndOfBattle)resmsg;
					ChessBoardController.Instance. EndOfBattle();
				});
				protoDic[protocolEnum](msgBase);
			}
		}
	}
	//���ӷ���������
	public void Connect(string ip, int port)
	{
		if (socket != null && socket.Connected)
		{
			Debug.LogError("����ʧ��,�Ѿ�������");
			return;
		}
		if (isConnecting)
		{
			Debug.LogError("����ʧ��,����������");
			return;
		}
		InitState();
		socket.NoDelay = true;
		isConnecting = true;
		socket.BeginConnect(ip, port, ConnectCallback, socket);
		m_ip = ip;
		m_port = port;
	}
	//��������
	public void ReConnect()
	{
		Connect(m_ip, m_port);
		isReconnect = true;
	}
	void ConnectCallback(IAsyncResult ar)
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;
			socket.EndConnect(ar);
			FirstEvent(NetEvent.ConnentSucc, "");
			isConnectSuccessed = true;
			msgThread = new Thread(MsgThread);
			msgThread.IsBackground = true;
			msgThread.Start();
			heartThread = new Thread(PingThread);
			heartThread.IsBackground = true;
			heartThread.Start();
			isConnecting = false;
			ProtocolManager.SecretRequest();
			Debug.Log("Socket Connect Success");
			socket.BeginReceive(readBuff.bytes, readBuff.writeIndex, readBuff.Remain, 0, ReceiveCallback, socket);
		}
		catch (SocketException ex)
		{
			Debug.LogError("����ʧ��" + ex.ToString());
			isConnecting = false;
		}
	}

	//�������ݻص�
	void ReceiveCallback(IAsyncResult ar)
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;
			int count = socket.EndReceive(ar);
			if (count <= 0)
			{
				//�ر�����
				Close();
				return;
			}
			readBuff.writeIndex += count;
			OnReceieveData();
			if (readBuff.Remain < 8)
			{
				readBuff.MoveBytes();
				readBuff.ReSize(readBuff.Length * 2);
			}
			socket.BeginReceive(readBuff.bytes, readBuff.writeIndex, readBuff.Remain, 0, ReceiveCallback, socket);
		}
		catch (SocketException ex)
		{
			Debug.LogError("Socket ReceiveCallbackʧ��" + ex.ToString());
			Close();
		}

	}
	//�����ݽ��д���
	void OnReceieveData()
	{
		if (readBuff.Length <= 4 || readBuff.readIndex < 0) { return; }
		int readIndex = readBuff.readIndex;
		byte[] bytes = readBuff.bytes;
		int bodyLength = BitConverter.ToInt32(bytes, readIndex);
		//��ȡЭ�鳤�Ⱥ�����жϣ������Ϣ����С�ڶ���������Ϣ���ȣ�˵��û������������
		if (readBuff.Length < bodyLength + 4)
		{
			return;
		}
		readBuff.readIndex += 4;
		int nameCount = 0;
		ProtocolEnum protocol = MsgBase.DecodeName(readBuff.bytes, readBuff.readIndex, out nameCount);
		if (protocol == ProtocolEnum.None)
		{
			Debug.LogError("OnReceieveData MsgBase.DecodeName Fail");
			Close();
			return;
		}
		readBuff.readIndex += nameCount;
		//����Э����
		int bodyCount = bodyLength - nameCount;
		try
		{
			MsgBase msgBase = MsgBase.Decode(protocol, readBuff.bytes, readBuff.readIndex, bodyCount);
			if (msgBase == null)
			{
				Debug.LogError("��������Э�����ݽ�������");
				Close();
				return;
			}
			readBuff.readIndex += bodyCount;
			readBuff.CheckAndMoveBytes();
			//Э�����Ĳ���
			lock (msgList)
			{
				msgList.Add(msgBase);
			}
			msgCount++;
			//����ճ�������
			if (readBuff.Length > 4)
			{
				OnReceieveData();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Socket OnReceieveData Error" + ex.ToString());
			Close();
		}
	}
	public void Update()
	{
		if (isLostConnection &&isConnectSuccessed)
		{
			//UI�����Ƿ�����
			//��������
			ReConnect();
			isLostConnection = false;
		}
		MsgUpdate();
	}
	void MsgUpdate()
	{
		if (socket != null && socket.Connected)
		{
			if (msgCount == 0) return;
			MsgBase msgBase = null;
			lock (unityMsgList)
			{
				if (unityMsgList.Count > 0)
				{
					msgBase = unityMsgList[0];
					unityMsgList.RemoveAt(0);
					msgCount--;
				}
			}
			if (msgBase != null)
			{
				FirstProto(msgBase.ProtoType, msgBase);
			}
		}
	}
	void MsgThread()
	{
		while (socket != null && socket.Connected)
		{
			if (msgList.Count <= 0) continue;
			MsgBase msgBase = null;
			lock (msgList)
			{
				if (msgList.Count > 0)
				{
					msgBase = msgList[0];
					msgList.RemoveAt(0);
				}
			}
			if (msgBase != null)
			{
				if (msgBase is MsgPing)
				{
					lastPongTime = GetTimeStamp();
					Debug.Log("�յ�����������");
					msgCount--;
				}
				else
				{
					lock (unityMsgList)
					{
						unityMsgList.Add(msgBase);
					}
				}
			}
			else
			{
				break;
			}
		}
	}
	void PingThread()
	{
		while (socket != null && socket.Connected)
		{
			long timeNow = GetTimeStamp();
			if (timeNow - lastPingTime > pingInterval)
			{
				MsgPing msgPing = new MsgPing();
				SendMessage(msgPing);
				lastPingTime = GetTimeStamp();
			}
			if (timeNow - lastPongTime > pingInterval * 4)
			{
				Close(false);
			}
		}
	}
	//�������ݵ�������
	public void SendMessage(MsgBase msgBase)
	{
		if (socket == null || !socket.Connected) return;
		if (isConnecting)
		{
			Debug.LogError("�������ӷ������У��޷�������Ϣ��");
			return;
		}
		if (isClosing)
		{
			Debug.LogError("���ڹرշ������У��޷�������Ϣ��");
			return;
		}
		try
		{
			byte[] nameBytes = MsgBase.EncodeName(msgBase);
			byte[] bodyBytes = MsgBase.Encode(msgBase);
			int len = nameBytes.Length + bodyBytes.Length;
			byte[] byteHead = BitConverter.GetBytes(len);
			byte[] sendByte = new byte[len + byteHead.Length];
			Array.Copy(byteHead, 0, sendByte, 0, byteHead.Length);
			Array.Copy(nameBytes, 0, sendByte, byteHead.Length, nameBytes.Length);
			Array.Copy(bodyBytes, 0, sendByte, byteHead.Length + nameBytes.Length, bodyBytes.Length);
			ByteArray ba = new ByteArray(sendByte);
			int count = 0;
			lock (writeQueue)
			{
				writeQueue.Enqueue(ba);
				count = writeQueue.Count;
			}
			if (count == 1)
			{
				socket.BeginSend(sendByte, 0, sendByte.Length, 0, SendCallBack, socket);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("SendMessage Error" + ex.ToString());
			Close();
		}
	}
	void SendCallBack(IAsyncResult ar)
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;
			if (socket == null || !socket.Connected) return;
			int count = socket.EndSend(ar);
			//�ж��Ƿ�������
			ByteArray ba;
			lock (writeQueue)
			{
				ba = writeQueue.First();
			}
			ba.readIndex += count;
			if (ba.Length == 0)
			{
				lock (writeQueue)
				{
					writeQueue.Dequeue();
					if (writeQueue.Count > 0)
					{
						ba = writeQueue.First();
					}
					else
					{
						ba = null;
					}
				}
			}
			//���Ͳ��������߷����������ڵڶ�������
			if (ba != null)
			{
				socket.BeginSend(ba.bytes, ba.readIndex, ba.Length, 0, SendCallBack, socket);
			}
			//ȷ���ر�����ʱ���Ȱ���Ϣ���ͳ�ȥ
			else if (isClosing)
			{
				RealClose();
			}
		}
		catch (SocketException se)
		{
			Debug.LogError("SendCallBack Error" + se.ToString());
			Close();
		}
	}
	public void Close(bool normal = true)
	{
		if (socket == null || isConnecting)
		{
			return;
		}
		if (writeQueue.Count > 0)
		{
			isClosing = true;
		}
		else
		{
			RealClose(normal);
		}
	}
	void RealClose(bool normal = true)
	{
		SercetKey = "";
		socket.Close();
		FirstEvent(NetEvent.Close, normal.ToString());
		isLostConnection = true;
		if (heartThread != null && heartThread.IsAlive)
		{
			heartThread.Abort();
			heartThread = null;
		}
		if (msgThread != null && msgThread.IsAlive)
		{
			msgThread.Abort();
			msgThread = null;
		}
		Debug.Log("Close Socket");
	}
	public void Setkey(string key)
	{
		SercetKey = key;
	}
	public static long GetTimeStamp()
	{
		TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
		return Convert.ToInt64(ts.TotalSeconds);
	}
}
