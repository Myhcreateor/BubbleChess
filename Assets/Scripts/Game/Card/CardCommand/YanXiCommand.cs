using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YanXiCommand : ICommand
{
    private int[][] boardChessArrays;
    private int pieceType;
	private List<string> chessBoardSpaceList = new List<string>();

	public YanXiCommand(ref int[][] chessPieceArrays, int pieceType)
    {
        this.boardChessArrays = chessPieceArrays;
        this.pieceType = pieceType;
    }
    public void Execute()
	{
		string s = FindChessBoardSpace();
		boardChessArrays[int.Parse(s.Split(',')[0])][int.Parse(s.Split(',')[1])] = pieceType;
		CardManager.Instance.GenerateRandomCard();
		CardManager.Instance.GenerateRandomCard();
	}
	public string  FindChessBoardSpace()
	{
		string randomSpaceTrans = "";
		for (int j = 0; j < 8; j++)
		{
			for (int k = 0; k < 8; k++)
			{
				if (boardChessArrays[j][k] == 0)
				{
					chessBoardSpaceList.Add(j.ToString() + "," + k.ToString());
				}
			}
		}
		int count =Random.Range(0, chessBoardSpaceList.Count);
		randomSpaceTrans = chessBoardSpaceList[count];
		return randomSpaceTrans;
	}
}
