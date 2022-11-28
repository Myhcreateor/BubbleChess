using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuLiCommand : ICommand
{
	private int[][] boardChessArrays;
	private int pieceType;
	private int enemyTpye;
	public bool isSuccessRelease = false;
	private string clickTrans;
	private int[,] offsetArray = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };
	private List<string> adjoinOwnPieceList = new List<string>();
	private List<string> adjoinSpaceList = new List<string>();
	public QuLiCommand(ref int[][] chessPieceArrays, int pieceType, string clickTrans)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
		this.clickTrans = clickTrans;
		if (pieceType == 1) enemyTpye = 2;
		else enemyTpye = 1;
	}
	public void Execute()
	{
		int x = int.Parse(clickTrans.Split(',')[0]);
		int y = int.Parse(clickTrans.Split(',')[1]);
		if (boardChessArrays[x][y] == enemyTpye)
		{
			if(IsAdjoinAllOwnPiece(x, y, pieceType))//周围都是己方棋子
			{
				boardChessArrays[x][y] = pieceType;
				isSuccessRelease = true;
			}
			else
			{
				if (adjoinSpaceList.Count > 0)
				{
					int randomNum = Random.Range(0, adjoinSpaceList.Count);
					string s = adjoinSpaceList[randomNum];
					boardChessArrays[x][y] = 0;
					boardChessArrays[int.Parse(s.Split(',')[0])][int.Parse(s.Split(',')[1])] = enemyTpye;
					isSuccessRelease = true;
				}
			}
		}
	}

    public bool IsAdjoinAllOwnPiece(int x, int y,int pieceType)
	{
		int num = 0;
		for (int i = 0; i < offsetArray.GetLength(0); i++)
		{
			num++;
			if ((x + offsetArray[i, 0] > 0) && (x + offsetArray[i, 0] < 8) && (y + offsetArray[i, 1] < 8) && (y + offsetArray[i, 1] > 0))
			{
				if (boardChessArrays[x + offsetArray[i, 0]][y + offsetArray[i, 1]] == pieceType)
				{
					adjoinOwnPieceList.Add((x + offsetArray[i, 0]).ToString() + "," + (y + offsetArray[i, 1]).ToString());
				}
				else if(boardChessArrays[x + offsetArray[i, 0]][y + offsetArray[i, 1]] == 0)
				{
					adjoinSpaceList.Add((x + offsetArray[i, 0]).ToString() + "," + (y + offsetArray[i, 1]).ToString());
				}
			}
		}
		if (adjoinOwnPieceList.Count == num)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

