using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardPanel : BasePanel
{

	public override void OnEnter()
	{
		base.OnEnter();
		CardTurnOver[] turnOvers = transform.Find("Card_TurnOver").GetComponentsInChildren<CardTurnOver>();
		foreach (var i in turnOvers)
		{
			i.ToTurnOver();
		}
	}
}
