using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardModel_SO", menuName = "Model/CardModel_SO")]
public class CardModel : ScriptableObject
{
    public List<CardDetails> CardList;
    public CardDetails GetCardDetailsWithId(int id)
	{
        return CardList.Find(i => i.id == id);
	}
    public CardDetails GetCardDetailsWithName(CardName name)
    {
        return CardList.Find(i => i.cardName == name);
    }
}
[System.Serializable]
public class CardDetails
{
    public CardName cardName;//���Ƶ�����
    public int id;//���Ƶ�Id
    public Sprite cardDetails;//���Ƶ�ͼƬ
    public int costNum;//����ħ����
    public int roundUpNum;//���ٻغ�ħ����������
    public string description;//���Ƶ�����
}
public enum CardName
{
    None, GuXuan, FengYin, huanMie, XingYi, LianYin
}
