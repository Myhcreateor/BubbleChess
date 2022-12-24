using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiHuanCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;
	public bool isSuccessRelease = false;
	private string clickTrans;
	private int[,] offsetArray = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };
	private List<string> adjoinEnemyPieceList = new List<string>();
	public ZhiHuanCommand(ref int[][] chessPieceArrays, int pieceType, string clickTrans)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
		this.clickTrans = clickTrans;
	}
	public void Execute()
	{
		int x = int.Parse(clickTrans.Split(',')[0]);
		int y = int.Parse(clickTrans.Split(',')[1]);
		int emenyType = 0;
		if (pieceType == 1) emenyType = 2;
		else emenyType = 1;
		if (boardChessArrays[x][y] == pieceType)
		{
			for (int i = 0; i < offsetArray.GetLength(0); i++)
			{
				if ((x + offsetArray[i, 0]>=0)&&( x + offsetArray[i, 0]<8)&& (y +offsetArray[i, 1] < 8) && (y + offsetArray[i, 1] >=0))
				{
					if(boardChessArrays[x + offsetArray[i, 0]][y + offsetArray[i, 1]] == emenyType)
					{
						adjoinEnemyPieceList.Add((x + offsetArray[i, 0]).ToString() + "," + (y + offsetArray[i, 1]).ToString());
					}
				}
			}
			if (adjoinEnemyPieceList.Count > 0)
			{
				boardChessArrays[x][y] = emenyType;
				int randomNum = Random.Range(0, adjoinEnemyPieceList.Count);
				string s = adjoinEnemyPieceList[randomNum];
				boardChessArrays[int.Parse(s.Split(',')[0])][int.Parse(s.Split(',')[1])]= pieceType;
				isSuccessRelease = true;
			}

		}
	}

	//public string AdjoinEnemyTrans()
	//{

	//}
}

