using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
	public CardModel cardModel_SO;
	public Transform handCardPlacementTran;
	public Transform cardBoxTran;
	public bool isAddCardToHand;
	protected virtual void Awake()
	{
		base.Awake();
	}
	protected virtual void Update()
	{
		if (GameController.Instance.gameMode == GameMode.Stand_Alone)
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
			GameObject card=  Instantiate(go, cardBoxTran.position, Quaternion.identity, handCardPlacementTran);
			//ToDo:���ӿ��Ʒ�ת�Ĺ��ɶ���
			HandCardLayout.Instance.AddCard(card.transform);
		}
	}
	//ͨ��Id����һ����
	public void GenerateCardWithId(int id)
	{
		GameObject go = cardModel_SO.cardList[id].cardPrefabs;
		if (go != null)
		{
			GameObject card = Instantiate(go, cardBoxTran.position, Quaternion.identity, handCardPlacementTran);
			HandCardLayout.Instance.AddCard(card.transform);
		}
	}
	//ͨ��name����һ����
	public void GenerateCardWithName(string name)
	{
		int id = -1;
		foreach (var item in cardModel_SO.cardList)
		{
			if (item.cardName.ToString().Equals(name))
			{
				id = item.id;
			}
		}
		if (id != -1)
		{
			GameObject go = cardModel_SO.cardList[id].cardPrefabs;
			Debug.Log(id);
			if (go != null)
			{
				GameObject card = Instantiate(go, cardBoxTran.position, Quaternion.identity, handCardPlacementTran);
				HandCardLayout.Instance.AddCard(card.transform);
			}
		}
		else
		{
			Debug.LogWarning("name maybe Error");
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
