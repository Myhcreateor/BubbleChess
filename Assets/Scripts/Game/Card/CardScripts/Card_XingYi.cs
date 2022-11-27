using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_XingYi : Card
{
	private string clickTrans;
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.XingYi);
	}
	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		XingYiCommand xingYiCommand = new XingYiCommand(ref c.chessPieceArrays, 1, clickTrans);
		xingYiCommand.Execute();
		return xingYiCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
}

