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
    public CardName cardName;//卡牌的名字
    public int id;//卡牌的Id
    public GameObject cardPrefabs;//卡牌的预制体
    public Sprite cardSprite;//卡牌的图片
    public int costNum;//消耗魔晶数
    public int roundUpNum;//多少回合魔晶消耗上升
    public string description;//卡牌的描述
}
public enum CardName
{
    None, GuXuan, FengYin, HuanMie, XingYi, LianYin
}
