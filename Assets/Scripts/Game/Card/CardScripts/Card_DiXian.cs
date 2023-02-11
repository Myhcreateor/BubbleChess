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
		DiXianCommand diXianCommand;
		ChessBoardController c = ChessBoardController.Instance;
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			diXianCommand = new DiXianCommand(ref c.chessPieceArrays, 1, clickTrans);
		}
		else
		{
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				diXianCommand = new DiXianCommand(ref c.chessPieceArrays, 1, clickTrans);
			}
			else
			{
				diXianCommand = new DiXianCommand(ref c.chessPieceArrays, 2, clickTrans);
			}
		    
		}
		diXianCommand.Execute();
		ProtocolManager.CardTrigger(CardName.DiXian, (chessPieceLinearArray, res) =>
		{
			for (int i = 0; i < chessPieceLinearArray.Length; i++)
			{
				c.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
			}
			EventHandler.CallUpdateChessBoardEvent();
		});
		return diXianCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
}
