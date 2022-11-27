using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card_GuXuan : Card
{
    public GuXuanCommand guXuanCommand;

	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.GuXuan);
	}

	public override bool ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		GuXuanCommand guXuanCommand = new GuXuanCommand(ref c.chessPieceArrays, 1);
		guXuanCommand.Execute();
		for (int i = 0; i < guXuanCommand.str.Length; i++)
		{
			if (c.chessPieceArrays[int.Parse(guXuanCommand.str[i].Split(',')[0])][int.Parse(guXuanCommand.str[i].Split(',')[1])] == 0)
			{
				GameObject go = Instantiate(c.piecesList[0], c.GetChessBoardGridTransform().Find(guXuanCommand.str[i]).transform.position, Quaternion.identity, c.GetChessBoardPiecesTransform());
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
		return true;
	}

	public override void SetClickTrans(string s)
	{
		
	}
}
