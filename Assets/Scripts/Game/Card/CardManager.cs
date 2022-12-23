using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
	public CardModel cardModel_SO;
	public Transform handCardPlacementTran;
	public bool isAddCardToHand;
	protected virtual void Awake()
	{
		base.Awake();
	}
	protected virtual void Update()
	{
		if (GameController.Instance.gameMode == GameMode.Test)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				isAddCardToHand = true; ;
			}
		}
		if (isAddCardToHand)
		{
			//���ɿ���
			GenerateRandomCard();
			isAddCardToHand = false;
		}
	}
	//�����cardModel������һ����
	public void GenerateRandomCard()
	{
		int randomNum = Random.Range(0, cardModel_SO.cardList.Count);
		GameObject go =  cardModel_SO.cardList[randomNum].cardPrefabs;
		if (go != null)
		{
			GameObject card=  Instantiate(go, handCardPlacementTran.position, Quaternion.identity, handCardPlacementTran);
			HandCardLayout.Instance.AddCard(card.transform);
		}
	}
	//�����cardModel������һ����
	public void GenerateCardWithId(int id)
	{
		GameObject go = cardModel_SO.cardList[id].cardPrefabs;
		if (go != null)
		{
			GameObject card = Instantiate(go, handCardPlacementTran.position, Quaternion.identity, handCardPlacementTran);
			HandCardLayout.Instance.AddCard(card.transform);
		}
	}
	//������������
	public void RemoveAllCard()
	{
		HandCardLayout.Instance.RemoveAllCard();
		for (int i = 0; i < handCardPlacementTran.childCount; i++)
		{
			Destroy(handCardPlacementTran.GetChild(i).gameObject); 
		}
	}
}
