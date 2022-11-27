using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_DiXian : Card
{
	private string clickTrans;
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.DiXian);
	}
	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		DiXianCommand diXianCommand = new DiXianCommand(ref c.chessPieceArrays, 1, clickTrans);
		diXianCommand.Execute();
		return diXianCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
}
