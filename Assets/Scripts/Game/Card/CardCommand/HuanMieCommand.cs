using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuanMieCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;
	public bool isSuccessRelease = false;
	private string clickTrans;
	public HuanMieCommand(ref int[][] chessPieceArrays, int pieceType,string clickTrans)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
		this.clickTrans = clickTrans;
	}
	public void Execute()
	{
		int x =int.Parse( clickTrans.Split(',')[0]);
		int y = int.Parse(clickTrans.Split(',')[1]);
		if (boardChessArrays[x][y] == pieceType)
		{
			//消除非连对的棋子并自动更新棋盘
			//if(boardChessArrays[x+1][y]== pieceType&& boardChessArrays[x + 2][y] == pieceType)
			boardChessArrays[x][y] = 0;
			EventHandler.CallUpdateChessBoardEvent();
			isSuccessRelease = true;
		}

	}
}
