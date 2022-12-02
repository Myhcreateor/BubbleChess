using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_CiKe : Character
{
	private int[][] boardChessArrays;
	private int pieceType;
	private int[,] offsetArray = new int[8, 2] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 }, { 1, 1 }, { -1, 1 }, { -1, -1 }, { 1, -1 } };
	private List<string> DoubleHitSkillList = new List<string>();
	public Character_CiKe(ref int[][] chessPieceArrays, int pieceType)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
		isFiveRoundDetection = true;
	}

	public override void PassiveSkill()
	{
		ShowSkillImage("LianJi");
		DoubleHitSkill(ref boardChessArrays, pieceType);
		EventHandler.CallUpdateChessBoardEvent();
	}
	public void DoubleHitSkill(ref int[][] chessPieceArrays, int pieceType)
	{
		int max = int.MinValue;
		int num = int.MinValue;
		int num1 = int.MinValue;
		int num2 = int.MinValue;
		string maxStr = "";
		int enemyType = 0;
		if (pieceType == 1) enemyType = 2;
		else enemyType = 1;
		for (int x = 0; x < 8; x++)
		{
			for (int y = 0; y < 8; y++)
			{
				if (boardChessArrays[x][y] == 0)
				{
					int currentNum = ChessBoardController.Instance.CalculateScoreGap(pieceType);
					boardChessArrays[x][y] = pieceType;
					num1 = ChessBoardController.Instance.CalculateScoreGap(pieceType) - currentNum;
					boardChessArrays[x][y] = enemyType;
					num2 = ChessBoardController.Instance.CalculateScoreGap(enemyType) + currentNum;
					num = num1 + num2;
					if (num > max)
					{
						max = num;
						maxStr = x.ToString() + ',' + y.ToString();
					}
					boardChessArrays[x][y] = 0;
				}
			}
		}
		
		int maxStrx = int.Parse(maxStr.Split(',')[0]);
		int maxStry = int.Parse(maxStr.Split(',')[1]);
		boardChessArrays[int.Parse(maxStr.Split(',')[0])][int.Parse(maxStr.Split(',')[1])] = pieceType;
		
	}
}
