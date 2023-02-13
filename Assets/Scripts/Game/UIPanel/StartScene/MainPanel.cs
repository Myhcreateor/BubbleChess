using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private Button aboutUsButton;
	//private Button loginButton;
	private Button galleryButton;
	private Button choiceModelButton;
    void Start()
	{
		aboutUsButton = transform.Find("AboutUsButton").GetComponent<Button>();
		//loginButton = transform.Find("LoginButton").GetComponent<Button>();
		galleryButton= transform.Find("GalleryButton").GetComponent<Button>();
		choiceModelButton = transform.Find("ChoiceModelButton").GetComponent<Button>();
		aboutUsButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PushPanel(UIPanelType.AboutUs);
			AudioController.Instance.PlayAudio(0);
		});
		//loginButton.onClick.AddListener(() =>
		//{
		//	UIManager.Instance.PushPanel(UIPanelType.Login);
		//});
		galleryButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PushPanel(UIPanelType.Gallery);
			AudioController.Instance.PlayAudio(0);
		});
		choiceModelButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PushPanel(UIPanelType.ChoiceModel);
			AudioController.Instance.PlayAudio(0);
		});
	}
}
