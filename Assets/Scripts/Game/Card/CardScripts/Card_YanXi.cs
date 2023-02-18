using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card_YanXi : BaseCard
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
		if (cardDetails.costNum > ChessBoardController.Instance.crystalManager.CrystalNum)
		{
			return false;
		}
		if (GameController.Instance.gameMode == GameMode.NetWorking && !ChessBoardController.Instance.IsPlayeChess)
		{
			return false;
		}
		yanXiCommand.Execute();
		ChessBoardController.Instance.UpdateCrytralNum(-cardDetails.costNum);
		string clickTrans = "";
		if (GameController.Instance.gameMode == GameMode.NetWorking)
		{
			ProtocolManager.CardTrigger(cardDetails.particleEffectPath, clickTrans, (path, trans, chessPieceLinearArray, res) =>
			{
				for (int i = 0; i < chessPieceLinearArray.Length; i++)
				{
					c.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
				}
				EventHandler.CallUpdateChessBoardEvent();
			});
		}
		EventHandler.CallUpdateChessBoardEvent();
		return true;
	}

	public override void SetClickTrans(string s)
	{

	}
}