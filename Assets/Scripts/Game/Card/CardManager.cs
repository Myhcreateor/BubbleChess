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
				//���ɿ���
				GenerateRandomCard();
			}
		}else if (GameController.Instance.gameMode == GameMode.Man_Machine)
		{
			//�˻���ս���������趨�õĹ̶�����
		}
		
	}
	//�����cardModel������һ����
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
