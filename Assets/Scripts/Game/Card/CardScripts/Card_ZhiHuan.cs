using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_ZhiHuan : Card
{
	private string clickTrans;
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.ZhiHuan);
	}
	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		ZhiHuanCommand zhiHuanCommand = new ZhiHuanCommand(ref c.chessPieceArrays, 1, clickTrans);
		zhiHuanCommand.Execute();
		EventHandler.CallUpdateChessBoardEvent();
		return zhiHuanCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
}
