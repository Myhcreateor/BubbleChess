using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiHuanCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;
	public bool isSuccessRelease = false;
	private string clickTrans;
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
			if (boardChessArrays[x - 1][y] == emenyType)
			{
				adjoinEnemyPieceList.Add((x - 1).ToString() + "," + y.ToString());
			}
			if (boardChessArrays[x + 1][y] == emenyType)
			{
				adjoinEnemyPieceList.Add((x + 1).ToString() + "," + y.ToString());
			}
			if (boardChessArrays[x][y - 1] == emenyType)
			{
				adjoinEnemyPieceList.Add((x).ToString() + "," + (y - 1).ToString());
			}
			if (boardChessArrays[x][y + 1] == emenyType)
			{
				adjoinEnemyPieceList.Add((x).ToString() + "," + (y + 1).ToString());
			}
			if (adjoinEnemyPieceList.Count > 0)
			{
				isSuccessRelease = true;
				boardChessArrays[x][y] = emenyType;
				int randomNum = Random.Range(0, adjoinEnemyPieceList.Count);
				string s = adjoinEnemyPieceList[randomNum];
				boardChessArrays[int.Parse(s.Split(',')[0])][int.Parse(s.Split(',')[1])]= pieceType;
			}

		}
	}

	//public string AdjoinEnemyTrans()
	//{

	//}
}

