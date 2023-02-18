using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_QuLi : BaseCard
{
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.QuLi);
	}
	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		QuLiCommand QuLiCommand = new QuLiCommand(ref c.chessPieceArrays, GameController.Instance.GetPlayer(), clickTrans);
		if (cardDetails.costNum > ChessBoardController.Instance.crystalManager.CrystalNum)
		{
			return false;
		}
		if (GameController.Instance.gameMode == GameMode.NetWorking && !ChessBoardController.Instance.IsPlayeChess)
		{
			return false;
		}
		QuLiCommand.Execute();
		if (QuLiCommand.isSuccessRelease)
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
		return QuLiCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		base.SetClickTrans(s);
	}
}
