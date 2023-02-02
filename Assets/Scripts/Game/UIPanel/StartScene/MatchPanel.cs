using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanel : BasePanel
{
	private Button cancelButton;
	private Text waitingTimeText;
	private float timer=0;
	private bool isSuccessMatch = false;
	private bool isMatch = false;
	private void Start()
	{
		cancelButton = transform.Find("CancelButton").GetComponent<Button>();
		waitingTimeText = transform.Find("WaitingTimeText").GetComponent<Text>();
		cancelButton.onClick.AddListener(() =>
		{
			isMatch = false;
			UIManager.Instance.PopPanel();
			//TODO向服务器发送取消匹配请求
			CancelInvoke("MatchOpponent");
		});
	}
	public override void OnEnter()
	{
		base.OnEnter();
		timer = 0;
		isMatch = true;
		isSuccessMatch = false;
		//向服务器发送匹配请求
		InvokeRepeating("MatchOpponent",0,0.3f);
	}
	private void Update()
	{
		if (isMatch)
		{
			timer += Time.deltaTime;
			waitingTimeText.text = ((int)timer).ToString() + "s";
		}

	}
	void MatchOpponent()
	{
		if (isMatch&&!isSuccessMatch)
		{
			ProtocolManager.Match("111", (res, resText) =>
			{
				if (res == MatchResult.Success)
				{
					waitingTimeText.text = "Your Opponent"+resText;
					isSuccessMatch = true;
					isMatch = false;
				}
				else
				{
					Debug.Log(res);
				}
				Debug.Log(resText);
			});
		}
	}
}
