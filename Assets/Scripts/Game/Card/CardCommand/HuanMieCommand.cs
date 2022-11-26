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
			if(!isHasAdjacentPiece(x,y, pieceType))
			{
				boardChessArrays[x][y] = 0;
				EventHandler.CallUpdateChessBoardEvent();
				isSuccessRelease = true;
				//TODO:对手增加一张手牌
			}
		}

	}
	public bool isHasAdjacentPiece(int x,int y,int pieceType)
	{
		for(int i = -1; i <= 1; i++)
		{
			for(int j = -1; j <= 1; j++)
			{
				if (i == 0 && j == 0) continue;
				if((i +x) >= 0 && (i + x) < 8 && (y + j) >= 0 && (y + j) < 8)
				{
					if (boardChessArrays[x + i][y + j] == pieceType)
					{
						return true;
					}
				}
				else
				{
					continue;
				}
			}
		}
		return false;
	}
}
