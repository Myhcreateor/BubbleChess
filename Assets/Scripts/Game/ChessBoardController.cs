using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardController : Singleton<ChessBoardController>
{
	public ChessBoardModel chessBoardModel;
	public int blackScore;
	public int writeScore;
	public List<GameObject> piecesList;

	//�������������
	private int[][] chessPieceArrays = new int[8][];
	private int[][] transposeArrays = new int[8][];
	private int[][] reversalArrays = new int[8][];
	private int[][] slantArrays = new int[15][];
	private int[][] transposeReversalArray = new int[15][];
	private void Awake()
	{
		base.Awake();
		InitChessPieceArrays();
		piecesList = chessBoardModel.piecesList;
	}

	//��ʼ����������
	private void InitChessPieceArrays()
	{
		for (int col = 0; col < 8; col++)//8��
		{
			chessPieceArrays[col] = new int[8];
			transposeArrays[col] = new int[8];
		}
		for (int col = 0; col < 8; col++)//8��
		{
			for (int row = 0; row < 8; row++)//8��
			{
				chessPieceArrays[col][row] = 0;
			}
		}
	}
	//�жϻغ����Ƿ����
	public bool isRoundOver(int pieceNum)
	{
		if (chessBoardModel.roundNum * 2 <= pieceNum)
		{
			//��Ϸ����
			EventHandler.CallGameOverEvent();
			return true;
		}
		return false;
	}
	//�������̣�����Ǻ�ɫ����ֵΪ1����ɫ����ֵΪ2
	public void UpdateChessPieceArrays(int col, int row, int pieceType)
	{
		chessPieceArrays[col][row] = pieceType;
	}
	/// <summary>
	/// �����������
	/// </summary>
	public void CalculateScore()
	{
		blackScore = 0;
		writeScore = 0;
		for (int index = 0; index < 8; index++)
		{
			CalculateLineScore(chessPieceArrays[index]);
		}
		transposeArrays = TransposeArray(chessPieceArrays);
		for (int index = 0; index < 8; index++)
		{
			CalculateLineScore(transposeArrays[index]);
		}
		slantArrays = SlantArray(chessPieceArrays);
		for (int index = 0; index < 15; index++)
		{
			CalculateLineScore(slantArrays[index]);
		}
		reversalArrays = ReversalArray(chessPieceArrays);
		transposeReversalArray = SlantArray(reversalArrays);
		for (int index = 0; index < 15; index++)
		{
			CalculateLineScore(transposeReversalArray[index]);
		}
	}
	//��ת����
	public int[][] ReversalArray(int[][] arr)
	{
		int[][] arrNew = new int[8][];
		for (int col = 0; col < 8; col++)
		{
			arrNew[col] = new int[8];
		}
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				arrNew[i][j] = arr[i][7-j];
			}
		}
		return arrNew;
	}
	//ת������
	public int[][] TransposeArray(int[][] arr)
	{
		int[][] arrNew = new int[8][];
		for (int col = 0; col < 8; col++)
		{
			arrNew[col] = new int[8];
		}
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				arrNew[j][i] = arr[i][j];
			}
		}
		return arrNew;
	}
	//б������
	public int[][] SlantArray(int[][] arr)
	{
		int[][] arrNew = new int[15][];
		for (int col = 0; col < 15; col++)
		{
			arrNew[col] = new int[15];
			for (int row = 0; row < 8; row++)
			{
				arrNew[col][row] = 0;
			}
		}
		for (int col = 0; col < 15; col++)
		{
			int k = 0;
			arrNew[col] = new int[15];
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (i + j == col)
					{
						arrNew[col][k++] = arr[i][j];
					}
				}
			}
		}
		return arrNew;
	}
	public void CalculateLineScore(int[] line)
	{
		string s = "";
		for (int i = 0; i < line.Length; i++)
		{
			s += line[i].ToString();
		}
		s += '0';
		blackScore += CheckOneLine(s)[0];
		writeScore += CheckOneLine(s)[1];
		//Debug.Log(s);
		//UI��ʾ����
		EventHandler.CallShowScoreEvent(blackScore,writeScore);
	}

	public int[] CheckOneLine(string line)
	{
		int writeScore = 0;
		int blackScore = 0;
		int currentLinkNum = 1;
		int currentType = 0;
		foreach (var i in line)
		{
			if (i == '1')
			{
				if (currentType != 1)
				{
					if (currentType == 2)
					{
						writeScore += chessBoardModel.GetScore(currentLinkNum);
					}
					currentLinkNum = 1;
					currentType = 1;
				}
				else
				{
					currentLinkNum++;
				}
			}
			else if (i == '2')
			{
				if (currentType != 2)
				{
					if (currentType == 1)
					{
						blackScore += chessBoardModel.GetScore(currentLinkNum);
					}
					currentLinkNum = 1;
					currentType = 2;
				}
				else
				{
					currentLinkNum++;
				}
			}
			else if (i == '0')
			{
				if (currentType == 1)
				{
					blackScore += chessBoardModel.GetScore(currentLinkNum);
				}
				else if (currentType == 2)
				{
					writeScore += chessBoardModel.GetScore(currentLinkNum);
				}
				currentLinkNum = 1;
				currentType = 0;
			}
		}
		return new int[2] { blackScore, writeScore };
	}

}
