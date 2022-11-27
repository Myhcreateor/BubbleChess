using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiXianCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;
	public bool isSuccessRelease = false;
	private string clickTrans;
	public DiXianCommand(ref int[][] chessPieceArrays, int pieceType, string clickTrans)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
		this.clickTrans = clickTrans;
	}
	public void Execute()
	{
		int x = int.Parse(clickTrans.Split(',')[0]);
		int y = int.Parse(clickTrans.Split(',')[1]);
		if (boardChessArrays[x][y] == 0)
		{
			isSuccessRelease = true;
			//TODO:下回合对手下棋位置如果是clickTrans则消灭
			Debug.Log("TODO:下回合对手下棋位置如果是" + clickTrans + "则消灭");
		}
	}
}

