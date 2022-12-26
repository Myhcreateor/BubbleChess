using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XingYiCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;
	public bool isSuccessRelease = false;
	private string clickTrans;
	private List<string> adjacentSpaceList = new List<string>();
	public XingYiCommand(ref int[][] chessPieceArrays, int pieceType, string clickTrans)
	{
		this.boardChessArrays = chessPieceArrays;
		if (GameController.Instance.gameMode != GameMode.Stand_Alone)
		{
			this.pieceType = pieceType;
		}
		else
		{
			if (ChessBoardController.Instance.GetPlayer() == Player.One)
			{
				this.pieceType = 1;
			}
			else
			{
				this.pieceType = 2;
			}
		}
		this.clickTrans = clickTrans;
	}
	public void Execute()
	{
		int x = int.Parse(clickTrans.Split(',')[0]);
		int y = int.Parse(clickTrans.Split(',')[1]);
		int max =-100;
		int num = ChessBoardController.Instance.CalculateScoreGap(pieceType);
		string maxStr = "";
		if (boardChessArrays[x][y] == pieceType)
		{
			if (IsHasAdjacentSpace(x,y,pieceType))
			{
				
				boardChessArrays[x][y] = 0;
				for(int i=0;i< adjacentSpaceList.Count; i++)
				{
					boardChessArrays[int.Parse(adjacentSpaceList[i].Split(',')[0])][int.Parse(adjacentSpaceList[i].Split(',')[1])] = pieceType;
					int index= ChessBoardController.Instance.CalculateScoreGap(pieceType);
					if (index > max)
					{
						max = index;
						maxStr = adjacentSpaceList[i];
					}
					boardChessArrays[int.Parse(adjacentSpaceList[i].Split(',')[0])][int.Parse(adjacentSpaceList[i].Split(',')[1])] = 0;
				}
				for (int i = 0; i < adjacentSpaceList.Count; i++)
				{
					int emenyType = 0;
					if (pieceType == 1) emenyType = 2;
					else emenyType = 1;
					boardChessArrays[int.Parse(adjacentSpaceList[i].Split(',')[0])][int.Parse(adjacentSpaceList[i].Split(',')[1])] = emenyType;
					int index = ChessBoardController.Instance.CalculateScoreGap(emenyType);
					if (index > max)
					{
						max = index;
						maxStr = adjacentSpaceList[i];
					}
					boardChessArrays[int.Parse(adjacentSpaceList[i].Split(',')[0])][int.Parse(adjacentSpaceList[i].Split(',')[1])] = 0;
				}
				if (num >= max)
				{
					int randomNum = Random.Range(0, adjacentSpaceList.Count);
					boardChessArrays[int.Parse(adjacentSpaceList[randomNum].Split(',')[0])][int.Parse(adjacentSpaceList[randomNum].Split(',')[1])] = pieceType;
				}
				else
				{
					boardChessArrays[int.Parse(maxStr.Split(',')[0])][int.Parse(maxStr.Split(',')[1])] = pieceType;
				}
				isSuccessRelease = true;
			}
		}
	}

	public bool IsHasAdjacentSpace(int x,int y,int pieceType)
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
