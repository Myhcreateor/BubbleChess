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
                Debug.Log("登录成功！");
            }
            else if (res == LoginResult.Failed)
            {
                Debug.LogError("登录失败");
            }
            else if (res == LoginResult.WrongPwd)
            {
                Debug.LogError("密码错误！");
            }
            else if (res == LoginResult.UserNotExist)
            {
                Debug.LogError("用户名不存在！");
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
        //        Debug.LogError("该手机已经注册过了");
        //    }
        //    else if (res == RegisterResult.WrongCode)
        //    {
        //        Debug.LogError("验证码错误");
        //    }
        //    else if (res == RegisterResult.Forbidden)
        //    {
        //        Debug.LogError("该用户被封禁！");
        //    }
        //    else if (res == RegisterResult.Success)
        //    {
        //        Debug.Log("注册成功！");
        //    }
        //    else
        //    {
        //        Debug.LogError("注册失败！");
        //    }
        //});
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ProtocolManager.Login(LoginType.Phone, "15797271915", "myh997762", (res, restoken) =>
        //    {
        //        if (res == LoginResult.Success)
        //        {
        //            Debug.Log("登录成功！");
        //        }
        //        else if (res == LoginResult.Failed)
        //        {
        //            Debug.LogError("登录失败");
        //        }
        //        else if (res == LoginResult.WrongPwd)
        //        {
        //            Debug.LogError("密码错误！");
        //        }
        //        else if (res == LoginResult.UserNotExist)
        //        {
        //            Debug.LogError("用户名不存在！");
        //        }

        //    });
        //}
    }
    private void OnApplicationQuit()
    {
        NetManager.Instance.Close();
    }
}
