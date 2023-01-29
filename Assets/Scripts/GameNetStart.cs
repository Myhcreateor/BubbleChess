using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNetStart : MonoBehaviour
{
    public Text LoginUsername;
    public Text LoginPassword;
    public Text registerUsername;
    public Text registerPassword;
    public Text registerRePassword;
    void Start()
    {
        NetManager.Instance.Connect("127.0.0.1", 10005);
        StartCoroutine(NetManager.Instance.CheckNet());
    }
    public void LoginUI()
	{
        ProtocolManager.Login(LoginType.Phone, LoginUsername.text, LoginPassword.text, (res, restoken) =>
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
    public void RegisterUI()
	{
		if (registerPassword.text.Equals(registerRePassword.text))
		{
			ProtocolManager.Register(RegisterType.Phone, registerUsername.text, registerPassword.text, "123456", (res) =>
			{
				if (res == RegisterResult.AlreadlyExist)
				{
					Debug.LogError("���ֻ��Ѿ�ע�����");
				}
				else if (res == RegisterResult.WrongCode)
				{
					Debug.LogError("��֤�����");
				}
				else if (res == RegisterResult.Forbidden)
				{
					Debug.LogError("���û��������");
				}
				else if (res == RegisterResult.Success)
				{
					Debug.Log("ע��ɹ���");
				}
				else
				{
					Debug.LogError("ע��ʧ�ܣ�");
				}
			});
		}
        else
		{
            Debug.LogError("���벻һ�£�");
        }
		
	}
    // Update is called once per frame
    void Update()
    {
        NetManager.Instance.Update();
    }
    private void OnApplicationQuit()
    {
        NetManager.Instance.Close();
    }
}
