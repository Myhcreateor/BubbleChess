using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryPanel : BasePanel
{
	private ScrollRect scrollRect;
	private SlideScrollView slideScrollView;
	private Button lastPageButton;
	private Button nextPageButton;
	private Button closeButton;


	private void Start()
	{
		scrollRect = transform.Find("Scroll View").GetComponent<ScrollRect>();
		lastPageButton = transform.Find("LastPageButton").GetComponent<Button>();
		nextPageButton = transform.Find("NextPageButton").GetComponent<Button>();
		closeButton = transform.Find("CloseButton").GetComponent<Button>();
		slideScrollView = scrollRect.gameObject.GetComponent<SlideScrollView>();
		lastPageButton.onClick.AddListener(slideScrollView.ToLastPage);
		nextPageButton.onClick.AddListener(slideScrollView.ToNextPage);
		closeButton.onClick.AddListener(() =>
		{
			AudioController.Instance.PlayAudio(0);
			UIManager.Instance.PopPanel();
		});
	}


}
