using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutUsPanel : BasePanel
{
    private Button closeButton;
    void Start()
	{
		closeButton = transform.Find("CloseButton").GetComponent<Button>();
		closeButton.onClick.AddListener(() =>
		{
			UIManager.Instance.PopPanel();
		});
	}
}
