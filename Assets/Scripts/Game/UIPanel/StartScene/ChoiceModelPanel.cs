using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoiceModelPanel : BasePanel
{
	private ScrollRect scrollRect;
	private SlideScrollView slideScrollView;
	private Button lastPageButton;
	private Button nextPageButton;
	private Button confirmButton;
	private Button closeButton;
	private Text choiceModelText;

	private void Start()
	{
		scrollRect = transform.Find("Scroll View").GetComponent<ScrollRect>();
		lastPageButton = transform.Find("LastPageButton").GetComponent<Button>();
		nextPageButton = transform.Find("NextPageButton").GetComponent<Button>();
		confirmButton = transform.Find("ConfirmButton").GetComponent<Button>();
		closeButton = transform.Find("CloseButton").GetComponent<Button>();
		choiceModelText = transform.Find("ChoiceModelText").GetComponent<Text>();
		slideScrollView = scrollRect.gameObject.GetComponent<SlideScrollView>();
		lastPageButton.onClick.AddListener(slideScrollView.ToLastPage);
		nextPageButton.onClick.AddListener(slideScrollView.ToNextPage);
		confirmButton.onClick.AddListener(() =>
		{
			if(slideScrollView.currentIndex==2|| slideScrollView.currentIndex == 3)
			{
				AudioController.Instance.PlayAudio(0);
				SceneManager.LoadScene(slideScrollView.currentIndex);
			}
			else
			{
				AudioController.Instance.PlayAudio(0);
				UIManager.Instance.PushPanel(UIPanelType.Match);
			}															
		});
		closeButton.onClick.AddListener(() => 
		{
			AudioController.Instance.PlayAudio(0);
			UIManager.Instance.PopPanel();
		});
	}
	public void UpdatePanelText()
	{
		int index = slideScrollView.currentIndex;
		if (index == 1)
		{
			choiceModelText.text = "Networking";
		}
		else if (index == 2)
		{
			choiceModelText.text = "Stand-Alone";
		}
		else
		{
			choiceModelText.text = "Man-Machine";
		}
	}


}
