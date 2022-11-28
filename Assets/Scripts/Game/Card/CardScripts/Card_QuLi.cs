using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_QuLi : Card
{
	private string clickTrans;
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.QuLi);
	}
	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		QuLiCommand QuLiCommand = new QuLiCommand(ref c.chessPieceArrays, 1, clickTrans);
		QuLiCommand.Execute();
		EventHandler.CallUpdateChessBoardEvent();
		return QuLiCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
}
