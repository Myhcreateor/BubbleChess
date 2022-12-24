using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

//���Ƶ�״̬�����桢����
public enum CardState
{
    Front,
    Back
}

public class CardTurnOver : MonoBehaviour
{

    private GameObject cardFront;       //��������
    private GameObject cardBack;        //���Ƶı���
    public CardState cardState = CardState.Front;  //���Ƶ�ǰ��״̬�������滹�Ǳ���
    public float mTime = 0.4f;
    public bool isActive = false;       //true��������ִ�з�ת���������

    /// <summary>
    /// ��ʼ�����ƽǶȣ�����mCardState
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
	//��ʼ���ת
	public void StartBack()
    {
        if (isActive)
        {
            return;
        }
        StartCoroutine(ToBack());
    }

    //��ʼǰת
    public void StartFront()
    {
        if (isActive)
        {
            return;
        }
        StartCoroutine(ToFront());
    }

    /// <summary>
    /// ��ת������
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
    /// ��ת������
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