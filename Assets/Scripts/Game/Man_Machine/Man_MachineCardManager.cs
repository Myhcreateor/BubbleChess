using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man_MachineCardManager : CardManager
{
	protected override void Awake()
	{
		base.Awake();
	}
	protected override void Update()
	{
		if (GameController.Instance.gameMode == GameMode.Man_Machine)
		{
			//人机对战生成卡牌
			if (isAddCardToHand)
			{
				GenerateRandomCard();
				isAddCardToHand = false;
			}
		}
	}
}
