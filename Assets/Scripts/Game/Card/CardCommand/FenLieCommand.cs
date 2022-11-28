using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenLieCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;
	public bool isSuccessRelease = false;
	private string clickTrans;
	private List<string> adjacentSpaceList = new List<string>();
	public FenLieCommand(ref int[][] chessPieceArrays, int pieceType, string clickTrans)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
		this.clickTrans = clickTrans;
	}
	public void Execute()
	{
		int x = int.Parse(clickTrans.Split(',')[0]);
		int y = int.Parse(clickTrans.Split(',')[1]);
		if (boardChessArrays[x][y] == pieceType)
		{
			if (IsHasAdjacentSpace(x, y, pieceType))
			{

				boardChessArrays[x][y] = 0;
				if (adjacentSpaceList.Count > 0)
				{
					int randomNum = Random.Range(0, adjacentSpaceList.Count);
					string s = adjacentSpaceList[randomNum];
					boardChessArrays[int.Parse(s.Split(',')[0])][int.Parse(s.Split(',')[1])] = pieceType;
					adjacentSpaceList.RemoveAt(randomNum);
				}
				if (adjacentSpaceList.Count > 0)
				{
					int randomNum = Random.Range(0, adjacentSpaceList.Count);
					string s = adjacentSpaceList[randomNum];
					boardChessArrays[int.Parse(s.Split(',')[0])][int.Parse(s.Split(',')[1])] = pieceType;
					adjacentSpaceList.RemoveAt(randomNum);
				}
				isSuccessRelease = true;
			}
		}
	}

	public bool IsHasAdjacentSpace(int x, int y, int pieceType)
	{
		bool isHasAdjacentSpace = false;
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if (i == 0 && j == 0) continue;
				if ((i + x) >= 0 && (i + x) < 8 && (y + j) >= 0 && (y + j) < 8)
				{
					if (boardChessArrays[x + i][y + j] == 0)
					{
						isHasAdjacentSpace = true;
						adjacentSpaceList.Add((x + i).ToString() + "," + (y + j).ToString());
					}
				}
				else
				{
					continue;
				}
			}
		}
		return isHasAdjacentSpace;
	}
}
