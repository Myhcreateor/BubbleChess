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
		YanXiCommand yanXiCommand = new YanXiCommand(ref c.chessPieceArrays, GameController.Instance.GetPlayer());
		yanXiCommand.Execute();
		ProtocolManager.CardTrigger(CardName.YanXi, (chessPieceLinearArray, res) =>
		{
			for (int i = 0; i < chessPieceLinearArray.Length; i++)
			{
				c.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
			}
			EventHandler.CallUpdateChessBoardEvent();
		});
		EventHandler.CallUpdateChessBoardEvent();
		return true;
	}

	public override void SetClickTrans(string s)
	{

	}
}