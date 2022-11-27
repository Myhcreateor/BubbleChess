using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card_YanXi : Card
{
	public YanXiCommand yanXiCommand;

	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.YanXi);
	}

	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		YanXiCommand yanXiCommand = new YanXiCommand(ref c.chessPieceArrays, 1);
		yanXiCommand.Execute();
		EventHandler.CallUpdateChessBoardEvent();
		return true;
	}

	public override void SetClickTrans(string s)
	{

	}
}