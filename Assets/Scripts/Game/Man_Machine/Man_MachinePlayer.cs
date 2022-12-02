using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man_MachinePlayer :MonoBehaviour
{
	private Character character;
	private int[,] offsetArray = new int[8, 2] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 }, { 1, 1 }, { -1, 1 }, { -1, -1 }, { 1, -1 } };
	private int roundNum;
	public void SetCharacter(Character character)
	{
		this.character = character;
	}
	public Character GetCharacter()
	{
		return character;
	}
	public void Update()
	{
		if (character.isReleaseSkill)
		{
			character.PassiveSkill();
			character. isReleaseSkill = false;
		}
	}
	public void Man_MachineFindChessTran(ref int[][] boardChessArrays, int pieceType)
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
					num1 = ChessBoardController.Instance.CalculateScoreGap(pieceType)- currentNum;
					boardChessArrays[x][y] = enemyType;
					num2 = ChessBoardController.Instance.CalculateScoreGap(enemyType)+currentNum;
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
		if (max <= 0)
		{
			bool flag = false;
			List<string> transList = new List<string>();
			for (int x = 0; x < 8; x++)
			{
				for (int y = 0; y < 8; y++)
				{
					if (boardChessArrays[x][y] == pieceType)
					{
						for (int i = 0; i < offsetArray.GetLength(0); i++)
						{
							if ((x + offsetArray[i, 0] >= 0) && (x + offsetArray[i, 0] < 8) && (y + offsetArray[i, 1] < 8) && (y + offsetArray[i, 1] >= 0))
							{
								if (boardChessArrays[x + offsetArray[i, 0]][y + offsetArray[i, 1]] == 0)
								{
									int xOffset = offsetArray[i, 0] * 2;
									int yOffset = offsetArray[i, 1] * 2;
									if ((x + xOffset >= 0) && (x + xOffset < 8) && (y + yOffset < 8) && (y + yOffset >= 0))
									{
										if (boardChessArrays[x + xOffset][y + yOffset] == pieceType)
										{
											maxStr = (x + offsetArray[i, 0]).ToString() + ',' + (y + offsetArray[i, 1]).ToString();
											flag = true;
										}
										else if (boardChessArrays[x + xOffset][y + yOffset] == 0)
										{
											transList.Add((x + offsetArray[i, 0]).ToString() + ',' + (y + offsetArray[i, 1]).ToString());
										}
									}

								}
							}
						}
					}
				}
			}
			if (flag == false)
			{
				if (transList.Count == 0)
				{
					for (int x = 0; x < 8; x++)
					{
						for (int y = 0; y < 8; y++)
						{
							if (boardChessArrays[x][y] == enemyType)
							{
								for(int i = 0; i < offsetArray.GetLength(0); i++)
								{
									if ((x + offsetArray[i, 0] >= 0) && (x + offsetArray[i, 0] < 8) && (y + offsetArray[i, 1] < 8) && (y + offsetArray[i, 1] >= 0))
									{ transList.Add((x + offsetArray[i, 0]).ToString() + ',' + (y + offsetArray[i, 1]).ToString()); }
								}
								
							}
						}
					}
				}
				int randomNum = Random.Range(0, transList.Count);
				maxStr = transList[randomNum];
			}
		}
		int maxStrx = int.Parse(maxStr.Split(',')[0]);
		int maxStry = int.Parse(maxStr.Split(',')[1]);
		boardChessArrays[int.Parse(maxStr.Split(',')[0])][int.Parse(maxStr.Split(',')[1])] = pieceType;
		if (character.isFiveConsecutive)
		{
			character.isReleaseSkill= IsGeneratedFiveConsecutive(boardChessArrays, maxStrx, maxStry, pieceType);
		}
		if (character.isFiveRoundDetection)
		{
			if (ChessBoardController.Instance.twoSideRoundNum!=0&&(ChessBoardController.Instance.twoSideRoundNum / 2 % 5) == 0)
			{
				character.isReleaseSkill = true;
			}
		}
	}

	public bool IsGeneratedFiveConsecutive(int[][] boardChessArrays, int x, int y, int pieceType)
	{
		int[,] offsetArray = new int[4, 2] { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 1, -1 } };
		for (int k = 0; k < offsetArray.GetLength(0); k++)
		{
			int length = 1;
			int offsetx = offsetArray[k, 0];
			int offsety = offsetArray[k, 1];

			while ((x + offsetx >= 0) && (x + offsetx < 8) && (y + offsety < 8) && (y + offsety >= 0) && boardChessArrays[x + offsetx][y + offsety] == pieceType)
			{
				offsetx += offsetArray[k, 0];
				offsety += offsetArray[k, 1];
				length++;
			}
			offsetx = -offsetArray[k, 0];
			offsety = -offsetArray[k, 1];
			while ((x + offsetx >= 0) && (x + offsetx < 8) && (y + offsety < 8) && (y + offsety >= 0) && boardChessArrays[x + offsetx][y + offsety] == pieceType)
			{
				offsetx -= offsetArray[k, 0];
				offsety -= offsetArray[k, 1];
				length++;
			}
			if (length >= 5)
			{
				return true;
			}
		}
		return false;

	}
}



