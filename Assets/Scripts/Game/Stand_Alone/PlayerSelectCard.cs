using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectCard : MonoBehaviour
{
	public Image[] mCard_Image;
	public CardModel cardModel;
    private List<CardDetails> randomSequenceList = new List<CardDetails>();
    public Text titleText;
    private int cardListCount;
	private void Awake()
	{
        RefleshSelectCard();
    }
    public void RefleshSelectCard()
	{
        cardListCount = cardModel.cardList.Count;
        randomSequenceList = GetRandomSequence(cardModel.cardList, cardListCount);
        //���ѡ�������ƣ����ѡ��һ�ż�������
        for (int i = 0; i < 3; i++)
        {
            mCard_Image[i].sprite = randomSequenceList[i].cardSprite;
        }
    }
    public void ChangeTitleText()
	{
        titleText.text = "Player2ѡ����";

    }
    public static List<CardDetails> GetRandomSequence(List<CardDetails> cardList, int count)
    {
        List<CardDetails> output = new List<CardDetails>();
        int num;
        for (int i = 0; i < count; i++)
        {
            num = Random.Range(0, cardList.Count);
            if (output.Contains(cardList[num]))
            {
                i--;
                continue;
            }
            output.Add(cardList[num]);
        }
        return output;
    }
}
