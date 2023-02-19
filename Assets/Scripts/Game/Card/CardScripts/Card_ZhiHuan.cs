using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_ZhiHuan : BaseCard
{
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.ZhiHuan);
	}
	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		ZhiHuanCommand zhiHuanCommand = new ZhiHuanCommand(ref c.chessPieceArrays, GameController.Instance.GetPlayer(), clickTrans);
		if (!CheckIsSufficientCost())
		{
			return false;
		}
		zhiHuanCommand.Execute();
		if (zhiHuanCommand.isSuccessRelease)
		{
			EventHandler.CallGenerateParticleEffectEvent(clickTrans, cardDetails.particleEffectPath);
			ChessBoardController.Instance.UpdateCrytralNum(-cardDetails.costNum);
			if (GameController.Instance.gameMode == GameMode.NetWorking)
			{
				ProtocolManager.CardTrigger(cardDetails.particleEffectPath, clickTrans, (path, trans, chessPieceLinearArray, res) =>
				{
					for (int i = 0; i < chessPieceLinearArray.Length; i++)
					{
						//ChessBoardController.Instance.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
						c.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
					}
					Invoke("CallUpdateChessBoardEvent", 0.5f);
					EventHandler.CallGenerateParticleEffectEvent(trans, path);
				});
			}
			Invoke("CallUpdateChessBoardEvent", 0.5f);
		}
		return zhiHuanCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		base.SetClickTrans(s);
	}
	public void CallUpdateChessBoardEvent()
	{
		EventHandler.CallUpdateChessBoardEvent();
	}
}
