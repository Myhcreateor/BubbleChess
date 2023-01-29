using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNetStart : MonoBehaviour
{
    public Text username;
    public Text password;
    void Start()
    {
        NetManager.Instance.Connect("127.0.0.1", 10005);
        StartCoroutine(NetManager.Instance.CheckNet());
    }
    public void RegisterUI()
	{
        ProtocolManager.Login(LoginType.Phone, username.text, password.text, (res, restoken) =>
        {
            if (res == LoginResult.Success)
            {
                Debug.Log("��¼�ɹ���");
            }
            else if (res == LoginResult.Failed)
            {
                Debug.LogError("��¼ʧ��");
            }
            else if (res == LoginResult.WrongPwd)
            {
                Debug.LogError("�������");
            }
            else if (res == LoginResult.UserNotExist)
            {
                Debug.LogError("�û��������ڣ�");
            }

        });
        
    }
    // Update is called once per frame
    void Update()
    {
        NetManager.Instance.Update();
        //ProtocolManager.Register(RegisterType.Phone, username.text, password.text, "123456", (res) =>
        //{
        //    if (res == RegisterResult.AlreadlyExist)
        //    {
        //        Debug.LogError("���ֻ��Ѿ�ע�����");
        //    }
        //    else if (res == RegisterResult.WrongCode)
        //    {
        //        Debug.LogError("��֤�����");
        //    }
        //    else if (res == RegisterResult.Forbidden)
        //    {
        //        Debug.LogError("���û��������");
        //    }
        //    else if (res == RegisterResult.Success)
        //    {
        //        Debug.Log("ע��ɹ���");
        //    }
        //    else
        //    {
        //        Debug.LogError("ע��ʧ�ܣ�");
        //    }
        //});
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ProtocolManager.Login(LoginType.Phone, "15797271915", "myh997762", (res, restoken) =>
        //    {
        //        if (res == LoginResult.Success)
        //        {
        //            Debug.Log("��¼�ɹ���");
        //        }
        //        else if (res == LoginResult.Failed)
        //        {
        //            Debug.LogError("��¼ʧ��");
        //        }
        //        else if (res == LoginResult.WrongPwd)
        //        {
        //            Debug.LogError("�������");
        //        }
        //        else if (res == LoginResult.UserNotExist)
        //        {
        //            Debug.LogError("�û��������ڣ�");
        //        }

        //    });
        //}
    }
    private void OnApplicationQuit()
    {
        NetManager.Instance.Close();
    }
}
