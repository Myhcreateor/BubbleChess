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
		QuLiCommand QuLiCommand = new QuLiCommand(ref c.chessPieceArrays, GameController.Instance.GetPlayer(), clickTrans);
		QuLiCommand.Execute();
		if (QuLiCommand.isSuccessRelease)
		{
			EventHandler.CallGenerateParticleEffectEvent(clickTrans, cardDetails.particleEffect);
			ProtocolManager.CardTrigger(CardName.QuLi, (chessPieceLinearArray, res) =>
			{
				for (int i = 0; i < chessPieceLinearArray.Length; i++)
				{
					c.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
				}
				EventHandler.CallUpdateChessBoardEvent();
			});
			EventHandler.CallUpdateChessBoardEvent();
		}
		return QuLiCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
}
