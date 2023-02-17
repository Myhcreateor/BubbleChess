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
		XingYiCommand xingYiCommand = new XingYiCommand(ref c.chessPieceArrays, GameController.Instance.GetPlayer(), clickTrans);
		if (cardDetails.costNum > ChessBoardController.Instance.crystalManager.CrystalNum)
		{
			return false;
		}
		xingYiCommand.Execute();
		if (xingYiCommand.isSuccessRelease)
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
			EventHandler.CallUpdateChessBoardEvent();
		}
		
		return xingYiCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
}

