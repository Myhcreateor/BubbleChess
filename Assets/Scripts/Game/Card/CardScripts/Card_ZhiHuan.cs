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
		ZhiHuanCommand zhiHuanCommand = new ZhiHuanCommand(ref c.chessPieceArrays, GameController.Instance.GetPlayer(), clickTrans);
		zhiHuanCommand.Execute();
		if (zhiHuanCommand.isSuccessRelease)
		{
			EventHandler.CallGenerateParticleEffectEvent(clickTrans, cardDetails.particleEffect);
			ProtocolManager.CardTrigger(CardName.ZhiHuan, (chessPieceLinearArray, res) =>
			{
				for (int i = 0; i < chessPieceLinearArray.Length; i++)
				{
					//ChessBoardController.Instance.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
					c.chessPieceArrays[i / 8][i % 8] = chessPieceLinearArray[i];
				}
				Invoke("CallUpdateChessBoardEvent", 0.5f);
			});
			Invoke("CallUpdateChessBoardEvent", 0.5f);
		}
		return zhiHuanCommand.isSuccessRelease;
	}
	public override void SetClickTrans(string s)
	{
		clickTrans = s;
	}
	public void CallUpdateChessBoardEvent()
	{
		EventHandler.CallUpdateChessBoardEvent();
	}
}
