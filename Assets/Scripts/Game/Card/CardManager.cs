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
		if (GameController.Instance.gameMode == GameMode.Stand_Alone|| GameController.Instance.gameMode == GameMode.NetWorking)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				isAddCardToHand = true; 
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
		GenerateCard(go);
	}
	//ͨ��Id����һ����
	public void GenerateCardWithId(int id)
	{
		GameObject go = cardModel_SO.cardList[id].cardPrefabs;
		GenerateCard(go);
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
			if (go != null)
			{
				GenerateCard(go);
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
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			HandCardLayout.Instance.RemoveAllCard();
			for (int i = 0; i < handCardPlacementTran.childCount; i++)
			{
				Destroy(handCardPlacementTran.GetChild(i).gameObject);
			}
		}
		else
		{
			HandCardLayout.Instance.RemoveAllCard();
			Transform trans = handCardPlacementTran.Find("Player1");
			for (int i = 0; i < trans.childCount; i++)
			{
				Destroy(handCardPlacementTran.GetChild(i).gameObject);
			}
		    trans = handCardPlacementTran.Find("Player2");
			for (int i = 0; i < trans.childCount; i++)
			{
				Destroy(handCardPlacementTran.GetChild(i).gameObject);
			}
		}
	}
	private void GenerateCard(GameObject go)
	{
		if (go != null)
		{
			if (GameController.Instance.gameMode != GameMode.Stand_Alone)
			{
				GameObject card = Instantiate(go, cardBoxTran.position, Quaternion.identity, handCardPlacementTran);
				//ToDo:���ӿ��Ʒ�ת�Ĺ��ɶ���
				HandCardLayout.Instance.AddCard(card.transform);
			}
			else
			{
				Transform trans;
				if (ChessBoardController.Instance.GetPlayer() == Player.One)
				{
					trans = handCardPlacementTran.Find("Player1");
				}
				else
				{
					trans = handCardPlacementTran.Find("Player2");
				}
				GameObject card = Instantiate(go, cardBoxTran.position, Quaternion.identity, trans);
				//ToDo:���ӿ��Ʒ�ת�Ĺ��ɶ���
				HandCardLayout.Instance.AddCard(card.transform);
			}

		}
	}
}
