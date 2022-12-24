using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

//卡牌的状态，正面、背面
public enum CardState
{
    Front,
    Back
}

public class CardTurnOver : MonoBehaviour
{

    private GameObject cardFront;       //卡牌正面
    private GameObject cardBack;        //卡牌的背面
    public CardState cardState = CardState.Front;  //卡牌当前的状态，是正面还是背面
    public float mTime = 0.4f;
    public bool isActive = false;       //true代表正在执行翻转，不许被打断

    /// <summary>
    /// 初始化卡牌角度，根据mCardState
    /// </summary>
    public void Init()
    {
        if (cardState == CardState.Front)
        {
            cardFront.transform.eulerAngles = new Vector3(cardFront.transform.eulerAngles.x, 0, cardFront.transform.eulerAngles.z);
            cardBack.transform.eulerAngles = new Vector3(cardBack.transform.eulerAngles.x, 90, cardBack.transform.eulerAngles.z);
        }
        else
        {
            cardFront.transform.eulerAngles = new Vector3(cardFront.transform.eulerAngles.x, 90, cardFront.transform.eulerAngles.z);
            cardBack.transform.eulerAngles = new Vector3(cardBack.transform.eulerAngles.x, 0, cardBack.transform.eulerAngles.z);
        }
    }

	void Awake()
    {
        cardBack = transform.Find("CardBack").gameObject;
        cardFront = transform.GetChild(1).gameObject;
        cardFront.GetComponent<Button>().onClick.AddListener(() =>
        {
            Stand_AloneUIManager.Instance.CloseSelectCardPlane();
            string s = cardFront.GetComponent<Image>().sprite.name;
            CardManager.Instance.GenerateCardWithName(s);
        });
        Init();
    }
	
    public void ToTurnOver()
	{
        if (cardState == CardState.Front)
        {
            StartCoroutine(ToBack());
        }
        else
        {
            StartCoroutine(ToFront());
        }
    }
	//开始向后转
	public void StartBack()
    {
        if (isActive)
        {
            return;
        }
        StartCoroutine(ToBack());
    }

    //开始前转
    public void StartFront()
    {
        if (isActive)
        {
            return;
        }
        StartCoroutine(ToFront());
    }

    /// <summary>
    /// 翻转到背面
    /// </summary>
    /// <returns></returns>
    public IEnumerator ToBack()
    {
        isActive = true;
        cardFront.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
        {
            yield return 0;
        }
        cardBack.transform.DORotate(new Vector3(0, 0, 0), mTime);
        cardState = CardState.Back;
        isActive = false;
    }

    /// <summary>
    /// 翻转到正面
    /// </summary>
    /// <returns></returns>
    public IEnumerator ToFront()
    {
        isActive = true;
        cardBack.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
        {
            yield return 0;
        }
        cardFront.transform.DORotate(new Vector3(0, 0, 0), mTime);
        cardState = CardState.Front;
        isActive = false;
    }

}