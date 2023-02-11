using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtocolManager
{
	public static void SecretRequest()
	{
		MsgSecret msgSecret = new MsgSecret();
		NetManager.Instance.SendMessage(msgSecret);
		NetManager.Instance.AddProtoListener(ProtocolEnum.MsgSecret, (resmsg) =>
		{
			NetManager.Instance.Setkey(((MsgSecret)resmsg).secret);
			Debug.Log("��ȡ��Կ��" + ((MsgSecret)resmsg).secret);
		});
	}
	//ע��Э����ύ
	public static void Register(RegisterType registerType, string userName, string password, string code, Action<RegisterResult> callback)
	{
		MsgRegister msgRegister = new MsgRegister();
		msgRegister.RegisterType = registerType;
		msgRegister.Account = userName;
		msgRegister.Password = password;
		msgRegister.Code = code;
		NetManager.Instance.SendMessage(msgRegister);
		NetManager.Instance.AddProtoListener(ProtocolEnum.MsgRegister, (resmsg) =>
		{
			MsgRegister msg = (MsgRegister)resmsg;
			callback(msg.Result);
		});
	}
	//��¼Э����ύ
	public static void Login(LoginType loginType, string userName, string password, Action<LoginResult, string> callback)
	{
		MsgLogin msgLogin = new MsgLogin();
		msgLogin.LoginType = loginType;
		msgLogin.Account = userName;
		msgLogin.Password = password;
		NetManager.Instance.SendMessage(msgLogin);
		NetManager.Instance.AddProtoListener(ProtocolEnum.MsgLogin, (resmsg) =>
		{
			MsgLogin msg = (MsgLogin)resmsg;
			callback(msg.Result, msg.Token);
		});
	}
	//ƥ��Э����ύ
	public static void Match(string userName,bool isMatch ,Action<MatchResult,string,int> callback)
	{
		MsgMatch msgMatch= new MsgMatch();
		msgMatch.Account = userName;
		msgMatch.IsMatch = isMatch;
		NetManager.Instance.SendMessage(msgMatch); 
		NetManager.Instance.AddProtoListener(ProtocolEnum.MsgMatch, (resmsg) =>
		{
			MsgMatch msg = (MsgMatch)resmsg;
			callback(msg.MatchResult,msg.OpponentName,msg.OpponentId);
		});
	}
	//��Ϸ��ʼЭ��
	public static void StartBattle(Action<int> callback)
	{
		MsgStartBattle msgStartBattle = new MsgStartBattle();
		NetManager.Instance.SendMessage(msgStartBattle);
		NetManager.Instance.AddProtoListener(ProtocolEnum.MsgStartBattle, (resmsg) =>
		{
			MsgStartBattle msg = (MsgStartBattle)resmsg;
			Debug.Log(msg.order);
			callback(msg.order);
		});
	}
	//��������Э��
	public static void UpdateChessBroad(string s,Action<string,UpdateResult> callback)
	{
		MsgUpdateChessBoard msgUpdateChessBroad = new MsgUpdateChessBoard();
		msgUpdateChessBroad.trans = s;
		NetManager.Instance.SendMessage(msgUpdateChessBroad);
		NetManager.Instance.AddProtoListener(ProtocolEnum.MsgUpdateChessBoard, (resmsg) =>
		{
			MsgUpdateChessBoard msg = (MsgUpdateChessBoard)resmsg;
			callback(msg.trans,msg.UpdateResult);
		});
	}
}
