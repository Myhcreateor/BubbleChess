using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_FenLie : Card
{
	private string clickTrans;
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.FenLie);
	}
	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		FenLieCommand fenLieCommand = new FenLieCommand(ref c.chessPieceArrays, GameController.Instance.GetPlayer(), clickTrans);
		fenLieCommand.Execute();
		if (fenLieCommand.isSuccessRelease)
		{
			EventHandler.CallGenerateParticleEffectEvent(clickTrans, cardDetails.particleEffect);
			ProtocolManager.CardTrigger(CardName.FenLie, (chessPieceLinearArray, res) =>
			{
				for (int i = 0; i < chessPieceLinearArray.Length; i++)
				{
					c.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
				}
				EventHandler.CallUpdateChessBoardEvent();
			});
			EventHandler.CallUpdateChessBoardEvent();
		}
		return fenLieCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
}

