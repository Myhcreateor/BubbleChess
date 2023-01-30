using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
	private Text registerUsername;
	private Text registerPassword;
	private Text registerRePassword;
	private Text tipText;
	private Button registerButton;
	private Button closeButton;
	public override void OnEnter()
	{
		base.OnEnter();
		registerUsername = transform.Find("UserNameInputField/Text").GetComponent<Text>();
		registerPassword = transform.Find("PasswordInputField/Text").GetComponent<Text>();
		registerRePassword = transform.Find("RePasswordInputField/Text").GetComponent<Text>();
		tipText = transform.Find("TipText").GetComponent<Text>();
		registerButton = transform.Find("RegisterButton").GetComponent<Button>();
		closeButton = transform.Find("CloseButton").GetComponent<Button>();
		registerButton.onClick.AddListener(RegisterUI);
		closeButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PopPanel();
		});
	}
	public void RegisterUI()
	{
		if (registerPassword.text.Equals(registerRePassword.text))
		{
			ProtocolManager.Register(RegisterType.Phone, registerUsername.text, registerPassword.text, "123456", (res) =>
			{
				if (res == RegisterResult.AlreadlyExist)
				{
					tipText.text = "该手机已经注册过了";
				}
				else if (res == RegisterResult.WrongCode)
				{
					tipText.text = "验证码错误";
				}
				else if (res == RegisterResult.Forbidden)
				{
					tipText.text = "该用户被封禁!";
				}
				else if (res == RegisterResult.Success)
				{
					tipText.text = "注册成功!";
				}
				else
				{
					tipText.text = "注册失败!";
				}
			});
		}
		else
		{
			tipText.text = "密码不一致!";
		}

	}
}
