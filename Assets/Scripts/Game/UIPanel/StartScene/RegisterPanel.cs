using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
	private InputField registerUsername;
	private InputField registerPassword;
	private InputField registerRePassword;
	private Text tipText;
	private Button registerButton;
	private Button closeButton;
	public void Start()
	{
		registerUsername = transform.Find("UserNameInputField").GetComponent<InputField>();
		registerPassword = transform.Find("PasswordInputField").GetComponent<InputField>();
		registerRePassword = transform.Find("RePasswordInputField").GetComponent<InputField>();
		tipText = transform.Find("TipText").GetComponent<Text>();
		registerButton = transform.Find("RegisterButton").GetComponent<Button>();
		closeButton = transform.Find("CloseButton").GetComponent<Button>();
		registerButton.onClick.AddListener(RegisterUI);
		closeButton.onClick.AddListener(() =>
		{
			AudioController.Instance.PlayAudio(0);
			UIManager.Instance.PopPanel();
		});
	}
	public override void OnEnter()
	{
		base.OnEnter();
	}

	public void RegisterUI()
	{
		AudioController.Instance.PlayAudio(0);
		if (registerPassword.text.Equals(registerRePassword.text))
		{
			ProtocolManager.Register(RegisterType.Phone, registerUsername.text, registerPassword.text, "123456", (res) =>
			{
				if (res == RegisterResult.AlreadlyExist)
				{
					tipText.text = "���ֻ��Ѿ�ע�����";
				}
				else if (res == RegisterResult.WrongCode)
				{
					tipText.text = "��֤�����";
				}
				else if (res == RegisterResult.Forbidden)
				{
					tipText.text = "���û������!";
				}
				else if (res == RegisterResult.Success)
				{
					tipText.text = "ע��ɹ�!";
				}
				else
				{
					tipText.text = "ע��ʧ��!";
				}
			});
		}
		else
		{
			tipText.text = "���벻һ��!";
		}

	}
}
