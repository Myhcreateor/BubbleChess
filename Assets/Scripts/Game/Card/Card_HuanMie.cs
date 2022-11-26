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
		HuanMieCommand huanMieCommand = new HuanMieCommand(ref c.chessPieceArrays, 1, clickTrans);
		huanMieCommand.Execute();
		return huanMieCommand.isSuccessRelease;
	}
	public override void  SetClickTrans(string s)
	{
		clickTrans = s;
	}


	// Update is called once per frame
	void Update()
    {
        
    }
}
