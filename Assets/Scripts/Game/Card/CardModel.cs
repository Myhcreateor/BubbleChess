using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardModel_SO", menuName = "Model/CardModel_SO")]
public class CardModel : ScriptableObject
{
    public List<CardDetails> cardList;
    public CardDetails GetCardDetailsWithId(int id)
	{
        return cardList.Find(i => i.id == id);
	}
    public CardDetails GetCardDetailsWithName(CardName name)
    {
        return cardList.Find(i => i.cardName == name);
    }
}
[System.Serializable]
public class CardDetails
{
    public CardName cardName;//���Ƶ�����
    public int id;//���Ƶ�Id
    public GameObject cardPrefabs;//���Ƶ�Ԥ����
    public Sprite cardSprite;//���Ƶ�ͼƬ
    public int costNum;//����ħ����
    public int roundUpNum;//���ٻغ�ħ����������
    public string description;//���Ƶ�����
}
public enum CardName
{
    None, GuXuan, FengYin, HuanMie, XingYi, LianYin
}
