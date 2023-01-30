using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNetStart : MonoBehaviour
{
    void Start()
    {
        NetManager.Instance.Connect("127.0.0.1", 10005);
        StartCoroutine(NetManager.Instance.CheckNet());
        UIManager.Instance.PushPanel(UIPanelType.Login);
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
