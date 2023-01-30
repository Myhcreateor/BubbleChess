using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private Button aboutUsButton;
	private Button loginButton;
    void Start()
	{
		aboutUsButton = transform.Find("AboutUsButton").GetComponent<Button>();
		loginButton = transform.Find("LoginButton").GetComponent<Button>();
		aboutUsButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PushPanel(UIPanelType.AboutUs);
		});
		loginButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PushPanel(UIPanelType.Login);
		});
	}
}
