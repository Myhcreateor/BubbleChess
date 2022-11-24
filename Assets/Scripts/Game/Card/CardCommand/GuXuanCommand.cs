using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuXuanCommand :ICommand
{
	private  int[][] boardChessArrays;
	private int pieceType;
	public string[] str=new string[2];
	public GuXuanCommand(ref int[][] chessPieceArrays,int pieceType)
	{
		this.boardChessArrays = chessPieceArrays;
		this.pieceType = pieceType;
	}


	public void Execute()
	{
		for (int i = 0; i < 2&& !isFullChessBoard();)
		{
			int randomX = Random.Range(0, 8);
			int randomY = Random.Range(0, 8);
			if (boardChessArrays[randomX][randomY] == 0)
			{
				//Debug.Log("棋盘中x:" + randomX + "y:" + randomY + "改变成了" + pieceType);
				bool isAroundHasPiece = false;
				for (int j = -1; j <= 1; j++)
				{
					for(int k = -1; k <= 1; k++)
					{
						if((randomY + k) >= 0 && (randomY + k) < 8 && (randomX + j) >= 0 && (randomX + j) < 8)
						{
							if (boardChessArrays[randomX + j][randomY + k] == pieceType)
							{
								isAroundHasPiece = true;
							}
						}
						else
						{
							continue;
						}
						
					}
				}
				if (!isAroundHasPiece)
				{
					boardChessArrays[randomX][randomY] = pieceType;
				}
				str[i] = randomX.ToString() + "," + randomY.ToString();
				i++;
			}
		}
	}
	public bool isFullChessBoard()
	{
		bool isfull = true;
		for (int j = 0; j < 8; j++)
		{
			for (int k = 0; k < 8; k++)
			{
				if (boardChessArrays[j][k] == 0)
				{
					isfull = false;
				}
			}
		}
		return isfull;
	}
}
