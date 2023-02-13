using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
	private InputField loginUsername;
	private InputField loginPassword;
	private Text tipText;
	//private Button closeButton;
	private Button loginButton;
	private Button registerButton;
	void Start()
	{
		loginUsername = transform.Find("UserNameInputField").GetComponent<InputField>();
		loginPassword = transform.Find("PasswordInputField").GetComponent<InputField>();
		tipText = transform.Find("TipText").GetComponent<Text>();
		//closeButton = transform.Find("CloseButton").GetComponent<Button>();
		loginButton = transform.Find("LoginButton").GetComponent<Button>();
		registerButton = transform.Find("RegisterButton").GetComponent<Button>();
		//closeButton.onClick.AddListener(ClosePanel);
		loginButton.onClick.AddListener(LoginUI);
		registerButton.onClick.AddListener(OpenRegisterUI);
	}
	public override void OnEnter()
	{
		base.OnEnter();
	}

	void ClosePanel()
	{
		UIManager.Instance.PopPanel();
	}

	void LoginUI()
	{
		AudioController.Instance.PlayAudio(0);
		ProtocolManager.Login(LoginType.Phone, loginUsername.text, loginPassword.text, (res, restoken) =>
		{
			if (res == LoginResult.Success)
			{
				tipText.text = "��¼�ɹ�!";
				//TODOѡ��ģʽ
				UIManager.Instance.PopPanel();
				UIManager.Instance.PushPanel(UIPanelType.ChoiceModel);
			}
			else if (res == LoginResult.Failed)
			{
				tipText.text = "��¼ʧ��";
			}
			else if (res == LoginResult.WrongPwd)
			{
				tipText.text = "�������";
			}
			else if (res == LoginResult.UserNotExist)
			{
				tipText.text = "�û��������ڣ�";
			}
		});
		AudioController.Instance.PlayAudio(0);
	}
	void OpenRegisterUI()
	{
		AudioController.Instance.PlayAudio(0);
		UIManager.Instance.PushPanel(UIPanelType.Register);
	}
}
