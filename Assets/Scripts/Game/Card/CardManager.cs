using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
	public CardModel cardModel_SO;
	public Transform handPlacementTran;
	private void Awake()
	{
		base.Awake();
	}
	private void Update()
	{
		if (GameController.Instance.gameMode == GameMode.Test)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				//生成卡牌
				GenerateRandomCard();
			}
		}else if (GameController.Instance.gameMode == GameMode.Man_Machine)
		{
			//人机对战开局生成设定好的固定卡牌
		}
		
	}
	//随机在cardModel里生成一张牌
	public void GenerateRandomCard()
	{
		int randomNum = Random.Range(0, cardModel_SO.cardList.Count);
		GameObject go =  cardModel_SO.cardList[randomNum].cardPrefabs;
		if (go != null)
		{
			GameObject card=  Instantiate(go, handPlacementTran.position, Quaternion.identity, handPlacementTran);
			HandCardLayout.Instance.AddCard(card.transform);
		}
	}
}
