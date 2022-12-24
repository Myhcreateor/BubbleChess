using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectCard : MonoBehaviour
{
	public Image[] mCard_Image;
	public CardModel cardModel;
    private List<CardDetails> randomSequenceList = new List<CardDetails>();
    private int cardListCount;
	private void Awake()
	{
		cardListCount = cardModel.cardList.Count;
        randomSequenceList =GetRandomSequence(cardModel.cardList, cardListCount);
        //随机选出三张牌，玩家选择一张加入手牌
        for(int i = 0; i < 3; i++)
		{
            mCard_Image[i].sprite = randomSequenceList[i].cardSprite;
        }
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
