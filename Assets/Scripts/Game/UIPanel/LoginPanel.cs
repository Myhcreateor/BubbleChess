using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
	private Text loginUsername;
	private Text loginPassword;
	private Text tipText;
	private Button closeButton;
	private Button loginButton;
	private Button registerButton;

	public override void OnEnter()
	{
		base.OnEnter();
		loginUsername = transform.Find("UserNameInputField/Text").GetComponent<Text>();
		loginPassword = transform.Find("PasswordInputField/Text").GetComponent<Text>();
		tipText = transform.Find("TipText").GetComponent<Text>();
		closeButton = transform.Find("CloseButton").GetComponent<Button>();
		loginButton = transform.Find("LoginButton").GetComponent<Button>();
		registerButton = transform.Find("RegisterButton").GetComponent<Button>();
		closeButton.onClick.AddListener(ClosePanel);
		loginButton.onClick.AddListener(LoginUI);
		registerButton.onClick.AddListener(OpenRegisterUI);
	}

	void ClosePanel()
	{
		UIManager.Instance.PopPanel();
	}
	void LoginUI()
	{
		ProtocolManager.Login(LoginType.Phone, loginUsername.text, loginPassword.text, (res, restoken) =>
		{
			if (res == LoginResult.Success)
			{
				tipText.text = "登录成功!";
			}
			else if (res == LoginResult.Failed)
			{
				tipText.text = "登录失败";
			}
			else if (res == LoginResult.WrongPwd)
			{
				tipText.text = "密码错误！";
			}
			else if (res == LoginResult.UserNotExist)
			{
				tipText.text = "用户名不存在！";
			}
		});
	}
	void OpenRegisterUI()
	{
		UIManager.Instance.PushPanel(UIPanelType.Register);
	}
}
