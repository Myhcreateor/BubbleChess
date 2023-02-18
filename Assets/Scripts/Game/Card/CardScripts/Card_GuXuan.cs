using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card_GuXuan : BaseCard
{
    public GuXuanCommand guXuanCommand;

	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.GuXuan);
	}

	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		GuXuanCommand guXuanCommand = new GuXuanCommand(ref c.chessPieceArrays, GameController.Instance.GetPlayer());
		if (cardDetails.costNum > ChessBoardController.Instance.crystalManager.CrystalNum)
		{
			return false;
		}
		guXuanCommand.Execute();
		for (int i = 0; i < guXuanCommand.str.Length; i++)
		{
			if (c.chessPieceArrays[int.Parse(guXuanCommand.str[i].Split(',')[0])][int.Parse(guXuanCommand.str[i].Split(',')[1])] == 0)
			{
				GameObject go = Instantiate(c.piecesList[GameController.Instance.GetPlayer()-1], c.GetChessBoardGridTransform().Find(guXuanCommand.str[i]).transform.position, Quaternion.identity, c.GetChessBoardPiecesTransform());
				go.GetComponent<Image>().DOFade(0, 2f);
				Destroy(go, 2f);
			}
			EventHandler.CallUpdateChessBoardEvent();
			//if (c.chessPieceArrays[int.Parse(guXuanCommand.str[i].Split(',')[0])][int.Parse(guXuanCommand.str[i].Split(',')[1])] != 0)
			//{
			//	Instantiate(c.piecesList[0], c.GetChessBoardGridTransform().Find(guXuanCommand.str[i]).transform.position, Quaternion.identity,c.GetChessBoardPiecesTransform());
			//}
			//else
			//{
			//	GameObject go = Instantiate(c.piecesList[0], c.GetChessBoardGridTransform().Find(guXuanCommand.str[i]).transform.position, Quaternion.identity, c.GetChessBoardPiecesTransform());
			//	go.GetComponent<Image>().DOFade(0, 2f);
			//	Destroy(go, 2f);
			//}
		}
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
		ChessBoardController.Instance.UpdateCrytralNum(-cardDetails.costNum);
		return true;
	}

	public override void SetClickTrans(string s)
	{
		
	}
}
