using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_HuanMie : Card
{
	private string clickTrans;
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.HuanMie);
	}
	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		HuanMieCommand huanMieCommand = new HuanMieCommand(ref c.chessPieceArrays, GameController.Instance.GetOpponent(), clickTrans);
		if (cardDetails.costNum > ChessBoardController.Instance.crystalManager.CrystalNum)
		{
			return false;
		}
		huanMieCommand.Execute();
		if (huanMieCommand.isSuccessRelease)
		{
			EventHandler.CallGenerateParticleEffectEvent(clickTrans, cardDetails.particleEffectPath);
			ChessBoardController.Instance.UpdateCrytralNum(-cardDetails.costNum);
			if (GameController.Instance.gameMode == GameMode.NetWorking)
			{
				ProtocolManager.CardTrigger(cardDetails.particleEffectPath, clickTrans, (path, trans, chessPieceLinearArray, res) =>
				{
					for (int i = 0; i < chessPieceLinearArray.Length; i++)
					{
						c.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
					}
					EventHandler.CallUpdateChessBoardEvent();
					EventHandler.CallGenerateParticleEffectEvent(trans, path);
				});
			}
		}
		return huanMieCommand.isSuccessRelease;
	}
	public override void  SetClickTrans(string s)
	{
		clickTrans = s;
	}
}
