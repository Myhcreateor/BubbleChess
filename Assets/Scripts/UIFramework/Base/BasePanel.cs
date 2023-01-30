using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour {

    protected CanvasGroup canvasGroup;
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        this.gameObject.SetActive(true);
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
		if (canvasGroup == null)
		{
            Debug.Log(this.gameObject + "没有canvasGroup组件");
		}
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关闭
    /// </summary>
    public virtual void OnExit()
    {
        this.gameObject.SetActive(false);
    }
}
