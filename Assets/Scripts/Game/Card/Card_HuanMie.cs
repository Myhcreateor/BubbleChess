using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_HuanMie : Card
{
	private void Start()
	{
		cardDetails = CardManager.Instance.cardModel_SO.GetCardDetailsWithName(CardName.HuanMie);
	}
	public override void ExecuteCommand()
	{
		ChessBoardController c = ChessBoardController.Instance;
		HuanMieCommand huanMieCommand = new HuanMieCommand(ref c.chessPieceArrays,1);
		huanMieCommand.Execute();
	}


    // Update is called once per frame
    void Update()
    {
        
    }
}
