using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        //NetManager.Instance.Connect("127.0.0.1", 10005);
        NetManager.Instance.Connect("123.60.160.76", 10005);
        StartCoroutine(NetManager.Instance.CheckNet());
        UIManager.Instance.PushPanel(UIPanelType.Main);
        UIManager.Instance.PushPanel(UIPanelType.Login);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
	{
		if (scene.buildIndex == 0)
		{
            UIManager.Instance.PushPanel(UIPanelType.Main);
            UIManager.Instance.PushPanel(UIPanelType.ChoiceModel);
        }
	}

	void Update()
    {
        NetManager.Instance.Update();
    }
    private void OnApplicationQuit()
    {
        NetManager.Instance.Close();
    }
}
