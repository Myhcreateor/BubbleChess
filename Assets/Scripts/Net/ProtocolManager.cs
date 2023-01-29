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
			Debug.Log("获取密钥：" + ((MsgSecret)resmsg).secret);
		});
	}
	//注册协议的提交
	public static void Register(RegisterType registerType ,string userName,string password ,string code, Action<RegisterResult> callback)
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
	//登录协议的提交
	public static void Login(LoginType loginType,string userName,string password, Action<LoginResult,string> callback)
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
}
