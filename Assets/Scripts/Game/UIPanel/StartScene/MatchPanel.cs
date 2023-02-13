using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
			UIManager.Instance.PopPanel();
			AudioController.Instance.PlayAudio(0);
			//TODO向服务器发送取消匹配请求
			CancelInvoke("MatchOpponent");
			ProtocolManager.Match("111", false, (res, resText,resId) =>
			{
				if (res == MatchResult.Cancel)
				{
					isSuccessMatch = false;
					isMatch = false;
				}
				else
				{
					Debug.LogError("未能成功取消匹配请求");
				}
			});
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
			ProtocolManager.Match("111",true, (res, resText,resId) =>
			{
				if (res == MatchResult.Success)
				{
					waitingTimeText.text = "Your Opponent"+resText;
					isSuccessMatch = true;
					isMatch = false;
					Invoke("LoadNetworkScene",2f);
				}
				else
				{
					//Debug.Log(res);
				}
			});
		}
	}
	void LoadNetworkScene()
	{
		SceneManager.LoadScene(1);
	}
}
